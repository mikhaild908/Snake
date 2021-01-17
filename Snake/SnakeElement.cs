using System;
namespace Snake
{
    public class SnakeElement : DisplayElement
    {
        public bool IsHead { get; private set; } = false;
        public Direction CurrentDirection { get; private set; } = Direction.Right;
        public Direction PreviousDirection { get; private set; } = Direction.Right;
        
        public SnakeElement(Position position, ConsoleColor foregroundColor, ConsoleColor backgroundColor, string display = "0", bool isHead = false)
            : base(position, foregroundColor, backgroundColor, display)
        {
            IsHead = isHead;

            if (isHead)
            {
                CurrentDirection = Direction.Right;
            }
        }

        public void UpdatePosition(Direction previousElementPreviousDirection, PlayingArea playingArea)
        {
            try
            {
                ShowElement(ClearDisplay);

                switch (CurrentDirection)
                {
                    case Direction.Right:
                        CurrentPosition.UpdatePosition(CurrentPosition.X + 1, CurrentPosition.Y);
                        break;
                    case Direction.Left:
                        CurrentPosition.UpdatePosition(CurrentPosition.X - 1, CurrentPosition.Y);
                        break;
                    case Direction.Up:
                        CurrentPosition.UpdatePosition(CurrentPosition.X, CurrentPosition.Y - 1);
                        break;
                    case Direction.Down:
                        CurrentPosition.UpdatePosition(CurrentPosition.X, CurrentPosition.Y + 1);
                        break;
                    default:
                        break;
                }

                if (IsHeadOutOffBounds(playingArea))
                {
                    throw new Exception("Snake is out of bounds");
                }

                ShowElement(Display);

                if (!IsHead)
                {
                    PreviousDirection = CurrentDirection;
                    CurrentDirection = previousElementPreviousDirection;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ChangeDirection(Direction direction)
        {
            if (IsDirectionUpdateValid(direction))
            {
                PreviousDirection = CurrentDirection;
                CurrentDirection = direction;
            }
        }

        private bool IsDirectionUpdateValid(Direction direction)
        {
            return
                (CurrentDirection == Direction.Left || CurrentDirection == Direction.Right)
                && (direction == Direction.Up || direction == Direction.Down)
                ||
                (CurrentDirection == Direction.Up || CurrentDirection == Direction.Down)
                && (direction == Direction.Left || direction == Direction.Right);
        }

        private bool IsHeadOutOffBounds(PlayingArea playingArea)
        {
            return (this.IsHead && (this.CurrentPosition.X < 0 || this.CurrentPosition.X > playingArea.Width
                || this.CurrentPosition.Y < 0 || this.CurrentPosition.Y > playingArea.Height));
        }
    }
}
