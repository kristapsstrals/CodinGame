using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;


namespace Temperatures
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine()); // the number of temperatures to analyse
            string[] inputs = Console.ReadLine().Split(' ');

            int min = 6000;

            for (int i = 0; i < n; i++)
            {
                int t = int.Parse(inputs[i]); // a temperature expressed as an integer ranging from -273 to 5526

                Console.Error.WriteLine($"Current inputs: [{t}, {min}]");

                if (Math.Abs(t) <= Math.Abs(min))
                {
                    Console.Error.WriteLine($"[{t} < {min}]");
                    //If two numbers are equally close to zero, positive integer has to be considered closest to zero
                    if (Math.Abs(t) == Math.Abs(min))
                    {
                        min = t > 0 ? t : min;
                    }
                    else
                    {
                        min = t;
                    }
                }
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            
            if (min == 6000)
            {
                Console.Error.WriteLine($"No inputs were provided");
                Console.WriteLine(0);    
            }
            else
            {
                Console.WriteLine(min);
            }
        }
    }
}
