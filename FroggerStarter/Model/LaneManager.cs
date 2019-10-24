﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Manages the vehicles in a lane of the game.
    /// </summary>
    public class LaneManager : IEnumerable<Vehicle>
    {
        #region Data members

        private IList<Vehicle> vehicles;

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
            this.createAndPopulateListOfVehicles(vehicleType, direction, numberOfVehicles);
            this.assignVehicleCoordinates(numberOfVehicles, laneYCoordinate);
            this.SetSpeedOfVehicles(speed);
        }

        #endregion

        #region Methods

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

        private void createAndPopulateListOfVehicles(VehicleType vehicleType, Direction direction, int numberOfVehicles)
        {
            this.vehicles = new List<Vehicle>();
            for (var i = 0; i < numberOfVehicles; i++)
            {
                this.vehicles.Add(new Vehicle(direction, vehicleType));
            }
        }

        private void assignVehicleCoordinates(int numberOfVehicles, int laneYCoordinate)
        {
            var totalLengthOfVehicles = this.vehicles.First().Width * numberOfVehicles;
            var spaceBetweenVehicles = (DefaultValues.LaneWidth - totalLengthOfVehicles) / numberOfVehicles;
            double endOfPreviousVehicle = 0;
            foreach (var vehicle in this.vehicles)
            {
                vehicle.Y = laneYCoordinate + (DefaultValues.LaneHeight - vehicle.Height) / 2;
                vehicle.X = endOfPreviousVehicle + spaceBetweenVehicles;
                endOfPreviousVehicle = vehicle.X + vehicle.Width;
            }
        }

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



        #endregion
    }
}