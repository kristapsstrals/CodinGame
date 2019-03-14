using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;


namespace ChuckNorris
{
    class Program
    {
        static void Main(string[] args)
        {
            string MESSAGE = Console.ReadLine();
            // string MESSAGE = "CC";

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            // string result = "";
            // foreach(char c in MESSAGE)
            // {
            //     result += CharToChuck(c);
            // }
            string result = CharToChuck(MESSAGE);

            Console.WriteLine(result);
        }

        static string StringToBits(string s)
        {
            var sb = new StringBuilder();
            foreach(var c in s)
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(7, '0'));
            }

            return sb.ToString();
        }
        
        static string CharToChuck(string message)
        {
            StringBuilder sb = new StringBuilder();
            var arr = StringToBits(message);

            var previous = ' ';
            
            var charList = new List<List<char>>();

            foreach(var b in arr)
            {
                if (b != previous)
                {
                    charList.Add(
                        new List<char>
                        {
                            b
                        }
                    );

                    previous = b;
                    continue;
                }                 

                charList.Last().Add(b);
                previous = b;
            }

            for (int i = 0; i < charList.Count; i++)
            {
                if (charList[i][0] == '1')
                    sb.Append("0 ");
                else
                    sb.Append("00 ");

                sb.Append('0',charList[i].Count);
                
                if (i < charList.Count - 1)
                    sb.Append(" ");
            }

            return sb.ToString();
        }
    }
}
