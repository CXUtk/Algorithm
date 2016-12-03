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
    /// 算法数据及显示的类型
    /// </summary>
    public class AlgorithmData
    {
        public int value;
        public Color color;
        public float scale;
        public AlgorithmData(int value)
        {
            this.value = value;
            color = Color.Black;
        }
        public AlgorithmData()
        {
            this.value = 0;
            color = Color.Black;
        }
    }
    public enum CalType
    {
        InsertionSort=0,
        BubbleSort=1,
        ShellSort=2,
        QuickSort=3,
        MergeSort=4
    }
}
