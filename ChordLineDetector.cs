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
        private readonly char[] wordSeperators = { ' ', ',', '[', ']' };
        internal ChordLineDetector(string txt)
        {
            line = txt;
        }
        internal bool isChords()
        {
            string[] words = line.Trim().Split(wordSeperators, StringSplitOptions.RemoveEmptyEntries);
            int maxLength = findMaxLengthOfWords(words);

            // When all words are at maximum 2 characters long, then it is a chord line
            if (maxLength <= 2)
            {
                return true;
            }

            int chordCount = 0;
            int wordcount = words.Length;
            var chordList = new ChordList();
            foreach(var word in words)
            {
                var possibleChord = word.Trim();

                if (possibleChord.IndexOfAny( new char[] { '{', '}', '|', '/', '*' }) >= 0 )
                {
                    wordcount--;
                }

                //possibleChord = possibleChord.Replace("[", "");
                //possibleChord = possibleChord.Replace("]", "");

                if (chordList.ContainsChord(possibleChord))
                {
                    chordCount++;
                }
            }

            // Check in the words are (known) chords
            int realwords = wordcount - chordCount;
            if (realwords < 1)
            {
                return true;
            }

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
