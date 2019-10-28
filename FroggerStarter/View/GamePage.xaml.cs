using System;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using FroggerStarter.Controller;
using Size = Windows.Foundation.Size;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FroggerStarter.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage
    {
        #region Data members

        private readonly double applicationHeight = (double)Application.Current.Resources["AppHeight"];
        private readonly double applicationWidth = (double)Application.Current.Resources["AppWidth"];
        private readonly GameManager gameManager;


        #endregion

        #region Constructors

        public GamePage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size
            { Width = this.applicationWidth, Height = this.applicationHeight };
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView()
                           .SetPreferredMinSize(new Size(this.applicationWidth, this.applicationHeight));

            Window.Current.CoreWindow.KeyDown += this.coreWindowOnKeyDown;
            this.gameOverLabel.Visibility = Visibility.Collapsed;
            this.gameManager = new GameManager(this.applicationHeight, this.applicationWidth);
            this.gameManager.InitializeGame(this.canvas);
            this.gameManager.LifeLost += this.handleLifeLost;
            this.gameManager.ScoreIncreased += this.handleScoreIncreased;
            this.gameManager.GameOver += this.handleGameOver;
            this.gameManager.ProgressBarIncrease += this.increaseProgressBar;
        }

        #endregion

        #region Methods

        private void increaseProgressBar(object sender, object args)
        {
            this.progressBar.Value++;
        }

        private void handleScoreIncreased(object sender, GameManager.ScoreIncreasedEventArgs args)
        {
            this.scoreLabel.Text = $"Score: {args.Score}";
        }

        private void handleLifeLost(object sender, GameManager.LifeLostEventArgs args)
        {
            this.livesLabel.Text = $"Lives: {args.Lives}";
        }

        private void handleGameOver(object sender, EventArgs args)
        {
            this.gameOverLabel.Visibility = Visibility.Visible;
        }
        private void coreWindowOnKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    this.gameManager.MovePlayerLeft();
                    break;
                case VirtualKey.Right:
                    this.gameManager.MovePlayerRight();
                    break;
                case VirtualKey.Up:
                    this.gameManager.MovePlayerUp();
                    break;
                case VirtualKey.Down:
                    this.gameManager.MovePlayerDown();
                    break;
            }
        }

        #endregion
    }
}