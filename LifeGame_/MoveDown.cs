using System;

namespace LifeGame
{ 
    internal class MoveDown : Life, IKey
    {
        private const ConsoleKey input = ConsoleKey.DownArrow;

        public ConsoleKey Input
        {
            get => input;
        }

        public bool Action()
        {
            cellOrdinateY++;
            // если ордината заходит на нижнюю границу, устанавливаем над ней
            if (cellOrdinateY > fieldHeight - 1)
            {
                cellOrdinateY = fieldHeight - 1;
            }
            return true;
        }
    }
}