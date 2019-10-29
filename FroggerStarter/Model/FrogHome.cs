using Windows.UI.Xaml;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>Class containing information on the FrogHome object</summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public class FrogHome : GameObject
    {
        #region Properties

        /// <summary>Gets a value indicating whether this instance is filled.</summary>
        /// <value>
        ///     <c>true</c> if this instance is filled; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilled { get; private set; }

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="FrogHome" /> class.</summary>
        /// Precondition: none
        /// Postcondition: this.Sprite = new FrogHomeSprite, and the sprite is collapsed
        public FrogHome()
        {
            Sprite = new FrogHomeSprite {
                Visibility = Visibility.Collapsed
            };
            this.IsFilled = false;
        }

        #endregion

        #region Methods

        /// <summary>Shows the sprite.</summary>
        public void ShowSprite()
        {
            Sprite.Visibility = Visibility.Visible;
            this.IsFilled = true;
        }

        #endregion
    }
}