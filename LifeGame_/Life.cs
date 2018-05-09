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
        private static int equalGenerationNumber = 0;
        private static int generationCounter = 0;
        private Cell[,] field = new Cell[fieldHeight, fieldWidth];
        private Dictionary<Cell[,], int> history = new Dictionary<Cell[,], int>();
        private Cell[,] previousField = new Cell[fieldHeight, fieldWidth];

        public static bool CompareFields(Cell[,] field1, Cell[,] field2)
        {
            bool isEqual = true;
            for (uint i = 0; i < fieldHeight; i++)
            {
                for (uint j = 0; j < fieldWidth; j++)
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

        private uint CountLiveNeighborCells(uint y, uint x)
        {
            // число измерений игрового пространства - 2
            const uint dimensions = 2;
            const uint maxCountOfNeighborCells = 8;
            // создаем двумерный массив для хранения координат соседних ячеек
            // абсциссы хранятся в строке 0
            const uint xBuffer = 0;
            // ординаты хранятся в строке 1
            const uint yBuffer = 1;
            uint coordinate = 0;
            uint liveNeighborCells = 0;
            // двумерный массив для хранения координат соседних ячеек
            uint[,] neighborCells = new uint[maxCountOfNeighborCells, dimensions];
            for (uint i = y - 1; i <= y + 1; i++)
            {
                for (uint j = x - 1; j <= x + 1; j++)
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
            for (uint i = 0; i < maxCountOfNeighborCells; i++)
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
                PrintField('O');
            } while (liveCells != 0 && !isEqual);
        }       

        private int GetLiveCells(Cell[,] field)
        {
            int liveCells = 0;
            for (uint i = 0; i < fieldHeight; i++)
            {
                for (uint j = 0; j < fieldWidth; j++)
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
            for (uint i = 0; i < fieldHeight; i++)
            {
                for (uint j = 0; j < fieldWidth; j++)
                {
                    field[i, j] = new Cell()
                    {
                        IsAlive = false
                    };
                }
            }
        }

        private bool IsEqual()
        {
            bool isEqual = false;
            foreach (KeyValuePair<Cell[,], int> member in history)
            {
                if (CompareFields(member.Key, field))
                {
                    equalGenerationNumber = member.Value;
                    isEqual = true;
                }
            }
            return isEqual;
        }

        private void ManualInput()
        {
            int x = 1;
            int y = 2;
            ConsoleKey input;
            do
            {
                PrintField('X');
                Console.SetCursorPosition(x, y);
                input = Console.ReadKey().Key;
                switch (input)
                {
                    case ConsoleKey.UpArrow:
                        y--;
                        if (y < 2)
                        {
                            y = 2;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        y++;
                        if (y > fieldHeight - 1)
                        {
                            y = fieldHeight - 1;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        x--;
                        if (x < 1)
                        {
                            x = 1;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        x++;
                        if (x > fieldWidth - 2)
                        {
                            x = fieldWidth - 2;
                        }
                        break;
                    case ConsoleKey.Enter:
                       if(field[y - 1, x].IsAlive)
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
            for (uint i = 1; i < fieldHeight - 1; i++)
            {
                for (uint j = 1; j < fieldWidth - 1; j++)
                {
                    uint liveNeighbors = CountLiveNeighborCells(i, j);
                    if (!previousField[i, j].IsAlive)
                    {
                        if (liveNeighbors == 3)
                        {
                            field[i, j].IsAlive = true;
                        }
                    }
                    else
                    {
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
            history.Add(temp, generationCounter);
            generationCounter++;
        }

        private void PrintField(Char cell)
        {
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
                        Console.Write('+');
                    }
                    else
                    {
                        if (field[i, j].IsAlive)
                        {
                            Console.SetCursorPosition(j, i + 1);
                            if (cell == 'X')
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            if (cell == 'O')
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                            }
                            Console.Write(cell);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(' ');
                        }
                    }
                }
                Console.Write('\n');
            }
        }
    }
}






