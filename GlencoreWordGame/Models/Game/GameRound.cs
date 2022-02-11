using GlencoreWordGame.Models.User;
using System.Collections.Generic;

namespace GlencoreWordGame.Models.Game
{
    public class GameRound
    {
        public GameRound(string randomLetters)
        {
            this.GameUser = new GameUser();
            this.AlreadyInputtedWords = new List<string>();
            this.AvailableLetters = randomLetters;
            this.MaximumWordLength = randomLetters.Length;
        }

        public GameUser GameUser { get; set; }

        public string AvailableLetters { get; set; }

        public List<string> AlreadyInputtedWords { get; set; }

        public int MaximumWordLength { get; set; }
    }
}
