using System;

namespace LifeGame
{
    internal class MoveRight : Life, IKey
    {
        private const ConsoleKey input = ConsoleKey.RightArrow;
        public ConsoleKey Input
        {
            get => input;
        }
        public bool Action()
        {
            cellAbscissaX++;
            // если абсцисса заходит на правую границу рамки, смещаем на 2 влево, чтобы установить левее границы;
            // отнимется 2, т.к. fieldWidth это количество клеток по ширине, но нумерация начинается с нуля,
            // соответственно правая граница рамки это fieldWidth - 1, соотв. крайняя абсцисса - fieldWidth - 2
            if (cellAbscissaX > fieldWidth - 2)
            {
                cellAbscissaX = fieldWidth - 2;
            }
            return true;
        }
    }
}