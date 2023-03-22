using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sanford.Multimedia.Timers;
using Sanford.Multimedia.Midi;
using System.Windows.Forms;

namespace MidiChord
{
    public class ChordLivePlayer : ChordPlayer
    {
        private System.Windows.Forms.Timer _playbackTimer = new System.Windows.Forms.Timer();
        private OutputDevice _midiOutputDevice;
        private int _countDown = 0;



        public ChordLivePlayer(ChordList chordNotes)
            : base(chordNotes)
        {
            _playbackTimer.Tick += new EventHandler(_playbackTimer_Tick);


        }

        public delegate void delegateBeatTick(int beat, int maxBeat, int position, string currentChord);
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
                PlayCountDown();
            }
            else
            {
                PlaySong();
            }
        }

        void PlayCountDown()
        {
            int parserPosition = 0;
            if (EnableMetronome == true)
            {
                _midiOutputDevice.Send(_metronomeNoteOff.Result);
                if (_countDown % 4 == 0)
                {
                    _midiOutputDevice.Send(_metronomeFirstBeatInstrument.Result);
                }
                else
                {
                    _midiOutputDevice.Send(_metronomeBeatInstument.Result);
                }
                _midiOutputDevice.Send(_metronomeNoteOn.Result);
            }
            // Notify client
            if (BeatTick != null)
            {
                BeatTick( -_countDown, _lastBeatIndex, parserPosition, "");
            }
            _countDown--;
        }

        void PlaySong()
        {
            int parserPosition = 0;

            // find ALL notes on the beat index
            foreach( var entry in _data)
            {
                MidiChord midiChord = GetMidiChord(entry.Data);

                if (midiChord == null)
                {
                    Stop();
                    if (SongEnded != null) SongEnded();
                    MessageBox.Show("Unknown chord: '" + entry.Data + "'");
                    return;
                }

                if (entry.BeatIndex == _beatIndex)
                {
                    parserPosition = entry.ParserPosition;

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

            if (EnableMetronome == true)
            {
                _midiOutputDevice.Send(_metronomeNoteOff.Result);
                if (_beatIndex % 4 == 0)
                {
                    _midiOutputDevice.Send( _metronomeFirstBeatInstrument.Result);
                }
                else
                {
                    _midiOutputDevice.Send(_metronomeBeatInstument.Result);
                }
                _midiOutputDevice.Send(_metronomeNoteOn.Result);
            }
            // Notify client
            if (BeatTick != null)
            {
                string chordname = _lastMidiChord != null ? _lastMidiChord.Name : "";
                BeatTick(_beatIndex, _lastBeatIndex, parserPosition, chordname);
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



        public void Start()
        {
            Reset();
            _countDown = 0; // make option
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




    }
}
