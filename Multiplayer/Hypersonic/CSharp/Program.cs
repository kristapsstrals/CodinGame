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
            Grid.CreateGrid();
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
                        Grid.SetAtPosition(i, j, row[j]);
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
                        case EntityTypes.Player:
                            if (owner == myId)
                            {
                                player.Move(x, y);
                            }
                            else
                            {
                                enemy.Move(x, y);
                            }
                            break;
                        case EntityTypes.Bomb:
                            Console.Error.WriteLine("Got Bomb entity");
                            var bomb = Grid.GetAt(x, y) as Bomb;
                            if (bomb == null)
                            {
                                Grid.SetAtPosition(x, y,'b');
                                break;
                            }
                            bomb.CountDown();
                            break;
                        default:
                            break;
                    }
                }

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");
                
                Console.WriteLine("BOMB 6 5");
            }
        }
    }

    abstract class Entity
    {
        public Entity(int entityType)
        {
            EntityType = entityType;
        }
        
        public Entity(int x, int y, int entityType) : this(entityType)
        {
            Position = new Position(x, y);
        }
        
        public Position Position { get; set; }

        public int EntityType { get; }
    }

    class Player: Entity
    {    
        public Player() : base(EntityTypes.Player)
        {}
        
        public Player(int x, int y) : base(x, y, EntityTypes.Player)
        {}

        public List<Position> AvailableMovement()
        {
            var positions = new List<Position>();

            var entityLeft = Grid.GetAt(Position.X - 1, Position.Y);
            if (entityLeft != null && entityLeft.EntityType == EntityTypes.Floor)
                positions.Add(entityLeft.Position);

            var entityRight = Grid.GetAt(Position.X + 1, Position.Y);
            if (entityRight != null && entityRight.EntityType == EntityTypes.Floor)
                positions.Add(entityRight.Position);

            var entityDown = Grid.GetAt(Position.X, Position.Y - 1);
            if (entityDown != null && entityDown.EntityType == EntityTypes.Floor)
                positions.Add(entityDown.Position);

            var entityUp = Grid.GetAt(Position.X, Position.Y + 1);
            if (entityUp != null && entityUp.EntityType == EntityTypes.Floor)
                positions.Add(entityUp.Position);

            return positions;
        }

        public void Move(int x, int y)
        {
            if (Position == null)
            {
                Position = new Position(x, y);
                Console.Error.WriteLine($"Player Initial movement to position [{x}, {y}]");
                return;    
            }

            if (!CanMove(x, y))
            {
                Console.Error.WriteLine($"Cannot move to new position [{x}, {y}]");
                return;
            }

            Position = new Position(x, y);
            Console.Error.WriteLine($"Player moved to position [{x}, {y}]");
        }

        bool CanMove(int x, int y)
        {
            return AvailableMovement().Contains(new Position(x, y));
        }
        
    }

    class Bomb: Entity
    {
        int timeRemaining = 8;
        int explosionSize = 3;

        List<Position> impactArea;

        public Bomb() : base(EntityTypes.Bomb)
        {
            impactArea = GetImpactArea();
        }
        
        public Bomb(int x, int y) : base(x, y, EntityTypes.Bomb)
        {
            impactArea = GetImpactArea();
        }

        public void CountDown()
        {

            if (timeRemaining == 0)
            {
                Explode();
                return;
            }

            timeRemaining--;
            Console.Error.WriteLine($"Bomb counting down, time remaining {timeRemaining}");
        }

        int Explode(bool destroy = true)
        {
            /*
                Get All entities in bombs range and check what happens
                      -
                      -
                      -
                - - - * - - -
                      -
                      -
                      -
            */
            var destroyedBoxes = new List<Box>();

            foreach(var position in impactArea)
            {
                var entity = Grid.GetAt(position);
                if(entity == null)
                    continue;

                //Cannot use type pattern matching in CodinGame
                switch(entity.EntityType)
                {
                    case EntityTypes.Floor:
                        continue;
                    case EntityTypes.Player:
                        //for now player takes no damage from bombs (Wood 3)
                        continue;
                    case EntityTypes.Box:
                        //Change box to floor
                        var box = entity as Box;
                        if (box == null)
                        {
                            Console.Error.WriteLine($"Cannot destroy box - null");
                            continue;
                        }

                        if (destroy)
                            box.Destroy();

                        destroyedBoxes.Add(box);
                        continue;
                }
            }

            Console.Error.WriteLine($"Bomb exploaded! Destroyed {destroyedBoxes.Count} boxes.");

            return destroyedBoxes.Count;
        }

        List<Position> GetImpactArea()
        {
            var positions = new List<Position>();
            positions.Add(new Position(Position.X, Position.Y));

            for (int i = 1; i <= explosionSize; i++)
            {
                positions.Add(new Position(Position.X - i, Position.Y));
                positions.Add(new Position(Position.X + i, Position.Y));
                positions.Add(new Position(Position.X, Position.Y - i));
                positions.Add(new Position(Position.X, Position.Y + i));
            }

            return positions;
        }

    }

    class Box: Entity
    {
        public Box() : base(EntityTypes.Box)
        {}
        
        public Box(int x, int y) : base(x, y, EntityTypes.Box)
        {}

        public void Destroy()
        {
            //Change the box for floor on Grid
            Grid.SetAtPosition(Position, '.');
        }
    }

    class Floor: Entity
    {
        public Floor() : base(EntityTypes.Floor)
        {}
        
        public Floor(int x, int y) : base(x, y, EntityTypes.Floor)
        {}
    }

    static class Grid
    {
        
        static Dictionary<Position, Entity> grid;
        
        public static void CreateGrid()
        {
            grid = new Dictionary<Position, Entity>();        
        }
        
        public static void SetAtPosition(int x, int y, char c)
        {        
            // Console.Error.WriteLine($"Setting grid: [{x}, {y}, {c}]");

            switch(c)
            {
                case '0':
                    var box = new Box(x, y);
                    grid[box.Position] = box;
                    break;
                case '.':
                    var floor = new Floor(x, y);
                    
                    //This could be overhead...
                    var b = GetAt(floor.Position);
                    if (b != null && (b as Bomb) != null)
                        break;

                    grid[floor.Position] = floor;
                    break;
                case 'b':
                    var bomb = new Bomb(x, y);
                    grid[bomb.Position] = bomb;
                    break;
            }
        }

        public static void SetAtPosition(Position position, char c)
        {
            SetAtPosition(position.X, position.Y, c);
        }

        public static Entity GetAt(Position position)
        {
            if (grid.ContainsKey(position))
                return grid[position];

            return null;
        }

        public static Entity GetAt(int x, int y)
        {
            return GetAt(new Position(x, y));
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

    static class EntityTypes
    {
        public const int Player = 0;
        public const int Bomb = 1;
        public const int Box = 2;
        public const int Floor = 3;
    }
}
