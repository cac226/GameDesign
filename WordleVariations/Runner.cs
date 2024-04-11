﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordleVariations.DataObjects;

namespace WordleVariations
{
    /*
     TODO: include a "you got this in X guesses" 
     TODO: add "give up" option 
     TODO: don't let users repeat guesses 
     */
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
                "while correct letters in the wrong location will be printed as lowercase letters.");

            while(playAgain)
            {
                Console.Write("Guess: ");
                string guess = Console.ReadLine();

                LetterResponse[] result;
                GuessResponseData response = instance.MakeGuess(guess);
                if (response.WasLastGuessValid())
                {
                    printResponse(guess, response);

                    if (response.HasWon())
                    {
                        wonGame(response);
                    }
                } else
                {
                    Console.WriteLine("Not a valid guess!");
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
            int highestGuessWord = winningResponse.GuessCount();

            Console.WriteLine("You won with " + highestGuessWord + " guesses! Play again? (Y/N)");
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