using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sanford.Multimedia.Midi;
//using System.IO;
//using static System.Net.Mime.MediaTypeNames;
//using System.Reflection;
//using System.Data.SqlTypes;

namespace MidiChord
{
    public class MetaData
    {
        public string Title;
        public string SubTitle;
        public string Artist;
        public string Composer;
        public string Year;
    }

    public class LabelledPart
    {
        public string Name;
        public int FirstLine;
        public int LastLine;
    }

    public class ChordParser
    {
        private enum ParserLineMode { CHORDS_OR_TEXT, COMMAND, LABEL, COMMENT_LINE, DISABLE_PART };
        private enum ParserCommand { NONE, LABEL, INSTRUMENT, DRUM, TITLE, SUBTITLE, TEMPO, KEY, REPEAT, JUMP };

        private enum ParserDuration { WHOLE, HALF, QUARTER };

        private enum ParserChordMode { MEASURE, BEAT, COMMENT, TEXT };

        private string _lastChord;
        private List<string> _logging;
        private int _beatIndex; // index of beat (position in song) during parsing
        private ParserLineMode _parserLineMode;
        private ParserChordMode _parserChordMode;
        private List<LabelledPart> _labels;

        // Music data
        private string _key;
        private int _tempo;
        private GeneralMidiInstrument _instrument;
        private string _currentLabel;

        private List<SongItem> _parserOutput;
        private readonly ChordList _chordList;
        private MetaData _metaData;

        public string Instrument { get { return _instrument.ToString(); } }
        public string Key { get { return _key; } }
        public int Tempo { get { return _tempo; } }
        public int BeatCount { get { return _beatIndex; } }
        public int MeasureCount { get { return 1 + (_beatIndex - 1) / 4; } }
        public MetaData Song { get { return _metaData; } }

        public ChordParser()
        {
            _chordList = new ChordList();
        }

        public ChordParser(ChordList chordNotes)
        {
            _chordList = chordNotes;
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

        public List<SongItem> Parse(string[] lines)
        {
            initializeParser();

            var chordProConvertor = new ChordProConverter();
            lines = chordProConvertor.Convert(lines);

            int lineCount = 0;

            foreach (var line in lines)
            {
                parseLine(lineCount++, line);
            }

            return _parserOutput;
        }

        private void initializeParser()
        {
            if (_logging == null) _logging = new List<string>();
            _logging.Clear();

            if(_parserOutput == null) _parserOutput = new List<SongItem>();
            _parserOutput.Clear();

            if(_labels == null) _labels = new List<LabelledPart>();
            _labels.Clear();

            _metaData = new MetaData();
            _beatIndex = 0;
            _parserLineMode = ParserLineMode.CHORDS_OR_TEXT;
            _currentLabel = "";
        }

        private void parseLine(int lineNumber, string txt)
        {
            ParserCommand _parserCommand;

            txt = txt.Trim();
            if (txt.Length == 0) return;

            string firstChar = txt.Substring(0, 1);

            if (firstChar.Equals(@"#"))
            {
                _parserLineMode = ParserLineMode.COMMENT_LINE;
            }
            else if (firstChar == "{")
            {
                if (_parserChordMode != ParserChordMode.COMMENT)
                {
                    _parserCommand = parseCommand(txt);

                    if (_parserCommand == ParserCommand.NONE)
                    {
                        handleLabel(txt, lineNumber);
                        _parserLineMode = ParserLineMode.LABEL;

                        // a label can be the end of a DISABLE_PART
                    }
                    else
                    {
                        _parserLineMode = ParserLineMode.COMMAND;
                    }
                }
            }
            else if (firstChar == "-")
            {
                _parserLineMode = ParserLineMode.DISABLE_PART;
            }

            // Handle chords in parser mode
            if (_parserLineMode == ParserLineMode.CHORDS_OR_TEXT )
            {
                // Chords of songtext
                var chordLineDetector = new ChordLineDetector(txt);
                if (chordLineDetector.isChords())
                {
                    parseChords(txt);
                }
                else
                {
                    parseSongText(txt);
                }
            }

            // return to chord/text parsing
            if (_parserLineMode == ParserLineMode.COMMENT_LINE ||
                _parserLineMode == ParserLineMode.LABEL ||
                _parserLineMode == ParserLineMode.COMMAND)
            {
                _parserLineMode = ParserLineMode.CHORDS_OR_TEXT;
            }

            //while (pointer < txt.Length)
            //{
            //    chordFound = false;
            //    movepointer = 1;

            //    // Determine mode
            //    if (txt.Substring(pointer, 1).Equals(@"#"))
            //    {
            //        mode = ParserMode.SONG_TEXT;
            //    }
            //    else if ( mode == ParserMode.BEGIN_OF_LINE && firstChar.Equals(@"-"))
            //    {
            //        mode = ParserMode.DISABLE_PART;
            //    }
            //    else if (txt.Substring(pointer, 1).Equals(@"/"))
            //    {
            //        mode = ParserMode.COMMENT;
            //    }
            //    else if (mode == ParserMode.BEGIN_OF_LINE && firstChar.Equals(@"("))
            //    {
            //        mode = ParserMode.LABEL;
            //    }
            //    else if (txt.Substring(pointer, 1).Equals(@"{"))
            //    {
            //        mode = ParserMode.INSTRUMENT;
            //    }
            //    else if (txt.Substring(pointer, 1).Equals(@"["))
            //    {
            //        mode = ParserMode.BEAT_CHORD;
            //    }
            //    else if (txt.Substring(pointer, 1).Equals(@"]"))
            //    {
            //        mode = ParserMode.MEASURE_CHORD;
            //    }
            //    else if (firstChar.Equals("\r") || firstChar.Equals("\n"))
            //    {
            //        mode = ParserMode.END_OF_LINE;
            //    }
            //    else // chord or nothing
            //    {
            //        if (chordStartCharacters.IndexOf(txt.Substring(pointer, 1).ToUpper()) != -1)
            //        {
            //            chordFound = true;
            //        }
            //        else
            //        {
            //            // chordFound is always false
            //            movepointer = 1;
            //        }
            //    }

            //    // handle chord modes
            //    if (chordFound)
            //    {
            //        switch (mode)
            //        {
            //            case ParserMode.MEASURE_CHORD:
            //                movepointer = HandleMeasureChord(txt, pointer);
            //                break;
            //            case ParserMode.BEAT_CHORD:
            //                movepointer = HandleBeatChord(txt, pointer);
            //                break;
            //        }
            //    }
            //    else
            //    {
            //        // Handle special modes
            //        switch (mode)
            //        {
            //            case ParserMode.END_OF_LINE:
            //                movepointer = HandleEndOfLine(txt, pointer);
            //                mode = ParserMode.BEGIN_OF_LINE;
            //                break;
            //            case ParserMode.COMMENT:
            //                movepointer = HandleComment(txt, pointer);
            //                mode = ParserMode.MEASURE_CHORD;
            //                break;
            //            case ParserMode.INSTRUMENT:
            //                movepointer = HandleInstrument(txt, pointer);
            //                mode = ParserMode.MEASURE_CHORD;
            //                break;
            //            case ParserMode.LABEL:
            //                movepointer = HandleLabel(txt, pointer);
            //                mode = ParserMode.MEASURE_CHORD;
            //                break;
            //            case ParserMode.SONG_TEXT:
            //                movepointer = HandleSongText(txt,pointer);
            //                mode = ParserMode.MEASURE_CHORD;
            //                break;
            //            case ParserMode.DISABLE_PART:
            //                movepointer = HandleDisablePart(txt, pointer);
            //                mode = ParserMode.MEASURE_CHORD;
            //                break;

            //            default: break;
            //        }
            //    }

            //    // move pointer
            //    pointer += movepointer;
            //}

            //return _parserOutput;
        }

        private void parseChords(string txt)
        {
            int pointer = 0;
            int movepointer = 0;
            bool chordFound = false;

            if (_parserChordMode != ParserChordMode.COMMENT)
            {
                _parserChordMode = ParserChordMode.MEASURE;
            }

            while (pointer < txt.Length)
            {
                chordFound = false;
                movepointer = 1;

                string firstChar = txt.Substring(pointer, 1);

                if (_parserChordMode == ParserChordMode.COMMENT)
                {
                    if (firstChar.Equals(@"/"))
                    {
                        // Disable comment
                        _parserChordMode = ParserChordMode.MEASURE;
                    }
                    movepointer = 1;
                }
                else
                {
                    if (firstChar.Equals(@"|"))
                    {
                        if (_parserChordMode == ParserChordMode.MEASURE)
                        {
                            _parserChordMode = ParserChordMode.BEAT;
                            movepointer = 1;
                        }
                        else
                        {
                            _parserChordMode = ParserChordMode.MEASURE;
                            movepointer = 1;
                        }
                    }
                    else if (firstChar.Equals(@"/"))
                    {
                        // Enable comment
                        Log("Comment part");
                        _parserChordMode = ParserChordMode.COMMENT;
                        movepointer = 1;
                    }
                    else if (firstChar.Equals(@" "))
                    {
                        movepointer = 1;
                    }
                    else
                    {
                        if (_parserChordMode == ParserChordMode.MEASURE)
                        {
                            movepointer = handleMeasureChord(txt, pointer);
                            chordFound = movepointer > 0;
                        }
                        else if (_parserChordMode == ParserChordMode.BEAT)
                        {
                            movepointer = handleBeatChord(txt, pointer);
                            chordFound = movepointer > 0;
                        }
                    }
                }
                pointer += movepointer;

            }
        }

        private ParserCommand parseCommand(string txt)
        {
            ParserCommand parserCommand = ParserCommand.NONE;

            int pos1 = txt.IndexOf('{');
            int pos2 = txt.IndexOf('}');
            int pos3 = txt.IndexOf(':');

            if (pos2 < 0) return ParserCommand.NONE;

            string command = pos3 < 0 ? txt.Substring(pos1 + 1, pos2 - pos1 - 1)
                                      : txt.Substring(pos1 + 1, pos3 - pos1 - 1);

            string argument = pos3 < 0 ? ""
                                       : txt.Substring(pos3 + 1, pos2 - pos3 - 1);

            command = command.Trim().ToLower();
            argument = argument.Trim();

            if (command.Equals("instrument"))
            {
                parserCommand = handleInstrument(argument);
            }
            else if (command.Equals("title"))
            {
                parserCommand = handleTitle(argument);
            }
            return parserCommand;
        }

        private ParserCommand handleTitle(string argument)
        {
            // if instrument is unknown then LOG ERROR, and not set the string
            _metaData.Title = argument;

            Log("Song title:", argument);
            return ParserCommand.TITLE;

        }

        private int HandleEndOfLine(string txt, int pointer)
        {
            int pos = txt.IndexOf('\n');
            if (pos < 0) pos = 1;
            return pos + 1;
        }

        private int HandleDisablePart(string txt, int pointer)
        {
            string substring = txt.Substring(pointer + 1);

            int pos1 = substring.IndexOf('(');
            string label = substring.Substring(pos1);
            int pos2 = label.IndexOf(')');
            label = label.Substring(1, pos2 - 1);
            label = label.Trim();

            Log("Part disabled: '" + label + "'");

            int pos3 = GetOffsetToNextPart(substring, pos1 + pos2 + 1);
            int pos = 1 + pos1 + pos2 + pos3;
            string removed = txt.Substring(pointer, pos);
            Log("Part to disable: '" + removed + "'");
            return pos;
        }

        private int GetOffsetToNextPart(string txt, int pointer)
        {
            string substring = txt.Substring(pointer);
            int pos1 = substring.IndexOf('(');
            int pos2 = substring.IndexOf('-');
            if (pos1 < 0)
            {
                pos1 = substring.Length - 1;
            }
            if (pos2 < 0)
            {
                pos2 = substring.Length - 1;
            }
            int pos = Math.Min(pos1, pos2);
            return pos;
        }

        private int HandleComment(string txt, int pointer)
        {
            string substring = txt.Substring(pointer + 1);

            // get length until next comment character
            int pos = substring.IndexOf(@"/");
            if (pos < 0)
            {
                pos = substring.Length - 1;
            }
            else
            {
                pos += 2; // the two comment characters
            }
            Log("Comment: '" + substring.Substring(0, pos - 2) + "'");
            return pos;
        }

        private ParserCommand handleInstrument(string argument)
        {
            foreach (GeneralMidiInstrument instrument in Enum.GetValues(typeof(GeneralMidiInstrument)))
            {
                if (argument.Equals(instrument.ToString()))
                {
                    _instrument = instrument;

                    SongItem c = new SongItem
                    {
                        Type            = SongItem.SongItemType.CHANGE_INSTRUMENT,
                        BeatIndex       = _beatIndex,
                        Instrument      = instrument,
                        ParserPosition  = 0, // Not used anymore
                        Part            = _currentLabel
                    };
                    _parserOutput.Add(c);
                    Log("Instrument:", argument);
                }
            }

            return ParserCommand.INSTRUMENT;
        }

        private int old_HandleInstrument(string txt, int pointer)
        {
            string substring = txt.Substring(pointer);

            // get length until end of line
            int pos = substring.IndexOf('}');
            string instrument = substring.Substring(1, pos);
            Log("Instrument: " + instrument);

            SongItem c = new SongItem
            {
                Type            = SongItem.SongItemType.CHANGE_INSTRUMENT,
                BeatIndex       = _beatIndex,
                Data            = instrument,
                ParserPosition  = pointer,
                Part            = _currentLabel
            };
            _parserOutput.Add(c);

            return pos > 0 ? pos : 0;
        }

        private string handleLabel(string txt, int lineNumber)
        {
            int pos1 = txt.IndexOf('{');
            int pos2 = txt.IndexOf('}');

            if (pos2 < 0) return "";

            string label = txt.Substring(pos1 + 1, pos2 - pos1 - 1);
            label = label.Trim().ToLower();

            if (!string.IsNullOrEmpty(label))
            {
                addLabelFirstLine(label, lineNumber);
                _currentLabel = label;
            }

            return label;
        }

        private void addLabelFirstLine(string label, int lineNumber)
        {
            Log(string.Format("Label '{0}' starts at line {1}", label, lineNumber));

            var item = new LabelledPart
            {
                Name = label,
                FirstLine = lineNumber
            };

            _labels.Add(item);
        }

        private void addLabelLastLine(string label, int lineNumber)
        {
            Log(string.Format("Label '{0}' ends at line {1}", label, lineNumber));

            // Find the last label in the list with this name
        }

        private void parseSongText(string txt)
        {
            int pointer = 0;
            int movepointer = 0;
            bool chordFound = false;

            if (_parserChordMode != ParserChordMode.COMMENT)
            {
                _parserChordMode = ParserChordMode.TEXT;
            }

            while (pointer < txt.Length)
            {
                chordFound = false;
                movepointer = 1;

                string firstChar = txt.Substring(pointer, 1);

                if (_parserChordMode == ParserChordMode.COMMENT)
                {
                    if (firstChar.Equals(@"/"))
                    {
                        // Disable comment
                        _parserChordMode = ParserChordMode.TEXT;
                    }
                    movepointer = 1;
                }
                else
                {
                    if (firstChar.Equals(@"["))
                    {
                        _parserChordMode = ParserChordMode.MEASURE;
                        movepointer = 1;
                    }
                    else if (firstChar.Equals(@"]"))
                    {
                        _parserChordMode = ParserChordMode.TEXT;
                        movepointer = 1;
                    }
                    else if (firstChar.Equals(@"/"))
                    {
                        // Enable comment
                        Log("Comment part");
                        _parserChordMode = ParserChordMode.COMMENT;
                        movepointer = 1;
                    }
                    else if (firstChar.Equals(@" "))
                    {
                        movepointer = 1;
                    }
                    else
                    {
                        if (_parserChordMode == ParserChordMode.MEASURE)
                        {
                            movepointer = handleMeasureChord(txt, pointer);
                            chordFound = movepointer > 0;
                        }
                        else if (_parserChordMode == ParserChordMode.BEAT)
                        {
                            movepointer = handleBeatChord(txt, pointer);
                            chordFound = movepointer > 0;
                        }
                    }
                }
                pointer += movepointer;
            }
        }

        private int handleBeatChord(string txt, int pointer)
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
                string[] chordDescription = findChordAtStartOfString(substring, out chordName);

                if (chordDescription != null)
                {
                    Log("Beat chord: " + chordName);

                    SongItem c = new SongItem
                    {
                        Type           = SongItem.SongItemType.BEAT_CHORD,
                        BeatIndex      = _beatIndex,
                        Data           = chordName,
                        ParserPosition = pointer,
                        Part           = _currentLabel
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

        private int handleMeasureChord(string txt, int pointer)
        {
            int skipCharacters = 0;

            string substring = txt.Substring(pointer);

            if (substring[0] == '*')
            {
                Log("Keep chord: " + _lastChord);
                skipCharacters = 1;
            }
            //if (substring[0] == ' ')
            //{
            //    skipCharacters = 1;
            //}
            else
            {
                string chordName = string.Empty;
                string[] chordDescription = findChordAtStartOfString(substring, out chordName);

                if (chordDescription != null)
                {
                    Log("Measure chord: " + chordName);

                    SongItem c = new SongItem
                    {
                        Type           = SongItem.SongItemType.MEASURE_CHORD,
                        BeatIndex      = _beatIndex,
                        Data           = chordName,
                        ParserPosition = pointer,
                        Part           = _currentLabel
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

        private string[] findChordAtStartOfString(string str, out string chordName)
        {
            string longestChordFound = string.Empty;

            // add some characters to make sure the string is long enough to be compared with all chords
            str = str + "     ";

            // find longest chord at start of string str
            foreach (var chord in _chordList.GetAllChords())
            {
                if (str.Substring(0, chord.Length).ToUpper().Equals(chord.ToUpper()) &&
                     chord.Length > longestChordFound.Length)
                {
                    longestChordFound = chord;
                }
            }

            chordName = longestChordFound;

            if (_chordList.ContainsChord(longestChordFound))
            {
                return _chordList.GetChord(longestChordFound);
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

        private void Log(string msg, string parameter)
        {
            string txt = string.Format("{0} '{1}'", msg, parameter);
            Log(txt);
        }


        public string[] Logging
        {
            get
            {
                //return string.Join("\n", _logging.ToArray());;
                return _logging.ToArray();
            }
        }

        public int GetNumberOfChords()
        {
            int count = 0;
            foreach (var songitem in _parserOutput)
            {
                if ( songitem.Type == SongItem.SongItemType.MEASURE_CHORD ||
                     songitem.Type == SongItem.SongItemType.BEAT_CHORD )
                {
                    count++;
                }
            }
            return count;
        }

        public int GetNumberOfInstrumentChanges()
        {
            int count = 0;
            foreach (var songitem in _parserOutput)
            {
                if (songitem.Type == SongItem.SongItemType.CHANGE_INSTRUMENT)
                {
                    count++;
                }
            }
            return count;
        }
        public int GetNumberOfDrumPatterns()
        {
            int count = 0;
            foreach (var songitem in _parserOutput)
            {
                if (songitem.Type == SongItem.SongItemType.DRUM_PATTERN)
                {
                    count++;
                }
            }
            return count;
        }
        public int GetNumberOfTempoChanges()
        {
            int count = 0;
            foreach (var songitem in _parserOutput)
            {
                if (songitem.Type == SongItem.SongItemType.CHANGE_TEMPO)
                {
                    count++;
                }
            }
            return count;
        }

        public int GetNumberOfLabelledParts()
        {
            if (_labels == null) return 0;
            return _labels.Count;
        }

    }
}
