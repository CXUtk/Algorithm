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
    /// 游戏物体管理器
    /// </summary>
    public class ObjectManager:DrawableGameComponent
    {
        public List<GUI.GUI> GUIs = new List<GUI.GUI>();
        public Game1 instance;
        public ObjectManager(Game game)
            : base(game)
        {
        }
        public override void Initialize()
        {
            instance = (Game1)Game;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            GUIs.Add(new GUI.GUIButton(Game1.DefaultTex,new Vector2(20, 20), new Vector2(50, 30), "Reset", Color.Black, Color.Gray, Color.White, () =>
            {
                (Game as Game1).ResetData();
            }));
            GUIs.Add(new GUI.GUIButton(Game1.DefaultTex, new Vector2(630, 20), new Vector2(150, 30), "Bad Condition", Color.Black, Color.Gray, Color.White, () =>
            {
                instance.ResetBadData();
            }));
            GUIs.Add(new GUI.GUIButton(Game1.DefaultTex,new Vector2(80, 20), new Vector2(50, 30), "Start", Color.Black, Color.Gray, Color.White, () =>
            {
                if (instance.th != null)
                {
                    instance.th.Abort();
                }
                instance.th = new Thread(() =>
                {
                    //instance.QuickSort(instance.datas, 0, instance.datas.Length - 1);
                    instance.Caculate(instance.nAlgorithm);
                });
                instance.th.Start();
                instance.th.IsBackground = true;
            }));
            GUIs.Add(new GUI.GUILabel(new Vector2(200, 10), new Vector2(30, 50), "Delay: ", Color.Black, delegate() { return "Delay: " + instance.Delay.ToString(); }));
            GUIs.Add(new GUI.GUILabel(new Vector2(350, 10), new Vector2(30, 50), "", Color.Black, delegate() {
                switch (instance.nAlgorithm)
                {
                    case CalType.InsertionSort:
                        return "InsertSort";
                    case CalType.BubbleSort:
                        return "BubbleSort";
                    case CalType.ShellSort:
                        return "ShellSort";
                    case CalType.QuickSort:
                        return "QuickSort";
                    case CalType.MergeSort:
                        return "MergeSort";
                    default:
                        return "";
                }
            }));
        }
        public override void Update(GameTime gameTime)
        {
            foreach (GUI.GUI gb in GUIs)
            {
                if (gb.isActive)
                {
                    gb.Update();
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            Game1.spriteBatch.Begin();
            foreach (GUI.GUI gb in GUIs)
            {
                if (gb.isActive)
                {
                    gb.Draw(Game1.spriteBatch);
                }
            }
            Game1.spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
