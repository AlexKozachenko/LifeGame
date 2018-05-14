using System;

namespace LifeGame
{
    internal class MoveUp : Life, IKey
    {
        private const ConsoleKey input = ConsoleKey.UpArrow;
        public ConsoleKey Input
        {
            get => input;
        }
        public bool Action()
        {
            cellOrdinateY--;
            // если ордината заходит на верхнюю границу рамки, смещаем под нее (0 - счетчик, 1 - рамка, 2 - начальная ордината)
            if (cellOrdinateY == 1)
            {
                cellOrdinateY = 2;
            }
            return true;
        }
    }
}
