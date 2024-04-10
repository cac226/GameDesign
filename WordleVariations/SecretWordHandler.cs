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
    internal class SecretWordHandler 
    {
        IGetWords wordGetter;

        public SecretWordHandler(IGetWords wordGetter)
        {
            this.wordGetter = wordGetter;
        }

        public SecretWord GetRandomFiveLetterWord()
        {
            Random random = new Random();
            int index = random.Next(wordGetter.GetFiveLetterWords().Length);
            return new SecretWord(wordGetter.GetFiveLetterWords()[index]);
        }

        public bool IsValidFiveLetterWord(string word)
        {
            return wordGetter.GetFiveLetterWords().Contains(word);
        }
    }
}
