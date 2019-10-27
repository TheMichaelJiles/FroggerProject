using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    public class FrogDeathAnimation : GameObject
    {
        public FrogDeathAnimation(BaseSprite frame)
        {
            Sprite = frame;
        }
    }
}
