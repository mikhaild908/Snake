using System;
using System.Collections.Generic;

namespace Snake
{
    public class Snake
    {
        #region Snake Elements
        private readonly SnakeElement _head;
        private readonly List<SnakeElement> _snakeElements = new List<SnakeElement>();
        private readonly PlayingArea _playingArea;
        #endregion

        public Snake(Position startingPosition, ConsoleColor headBackgroundColor, string display, PlayingArea playingArea)
        {
            _head = new SnakeElement(startingPosition, ConsoleColor.DarkGray, headBackgroundColor, display, true);
            _playingArea = playingArea;

            _snakeElements.Add(_head);
            AddSnakeElement(ConsoleColor.Yellow, headBackgroundColor, display);
            AddSnakeElement(ConsoleColor.Red, headBackgroundColor, display);
            AddSnakeElement(ConsoleColor.Red, headBackgroundColor, display);
            AddSnakeElement(ConsoleColor.Yellow, headBackgroundColor, display);
            AddSnakeElement(ConsoleColor.DarkGray, headBackgroundColor, display);
            AddSnakeElement(ConsoleColor.DarkGray, headBackgroundColor, display);
            AddSnakeElement(ConsoleColor.Yellow, headBackgroundColor, display);
            AddSnakeElement(ConsoleColor.Red, headBackgroundColor, display);
            AddSnakeElement(ConsoleColor.Red, headBackgroundColor, display);
            AddSnakeElement(ConsoleColor.Yellow, headBackgroundColor, display);
            AddSnakeElement(ConsoleColor.DarkGray, headBackgroundColor, display);
            AddSnakeElement(ConsoleColor.DarkGray, headBackgroundColor, display);
            AddSnakeElement(ConsoleColor.Yellow, headBackgroundColor, display);
        }

        public void AddSnakeElement(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string display)
        {
            var lastElement = _snakeElements[_snakeElements.Count - 1];

            int newX = 0, newY = 0;

            switch (lastElement.CurrentDirection)
            {
                case Direction.Right:
                    newX = lastElement.CurrentPosition.X - 1;
                    newY = lastElement.CurrentPosition.Y;
                    break;
                case Direction.Left:
                    newX = lastElement.CurrentPosition.X + 1;
                    newY = lastElement.CurrentPosition.Y;
                    break;
                case Direction.Up:
                    newX = lastElement.CurrentPosition.X;
                    newY = lastElement.CurrentPosition.Y + 1;
                    break;
                case Direction.Down:
                    newX = lastElement.CurrentPosition.X;
                    newY = lastElement.CurrentPosition.Y - 1;
                    break;
                default:
                    break;
            }


            var newTail = new SnakeElement(new Position(newX, newY), foregroundColor, backgroundColor, display, false);
            _snakeElements.Add(newTail);
        }

        public void ChangeDirection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    _head.ChangeDirection(Direction.Up);
                    break;
                case ConsoleKey.RightArrow:
                    _head.ChangeDirection(Direction.Right);
                    break;
                case ConsoleKey.DownArrow:
                    _head.ChangeDirection(Direction.Down);
                    break;
                case ConsoleKey.LeftArrow:
                    _head.ChangeDirection(Direction.Left);
                    break;
            }
        }

        public void UpdatePosition()
        {
            try
            {
                for (int i = 0; i < _snakeElements.Count; i++)
                {
                    var previousElementPreviousDirection = i > 1 ? _snakeElements[i - 1].PreviousDirection : _snakeElements[0].CurrentDirection;
                    _snakeElements[i].UpdatePosition(previousElementPreviousDirection, _playingArea);

                    if (DidHeadCollideWithItsBody())
                    {
                        throw new Exception("Game over");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool DidHeadCollideWithItsBody()
        {
            for (int i = 1; i < _snakeElements.Count; i++)
            {
                if (DidSnakeCollideWithADisplayElement(_snakeElements[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public bool DidSnakeCollideWithADisplayElement(DisplayElement element)
        {
            return _head.CurrentPosition.X == element.CurrentPosition.X && _head.CurrentPosition.Y == element.CurrentPosition.Y;
        }
    }
}
