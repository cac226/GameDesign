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
        private bool hasBeenRevealed;

        public SecretWord(string word)
        {
            secretWord = word.ToUpper();
            hasBeenRevealed = false;
        }

        public string PeekWord()
        {
            return secretWord;
        }

        public string RevealWord()
        {
            hasBeenRevealed = true;
            return secretWord;
        }

        public bool HasBeenRevealed()
        {
            return hasBeenRevealed;
        }
    }
}
