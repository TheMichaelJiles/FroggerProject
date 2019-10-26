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
        public int Speed { get; private set; }
        public int numberOfVehicles { get; private set; }
        public Direction direction { get; private set; }
        public VehicleType vehicleType { get; private set; }
        public int YCoordinate { get; private set; }


        public DefaultLane(int speed, int numberOfVehicles, Direction direction, VehicleType vehicleType,
            int yCoordinate)
        {
            this.Speed = speed;
            this.vehicleType = vehicleType;
            this.numberOfVehicles = numberOfVehicles;
            this.direction = direction;
            this.YCoordinate = yCoordinate;
        }


    }
}
