using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleVariations
{
    internal interface IGetWords
    {
        public SecretWord GetRandomFiveLetterWord();
    }
}
