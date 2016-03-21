using MiNET.Blocks;
using System;

namespace Saresh
{
    public class Utils
    {
        public static void WriteConsoleLine(string message, ConsoleColor color = ConsoleColor.Yellow)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("[Saresh] " + message.PadRight(Console.WindowWidth - 1));
            Console.ResetColor();
        }

        public static Block ConvertBlock(Block block)
        {
            /*
            // TODO
            switch (block.Id)
            {
                case 0:
                    return 0;
                    break;
                default:
                    return id;
                    break;
            }
            */
            return block;
        }
    }
}
