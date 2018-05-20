using System;

namespace LifeGame
{
    internal class CellState : Key
    {
        public CellState()
        {
            Input = ConsoleKey.Enter;
        }

        public override bool Action(Life life)
        {
            //ордината смещается вниз за счет счетчика поколений, поднимаем на 1
            life.Field[life.CellOrdinateY - 1, life.CellAbscissaX].IsAlive = !(life.Field[life.CellOrdinateY - 1, life.CellAbscissaX].IsAlive);
            return true;
        }
    }
}