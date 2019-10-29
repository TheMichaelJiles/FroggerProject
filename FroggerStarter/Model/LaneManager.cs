using System.Collections;
using System.Collections.Generic;
using FroggerStarter.Model.Vehicles;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Manages the vehicles in a lane of the game.
    /// </summary>
    public class LaneManager : IEnumerable<Vehicle>
    {
        #region Data members

        private IList<Vehicle> vehicles;
        private readonly Direction direction;
        private readonly int numberOfVehicles;
        private readonly int laneYCoordinate;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="LaneManager" /> class.</summary>
        /// <param name="vehicleType">Type of the vehicle in the lane.</param>
        /// <param name="direction">The direction of the vehicles in the lane.</param>
        /// <param name="numberOfVehicles">The number of vehicles in the lane.</param>
        /// <param name="laneYCoordinate">The lane y coordinate.</param>
        /// <param name="speed">The speed of the vehicles in the lane.</param>
        public LaneManager(VehicleType vehicleType, Direction direction, int numberOfVehicles, int laneYCoordinate,
            int speed)
        {
            this.direction = direction;
            this.numberOfVehicles = numberOfVehicles;
            this.laneYCoordinate = laneYCoordinate;
            this.createAndPopulateListOfVehicles(vehicleType);
            this.stackVehiclesOffScreen();
            this.SetSpeedOfVehicles(speed);
        }

        #endregion

        #region Methods

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<Vehicle> GetEnumerator()
        {
            return this.vehicles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        ///     Sets the speed of the vehicles.
        /// </summary>
        /// Precondition: none
        /// Postcondition: Speed of all vehicles in this lane set to param.
        /// <param name="speed">The speed to assign to the vehicles.</param>
        public void SetSpeedOfVehicles(int speed)
        {
            foreach (var vehicle in this.vehicles)
            {
                vehicle.SpeedX = speed;
            }
        }

        /// <summary>Resets all vehicles to starting positions.</summary>
        /// Precondition: None
        /// Postcondition: All vehicles put to starting positions
        public void Reset()
        {
            foreach (var vehicle in this.vehicles)
            {
                vehicle.IsActivated = false;
            }

            this.stackVehiclesOffScreen();
        }

        private void createAndPopulateListOfVehicles(VehicleType vehicleType)
        {
            this.vehicles = new List<Vehicle>();
            for (var i = 0; i < this.numberOfVehicles; i++)
            {
                this.vehicles.Add(VehicleFactory.createNewVehicle(vehicleType, this.direction));
            }
        }

        private void stackVehiclesOffScreen()
        {
            foreach (var vehicle in this.vehicles)
            {
                vehicle.Y = this.laneYCoordinate + (DefaultValues.LaneHeight - vehicle.Height) / 2;
                if (this.direction == Direction.Left)
                {
                    vehicle.X = DefaultValues.LaneWidth;
                }
                else
                {
                    vehicle.X = -vehicle.Width;
                }
            }
        }

        #endregion
    }
}