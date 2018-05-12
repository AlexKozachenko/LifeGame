namespace LifeGame
{
    internal class CellState : Life, IKey
    {
        public bool Action()
        {
            //ордината смещается вниз за счет счетчика поколений, поднимаем на 1
            if (field[cellOrdinateY - 1, cellAbscissaX].IsAlive)
            {
                field[cellOrdinateY - 1, cellAbscissaX].IsAlive = false;
            }
            else
            {
                field[cellOrdinateY - 1, cellAbscissaX].IsAlive = true;
            }
            return true;
        }
    }
}
