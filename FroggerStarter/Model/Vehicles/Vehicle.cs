using Windows.Foundation;
using Windows.UI.Xaml.Media;
using FroggerStarter.Model.Vehicles;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Vehicles
{
    /// <summary>Contains information for the orientation and type of a vehicle.</summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public abstract class Vehicle : GameObject
    {
        #region Data members

        private readonly Direction direction;
        public bool IsActivated { get; set; }

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="Vehicle" /> class.</summary>
        /// Precondition: None
        /// Postcondition: None
        /// <param name="direction">The direction.</param>
        protected Vehicle(Direction direction)
        {
            this.direction = direction;
            this.IsActivated = false;
        }

        #endregion

        #region Methods

        protected void reflectSpriteIfFacingLeft()
        {
            if (this.direction == Direction.Left)
            {
                Sprite.RenderTransformOrigin = new Point(0.5, 0.5);
                Sprite.RenderTransform = new ScaleTransform { ScaleX = -1 };
            }
        }

        /// <summary>Moves the vehicle depending on the direction variable.</summary>
        /// Precondition: None
        /// Postcondition: Vehicle moves its current speed in it's current direction.
        public void Move()
        {
            if (IsActivated)
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
        }

        public void MoveBack()
        {
            if (this.direction == Direction.Right)
            {
                this.X -= this.Width*2;
            }
            else
            {
                this.X += this.Width*2;
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