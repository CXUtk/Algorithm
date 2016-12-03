using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Algorithm.GUI
{
    public abstract class GUI:GameObject
    {
        public bool isActive
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }
        protected bool active = true;
        public GUI(Vector2 drawPos, Vector2 boxSize)
            : base(drawPos, boxSize)
        {

        }
        public override Rectangle hitbox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            }
            set
            {
                hitbox = value;
            }
        }
    }
}
