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
    public class GUILabel:GUI
    {
        public string Text;
        public Color FontColor;
        protected Vector2 strPos;
        private Func<string> _updateStr;
        public GUILabel(Vector2 drawPos, Vector2 boxSize, string text,Color c,Func<string> update=null)
            : base(drawPos, boxSize)
        {
            Text = text;
            FontColor = c;
            _updateStr = update;
        }
        public override void Update()
        {
            if (_updateStr != null)
            {
                Text = _updateStr();
            }
            Vector2 strRect = Game1.defFont.MeasureString(Text);
            strPos = new Vector2(Center.X - strRect.X / 2, Center.Y - strRect.Y / 2);
            base.Update();
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.DrawString(Game1.defFont, Text, strPos, FontColor);
            base.Draw(sb);
        }
    }
}
