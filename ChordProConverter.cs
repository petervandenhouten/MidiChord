using System.Collections.Generic;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace MidiChord
{
    internal class ChordProConverter
    {
        List<string> output = new List<string>();

        internal string[] Convert(string[] lines)
        {
            output.Clear();

            foreach (var line in lines)
            {
                string str = line;

                str = str.Replace("{start_of_verse}", "{verse}");

                // {start_of_verse: X} => {X}
                str = str.Replace("{start_of_verse: ", "{");

                output.Add(str);
            }

            return output.ToArray();
        }
    }
}
