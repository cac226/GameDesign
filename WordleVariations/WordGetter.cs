using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WordleVariations
{
    internal class WordGetter : IGetWords
    {
        private static readonly string FIVE_LETTER_WORD_FILE_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"FiveLetterWords.txt");
        private readonly string[] fiveLetterWords;

        private WordGetter(string[] fiveLetterWords)
        {
            this.fiveLetterWords = fiveLetterWords;
        }

        public static WordGetter CreateFromTextFile()
        {
            List<string> words = new List<string>();

            using(StreamReader reader = new StreamReader(FIVE_LETTER_WORD_FILE_PATH))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    words.Add(line);
                }
            }

            return new WordGetter(words.ToArray());
        }

        public SecretWord GetRandomFiveLetterWord()
        {
            Random random = new Random();
            int index = random.Next(fiveLetterWords.Length);
            return new SecretWord(fiveLetterWords[index]);
        }

        public bool IsValidFiveLetterWord(string word)
        {
            return fiveLetterWords.Contains(word);
        }
    }
}
