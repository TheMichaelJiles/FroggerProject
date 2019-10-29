using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Vehicles
{
    /// <summary>Assigns TruckSprite to the Sprite property of the Vehicle class</summary>
    /// <seealso cref="FroggerStarter.Model.Vehicles.Vehicle" />
    public class Truck : Vehicle
    {
        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="Truck" /> class.</summary>
        /// <param name="direction">The direction.</param>
        /// Precondition: None
        /// Postcondition: this.Sprite = new TruckSprite
        public Truck(Direction direction) : base(direction)
        {
            Sprite = new TruckSprite();
            reflectSpriteIfFacingLeft();
        }

        #endregion
    }
}