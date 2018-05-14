using System;
using System.Collections.Generic;

namespace LifeGame
{
    internal class KeyRead : ICommand
    {
        private static bool exit = true;
        private ConsoleKey input;
        private List<IKey> keys = new List<IKey>();

        public KeyRead(ConsoleKey input)
        {
            this.input = input;
            keys.Add(new MoveLeft());
            keys.Add(new MoveRight());
            keys.Add(new MoveUp());
            keys.Add(new MoveDown());
            keys.Add(new CellState());
            keys.Add(new ExitInput());
        }

        public static bool Exit
        {
            get => exit;
        }

        public void Execute()
        {
            foreach (IKey key in keys)
            {
                if (key.Input == input)
                {
                    if (!key.Action())
                    {
                        exit = false;
                    }
                    break;
                }
            }
        }
    }
}