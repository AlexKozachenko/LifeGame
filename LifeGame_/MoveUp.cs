using System;

namespace LifeGame
{
    internal class MoveUp : IKey
    {
        private const ConsoleKey input = ConsoleKey.UpArrow;

        public ConsoleKey Input
        {
            get => input;
        }

        public bool Action()
        {
            Life.CellOrdinateY--;
            // если ордината заходит на верхнюю границу рамки, смещаем под нее (0 - счетчик, 1 - рамка, 2 - начальная ордината)
            if (Life.CellOrdinateY == 1)
            {
                Life.CellOrdinateY = 2;
            }
            return true;
        }
    }
}