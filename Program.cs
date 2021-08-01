using System;
using System.Linq;
using System.Text;
using GenPassword;

namespace ShifrXor
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write Text");
            string text = Console.ReadLine();
            Password genKey = new Password(@"#!@$%^&*_", text.Length, true, true,
                true, true, true, true);
            genKey.GenerateFinalPassword();
            StringBuilder key = genKey.GetPassword();
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Key");
            Console.ResetColor();
            
            Console.WriteLine(key);
            var textBinary = ConvertToBinary(text);
            var keyBinary = ConvertToBinary(key.ToString());
            Console.WriteLine("TextBinary");
            Console.WriteLine(textBinary);
            Console.WriteLine("KeyBinary");
            Console.WriteLine(keyBinary);
            var xor = Xor(text, key);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("ShifrText");
            Console.WriteLine(xor);
            Console.ResetColor();
            Console.WriteLine(ConvertToBinary(xor));
            Console.WriteLine("Decrypt");
            Console.WriteLine(Xor(xor,key));
        }
        static string ConvertToBinary(string text)
        {
            string res="";
            for (var i = 0; i < text.Length; i++)
            {
                res += Convert.ToString(text[i], 2).PadLeft(16,'0');
            }
            return res;
        }

        static string Xor(string text,StringBuilder key)
        {
            string res = "";
            for (var i = 0; i < text.Length; i++)
            {
                res += (char)(text[i] ^ key[i]);
            }

            return res;
        }
    }
}

