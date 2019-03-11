using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Hypersonic
{
    

    /**
    * Auto-generated code below aims at helping you parse
    * the standard input according to the problem statement.
    **/
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            int width = int.Parse(inputs[0]);
            int height = int.Parse(inputs[1]);
            int myId = int.Parse(inputs[2]);
            
            //Setup
            var grid = new Grid();
            var player = new Player();
            var enemy = new Player();
            
            var boxes = new List<Box>();

            // game loop
            while (true)
            {
                for (int i = 0; i < height; i++)
                {
                    string row = Console.ReadLine();
                    for (int j = 0; j < row.Length; j++)
                    {
                        grid.AddToGrid(i, j, row[j]);
                    }
                }
                int entities = int.Parse(Console.ReadLine());
                for (int i = 0; i < entities; i++)
                {
                    inputs = Console.ReadLine().Split(' ');
                    int entityType = int.Parse(inputs[0]);
                    int owner = int.Parse(inputs[1]);
                    int x = int.Parse(inputs[2]);
                    int y = int.Parse(inputs[3]);
                    int param1 = int.Parse(inputs[4]);
                    int param2 = int.Parse(inputs[5]);

                    switch(entityType)
                    {
                        case EntityType.Player:
                            HandlePlayer();
                            break;
                        case EntityType.Bomb:
                            HandleBomb();
                            break;
                        default:
                            break;
                    }
                }

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");
                
                Console.Error.WriteLine($"");
                
                Console.WriteLine("BOMB 6 5");
            }
        }

        static void HandlePlayer() {}

        static void HandleBomb() {}
    }

    abstract class Entity
    {
        public Entity() {}
        
        public Entity(int x, int y)
        {
            Position = new Position(x, y);
        }
        
        public Position Position { get; set; }
    }

    class Player: Entity
    {    
        public Player() : base()
        {}
        
        public Player(int x, int y) : base(x, y)
        {}
        
    }

    class Box: Entity
    {
        public Box() : base()
        {}
        
        public Box(int x, int y) : base(x, y)
        {}
    }

    class Floor: Entity
    {
        public Floor() : base()
        {}
        
        public Floor(int x, int y) : base(x, y)
        {}
    }

    class Grid
    {
        
        readonly Dictionary<Position, Entity> grid;
        
        public Grid()
        {
            grid = new Dictionary<Position, Entity>();        
        }
        
        public void AddToGrid(int x, int y, char c)
        {        
            
            switch(c)
            {
                case '.':
                    var box = new Box(x, y);
                    grid.Add(box.Position, box);
                    break;
                case '0':
                    var floor = new Floor(x, y);
                    grid.Add(floor.Position, floor);
                    break;
            }
        }
    }

    class Position : IEquatable<Position> 
    {
        public int X;
        public int Y;
        
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override int GetHashCode() {
            return X + Y;
        }
        public override bool Equals(object obj) {
            return Equals(obj as Position);
        }
        public bool Equals(Position obj) {
            return obj != null 
                    && obj.X == this.X
                    && obj.Y == this.Y;
        }
    }

    static class EntityType
    {
        public const int Player = 0;
        public const int Bomb = 1;
    }
}
