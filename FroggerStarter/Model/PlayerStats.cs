namespace FroggerStarter.Model
{
    /// <summary>Class containing the player stats like Lives and Score</summary>
    public class PlayerStats
    {
        #region Properties

        /// <summary>
        ///     Gets the lives of the player, with a default value of three.
        /// </summary>
        /// <value>The current lives of the player.</value>
        public int Lives { get; private set; } = DefaultValues.StartingLives;

        /// <summary>Gets the score accumulated byt the player.</summary>
        /// <value>The score accumulated by the player.</value>
        public int Score { get; private set; }

        #endregion

        #region Methods

        /// <summary>Decrements the lives property by 1.</summary>
        /// Precondition: None
        /// Postcondition: Lives = Lives@prev - 1
        public void DecreaseLivesByOne()
        {
            this.Lives--;
        }

        /// <summary>Increments the score by one.</summary>
        /// Precondition: None
        /// Postcondition: Score = Score@prev + 1
        public void IncreaseScore(int score)
        {
            this.Score += score;
        }

        #endregion
    }
}