using GlencoreWordGame.Models.Game;
using GlencoreWordGame.Scoring;
using GlencoreWordGame.Utility;
using GlencoreWordGame.Validation;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GlencoreWordGame.Service
{
    public interface IWordGameOrchestrator
    {
        Task<GameRound> ReturnWordScore(GameRound round, string inputtedWord);
    }

    public class WordGameOrchestrator : IWordGameOrchestrator
    {
        private readonly ILogger<WordGameOrchestrator> Logger;
        public IUserInputValidator UserInputValidator;
        public IWordGameScorer WordGameScorer;

        public WordGameOrchestrator(IUserInputValidator userInputValidator, IWordGameScorer wordGameScorer, ILogger<WordGameOrchestrator> logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.UserInputValidator = userInputValidator ?? throw new ArgumentNullException(nameof(userInputValidator));
            this.WordGameScorer = wordGameScorer ?? throw new ArgumentNullException(nameof(wordGameScorer));
        }

        public async Task<GameRound> ReturnWordScore(GameRound round, string inputtedWord)
        {
            int score = 0;
            
            if (await this.UserInputValidator.ValidateUserInput(round, inputtedWord))
            {
                this.Logger.LogInformation($"Operation=ReturnWordScore(WordGameOrchestrator), Status=Success, Message=word {inputtedWord} has been successfully validated.");

                round.AlreadyInputtedWords.Add(inputtedWord);
                
                score = this.WordGameScorer.GetWordScore(inputtedWord);

                this.Logger.LogInformation($"Operation=ReturnWordScore(WordGameOrchestrator), Status=Success, Message=word {inputtedWord} has been successfully scored for a score of {score}.");

                round.GameUser.Score += score;
            }

            this.NotifyUser(score, inputtedWord);

            return round;
        }

        private void NotifyUser(int score, string inputtedWord)
        {
            if(score == 0)
            {
                Console.WriteLine($"Sorry the word {inputtedWord}, is not valid and scores 0. Please try again.");
            }

            else
            {
                Console.WriteLine($"You have inputted the word {inputtedWord} for a score of {score}");
            }

            Thread.Sleep(2000);
            ClearLastConsoleLines.ClearLastLine();
        }
    }
}
