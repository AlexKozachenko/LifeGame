using System;
using System.Collections.Generic;

namespace LifeGame
{
    internal class KeyRead : ICommand
    {
        private static bool exit;

        private ConsoleKey input;
        private Dictionary<ConsoleKey, IKey> keys = new Dictionary<ConsoleKey, IKey>();

        public static bool Exit
        {
            get => exit;
            set => exit = value;
        }
        public KeyRead(ConsoleKey input)
        {
            exit = false;
            this.input = input;
            keys.Add(ConsoleKey.LeftArrow, new MoveLeft());
            keys.Add(ConsoleKey.RightArrow, new MoveRight());
            keys.Add(ConsoleKey.UpArrow, new MoveUp());
            keys.Add(ConsoleKey.DownArrow, new MoveDown());
            keys.Add(ConsoleKey.Enter, new CellState());
            keys.Add(ConsoleKey.Spacebar, new ExitInput());
        }
        public void Execute()
        {
            try
            {
                if (keys[input].Action())
                {
                    Exit = true;
                }
            }
            catch (KeyNotFoundException)
            {
                input = ConsoleKey.Enter;
            }
        }
    }
}
