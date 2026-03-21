using CaveGame;

namespace Config
{
    class Settings
    {
        public int selectedButton = 0;
        public static int selectedSpeed = 3;
        public static int selectedQuantity = 1;
        public static bool selectedRender = false;
        public string AmountMonsters { get; } = "Количество монстров: ";
        public string MonstersSpeed { get; } = "Скорость монстров: ";
        public string Render { get; } = "Ограниченная видимость: ";
        public string Back { get; } = "Назад";
        public string symbol { get; } = "<";

        public int width = Console.WindowWidth;
        public int height = Console.WindowHeight;

        private Dictionary<int, string> speedList = new Dictionary<int, string>
        {
            {1, "высокая"},
            {2, "умеренная"},
            {3, "медленная"}
        };

        private Dictionary<bool, string> renderList = new Dictionary<bool, string>
        {
            {false, "да"},
            {true, "нет"}
        };

        public void ShowSettings()
        {
            int centerX = (Console.WindowWidth / 2) - (AmountMonsters.Length / 2);
            int centerY = (Console.WindowHeight / 2) - 1;

            if (selectedButton == 0)
            {
                Console.SetCursorPosition(centerX, centerY);
                Console.WriteLine(MonstersSpeed + speedList[selectedSpeed] + symbol + "  ");
                Console.SetCursorPosition(centerX, centerY + 1);
                Console.WriteLine(AmountMonsters + selectedQuantity + " ");
                Console.SetCursorPosition(centerX, centerY + 2);
                Console.WriteLine(Render + renderList[selectedRender] + " ");
                Console.SetCursorPosition(centerX, centerY + 3);
                Console.WriteLine(Back + " ");
            }
            else if (selectedButton == 1)
            {
                Console.SetCursorPosition(centerX, centerY);
                Console.WriteLine(MonstersSpeed + speedList[selectedSpeed] + "   ");
                Console.SetCursorPosition(centerX, centerY + 1);
                Console.WriteLine(AmountMonsters + selectedQuantity + symbol + " ");
                Console.SetCursorPosition(centerX, centerY + 2);
                Console.WriteLine(Render + renderList[selectedRender] + " ");
                Console.SetCursorPosition(centerX, centerY + 3);
                Console.WriteLine(Back + " ");
            }
            else if (selectedButton == 2)
            {
                Console.SetCursorPosition(centerX, centerY);
                Console.WriteLine(MonstersSpeed + speedList[selectedSpeed] + "   ");
                Console.SetCursorPosition(centerX, centerY + 1);
                Console.WriteLine(AmountMonsters + selectedQuantity + " ");
                Console.SetCursorPosition(centerX, centerY + 2);
                Console.WriteLine(Render + renderList[selectedRender] + symbol + " ");
                Console.SetCursorPosition(centerX, centerY + 3);
                Console.WriteLine(Back + " ");
            }
            else
            {
                Console.SetCursorPosition(centerX, centerY);
                Console.WriteLine(MonstersSpeed + speedList[selectedSpeed] + "   ");
                Console.SetCursorPosition(centerX, centerY + 1);
                Console.WriteLine(AmountMonsters + selectedQuantity + " ");
                Console.SetCursorPosition(centerX, centerY + 2);
                Console.WriteLine(Render + renderList[selectedRender] + " ");
                Console.SetCursorPosition(centerX, centerY + 3);
                Console.WriteLine(Back + symbol + " ");
            }
        }

        public int GetInputSettings()
        {
            while (true)
            {

                if (width != Console.WindowWidth || height != Console.WindowHeight) // не доделал
                {
                    width = Console.WindowWidth;
                    height = Console.WindowHeight;
                    Console.Clear();
                }

                ShowSettings();

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        if (selectedButton == 3)
                        {
                            return 3;
                        }
                        break;
                    case ConsoleKey.W:
                        if (selectedButton > 0)
                        {
                            selectedButton--;
                        }
                        break;
                    case ConsoleKey.S:
                        if (selectedButton < 3)
                        {
                            selectedButton++;
                        }
                        break;
                    case ConsoleKey.A:
                        if (selectedButton == 0)
                        {
                            if (selectedSpeed < 3)
                            {
                                selectedSpeed++;
                            }
                        }
                        else if (selectedButton == 1)
                        {
                            if (selectedQuantity > 0)
                            {
                                selectedQuantity--;
                            }
                        }
                        else if (selectedButton == 2)
                        {
                            selectedRender = !selectedRender;
                        }
                        break;
                    case ConsoleKey.D:
                        if (selectedButton == 0)
                        {
                            if (selectedSpeed > 1)
                            {
                                selectedSpeed--;
                            }
                        }
                        else if (selectedButton == 1)
                        {
                            if (selectedQuantity < 10)
                            {
                                selectedQuantity++;
                            }
                        }
                        else if (selectedButton == 2)
                        {
                            selectedRender = !selectedRender;
                        }
                        break;
                }
            }
        }
    }
}
