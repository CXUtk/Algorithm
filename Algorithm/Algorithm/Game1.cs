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
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static Texture2D DefaultTex;
        public static SpriteFont defFont;
        public static int ScreenHeight;
        public static int ScreenWidth;
        public static MouseState ms;
        public static KeyboardState keyState;
        public static bool LeftDown;
        public static bool LeftReleased;
        public static Random rand;
        internal Thread th;
        public AlgorithmData[] datas = new AlgorithmData[100];
        AlgorithmData[] temp = new AlgorithmData[100];
        public float range = 1;
        public int Delay = 20;
        public bool isFinished = false;
        public CalType nAlgorithm;
        public List<float> timers = new List<float>();
        public bool displayMode = true;

        private int timer1 = 0;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Components.Add(new ObjectManager(this));
            // TODO: Add your initialization logic here
            ScreenWidth = Window.ClientBounds.Width;
            ScreenHeight = Window.ClientBounds.Height;
            rand = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 10; i++)
            {
                timers.Add(0);
            }
            ResetData();
            nAlgorithm = CalType.BubbleSort;
            base.Initialize();
        }
        public void ResetData()
        {
            if (th != null) th.Abort();
            for (int i = 0; i < datas.Length; i++)
            {
                datas[i] = new AlgorithmData(rand.Next(50, ScreenHeight - 20));
                temp[i] = new AlgorithmData();
            }

            
        }
        public void ResetBadData()
        {
            if (th != null) th.Abort();
            for (int i = datas.Length-1; i > -1; i--)
            {
                datas[i] = new AlgorithmData((int)(((100-i)/(float)datas.Length)*(ScreenHeight-70))+50);
                temp[i] = new AlgorithmData();
            }

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            IsMouseVisible = true;
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            DefaultTex = Content.Load<Texture2D>("Images/Default");
            defFont = Content.Load<SpriteFont>("Fonts/DefaultFont");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            ms = Mouse.GetState();
            keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.OemPlus))
            {
                if (Delay < 1000)
                    Delay += 5;

            }
            if (keyState.IsKeyDown(Keys.OemMinus))
            {
                if (Delay > 5)
                {
                    Delay -= 5;
                }
            }
            if (keyState.IsKeyDown(Keys.D1) && timers[0]>40)
            {
                timers[0] = 0;
                nAlgorithm++;
                if (nAlgorithm >= (CalType)5)
                {
                    nAlgorithm = CalType.InsertionSort;
                }
            }
            if (ms.LeftButton == ButtonState.Pressed)
            {
                LeftDown = true;
            }
            if (ms.LeftButton == ButtonState.Released)
            {
                LeftDown = false;
            }
            if (timer1 < 180)
                timer1 = 180;
            if (timer1 == 190)
            {
                if (isFinished)
                {
                    timer1 = 200;
                }
            }
            timers[0]++;
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSeaGreen);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(DefaultTex, new Rectangle(20, 50, ScreenWidth - 40, ScreenHeight - 70), new Rectangle(0, 0, 1, 1), Color.White);
            if (timer1 == 190)
            {
                spriteBatch.DrawString(defFont, "Started!", new Vector2(0, 0), Color.Red);
            }
            else if (timer1 == 200)
            {
                spriteBatch.DrawString(defFont, "Stoped!", new Vector2(0, 0), Color.Red);
            }
            for (int i = 0; i < datas.Length; i++)
            {
                Vector2 drawPos = new Vector2(((ScreenWidth - 45f) / (float)datas.Length) * i + 25, datas[i].value);
                spriteBatch.Draw(DefaultTex, new Rectangle((int)drawPos.X, (int)drawPos.Y, 4, ScreenHeight - (int)drawPos.Y - 20), new Rectangle(0, 0, 4, 4), datas[i].color);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// 游戏退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected override void OnExiting(object sender, EventArgs args)
        {
            if (th != null)
            {
                if (th.IsAlive)
                {
                    th.Abort();
                }
            }
        }
        /// <summary>
        /// 用于进行处理算法代码及动画效果
        /// </summary>
        /// <param name="para">参数，目前保留</param>
        public void Caculate(CalType alg)
        {
            switch (alg)
            {
                case CalType.InsertionSort:
                    InsertSort();
                    break;
                case CalType.BubbleSort:
                    BubbleSort();
                    break;
                case CalType.ShellSort:
                    ShellSort();
                    break;
                case CalType.QuickSort:
                    QuickSort(datas, 0, datas.Length - 1);
                    break;
                case CalType.MergeSort:
                    MergeSort(datas,0,datas.Length-1,temp);
                    break;
                default:
                    break;
            }
            isFinished = true;
        }
        void ShellSort()
        {
            int i, j, k;
            k = datas.Length / 2;
            while (k >= 1)
            {
                for (i = k; i < datas.Length; i++)
                {
                    int temp = datas[i].value;
                    j = i - k;
                    while (j >= 0 && temp < datas[j].value)
                    {
                        datas[j + k].value = datas[j].value;
                        Display(j + k, j, Delay);
                        j -= k;
                    }
                    Display(j + k,i, Delay);
                    datas[j + k].value = temp;
                }
                Thread.Sleep(Delay);
                k /= 2;
            }//希尔排序法
        }
        void BubbleSort()
        {
            for (int j = 0; j < datas.Length; j++)
            {
                for (int i = 0; i < datas.Length - 1; i++)
                {
                    if (datas[i].value > datas[i + 1].value)
                    {
                        int t = datas[i].value;
                        datas[i].value = datas[i + 1].value;
                        datas[i + 1].value = t;
                        Display(i, i + 1, Delay);
                    }
                }
            }//冒泡排序
        }
        void InsertSort()
        {
            int j = 0;
            for (int i = 1; i < datas.Length; i++)
            {
                int temp = datas[i].value;
                Display(i, Delay);
                for (j = i; j > 0 && temp < datas[j - 1].value; j--)
                {
                    datas[j].value = datas[j - 1].value;
                    Display(j, j - 1, Delay);
                }
                datas[j].value = temp;
            }//插入排序法
        }
        int partition(AlgorithmData[] arr, int l, int r)
        {
            int key = arr[l].value;
            while (l < r)
            {
                while (l < r && arr[r].value >= key)
                {
                    r--;
                    Display(r, Delay);
                }
                if (l < r)
                {
                    arr[l++].value = arr[r].value;
                    Display(r, l, Delay);
                }
                while (l < r && arr[l].value <= key)
                {
                    l++;
                    Display(l, Delay);
                }
                if (l < r)
                {
                    arr[r--].value = arr[l].value;
                    Display(r, l, Delay);
                }
            }
            arr[l].value = key;
            return l;
        }
        public void QuickSort(AlgorithmData[] arr, int l, int r)
        {
            if (l < r)
            {
                int m = 0;
                m = partition(arr, l, r);
                QuickSort(arr, l, m - 1);
                QuickSort(arr, m + 1, r);
            }
        }
        void mergeArr(AlgorithmData[] arr, int l, int mid, int r, AlgorithmData[] temp)
        {
            int i = l, j = mid + 1;
            int m = mid, n = r;
            int k = 0;

            while (i <= m && j <= n)
            {
                if (arr[i].value <= arr[j].value)
                {
                    temp[k++].value = arr[i++].value;
                }
                else
                {
                    temp[k++].value = arr[j++].value;
                }
                Display(i, j - 1, Delay);
            }

            while (i <= m)
                temp[k++].value = arr[i++].value;

            while (j <= n)
                temp[k++].value = arr[j++].value;

            for (i = 0; i < k; i++)
            {
                arr[l + i].value = temp[i].value;
                Display(l + i, Delay);
            }
        }
        public void MergeSort(AlgorithmData[] arr, int l, int r, AlgorithmData[] temp)
        {
            if (l < r)
            {
                int mid = (l + r) / 2;
                MergeSort(arr, l, mid, temp);
                MergeSort(arr, mid + 1, r, temp);
                mergeArr(arr, l, mid, r, temp);
            }
        }
        public void Display(int i, int delay)
        {
            if (displayMode)
            {
                datas[i].color = Color.Red;
                Thread.Sleep(delay);
                datas[i].color = Color.Black;
            }
        }
        public void Display(int i,int j, int delay)
        {
            if (displayMode)
            {
                datas[i].color = Color.Red;
                datas[j].color = Color.Red;
                Thread.Sleep(delay);
                datas[i].color = Color.Black;
                datas[j].color = Color.Black;
            }
        }
    }
}