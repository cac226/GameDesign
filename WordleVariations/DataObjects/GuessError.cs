using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleVariations.DataObjects
{
    internal enum GuessError
    {
        NONE = 0,
        INVALID_WORD = 1,
        REPEAT_GUESS = 2,
    }
}
