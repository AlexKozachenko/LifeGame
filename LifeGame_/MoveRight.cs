using System;

namespace LifeGame
{
    internal class MoveRight : IKey
    {
        private const ConsoleKey input = ConsoleKey.RightArrow;

        public ConsoleKey Input
        {
            get => input;
        }

        public bool Action()
        {
            Life.CellAbscissaX++;
            // если абсцисса заходит на правую границу рамки, смещаем на 2 влево, чтобы установить левее границы;
            // отнимется 2, т.к. fieldWidth это количество клеток по ширине, но нумерация начинается с нуля,
            // соответственно правая граница рамки это fieldWidth - 1, соотв. крайняя абсцисса - fieldWidth - 2
            if (Life.CellAbscissaX > Life.FieldWidth - 2)
            {
                Life.CellAbscissaX = Life.FieldWidth - 2;
            }
            return true;
        }
    }
}