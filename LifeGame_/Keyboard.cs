using System.Collections.Generic;

namespace LifeGame
{
    internal class Keyboard
    {
        protected internal static List<IKey> keys = new List<IKey>();

        public Keyboard()
        {
            keys.Add(new MoveLeft());
            keys.Add(new MoveRight());
            keys.Add(new MoveUp());
            keys.Add(new MoveDown());
            keys.Add(new CellState());
            keys.Add(new ExitInput());
        }
    }
}