using System.Collections.Generic;

namespace LifeGame
{
    internal class Keyboard
    {
        private IEnumerable<Key> keyList = new List<Key>();

        public Keyboard()
        {
            List<Key> keys = (List<Key>)keyList;
            keys.Add(new MoveUp());
            keys.Add(new MoveDown());
            keys.Add(new MoveLeft());
            keys.Add(new MoveRight());
            keys.Add(new CellState());
            keys.Add(new ExitInput());
        }

        public IEnumerator<Key> GetEnumerator()
        {
            return keyList.GetEnumerator();
        }
    }
}