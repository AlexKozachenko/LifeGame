using System;

namespace LifeGame
{
    internal class MoveLeft : Key
    {
        public MoveLeft()
        {
            Input = ConsoleKey.LeftArrow;
        }
        
        public override bool Action(ref Life life)
        {
            life.CellAbscissaX--;
            // если абсцисса заходит на левую границу рамки, смещаем на 1 вправо (0 - рамка, 1 - начальная абсцисса)
            if (life.CellAbscissaX == 0)
            {
                life.CellAbscissaX = 1;
            }
            return true;
        }
    }
}