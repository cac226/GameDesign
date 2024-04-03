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
            secretWord = word.ToUpper();
        }

        public GuessResult[] GuessWord(string guess)
        {
            string guessUpper = guess.ToUpper();
            GuessResult[] result = guessWord(guessUpper);

            return result;
        }

        public string RevealWord()
        {
            return secretWord;
        }

        private GuessResult[] guessWord(string guess)
        {
            GuessResult[] result = new GuessResult[guess.Length];

            for (int i = 0; i < guess.Length; i++)
            {
                char guessedLetter = guess[i];

                if (guessedLetter == secretWord[i])
                {
                    result[i] = GuessResult.CORRECT;

                }
                else if (!containsLetter(guessedLetter))
                {
                    result[i] = GuessResult.INCORRECT;

                }
                else
                {
                    // if a secret word has 1 of a letter, but the guess has 2 of that letter, we only want one of those guess letters to be marked as correct 
                    // e.g. if the secret word is "FACTS" and a user guesses "CHESS" the first S should be marked as incorrect and the second marked as correct 

                    int secretWordLetterCount = countOccurances(secretWord, guessedLetter);
                    int guessLetterCount = countOccurances(guess, guessedLetter);

                    if(guessLetterCount > secretWordLetterCount)
                    {
                        int truncatedGuessLetterCount = countOccurances(guess.Substring(0, i), guessedLetter); 
                        int correctlyPlaced = countCorrectOccurances(guess, guessedLetter);

                        if (truncatedGuessLetterCount >= secretWordLetterCount - correctlyPlaced)
                        {
                            result[i] = GuessResult.INCORRECT;
                        } else
                        {
                            result[i] = GuessResult.RIGHT_LETTER_WRONG_LOCATION;
                        }

                    } else
                    {
                        result[i] = GuessResult.RIGHT_LETTER_WRONG_LOCATION;
                    }
                }
            }

            return result;
        }

        private bool containsLetter(char c)
        {
            return secretWord.Contains(c);
        }

        private int countCorrectOccurances(string guess, char letter)
        {
            int count = 0;

            for(int i = 0; i < guess.Length; i++)
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

        
    }
}
