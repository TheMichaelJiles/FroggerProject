using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Vehicles
{
    public class Truck : Vehicle
    {
        public Truck(Direction direction) : base(direction)
        {
            Sprite = new TruckSprite();
            this.reflectSpriteIfFacingLeft();
        }
    }
}
