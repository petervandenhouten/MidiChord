using System;
using System.Collections.Generic;
using System.IO;

namespace MidiChord
{
    public class DrumList
    {
        private Dictionary<string, int> _drumNotes;
        const string drumsFilename = @"drums.txt";

        public DrumList()
        {
            bool loaded = false;
            if (File.Exists(drumsFilename))
            {
                loaded = LoadDrums(drumsFilename);
            }
            if (!loaded)
            {
                _drumNotes = getNotesOfDrums();
            }
        }

        private bool LoadDrums(string drumsFilename)
        {
            throw new NotImplementedException();
        }

        public int GetDrum(string drum)
        {
            if (ContainsDrum(drum))
            {
                return _drumNotes[drum];
            }
            return -1;
        }

        public bool ContainsDrum(string chord)
        {
            if (string.IsNullOrEmpty(chord)) return false;
            return _drumNotes.ContainsKey(chord);
        }

        private Dictionary<string, int> getNotesOfDrums()
        {
            return new Dictionary<string, int>
            {
                ["b"] = 35,     // acoustic base drum
                ["B"] = 36,     // base drum

                ["s"] = 38,     // acoustic snare
                ["S"] = 40,     // snare

                ["f"] = 41,     // low floor tom
                ["F"] = 43,      // high floor tom
                ["t"] = 45,      // low tom
                ["T"] = 50,     // high tom
                ["m"] = 47,      // low mid tom
                ["M"] = 48,      // hi mid tom

                ["C"] = 49,    // Crash cymbal
                ["c"] = 51,    // Ride cymbal
                ["SC"] = 51,    // Splash cymbal

                ["h"] = 42,   // close hi-hat
                ["H"] = 46,   // open hi-hat

                ["w"] = 77,  // low wood-block
                ["W"] = 76,  // high wood-block
                ["st"] = 37,  // side stick

                ["A"] = 54, // tambourine

            };
        }
    }
}
