using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCollections.Utilities
{
    public class StringUtilities
    {

        // Refactored code from Browse Controller for Sprint 3 testing
        public static string[] SplitBySpace(string keywords)
        {
            string[] keywordArray = keywords.Split(" ");

            return (keywordArray);
        }
    }
}
