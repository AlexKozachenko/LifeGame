using System;

namespace LifeGame
{
    internal class ExitInput : Life, IKey
    {
        private const ConsoleKey input = ConsoleKey.Spacebar;

        public ConsoleKey Input
        {
            get => input;
        }

        public bool Action()
        {
            return false;
        }
    }
}