using System.Collections.Generic;
using System.Linq;

namespace GlencoreWordGame.Scoring
{
    public interface IWordGameScorer
    {
        public int GetWordScore(string wordToScore);
    }
    
    public class WordGameScorer : IWordGameScorer
    {
        public int GetWordScore(string wordToScore)
        {
            int score = 0;
            
            foreach(char c in wordToScore)
            {
                score += this.GetLetterScore(c);
            }

            return score;
        }

        //I considered doing something fancy with the number of synonyms or definitions the word has
        // but just went with the scrabble character scores. 
        private int GetLetterScore(char c)
        {
            Dictionary<char, int> LetterScores = new Dictionary<char, int>
            {
                { 'A', 1 },
                { 'B', 3 },
                { 'C', 3 },
                { 'D', 2 },
                { 'E', 1 },
                { 'F', 4 },
                { 'G', 2 },
                { 'H', 4 },
                { 'I', 1 },
                { 'J', 8 },
                { 'K', 5 },
                { 'L', 1 },
                { 'M', 3 },
                { 'N', 1 },
                { 'O', 1 },
                { 'P', 3 },
                { 'Q', 10},
                { 'R', 1 },
                { 'S', 1 },
                { 'T', 1 },
                { 'U', 1 },
                { 'V', 4 },
                { 'W', 4 },
                { 'X', 8 },
                { 'Y', 4 },
                { 'Z', 10 }
            };

            int LetterScore = LetterScores.FirstOrDefault(x => x.Key == c).Value;

            return LetterScore;
        }
    }
}
