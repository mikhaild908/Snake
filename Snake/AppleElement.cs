using System;

namespace Snake
{
    public class FruitElement : DisplayElement
    {   
        public FruitElement(Position position, ConsoleColor foregroundColor, ConsoleColor backgroundColor, string display = "0")
            : base(position, foregroundColor, backgroundColor, display)
        {
        }
    }
}
