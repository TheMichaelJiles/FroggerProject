using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    class FrogHome : GameObject
    {
        public bool IsFilled { get; private set; }

        public FrogHome()
        {
            this.Sprite = new FrogHomeSprite
            {
                Visibility = Visibility.Collapsed
            };
            this.IsFilled = false;
        }

        public void ShowSprite()
        {
            this.Sprite.Visibility = Visibility.Visible;
            this.IsFilled = true;
        }
    }
}
