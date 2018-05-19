using System;
using System.Collections.Generic;
using System.Threading;

namespace LifeGame
{
    internal class Life
    {
        // координаты начальной точки (верхний левый угол поля) с учетом счетчика поколений и рамки
        private int cellAbscissaX = 1;
        private int cellOrdinateY = 2;
        private Cell[,] field;
        private int fieldHeight = 12;
        private int fieldWidth = 42;
        private int generationCounter = 0;
        private List<Cell[,]> history = new List<Cell[,]>();
        private Cell[,] previousField;
        private int speed = 300;
      
        public Life()
        {
            Initialization();
        }

        public Life(int speed_)
        {
            speed = speed_;
            Initialization();
        }

        public Life(int height, int width)
        {
            fieldHeight = height;
            fieldWidth = width;
            Initialization();
        }

        public Life(int height, int width, int speed_)
        {
            fieldHeight = height;
            fieldWidth = width;
            speed = speed_;
            Initialization();
        }

        public int CellAbscissaX
        {
            get => cellAbscissaX;
            set => cellAbscissaX = value;
        }

        public int CellOrdinateY
        {
            get => cellOrdinateY;
            set => cellOrdinateY = value;
        }

        public Cell[,] Field
        {
            get => field;
        }

        public int FieldHeight
        {
            get => fieldHeight;
        }

        public int FieldWidth
        {
            get => fieldWidth;
        }

        public static void ManualInput(ref Life life)
        {
            do
            {
                const char accentuationLetter = 'X';
                Console.CursorVisible = false;
                life.PrintField();
                Console.SetCursorPosition(life.CellAbscissaX, life.CellOrdinateY);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(accentuationLetter);
                Console.ResetColor();
                Console.SetCursorPosition(life.CellAbscissaX, life.CellOrdinateY);
                ICommand useKey = new KeyRead(Console.ReadKey().Key, ref life);
                useKey.Execute();
            } while (KeyRead.Exit);
        }

        private bool CompareFields(Cell[,] field1, Cell[,] field2)
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

        private void CopyField(Cell[,] source, Cell[,] destination)
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
            int liveNeighborCells = 0;
            int neighborCellIndex = 0;
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
                    neighborCells[neighborCellIndex, yBuffer] = i;
                    neighborCells[neighborCellIndex, xBuffer] = j;
                    neighborCellIndex++;
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
            int liveCells;
            bool isEqual = false;
            do
            {
                Thread.Sleep(speed);
                CopyField(field, previousField);
                NextGeneration();
                isEqual = IsEqual();
                liveCells = GetLiveCells(field);
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

        private void Initialization()
        {
            field = new Cell[fieldHeight, fieldWidth];
            previousField = new Cell[fieldHeight, fieldWidth];
            InitializeField(field);
            InitializeField(previousField);
        }

        private void InitializeField(Cell[,] field)
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
            InitializeField(temp);
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
            Console.Write(generationCounter);
            Console.Write('\n');
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