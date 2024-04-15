using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleVariations.DataObjects;

namespace WordleVariations
{
    internal class SecretWordContainer
    {
        private SecretWord secretWord;

        public SecretWordContainer(SecretWord secretWordObject)
        {
            this.secretWord = secretWordObject;
        }

        public SecretWordContainer(string str)
        {
            secretWord = new SecretWord(str); 
        }

        /// <summary>
        /// Allows viewing of the secret word without marking it as revealed. Should only be used for tests
        /// </summary>
        /// <returns></returns>
        public string PeekWord()
        {
            return secretWord.PeekWord();
        }

        /// <summary>
        /// Reveals the secret word, guesses should not be allowed after this. 
        /// </summary>
        public string RevealWord()
        {
            return secretWord.RevealWord();
        }

        public bool HasBeenRevealed()
        {
            return secretWord.HasBeenRevealed();
        }

        public bool IsCorrectGuess(string guess)
        {
            return string.Equals(secretWord.PeekWord(), guess, StringComparison.OrdinalIgnoreCase);
        }

        public LetterResponse[] GuessWord(string guess)
        {
            string guessUpper = guess.ToUpper();
            LetterResponse[] result = guessWord(guessUpper);

            if(IsCorrectGuess(guess))
            {
                secretWord.RevealWord();
            }

            return result;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null) return false;

            string secretWordString = secretWord.PeekWord();

            if (obj is string)
            {
                return secretWordString.Equals(obj.ToString().ToUpper());
            }
            if(obj is SecretWord)
            {
                SecretWord secretWord = (SecretWord)obj;

                return secretWord.PeekWord().Equals(secretWordString);
            }

            SecretWordContainer other = (SecretWordContainer)obj;

            return other.PeekWord().Equals(secretWordString);
        }

        #region private methods

        private LetterResponse[] guessWord(string guess)
        {
            LetterResponse[] result = new LetterResponse[guess.Length];
            string secretWord = this.secretWord.PeekWord();

            for (int i = 0; i < guess.Length; i++)
            {
                char guessedLetter = guess[i];

                if (guessedLetter == secretWord[i])
                {
                    result[i] = LetterResponse.CORRECT;

                }
                else if (!containsLetter(guessedLetter))
                {
                    result[i] = LetterResponse.INCORRECT;

                }
                else
                {
                    // if a secret word has 1 of a letter, but the guess has 2 of that letter, we only want one of those guess letters to be marked as correct 
                    // e.g. if the secret word is "FACTS" and a user guesses "CHESS" the first S should be marked as incorrect and the second marked as correct 

                    int secretWordLetterCount = countOccurances(secretWord, guessedLetter);
                    int guessLetterCount = countOccurances(guess, guessedLetter);

                    if (guessLetterCount > secretWordLetterCount)
                    {
                        int truncatedGuessLetterCount = countOccurances(guess.Substring(0, i), guessedLetter);
                        int correctlyPlaced = countCorrectOccurances(guess, guessedLetter);

                        if (truncatedGuessLetterCount >= secretWordLetterCount - correctlyPlaced)
                        {
                            result[i] = LetterResponse.INCORRECT;
                        }
                        else
                        {
                            result[i] = LetterResponse.RIGHT_LETTER_WRONG_LOCATION;
                        }

                    }
                    else
                    {
                        result[i] = LetterResponse.RIGHT_LETTER_WRONG_LOCATION;
                    }
                }
            }

            return result;
        }

        private bool containsLetter(char c)
        {
            return secretWord.PeekWord().Contains(c);
        }

        private int countCorrectOccurances(string guess, char letter)
        {
            string secretWord = this.secretWord.PeekWord();
            int count = 0;

            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == letter && secretWord[i] == letter)
                {
                    count++;
                }
            }

            return count;
        }

        private static int countOccurances(string word, char targetLetter)
        {
            return word.Count(c => c == targetLetter);
        }

        #endregion
    }
}
