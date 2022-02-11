using GlencoreWordGame.Scoring;
using NUnit.Framework;

namespace GlencoreWordGameUnitTests.Scoring
{
    [TestFixture]
    class ScoringTests
    {
        [TestCase("SOME",6)]
        [TestCase("VOX", 13)]
        [TestCase("XEROX", 19)]
        public void WordsScoreAsExpected(string word, int expectedScore)
        {
            WordGameScorer wordGameScorer = new WordGameScorer();

            int actualScore = wordGameScorer.GetWordScore(word);

            Assert.AreEqual(expectedScore, actualScore);
        }
    }
}
