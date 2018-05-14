using System;

namespace LifeGame
{
    internal class MainLifeGame
    {
        public static void Main(string[] args)
        {
            foreach (string line in args)
            {
                if (line[0] != 'w' && line[0] != 'h' && line[0] != 's')
                {
                    return;
                }
            }
            SortStrings(ref args);
            int h, w, s;
            Life lifeGame;
            switch (args.Length)
            {
                case 0:
                    lifeGame = new Life();
                    lifeGame.Game();
                    break;
                case 1:
                    if (args[0][0] == 's' 
                        && Int32.TryParse(args[0].Remove(0, 1), out s))
                    {
                        lifeGame = new Life(s);
                        lifeGame.Game();
                    }
                    else
                    {
                        return;
                    }
                    break;
                case 2:
                    if (args[0][0] == 'h' 
                        && args[1][0] == 'w' 
                        && Int32.TryParse(args[0].Remove(0, 1), out h) 
                        && Int32.TryParse(args[1].Remove(0, 1), out w))
                    {
                        lifeGame = new Life(h + 2, w + 2);
                        lifeGame.Game();
                    }
                    else
                    {
                        if (args[0][0] == 'h' && args[1][0] == 's'
                           && Int32.TryParse(args[0].Remove(0, 1), out h)
                           && Int32.TryParse(args[1].Remove(0, 1), out s))
                        {
                            ShowMessage("Width ");
                        }
                        else
                        {
                            if (args[0][0] == 's' && args[1][0] == 'w'
                               && Int32.TryParse(args[0].Remove(0, 1), out s)
                               && Int32.TryParse(args[1].Remove(0, 1), out w)) 
                            {
                                ShowMessage("Height ");
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    break;
                case 3:
                    if (args[0][0] == 'h' 
                        && args[1][0] == 's' 
                        && args[2][0] == 'w'
                        && Int32.TryParse(args[0].Remove(0, 1), out h)
                        && Int32.TryParse(args[1].Remove(0, 1), out s)
                        && Int32.TryParse(args[2].Remove(0, 1), out w))
                    {
                        lifeGame = new Life(h + 2, w + 2, s);
                        lifeGame.Game();
                    }
                    else
                    {
                        return;
                    }
                    break;
                default:
                    return;
            }
            Console.ReadKey();
        }

        public static void ShowMessage(string line)
        {
            Console.WriteLine("Invalid arguments: ");
            Console.SetCursorPosition("Invalid arguments: ".Length, 0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0} of the Universe was not specified.", line);
            Console.ResetColor();
            Console.ReadKey();
        }

        public static void SortStrings(ref string[] strings)
        {
            if (strings.Length > 1)
            {
                string temp;
                for (int i = 0; i < strings.Length - 1; i++)
                {
                    for (int j = i + 1; j < strings.Length; j++)
                    {
                        if (String.Compare(strings[i], strings[j]) > 0)
                        {
                            temp = strings[i];
                            strings[i] = strings[j];
                            strings[j] = temp;
                        }
                    }
                }
            }
            else
            {
                return;
            }
        }
    }
}


    
