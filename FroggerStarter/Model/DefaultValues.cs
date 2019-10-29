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
            new DefaultLane(1, 3, Direction.Left, VehicleType.Car, BottomOfRoad - LaneHeight),
            new DefaultLane(2, 2, Direction.Right, VehicleType.Truck, BottomOfRoad - (LaneHeight * 2)),
            new DefaultLane(3, 4, Direction.Left, VehicleType.Car, BottomOfRoad - (LaneHeight * 3)),
            new DefaultLane(4, 3, Direction.Left, VehicleType.Truck, BottomOfRoad - (LaneHeight * 4)),
            new DefaultLane(5, 5, Direction.Left, VehicleType.Car, BottomOfRoad - (LaneHeight * 5)),
        };

        public const int ScoringTimerMaximum = 200;

        public const int LaneWidth = 650;
        public const int LaneHeight = 50;

        private const int FrogStartingAreaHeight = 55;
        private const int CanvasHeight = 410;
        private const int BottomOfRoad = CanvasHeight - FrogStartingAreaHeight;

        public const int MaxScore = 3;
        public const int StartingLives = 4;

        #endregion
    }
}