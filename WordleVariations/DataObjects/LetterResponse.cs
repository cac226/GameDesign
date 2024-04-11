using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleVariations.DataObjects
{
    internal enum LetterResponse
    {
        CORRECT = 0,
        INCORRECT = 1,
        RIGHT_LETTER_WRONG_LOCATION = 2,
    }
}
