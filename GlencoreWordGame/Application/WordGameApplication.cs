using GlencoreWordGame.Config;
using GlencoreWordGame.Models.Game;
using GlencoreWordGame.Models.User;
using GlencoreWordGame.Utility;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GlencoreWordGame.Service;
using System.Threading;

namespace GlencoreWordGame.Application
{
    public class WordGameApplication
    {
        private readonly ILogger<WordGameApplication> Logger;
        private readonly GameSettings GameSettings;
        private readonly IWordGameOrchestrator WordGameOrchestrator;
        public GameUser gameUser;

        public WordGameApplication(IOptions<GameSettings> gameSettings, IWordGameOrchestrator wordGameOrchestrator, ILogger<WordGameApplication> logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.GameSettings = gameSettings?.Value ?? throw new ArgumentNullException(nameof(gameSettings));
            this.WordGameOrchestrator = wordGameOrchestrator ?? throw new ArgumentNullException(nameof(wordGameOrchestrator));
        }

        public async Task RunGame(string[] args)
        {
            try
            {
                this.Logger.LogInformation($"Operation=RunGame(App), Status=Success, Message=Game has started successfully.");

                this.DisplayUserInstructions();

                await this.RunGameRound();

                this.Logger.LogInformation($"Operation=RunGame(App), Status=Success, Message=Game has finished successfully.");
            }

            catch (Exception ex)
            {
                Console.WriteLine("Sorry the game has experienced an unexpected failure, please restart.");

                this.Logger.LogError($"Operation=RunGame(App), Status=Failure, Message={ex.Message}");
            }
        }

        public async Task RunGameRound()
        {
            this.RoundHello();

            GameRound round = new GameRound(RandomString.GetRandomString(12));

            Console.WriteLine($"Your random string is {round.AvailableLetters}");

            Stopwatch s = new Stopwatch();
            s.Start();
            while (s.Elapsed < TimeSpan.FromMilliseconds(this.GameSettings.RoundLength))
            {
                string input = Console.ReadLine().ToUpper();

                round = await this.WordGameOrchestrator.ReturnWordScore(round, input);
            }

            Console.WriteLine($"Congratualtions your final score is {round.GameUser.Score}");
            this.RoundGoodbye();
        }

        private void DisplayUserInstructions()
        {
            Console.WriteLine($"Welcome to word game, in this game you have {this.GameSettings.RoundLength / 1000} seconds to find as many words as possible within the given random string!");
            Console.WriteLine($"Scores are calculated per letter following scrabble scoring system.");
            Console.WriteLine($"Instructions 1: You can use each character in the given string only once.");
            Console.WriteLine($"Instructions 2: Words must be at least 3 letters long.");
            Console.WriteLine($"Instructions 3: You cannot use the same word more than once.");
            Console.WriteLine($"");
        }

        private void RoundHello()
        {
            Console.WriteLine($"Your game will begin in {this.GameSettings.RoundStartDelay / 1000} seconds.....");
            Thread.Sleep(this.GameSettings.RoundStartDelay);
            Console.WriteLine("Go!");
        }

        private void RoundGoodbye()
        {
            Thread.Sleep(this.GameSettings.RoundEndDelay);
            Console.Clear();
        }
    }
}