using System;

namespace LifeGame
{
    internal class ExitInput : Key
    {
        public ExitInput()
        {
            Input = ConsoleKey.Spacebar;
        }

        public override bool Action(Life life)
        {
            return false;
        }
    }
}