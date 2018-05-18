using System;

namespace LifeGame
{ 
    internal class MoveDown : IKey
    {
        private const ConsoleKey input = ConsoleKey.DownArrow;

        public ConsoleKey Input
        {
            get => input;
        }

        public bool Action()
        {
            Life.CellOrdinateY++;
            // если ордината заходит на нижнюю границу, устанавливаем над ней
            if (Life.CellOrdinateY > Life.FieldHeight - 1)
            {
                Life.CellOrdinateY = Life.FieldHeight - 1;
            }
            return true;
        }
    }
}