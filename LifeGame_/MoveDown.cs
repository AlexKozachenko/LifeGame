using System;

namespace LifeGame
{ 
    internal class MoveDown : Key
    {
        public MoveDown()
        {
            Input = ConsoleKey.DownArrow;
        }
        
        public override bool Action(ref Life life)
        {
            life.CellOrdinateY++;
            // если ордината заходит на нижнюю границу, устанавливаем над ней
            if (life.CellOrdinateY > life.FieldHeight - 1)
            {
                life.CellOrdinateY = life.FieldHeight - 1;
            }
            return true;
        }
    }
}