using System;
using System.Collections.Generic;
using System.Threading;

namespace LifeGame
{
    internal class Life
    {
        private const int fieldHeight = 12 ;
        private const int fieldWidth = 42;
        private const int speed = 300;
        private static int generationCounter = 0;
        private Cell[,] field = new Cell[fieldHeight, fieldWidth];
        private List<Cell[,]> history = new List<Cell[,]>();
        private Cell[,] previousField = new Cell[fieldHeight, fieldWidth];

        public static bool CompareFields(Cell[,] field1, Cell[,] field2)
        { 
            bool isEqual = true;
            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    if (field1[i, j].IsAlive != field2[i, j].IsAlive)
                    {
                        isEqual = false;
                        break;
                    }
                }
            }
            return isEqual;
        }

        public static void CopyField(Cell[,] source, Cell[,] destination)
        {
            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    destination[i, j].IsAlive = source[i, j].IsAlive;
                }
            }
        }

        private int CountLiveNeighborCells(int y, int x)
        {   // число измерений игрового пространства - 2
            const int dimensions = 2;
            const int maxCountOfNeighborCells = 8;
            // создаем двумерный массив для хранения координат соседних ячеек
            // абсциссы хранятся в строке 0
            const int xBuffer = 0;
            // ординаты хранятся в строке 1
            const int yBuffer = 1;
            int coordinate = 0;
            int liveNeighborCells = 0;
            // двумерный массив для хранения координат соседних ячеек
            int[,] neighborCells = new int[maxCountOfNeighborCells, dimensions];
            for (int i = y - 1; i <= y + 1; i++)
            {
                for (int j = x - 1; j <= x + 1; j++)
                {
                    if (i == y && j == x)
                    {
                        continue;
                    }
                    neighborCells[coordinate, yBuffer] = i;
                    neighborCells[coordinate, xBuffer] = j;
                    coordinate++;
                }
            }
            for (int i = 0; i < maxCountOfNeighborCells; i++)
            {
                if (neighborCells[i, xBuffer] < 1 || neighborCells[i, yBuffer] < 1)
                {
                    continue;
                }
                if (neighborCells[i, xBuffer] > fieldWidth - 1 || neighborCells[i, yBuffer] > fieldHeight - 1)
                {
                    continue;
                }
                if (previousField[neighborCells[i, yBuffer], neighborCells[i, xBuffer]].IsAlive)
                {
                    liveNeighborCells++;
                }
            }
            return liveNeighborCells;
        }

        public void Game()
        {
            Initialize(field);
            Initialize(previousField);
            ManualInput();
            int liveCells;
            bool isEqual = false;
            do
            {
                Thread.Sleep(speed);
                CopyField(field, previousField);
                NextGeneration();
                isEqual = IsEqual();
                liveCells = GetLiveCells(field);
                Console.SetCursorPosition(0, fieldHeight + 1);
                PrintField();
            } while (liveCells != 0 && !isEqual);
        }       

        private int GetLiveCells(Cell[,] field)
        {
            int liveCells = 0;
            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    if (field[i, j].IsAlive)
                    {
                        liveCells++;
                    }
                }
            }
            return liveCells;
        }

        private void Initialize(Cell[,] field)
        {
            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    field[i, j] = new Cell();
                }
            }
        }

        private bool IsEqual()
        {
            bool isEqual = false;
            foreach (Cell[,] member in history)
            {
                if (CompareFields(member, field))
                {
                    isEqual = true;
                }
            }
            return isEqual;
        }

        private void ManualInput()
        {   // координаты начальной точки (верхний левый угол поля) с учетом счетчика поколений и рамки
            int y = 2;
            int x = 1;
            ConsoleKey input;
            do
            {
                Console.CursorVisible = false; ;
                const char accentuationLetter = 'X';
                PrintField();
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(accentuationLetter);
                Console.ResetColor();
                Console.SetCursorPosition(x, y);
                input = Console.ReadKey().Key;
                switch (input)
                {
                    case ConsoleKey.UpArrow:
                        y--;
                        // если ордината заходит на верхнюю границу рамки, смещаем под нее (0 - счетчик, 1 - рамка, 2 - начальная ордината)
                        if (y == 1)
                        {
                            y = 2;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        y++;
                        // если ордината заходит на нижнюю границу, устанавливаем над ней
                        if (y > fieldHeight - 1)
                        {
                            y = fieldHeight - 1;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        x--;
                        // если абсцисса заходит на левую границу рамки, смещаем на 1 вправо (0 - рамка, 1 - начальная абсцисса)
                        if (x == 0)
                        {
                            x = 1;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        x++;
                        // если абсцисса заходит на правую границу рамки, смещаем на 2 влево, чтобы установить левее границы;
                        // отнимется 2, т.к. fieldWidth это количество клеток по ширине, но нумерация начинается с нуля,
                        // соответственно правая граница рамки это fieldWidth - 1, соотв. крайняя абсцисса - fieldWidth - 2
                        if (x > fieldWidth - 2)
                        {
                            x = fieldWidth - 2;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (field[y - 1, x].IsAlive)
                        {
                            field[y - 1, x].IsAlive = false;
                        }
                        else
                        {
                            field[y - 1, x].IsAlive = true;
                        }
                        break;
                }
            } while (input != ConsoleKey.Spacebar);
        }

        private void NextGeneration()
        {
            for (int i = 1; i < fieldHeight - 1; i++)
            {
                for (int j = 1; j < fieldWidth - 1; j++)
                {
                    int liveNeighbors = CountLiveNeighborCells(i, j);
                    if (!previousField[i, j].IsAlive)
                    {   // согласно условию задания, жизнь зарождается, если вокруг текущей клетки 3 живых
                        if (liveNeighbors == 3)
                        {
                            field[i, j].IsAlive = true;
                        }
                    }
                    else
                    {   // согласно условию задания, жизнь в клетке умирает, если вокруг текущей клетки менее 2 или более 3-х живых
                        if (liveNeighbors < 2 || liveNeighbors > 3)
                        {
                            field[i, j].IsAlive = false;
                        }
                    }
                }
            }
            // запись предпоследней конфигурации в историю
            Cell[,] temp = new Cell[fieldHeight, fieldWidth];
            Initialize(temp);
            CopyField(previousField, temp);
            history.Add(temp);
            generationCounter++;
        }

        private void PrintField()
        {
            const char cellLetter = 'O';
            const char emptyCell = ' ';
            const char frameElement = '+';
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Generation: ");
            Console.SetCursorPosition("Generation: ".Length, 0);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(generationCounter);
            Console.ResetColor();
            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    if (i == 0 || j == 0 || j == fieldWidth - 1 || i == fieldHeight - 1)
                    { 
                        Console.Write(frameElement);
                    }
                    else
                    {
                        if (field[i, j].IsAlive)
                        {
                            Console.SetCursorPosition(j, i + 1);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(cellLetter);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(emptyCell);
                        }
                    }
                }
                Console.Write('\n');
            }
        }
    }
}