using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;



namespace WordleVariations
{
    public class SecretWordTests
    {
        private SecretWord secretWord;
        private GuessResult[] guessResult;

        #region givens
        private void secretWordChess()
        {
            secretWord = new SecretWord("Chess");
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

        private void whenGuessChess()
        {
            guessResult = secretWord.GuessWord("Chess");
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
        public void Chess_GuessPoint()
        {
            secretWordChess();

            Xunit.Assert.Equal("CHESS", secretWord.RevealWord());

            whenGuessPoint();
            whenGuessPoint();
            whenGuessPoint();
            thenAllIncorrect();

        }

        [Fact] 
        public void Chess_GuessChess()
        {
            secretWordChess();

            Xunit.Assert.Equal("CHESS", secretWord.RevealWord());

            whenGuessChess();
            thenAllCorrect();
        }

        #endregion
    }
}
