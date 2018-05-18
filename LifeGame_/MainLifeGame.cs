using System;

namespace LifeGame
{
    internal class MainLifeGame
    {
        private const int frameDoubleThickness = 2;
        private const char heightMarker = 'h';
        private const char speedMarker = 's';
        private const char widthMarker = 'w';
        private static int height = 0;
        private static int speed = 0;
        private static int width = 0;

        public static void Main(string[] arguments)
        {
            if (CheckFormat(arguments))
            {
                // сортировка по алфавиту
                SortStrings(arguments);
                Life lifeGame = null;
                // инициализация игры разными конструкторами в зависимости от к-ва параметров
                switch (arguments.Length)
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
                    lifeGame.Game();
                    Console.ReadKey();
                }
            }
        }

        private static bool CheckFormat(string[] lines)
        {
            bool isNumber = true;
            foreach (string line in lines)
            {
                switch (line[0])
                {
                    case widthMarker:
                        isNumber = int.TryParse(line.Remove(0, 1), out width);
                        break;
                    case heightMarker:
                        isNumber = int.TryParse(line.Remove(0, 1), out height);
                        break;
                    case speedMarker:
                        isNumber = int.TryParse(line.Remove(0, 1), out speed);
                        break;
                    default:
                        isNumber = false;
                        break;
                }
                if (!isNumber)
                {
                    break;
                }
            }
            return isNumber;
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

        private static void SortStrings(string[] strings)
        {
            if (strings.Length > 1)
            {
                string temp;
                for (int i = 0; i < strings.Length - 1; i++)
                {
                    for (int j = i + 1; j < strings.Length; j++)
                    {
                        if (string.Compare(strings[i], strings[j]) > 0)
                        {
                            temp = strings[i];
                            strings[i] = strings[j];
                            strings[j] = temp;
                        }
                    }
                }
            }
        }
    }
}