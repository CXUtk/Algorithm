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
using System.Threading;

namespace Algorithm
{
    /// <summary>
    /// 所有游戏物体的基类
    /// </summary>
    public abstract class GameObject
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Size;
        public virtual Vector2 Center
        {
            get
            {
                return new Vector2(Position.X + Size.X / 2, Position.Y + Size.Y / 2);
            }
        }
        public abstract Rectangle hitbox
        {
            get;
            set;
        }
        public GameObject(Vector2 pos, Vector2 size)
        {
            Position = pos;
            Size = size;
        }
        public virtual void Update() { }
        public virtual void Draw(SpriteBatch sb) { }
    }
}
