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
        protected bool IsFrozen = false;
        public IList<FrogDeathAnimation> DeathAnimation { get; protected set; }

        protected Player()
        {
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        /// <summary>Freezes the player to prevent movement and sets IsFrozen to true</summary>
        /// Precondition: None
        /// Postcondition Sets speed of player to zero && IsFrozen == true
        public void Freeze()
        {
            this.IsFrozen = true;
        }

        /// <summary>Sets player speed back to default and sets IsFrozen to false</summary>
        /// Precondition: None
        /// Postcondition Sets speed of player to default && IsFrozen == false
        public void Unfreeze()
        {
            this.IsFrozen = false;
        }

        public override void MoveUp()
        {
            if (!this.IsFrozen)
            {
                base.MoveUp();
            }
        }

        public override void MoveDown()
        {
            if (!this.IsFrozen)
            {
                base.MoveDown();
            }
        }

        public override void MoveLeft()
        {
            if (!this.IsFrozen)
            {
                base.MoveLeft();
            }
        }

        public override void MoveRight()
        {
            if (!this.IsFrozen)
            {
                base.MoveRight();
            }
        }
    }
}
