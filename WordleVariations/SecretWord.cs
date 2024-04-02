using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleVariations
{
    internal class SecretWord
    {
        private readonly string secretWord; 

        public SecretWord(string word)
        {
            secretWord = word;
        }

        public GuessResult[] GuessWord(string guess)
        {
            GuessResult[] result = new GuessResult[guess.Length];

            for(int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == secretWord[i])
                {
                    result[i] = GuessResult.CORRECT;
                } else if (!containsLetter(guess[i]))
                {
                    result[i] = GuessResult.INCORRECT;
                } else
                {
                    result[i] = GuessResult.RIGHT_LETTER_WRONG_LOCATION;
                }
            }

            return result;
        }

        private bool containsLetter(char c)
        {
            return secretWord.Contains(c);
        }
    }
}
