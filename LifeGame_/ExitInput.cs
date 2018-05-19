using System;

namespace LifeGame
{
    internal class ExitInput : Key
    {
        public ExitInput()
        {
            Input = ConsoleKey.Spacebar;
        }

        public override bool Action(ref Life life)
        {
            return false;
        }
    }
}