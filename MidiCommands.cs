using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiChord
{
    public class MidiCommands
    {
        public readonly List<IMidiMessage> MetaData = new List<IMidiMessage>(); // instrument, tempo, ...
        public readonly List<IMidiMessage> Chords = new List<IMidiMessage>();
        public readonly List<IMidiMessage> Metronome = new List<IMidiMessage>();
        public readonly List<IMidiMessage> Drums = new List<IMidiMessage>();
        public readonly List<IMidiMessage> Bass = new List<IMidiMessage>();
        public readonly List<IMidiMessage> Riffs = new List<IMidiMessage>();

        public bool EndOfSong { get; set; }
        public string ErrorMessage { get; set; }
        public int ParserLine { get; set; }
        public string Part { get; set; }
        public string ChordName { get; set; }
        
    }
}
