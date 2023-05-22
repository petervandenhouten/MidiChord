using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using Sanford.Multimedia.Midi;

namespace MidiChord
{
    public class ChordPlayer
    {
        // Base class internal data
        protected List<SongItem> _data = null;
        protected int _beatIndex = 0;
        protected int _lastBeatIndex = 0;
        internal MidiChord _lastMidiChord = null;
        protected DrumPattern[] _drumPatterns = new DrumPattern[3];
        //internal MidiDrum _lastMidiDrumLeft = null;
        //internal MidiDrum _lastMidiDrumRight = null;
        //internal MidiDrum _lastMidiDrumFoot = null;
        internal MidiRiff _lastMidiRiff = null;

        public enum DrumEvent { START_PATTERN, STOP_PATTERN };
        public enum DrumType { LEFT_HAND, RIGHT_HAND, FOOT };

        private readonly ChordList _chordList;
        private readonly DrumList _drumList;

        internal class MidiChord
        {
            internal string Name;
            internal List<ChannelMessage> NotesOn = new List<ChannelMessage>();
            internal List<ChannelMessage> NotesOff = new List<ChannelMessage>();
        }

        protected class DrumPattern
        {
            internal string Name;
            internal DrumType Type;
            internal DrumEvent Event;
            internal int TypeIndex;
            internal List<ChannelMessage> NotesOn = new List<ChannelMessage>();
            internal List<ChannelMessage> NotesOff = new List<ChannelMessage>();
        }

        internal class MidiRiff
        {
            internal string Name;
            internal List<ChannelMessage> NotesOn = new List<ChannelMessage>();
            internal List<ChannelMessage> NotesOff = new List<ChannelMessage>();
        }

        protected ChannelMessage _metronomeFirstBeatInstrument;
        protected ChannelMessage _metronomeBeatInstument;
        protected ChannelMessage _metronomeNoteOn;
        protected ChannelMessage _metronomeNoteOff;
        protected ChannelMessage _drumInstument;
        protected ChannelMessage _drumNoteOff;

        protected MetaMessage _chordMetaText;
        protected MetaMessage _metronomeMetaText;
        protected MetaMessage _drumMetaText;

        public GeneralMidiInstrument SongInstrument { get; set; }
        public GeneralMidiInstrument MetronomeFirstBeatInstrument { get; set; }
        public GeneralMidiInstrument MetronomeInstrument { get; set; }
        public int BeatsPerMinute { get; set; }
        public int MetronomeMidiChannel { get; set; }
        public int DrumMidiChannel { get; set; }
        public int ChordMidiChannel { get; set; }
        public bool EnableMetronome { get; set; }
        public int DrumVolume { get; set; }
        public int ChordVolume { get; set; }
        public int MetronomeVolume { get; set; }

        public ChordPlayer(ChordList chordNotes, DrumList drumNotes)
        {
            _chordList = chordNotes;
            _drumList = drumNotes;

            BeatsPerMinute = 60;
            SongInstrument = GeneralMidiInstrument.AcousticGrandPiano;
            MetronomeFirstBeatInstrument = GeneralMidiInstrument.Woodblock;
            MetronomeInstrument = GeneralMidiInstrument.Agogo;
            MetronomeMidiChannel = 1;
            ChordMidiChannel = 0;
            EnableMetronome = true;
            DrumMidiChannel = 9;
            DrumVolume = 127;
            ChordVolume = 127;
            MetronomeVolume = 60;
        }

        public void SetSong(List<SongItem> song)
        {
            _data = song;
            Reset();
            _lastBeatIndex = GetMaxIndex();
        }


        protected void Reset()
        {
            _beatIndex = 0;
            createCommonMidiMessages();
            ResetDrumPatterns();
        }

        private void ResetDrumPatterns()
        {
            _drumPatterns[0] = null;
            _drumPatterns[1] = null;
            _drumPatterns[2] = null;
        }

        protected void createCommonMidiMessages()
        {
            // create notes for metronome
            int metronomeNote = 60;
            int drumInstrument = 1;

            var metaBuilder = new MetaTextBuilder();
            metaBuilder.Type = MetaType.TrackName;
            metaBuilder.Text = "Chords";
            metaBuilder.Build();
            _chordMetaText = metaBuilder.Result;

            metaBuilder = new MetaTextBuilder();
            metaBuilder.Type = MetaType.TrackName;
            metaBuilder.Text = "Metronome";
            metaBuilder.Build();
            _metronomeMetaText = metaBuilder.Result;

            metaBuilder = new MetaTextBuilder();
            metaBuilder.Type = MetaType.TrackName;
            metaBuilder.Text = "Drums";
            metaBuilder.Build();
            _drumMetaText = metaBuilder.Result;

            var builder = new ChannelMessageBuilder();
            builder.Command = ChannelCommand.ProgramChange;
            builder.MidiChannel = MetronomeMidiChannel;
            builder.Data1 = (int)MetronomeFirstBeatInstrument;
            builder.Build();
            _metronomeFirstBeatInstrument = builder.Result;

            // MIDI event for normal channel (non-drum channel 10)
            builder = new ChannelMessageBuilder();
            builder.Command = ChannelCommand.ProgramChange;
            builder.MidiChannel = MetronomeMidiChannel;
            builder.Data1 = (int)MetronomeFirstBeatInstrument;
            _metronomeBeatInstument = builder.Result;

            builder = new ChannelMessageBuilder();
            builder.Command = ChannelCommand.ProgramChange;
            builder.MidiChannel = MetronomeMidiChannel;
            builder.Data1 = (int)MetronomeInstrument;
            builder.Build();
            _metronomeBeatInstument = builder.Result;

            builder = new ChannelMessageBuilder();
            builder.Command = ChannelCommand.NoteOn;
            builder.MidiChannel = MetronomeMidiChannel;
            builder.Data1 = metronomeNote;
            builder.Data2 = MetronomeVolume;
            builder.Build();
            _metronomeNoteOn = builder.Result;

            builder = new ChannelMessageBuilder();
            builder.Command = ChannelCommand.NoteOff;
            builder.MidiChannel = MetronomeMidiChannel;
            builder.Data1 = metronomeNote;
            builder.Data2 = MetronomeVolume;
            builder.Build();
            _metronomeNoteOff = builder.Result;

            builder = new ChannelMessageBuilder();
            builder.Command = ChannelCommand.ProgramChange;
            builder.MidiChannel = DrumMidiChannel;
            builder.Data1 = 80; // ??
            builder.Data2 = 100; // ??
            builder.Build();
            _drumInstument = builder.Result;

            builder = new ChannelMessageBuilder();
            builder.Command = ChannelCommand.NoteOff;
            builder.MidiChannel = DrumMidiChannel;
            builder.Data1 = metronomeNote;
            builder.Data2 = MetronomeVolume;
            builder.Build();
            _drumNoteOff = builder.Result;
        }

        internal MidiChord GetMidiChord(string chord)
        {
            MidiChord newChord = new MidiChord();

            string[] notesOfChord = GetNotesOfChord(chord);

            if (notesOfChord != null)
            {
                foreach (string note in notesOfChord)
                {
                    int data = GetMidiData(note);

                    if (data > 0)
                    {
                        ChannelMessageBuilder builder = new ChannelMessageBuilder();
                        builder.Command = ChannelCommand.NoteOn;
                        builder.MidiChannel = ChordMidiChannel;
                        builder.Data1 = data;
                        builder.Data2 = ChordVolume;
                        builder.Build();

                        newChord.NotesOn.Add(builder.Result);

                        builder.Command = ChannelCommand.NoteOff;
                        builder.Build();

                        newChord.NotesOff.Add(builder.Result);
                        newChord.Name = chord;
                    }
                }
            }
            else
            {
                return null;
            }
            return newChord;
        }

        protected DrumPattern GetDrumPattern(string drumpattern)
        {
            char[] drumSeperators = { ' ' };
            var drums = drumpattern.Split(drumSeperators, StringSplitOptions.RemoveEmptyEntries);
            int nrDrums = drums.Length;

            int drumNote = 1;

            if (nrDrums>=1 && nrDrums<=4 )
            {
                DrumPattern newDrum = new DrumPattern();

                foreach (var drum in drums)
                {
                    int data = 0;

                    if (!drum.Contains("."))
                    {
                        data = GetMidiDrumNote(drum);

                        if (data <= 0)
                        {
                            return null;
                        }
                    }

                    if (data > 0)
                    {
                        var builder = new ChannelMessageBuilder();
                        builder.Command = ChannelCommand.NoteOn;
                        builder.MidiChannel = DrumMidiChannel;
                        builder.Data1 = data;
                        builder.Data2 = DrumVolume;
                        builder.Build();

                        newDrum.NotesOn.Add(builder.Result);

                        builder = new ChannelMessageBuilder();
                        builder.Command = ChannelCommand.NoteOff;
                        builder.MidiChannel = DrumMidiChannel;
                        builder.Data1 = data;
                        builder.Data2 = DrumVolume;
                        builder.Build();

                        newDrum.NotesOff.Add(builder.Result);
                    }
                    else
                    {
                        newDrum.NotesOn.Add(null);
                    }
                }

                return newDrum;
            }
            return null;
        }

        private int GetMidiDrumNote(string drum)
        {
            return _drumList.GetDrum(drum);
        }

        protected ChannelMessage GetMidiProgram(GeneralMidiInstrument instrument)
        {
            ChannelMessageBuilder builder = new ChannelMessageBuilder();
            builder.Command = ChannelCommand.ProgramChange;
            builder.MidiChannel = 0;
            builder.Data1 = (int)instrument;
            builder.Build();

            return builder.Result;
        }

        protected int GetMidiData(string note)
        {
            switch (note)
            {
                case "C": return 60;
                case "C#": return 61;
                case "Db": return 61;
                case "D": return 62;
                case "D#": return 63;
                case "Eb": return 63;
                case "E": return 64;
                case "F": return 65;
                case "F#": return 66;
                case "Gb": return 66;
                case "G": return 67;
                case "G#": return 68;
                case "Ab": return 68;
                case "A": return 69;
                case "A#": return 70;
                case "Bb": return 70;
                case "B": return 71;
                default:
                    return 0;
            }

        }

        protected int GetBeatTimeInMs(int beatsPerMinute)
        {
            // BPM = 120
            // BPS = 120/60 = 2
            // BeatTime = 1/2 * 1000 ms

            float beatsPerSeconds = (float)beatsPerMinute / 60;
            float beatTime = 1000 * (1 / beatsPerSeconds);
            return (int)beatTime;
        }

        protected string[] GetNotesOfChord(string chord)
        {
            if (_chordList.ContainsChord(chord))
            {
                return _chordList.GetChord(chord);
            }
            return null;
        }

        protected int GetMaxIndex()
        {
            int max = 0;

            foreach (var entry in _data)
            {
                if (entry.Type == SongItem.SongItemType.MEASURE_CHORD)
                {
                    max = Math.Max(max, entry.BeatIndex + 3);
                }
                else
                {
                    max = Math.Max(max, entry.BeatIndex + 1);
                }
            }

            return max;
        }
        protected int getDrumIndex(SongItem.SongItemInstrument itemInstrument)
        {
            switch (itemInstrument)
            {
                case SongItem.SongItemInstrument.DRUM_LEFT: return 0;
                case SongItem.SongItemInstrument.DRUM_RIGHT: return 1;
                case SongItem.SongItemInstrument.DRUM_FOOT: return 2;
                default: return -1;
            }
        }


    }
}
