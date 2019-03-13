using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;


namespace MarsLander
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputs;
            int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.

            int groundLevel = 5000;
            for (int i = 0; i < surfaceN; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
                int landY = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
                
                if (landY < groundLevel)
                    groundLevel = landY;
            }


            double gravity = 3.711; //thrust 4 counteracts gravity
            int height = 0;

            /*
                Free fall then increased thrust
                //free fall h=1/2gt^2 => t = sqrt(2h/g);


                Energy conservation
                Kinetic K = (1/2) * m * v^2

            */
            var timeToFall = Math.Sqrt((2*height)/gravity);

            // game loop
            while (true)
            {
                inputs = Console.ReadLine().Split(' ');
                int X = int.Parse(inputs[0]);
                int Y = int.Parse(inputs[1]);
                int hSpeed = int.Parse(inputs[2]); // the horizontal speed (in m/s), can be negative.
                int vSpeed = int.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.
                int fuel = int.Parse(inputs[4]); // the quantity of remaining fuel in liters.
                int rotate = int.Parse(inputs[5]); // the rotation angle in degrees (-90 to 90).
                int power = int.Parse(inputs[6]); // the thrust power (0 to 4).

                height = Y - groundLevel;

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                Console.Error.WriteLine($"rmaining height: {height}");

                if (Math.Abs(vSpeed) < 40)
                    Console.WriteLine("0 0");
                else
                    Console.WriteLine("0 4");

                // //Gradual descent
                // if (height < 500)
                //     Console.WriteLine("0 4");
                // else if (height < 1000)
                //     Console.WriteLine("0 3");
                // else if (height < 1200)
                //     Console.WriteLine("0 2");
                // else if (height < 1500)
                //     Console.WriteLine("0 1");
                // else
                //     Console.WriteLine("0 0");
            }
        }
    }
}
