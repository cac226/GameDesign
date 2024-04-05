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

		public bool TryMakeGuess(string guess, out GuessResult[] result)
		{
			if(!isValidGuess(guess))
			{
				result = new GuessResult[0];
				return false;
			}
			result = secretWord.GuessWord(guess);
			guessCount++;
			return true;
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

