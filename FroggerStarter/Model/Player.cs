using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    public abstract class Player : GameObject
    {
        private const int SpeedXDirection = 50;
        private const int SpeedYDirection = 50;

        protected Player()
        {
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        /// <summary>Freezes the player to prevent movement.</summary>
        /// Precondition: None
        /// Postcondition Sets speed of player to zero
        public void Freeze()
        {
            SetSpeed(0, 0);
        }
    }
}
