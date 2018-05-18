using System;

namespace LifeGame
{
    internal class MoveLeft : IKey
    {
        private const ConsoleKey input = ConsoleKey.LeftArrow;

        public ConsoleKey Input
        {
            get => input;
        }

        public bool Action()
        {
            Life.CellAbscissaX--;
            // если абсцисса заходит на левую границу рамки, смещаем на 1 вправо (0 - рамка, 1 - начальная абсцисса)
            if (Life.CellAbscissaX == 0)
            {
                Life.CellAbscissaX = 1;
            }
            return true;
        }
    }
}