using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleVariations;
using Xunit;

namespace WordleVariations.Test
{
    public class SecretWordHandlerTests
    {
        private GetWordsStub wordGetterStub;
        private SecretWordRepository handler;

        private string[] oneWordList = new string[] { "COOKS" };
        private string[] fiveWordList = new string[] { "COOKS", "SCUBA", "POINT", "WHARF", "QUEUE" };

        private SecretWord secretWord;
        private SecretWord[] secretWords;

        #region givens

        private void Given1Word()
        {
            wordGetterStub = new GetWordsStub(oneWordList);
        }

        private void Given5Words()
        {
            wordGetterStub = new GetWordsStub(fiveWordList);
        }

        private void GivenSecretWordHandler()
        {
            handler = new SecretWordRepository(wordGetterStub);
        }

        #endregion

        #region whens

        private void WhenGenerateSecretWord()
        {
            secretWord = handler.GetRandomFiveLetterWord();
        }

        private void WhenGenerateFiveSecretWords()
        {
            secretWords = handler.GetRandomFiveLetterWords(5);
        }

        #endregion

        #region thens

        private void ThenSecretWordIsOnlyWord()
        {
            Assert.True(secretWord.IsGuessCorrect("COOKS"));
            Assert.True(secretWord.IsGuessCorrect("cooks"));
        }

        private void ThenSecretWordIsValidEntry()
        {
            Assert.True(handler.IsValidFiveLetterWord(secretWord.RevealWord()));
        }

        private void ThenScubaIsInvalidEntry()
        {
            Assert.False(handler.IsValidFiveLetterWord("scuba"));
        }

        private void ThenAllSecretWordsDistinct()
        {
            string[] secretWordStrings = secretWords.Select(x => x.RevealWord()).ToArray();
            Assert.Equal(secretWordStrings.Length, secretWordStrings.Distinct().Count());
        }

        #endregion

        #region tests

        [Fact]
        public void TestOneWordList_SecretWordCreation()
        {
            Given1Word();
            GivenSecretWordHandler();

            WhenGenerateSecretWord();

            ThenSecretWordIsOnlyWord();
        }

        [Fact]
        public void TestOneWordList_ValidGuess()
        {
            Given1Word();
            GivenSecretWordHandler();

            WhenGenerateSecretWord();

            ThenSecretWordIsValidEntry();
        }

        [Fact]
        public void TestOneWordList_InvalidGuess()
        {
            Given1Word();
            GivenSecretWordHandler();

            WhenGenerateSecretWord();

            ThenScubaIsInvalidEntry();
        }

        [Fact]
        public void TestFiveWordList_AllSecretWordsDistinct()
        {
            Given5Words();
            GivenSecretWordHandler();

            WhenGenerateFiveSecretWords();

            ThenAllSecretWordsDistinct();
        }

        [Fact]
        public void TestOneWordList_AttemptGenerateFiveSecretWords()
        {
            Given1Word();
            GivenSecretWordHandler();

            Assert.Throws<InvalidDataException>(() => WhenGenerateFiveSecretWords());
        }

        #endregion
    }
}
