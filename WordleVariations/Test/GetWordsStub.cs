using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleVariations.Test
{
    internal class GetWordsStub : IGetWords
    {
        private string[] words;
        public GetWordsStub(string[] words)
        {
            this.words = words;
        }

        public string[] GetFiveLetterWords()
        {
            return words;
        }
    }
}
