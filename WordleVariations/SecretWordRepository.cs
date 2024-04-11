using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WordleVariations
{
    /// <summary>
    /// Creates and validates possible secret words
    /// </summary>
    internal class SecretWordRepository 
    {
        IGetWords wordGetter;

        public SecretWordRepository(IGetWords wordGetter)
        {
            this.wordGetter = wordGetter;
        }

        public SecretWord GetRandomFiveLetterWord()
        {
            SecretWord[] result = GetRandomFiveLetterWords(1);
            return result[0];
        }

        public SecretWord[] GetRandomFiveLetterWords(int numWords)
        {
            int[] indices = getDistinctIndices(numWords);

            SecretWord[] result = new SecretWord[numWords];

            for(int i = 0; i < numWords; i++)
            {
                result[i] = new SecretWord(wordGetter.GetFiveLetterWords()[indices[i]]);
            }

            return result;
        }

        private int[] getDistinctIndices(int numWords)
        {
            Random random = new Random();
            int numPossibleWords = wordGetter.GetFiveLetterWords().Length;

            if (numWords > numPossibleWords)
            {
                // If we don't have enough words to support X secret words, it's likely a problem with the list of valid words gotten (as opposed to the number of secret words requested being too high)
                throw new InvalidDataException("Not enough valid words to generate " + numWords + " distinct secret words. Number of possible words loaded: " + numPossibleWords);
            }

            List<int> possibleIndices = Enumerable.Range(0, numPossibleWords).ToList();

            List<int> indices = new List<int>(numWords);
            int num;

            for (int i = 0; i < numWords; i++)
            {
                do
                {
                    num = possibleIndices[random.Next(possibleIndices.Count)];
                } while (indices.Contains(num));
                indices.Add(num);
                possibleIndices.Remove(num);
            }

            return indices.ToArray();
        }

        public bool IsValidFiveLetterWord(string word)
        {
            return wordGetter.GetFiveLetterWords().Contains(word);
        }
    }
}
