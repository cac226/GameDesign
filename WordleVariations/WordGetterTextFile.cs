using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WordleVariations
{
    internal class WordGetterTextFile : IGetWords
    {
        private static readonly string FIVE_LETTER_WORD_FILE_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"FiveLetterWords.txt");
        private readonly string[] fiveLetterWords;

        private WordGetterTextFile(string[] fiveLetterWords)
        {
            this.fiveLetterWords = fiveLetterWords;
        }

        public static WordGetterTextFile Create()
        {
            List<string> words = new List<string>();

            using (StreamReader reader = new StreamReader(FIVE_LETTER_WORD_FILE_PATH))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    words.Add(line);
                }
            }

            return new WordGetterTextFile(words.ToArray());
        }

        public string[] GetFiveLetterWords()
        {
            return fiveLetterWords;
        }
    }
}
