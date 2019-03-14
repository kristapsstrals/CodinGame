using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;


namespace ShadowOfTheKnight
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            int width = int.Parse(inputs[0]); // width of the building.
            int height = int.Parse(inputs[1]); // height of the building.
            int jumpsRemaining = int.Parse(Console.ReadLine()); // maximum number of turns before game over.
            inputs = Console.ReadLine().Split(' ');
            int batmanX = int.Parse(inputs[0]);
            int batmanY = int.Parse(inputs[1]);
            
            var batman = new Batman(batmanX, batmanY);
            var building = new Building(width, height, batman);

            // game loop
            while (true)
            {
                string bombDir = Console.ReadLine(); // the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L or UL)

                building.ReducePlayArea(bombDir);
                building.MoveBatman();


                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");
                

                // the location of the next window Batman should jump to.
                Console.WriteLine($"{building.Batman.X} {building.Batman.Y}");
            }
        }
    }

    class Building
    {     

        int height0;
        int height1;
        int width0;
        int width1;

        Dictionary<int, List<int>> playArea;
        public Batman Batman {get; private set;}
        
        public Building(int width, int height, Batman batman)
        {
            Batman = batman;

            height0 = 0;
            height1 = height;
            width0 = 0;
            width1 = width;
            SetupPlayArea();
        }

        public void ReducePlayArea(string direction)
        {

            Console.Error.WriteLine($"Moving to: {direction}");

            switch(direction)
            {
                case Directions.Up:
                    //height0 is the same
                    height1 = Batman.Y - 1;
                    width0 = Batman.X;
                    width1 = Batman.X + 1;
                    SetupPlayArea();
                    break;
                case Directions.UpRight:
                    //height0 is the same
                    height1 = Batman.Y - 1;
                    width0 = Batman.X + 1;
                    //width1 is the same
                    SetupPlayArea();
                    break;
                case Directions.Right:
                    height0 = Batman.Y;
                    height1 = Batman.Y + 1;
                    width0 = Batman.X + 1;
                    //width1 is the same
                    SetupPlayArea();
                    break;
                case Directions.DownRight:
                    height0 = Batman.Y + 1;
                    //height1 is the same
                    width0 = Batman.X + 1;
                    //width1 is the same
                    SetupPlayArea();
                    break;
                case Directions.Down:
                    height0 = Batman.Y + 1;
                    //height1 is the same
                    width0 = Batman.X;
                    width1 = Batman.X + 1;
                    SetupPlayArea();
                    break;
                case Directions.DownLeft:
                    height0 = Batman.Y + 1;
                    //height1 is the same
                    //width0 is the same
                    width1 = Batman.X - 1;
                    SetupPlayArea();
                    break;
                case Directions.Left:
                    height0 = Batman.Y;
                    height1 = Batman.Y + 1;
                    //width0 is the same
                    width1 = Batman.X - 1;
                    break;
                case Directions.UpLeft :
                    //height0 is the same
                    height1 = Batman.Y - 1;
                    //width0 is the same
                    width1 = Batman.X - 1;
                    SetupPlayArea();
                    break;
            }
        }

        public void MoveBatman()
        {
            var heightArr = playArea.Keys.ToArray();
            var newY = heightArr[heightArr.Length / 2];

            var widthArr = playArea[newY].ToArray();
            var newX = widthArr[widthArr.Length / 2];

            Console.Error.WriteLine($"New Batman coords: [{newX}, {newY}]");

            Batman.X = newX;
            Batman.Y = newY;
        }

        void SetupPlayArea()
        {
            Console.Error.WriteLine($"Area restricitons:");
            Console.Error.WriteLine($"Height0: {height0}");
            Console.Error.WriteLine($"Height1: {height1}");
            Console.Error.WriteLine($"Width0: {width0}");
            Console.Error.WriteLine($"Width1: {width1}");

            playArea = new Dictionary<int, List<int>>();

            for (int i = height0; i < height1; i++)
            {
                for (int j = width0; j < width1; j++)
                {
                    if (playArea.ContainsKey(i))
                        playArea[i].Add(j);
                    else
                        playArea[i] = new List<int>{j};
                }
            }
        }
    }

    static class Directions
    {
        public const string Up = "U";
        public const string UpRight = "UR";
        public const string Right = "R";
        public const string DownRight = "DR";
        public const string Down = "D";
        public const string DownLeft = "DL";
        public const string Left = "L";
        public const string UpLeft = "UL";
    }

    class Batman
    {
        public Batman(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
