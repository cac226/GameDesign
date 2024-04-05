using System;
namespace WordleVariations
{
	internal class GameInstance
	{
        private IGetWords wordGetter;
		private SecretWord secretWord;
		private int secretWordLength;
		private int guessCount;

        public GameInstance(IGetWords inpWordGetter)
		{
			wordGetter = inpWordGetter;
			secretWordLength = 5;
            secretWord = wordGetter.GetRandomFiveLetterWord();
            guessCount = 0;
        }

		public void SetupNewGame()
		{
			secretWord = wordGetter.GetRandomFiveLetterWord();
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
			return guess.Length == secretWordLength && wordGetter.IsValidFiveLetterWord(guess);
		}
	}
}

