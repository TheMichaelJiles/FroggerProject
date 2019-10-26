using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model.Vehicles
{
    public class VehicleFactory
    {
        public static Vehicle createNewVehicle(VehicleType type, Direction direction)
        {
            switch (type)
            {
                case VehicleType.Car:
                {
                    return new Car(direction);
                }
                case VehicleType.Truck:
                {
                    return new Truck(direction);
                }
                default:
                {
                    return null;
                }
            }
        }
    }
}
