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
        private SecretWordHandler handler;

        private string[] oneWordList = new string[] { "COOKS" };

        private SecretWord secretWord;

        #region givens

        private void Given1Word()
        {
            wordGetterStub = new GetWordsStub(oneWordList);
        }

        private void GivenSecretWordHandler()
        {
            handler = new SecretWordHandler(wordGetterStub);
        }

        #endregion

        #region whens

        private void WhenGenerateSecretWord()
        {
            secretWord = handler.GetRandomFiveLetterWord();
        }

        #endregion

        #region thens

        private void ThenSecretWordIsOnlyWord ()
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

        #endregion
    }
}
