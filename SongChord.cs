using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidiChord
{
    public class SongItem
    {
        public enum SongItemType { BAR_CHORD, BEAT_CHORD, CHANGE_INSTRUMENT };
        
        public SongItemType Timing;
        public string Data;
        public int ParserPosition;
        public int BeatIndex;

        public override string ToString()
        {
            return string.Format("{0}: {1} [{2}]", BeatIndex, Data, Timing.ToString());
        }

    }
}
