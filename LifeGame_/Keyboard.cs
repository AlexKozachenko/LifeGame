using System.Collections/*.Generic*/;

namespace LifeGame
{
    internal class Keyboard
    {
        private static ArrayList keys = new ArrayList();

        public Keyboard()
        {
            keys.Add(new MoveLeft());
            keys.Add(new MoveRight());
            keys.Add(new MoveUp());
            keys.Add(new MoveDown());
            keys.Add(new CellState());
            keys.Add(new ExitInput());
        }

        public ArrayList Keys { get => keys;}
    }
}