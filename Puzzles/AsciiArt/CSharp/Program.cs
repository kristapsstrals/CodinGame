using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;


namespace AsciiArt
{
    class Program
    {
        static void Main(string[] args)
        {
            int L = int.Parse(Console.ReadLine());
            // int L = 4;
            int H = int.Parse(Console.ReadLine());
            // int H = 5;
            string inputText = Console.ReadLine();
            // string inputText = "@";

            inputText = inputText.ToUpperInvariant();

            // string[] input = new []
            // {
            //     " #  ##   ## ##  ### ###  ## # # ###  ## # # #   # # ###  #  ##   #  ##   ## ### # # # # # # # # # # ### ### ",
            //     "# # # # #   # # #   #   #   # #  #    # # # #   ### # # # # # # # # # # #    #  # # # # # # # # # #   #   # ",
            //     "### ##  #   # # ##  ##  # # ###  #    # ##  #   ### # # # # ##  # # ##   #   #  # # # # ###  #   #   #   ## ",
            //     "# # # # #   # # #   #   # # # #  #  # # # # #   # # # # # # #    ## # #   #  #  # # # # ### # #  #  #       ",
            //     "# # ##   ## ##  ### #    ## # # ###  #  # # ### # # # #  #  #     # # # ##   #  ###  #  # # # #  #  ###  #  ",
            // };
            
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ?";
            
            var asciiCharacters = new Dictionary<char, Character>();
            
            foreach(var c in alphabet)
            {
                asciiCharacters.Add(c, new Character());           
            }

            //Setup empty character for spaces
            var empty = new Character();

            for (int i = 0; i < H; i++)
            {
                empty.AddRow(" ");

                string ROW = Console.ReadLine();
                // string ROW = input[i];
                
                var test1 = Split(ROW, L).ToList();
                for (int j = 0; j < test1.Count; j++)
                {
                    var cha = asciiCharacters[alphabet[j]];
                    cha.AddRow(test1[j]);
                }
            }

            asciiCharacters[' '] = empty;
            
            var resultLines = new Dictionary<int, string>();
            
            foreach(char r in inputText)
            {
                var c = r;
                if (!alphabet.Contains(c))
                    c = '?';

                var index = 0;
                foreach(var resString in asciiCharacters[c].Value)
                {
                    resultLines[index] = resultLines.ContainsKey(index) ? resultLines[index] + resString : resString;
                    index++;
                }
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            var result = "";
            foreach(var r in resultLines)
            {
                if (string.IsNullOrEmpty(result))
                    result += r.Value;
                else
                {
                    result += Environment.NewLine + r.Value;
                }
            }

            Console.WriteLine(result);
        }
        
        static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }
    }

    class Character 
    {
        public List<string> Value { get; set; } = new List<string>();
        
        public void AddRow(string val)
        {
            Value.Add(val);
        }
    }
}
