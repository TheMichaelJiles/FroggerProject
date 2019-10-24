using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Handles each LaneManager object and keeps a timer controlling the
    ///     acceleration of the vehicles
    /// </summary>
    public class RoadManager : IEnumerable<Vehicle>
    {
        #region Data members

        private LaneManager[] laneManagers;
        private DispatcherTimer timer;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="RoadManager" /> class.</summary>
        /// Precondition: None
        /// Postcondition: laneManagers populated, and timer is created.
        public RoadManager()
        {
            this.populateLaneManagersList();
            this.setupSpeedTimer();
        }

        #endregion

        #region Methods

        /// <summary>Resets the vehicle speeds to default values.</summary>
        /// Precondition: None
        /// Postcondition: Vehicles speeds are set to default values
        public void ResetVehicleSpeeds()
        {
            this.laneManagers[0].SetSpeedOfVehicles(DefaultValues.LaneOneSpeed);
            this.laneManagers[1].SetSpeedOfVehicles(DefaultValues.LaneTwoSpeed);
            this.laneManagers[2].SetSpeedOfVehicles(DefaultValues.LaneThreeSpeed);
            this.laneManagers[3].SetSpeedOfVehicles(DefaultValues.LaneFourSpeed);
            this.laneManagers[4].SetSpeedOfVehicles(DefaultValues.LaneFiveSpeed);
        }

        private void populateLaneManagersList()
        {
            this.laneManagers = new LaneManager[5];
            this.laneManagers[0] = new LaneManager(DefaultValues.LaneOneVehicleType, DefaultValues.LaneOneDirection,
                DefaultValues.LaneOneNumberOfVehicles, DefaultValues.LaneOneYCoord, DefaultValues.LaneOneSpeed);
            this.laneManagers[1] = new LaneManager(DefaultValues.LaneTwoVehicleType, DefaultValues.LaneTwoDirection,
                DefaultValues.LaneTwoNumberOfVehicles, DefaultValues.LaneTwoYCoord, DefaultValues.LaneTwoSpeed);
            this.laneManagers[2] = new LaneManager(DefaultValues.LaneThreeVehicleType, DefaultValues.LaneThreeDirection,
                DefaultValues.LaneThreeNumberOfVehicles, DefaultValues.LaneThreeYCoord, DefaultValues.LaneThreeSpeed);
            this.laneManagers[3] = new LaneManager(DefaultValues.LaneFourVehicleType, DefaultValues.LaneFourDirection,
                DefaultValues.LaneFourNumberOfVehicles, DefaultValues.LaneFourYCoord, DefaultValues.LaneFourSpeed);
            this.laneManagers[4] = new LaneManager(DefaultValues.LaneFiveVehicleType, DefaultValues.LaneFiveDirection,
                DefaultValues.LaneFiveNumberOfVehicles, DefaultValues.LaneFiveYCoord, DefaultValues.LaneFiveSpeed);
        }

        /// <summary>Returns all vehicles in all lanes.</summary>
        /// Precondition: None
        /// Postcondition: None
        /// <returns>A list of all vehicles on the canvas.</returns>
        public IEnumerable<Vehicle> GetAllVehicles()
        {
            var vehicles = new List<Vehicle>();
            foreach (var lane in this.laneManagers)
            {
                vehicles.AddRange(lane);
            }

            return vehicles;
        }

        private void setupSpeedTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.speedUpVehicles;
            this.timer.Interval = new TimeSpan(0, 0, 0, 5, 0);
            this.timer.Start();
        }

        private void speedUpVehicles(object sender, object e)
        {
            foreach (var vehicle in this.GetAllVehicles())
            {
                vehicle.SpeedUp();
            }
        }

        /// <summary>Calls each vehicles move method.</summary>
        /// Precondition: None
        /// Postcondition: All vehicles move in their own direction by their own speed.
        public void MoveAllVehicles()
        {
            foreach (var vehicle in this.GetAllVehicles())
            {
                vehicle.Move();
                if (this.vehicleIsOffRightSideOfCanvas(vehicle))
                {
                    vehicle.X = 0 - vehicle.Width;
                }
                else if (this.vehicleIsOffLeftSideOfCanvas(vehicle))
                {
                    vehicle.X = DefaultValues.LaneWidth;
                }
            }
        }

        /// <summary>Stops the game timer.</summary>
        /// Precondition: None
        /// Postcondition: timer is stopped
        public void StopTimer()
        {
            this.timer.Stop();
        }

        private bool vehicleIsOffLeftSideOfCanvas(GameObject vehicle)
        {
            return vehicle.X <= 0 - vehicle.Width;
        }

        private bool vehicleIsOffRightSideOfCanvas(GameObject vehicle)
        {
            return vehicle.X >= DefaultValues.LaneWidth;
        }

        public IEnumerator<Vehicle> GetEnumerator()
        {
            return laneManagers[0]?.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return laneManagers[0]?.GetEnumerator();
        }

        #endregion
    }
}