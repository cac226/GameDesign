using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordleVariations {
    /*
     TODO: include a "you got this in X guesses" 
     TODO: add instructions 
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

                LetterType[] result;
                GuessResponseData response = instance.MakeGuess(guess);
                if(response.WasLastGuessValid())
                {
                    string guessPrintable = getWordGuessString(response.GetLetterData(), guess);
                    Console.WriteLine(guessPrintable);

                    if (instance.IsSecretWord(guess))
                    {
                        Console.WriteLine("You won with " + response.GuessCount() + " guesses! Play again? (Y/N)");
                        string userResponse = Console.ReadLine();

                        if (string.Equals(userResponse, "N", StringComparison.OrdinalIgnoreCase))
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

        private static string getWordGuessString(LetterType[] guessResult, string guess)
        {
            char[] result = new char[guess.Length];
            for(int i = 0; i < guess.Length; i++)
            {
                switch(guessResult[i])
                {
                    case LetterType.CORRECT:
                        result[i] = guess.ToUpper()[i];
                        break;
                    case LetterType.RIGHT_LETTER_WRONG_LOCATION:
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