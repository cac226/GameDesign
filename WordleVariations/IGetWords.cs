using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleVariations
{
    internal interface IGetWords
    {
        public string[] GetFiveLetterWords(); // must be in all caps TODO: add way to guarantee this (maybe it's a list of secret words and not strings?) 
    }
}
