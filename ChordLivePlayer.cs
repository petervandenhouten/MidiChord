using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sanford.Multimedia.Timers;
using Sanford.Multimedia.Midi;
using System.Windows.Forms;
using Sanford.Multimedia;
using static MidiChord.ChordPlayer;

namespace MidiChord
{
    public class ChordLivePlayer : ChordPlayer
    {
        private System.Windows.Forms.Timer _playbackTimer = new System.Windows.Forms.Timer();
        private OutputDevice _midiOutputDevice;
        private int _countDown = 0;
        private string _currentLabel;
        private int _currentParserLine = -1;

        public ChordLivePlayer(ChordList chordNotes, DrumList drumNotes)
            : base(chordNotes, drumNotes)
        {
            _currentLabel = "";
            _playbackTimer.Tick += new EventHandler(_playbackTimer_Tick);
        }

        public delegate void delegateBeatTick(int beat, int maxBeat, int position, string currentChord, string nextChord, string currentPart);
        public event delegateBeatTick BeatTick;

        public event Action SongEnded;
        
        public int MidiOutputDeviceID
        {
            get
            {
                return _midiOutputDevice.DeviceID;
            }
            set
            {
                if (_midiOutputDevice != null)
                {
                    _midiOutputDevice.Close();
                    _midiOutputDevice.Dispose();
                }
                _midiOutputDevice = new OutputDevice(value);
            }
        }

        public void Mute()
        {
            _midiOutputDevice.Reset();
        }

        public void Delete()
        {
            if (_playbackTimer != null)
            {
                _playbackTimer.Stop();
                _playbackTimer.Dispose();
                _playbackTimer.Tick -= new EventHandler(_playbackTimer_Tick);
                _playbackTimer = null;
            }

            if (_midiOutputDevice != null)
            {
                _midiOutputDevice.Close();
                _midiOutputDevice.Dispose();
                _midiOutputDevice = null;
            }

        }

        void _playbackTimer_Tick(object sender, EventArgs e)
        {
            if (_countDown > 0)
            {
                playCountDown();
            }
            else
            {
                PlaySong();
            }
        }

        protected void playCountDown()
        {
            if (EnableMetronome == true)
            {
                _midiOutputDevice.Send(_metronomeNoteOff);
                if (_countDown % 4 == 0)
                {
                    _midiOutputDevice.Send(_metronomeFirstBeatInstrument);
                }
                else
                {
                    _midiOutputDevice.Send(_metronomeBeatInstument);
                }
                _midiOutputDevice.Send(_metronomeNoteOn);
            }
            // Notify client
            if (BeatTick != null)
            {
                BeatTick( -_countDown, _lastBeatIndex, 0, "", "", "Countdown");
            }
            _countDown--;
        }

        // Called every tick!
        void PlaySong()
        {
            // find ALL notes on the beat index
            foreach (var entry in _data)
            {
                if (entry.Type == SongItem.SongItemType.MEASURE_CHORD ||
                     entry.Type == SongItem.SongItemType.BEAT_CHORD)
                {
                    MidiChord midiChord = GetMidiChord(entry.Data);

                    if (midiChord == null)
                    {
                        Stop();
                        if (SongEnded != null) SongEnded();
                        MessageBox.Show("Unknown chord: '" + entry.Data + "'");
                        return;
                    }

                    // Find on which beat we are (every tick we move a next beat)
                    if (entry.BeatIndex == _beatIndex)
                    {
                        _currentLabel = entry.Part;

                        if (_lastMidiChord != null)
                        {
                            foreach (var midiEvent in _lastMidiChord.NotesOff)
                            {
                                _midiOutputDevice.Send(midiEvent);
                            }
                        }
                        foreach (var midiEvent in midiChord.NotesOn)
                        {
                            _midiOutputDevice.Send(midiEvent);
                        }
                        _lastMidiChord = midiChord;
                    }
                }
                else if (entry.Type == SongItem.SongItemType.START_DRUM_PATTERN)
                {
                    DrumPattern drumPattern = GetDrumPattern(entry.Data);

                    if (drumPattern == null)
                    {
                        Stop();
                        if (SongEnded != null) SongEnded();
                        MessageBox.Show("Invalid drum pattern: '" + entry.Data + "'");
                        return;
                    }

                    if (entry.BeatIndex <= _beatIndex)
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
                    if (entry.BeatIndex <= _beatIndex)
                    {
                        // deactivate
                        int drumPatternIndex = getDrumIndex(entry.ItemInstrument);
                        if (drumPatternIndex >= 0)
                        {
                            _drumPatterns[drumPatternIndex] = null;
                        }
                    }
                }

                if (entry.BeatIndex == _beatIndex)
                {
                    // check line
                    _currentParserLine = Math.Max(entry.LineNumber, _currentParserLine);
                }
            }

            playDrumPattern();

            if (EnableMetronome == true)
            {
                _midiOutputDevice.Send(_metronomeNoteOff);
                if (_beatIndex % 4 == 0)
                {
                    _midiOutputDevice.Send( _metronomeFirstBeatInstrument);
                }
                else
                {
                    _midiOutputDevice.Send(_metronomeBeatInstument);
                }
                _midiOutputDevice.Send(_metronomeNoteOn);
            }
            // Notify client
            if (BeatTick != null)
            {
                string chordname = _lastMidiChord != null ? _lastMidiChord.Name : "";

                // TODO: next chord

                BeatTick(_beatIndex, _lastBeatIndex, _currentParserLine, chordname, "", _currentLabel);
            }

            base._beatIndex++;

            if (_beatIndex > _lastBeatIndex)
            {
                if (_lastMidiChord != null)
                {
                    foreach (var midiEvent in _lastMidiChord.NotesOff)
                    {
                        _midiOutputDevice.Send(midiEvent);
                    }
                }

                Stop();
                if (SongEnded != null) SongEnded();
            }
        }

        private void playDrumPattern()
        {
            if (_beatIndex < _lastBeatIndex)
            {
                _midiOutputDevice.Send(_drumInstument);

                for (int drum = 0; drum < 3; drum++)
                {
                    if (_drumPatterns[drum] != null)
                    {
                        int beat_in_measure = (base._beatIndex % 4);
                        if (beat_in_measure < _drumPatterns[drum].NotesOn.Count)
                        {
                            var midiEvent = _drumPatterns[drum].NotesOn[beat_in_measure];
                            if (midiEvent != null)
                            {
                                _midiOutputDevice.Send(midiEvent);
                            }
                        }
                    }
                }
            }
        }

        public void Start()
        {
            Reset();
            _currentLabel = "";
            _countDown = 0; // make option
            _currentParserLine = 0;
            SetMidiInstrument();
            _playbackTimer.Interval = base.GetBeatTimeInMs(BeatsPerMinute);
            _playbackTimer.Start();
        }

        private void SetMidiInstrument()
        {
            if (_midiOutputDevice != null)
            {
                _midiOutputDevice.Send(GetMidiProgram(SongInstrument));
            }
        }


        public void Stop()
        {
            _playbackTimer.Stop();
            Reset();
        }

        public void Pauze()
        {
            _playbackTimer.Stop();
        }
        public void Continue()
        {
            _playbackTimer.Start();
        }

        internal void PlayNote(GeneralMidiInstrument instrument)
        {
            var builder = new ChannelMessageBuilder();
            builder.Command = ChannelCommand.NoteOn;
            builder.MidiChannel = 0;
            builder.Data1 = GetMidiData("C");
            builder.Data2 = 127;
            builder.Build();

            if (_midiOutputDevice != null)
            {
                _midiOutputDevice.Send(GetMidiProgram(instrument));
                _midiOutputDevice.Send(builder.Result);
            }

        }
    }
}
