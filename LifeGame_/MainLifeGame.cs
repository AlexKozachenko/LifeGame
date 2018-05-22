using System;
using System.Collections.Generic;

namespace LifeGame
{
    internal class MainLifeGame
    {
        private static List<string> arguments = new List<string>();
        private const int frameDoubleThickness = 2;
        private const char heightMarker = 'h';
        private const char speedMarker = 's';
        private const char widthMarker = 'w';
        private static int height = 0;
        private static int speed = 0;
        private static int width = 0;


        private static void CheckFormat(string[] lines)
        {
            bool isCorrect = true;
            foreach(string line in lines)
            {
                if (line != "")
                {
                    switch (line[0])
                    {
                        case widthMarker:
                            isCorrect = int.TryParse(line.Remove(0, 1), out width);
                            break;
                        case heightMarker:
                            isCorrect = int.TryParse(line.Remove(0, 1), out height);
                            break;
                        case speedMarker:
                            isCorrect = int.TryParse(line.Remove(0, 1), out speed);
                            break;
                        default:
                            isCorrect = false;
                            break;
                    }
                }
                else
                {
                    isCorrect = false;
                }
                if (isCorrect)
                {
                    arguments.Add(line);
                }
            }
        }

        public static void Main(string[] commandLineArguments)
        {
            CheckFormat(commandLineArguments);
            // сортировка по алфавиту
            arguments.Sort(string.Compare);
            Life lifeGame = null;
            // инициализация игры разными конструкторами в зависимости от к-ва параметров
            switch (arguments.Count)
            {
                case 0:
                    lifeGame = new Life();
                    break;
                case 1:
                    // прерывание с сообщением, если указанный параметр высота или ширина
                    switch (arguments[0][0])
                    {
                        case speedMarker:
                            lifeGame = new Life(speed);
                            break;
                        case heightMarker:
                            ShowMessage("Width ");
                            break;
                        case widthMarker:
                            ShowMessage("Height ");
                            break;
                    }
                    break;
                case 2:
                    if ((arguments[0][0] == heightMarker)
                        && (arguments[1][0] == widthMarker))
                    {
                        lifeGame = new Life(height + frameDoubleThickness, width + frameDoubleThickness);
                    }
                    // прерывание с сообщением, если один из указанных параметров - скорость
                    else
                    {
                        if ((arguments[0][0] == heightMarker)
                            && (arguments[1][0] == speedMarker))
                        {
                            ShowMessage("Width ");
                            break;
                        }
                        else
                        {
                            if ((arguments[0][0] == speedMarker
                                && arguments[1][0] == widthMarker))
                            {
                                ShowMessage("Height ");
                                break;
                            }
                        }
                    }
                    break;
                case 3:
                    lifeGame = new Life(height + frameDoubleThickness, width + frameDoubleThickness, speed);
                    break;
            }
            if (lifeGame != null)
            {
                Life.ManualInput(lifeGame);
                lifeGame.Game();
                Console.ReadKey();
            }
        }

        private static void ShowMessage(string line)
        {
            Console.WriteLine("Invalid arguments: ");
            Console.SetCursorPosition("Invalid arguments: ".Length, 0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0} of the Universe was not specified.", line);
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}

