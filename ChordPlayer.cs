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
    // Converts (parsed) song items in midi commands
    // Does not communicate with midi devices or IO.
    public class ChordPlayer
    {
        // Base class internal data
        protected List<SongItem> _data = null;

        private MidiChord _lastMidiChord = null;

        internal DrumPattern[] _drumPatterns = new DrumPattern[3];
        internal MidiRiff _lastMidiRiff = null;
        internal BassPattern _bassPattern = null;

        public enum DrumEvent { START_PATTERN, STOP_PATTERN };
        public enum DrumType { LEFT_HAND, RIGHT_HAND, FOOT };

        private readonly ChordList _chordList;
        private readonly DrumList _drumList;
        private readonly MidiCommandFactory _midiFactory = new MidiCommandFactory();
        internal class MidiChord
        {
            internal string Name;
            internal List<ChannelMessage> NotesOn = new List<ChannelMessage>();
            internal List<ChannelMessage> NotesOff = new List<ChannelMessage>();
        }

        internal class DrumPattern
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

        internal class BassPattern
        {
            internal string Name;
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

        public int CurrentBeatIndex { get; private set; }

        public int LastBeatIndex { get; private set; }

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

        // Set this to true when saving to MIDI file
        public bool SendNoteOffForDrums { get; set; }

        public ChordPlayer(ChordList chordNotes, DrumList drumNotes)
        {
            _chordList = chordNotes;
            _drumList = drumNotes;

            BeatsPerMinute = 60;
            SongInstrument = GeneralMidiInstrument.AcousticGrandPiano;
            MetronomeFirstBeatInstrument = GeneralMidiInstrument.Woodblock; //  (GeneralMidiInstrument)31; 
            MetronomeInstrument = GeneralMidiInstrument.Agogo; //  (GeneralMidiInstrument)33; //  ;
            MetronomeMidiChannel = 9;
            MetronomeVolume = 60;
            ChordMidiChannel = 0;
            EnableMetronome = true;
            DrumMidiChannel = 9;
            DrumVolume = 127;
            ChordVolume = 127;
            MetronomeVolume = 60;
            SendNoteOffForDrums = false;

            createCommonMidiMessages();
        }

        public void SetSong(List<SongItem> song)
        {
            _data = song;
            Reset();
            LastBeatIndex = GetMaxIndex();
        }


        protected void Reset()
        {
            CurrentBeatIndex    = 0;
            _lastMidiChord      = null;
            resetDrumPatterns();
        }

        private void resetDrumPatterns()
        {
            _drumPatterns[0] = null;
            _drumPatterns[1] = null;
            _drumPatterns[2] = null;
        }

        private void createCommonMidiMessages()
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
            builder.Data2 = MetronomeVolume;
            builder.Build();
            _metronomeFirstBeatInstrument = builder.Result;

            // MIDI event for normal channel (non-drum channel 10)
            builder = new ChannelMessageBuilder();
            builder.Command = ChannelCommand.ProgramChange;
            builder.MidiChannel = MetronomeMidiChannel;
            builder.Data1 = (int)MetronomeFirstBeatInstrument;
            builder.Data2 = MetronomeVolume;
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
                        newChord.NotesOn .Add(_midiFactory.NoteOn (ChordMidiChannel, data, ChordVolume));
                        newChord.NotesOff.Add(_midiFactory.NoteOff(ChordMidiChannel, data));
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

        private DrumPattern getDrumPattern(string drumpattern)
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
                        newDrum.NotesOn. Add(_midiFactory.NoteOn(DrumMidiChannel, data, DrumVolume));
                        newDrum.NotesOff.Add(_midiFactory.NoteOff(DrumMidiChannel, data));
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

        protected int NextBeatIndex()
        {
            return ++CurrentBeatIndex;
        }

        // converts song items into grouped midi commands
        // ready to but send to output device or midi file
        public MidiCommands GetMidiCommandsAtBeat(int beatIndex)
        {
            string currentLabel = "";
            int currentParserLine = 0;

            var midi = new MidiCommands();

            // find ALL notes on the beat index
            foreach (var entry in _data)
            {
                if (entry.Type == SongItem.SongItemType.CHANGE_INSTRUMENT)
                {
                    midi.MetaData.Add(_midiFactory.Instrument(entry.Instrument));
                }
                else if (entry.Type == SongItem.SongItemType.MEASURE_CHORD ||
                         entry.Type == SongItem.SongItemType.BEAT_CHORD)
                {
                    MidiChord midiChord = GetMidiChord(entry.Data);

                    if (midiChord == null)
                    {
                        midi.ErrorMessage = "Unknown chord: '" + entry.Data + "'";
                    }

                    // Find on which beat we are (every tick we move a next beat)
                    if (entry.BeatIndex == beatIndex)
                    {
                        if (_lastMidiChord != null)
                        {
                            foreach (var midiEvent in _lastMidiChord.NotesOff)
                            {
                                midi.Chords.Add(midiEvent);
                            }
                        }
                        foreach (var midiEvent in midiChord.NotesOn)
                        {
                            midi.Chords.Add(midiEvent);
                        }
                        _lastMidiChord = midiChord;
                    }
                }
                else if (entry.Type == SongItem.SongItemType.START_DRUM_PATTERN)
                {
                    DrumPattern drumPattern = getDrumPattern(entry.Data);

                    if (drumPattern == null)
                    {
                        midi.ErrorMessage = "Invalid drum pattern: '" + entry.Data + "'";
                    }

                    if (entry.BeatIndex <= beatIndex)
                    {
                        // Activate pattern
                        int drumPatternIndex = getDrumIndex(entry.ItemInstrument);
                        if (drumPatternIndex >= 0)
                        {
                            _drumPatterns[drumPatternIndex] = drumPattern;
                        }
                    }
                }
                else if (entry.Type == SongItem.SongItemType.STOP_DRUM_PATTERN)
                {
                    if (entry.BeatIndex <= beatIndex)
                    {
                        // deactivate
                        int drumPatternIndex = getDrumIndex(entry.ItemInstrument);
                        if (drumPatternIndex >= 0)
                        {
                            _drumPatterns[drumPatternIndex] = null;
                        }
                    }
                }

                // Update part and line of the song
                if (entry.BeatIndex <= beatIndex)
                {
                    if (!string.IsNullOrEmpty(entry.Part))
                    {
                        currentLabel = entry.Part;
                    }
                    currentParserLine = Math.Max(entry.LineNumber, currentParserLine);
                }
            }

            // Generate DRUM midi messages
            if (beatIndex < LastBeatIndex)
            {
                for (int drum = 0; drum < 3; drum++)
                {
                    if (_drumPatterns[drum] != null)
                    {
                        int beat_in_measure = (beatIndex % 4);
                        if (beat_in_measure < _drumPatterns[drum].NotesOn.Count)
                        {
                            var midiEvent = _drumPatterns[drum].NotesOn[beat_in_measure];
                            if (midiEvent != null)
                            {
                                midi.Drums.Add(midiEvent);
                            }
                        }
                        if (SendNoteOffForDrums)
                        {
                            if (beat_in_measure < _drumPatterns[drum].NotesOff.Count)
                            {
                                var midiEvent = _drumPatterns[drum].NotesOff[beat_in_measure];
                                if (midiEvent != null)
                                {
                                    midi.Drums.Add(midiEvent);
                                }
                            }
                        }
                    }
                }
            }

            // The metronome is active for all measures/beats
            if (EnableMetronome == true)
            {
                midi.Metronome.Add(_metronomeNoteOff);
                if (beatIndex % 4 == 0)
                {
                    midi.Metronome.Add(_metronomeFirstBeatInstrument);
                }
                else
                {
                    midi.Metronome.Add(_metronomeBeatInstument);
                }
                midi.Metronome.Add(_metronomeNoteOn);
            }

            midi.ChordName = _lastMidiChord != null ? _lastMidiChord.Name : "";

            if (beatIndex > LastBeatIndex)
            {
                if (_lastMidiChord != null)
                {
                    foreach (var midiEvent in _lastMidiChord.NotesOff)
                    {
                        midi.Chords.Add(midiEvent);
                    }
                }
                midi.EndOfSong = true;
            }

            midi.Part = currentLabel;
            midi.ParserLine = currentParserLine;

            return midi;
        }
    }
}
