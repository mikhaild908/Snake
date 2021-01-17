namespace Snake
{
    public class Position
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Position (int x, int y)
        {
            UpdatePosition(x, y);
        }

        public void UpdatePosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}