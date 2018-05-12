namespace LifeGame
{
     internal class MoveLeft : Life, IKey
    {
        public bool Action()
        {
            cellAbscissaX--;
            // если абсцисса заходит на левую границу рамки, смещаем на 1 вправо (0 - рамка, 1 - начальная абсцисса)
            if (cellAbscissaX == 0)
            {
                cellAbscissaX = 1;
            }
            return true;
        }
    }
}