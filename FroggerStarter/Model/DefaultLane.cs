using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FroggerStarter.Model.Vehicles;

namespace FroggerStarter.Model
{
    public class DefaultLane
    {
        public int Speed { get; }
        public int NumberOfVehicles { get; }
        public Direction Direction { get; }
        public VehicleType VehicleType { get; }
        public int YCoordinate { get; }


        public DefaultLane(int speed, int numberOfVehicles, Direction direction, VehicleType vehicleType,
            int yCoordinate)
        {
            this.Speed = speed;
            this.VehicleType = vehicleType;
            this.NumberOfVehicles = numberOfVehicles;
            this.Direction = direction;
            this.YCoordinate = yCoordinate;
        }


    }
}
