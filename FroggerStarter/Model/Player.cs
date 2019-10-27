using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    public abstract class Player : GameObject
    {
        private const int SpeedXDirection = 50;
        private const int SpeedYDirection = 50;
        protected bool isFrozen = false;
        public IList<FrogDeathAnimation> DeathAnimation { get; protected set; }

        protected Player()
        {
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        /// <summary>Freezes the player to prevent movement.</summary>
        /// Precondition: None
        /// Postcondition Sets speed of player to zero
        public void Freeze()
        {
            this.isFrozen = true;
            SetSpeed(0, 0);
        }

        public void Unfreeze()
        {
            this.isFrozen = false;
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
    }
}
