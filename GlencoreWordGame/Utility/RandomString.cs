using System;
using System.Linq;

namespace GlencoreWordGame.Utility
{
    //src https://www.codegrepper.com/code-examples/csharp/generate+random+string+c%23+with+fix+length

    public static class RandomString
    {
        private static Random random = new Random();
        public static string GetRandomString(int length)
        {
            //Using each letter once was producing strings that had too few vowels to really be viable
            //This method seems a bit of a cheat but will do for now.
            const string chars = "AAAAAABBCCDDDEEEEEEEFGGGHHIIIIIIJKLLMNOOOOOOOOPQRSTTTTTUUUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
