using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>Class containing a frame for the death animation of a Frog object</summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public class FrogDeathFrame : GameObject
    {
        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="FrogDeathFrame" /> class.</summary>
        /// <param name="frame">The frame.</param>
        public FrogDeathFrame(BaseSprite frame)
        {
            Sprite = frame;
        }

        #endregion
    }
}