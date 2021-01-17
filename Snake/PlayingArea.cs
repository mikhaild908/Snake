using System;
namespace Snake
{
    public class PlayingArea
    {
        public int Height { get; private set; }
        public int Width { get; private set; }

        public PlayingArea(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
