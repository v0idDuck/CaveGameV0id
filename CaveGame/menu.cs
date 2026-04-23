namespace CaveGame
{
    class Menu
    {
        public int ChoisedButton = 0;
        public string Play { get; } = "Играть";
        public string Settings { get; } = "Настройки";
        public string Quit { get; } = "Выйти";
        public string symbol { get; } = "<";

        public int width = Console.WindowWidth;
        public int height = Console.WindowHeight;
        
        private string[,] menus = new string[5, 14]
            {
    { "#"," ","#"," ","#","#"," ","#","#","#"," ","#"," ","#" },
    { "#","#","#"," ","#"," "," ","#"," ","#"," ","#"," ","#" },
    { "#","#","#"," ","#","#"," ","#"," ","#"," ","#"," ","#" },
    { "#"," ","#"," ","#"," "," ","#"," ","#"," ","#"," ","#" },
    { "#"," ","#"," ","#","#"," ","#"," ","#"," ","#","#","#" }
            };

        public void ShowMenuWord()
        {

            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth / 2) - (14 / 2), i);

                for (int j = 0; j < 14; j++)
                {
                    Console.Write(menus[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void ShowMenu()
        {
            int centerX = (Console.WindowWidth / 2) - (Play.Length / 2);
            int centerY = (Console.WindowHeight / 2) - 1;

            if (ChoisedButton  == 0)
            {
                Console.SetCursorPosition(centerX, centerY);
                Console.WriteLine(Play + symbol);
                Console.SetCursorPosition(centerX, centerY + 1);
                Console.WriteLine(Settings + " ");
                Console.SetCursorPosition(centerX, centerY + 2);
                Console.WriteLine(Quit + " ");
            }
            else if (ChoisedButton == 1)
            {
                Console.SetCursorPosition(centerX, centerY);
                Console.WriteLine(Play + " ");
                Console.SetCursorPosition(centerX, centerY + 1);
                Console.WriteLine(Settings + symbol);
                Console.SetCursorPosition(centerX, centerY + 2);
                Console.WriteLine(Quit + " ");
            }
            else
            {
                Console.SetCursorPosition(centerX, centerY);
                Console.WriteLine(Play + " ");
                Console.SetCursorPosition(centerX, centerY + 1);
                Console.WriteLine(Settings + " ");
                Console.SetCursorPosition(centerX, centerY + 2);
                Console.WriteLine(Quit + symbol);
            }
        }

        public int GetInputMenu()
        {
            ShowMenuWord();

            while (true)
            {
                if (width != Console.WindowWidth || height != Console.WindowHeight) // не доделал
                {
                    width = Console.WindowWidth;
                    height = Console.WindowHeight;
                    Console.Clear();
                    ShowMenuWord();
                }

                ShowMenu();

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        if (ChoisedButton == 0)
                        {
                            return 0;
                        }
                        else if (ChoisedButton == 1)
                        {
                            return 1;
                        }
                        else
                        {
                            return 2;
                        }
                    case ConsoleKey.W:
                        if (ChoisedButton > 0)
                        {
                            ChoisedButton--;
                        }
                        break;
                    case ConsoleKey.S:
                        if (ChoisedButton < 2)
                        {
                            ChoisedButton++;
                        }
                        break;
                    case ConsoleKey.Oem3:
                        VoidM.ActivateCheat();
                        break;
                }
            }
        }
    }
}