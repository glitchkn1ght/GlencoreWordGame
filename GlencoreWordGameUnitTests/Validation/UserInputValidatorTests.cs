using GlencoreWordGame.Models.Game;
using GlencoreWordGame.Models.Response;
using GlencoreWordGame.Service;
using GlencoreWordGame.Validation;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GlencoreWordGameUnitTests.Validation
{
    [TestFixture]
    public class UserInputValidatorTests
    {
        private Mock<ILogger<UserInputValidator>> loggerMock;
        private Mock<IWordGameService> gameServiceMock;
        private UserInputValidator userInputValidator;
        private GameRound round;
        
        [SetUp]
        public void Setup()
        {
            this.loggerMock = new Mock<ILogger<UserInputValidator>>();
            this.gameServiceMock = new Mock<IWordGameService>();

            this.round = new GameRound("SOMERANDOMSTRING");
        }

        [Test]
        public void WhenConstructorCalledWithNullLogger_ThenArgNullExceptionThrown()
        {
            Assert.Throws(
                Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("logger"), delegate
                {
                    this.userInputValidator = new UserInputValidator
                    (
                       this.gameServiceMock.Object,
                       null
                    );
                });
        }

        [Test]
        public void WhenConstructorCalledWithNullGameServiceObject_ThenArgNullExceptionThrown()
        {
            Assert.Throws(
                Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("wordGameService"), delegate
                {
                    this.userInputValidator = new UserInputValidator
                    (
                       null,
                       this.loggerMock.Object
                    );
                });
        }

        [Test]
        public void WhenConstructorCalledWithValidArguements_ThenNoExceptionThrown()
        {
            Assert.DoesNotThrow(
                delegate
                {
                    this.userInputValidator = new UserInputValidator
                    (
                        this.gameServiceMock.Object,
                        this.loggerMock.Object
                    );
                });
        }

        [TestCase("")]
        [TestCase("ON")]
        [TestCase("A")]
        [TestCase("SOMERANDOMSTRINGS")]
        public void WhenInputtedWordLengthInvalid_ThenReturnFalse(string testInput)
        {
            this.gameServiceMock.Setup(x => x.CheckWordValidity(testInput)).Returns(Task.FromResult(new WordResponse { IsSuccess = true }));

            this.userInputValidator = new UserInputValidator(this.gameServiceMock.Object, this.loggerMock.Object);

            bool actual = userInputValidator.ValidateUserInput(this.round, testInput).Result;

            Assert.AreEqual(false, actual);
        }

        [TestCase("WOMAN")] // 'W' not in avaiable letters
        [TestCase("DID")] // 'D' only appears once in available letters
        public void WhenInputtedWordNotInAvailableLetters_ThenReturnFalse(string testInput)
        {
            this.gameServiceMock.Setup(x => x.CheckWordValidity(testInput)).Returns(Task.FromResult(new WordResponse { IsSuccess = true }));

            this.userInputValidator = new UserInputValidator(this.gameServiceMock.Object, this.loggerMock.Object);

            bool actual = userInputValidator.ValidateUserInput(this.round, testInput).Result;

            Assert.AreEqual(false, actual);
        }

        [TestCase("SOME")]
        public void WhenInputtedWordAlreadyUsed_ThenReturnFalse(string testInput)
        {
            this.round.AlreadyInputtedWords.Add("SOME");
            
            this.gameServiceMock.Setup(x => x.CheckWordValidity(testInput)).Returns(Task.FromResult(new WordResponse { IsSuccess = true }));

            this.userInputValidator = new UserInputValidator(this.gameServiceMock.Object, this.loggerMock.Object);

            bool actual = userInputValidator.ValidateUserInput(this.round, testInput).Result;

            Assert.AreEqual(false, actual);
        }

        [TestCase("SOM")]
        public void WhenInputtedWordNotInAPIDictionary_ThenReturnFalse(string testInput)
        {
            this.gameServiceMock.Setup(x => x.CheckWordValidity(testInput)).Returns(Task.FromResult(new WordResponse { IsSuccess = false }));

            this.userInputValidator = new UserInputValidator(this.gameServiceMock.Object, this.loggerMock.Object);

            bool actual = userInputValidator.ValidateUserInput(this.round, testInput).Result;

            Assert.AreEqual(false, actual);
        }

        [TestCase("SOME")]
        public void WhenWordValid_ThenReturnTrue(string testInput)
        {
            this.gameServiceMock.Setup(x => x.CheckWordValidity(testInput)).Returns(Task.FromResult(new WordResponse { IsSuccess = true }));

            this.userInputValidator = new UserInputValidator(this.gameServiceMock.Object, this.loggerMock.Object);

            bool actual = userInputValidator.ValidateUserInput(this.round, testInput).Result;

            Assert.AreEqual(true, actual);
        }
    }
}