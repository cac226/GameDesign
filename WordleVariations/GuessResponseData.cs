using System;
namespace WordleVariations
{
	internal class GuessResponseData
	{
		private LetterType[] letterGuessResult;
		private bool hasWon;
        private bool wasLastGuessValid;
		private int guessCount;

        private GuessResponseData(LetterType[] letterGuessResult, bool hasWon, bool wasLastGuessValid, int guessCount)
        {
            this.letterGuessResult = letterGuessResult;
            this.hasWon = hasWon;
            this.wasLastGuessValid = wasLastGuessValid;
            this.guessCount = guessCount;
        }

        public static GuessResponseData CreateDataValidGuess(LetterType[] letterGuessResult, bool hasWon, int guessCount)
        {
            return new GuessResponseData(letterGuessResult, hasWon, true, guessCount);
        }

        public static GuessResponseData CreateDataInvalidGuess(int guessCount)
        {
            return new GuessResponseData(new LetterType[0], false, false, guessCount);
        }

        public LetterType[] GetLetterData()
        {
            return letterGuessResult;
        }

        public bool HasWon()
        {
            return hasWon;
        }

        public bool WasLastGuessValid()
        {
            return wasLastGuessValid;
        }

        public int GuessCount()
        {
            return guessCount;
        }
    }
}

