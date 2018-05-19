using System;

namespace LifeGame
{
    internal class MoveUp : Key
    {
        public MoveUp()
        {
            Input = ConsoleKey.UpArrow;
        }

        public override bool Action(ref Life life)
        {
            life.CellOrdinateY--;
            // если ордината заходит на верхнюю границу рамки, смещаем под нее (0 - счетчик, 1 - рамка, 2 - начальная ордината)
            if (life.CellOrdinateY == 1)
            {
                life.CellOrdinateY = 2;
            }
            return true;
        }
    }
}