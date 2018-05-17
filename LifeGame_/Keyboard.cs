namespace LifeGame
{
    internal class Keyboard
    {
        private static IKey[] keys = new IKey[6];

        public Keyboard()
        {
            keys[0] = new MoveLeft();
            keys[1] = new MoveRight();
            keys[2] = new MoveUp();
            keys[3] = new MoveDown();
            keys[4] = new CellState();
            keys[5] = new ExitInput();
        }

        public IKey[] Keys
        {
            get => keys;
        }
    }
}