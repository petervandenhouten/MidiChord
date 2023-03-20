using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiChord
{
    internal class ChordLineDetector
    {
        private readonly string line;
        private readonly char[] wordSeperators = { ' ', ',' };
        internal ChordLineDetector(string txt)
        {
            line = txt;
        }
        internal bool isChords()
        {
            string[] words = line.Trim().Split(wordSeperators, StringSplitOptions.RemoveEmptyEntries);
            int maxLength = findMaxLengthOfWords(words);

            if (maxLength <= 2)
            {
                return true;
            }
            else if (maxLength <= 4)
            {
                return true;
            }

            // Check in the words are (known) chords

            return false;
        }

        private int findMaxLengthOfWords(string[] words)
        {
            int maxLength = 0;

            foreach(var word in words) 
            {
                if ( word.Length > maxLength)
                {
                    maxLength = word.Length;
                }
            }

            return maxLength;
        }

        internal bool isSongText()
        {
            return !isChords();
        }

    }
}
