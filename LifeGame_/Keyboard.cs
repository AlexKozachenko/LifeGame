using System.Collections.Generic;
using System.Collections;

namespace LifeGame
{
    internal class Keyboard : IEnumerable
    {
        private ICollection<Key> keys = new List<Key>();

        public Keyboard()
        {
            keys.Add(new MoveUp());
            keys.Add(new MoveDown());
            keys.Add(new MoveLeft());
            keys.Add(new MoveRight());
            keys.Add(new CellState());
            keys.Add(new ExitInput());
        }

        public IEnumerator GetEnumerator()
        {
            return keys.GetEnumerator();
        }
    }
}