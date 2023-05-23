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
        public enum SongItemType { MEASURE_CHORD, BEAT_CHORD, CHANGE_INSTRUMENT, START_DRUM_PATTERN, STOP_DRUM_PATTERN, CHANGE_TEMPO };
        public enum SongItemInstrument {  CHORD_INSTRUMENT, DRUM_LEFT, DRUM_RIGHT, DRUM_FOOT, RIFF_INSTRUMENT };

        public SongItemType Type;
        public SongItemInstrument ItemInstrument;
        public string Data;
        public string Part;
        public int LineNumber;
        public int BeatIndex;
        public GeneralMidiInstrument Instrument;

        public override string ToString()
        {
            switch(Type)
            {
                case SongItemType.CHANGE_INSTRUMENT:
                    return string.Format("{0}: {1} [{2}] (line {3})", BeatIndex, "INSTRUMENT", Instrument.ToString(), LineNumber);

                case SongItemType.START_DRUM_PATTERN:
                    return string.Format("{0}: {1} [{2}] (line {3})", BeatIndex, "START DRUM", ItemInstrument.ToString(), LineNumber);

                case SongItemType.STOP_DRUM_PATTERN:
                    return string.Format("{0}: {1} [{2}] (line {3})", BeatIndex, "STOP DRUM", ItemInstrument.ToString(), LineNumber);

                default:
                    return string.Format("{0}: {1} [{2}] (line {3})", BeatIndex, Data, Type.ToString(), LineNumber);
            }
        }
    }
}
