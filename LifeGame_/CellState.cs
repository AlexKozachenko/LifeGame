using System;

namespace LifeGame
{
    internal class CellState : IKey
    {
        private const ConsoleKey input = ConsoleKey.Enter;

        public ConsoleKey Input
        {
            get => input;
        }

        public bool Action()
        {
            //ордината смещается вниз за счет счетчика поколений, поднимаем на 1
            Life.Field[Life.CellOrdinateY - 1, Life.CellAbscissaX].IsAlive = !(Life.Field[Life.CellOrdinateY - 1, Life.CellAbscissaX].IsAlive);
            return true;
        }
    }
}