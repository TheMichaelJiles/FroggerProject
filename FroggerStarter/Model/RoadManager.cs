using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
            this.speedUpVehicles(this, EventArgs.Empty);
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
            this.laneManagers[0] = new LaneManager(DefaultValues.DefaultLanes[0].VehicleType, DefaultValues.DefaultLanes[0].Direction,
                DefaultValues.DefaultLanes[0].NumberOfVehicles, DefaultValues.DefaultLanes[0].YCoordinate, DefaultValues.DefaultLanes[0].Speed);
            this.laneManagers[1] = new LaneManager(DefaultValues.DefaultLanes[1].VehicleType, DefaultValues.DefaultLanes[1].Direction,
                DefaultValues.DefaultLanes[1].NumberOfVehicles, DefaultValues.DefaultLanes[1].YCoordinate, DefaultValues.DefaultLanes[1].Speed);
            this.laneManagers[2] = new LaneManager(DefaultValues.DefaultLanes[2].VehicleType, DefaultValues.DefaultLanes[2].Direction,
                DefaultValues.DefaultLanes[2].NumberOfVehicles, DefaultValues.DefaultLanes[2].YCoordinate, DefaultValues.DefaultLanes[2].Speed);
            this.laneManagers[3] = new LaneManager(DefaultValues.DefaultLanes[3].VehicleType, DefaultValues.DefaultLanes[3].Direction,
                DefaultValues.DefaultLanes[3].NumberOfVehicles, DefaultValues.DefaultLanes[3].YCoordinate, DefaultValues.DefaultLanes[3].Speed);
            this.laneManagers[4] = new LaneManager(DefaultValues.DefaultLanes[4].VehicleType, DefaultValues.DefaultLanes[4].Direction,
                DefaultValues.DefaultLanes[4].NumberOfVehicles, DefaultValues.DefaultLanes[4].YCoordinate, DefaultValues.DefaultLanes[4].Speed);
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

        public List<Vehicle> GetAllActiveVehicles()
        {
            var vehicles = new List<Vehicle>();
            foreach (var lane in this.laneManagers)
            {
                foreach (var vehicle in lane)
                {
                    if (vehicle.IsActivated)
                    {
                        vehicles.Add(vehicle);
                    }
                }
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
            foreach (var lane in this.laneManagers)
            {
                foreach (var vehicle in lane)
                {
                    if (!vehicle.IsActivated)
                    {
                        vehicle.IsActivated = true;
                        break;
                    }
                }
            }
        }

        /// <summary>Calls each vehicles move method.</summary>
        /// Precondition: None
        /// Postcondition: All vehicles move in their own direction by their own speed.
        public void MoveAllVehicles()
        {
            foreach (var vehicle in this.GetAllActiveVehicles())
            {
                vehicle.Move();
                if (this.vehicleIsOffRightSideOfCanvas(vehicle))
                {
                    vehicle.X = 0 - vehicle.Width;
                    this.detectCollisionOfThisAndVehicle(vehicle);
                }
                else if (this.vehicleIsOffLeftSideOfCanvas(vehicle))
                {
                    vehicle.X = DefaultValues.LaneWidth;
                    this.detectCollisionOfThisAndVehicle(vehicle);
                }
            }
        }

        private void detectCollisionOfThisAndVehicle(Vehicle vehicle)
        {
            var vehicleBoundingBox = this.createGameObjectBoundingBox(vehicle);
            var activatedVehicles = this.GetAllActiveVehicles();
            foreach (var otherVehicle in activatedVehicles)
            {
                if (vehicle != otherVehicle) {
                    var otherVehicleBoundingBox = this.createGameObjectBoundingBox(otherVehicle);
                    if (vehicleBoundingBox.IntersectsWith(otherVehicleBoundingBox))
                    {
                        vehicle.MoveBack();
                    }
                }
            }
        }

        private Rectangle createGameObjectBoundingBox(GameObject gameObject)
        {
            return new Rectangle((int)gameObject.X, (int)gameObject.Y, (int)gameObject.Width,
                (int)gameObject.Height);
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