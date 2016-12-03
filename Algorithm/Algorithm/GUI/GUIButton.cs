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
    public class GUIButton:GUILabel
    {
        public Texture2D butTex;
        public Color BkColor;
        public Color BkColorChange;
        public Action OnPressed;
        private Color currentColor;
        private bool pressed;
        public GUIButton(Texture2D butTex, Vector2 pos, Vector2 size, string text, Color fontColor, Color bgColor, Color bgColorChange, Action onPress = null)
            : base(pos, size, text, fontColor)
        {
            this.butTex = butTex;
            BkColor = bgColor;
            BkColorChange = bgColorChange;
            OnPressed = onPress;
            currentColor = BkColor;
        }
        public GUIButton(Vector2 pos, Vector2 size, string text, Color bgColor, Action onPress = null)
            : this(Game1.DefaultTex,pos, size, text, Color.Black, bgColor, Color.White, onPress)
        {

        }
        public override void Update()
        {
            if (hitbox.Contains(new Point(Game1.ms.X, Game1.ms.Y)))
            {
                currentColor = BkColorChange;
                if (Game1.LeftDown && !pressed)
                {
                    pressed = true;
                    if (OnPressed != null)
                        OnPressed();
                }
                if(!Game1.LeftDown){pressed=false;}
            }
            else
            {
                currentColor = BkColor;
            }
            Vector2 strRect = Game1.defFont.MeasureString(Text);
            strPos = new Vector2(Center.X - strRect.X / 2, Center.Y - strRect.Y / 2);
        }
        public override void Draw(SpriteBatch sb)
        {
            if (butTex == null) butTex = Game1.DefaultTex;
            sb.Draw(butTex, hitbox, null,currentColor);
            sb.DrawString(Game1.defFont, Text,strPos, Color.Black);
        }
    }
}
