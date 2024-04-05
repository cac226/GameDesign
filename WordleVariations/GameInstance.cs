using System;
namespace WordleVariations
{
	internal class GameInstance
	{
        private IGetWords wordGetter;
		private SecretWord secretWord;
		private int guessCount;

        public GameInstance(IGetWords inpWordGetter)
		{
			wordGetter = inpWordGetter;
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
            LetterType[] letterResult = secretWord.GuessWord(guess);
			bool hasWon = IsSecretWord(guess);
			guessCount++;
			return GuessResponseData.CreateDataValidGuess(letterResult, hasWon, guessCount);
		}

		public bool IsSecretWord(string guess)
		{
            return string.Equals(secretWord.RevealWord(), guess, StringComparison.OrdinalIgnoreCase);
        }

		private bool isValidGuess(string guess)
		{
			return wordGetter.IsValidFiveLetterWord(guess);
		}
	}
}

