using System;

namespace WordleVariations.DataObjects
{
    internal class GuessResponseData
    {
        private WordFeedback[] wordFeedback;
        private bool hasWon;
        private bool wasLastGuessValid;
        private int guessCount;

        private GuessResponseData(WordFeedback[] letterGuessResult, bool hasWon, bool wasLastGuessValid, int guessCount)
        {
            this.wordFeedback = letterGuessResult;
            this.hasWon = hasWon;
            this.wasLastGuessValid = wasLastGuessValid;
            this.guessCount = guessCount;
        }

        public static GuessResponseData CreateDataValidGuess(WordFeedback[] wordFeedback, int guessCount)
        {
            bool hasWon = wordFeedback.All(word => word.HasCorrectlyGuessed());
            return new GuessResponseData(wordFeedback, hasWon, true, guessCount);
        }

        public static GuessResponseData CreateDataInvalidGuess(int guessCount)
        {
            return new GuessResponseData(new WordFeedback[0], false, false, guessCount);
        }

        public WordFeedback[] GetWordFeedback()
        {
            return wordFeedback;
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

