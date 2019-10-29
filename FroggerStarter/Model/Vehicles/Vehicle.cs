using Windows.Foundation;
using Windows.UI.Xaml.Media;

namespace FroggerStarter.Model.Vehicles
{
    /// <summary>Contains information for the orientation and type of a vehicle.</summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public abstract class Vehicle : MovableGameObject
    {
        #region Data members

        private readonly Direction direction;

        #endregion

        #region Properties

        /// <summary>Gets or sets a value indicating whether this Vehicle is activated for movement.</summary>
        /// <value>
        ///     <c>true</c> if this vehicle is activated; otherwise, <c>false</c>.
        /// </value>
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

        /// <summary>Reflects the sprite if facing left.</summary>
        protected void reflectSpriteIfFacingLeft()
        {
            if (this.direction == Direction.Left)
            {
                Sprite.RenderTransformOrigin = new Point(0.5, 0.5);
                Sprite.RenderTransform = new ScaleTransform {ScaleX = -1};
            }
        }

        /// <summary>Moves the vehicle depending on the direction variable.</summary>
        /// Precondition: None
        /// Postcondition: Vehicle moves its current speed in it's current direction.
        public void Move()
        {
            if (this.IsActivated)
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

        /// <summary>Moves vehicle backwards.</summary>
        /// Precondition: none
        /// Postcondition: Vehicle X += this.Width*2 if direction is facing left, and -= this.Width*2 if direction is facing right
        public void MoveBack()
        {
            if (this.direction == Direction.Right)
            {
                X -= Width * 2;
            }
            else
            {
                X += Width * 2;
            }
        }

        #endregion
    }
}