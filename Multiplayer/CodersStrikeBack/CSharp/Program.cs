using System;

namespace CodersStrikeBack
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputs;

            // game loop
            while (true)
            {
                inputs = Console.ReadLine().Split(' ');
                int x = int.Parse(inputs[0]);
                int y = int.Parse(inputs[1]);
                int nextCheckpointX = int.Parse(inputs[2]); // x position of the next check point
                int nextCheckpointY = int.Parse(inputs[3]); // y position of the next check point
                int nextCheckpointDist = int.Parse(inputs[4]); // distance to the next checkpoint
                int nextCheckpointAngle = int.Parse(inputs[5]); // angle between your pod orientation and the direction of the next checkpoint
                inputs = Console.ReadLine().Split(' ');
                int opponentX = int.Parse(inputs[0]);
                int opponentY = int.Parse(inputs[1]);

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");
                Console.Error.WriteLine($"Distance: {nextCheckpointDist}");
                Console.Error.WriteLine($"Angle: {nextCheckpointAngle}");
                
                int thrust = getThrust(nextCheckpointDist, nextCheckpointAngle);
                
                Console.Error.WriteLine($"Final Thrust: {thrust}");

                // You have to output the target position
                // followed by the power (0 <= thrust <= 100)
                // i.e.: "x y thrust"
                
                
                Console.WriteLine(nextCheckpointX + " " + nextCheckpointY + " " + thrust);
                
            }
        }
        
        static int getThrust(int distance, int angle)
        {
            if (angle > 90 || angle < -90)
                return 0;
                
            int angleThrust = 100;
            int distThrust = 100;
            
            //angle
            if (angle > 30 || angle < -30)
            {
                angleThrust = 80;
            }
            else
            {
                angleThrust = 100;
            }
            
            //distance    
            if (distance < 1000)
            {
                distThrust = 20;
            }
            else if (distance < 2000)
            {
                distThrust = 30;
            }
            else
            {
                distThrust = 100;
            }
            
            Console.Error.WriteLine($"Angle Thrust: {angleThrust}");
            Console.Error.WriteLine($"Dist Thrust: {distThrust}");
            
            float thrust = (angleThrust + distThrust) / 2;
            
            return (int)Math.Round(thrust);     
            
        }
    }
}
