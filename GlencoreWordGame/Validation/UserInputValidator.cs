using GlencoreWordGame.Models.Game;
using GlencoreWordGame.Models.Response;
using GlencoreWordGame.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlencoreWordGame.Validation
{
    public interface IUserInputValidator
    {
        Task<bool> ValidateUserInput(GameRound round, string inputtedWord);
    }

    public class UserInputValidator : IUserInputValidator
    {
        private readonly ILogger<UserInputValidator> Logger;
        public IWordGameService WordGameService;

        public UserInputValidator(IWordGameService wordGameService, ILogger<UserInputValidator> logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.WordGameService = wordGameService ?? throw new ArgumentNullException(nameof(wordGameService));
        }

        public async Task<bool> ValidateUserInput(GameRound round, string inputtedWord)
        {
            //Really should separate this into different validation methods.
            if(inputtedWord.Length < 3 || inputtedWord.Length > round.MaximumWordLength)
            {
                return false;
            }

            if (round.AlreadyInputtedWords.Any(str => str.Contains(inputtedWord)))
            {
                return false;
            }

            if (!this.IsWordAllowed(round.AvailableLetters, inputtedWord))
            {
                return false;
            }

            this.Logger.LogInformation($"Operation=ValidateUserInput(UserInputValidator), Status=Success, Message=Basic validation complete, checking dictionaryAPI");

            WordResponse apiReponse = await this.WordGameService.CheckWordValidity(inputtedWord);

            return apiReponse.IsSuccess;
        }

        private bool IsWordAllowed(string availableLetters, string inputtedWord)
        {
            //Get the occurance of each character for both strings.
            Dictionary<char, int> allowedWordChars = this.getCharacterFrequency(availableLetters);
            Dictionary<char, int> inputtedWordChars = this.getCharacterFrequency(inputtedWord);

            foreach (var item in inputtedWordChars)
            {
                //if inputted word contains a character that is not in available letters word is not valid
                if (!allowedWordChars.Any(x => (x.Key == item.Key)))
                {
                    return false;
                }

                //if there are less occurances of a character in the available letters than in the inputted word, than the word is not valid.
                if (allowedWordChars.Any(x => (x.Key == item.Key) && (x.Value < item.Value)))
                {
                    return false;
                }
            }

            return true;
        }

        private Dictionary<char, int> getCharacterFrequency(string str)
        {
            Dictionary<char, int> charOccurences = new Dictionary<char, int>();

            List<char> distinctChars = str.Distinct().ToList();

            foreach (char c in distinctChars)
            {
                int freq = str.Count(f => (f == c));

                charOccurences.Add(c, freq);
            }

            return charOccurences;
        }
    }
}
