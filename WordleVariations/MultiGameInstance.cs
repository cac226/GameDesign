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
        private List<string> pastGuesses;


        private MultiGameInstance(IGetWords inpWordGetter, int numSecretWords)
        {
            wordRepository = new SecretWordRepository(inpWordGetter);
            this.numSecretWords = numSecretWords;
        }

        public void SetupNewGame()
        {
            secretWords = wordRepository.GetRandomFiveLetterWords(numSecretWords);
            pastGuesses = new List<string>();
        }

        public static MultiGameInstance Create(IGetWords inpWordGetter, int numSecretWords)
        {
            MultiGameInstance result = new MultiGameInstance(inpWordGetter, numSecretWords);
            result.SetupNewGame();
            return result;
        }
        public GuessResponseData MakeGuess(string guess)
        {
            if (hasGuessedWordBefore(guess))
            {
                return GuessResponseData.CreateDataRepeatGuess();
            }
            if(!isRecognizedWord(guess))
            {
                return GuessResponseData.CreateDataInvalidGuess();
            }


            pastGuesses.Add(guess);
            return buildGuessResult(guess);
        }

        public int GetGuessCount()
        {
            return pastGuesses.Count;
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

        public string[] RevealAllWords()
        {
            string[] result = secretWords.Select(x => x.RevealWord()).ToArray();
            return result;
        }

        private GuessResponseData buildGuessResult(string validGuess)
        {
            // Contains data on correct/incorrect letter guesses for multiple secret words 
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

            GuessResponseData result = GuessResponseData.CreateDataValidGuess(wordFeedback);

            return result;
        }

        private bool hasGuessedWordBefore(string guess)
        {
            return pastGuesses.Contains(guess);
        }

        private bool isRecognizedWord(string guess)
        {
            return wordRepository.IsValidFiveLetterWord(guess);
        }
    }
}
