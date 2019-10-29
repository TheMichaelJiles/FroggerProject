using FroggerStarter.Model.Vehicles;

namespace FroggerStarter.Model
{
    /// <summary>Object containing default values for a LanManager object</summary>
    public class DefaultLane
    {
        #region Properties

        /// <summary>Gets the speed.</summary>
        /// <value>The speed.</value>
        public int Speed { get; }

        /// <summary>Gets the number of vehicles.</summary>
        /// <value>The number of vehicles.</value>
        public int NumberOfVehicles { get; }

        /// <summary>Gets the direction.</summary>
        /// <value>The direction.</value>
        public Direction Direction { get; }

        /// <summary>Gets the type of the vehicle.</summary>
        /// <value>The type of the vehicle.</value>
        public VehicleType VehicleType { get; }

        /// <summary>Gets the y coordinate.</summary>
        /// <value>The y coordinate.</value>
        public int YCoordinate { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="DefaultLane" /> class.</summary>
        /// <param name="speed">The speed.</param>
        /// <param name="numberOfVehicles">The number of vehicles.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="vehicleType">Type of the vehicle.</param>
        /// <param name="yCoordinate">The y coordinate.</param>
        /// Precondition: None
        /// Postcondition: this.speed == speed, this.numberOfVehicles == numberOfVehicles, this.direction == direction, this.vehicleType == vehicleType, this.yCoordinate == yCoordinate
        public DefaultLane(int speed, int numberOfVehicles, Direction direction, VehicleType vehicleType,
            int yCoordinate)
        {
            this.Speed = speed;
            this.VehicleType = vehicleType;
            this.NumberOfVehicles = numberOfVehicles;
            this.Direction = direction;
            this.YCoordinate = yCoordinate;
        }

        #endregion
    }
}