using System;

namespace WordleVariations.DataObjects
{
    internal class GuessResponseData
    {
        private WordFeedback[] wordFeedback;
        private bool hasWon;
        private GuessError errorMessage;

        private GuessResponseData(WordFeedback[] letterGuessResult, bool hasWon, GuessError errorMessage)
        {
            this.wordFeedback = letterGuessResult;
            this.hasWon = hasWon;
            this.errorMessage = errorMessage;
        }

        public static GuessResponseData CreateDataValidGuess(WordFeedback[] wordFeedback)
        {
            bool hasWon = wordFeedback.All(word => word.HasCorrectlyGuessed());
            return new GuessResponseData(wordFeedback, hasWon, GuessError.NONE);
        }

        public static GuessResponseData CreateDataInvalidGuess()
        {
            return new GuessResponseData(new WordFeedback[0], false, GuessError.INVALID_WORD);
        }

        public static GuessResponseData CreateDataRepeatGuess()
        {
            return new GuessResponseData(new WordFeedback[0], false, GuessError.REPEAT_GUESS);
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
            return errorMessage == GuessError.NONE;
        }

        public GuessError GetError()
        {
            return errorMessage;
        }
    }
}

