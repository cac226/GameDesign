using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordleVariations { 
    public class Program {
        public static void main(string[] args)
        {
            SecretWord secretWord = new SecretWord("cheese");

            bool playAgain = true;

            while(playAgain)
            {
                Console.Write("Guess: ");
                string guess = Console.ReadLine();

                GuessResult[] result = secretWord.GuessWord(guess);
                string guessPrintable = getWordGuess(result, guess);
                Console.WriteLine(guessPrintable);

            }
        }

        private static string getWordGuess(GuessResult[] guessResult, string guess)
        {
            char[] result = new char[guess.Length];
            for(int i = 0; i < guess.Length; i++)
            {
                switch(guessResult[i])
                {
                    case GuessResult.CORRECT:
                        result[i] = guess[i];
                        break;
                    case GuessResult.RIGHT_LETTER_WRONG_LOCATION:
                        result[i] = guess.ToLower()[i];
                        break;
                    default:
                        result[i] = '_';
                        break;
                }
            }
            return result.ToString();
        }
    }
}