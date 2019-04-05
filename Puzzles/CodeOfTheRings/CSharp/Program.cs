using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace CodeOfTheRings
{
    class Program
    {
        static void Main(string[] args)
        {
            // string magicPhrase = Console.ReadLine();
            string magicPhrase = "UMNE TALMAR RAHTAINE NIXENEN UMIR";

            magicPhrase = magicPhrase.ToUpperInvariant();

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            var result = new StringBuilder();
            var encoded = new List<string>();

            for (int i = 0; i < magicPhrase.Length; i++)
            {
                var position = 1 + i;
                var str = EncodeChar(magicPhrase[i]);
                encoded.Add(str);

                if (position <= 30)
                {
                    result.Append(str);
                    result.Append('.');
                    if (i != magicPhrase.Length - 1)
                        result.Append('>');
                    continue;
                }

                var test = position % 30;     
                var current = encoded[test];

                if (str == current)
                {
                    result.Append('.');
                }
                else if (current.Length < str.Length)
                {
                    var diff = str.Length - current.Length;
                    result.Append('+', diff);
                    result.Append('.');
                }
                else
                {
                    var diff = current.Length - str.Length;
                    result.Append('-', diff);
                    result.Append('.');
                }

                if (i != magicPhrase.Length - 1)
                        result.Append('>');
            }
            

            Console.WriteLine(result.ToString());
        }

        static string EncodeChar(char c)
        {
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var index = alphabet.IndexOf(c);
            //this can be done better by going back as well
            return new String('+', index + 1);
        }
    }
}
