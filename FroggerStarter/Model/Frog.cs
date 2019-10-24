using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Defines the frog model
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public class Frog : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 50;
        private const int SpeedYDirection = 50;

        #endregion

        #region Properties

        /// <summary>Gets the width of the game object.</summary>
        /// <value>The width of the underlying sprite.</value>
        public new double Width => Sprite.Width;

        /// <summary>Gets the height of the game object.</summary>
        /// <value>The height of the underlying sprite.</value>
        public new double Height => Sprite.Height;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Frog" /> class.
        /// </summary>
        public Frog()
        {
            Sprite = new FrogSprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        #endregion

        #region Methods

        /// <summary>Freezes the player to prevent movement.</summary>
        /// Precondition: None
        /// Postcondition Sets speed of player to zero
        public void Freeze()
        {
            SetSpeed(0, 0);
        }

        #endregion
    }
}