using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using System.Linq;
using FroggerStarter.Model.Vehicles;

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
            this.laneManagers[0].SetSpeedOfVehicles(DefaultValues.DefaultLanes[0].Speed);
            this.laneManagers[1].SetSpeedOfVehicles(DefaultValues.DefaultLanes[1].Speed);
            this.laneManagers[2].SetSpeedOfVehicles(DefaultValues.DefaultLanes[2].Speed);
            this.laneManagers[3].SetSpeedOfVehicles(DefaultValues.DefaultLanes[3].Speed);
            this.laneManagers[4].SetSpeedOfVehicles(DefaultValues.DefaultLanes[4].Speed);
        }

        private void populateLaneManagersList()
        {
            this.laneManagers = new LaneManager[5];
            this.laneManagers[0] = new LaneManager(DefaultValues.DefaultLanes[0].vehicleType, DefaultValues.DefaultLanes[0].direction,
                DefaultValues.DefaultLanes[0].numberOfVehicles, DefaultValues.DefaultLanes[0].YCoordinate, DefaultValues.DefaultLanes[0].Speed);
            this.laneManagers[1] = new LaneManager(DefaultValues.DefaultLanes[1].vehicleType, DefaultValues.DefaultLanes[1].direction,
                DefaultValues.DefaultLanes[1].numberOfVehicles, DefaultValues.DefaultLanes[1].YCoordinate, DefaultValues.DefaultLanes[1].Speed);
            this.laneManagers[2] = new LaneManager(DefaultValues.DefaultLanes[2].vehicleType, DefaultValues.DefaultLanes[2].direction,
                DefaultValues.DefaultLanes[2].numberOfVehicles, DefaultValues.DefaultLanes[2].YCoordinate, DefaultValues.DefaultLanes[2].Speed);
            this.laneManagers[3] = new LaneManager(DefaultValues.DefaultLanes[3].vehicleType, DefaultValues.DefaultLanes[3].direction,
                DefaultValues.DefaultLanes[3].numberOfVehicles, DefaultValues.DefaultLanes[3].YCoordinate, DefaultValues.DefaultLanes[3].Speed);
            this.laneManagers[4] = new LaneManager(DefaultValues.DefaultLanes[4].vehicleType, DefaultValues.DefaultLanes[4].direction,
                DefaultValues.DefaultLanes[4].numberOfVehicles, DefaultValues.DefaultLanes[4].YCoordinate, DefaultValues.DefaultLanes[4].Speed);
        }

        /// <summary>Returns all vehicles in all lanes.</summary>
        /// Precondition: None
        /// Postcondition: None
        /// <returns>A list of all vehicles on the canvas.</returns>
        public List<Vehicle> GetAllVehicles()
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
            this.GetAllVehicles().ForEach(vehicle => vehicle.SpeedUp());
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