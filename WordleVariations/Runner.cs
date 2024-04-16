using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordleVariations.DataObjects;

namespace WordleVariations
{
    public class Runner {

        private MultiGameInstance instance;
        bool playAgain;

        public Runner()
        {
            instance = MultiGameInstance.Create(WordGetterTextFile.Create(), 2);
            playAgain = true;
        }

        public void RunGame()
        {
            instance.SetupNewGame();

            Console.WriteLine("Try to guess the secret word in as few guesses as possible! " +
                "Correct letters correctly placed will be printed as capital letters, " +
                "while correct letters in the wrong location will be printed as lowercase letters. " + 
                "Enter -1 to reveal answers.");

            while(playAgain)
            {
                Console.Write("Guess: ");
                string userInput = Console.ReadLine();

                if(userInput.Equals("-1"))
                {
                    gaveUpGame();
                } else
                {
                    LetterResponse[] result;
                    GuessResponseData response = instance.MakeGuess(userInput);
                    if (response.WasLastGuessValid())
                    {
                        printResponse(userInput, response);

                        if (response.HasWon())
                        {
                            wonGame(response);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not a valid guess!");
                    }
                }
                
            }
        }

        private void printResponse(string guess, GuessResponseData response)
        {
            foreach(WordFeedback word in response.GetWordFeedback())
            {
                string responsePrintable = getWordGuessString(word.GetLetterResponse(), guess);
                Console.Write(responsePrintable + "\t\t");
            }
            Console.WriteLine();
        }

        private void wonGame(GuessResponseData winningResponse)
        {
            int highestGuessWord = instance.GetGuessCount();

            Console.WriteLine("You won with " + highestGuessWord + " guesses!");
            checkPlayAgain();
        }

        private void gaveUpGame()
        {
            printAnswers();
            Console.WriteLine("\nYou lost");
            checkPlayAgain();
        }

        private void checkPlayAgain()
        {
            Console.WriteLine("Play again? (Y/N)");
            string userResponse = Console.ReadLine();

            if (string.Equals(userResponse, "N", StringComparison.OrdinalIgnoreCase))
            {
                playAgain = false;
            }
            else
            {
                instance.SetupNewGame();
            }
        }

        private void printAnswers()
        {
            string[] words = instance.RevealAllWords();
            foreach (string word in words)
            {
                Console.Write(word + "\t\t");
            }
        }

        private static string getWordGuessString(LetterResponse[] guessResult, string guess)
        {
            if(guessResult.Length == 0)
            {
                return "     ";
            }
            char[] result = new char[guess.Length];
            for(int i = 0; i < guess.Length; i++)
            {
                switch(guessResult[i])
                {
                    case LetterResponse.CORRECT:
                        result[i] = guess.ToUpper()[i];
                        break;
                    case LetterResponse.RIGHT_LETTER_WRONG_LOCATION:
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