using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages all aspects of the game play including moving the player,
    ///     the vehicles as well as lives and score.
    /// </summary>
    public class GameManager
    {
        #region Data members

        private const int BottomLaneOffset = 5;

        private int currentProgressBarCount;
        private double playerXMinimum;
        private double playerYMinimum;
        private double playerXMaximum;
        private double playerYMaximum;
        private readonly double backgroundHeight;
        private readonly double backgroundWidth;
        private Canvas gameCanvas;
        private Player player;
        private DispatcherTimer timer;
        private DispatcherTimer deathTimer;
        private DispatcherTimer scoringTimer;
        private readonly RoadManager roadManager;
        private readonly PlayerStats playerStats;
        private IList<FrogHome> availableHomes;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        /// </summary>
        /// <param name="backgroundHeight">Height of the background.</param>
        /// <param name="backgroundWidth">Width of the background.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     backgroundHeight &lt;= 0
        ///     or
        ///     backgroundWidth &lt;= 0
        /// </exception>
        public GameManager(double backgroundHeight, double backgroundWidth)
        {
            if (backgroundHeight <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundHeight));
            }

            if (backgroundWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundWidth));
            }

            this.backgroundHeight = backgroundHeight;
            this.backgroundWidth = backgroundWidth;
            this.roadManager = new RoadManager();
            this.playerStats = new PlayerStats();

            this.setupGameTimer();
        }

        #endregion

        #region Methods

        /// <summary>Occurs when a life is lost.</summary>
        public event EventHandler<LifeLostEventArgs> LifeLost;

        /// <summary>Occurs when score increases</summary>
        public event EventHandler<ScoreIncreasedEventArgs> ScoreIncreased;

        /// <summary>Occurs when the game is over.</summary>
        public event EventHandler<EventArgs> GameOver;

        /// <summary>Occurs when the progress bar should increase.</summary>
        public event EventHandler<ProgressBarArgs> ProgressBarIncrease;

        /// <summary>
        ///     Initializes the game working with appropriate classes to play frog
        ///     and gameObject on game screen.
        ///     Precondition: background != null
        ///     Postcondition: Game is initialized and ready for play.
        /// </summary>
        /// <param name="gamePage">The game page.</param>
        /// <exception cref="ArgumentNullException">gameCanvas</exception>
        public void InitializeGame(Canvas gamePage)
        {
            this.gameCanvas = gamePage ?? throw new ArgumentNullException(nameof(gamePage));
            this.createAndPlacePlayer();
            this.placeAllVehicles();
            this.populateAvailableHomes();
            this.placeFrogHomesAtTopOfRoad();

            this.createScoringTimer();
        }

        private void createScoringTimer()
        {
            this.scoringTimer = new DispatcherTimer();
            this.scoringTimer.Tick += this.scoringTimerTick;
            this.scoringTimer.Interval = new TimeSpan(0, 0, 0, 0, 80);
            this.scoringTimer.Start();
        }

        private void setupGameTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.onGameTimerTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 15);
            this.timer.Start();
        }

        private void scoringTimerTick(object sender, object args)
        {
            this.currentProgressBarCount++;
            this.ProgressBarIncrease?.Invoke(this, new ProgressBarArgs {ProgressValue = this.currentProgressBarCount});
            if (this.currentProgressBarCount == DefaultValues.ScoringTimerMaximum)
            {
                this.resetProgressBar();
                this.executeLifeLostOperations();
            }
        }

        private void executeLifeLostOperations()
        {
            this.playerStats.DecreaseLivesByOne();
            this.detectGameOver();
            this.resetProgressBar();
            var lifeArgs = new LifeLostEventArgs {Lives = this.playerStats.Lives};
            this.LifeLost?.Invoke(this, lifeArgs);
            this.roadManager.ResetVehicleSpeeds();
            this.showDeathAnimation();
            this.setPlayerToCenterOfBottomLane();
        }

        private void resetProgressBar()
        {
            this.currentProgressBarCount = 0;
            this.ProgressBarIncrease?.Invoke(this,
                new ProgressBarArgs {ProgressValue = this.currentProgressBarCount});
        }

        private void populateAvailableHomes()
        {
            this.availableHomes = new List<FrogHome> {
                new FrogHome(),
                new FrogHome(),
                new FrogHome(),
                new FrogHome(),
                new FrogHome()
            };
        }

        private void placeFrogHomesAtTopOfRoad()
        {
            var homeWidth = this.availableHomes[0].Width;
            var totalHomesWidthWithSpaces = this.availableHomes.Count * homeWidth * 2 - homeWidth;
            var canvasWidth = this.gameCanvas.Width;
            var currentXCoordinate = (canvasWidth - totalHomesWidthWithSpaces) / 2;

            foreach (var home in this.availableHomes)
            {
                this.gameCanvas.Children.Add(home.Sprite);
                home.X = currentXCoordinate;
                home.Y = DefaultValues.DefaultLanes[4].YCoordinate - home.Height;
                currentXCoordinate += homeWidth * 2;
            }
        }

        private void placeAllVehicles()
        {
            var vehicles = this.roadManager.GetAllVehicles();
            foreach (var vehicle in vehicles)
            {
                this.addGameObjectToCanvas(vehicle);
            }
        }

        private void createAndPlacePlayer()
        {
            this.player = new Frog();
            this.addGameObjectToCanvas(this.player);
            this.player.DeathAnimation.ToList().ForEach(this.addGameObjectToCanvas);
            this.player.DeathAnimation.ToList().ForEach(frame => frame.Sprite.Visibility = Visibility.Collapsed);
            this.setPlayerBoundaries();
            this.setPlayerToCenterOfBottomLane();
        }

        private void addGameObjectToCanvas(GameObject gameObject)
        {
            this.gameCanvas.Children.Add(gameObject.Sprite);
        }

        private void setPlayerBoundaries()
        {
            this.playerXMaximum = this.gameCanvas.Width - this.player.Width;
            this.playerYMaximum = this.gameCanvas.Height - this.player.Height;
            this.playerYMinimum = DefaultValues.DefaultLanes[4].YCoordinate;
            this.playerXMinimum = 0;
        }

        private void setPlayerToCenterOfBottomLane()
        {
            this.player.X = this.backgroundWidth / 2 - this.player.Width / 2;
            this.player.Y = this.backgroundHeight - this.player.Height - BottomLaneOffset;
        }

        private void onGameTimerTick(object sender, object e)
        {
            this.roadManager.MoveAllVehicles();
            this.detectCollisionOfPlayerAndVehicle();
            this.detectSuccessfulScore();
        }

        private void detectSuccessfulScore()
        {
            if (this.playerSuccessfullyCrossedRoad())
            {
                this.detectCollisionBetweenFrogAndHome();
                this.playerStats.IncreaseScore((DefaultValues.ScoringTimerMaximum - this.currentProgressBarCount) * 10);
                var scoreIncreasedArgs = new ScoreIncreasedEventArgs {Score = this.playerStats.Score};
                this.resetProgressBar();
                this.ScoreIncreased?.Invoke(this, scoreIncreasedArgs);
                this.detectGameOver();
                this.setPlayerToCenterOfBottomLane();
            }
        }

        private bool playerAtLocationCollidesWithHome(double x, double y)
        {
            var playerBoundingBox = new Rectangle((int) x, (int) y, (int) this.player.Width, (int) this.player.Height);
            foreach (var home in this.availableHomes)
            {
                var homeBoundingBox = this.createGameObjectBoundingBox(home);
                if (playerBoundingBox.IntersectsWith(homeBoundingBox))
                {
                    return true;
                }
            }

            return false;
        }

        private void detectCollisionBetweenFrogAndHome()
        {
            var playerBoundingBox = this.createGameObjectBoundingBox(this.player);
            foreach (var home in this.availableHomes)
            {
                var homeBoundingBox = this.createGameObjectBoundingBox(home);
                if (this.collisionBetweenTheseObjects(homeBoundingBox, playerBoundingBox))
                {
                    home.ShowSprite();
                    this.availableHomes.Remove(home);
                    break;
                }
            }
        }

        private bool playerSuccessfullyCrossedRoad()
        {
            return this.player.Y < DefaultValues.DefaultLanes[4].YCoordinate;
        }

        private void detectGameOver()
        {
            if (this.playerLivesIsZero() || this.availableHomesIsEmpty())
            {
                this.timer.Stop();
                this.roadManager.StopTimer();
                this.player.Freeze();
                var gameOverArgs = new EventArgs();
                this.GameOver?.Invoke(this, gameOverArgs);
            }
        }

        private bool availableHomesIsEmpty()
        {
            return this.availableHomes.Count == 0;
        }

        private bool playerLivesIsZero()
        {
            return this.playerStats.Lives == 0;
        }

        private void detectCollisionOfPlayerAndVehicle()
        {
            var playerBoundingBox = this.createGameObjectBoundingBox(this.player);
            foreach (var vehicle in this.roadManager.GetAllActiveVehicles())
            {
                var vehicleBoundingBox = this.createGameObjectBoundingBox(vehicle);
                if (this.collisionBetweenTheseObjects(vehicleBoundingBox, playerBoundingBox))
                {
                    this.executeLifeLostOperations();
                }
            }
        }

        private void showDeathAnimation()
        {
            this.createDeathTimer();
            this.player.Sprite.Visibility = Visibility.Collapsed;
            this.setPlayerToCenterOfBottomLane();
            this.player.Freeze();
            this.player.DeathAnimation[0].Sprite.Visibility = Visibility.Visible;
            this.deathTimer.Start();
        }

        private void createDeathTimer()
        {
            this.deathTimer = new DispatcherTimer();
            this.deathTimer.Tick += this.deathTimerOnTick;
            this.deathTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
        }

        private void deathTimerOnTick(object sender, object args)
        {
            var visibleFrameIndex = this.getVisibleFrameIndexInPlayerDeathAnimations();
            this.player.DeathAnimation[visibleFrameIndex].Sprite.Visibility = Visibility.Collapsed;

            if (!(visibleFrameIndex + 1 >= this.player.DeathAnimation.Count))
            {
                this.player.DeathAnimation[visibleFrameIndex + 1].Sprite.Visibility = Visibility.Visible;
            }
            else
            {
                this.player.DeathAnimation[visibleFrameIndex].Sprite.Visibility = Visibility.Collapsed;
                this.player.Sprite.Visibility = Visibility.Visible;
                this.player.Unfreeze();
                this.deathTimer.Stop();
                this.detectGameOver();
            }
        }

        private int getVisibleFrameIndexInPlayerDeathAnimations()
        {
            foreach (var frame in this.player.DeathAnimation)
            {
                if (frame.Sprite.Visibility == Visibility.Visible)
                {
                    return this.player.DeathAnimation.IndexOf(frame);
                }
            }

            return -1;
        }

        private bool collisionBetweenTheseObjects(Rectangle playerBoundingBox, Rectangle vehicleBoundingBox)
        {
            return playerBoundingBox.IntersectsWith(vehicleBoundingBox);
        }

        private Rectangle createGameObjectBoundingBox(GameObject gameObject)
        {
            return new Rectangle((int) gameObject.X, (int) gameObject.Y, (int) gameObject.Width,
                (int) gameObject.Height);
        }

        /// <summary>
        ///     Moves the player to the left.
        ///     Precondition: player.X - player.Width must be greater than playerXMinimum
        ///     Postcondition: player.X = player.X@prev - player.Width
        /// </summary>
        public void MovePlayerLeft()
        {
            if (!this.playerAtLeftSideOfScreen())
            {
                this.player.MoveLeft();
            }
        }

        private bool playerAtLeftSideOfScreen()
        {
            return this.player.X - this.player.Width < this.playerXMinimum;
        }

        /// <summary>
        ///     Moves the player to the right.
        ///     Precondition: player.X + player.Width must be less than playerXMaximum
        ///     Postcondition: player.X = player.X@prev + player.Width
        /// </summary>
        public void MovePlayerRight()
        {
            if (!this.playerIsAtRightSideOfScreen())
            {
                this.player.MoveRight();
            }
        }

        private bool playerIsAtRightSideOfScreen()
        {
            return this.player.X + this.player.Width > this.playerXMaximum;
        }

        /// <summary>
        ///     Moves the player up.
        ///     Precondition: player.Y - player.Height must be greater than playerYMinimum
        ///     Postcondition: player.Y = player.Y@prev - player.Height
        /// </summary>
        public void MovePlayerUp()
        {
            if (!this.playerAtTopOfRoad() || this.playerNextToHome())
            {
                this.player.MoveUp();
            }
        }

        private bool playerNextToHome()
        {
            return this.playerAtLocationCollidesWithHome(this.player.X, this.player.Y - this.player.Height);
        }

        private bool playerAtTopOfRoad()
        {
            return this.player.Y - this.player.Height < this.playerYMinimum;
        }

        /// <summary>
        ///     Moves the player down.
        ///     Precondition: player.Y + player.Height must be less than playerYMaximum
        ///     Postcondition: player.Y = player.Y@prev + player.Height
        /// </summary>
        public void MovePlayerDown()
        {
            if (!this.playerAtBottomOfScreen())
            {
                this.player.MoveDown();
            }
        }

        private bool playerAtBottomOfScreen()
        {
            return this.player.Y + this.player.Height > this.playerYMaximum;
        }

        /// <summary>
        ///   Class containing arguments for the progress bar.
        /// </summary>
        public class ProgressBarArgs
        {
            #region Properties

            /// <summary>Gets or sets the progress value.</summary>
            /// <value>The progress value.</value>
            public int ProgressValue { get; set; }

            #endregion
        }

        /// <summary>
        ///   Class containing arguments concerning the lives of the player.
        /// </summary>
        public class LifeLostEventArgs : EventArgs
        {
            #region Properties

            /// <summary>Gets or sets the lives.</summary>
            /// <value>The lives.</value>
            public int Lives { get; set; }

            #endregion
        }

        /// <summary>
        ///   Class containing arguments concerning the Score of the player.
        /// </summary>
        public class ScoreIncreasedEventArgs : EventArgs
        {
            #region Properties

            /// <summary>Gets or sets the score.</summary>
            /// <value>The score.</value>
            public int Score { get; set; }

            #endregion
        }

        #endregion
    }
}