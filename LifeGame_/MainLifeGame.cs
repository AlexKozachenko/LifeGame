using System;

namespace LifeGame
{
    internal class MainLifeGame
    {
        public static void Main(string[] arguments)
        {
            const int frameDoubleThickness = 2;
            const char heightMarker = 'h';
            const char speedMarker = 's';
            const char widthMarker = 'w';
            int height = 0;
            int speed = 0;
            int width = 0;
            // проверка на неформат
            foreach (string line in arguments)
            {
                if ((line[0] != widthMarker) 
                    && (line[0] != heightMarker) 
                    && (line[0] != speedMarker))
                {
                    return;
                }
            }
            // проверка на повторы ширины, высоты, скорости
            if (arguments.Length > 1)
            {
                for (int i = 0; i < arguments.Length; i++)
                {
                    for (int j = 0; j < arguments.Length; j++)
                    {
                        if ((j != i) && (arguments[j][0] == arguments[i][0]))
                        {
                            return;
                        }
                    }
                }
            }
            // сортировка по алфавиту
            SortStrings(arguments);
            // прерывание, если все, что после первого знака - не число
            foreach (string line in arguments)
            {
                bool isNumber = false;
                switch (line[0])
                {
                    case widthMarker:
                        isNumber = int.TryParse(line.Remove(0, 1), out width);
                        break;
                    case heightMarker:
                        isNumber = int.TryParse(line.Remove(0, 1), out height);
                        break;
                    case 's':
                        isNumber = int.TryParse(line.Remove(0, 1), out speed);
                        break;
                }
                if (!isNumber)
                {
                    return;
                }
            }
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
                            return;
                        case widthMarker:
                            ShowMessage("Height ");
                            return;
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
                            return;
                        }
                        else
                        {
                            if ((arguments[0][0] == speedMarker
                                && arguments[1][0] == widthMarker))
                            {
                                ShowMessage("Height ");
                                return;
                            }
                        }
                    }
                    break;
                case 3:
                    lifeGame = new Life(height + frameDoubleThickness, width + frameDoubleThickness, speed);
                    break;
            }
            lifeGame.Game();
            Console.ReadKey();
        }

        public static void ShowMessage(string line)
        {
            Console.WriteLine("Invalid arguments: ");
            Console.SetCursorPosition("Invalid arguments: ".Length, 0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0} of the Universe was not specified.", line);
            Console.ResetColor();
            Console.ReadKey();
        }

        public static void SortStrings(string[] strings)
        {
            if (strings.Length > 1)
            {
                string temp;
                for (int i = 0; i < strings.Length - 1; i++)
                {
                    for (int j = i + 1; j < strings.Length; j++)
                    {
                        if (String.Compare(strings[i], strings[j]) > 0)
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