using System;

namespace LifeGame
{
    internal abstract class Key
    {
        private ConsoleKey input;
       
        public ConsoleKey Input
        {
            get => input;
            set => input = value;
        }

        public abstract bool Action(Life life);
    }
}