using System;
namespace Snake
{
    public class DisplayElement
    {
        public string Display { get; protected set; } = "0";
        public Position CurrentPosition { get; set; }

        protected ConsoleColor ForegroundColor { get; set; }
        protected ConsoleColor BackgroundColor { get; set; }
        protected string ClearDisplay = " ";

        public DisplayElement(Position position, ConsoleColor foregroundColor, ConsoleColor backgroundColor, string display = "0")
        {
            Display = display;
            CurrentPosition = position;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public void ShowElement(string s)
        {
            try
            {
                Console.ForegroundColor = ForegroundColor;
                Console.BackgroundColor = BackgroundColor;
                Console.SetCursorPosition(CurrentPosition.X, CurrentPosition.Y);
                Console.CursorVisible = false;
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
