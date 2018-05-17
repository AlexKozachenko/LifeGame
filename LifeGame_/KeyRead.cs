﻿using System;

namespace LifeGame
{
    internal class KeyRead : Keyboard, ICommand
    {
        private static bool exit = true;
        private ConsoleKey input;
       
        public KeyRead(ConsoleKey input)
        {
            this.input = input;
        }

        public static bool Exit
        {
            get => exit;
        }

        public void Execute()
        {
            foreach (IKey key in Keys)
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