using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Vehicles
{
    /// <summary></summary>
    /// <seealso cref="FroggerStarter.Model.Vehicles.Vehicle" />
    public class Car : Vehicle
    {
        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="Car" /> class.</summary>
        /// <param name="direction">The direction.</param>
        /// Precondition: None
        /// Postcondition: None
        public Car(Direction direction) : base(direction)
        {
            Sprite = new CarSprite();
            reflectSpriteIfFacingLeft();
        }

        #endregion
    }
}