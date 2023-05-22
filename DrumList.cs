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
                ["B"] = 35,     // acoustic base
                ["S"] = 38,     // acoustic snare
                ["LT"] = 45,    // low tom
                ["HT"] = 50     // high tom
            };
        }
    }
}
