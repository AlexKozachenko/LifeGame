using System.Collections;

namespace LifeGame
{
    internal class Keyboard
    {
        private static ArrayList keys = new ArrayList();

        static Keyboard()
        {
            keys.Add(new MoveLeft());
            keys.Add(new MoveRight());
            keys.Add(new MoveUp());
            keys.Add(new MoveDown());
            keys.Add(new CellState());
            keys.Add(new ExitInput());
        }

        public static ArrayList Keys { get => keys;}
    }
}