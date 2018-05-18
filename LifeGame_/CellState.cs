using System;

namespace LifeGame
{
    internal class CellState : Life, IKey
    {
        private const ConsoleKey input = ConsoleKey.Enter;

        public ConsoleKey Input
        {
            get => input;
        }

        public bool Action()
        {
            //ордината смещается вниз за счет счетчика поколений, поднимаем на 1
            field[cellOrdinateY - 1, cellAbscissaX].IsAlive = !(field[cellOrdinateY - 1, cellAbscissaX].IsAlive);
            return true;
        }
    }
}