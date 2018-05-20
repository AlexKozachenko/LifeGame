using System;

namespace LifeGame
{
    internal class KeyRead : ICommand
    {
        private static bool exit = true;
        private ConsoleKey input;
        private Life life;

        public KeyRead(ConsoleKey input, Life life)
        {
            this.life = life;
            this.input = input;
        }

        public static bool Exit
        {
            get => exit;
        }

        public void Execute()
        {
            foreach (Key key in new Keyboard())
            {
                if (key.Input == input)
                {
                    if (!key.Action(life))
                    {
                        exit = false;
                    }
                    break;
                }
            }
        }
    }
}