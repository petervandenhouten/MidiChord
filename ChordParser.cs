using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sanford.Multimedia.Midi;
using System.IO;

namespace MidiChord
{
    public class ChordParser
    {
        private enum ParserMode { BAR_CHORD, BEAT_CHORD, LABEL, SONG_TEXT, INSTRUMENT, COMMENT };

        //private Sequence _sequence;
        //private Track _track;
        private string _lastChord;
        private List<string> _logging;
        private int _beatIndex; // index of beat (position in song) during parsing

        private List<SongItem> _parserOutput;
        private readonly Dictionary<string, string[]> _refChordNotes;

        #region Chord data

        #endregion

        public ChordParser(Dictionary<string, string[]> chordNotes)
        {
            _refChordNotes = chordNotes;

//            _sequence = new Sequence();
            _logging = new List<string>();
            _parserOutput = new List<SongItem>();

            
        }



        public int ParseErrorMessage { get; private set; }

        /***
        private Track ParseTrack(string txt)
        {
            _sequence.Clear();
            _logging.Clear();

            // Calculate timing
            int division = _sequence.Division;
            int quarterDelay = (60 * 4 * 1000) / (division * BeatsPerMinute);
            int beatDelay = quarterDelay / 4; 
            quarterDelay = beatDelay * 4; // accurate again for 4 beats

            // create notes for metronome
            int metronomeVolume = 75;
            int metronomeNote = 60;
            int metronomeDelay = 10;

            ChannelMessageBuilder msg1 = new ChannelMessageBuilder();
            msg1.Command = ChannelCommand.ProgramChange;
            msg1.MidiChannel = MetronomeMidiChannel;
            msg1.Data1 = MetronomeFirstBeatInstrument;
            msg1.Build();

            ChannelMessageBuilder msg2 = new ChannelMessageBuilder();
            msg2.Command = ChannelCommand.ProgramChange;
            msg2.MidiChannel = MetronomeMidiChannel;
            msg2.Data1 = MetronomeInstrument;
            msg2.Build();

            ChannelMessageBuilder noteOn = new ChannelMessageBuilder();
            noteOn.Command = ChannelCommand.NoteOn;
            noteOn.MidiChannel = MetronomeMidiChannel;
            noteOn.Data1 = metronomeNote;
            noteOn.Data2 = metronomeVolume;
            noteOn.Build();

            ChannelMessageBuilder noteOff = new ChannelMessageBuilder();
            noteOff.Command = ChannelCommand.NoteOff;
            noteOff.MidiChannel = MetronomeMidiChannel;
            noteOff.Data1 = metronomeNote;
            noteOff.Data2 = metronomeVolume;
            noteOff.Build();

            // Initialize new track and the time variable
            if (_track == null)
            {
                _track = new Track();
            }
            _track.Clear();
            int time = 0;

            // Insert metronome
            if (EnableMetronome)
            {
                for (int i = 0; i < 4; i++)
                {
                    _track.Insert(time, msg2.Result);
                    _track.Insert(time, noteOn.Result);
                    _track.Insert(time + metronomeDelay, noteOff.Result);
                    time += beatDelay;
                }
            }

            // Initialize parser
            ParserMode mode = ParserMode.BAR_CHORD;
            int pointer = 0;
            int movepointer = 0;
            string chordStartCharacters = "ABCDEFG*";

            bool chordFound = false;

            while (pointer < txt.Length)
            {
                string firstChar = txt.Substring(pointer, 1);
                chordFound = false;

                movepointer = 1;

                // Determine mode
                if (txt.Substring(pointer, 1).Equals(@"#"))
                {
                    mode = ParserMode.SONG_TEXT;
                }
                else if (txt.Substring(pointer, 1).Equals(@"/"))
                {
                    mode = ParserMode.COMMENT;
                }
                else if (txt.Substring(pointer, 1).Equals(@"\"))
                {
                    mode = ParserMode.BAR_CHORD;
                }
                else if (txt.Substring(pointer, 1).Equals(@"("))
                {
                    mode = ParserMode.LABEL;
                }
                else if (txt.Substring(pointer, 1).Equals(@"{"))
                {
                    mode = ParserMode.INSTRUMENT;
                }
                else if (txt.Substring(pointer, 1).Equals(@"["))
                {
                    mode = ParserMode.BEAT_CHORD;
                }
                else if (txt.Substring(pointer, 1).Equals(@"]"))
                {
                    mode = ParserMode.BAR_CHORD;
                }
                else // chord or nothing
                {
                    if (chordStartCharacters.IndexOf(txt.Substring(pointer, 1).ToUpper()) != -1)
                    {
                        chordFound = true;
                    }
                    else
                    {
                        // chordFound is always false
                        movepointer = 1;
                    }
                }

                // handle chord modes
                if ( chordFound )
                {
                    switch (mode)
                    {
                        case ParserMode.BAR_CHORD:
                            movepointer = HandleBarChord(txt, pointer);
                            break;
                        case ParserMode.BEAT_CHORD:
                            movepointer = HandleBeatChord(txt, pointer);
                            break;
                    }
                }
                else
                {
                    switch (mode)
                    {
                        case ParserMode.COMMENT:
                            break;
                        case ParserMode.INSTRUMENT:
                            break;
                        case ParserMode.LABEL:
                            break;
                        case ParserMode.SONG_TEXT:
                            movepointer = HandleSongText(txt, pointer);
                            mode = ParserMode.BAR_CHORD;
                            break;
                        default: break;
                    }
                }

                // move pointer
                pointer += movepointer;
            }

            return _track;
        }
         * ****/

        public List<SongItem> ParseText(string txt)
        {
            _logging.Clear();
            _beatIndex = 0;

            // Initialize parser
            ParserMode mode = ParserMode.BAR_CHORD;
            int pointer = 0;
            int movepointer = 0;
            string chordStartCharacters = "ABCDEFG*";
            

            bool chordFound = false;

            while (pointer < txt.Length)
            {
                string firstChar = txt.Substring(pointer, 1);
                chordFound = false;

                movepointer = 1;

                // Determine mode
                if (txt.Substring(pointer, 1).Equals(@"#"))
                {
                    mode = ParserMode.SONG_TEXT;
                }
                else if (txt.Substring(pointer, 1).Equals(@"/"))
                {
                    if (mode != ParserMode.COMMENT)
                    {
                        mode = ParserMode.COMMENT;
                    }
                    else
                    {
                        mode = ParserMode.BAR_CHORD;
                    }
                }
                else if (txt.Substring(pointer, 1).Equals(@"("))
                {
                    mode = ParserMode.LABEL;
                }
                else if (txt.Substring(pointer, 1).Equals(@"{"))
                {
                    mode = ParserMode.INSTRUMENT;
                }
                else if (txt.Substring(pointer, 1).Equals(@"["))
                {
                    mode = ParserMode.BEAT_CHORD;
                }
                else if (txt.Substring(pointer, 1).Equals(@"]"))
                {
                    mode = ParserMode.BAR_CHORD;
                }
                else // chord or nothing
                {
                    if (chordStartCharacters.IndexOf(txt.Substring(pointer, 1).ToUpper()) != -1)
                    {
                        chordFound = true;
                    }
                    else
                    {
                        // chordFound is always false
                        movepointer = 1;
                    }
                }

                // handle chord modes
                if (chordFound)
                {
                    switch (mode)
                    {
                        case ParserMode.BAR_CHORD:
                            movepointer = HandleBarChord(txt, pointer);
                            break;
                        case ParserMode.BEAT_CHORD:
                            movepointer = HandleBeatChord(txt, pointer);
                            break;
                    }
                }
                else
                {
                    switch (mode)
                    {
                        case ParserMode.COMMENT:
                            break;
                        case ParserMode.INSTRUMENT:
                            movepointer = HandleInstrument(txt, pointer);
                            mode = ParserMode.BAR_CHORD;
                            break;
                        case ParserMode.LABEL:
                            movepointer = HandleLabel(txt, pointer);
                            mode = ParserMode.BAR_CHORD;
                            break;
                        case ParserMode.SONG_TEXT:
                            movepointer = HandleSongText(txt,pointer);
                            mode = ParserMode.BAR_CHORD;
                            break;
                        default: break;
                    }
                }

                // move pointer
                pointer += movepointer;
            }

            return _parserOutput;
        }

        private int HandleInstrument(string txt, int pointer)
        {
            string substring = txt.Substring(pointer);

            // get length until end of line
            int pos = substring.IndexOf('}');
            string instrument = substring.Substring(1, pos);
            Log("Instrument: " + instrument);

            SongItem c = new SongItem
            {
                Timing = SongItem.SongItemType.CHANGE_INSTRUMENT,
                BeatIndex = _beatIndex,
                Data = instrument,
                ParserPosition = pointer
            };
            _parserOutput.Add(c);

            return pos > 0 ? pos : 0;
        }

        private int HandleLabel(string txt, int pointer)
        {
            string substring = txt.Substring(pointer);

            // get length until end of line
            int pos = substring.IndexOf(')');
            Log("Label: " + substring.Substring(1, pos));
            return pos > 0 ? pos : 0;
        }

        private int HandleSongText(string txt, int pointer)
        {
            string substring = txt.Substring(pointer);

            // get length until end of line
            int pos = substring.IndexOf('\n');
            Log("Song text: " + substring.Substring(1,pos));
            return pos > 0 ? pos : 0;
        }

        private int HandleBeatChord(string txt, int pointer)
        {
            int skipCharacters = 0;
            string substring = txt.Substring(pointer);

            if (substring[0] == '*')
            {
                Log("Keep chord: " + _lastChord);
                skipCharacters = 1;
            }
            else
            {
                string chordName = string.Empty;
                string[] chordDescription = FindChordAtStartOfString(substring, out chordName);

                if (chordDescription != null)
                {
                    Log("Beat chord: " + chordName);

                    SongItem c = new SongItem
                    {
                        Timing = SongItem.SongItemType.BEAT_CHORD,
                        BeatIndex = _beatIndex,
                        Data = chordName,
                        ParserPosition = pointer
                    };
                    _parserOutput.Add(c);
                    _lastChord = chordName;
                    skipCharacters = chordName.Length;
                }
                else
                {
                    Log("Unknown chord: " + chordName);
                }
            }

            _beatIndex += 1;

            return skipCharacters;
        }

        private int HandleBarChord(string txt, int pointer)
        {
            int skipCharacters = 0;

            string substring = txt.Substring(pointer);

            if (substring[0] == '*')
            {
                Log("Keep chord: " + _lastChord);
                skipCharacters = 1;
            }
            else
            {
                string chordName = string.Empty;
                string[] chordDescription = FindChordAtStartOfString(substring, out chordName);

                if (chordDescription != null)
                {
                    Log("Bar chord: " + chordName);

                    SongItem c = new SongItem
                    {
                        Timing = SongItem.SongItemType.BAR_CHORD,
                        BeatIndex = _beatIndex,
                        Data = chordName,
                        ParserPosition = pointer
                    };
                    _parserOutput.Add(c);
                    _lastChord = chordName;
                    skipCharacters = chordName.Length;
                }
                else
                {
                    Log("Unknown chord: " + chordName);
                    skipCharacters = 1;
                }
            }

            _beatIndex += 4;

            return skipCharacters;
        }

        private string[] FindChordAtStartOfString(string str, out string chordName)
        {
            string longestChordFound = string.Empty;

            // add some characters to make sure the string is long enough to be compared with all chords
            str = str + "     ";

            // find longest chord at start of string str
            foreach (var pair in _refChordNotes)
            {
                if ( str.Substring(0, pair.Key.Length).ToUpper().Equals(pair.Key.ToUpper()) &&
                     pair.Key.Length > longestChordFound.Length )
                {
                    longestChordFound = pair.Key;
                }
            }

            chordName = longestChordFound;

            if (_refChordNotes.ContainsKey(longestChordFound))
            {
                return _refChordNotes[longestChordFound];
            }

            return null;
        }

        /****
        private Track CreateMetronomeTrack(int bpm, int instrument1, int instrument2, int length)
        {
            Track track = new Track();

            int division = _sequence.Division;
            int quarterDelay = (60 * 2 * 1000) / (division * bpm);
            int beatDelay = quarterDelay / 4; // not accurate
            quarterDelay = beatDelay * 4; // accurate again
            int delay = 10;
            int metronomeVolume = 60;
            int metronomeNote = 60;

            // four count intro with instrument 2
            ChannelMessageBuilder msg1 = new ChannelMessageBuilder();
            msg1.Command = ChannelCommand.ProgramChange;
            msg1.MidiChannel = 1;
            msg1.Data1 = instrument1;
            msg1.Build();

            ChannelMessageBuilder msg2 = new ChannelMessageBuilder();
            msg2.Command = ChannelCommand.ProgramChange;
            msg2.MidiChannel = 1;
            msg2.Data1 = instrument2;
            msg2.Build();

            ChannelMessageBuilder noteOn = new ChannelMessageBuilder();
            noteOn.Command = ChannelCommand.NoteOn;
            noteOn.MidiChannel = 1;
            noteOn.Data1 = metronomeNote;
            noteOn.Data2 = metronomeVolume;
            noteOn.Build();

            ChannelMessageBuilder noteOff = new ChannelMessageBuilder();
            noteOff.Command = ChannelCommand.NoteOff;
            noteOff.MidiChannel = 1;
            noteOff.Data1 = metronomeNote;
            noteOff.Data2 = metronomeVolume;
            noteOff.Build();

            for (int i = 0; i < 4;i++ )
            {
                track.Insert(i * beatDelay, msg2.Result);
                track.Insert(i * beatDelay, noteOn.Result);
                track.Insert(i * beatDelay + delay, noteOff.Result);
            }

            // repeating bars with 1x instrument1  + 3x instrument2
            int beatCount = 1;
            int time = 1 * quarterDelay;

            while (time < length)
            {
                if (beatCount % 4 == 1)
                {
                    track.Insert(time, msg1.Result);
                }
                else
                {
                    track.Insert(time, msg2.Result);
                }

                track.Insert(time, noteOn.Result);
                track.Insert(time + delay, noteOff.Result);

                time += beatDelay;
                beatCount++;
            }

            return track;
        }

        ***/

        /****
        private Track CreateChordTrack_old(string txt, int bpm, int instrument)
        {
            char[] endOfLineChars = { '\n', ';' };
            char[] endOfChordChars = { ' ', '.' };
            char[] remarkChars = { '#', '[' };
            char[] instrumentChars = { '{' };

            string[] lines = txt.Split(endOfLineChars);

            Track track = new Track();

            int division = _sequence.Division;
                
            int quarterDelay = (60 * 2 * 1000) / (division * bpm);
            int beatDelay = quarterDelay / 4; // not accurate
            quarterDelay = beatDelay * 4; // accurate again
            int time = 1 * quarterDelay;
            
            // Set instrument
            track.Insert(time, GetProgram(instrument));
            
            foreach (string line in lines)
            {
                if (line.Length>0 && !remarkChars.Contains(line[0]))
                {
                    if (instrumentChars.Contains(line[0]))
                    {
                        // change instrument
                    }
                    else // generate a chord
                    {
                        string[] chords = line.Trim().Split(endOfChordChars, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string chord in chords)
                        {
                            Chord notesOfChord = GetChord(chord);

                            int chordDelay = 1 * quarterDelay;

                            if (chords.Length == 2)
                            {
                                chordDelay = 2 * quarterDelay;
                            }
                            else if (chords.Length == 1)
                            {
                                chordDelay = 4 * quarterDelay;
                            }

                            foreach (var midiEvent in notesOfChord.NotesOn)
                            {
                                track.Insert(time, midiEvent);
                            }
                            foreach (var midiEvent in notesOfChord.NotesOff)
                            {
                                track.Insert(time + chordDelay - 1, midiEvent);
                            }

                            time += chordDelay;
                        }
                    }
                }
            }
            
            return track;
        }

        ***/



        private void Log(string msg)
        {
            if (_logging != null)
            {
                _logging.Add(msg);
            }
        }

        private string[] GetNotesOfChord(string chord)
        {
            switch (chord.ToUpper().Trim())
            {
                case "A": return new string[] { "A", "C#", "E" };
                case "A7": return new string[] { "A", "C#", "E", "G" };
                case "AM":  return new string[] { "A", "C", "E" };
                case "AM7": return new string[] { "A", "C", "E", "G" };

                case "A#": return new string[] { "Bb", "D", "F" };
                case "Bb": return new string[] { "Bb", "D", "F" };

                case "B": return new string[] { "A", "D#", "F#" };

                case "C":   return new string[] { "C", "E", "G" };

                case "C#": return new string[] { "Db", "F", "Ab" };
                case "Db": return new string[] { "Db", "F", "Ab" };

                case "D":   return new string[] { "D", "F#", "A" };
                case "D7": return new string[] { "D", "F#", "A", "C" };

                case "D#": return new string[] { "Eb", "G", "Bb" };
                case "Eb#": return new string[] { "Eb", "G", "Bb" };

                case "E": return new string[] { "E", "G#", "B" };
                case "EM": return new string[] { "E", "G", "B" };

                case "F": return new string[] { "F", "A", "C" };

                case "F#": return new string[] { "F#", "A#", "C#" };
                case "Gb": return new string[] { "F#", "A#", "C#" };

                case "G":   return new string[] { "G", "B", "D" };

                case "G#": return new string[] { "Ab", "C", "Eb" };
                case "Ab": return new string[] { "Ab", "C", "Eb" };



                default:
                    return null;
            }


        }

        public string Logging 
        {
            get
            {
                return string.Join("\n", _logging.ToArray());;
            }
        }
    }
}
