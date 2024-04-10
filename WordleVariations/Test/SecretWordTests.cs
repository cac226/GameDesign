using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using WordleVariations;



namespace WordleVariations.Test
{
    public class SecretWordTests
    {
        private SecretWord secretWord;
        private LetterType[] guessResult;

        #region givens
        private void secretWordChess()
        {
            secretWord = new SecretWord("Chess");
        }

        private void secretWordScuba()
        {
            secretWord = new SecretWord("scUBA");
        }

        private void secretWordFacts()
        {
            secretWord = new SecretWord("facts");
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

        private void whenGuessFacts()
        {
            guessResult = secretWord.GuessWord("Facts");
        }

        private void whenGuessSSSSS()
        {
            guessResult = secretWord.GuessWord("SSSSS");
        }

        #endregion

        #region thens 

        private void thenAllIncorrect()
        {
            for (int i = 0; i < guessResult.Length; i++)
            {
                Assert.Equal(LetterType.INCORRECT, guessResult[i]);
            }
        }

        private void thenAllCorrect()
        {
            for (int i = 0; i < guessResult.Length; i++)
            {
                Assert.Equal(LetterType.CORRECT, guessResult[i]);
            }
        }

        /*
         * C = Correct 
         * X = Incorrect 
         * P = Partially correct (right letter wrong location) 
         */



        private void thenResultIs_PXXPX()
        {
            Assert.Equal(LetterType.RIGHT_LETTER_WRONG_LOCATION, guessResult[0]);
            Assert.Equal(LetterType.INCORRECT, guessResult[1]);
            Assert.Equal(LetterType.INCORRECT, guessResult[2]);
            Assert.Equal(LetterType.RIGHT_LETTER_WRONG_LOCATION, guessResult[3]);
            Assert.Equal(LetterType.INCORRECT, guessResult[4]);
        }

        private void thenResultIs_PXXXC()
        {
            Assert.Equal(LetterType.RIGHT_LETTER_WRONG_LOCATION, guessResult[0]);
            Assert.Equal(LetterType.INCORRECT, guessResult[1]);
            Assert.Equal(LetterType.INCORRECT, guessResult[2]);
            Assert.Equal(LetterType.INCORRECT, guessResult[3]);
            Assert.Equal(LetterType.CORRECT, guessResult[4]);
        }

        private void thenResultIs_XXPXC()
        {
            Assert.Equal(LetterType.INCORRECT, guessResult[0]);
            Assert.Equal(LetterType.INCORRECT, guessResult[1]);
            Assert.Equal(LetterType.RIGHT_LETTER_WRONG_LOCATION, guessResult[2]);
            Assert.Equal(LetterType.INCORRECT, guessResult[3]);
            Assert.Equal(LetterType.CORRECT, guessResult[4]);
        }

        private void thenResultIs_XXXCC()
        {
            Assert.Equal(LetterType.INCORRECT, guessResult[0]);
            Assert.Equal(LetterType.INCORRECT, guessResult[1]);
            Assert.Equal(LetterType.INCORRECT, guessResult[2]);
            Assert.Equal(LetterType.CORRECT, guessResult[3]);
            Assert.Equal(LetterType.CORRECT, guessResult[4]);
        }


        #endregion

        #region tests 

        [Fact]
        public void Chess_GuessPoint()
        {
            secretWordChess();

            Assert.Equal("CHESS", secretWord.RevealWord());

            whenGuessPoint();
            thenAllIncorrect();

        }

        [Fact]
        public void Chess_GuessChess()
        {
            secretWordChess();

            Assert.Equal("CHESS", secretWord.RevealWord());

            whenGuessChess();
            thenAllCorrect();
        }

        [Fact]
        public void Chess_GuessSSSSS()
        {
            secretWordChess();

            whenGuessSSSSS();
            thenResultIs_XXXCC();
        }

        [Fact]
        public void Chess_GuessFacts()
        {
            secretWordChess();
            whenGuessFacts();

            thenResultIs_XXPXC();
        }

        [Fact]
        public void Facts_GuessChess()
        {
            secretWordFacts();
            whenGuessChess();

            thenResultIs_PXXXC();
        }

        [Fact]
        public void Scuba_GuessChess()
        {
            secretWordScuba();

            Assert.Equal("SCUBA", secretWord.RevealWord());

            whenGuessChess();
            thenResultIs_PXXPX();
        }



        #endregion
    }
}
