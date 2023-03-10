using System;
using System.Collections.Generic;
using System.Text;
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

        private readonly Dictionary<string, string[]> _refChordNotes;

        internal class MidiChord
        {
            internal List<ChannelMessage> NotesOn = new List<ChannelMessage>();
            internal List<ChannelMessage> NotesOff = new List<ChannelMessage>();
        }

        protected ChannelMessageBuilder _metronomeFirstBeatInstrument;
        protected ChannelMessageBuilder _metronomeBeatInstument;
        protected ChannelMessageBuilder _metronomeNoteOn;
        protected ChannelMessageBuilder _metronomeNoteOff;

        public int SongInstrument { get; set; }
        public int MetronomeFirstBeatInstrument { get; set; }
        public int MetronomeInstrument { get; set; }
        public int BeatsPerMinute { get; set; }
        public int MetronomeMidiChannel { get; set; }
        public int SongMidiChannel { get; set; }
        public bool EnableMetronome { get; set; }

        public ChordPlayer(Dictionary<string, string[]> chordNotes)
        {
            _refChordNotes = chordNotes;

            BeatsPerMinute = 60; // 1 beat (quarter note) per seconds, that a bar of 4 seconds.
            SongInstrument = 1;
            MetronomeFirstBeatInstrument = 122;
            MetronomeInstrument = 123;
            MetronomeMidiChannel = 1;
            SongMidiChannel = 0;
            EnableMetronome = true;

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
            UpdateMetronomeNotes();
        }

        protected void UpdateMetronomeNotes()
        {
            // create notes for metronome
            int metronomeVolume = 75;
            int metronomeNote = 60;
            int metronomeDelay = 10;

            _metronomeFirstBeatInstrument = new ChannelMessageBuilder();
            _metronomeFirstBeatInstrument.Command = ChannelCommand.ProgramChange;
            _metronomeFirstBeatInstrument.MidiChannel = MetronomeMidiChannel;
            _metronomeFirstBeatInstrument.Data1 = MetronomeFirstBeatInstrument;
            _metronomeFirstBeatInstrument.Build();

            _metronomeBeatInstument = new ChannelMessageBuilder();
            _metronomeBeatInstument.Command = ChannelCommand.ProgramChange;
            _metronomeBeatInstument.MidiChannel = MetronomeMidiChannel;
            _metronomeBeatInstument.Data1 = MetronomeInstrument;
            _metronomeBeatInstument.Build();

            _metronomeNoteOn = new ChannelMessageBuilder();
            _metronomeNoteOn.Command = ChannelCommand.NoteOn;
            _metronomeNoteOn.MidiChannel = MetronomeMidiChannel;
            _metronomeNoteOn.Data1 = metronomeNote;
            _metronomeNoteOn.Data2 = metronomeVolume;
            _metronomeNoteOn.Build();

            _metronomeNoteOff = new ChannelMessageBuilder();
            _metronomeNoteOff.Command = ChannelCommand.NoteOff;
            _metronomeNoteOff.MidiChannel = MetronomeMidiChannel;
            _metronomeNoteOff.Data1 = metronomeNote;
            _metronomeNoteOff.Data2 = metronomeVolume;
            _metronomeNoteOff.Build();

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
                        builder.MidiChannel = 0;
                        builder.Data1 = data;
                        builder.Data2 = 127;
                        builder.Build();

                        newChord.NotesOn.Add(builder.Result);

                        builder.Command = ChannelCommand.NoteOff;
                        builder.Build();

                        newChord.NotesOff.Add(builder.Result);
                    }
                }
            }
            else
            {
                throw (new Exception("Unknown chord: '" + chord + "'"));
            }
            return newChord;
        }

        protected ChannelMessage GetMidiProgram(int instrument)
        {
            ChannelMessageBuilder builder = new ChannelMessageBuilder();
            builder.Command = ChannelCommand.ProgramChange;
            builder.MidiChannel = 0;
            builder.Data1 = instrument;
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

        protected int GetBeatDelay(int beatsPerMinute)
        {
            int quarterDelay = (60 * 1000) / beatsPerMinute;
            int beatDelay = quarterDelay / 4;

            return beatDelay * 10;

        }

        protected string[] GetNotesOfChord(string chord)
        {
            if (_refChordNotes.ContainsKey(chord))
            {
                return _refChordNotes[chord];
            }
            return null;
        }

        protected int GetMaxIndex()
        {
            int max = 0;

            foreach (var entry in _data)
            {
                if (entry.Timing == SongItem.SongItemType.BAR_CHORD)
                {
                    max = Math.Max(max, entry.BeatIndex + 3);
                }
                else
                {
                    max = Math.Max(max, entry.BeatIndex);
                }
            }

            return max;
        }

    }
}
