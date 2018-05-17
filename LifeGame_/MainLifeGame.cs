using System;

namespace LifeGame
{
    internal class MainLifeGame
    {
        public static void Main(string[] arguments)
        {
            const int frameDoubleThickness = 2;
            int height = 0, width = 0, speed = 0;
            // проверка на неформат
            foreach (string line in arguments)
            {
                if ((line[0] != 'w') && (line[0] != 'h') && (line[0] != 's'))
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
                    case 'w':
                        isNumber = int.TryParse(line.Remove(0, 1), out width);
                        break;
                    case 'h':
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
                        case 's':
                            lifeGame = new Life(speed);
                            break;
                        case 'h':
                            ShowMessage("Width ");
                            return;
                        case 'w':
                            ShowMessage("Height ");
                            return;
                    }
                    break;
                case 2:
                    if ((arguments[0][0] == 'h') 
                        && (arguments[1][0] == 'w'))
                    {
                        lifeGame = new Life(height + frameDoubleThickness, width + frameDoubleThickness);
                    }
                    // прерывание с сообщением, если один из указанных параметров - скорость
                    else
                    {
                        if ((arguments[0][0] == 'h') 
                            && (arguments[1][0] == 's'))
                        {
                            ShowMessage("Width ");
                            return;
                        }
                        else
                        {
                            if ((arguments[0][0] == 's' 
                                && arguments[1][0] == 'w'))
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