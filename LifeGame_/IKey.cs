using System;

namespace LifeGame
{
     public interface IKey
    {
        ConsoleKey Input
        {
            get;
        }

        bool Action();
    }
}