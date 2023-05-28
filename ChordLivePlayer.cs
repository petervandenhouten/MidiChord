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
        public bool PlayCountDown = false;

        private System.Windows.Forms.Timer _playbackTimer = new System.Windows.Forms.Timer();
        private OutputDevice _midiOutputDevice;
        private int _countDown = 0;

        public ChordLivePlayer(ChordList chordNotes, DrumList drumNotes)
            : base(chordNotes, drumNotes)
        {
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
                BeatTick( -_countDown, LastBeatIndex, 0, "", "", "Countdown");
            }
            _countDown--;
        }

        void PlaySong()
        {
            var midi = GetMidiCommandsAtBeat(CurrentBeatIndex);

            playMidiCommands(midi);

            NextBeatIndex();

            if (midi.EndOfSong)
            {
                Stop();
                if (SongEnded != null) SongEnded();
            }

            // Notify client
            if (BeatTick != null)
            {
                // TODO: next chord
                BeatTick(CurrentBeatIndex, LastBeatIndex, midi.ParserLine, midi.ChordName, "", midi.Part);
            }
        }

        private void playMidiCommands(MidiCommands midi)
        {
            var order = new List<IMidiMessage>[] { midi.MetaData, midi.Metronome, midi.Chords, midi.Drums  };

            foreach(var list in order)
            {
                foreach (var cmd in list)
                {
                    _midiOutputDevice.Send(cmd as ChannelMessage);
                }
            }
        }

        public void Start()
        {
            Reset();
            _countDown = PlayCountDown ? 4 : 0;
            SetDefaultMidiInstrument();
            _playbackTimer.Interval = base.GetBeatTimeInMs(BeatsPerMinute);
            _playbackTimer.Start();
        }

        private void SetDefaultMidiInstrument()
        {
            if (_midiOutputDevice != null)
            {
                var factory = new MidiCommandFactory();
                _midiOutputDevice.Send(factory.Instrument(SongInstrument));
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
            var factory = new MidiCommandFactory();

            if (_midiOutputDevice != null)
            {
                _midiOutputDevice.Send(factory.Instrument(instrument));
                _midiOutputDevice.Send(factory.NoteCOn());
            }

        }
    }
}
