using System;
using System.Timers;

namespace Snake
{
    class Program
    {
        #region Constants
        const ConsoleColor DEFAULT_BACKGROUND_COLOR = ConsoleColor.Black;
        const ConsoleColor DEFAULT_FOREGROUND_COLOR = ConsoleColor.Yellow;
        const string BLOCK = "0";
        const string APPLE = "\uF8FF";
        //const string APPLE = "🍎";
        #endregion

        #region Private Members
        static int _windowWidth;
        static int _windowHeight;
        
        static Timer _timer;
        static bool _gameOver = false;

        static Snake _snake;
        static FruitElement _apple;
        static Random _random = new Random();
        #endregion

        static void Main(string[] args)
        {
            Initialize();
            ReadKey();
        }

        static void Initialize()
        {
            try
            {
                Console.CursorVisible = false;

                Console.ForegroundColor = DEFAULT_FOREGROUND_COLOR;
                Console.BackgroundColor = DEFAULT_BACKGROUND_COLOR;
                Console.Clear();

                _windowWidth = Console.WindowWidth;
                _windowHeight = Console.WindowHeight;

                _snake = new Snake(new Position(_windowWidth / 2, _windowHeight / 2), DEFAULT_BACKGROUND_COLOR, BLOCK, new PlayingArea(_windowWidth, _windowHeight));
                _apple = new FruitElement(new Position(0, 0), ConsoleColor.Red, DEFAULT_BACKGROUND_COLOR, APPLE);

                DisplayFruit(_apple);

                _timer = new Timer(100);
                _timer.Elapsed += (sender, e) =>
                {
                    try
                    {
                        if (!_gameOver)
                        {
                            _snake.UpdatePosition();

                            if (HasSnakeCollidedWithFruit())
                            {
                                DisplayFruit(_apple);
                                
                                // TODO: grow snake
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _timer.Stop();
                        _gameOver = true;

                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Press any key to exit.");
                    }
                };

                _timer.Start();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
            }
        }

        static bool HasSnakeCollidedWithFruit()
        {
            return _snake.DidSnakeCollideWithADisplayElement(_apple);
        }

        static void SetRandomPositionForDisplayElement(DisplayElement element)
        {
            var x = _random.Next(0, _windowWidth + 1);
            var y = _random.Next(0, _windowHeight + 1);

            element.CurrentPosition = new Position(x, y);
        }

        static void DisplayFruit(DisplayElement fruit)
        {
            SetRandomPositionForDisplayElement(fruit);
            _apple.ShowElement(fruit.Display);
        }

        static void ReadKey()
        {
            ConsoleKeyInfo keyInfo;

            try
            {
                while (!_gameOver && (keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
                {
                    _snake.ChangeDirection(keyInfo.Key);
                }

                return;
            }
            catch (Exception ex)
            {
                //Console.Clear();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
