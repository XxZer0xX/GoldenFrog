using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringOperation
{
    using System.Globalization;

    class Program
    {
        private static IDictionary<char, char> _vowels;
 
        private static IDictionary<char, char> Vowels
        {
            get { return _vowels ?? (_vowels = ("AEIOU".ToDictionary(key => key, value => value))); }
        } 
        static void Main(string[] args)
        {
            // get string from console
            var strResult = Console.ReadLine();

            if (string.IsNullOrEmpty(strResult))
            {
                Console.WriteLine("You did't input a valid string, you have 3 more attemps remaining.");
                for (int i = 3; i > 0; i--)
                {
                    Console.WriteLine("Attempt {0}:",i);
                    strResult = Console.ReadLine();

                    if (!string.IsNullOrEmpty(strResult))
                        break;
                    if (i != 1) 
                        continue;

                    strResult =
                        "Exiting, Really? You can't type. At all? I mean seriously...\nTo prove this works ill process this message.\n";
                    Console.WriteLine(strResult);
                }
            }

            Console.WriteLine(processInput(strResult));
 
            Console.ReadKey();
        }

        private static string processInput(string strResult)
        {
            var strBuilder = new StringBuilder("\n",strResult.Length);

            // iterate chars
            for (var i = strResult.Length - 1; i > -1; i--)
            {
                var chr = strResult[i];

                // append char to string
                strBuilder.Append(

                    // check for char is digit
                    Char.IsDigit(chr)
                    ? chr

                    // check for vowel
                    : Vowels.ContainsKey(char.ToUpper(chr, CultureInfo.CurrentCulture))

                        // check upper
                        ? Char.IsUpper(chr)
                            ? chr
                            : Char.ToUpper(chr)

                        // check lower
                        : Char.IsLower(chr)
                            ? chr
                            : Char.ToLower(chr));
            }

            return strBuilder.ToString();
        }
    }
}
