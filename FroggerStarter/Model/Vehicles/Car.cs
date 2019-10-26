using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    public class Car : Vehicle
    {
        public Car(Direction direction) : base(direction)
        {
            Sprite = new CarSprite();
        }
    }
}
