using System.Collections.Generic;
using FroggerStarter.Model.Vehicles;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     <para>Class containing functionality for Player objects</para>
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.Vehicles.MovableGameObject" />
    public abstract class Player : MovableGameObject
    {
        #region Data members

        private const int SpeedXDirection = 50;
        private const int SpeedYDirection = 50;

        /// <summary>  Whether or not the player is frozen</summary>
        protected bool IsFrozen;

        #endregion

        #region Properties

        /// <summary>Gets or sets the death animation frames.</summary>
        /// <value>The death animation.</value>
        public IList<FrogDeathFrame> DeathAnimationFrames { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="Player" /> class.</summary>
        /// Precondition: none
        /// Postcondition: Speed is set to SPeedXDirection and SpeedYDirection
        protected Player()
        {
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        #endregion

        #region Methods

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

        /// <summary>
        ///     Moves the player up if it is unfrozen.
        ///     Precondition: None
        ///     Postcondition: Y == Y@prev - SpeedY
        /// </summary>
        public override void MoveUp()
        {
            if (!this.IsFrozen)
            {
                base.MoveUp();
            }
        }

        /// <summary>
        ///     Moves the player down if unfrozen.
        ///     Precondition: None
        ///     Postcondition: Y == Y@prev + SpeedY
        /// </summary>
        public override void MoveDown()
        {
            if (!this.IsFrozen)
            {
                base.MoveDown();
            }
        }

        /// <summary>
        ///     Moves the player left if it is unfrozen.
        ///     Precondition: None
        ///     Postcondition: X == X@prev + SpeedX
        /// </summary>
        public override void MoveLeft()
        {
            if (!this.IsFrozen)
            {
                base.MoveLeft();
            }
        }

        /// <summary>
        ///     Moves the player right if it is unfrozen.
        ///     Precondition: None
        ///     Postcondition: X == X@prev + SpeedX
        /// </summary>
        public override void MoveRight()
        {
            if (!this.IsFrozen)
            {
                base.MoveRight();
            }
        }

        #endregion
    }
}