
using System;

namespace CaveGame
{
    class VoidM
    {
        public static bool VisionApplied = false;
        public static bool CollisionC = false;
        public static bool VisionCheat = false;

        public static void ActivateCheat()
        {
            Console.Clear();


            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("[CHEAT] v0idmenu // by v0idDuck");
            Console.WriteLine("C.Disable/Enable collision");
            Console.WriteLine("V.Enable/Disable vision");
            Console.WriteLine("`/~. Close menu");
            while (true)
            {
                Console.Write("x:/> ");
            
                char key = Console.ReadKey().KeyChar;

                switch (key)
                {
                    case 'c':
                    case 'C':
                        CollisionC = !CollisionC;
                        Console.WriteLine("\r\n[CHEAT] Collision switched!");
                        break;
                    case 'v':
                    case 'V':
                        VisionCheat = !VisionCheat;
                        Console.WriteLine("\r\n[CHEAT] Vision switched!");
                        break;
                    case '`':
                    case '~':
                        // закрыть меню
                        Console.Clear();
                        Console.ResetColor();
                        return;
                }

                Thread.Sleep(200);
            }
        }

        public static void ActivateVision()
        {
            VisionCheat = !VisionCheat;
        }
    }
}