using System;

namespace LifeGame
{
    public interface IKey
    {
        bool Action();
        ConsoleKey Input
        {
            get;
        }
    }
}
