using System;

namespace GreatEscape
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            int w = int.Parse(inputs[0]); // width of the board
            int h = int.Parse(inputs[1]); // height of the board
            int playerCount = int.Parse(inputs[2]); // number of players (2 or 3)
            int myId = int.Parse(inputs[3]); // id of my player (0 = 1st player, 1 = 2nd player, ...)

            // game loop
            while (true)
            {
                for (int i = 0; i < playerCount; i++)
                {
                    inputs = Console.ReadLine().Split(' ');
                    int x = int.Parse(inputs[0]); // x-coordinate of the player
                    int y = int.Parse(inputs[1]); // y-coordinate of the player
                    int wallsLeft = int.Parse(inputs[2]); // number of walls available for the player

                    
                }
                int wallCount = int.Parse(Console.ReadLine()); // number of walls on the board
                for (int i = 0; i < wallCount; i++)
                {
                    inputs = Console.ReadLine().Split(' ');
                    int wallX = int.Parse(inputs[0]); // x-coordinate of the wall
                    int wallY = int.Parse(inputs[1]); // y-coordinate of the wall
                    string wallOrientation = inputs[2]; // wall orientation ('H' or 'V')
                }

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");


                // action: LEFT, RIGHT, UP, DOWN or "putX putY putOrientation" to place a wall
                if (myId == 0)
                {
                    Console.WriteLine("RIGHT");
                }
                else
                {
                    Console.WriteLine("LEFT");
                }
            }
        }
    }
}
