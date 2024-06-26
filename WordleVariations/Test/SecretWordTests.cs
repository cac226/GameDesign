﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using WordleVariations.DataObjects;

namespace WordleVariations.Test
{
    public class SecretWordTests
    {
        private SecretWordContainer secretWord;
        private LetterResponse[] guessResult;

        #region givens
        private void secretWordChess()
        {
            secretWord = new SecretWordContainer("Chess");
        }

        private void secretWordScuba()
        {
            secretWord = new SecretWordContainer("scUBA");
        }

        private void secretWordFacts()
        {
            secretWord = new SecretWordContainer("facts");
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
                Assert.Equal(LetterResponse.INCORRECT, guessResult[i]);
            }
        }

        private void thenAllCorrect()
        {
            for (int i = 0; i < guessResult.Length; i++)
            {
                Assert.Equal(LetterResponse.CORRECT, guessResult[i]);
            }
        }

        /*
         * C = Correct 
         * X = Incorrect 
         * P = Partially correct (right letter wrong location) 
         */



        private void thenResultIs_PXXPX()
        {
            Assert.Equal(LetterResponse.RIGHT_LETTER_WRONG_LOCATION, guessResult[0]);
            Assert.Equal(LetterResponse.INCORRECT, guessResult[1]);
            Assert.Equal(LetterResponse.INCORRECT, guessResult[2]);
            Assert.Equal(LetterResponse.RIGHT_LETTER_WRONG_LOCATION, guessResult[3]);
            Assert.Equal(LetterResponse.INCORRECT, guessResult[4]);
        }

        private void thenResultIs_PXXXC()
        {
            Assert.Equal(LetterResponse.RIGHT_LETTER_WRONG_LOCATION, guessResult[0]);
            Assert.Equal(LetterResponse.INCORRECT, guessResult[1]);
            Assert.Equal(LetterResponse.INCORRECT, guessResult[2]);
            Assert.Equal(LetterResponse.INCORRECT, guessResult[3]);
            Assert.Equal(LetterResponse.CORRECT, guessResult[4]);
        }

        private void thenResultIs_XXPXC()
        {
            Assert.Equal(LetterResponse.INCORRECT, guessResult[0]);
            Assert.Equal(LetterResponse.INCORRECT, guessResult[1]);
            Assert.Equal(LetterResponse.RIGHT_LETTER_WRONG_LOCATION, guessResult[2]);
            Assert.Equal(LetterResponse.INCORRECT, guessResult[3]);
            Assert.Equal(LetterResponse.CORRECT, guessResult[4]);
        }

        private void thenResultIs_XXXCC()
        {
            Assert.Equal(LetterResponse.INCORRECT, guessResult[0]);
            Assert.Equal(LetterResponse.INCORRECT, guessResult[1]);
            Assert.Equal(LetterResponse.INCORRECT, guessResult[2]);
            Assert.Equal(LetterResponse.CORRECT, guessResult[3]);
            Assert.Equal(LetterResponse.CORRECT, guessResult[4]);
        }

        #endregion

        #region tests 

        [Fact]
        public void Chess_GuessPoint()
        {
            secretWordChess();

            Assert.Equal("CHESS", secretWord.PeekWord());

            whenGuessPoint();
            thenAllIncorrect();

        }

        [Fact]
        public void Chess_GuessChess()
        {
            secretWordChess();

            Assert.Equal("CHESS", secretWord.PeekWord());

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

            Assert.Equal("SCUBA", secretWord.PeekWord());

            whenGuessChess();
            thenResultIs_PXXPX();
        }



        #endregion
    }
}
