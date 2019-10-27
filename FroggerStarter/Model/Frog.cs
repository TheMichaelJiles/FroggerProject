using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Defines the frog model
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public class Frog : Player
    {
        #region Data members


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
        public Frog() : base()
        {
            Sprite = new FrogSprite();
        }

        #endregion

        #region Methods



        #endregion
    }
}