using Game;
using Config;
using System;
using System.Data;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;

namespace CaveGame
{
    class Program
    {
        public static void Main()
        {
            Console.Title = "CaveGame";
            Console.CursorVisible = false;


            while (true)
            {
                Menu menu = new Menu();
                Settings set = new Settings();
                GameMap map = new GameMap();
                Person person = new Person('@', 1, 1);
                Light light = new Light(map, '*');
                ExitSymbol exit = new ExitSymbol(map);
                GetInput input = new GetInput();
                GUI gui = new GUI();
                Render render = new Render();

                int menuChoise = menu.GetInputMenu();

                List<Entity> list = new List<Entity>();
                list.Add(person);
                list.Add(light);
                list.Add(exit);

                for (int i = 1; i <= Settings.selectedQuantity; i++)
                {
                    list.Add(new Monster(map));
                }

                if (menuChoise == 0)
                {
                    Console.Clear();

                    while (exit.EndOfGame())
                    {
                        gui.FPSCounter();
                        Console.SetCursorPosition(0, 0);
                        foreach (var ent in list)
                        {
                            if (ent is Monster m)
                            {
                                m.UpdatePosition(person, map);
                            }
                        }
                        person.CheckCollisions(list, render);
                        render.Draw(map, person, list);
                        gui.ShowFPS();
                        input.GetInputMenu(person, map, render);
                    }


                    Console.Clear();

                    Thread.Sleep(2000);

                }
                else if (menuChoise == 1)
                {
                    Console.Clear();
                    if (set.GetInputSettings() == 3)
                    {
                        Console.Clear();
                        continue;
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }

    class GameMap
    {
        private string[] map { get; } = new string[]
        {
    "########################################################################################################################",
    "#        #######                  #                              #             #                  #                    #",
    "#        #     #        #####     #        ######                                                 #                    #",
    "#        #     #     ####         #   #####                                                       #                    #",
    "#        #     #                  #                              #             #                  #                    #",
    "#              #######                                           #             #                  #                    #",
    "#                                 #   #             ########     #             #                                       #",
    "#                                 #   #             #            #     #       #     #########  ################   #####",
    "#                         ####    #   ####          #            #     #       #######                                 #",
    "#        #                #       #           #######            #      #      #          #                  #         #",
    "#        ###########      #       #   ####          #            #      #      #          #                  #         #",
    "#####              #      #       #   #             #            #      #      #          #                  #         #",
    "#           #      #              ###               #            #             #     ######                  #         #",
    "###         #      #                        #                                           #                    #         #",
    "#        #####     #              ###        #                   #             #        #                              #",
    "#        #         #      #       #           #         #        #             #                                       #",
    "#        #                #       #            #        ##########             #          #########                    #",
    "#                  #      ####    #                     #        #             #          #                            #",
    "#         #        #      #       #      #              #        #             #          #                            #",
    "#         #        #              #      #                       #             #          #                            #",
    "#    ######        #                     #        #######        #             #          #            ##########  #####",
    "#         #        #              #      #     ####   #                        #          #            #               #",
    "#         #        ########  ######                   #          #    ####     #                       #               #",
    "#         #        #              #                   #          #  ###        #          #            #               #",
    "#         #        #              #     ###           #          #             #          #            #               #",
    "#   #       #      #              #       #         ######       #             #          #                            #",
    "#   #       #      #                      #                      #             #          #            #               #",
    "#   #       #      #              #       #                      #             #          #            #               #",
    "########################################################################################################################"
        };

        public int mapWidth => map[0].Length;
        public int mapHeight => map.Length;

        public char GetCharOfMap(int y, int x)
        {
            return map[y][x];
        }
    }

    class Render
    {
        public bool visionFlag { get; private set; } = Settings.selectedRender;
        public bool fieldOnScreen = false;
        private DateTime lightEnd = DateTime.MinValue;

        //чит-костыль(мб убрать потом)
        public void Cheat()
        {

            Console.Clear();
            visionFlag = !visionFlag;


            if (visionFlag)
            {
                lightEnd = DateTime.Now.AddYears(1);
            }
            else
            {
                lightEnd = DateTime.MinValue;
            }
        }

        public void ActivateVision()
        {
            lightEnd = DateTime.Now.AddSeconds(5);
            visionFlag = true;
        }

        public void Draw(GameMap map, Person person, List<Entity> list)
        {
            if (Settings.selectedRender == false)
            {
                if (visionFlag && DateTime.Now > lightEnd)
                {
                    visionFlag = false;
                    Console.Clear();
                }
            }

            if (visionFlag)
            {
                if (fieldOnScreen == false)
                {
                    for (int i = 0; i < map.mapHeight; i++)
                    {
                        for (int j = 0; j < map.mapWidth; j++)
                        {
                            Console.Write(map.GetCharOfMap(i, j));
                        }

                        if (i < map.mapHeight - 1)
                        {
                            Console.WriteLine();
                        }
                    }
                }

                fieldOnScreen = true;

                foreach (var ent in list)
                {
                    if (ent is Person p)
                    {
                        Console.SetCursorPosition(person.entityX, person.entityY);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(person.entity);
                        Console.ResetColor();

                        if (person.entityX != person.lastEntityX || person.entityY != person.lastEntityY)
                        {
                            Console.SetCursorPosition(person.lastEntityX, person.lastEntityY);
                            Console.WriteLine(map.GetCharOfMap(person.lastEntityY, person.lastEntityX));
                        }    
                    }
                    if (ent is Light light)
                    {
                        if (light.entityX != -1 && light.entityY != -1)
                        {
                            Console.SetCursorPosition(light.entityX, light.entityY);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(light.entity);
                            Console.ResetColor();
                        }
                    }
                    if (ent is Monster monster)
                    {
                        Console.SetCursorPosition(monster.entityX, monster.entityY);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(monster.entity);
                        Console.ResetColor();

                        if (monster.entityX != monster.lastEntityX || monster.entityY != monster.lastEntityY)
                        {
                            Console.SetCursorPosition(monster.lastEntityX, monster.lastEntityY);
                            Console.WriteLine(map.GetCharOfMap(monster.lastEntityY, monster.lastEntityX));
                        }
                    }
                    if (ent is ExitSymbol exit)
                    {
                        Console.SetCursorPosition(exit.entityX, exit.entityY);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(exit.entity);
                        Console.ResetColor();
                    }
                }
            }
            else
            {
                fieldOnScreen = false;
                List<int> cords = new List<int>();

                foreach (var ent in list)
                {
                    if (Math.Abs(ent.entityX - person.entityX) < 4 && Math.Abs(ent.entityY - person.entityY) < 4)
                    {
                        cords.Add(ent.entityX);
                        cords.Add(ent.entityY);

                        Console.SetCursorPosition(ent.entityX, ent.entityY);

                        if (ent is Person)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(ent.entity);
                            Console.ResetColor();;
                        }
                        if (ent is Light light)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(light.entity);
                            Console.ResetColor();
                        }
                        if (ent is Monster monster)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(monster.entity);
                            Console.ResetColor();
                        }
                        if (ent is ExitSymbol exit)
                        {
                            Console.SetCursorPosition(exit.entityX, exit.entityY);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(exit.entity);
                            Console.ResetColor();
                        }
                    }
                }

                for (int y = -4; y <= 4; y++)
                {
                    for (int x = -4; x <= 4; x++)
                    {
                        if (person.entityY + y >= 0 && person.entityY + y < map.mapHeight && person.entityX + x >= 0 && person.entityX + x < map.mapWidth)
                        {

                            bool isEntityHere = false;

                            for (int i = 0; i < cords.Count - 1; i += 2)
                            {
                                if (cords[i] == person.entityX + x && cords[i + 1] == person.entityY + y)
                                {
                                    isEntityHere = true;
                                    break;
                                }
                            }

                            if (isEntityHere)
                            {
                                continue;
                            }

                            Console.SetCursorPosition(person.entityX + x, person.entityY + y);

                            if (y == -4 || y == 4 || x == -4 || x == 4)
                            {
                                Console.Write(" ");
                            }
                            else
                            {
                                Console.Write(map.GetCharOfMap(person.entityY + y, person.entityX + x));
                            }
                        }
                    }
                }
            }
        }
    }

    class Entity
    {
        public char entity { get; protected set; }
        public int entityX { get; protected set; }
        public int entityY { get; protected set; }
        public int lastEntityX { get; protected set; }
        public int lastEntityY { get; protected set; }

        protected DateTime lastMoveTime = DateTime.MinValue;
        private Queue<(int, int)> visitedCells = new Queue<(int, int)>();
        private int visionRadius = 20;
        private int memorySize = 50;

        private static Random rnd = new Random();

        public void PersonLastPosition()
        {
            lastEntityX = entityX;
            lastEntityY = entityY;
        }

        public virtual void UpdatePosition(Person player, GameMap map)
        {
            if ((DateTime.Now - lastMoveTime).TotalSeconds < (double)Settings.selectedSpeed / 10.0)
            {
                return;
            }

            lastMoveTime = DateTime.Now;

            int distanceToPlayer = Math.Abs(entityX - player.entityX) + Math.Abs(entityY - player.entityY);

            visitedCells.Enqueue((entityX, entityY));

            if (visitedCells.Count > memorySize)
            {
                visitedCells.Dequeue();
            }

            PersonLastPosition();

            var directions = new List<(int x, int y)>();

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                    {
                        continue;
                    }

                    int nx = entityX + dx;
                    int ny = entityY + dy;

                    if (nx >= 0 && nx < map.mapWidth && ny >= 0 && ny < map.mapHeight && map.GetCharOfMap(ny, nx) != '#')
                    {
                        directions.Add((nx, ny));
                    }
                }
            }

            (int x, int y) nextStep;

            if (distanceToPlayer <= visionRadius)
            {
                var bestMoves = directions.OrderBy(m =>
                    Math.Abs(m.x - player.entityX) + Math.Abs(m.y - player.entityY)
                ).ToList();

                nextStep = bestMoves.FirstOrDefault(m => !visitedCells.Contains(m));

                if (nextStep == default)
                {
                    nextStep = bestMoves[0];
                }
            }
            else
            {
                var freshMoves = directions.Where(m => !visitedCells.Contains(m)).ToList();

                if (freshMoves.Count > 0)
                {
                    nextStep = freshMoves[rnd.Next(freshMoves.Count)];
                }
                else
                {
                    nextStep = directions[rnd.Next(directions.Count)];
                }
            }

            entityX = nextStep.x;
            entityY = nextStep.y;
        }

        public void CheckCollisions(List<Entity> list, Render render)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var ent = list[i];

                if (ent != this && ent.entityX == this.entityX && ent.entityY == this.entityY)
                {
                    if (ent is Light light)
                    {
                        render.ActivateVision();
                        list.RemoveAt(i);
                    }
                    if (ent is Monster monster)
                    {
                        Environment.Exit(0);
                    }
                    if (ent is ExitSymbol exit)
                    {
                        list.RemoveAt(i);
                        ent.entityY = -1;
                    }
                }
            }
        }
    }

    class Person : Entity
    {
        public Person(char entity, int x, int y)
        {
            this.entity = entity;
            entityX = x;
            entityY = y;
        }

        public void TryMovePerson(int newY, int newX, GameMap map)
        {
            if (newX >= 0 && newX <= map.mapWidth - 2 && newY >= 0 && newY <= map.mapHeight - 2)
            {
                if (map.GetCharOfMap(newY, newX) != '#')
                {
                    entityX = newX;
                    entityY = newY;
                }
            }
        }
    }

    class Light : Entity
    {
        private static Random rnd = new Random();

        public Light(GameMap map, char entity)
        {
            if (Settings.selectedRender == false)
            {
                while (true)
                {
                    entityX = rnd.Next(1, map.mapWidth - 2);
                    entityY = rnd.Next(1, map.mapHeight - 2);

                    if (map.GetCharOfMap(entityY, entityX) != '#')
                    {
                        this.entity = entity;
                        break;
                    }
                }
            }
        }
    }

    class Monster : Entity
    {
        private static Random rnd = new Random();

        public Monster(GameMap map)
        {
            entity = '&';

             while (map.GetCharOfMap(entityY, entityX) == '#')
             {
                entityX = rnd.Next(50, map.mapWidth - 2);
                entityY = rnd.Next(1, map.mapHeight - 2);
             }
        }

        public new void UpdatePosition(Person player, GameMap map)
        {
            PersonLastPosition();
            base.UpdatePosition(player, map);
        }
    }

    class ExitSymbol : Entity
    {
        private static Random rnd = new Random();
        private DateTime time = DateTime.Now;

        public ExitSymbol(GameMap map)
        {
            while (true)
            {
                entityX = map.mapWidth - 2;
                entityY = rnd.Next(1, map.mapHeight - 2);

                if (map.GetCharOfMap(entityY, entityX) != '#')
                {
                    entity = 'X';
                    break;
                }
            }
        }

        public bool EndOfGame()
        {
            if (entityY == -1)
            {
                return false;
            }

            return true;
        }
    }

    class GetInput
    {
        private DateTime lastMoveTime = DateTime.Now;

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int key);

        private bool IsKeyDown(int vKey)
        {
            return (GetAsyncKeyState(vKey) & 0x8000) != 0;
        }
        public void GetInputMenu(Person person, GameMap map, Render render)
        {

            if ((DateTime.Now - lastMoveTime).TotalMilliseconds < 100)
            {
                return;
            }

            person.PersonLastPosition();

            bool moved = false;

            if (IsKeyDown(0x57))
            {
                person.TryMovePerson(person.entityY - 1, person.entityX, map);
                moved = true;
            }
            if (IsKeyDown(0x53))
            {
                person.TryMovePerson(person.entityY + 1, person.entityX, map);
                moved = true;
            }
            if (IsKeyDown(0x41))
            {
                person.TryMovePerson(person.entityY, person.entityX - 1, map);
                moved = true;
            }
            if (IsKeyDown(0x44))
            {
                person.TryMovePerson(person.entityY, person.entityX + 1, map);
                moved = true;
            }

            if (IsKeyDown(0x20))
            {
                render.Cheat();
                Thread.Sleep(200);
            }

            if (moved)
            {
                lastMoveTime = DateTime.Now;
            }

            while (Console.KeyAvailable) Console.ReadKey(true);
        }
    }

    class GUI
    {
        private int frames;
        private int FPS;
        private DateTime lastTime = DateTime.Now;

        public void FPSCounter()
        {
            frames++;
        }

        public void ShowFPS()
        {

            if ((DateTime.Now - lastTime).TotalSeconds >= 1.0)
            {
                FPS = frames;
                frames = 0;
                lastTime = DateTime.Now;

                Console.SetCursorPosition(0, 29);
                Console.Write($"FPS: {FPS} ");
            }
        }
    }
}