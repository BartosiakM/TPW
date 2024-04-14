using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Window
    {
        public int Width { get; init; }

        public int Height { get; init; }

        public Window(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
