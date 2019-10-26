using System;
using System.Drawing;
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
        #region Types and Delegates

        /// <summary>Delegate to handle collisions</summary>
        /// <param name="lives">The current number of lives.</param>
        public delegate void CollisionHandler(int lives);

        /// <summary>Delegate to handle when the game is over.</summary>
        public delegate void GameOverHandler();

        /// <summary>Delegate to handle score increase.</summary>
        /// <param name="score">The current score.</param>
        public delegate void ScoreHandler(int score);

        #endregion

        #region Data members

        private const int BottomLaneOffset = 5;
        private double playerXMinimum;
        private double playerYMinimum;
        private double playerXMaximum;
        private double playerYMaximum;
        private readonly double backgroundHeight;
        private readonly double backgroundWidth;
        private Canvas gameCanvas;
        private Frog player;
        private DispatcherTimer timer;
        private readonly RoadManager roadManager;
        private readonly PlayerStats playerStats;

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
        public event CollisionHandler LifeLost;

        /// <summary>Occurs when score increases</summary>
        public event ScoreHandler ScoreIncreased;

        /// <summary>Occurs when the game is over.</summary>
        public event GameOverHandler GameOver;

        private void setupGameTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.timerOnTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 15);
            this.timer.Start();
        }

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
        }

        private void placeAllVehicles()
        {
            var vehicles = this.roadManager.GetAllVehicles();
            foreach (var vehicle in vehicles)
            {
                this.gameCanvas.Children.Add(vehicle.Sprite);
            }
        }

        private void createAndPlacePlayer()
        {
            this.player = new Frog();
            this.gameCanvas.Children.Add(this.player.Sprite);
            this.setPlayerBoundaries();
            this.setPlayerToCenterOfBottomLane();
        }

        private void setPlayerBoundaries()
        {
            this.playerXMaximum = this.gameCanvas.Width - this.player.Width;
            this.playerYMaximum = this.gameCanvas.Height - this.player.Height;
            this.playerYMinimum = this.player.Height;
            this.playerXMinimum = 0;
        }

        private void setPlayerToCenterOfBottomLane()
        {
            this.player.X = this.backgroundWidth / 2 - this.player.Width / 2;
            this.player.Y = this.backgroundHeight - this.player.Height - BottomLaneOffset;
        }

        private void timerOnTick(object sender, object e)
        {
            this.roadManager.MoveAllVehicles();
            this.detectCollisionOfPlayerAndVehicle();
            this.detectSuccessfulScore();
        }

        private void detectSuccessfulScore()
        {
            if (this.playerSuccessfullyCrossesRoad())
            {
                this.playerStats.IncreaseScore();
                this.ScoreIncreased?.Invoke(this.playerStats.Score);
                this.detectGameOver();
                this.setPlayerToCenterOfBottomLane();
            }
        }

        private bool playerSuccessfullyCrossesRoad()
        {
            return this.player.Y < DefaultValues.LaneFiveYCoord;
        }

        private void detectGameOver()
        {
            if (this.playerLivesIsZero() || this.playerReachesMaxScore())
            {
                this.timer.Stop();
                this.roadManager.StopTimer();
                this.player.Freeze();
                this.GameOver?.Invoke();
            }
        }

        private bool playerReachesMaxScore()
        {
            return this.playerStats.Score == DefaultValues.MaxScore;
        }

        private bool playerLivesIsZero()
        {
            return this.playerStats.Lives == 0;
        }

        private void detectCollisionOfPlayerAndVehicle()
        {
            var playerBoundingBox = this.createGameObjectBoundingBox(this.player);
            foreach (var vehicle in this.roadManager.GetAllVehicles())
            {
                var vehicleBoundingBox =
                    this.createGameObjectBoundingBox(vehicle);
                if (this.playerCollidesWithVehicle(playerBoundingBox, vehicleBoundingBox))
                {
                    this.playerStats.DecreaseLivesByOne();
                    this.LifeLost?.Invoke(this.playerStats.Lives);
                    this.detectGameOver();
                    this.setPlayerToCenterOfBottomLane();
                    this.roadManager.ResetVehicleSpeeds();
                }
            }
        }

        private bool playerCollidesWithVehicle(Rectangle playerBoundingBox, Rectangle vehicleBoundingBox)
        {
            return playerBoundingBox.IntersectsWith(vehicleBoundingBox);
        }

        private Rectangle createGameObjectBoundingBox(GameObject gameObject)
        {
            return new Rectangle((int)gameObject.X, (int)gameObject.Y, (int)gameObject.Width,
                (int)gameObject.Height);
        }

        /// <summary>
        ///     Moves the player to the left.
        ///     Precondition: player.X - player.Width must be greater than playerXMinimum
        ///     Postcondition: player.X = player.X@prev - player.Width
        /// </summary>
        public void MovePlayerLeft()
        {
            if (!(this.player.X - this.player.Width < this.playerXMinimum))
            {
                this.player.MoveLeft();
            }
        }

        /// <summary>
        ///     Moves the player to the right.
        ///     Precondition: player.X + player.Width must be less than playerXMaximum
        ///     Postcondition: player.X = player.X@prev + player.Width
        /// </summary>
        public void MovePlayerRight()
        {
            if (!(this.player.X + this.player.Width > this.playerXMaximum))
            {
                this.player.MoveRight();
            }
        }

        /// <summary>
        ///     Moves the player up.
        ///     Precondition: player.Y - player.Height must be greater than playerYMinimum
        ///     Postcondition: player.Y = player.Y@prev - player.Height
        /// </summary>
        public void MovePlayerUp()
        {
            if (!(this.player.Y - this.player.Height < this.playerYMinimum))
            {
                this.player.MoveUp();
            }
        }

        /// <summary>
        ///     Moves the player down.
        ///     Precondition: player.Y + player.Height must be less than playerYMaximum
        ///     Postcondition: player.Y = player.Y@prev + player.Height
        /// </summary>
        public void MovePlayerDown()
        {
            if (!(this.player.Y + this.player.Height > this.playerYMaximum))
            {
                this.player.MoveDown();
            }
        }

        #endregion
    }
}