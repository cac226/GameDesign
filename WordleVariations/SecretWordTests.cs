using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace WordleVariations
{
    [TestClass]
    public class SecretWordTests
    {
        private SecretWord secretWord;
        private GuessResult[] guessResult;

        #region givens
        private void secretWordCheese()
        {
            secretWord = new SecretWord("cheese");
        }

        private void secretWordScuba()
        {
            secretWord = new SecretWord("scUBA");
        }

        #endregion

        #region whens

        private void whenGuessPoint()
        {
            guessResult = secretWord.GuessWord("point");
        }

        private void whenGuessCheese()
        {
            guessResult = secretWord.GuessWord("cheese");
        }

        #endregion

        #region thens 

        private void thenAllIncorrect()
        {
            for(int i = 0; i < guessResult.Length; i++)
            {
                Xunit.Assert.Equal(GuessResult.INCORRECT, guessResult[i]);
            }
        }

        private void thenAllCorrect()
        {
            for (int i = 0; i < guessResult.Length; i++)
            {
                Xunit.Assert.Equal(GuessResult.CORRECT, guessResult[i]);
            }
        }

        #endregion

        #region tests 

        [Fact]
        public void Cheese_GuessPoint()
        {
            secretWordCheese();

            Xunit.Assert.Equal("CHEESE", secretWord.RevealWord());

            whenGuessPoint();
            whenGuessPoint();
            whenGuessPoint();
            thenAllIncorrect();

        }

        [Fact] 
        public void Cheese_GuessCheese()
        {
            secretWordCheese();

            Xunit.Assert.Equal("CHEESE", secretWord.RevealWord());

            whenGuessCheese();
            thenAllCorrect();
        }

        #endregion
    }
}
