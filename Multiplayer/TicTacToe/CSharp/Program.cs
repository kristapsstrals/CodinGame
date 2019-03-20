using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputs;

            var mySymbol = "x";
            var opponentSymbol = "o";

            var board = new Board();

            // game loop
            while (true)
            {
                inputs = Console.ReadLine().Split(' ');
                int opponentRow = int.Parse(inputs[0]);
                int opponentCol = int.Parse(inputs[1]);
                int validActionCount = int.Parse(Console.ReadLine());

                board.ClearValidFields();
                for (int i = 0; i < validActionCount; i++)
                {
                    inputs = Console.ReadLine().Split(' ');
                    int row = int.Parse(inputs[0]);
                    int col = int.Parse(inputs[1]);

                    board.SetValidFields(row, col);
                }

                int[] move;

                // If I move first, take the middle
                if (opponentRow == -1 && opponentCol == -1)
                {
                    move = board.GetNextMove(mySymbol);
                    Console.WriteLine($"{move[0]} {move[1]}");
                    continue;
                }

                board.SetCharacter(opponentRow, opponentCol, opponentSymbol);

                move = board.GetNextMove(mySymbol);
                Console.WriteLine($"{move[0]} {move[1]}");
            }
        }
    }

    class Board
    {
        new string[][] _map = new string[][] {
            new string[] {"", "", ""},
            new string[] {"", "", ""},
            new string[] {"", "", ""}
        };

        Dictionary<Position, string> map = new Dictionary<Position, string>();

        List<Position> validPositions = new List<Position>();

        public void SetCharacter(int row, int col, string character)
        {
            var pos = new Position(row, col);
            map[pos] = character;
        }

        public void SetValidFields(int row, int col)
        {
            validPositions.Add(new Position(row, col));
        }

        public void ClearValidFields()
        {
            validPositions = new List<Position>();
        }

        public int[] GetNextMove(string mySymbol)
        {
            // If middle is open, take the middle
            if (validPositions.Any(x => x.X == 1 && x.Y == 1))
            {
                SetCharacter(1, 1, mySymbol);
                return new int[] {1, 1};
            }

            var bestPosition = new Position(0,0);
            int interaction = 0;

            foreach(var p in validPositions)
            {
                //look around the position

                var list = new List<Position>
                {
                    new Position(p.X, p.Y - 1),
                    new Position(p.X - 1, p.Y - 1),
                    new Position(p.X + 1, p.Y - 1),
                    new Position(p.X - 1, p.Y),
                    new Position(p.X + 1, p.Y),
                    new Position(p.X, p.Y + 1),
                    new Position(p.X - 1, p.Y + 1),
                    new Position(p.X + 1, p.Y + 1),
                };

                var count = map.Keys.Intersect(list).Count();

                if (count > interaction)
                {
                    bestPosition = p;
                    interaction = count;
                }
            }

            var row = bestPosition.X;
            var col = bestPosition.Y;
            SetCharacter(row, col, mySymbol);

            // Look at each placement for a win. If found take it otherwise continue.
            
            // Look at each placement for an opponent's win. If found, take it for a block otherwise continue.
            // Look at the center square. If open take it otherwise continue.
            // Look at the 4 corners of the board. Randomly choose from any open ones. None open, continue.
            // Look at the 4 sides and randomly choose from any open ones. None open, Then the game is a tie.

            return new int[] {row, col};
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
}
