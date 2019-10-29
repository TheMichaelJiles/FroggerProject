namespace FroggerStarter.Model.Vehicles
{
    /// <summary>Class with one static method that returns an object based ont he VehicleType</summary>
    public class VehicleFactory
    {
        #region Methods

        /// <summary>Returns a new vehicle object based on the passed in type</summary>
        /// <param name="type">The type.</param>
        /// <param name="direction">The direction.</param>
        /// Precondition: None
        /// Postcondition: None
        /// <returns>a new vehicle object based on the passed in type</returns>
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

        #endregion
    }
}