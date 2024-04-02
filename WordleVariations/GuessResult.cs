using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleVariations
{
    internal enum GuessResult
    {
        CORRECT = 0,
        INCORRECT = 1,
        RIGHT_LETTER_WRONG_LOCATION = 2,
    }
}
