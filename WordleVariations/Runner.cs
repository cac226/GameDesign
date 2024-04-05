using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordleVariations {
    /*
     TODO: include a "you got this in X guesses" 
     TODO: add instructions 
     TODO: seperate the game logic from input/output 
     */
    public class Runner {
        public static void RunGame()
        {
            GameInstance instance = new GameInstance(WordGetter.CreateFromTextFile());
            instance.SetupNewGame();
            bool playAgain = true;

            while(playAgain)
            {
                Console.Write("Guess: ");
                string guess = Console.ReadLine();

                GuessResult[] result;
                bool isValidGuess = instance.TryMakeGuess(guess, out result);
                if(isValidGuess)
                {
                    string guessPrintable = getWordGuess(result, guess);
                    Console.WriteLine(guessPrintable);

                    if (instance.IsSecretWord(guess))
                    {
                        Console.WriteLine("You win! Play again? (Y/N)");
                        string response = Console.ReadLine();

                        if (string.Equals(response, "N", StringComparison.OrdinalIgnoreCase))
                        {
                            playAgain = false;
                        } else
                        {
                            instance.SetupNewGame();
                        }
                    }
                } else
                {
                    Console.WriteLine("not a valid word!");
                }
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
                        result[i] = guess.ToUpper()[i];
                        break;
                    case GuessResult.RIGHT_LETTER_WRONG_LOCATION:
                        result[i] = guess.ToLower()[i];
                        break;
                    default:
                        result[i] = '_';
                        break;
                }
            }
            return new string(result);
        }
    }
}