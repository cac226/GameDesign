using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleVariations.DataObjects
{
    internal class WordFeedback
    {
        private LetterResponse[] letterResponse;
        private bool wasGuessCorrect;

        private WordFeedback(LetterResponse[] letterResponse, bool wasGuessCorrect)
        {
            this.letterResponse = letterResponse;
            this.wasGuessCorrect = wasGuessCorrect;
        }

        public static WordFeedback Create(LetterResponse[] response)
        {
            bool wasGuessCorrect = responseCompletelyCorrect(response);
            return new WordFeedback(response, wasGuessCorrect);
        }

        public static WordFeedback CreateEmptyCorrectResponse()
        {
            return new WordFeedback(new LetterResponse[0], true);
        }

        public bool HasCorrectlyGuessed()
        {
            return wasGuessCorrect;
        }

        public LetterResponse[] GetLetterResponse() {  return letterResponse; }

        private static bool responseCompletelyCorrect(LetterResponse[] response)
        {
            return response.All(x => x == LetterResponse.CORRECT);
        }
    }
}
