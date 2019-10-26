using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Vehicles
{
    public class Car : Vehicle
    {
        public Car(Direction direction) : base(direction)
        {
            Sprite = new CarSprite();
            this.reflectSpriteIfFacingLeft();
        }
    }
}
