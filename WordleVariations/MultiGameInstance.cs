using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleVariations.DataObjects;

namespace WordleVariations
{
    internal class MultiGameInstance
    {
        private SecretWordRepository wordRepository;
        private int numSecretWords;

        private SecretWordContainer[] secretWords;
        private int guessCount;


        private MultiGameInstance(IGetWords inpWordGetter, int numSecretWords)
        {
            wordRepository = new SecretWordRepository(inpWordGetter);
            this.numSecretWords = numSecretWords;
        }

        public void SetupNewGame()
        {
            secretWords = wordRepository.GetRandomFiveLetterWords(numSecretWords);
            guessCount = 0;
        }

        public static MultiGameInstance Create(IGetWords inpWordGetter, int numSecretWords)
        {
            MultiGameInstance result = new MultiGameInstance(inpWordGetter, numSecretWords);
            result.SetupNewGame();
            return result;
        }
        public GuessResponseData MakeGuess(string guess)
        {
            if (!isValidGuess(guess))
            {
                return GuessResponseData.CreateDataInvalidGuess(guessCount);
            }

            return buildGuessResult(guess);
        }

        public bool[] IsSecretWord(string guess)
        {
            bool[] result = new bool[secretWords.Length];

            for(int i = 0; i < secretWords.Length; i++)
            {
                result[i] = secretWords[i].IsCorrectGuess(guess);
            }

            return result;
        }

        private GuessResponseData buildGuessResult(string validGuess)
        {
            guessCount++;
            WordFeedback[] wordFeedback = new WordFeedback[secretWords.Length];

            for(int i = 0; i < wordFeedback.Length; i++)
            {
                if (!secretWords[i].HasBeenRevealed())
                {
                    LetterResponse[] letterResult = secretWords[i].GuessWord(validGuess);
                    bool hasWon = secretWords[i].IsCorrectGuess(validGuess);
                    wordFeedback[i] = WordFeedback.Create(letterResult);
                } else
                {
                    wordFeedback[i] = WordFeedback.CreateEmptyCorrectResponse();
                }
                
            }

            GuessResponseData result = GuessResponseData.CreateDataValidGuess(wordFeedback, guessCount);

            return result;
        }

        private bool isValidGuess(string guess)
        {
            return wordRepository.IsValidFiveLetterWord(guess);
        }
    }
}
