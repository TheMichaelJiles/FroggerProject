using Windows.Foundation;
using Windows.UI.Xaml.Media;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>Contains information for the orientation and type of a vehicle.</summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public class Vehicle : GameObject
    {
        #region Data members

        private readonly Direction direction;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="Vehicle" /> class.</summary>
        /// Precondition: None
        /// Postcondition: None
        /// <param name="direction">The direction.</param>
        /// <param name="vehicleType">Type of the vehicle.</param>
        public Vehicle(Direction direction, VehicleType vehicleType)
        {
            this.direction = direction;
            this.setVehicleSprite(vehicleType);
            this.reflectSpriteIfFacingLeft();
        }

        #endregion

        #region Methods

        private void reflectSpriteIfFacingLeft()
        {
            if (this.direction == Direction.Left)
            {
                Sprite.RenderTransformOrigin = new Point(0.5, 0.5);
                Sprite.RenderTransform = new ScaleTransform {ScaleX = -1};
            }
        }

        private void setVehicleSprite(VehicleType vehicleType)
        {
            switch (vehicleType)
            {
                case VehicleType.Car:
                    Sprite = new CarSprite();
                    break;
                case VehicleType.Truck:
                    Sprite = new Truck();
                    break;
            }
        }

        /// <summary>Moves the vehicle depending on the direction variable.</summary>
        /// Precondition: None
        /// Postcondition: Vehicle moves its current speed in it's current direction.
        public void Move()
        {
            if (this.direction == Direction.Right)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }

        /// <summary>Speeds up.</summary>
        /// Precondition: None
        /// Postcondition: Increments the SpeedX by one.
        public void SpeedUp()
        {
            SetSpeed(SpeedX + 1, SpeedY);
        }

        #endregion
    }
}