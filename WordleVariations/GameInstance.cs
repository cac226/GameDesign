using System;
namespace WordleVariations
{
	internal class GameInstance
	{
		private SecretWordHandler wordHandler;
		private SecretWord secretWord;
		private int secretWordLength;
		private int guessCount;

        public GameInstance(IGetWords inpWordGetter)
		{
			wordHandler = new SecretWordHandler(inpWordGetter);
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
            return string.Equals(secretWord.RevealWord(), guess, StringComparison.OrdinalIgnoreCase);
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

