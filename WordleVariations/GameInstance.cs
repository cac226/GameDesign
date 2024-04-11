using System;
namespace WordleVariations
{
	internal class GameInstance
	{
		private SecretWordRepository wordHandler;
		private SecretWordContainer secretWord;
		private int secretWordLength;
		private int guessCount;

        public GameInstance(IGetWords inpWordGetter)
		{
			wordHandler = new SecretWordRepository(inpWordGetter);
			secretWordLength = 5;
            secretWord = wordHandler.GetRandomFiveLetterWord();
            guessCount = 0;
        }

		public void SetupNewGame()
		{
			secretWord = wordHandler.GetRandomFiveLetterWord();
			guessCount = 0;
		}

		public GuessResponseData MakeGuess(string guess)
		{
			if(!isValidGuess(guess))
			{
				return GuessResponseData.CreateDataInvalidGuess(guessCount);
			}

			return buildGuessResult(guess);
		}

		public bool IsSecretWord(string guess)
		{
            return secretWord.IsCorrectGuess(guess);
        }

		private GuessResponseData buildGuessResult(string validGuess)
		{
            LetterType[] letterResult = secretWord.GuessWord(validGuess);
            bool hasWon = IsSecretWord(validGuess);
            guessCount++;
            return GuessResponseData.CreateDataValidGuess(letterResult, hasWon, guessCount);
        }

		private bool isValidGuess(string guess)
		{
			return guess.Length == secretWordLength && wordHandler.IsValidFiveLetterWord(guess);
		}
	}
}

