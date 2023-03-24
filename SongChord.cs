using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidiChord
{
    // A single data item that can be converted into MIDI
    public class SongItem
    {
        public enum SongItemType { MEASURE_CHORD, BEAT_CHORD, CHANGE_INSTRUMENT, DRUM_PATTERN, CHANGE_TEMPO };
        
        public SongItemType Type;
        public string Data;
        public string Part;
        public int ParserPosition;
        public int BeatIndex;
        public GeneralMidiInstrument Instrument;

        public override string ToString()
        {
            switch(Type)
            {
                case SongItemType.CHANGE_INSTRUMENT:
                    return string.Format("{0}: {1} [{2}]", BeatIndex, "Instrument", Instrument.ToString());

                default:
                    return string.Format("{0}: {1} [{2}]", BeatIndex, Data, Type.ToString());
            }
        }
    }
}
