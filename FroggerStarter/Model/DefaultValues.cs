using System.Collections.Generic;
using System.Linq.Expressions;
using FroggerStarter.Model.Vehicles;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Class containing several default values used during the construction
    ///     of the game.
    /// </summary>
    public class DefaultValues
    {
        #region Data members

        public static DefaultLane[] DefaultLanes = {
            new DefaultLane(1, 2, Direction.Left, VehicleType.Car, 305),
            new DefaultLane(2, 3, Direction.Right, VehicleType.Truck, 255),
            new DefaultLane(3, 3, Direction.Left, VehicleType.Car, 205),
            new DefaultLane(4, 2, Direction.Left, VehicleType.Truck, 155),
            new DefaultLane(5, 3, Direction.Left, VehicleType.Car, 105),
        };

        public const int LaneWidth = 650;
        public const int LaneHeight = 50;

        public const int MaxScore = 3;
        public const int StartingLives = 4;

        #endregion
    }
}