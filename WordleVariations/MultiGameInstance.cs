using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public GuessResponseData[] MakeGuess(string guess)
        {
            if (!isValidGuess(guess))
            {
                return Enumerable.Repeat(GuessResponseData.CreateDataInvalidGuess(guessCount), secretWords.Length).ToArray();
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

        private GuessResponseData[] buildGuessResult(string validGuess)
        {
            guessCount++;
            GuessResponseData[] result = new GuessResponseData[secretWords.Length];

            for(int i = 0; i <  result.Length; i++)
            {
                if (!secretWords[i].HasBeenRevealed())
                {
                    LetterType[] letterResult = secretWords[i].GuessWord(validGuess);
                    bool hasWon = secretWords[i].IsCorrectGuess(validGuess);
                    result[i] = GuessResponseData.CreateDataValidGuess(letterResult, hasWon, guessCount);
                } else
                {
                    result[i] = GuessResponseData.CreateDataValidGuess(new LetterType[0], true, guessCount);
                }
                
            }

            return result;
        }

        private bool isValidGuess(string guess)
        {
            return wordRepository.IsValidFiveLetterWord(guess);
        }
    }
}
