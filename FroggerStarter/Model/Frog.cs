using System.Collections.Generic;
using System.Linq;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Defines the frog model
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public class Frog : Player
    {
        #region Data members

        #endregion

        #region Properties

        /// <summary>Gets the width of the game object.</summary>
        /// <value>The width of the underlying sprite.</value>
        public new double Width => Sprite.Width;

        /// <summary>Gets the height of the game object.</summary>
        /// <value>The height of the underlying sprite.</value>
        public new double Height => Sprite.Height;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Frog" /> class.
        /// </summary>
        public Frog() : base()
        {
            Sprite = new FrogSprite();

            this.DeathAnimation = new List<FrogDeathAnimation>() {
                new FrogDeathAnimation(new FrogDeathOne()),
                new FrogDeathAnimation(new FrogDeathTwo()),
                new FrogDeathAnimation(new FrogDeathThree()),
            };
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves the game object right.
        ///     Precondition: None
        ///     Postcondition: X == X@prev + SpeedX
        /// </summary>
        public override void MoveRight()
        {
            base.MoveRight();
            if (!this.IsFrozen)
            {
                this.moveDeathFramesToFrogLocation(); 
            }
        }

        /// <summary>
        ///     Moves the game object left.
        ///     Precondition: None
        ///     Postcondition: X == X@prev + SpeedX
        /// </summary>
        public override void MoveLeft()
        {
            base.MoveLeft();
            if (!this.IsFrozen)
            {
                this.moveDeathFramesToFrogLocation();
            }
        }

        /// <summary>
        ///     Moves the game object up.
        ///     Precondition: None
        ///     Postcondition: Y == Y@prev - SpeedY
        /// </summary>
        public override void MoveUp()
        {
            base.MoveUp();
            if (!this.IsFrozen)
            {
                this.moveDeathFramesToFrogLocation();
            }
        }

        /// <summary>
        ///     Moves the game object down.
        ///     Precondition: None
        ///     Postcondition: Y == Y@prev + SpeedY
        /// </summary>
        public override void MoveDown()
        {
            base.MoveDown();
            if(!this.IsFrozen)
            {
                this.moveDeathFramesToFrogLocation();
            }
        }

        private void moveDeathFramesToFrogLocation()
        {
            foreach (var frame in this.DeathAnimation)
            {
                frame.X = this.X;
                frame.Y = this.Y;
            }
        }

        #endregion
    }
}