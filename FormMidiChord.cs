using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Threading;

using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.UI;

namespace MidiChord
{
    public partial class FormMidiChord : Form
    {
        private const string AppName = @"MIDI Chord - ";
        private ChordParser _chordPlayer;

        private int _beatsPerMinute;
        private string _filename;
        private bool _saved = false;
        private GeneralMidiInstrument _instrument;
        private GeneralMidiInstrument _metronomeInstrument1;
        private GeneralMidiInstrument _metronomeInstrument2;

        private ChordLivePlayer _midiLivePlayer;
        private Dictionary<string, string[]> _chordNotes;

        private bool _playing = false;

        public FormMidiChord()
        {
            InitializeComponent();

            _metronomeInstrument1 = GeneralMidiInstrument.TaikoDrum;
            _metronomeInstrument2 = GeneralMidiInstrument.Woodblock;

            // Load choard
            CreateChordNotes();

            // Midi sequence generator
            _chordPlayer = new ChordParser(_chordNotes);
            _midiLivePlayer = new ChordLivePlayer(_chordNotes);
            _midiLivePlayer.BeatTick += new ChordLivePlayer.delegateBeatTick(_midiLivePlayer_BeatTick);
            _midiLivePlayer.SongEnded += new Action(_midiLivePlayer_SongEnded);

            SetMIDIDevice(0);
            SetBeatsPerMinute(200);
            SetInstrument(GeneralMidiInstrument.AcousticGrandPiano);

        }

        void _midiLivePlayer_SongEnded()
        {
            _playing = false;
            _toolStripStatusPlaying.Text = "Completed.";
        }

        void _midiLivePlayer_BeatTick(int beat, int max, int position)
        {
            // todo cursos in text @ position

            _statusBeatIndex.Text = beat.ToString() + "/" + max.ToString();
        }

        private void CreateChordNotes()
        {
            if (_chordNotes == null)
            {
                _chordNotes = new Dictionary<string, string[]>();
            }
            _chordNotes.Clear();

            const string chordsFilename = @"chords.txt";
            try
            {
                System.IO.TextReader readFile = new StreamReader(chordsFilename);
                string txt = readFile.ReadToEnd();

                readFile.Close();
                readFile = null;

                // parse chords
                string[] lines = txt.Split('\n');

                foreach (string line in lines)
                {
                    string cleanLine = line.Replace("\r", "").Replace("\n", "").Trim();

                    if (!string.IsNullOrWhiteSpace(cleanLine))
                    {
                        string chordName = cleanLine.Split(':')[0];
                        string notes = cleanLine.Split(':')[1];

                        string [] noteArray = notes.Split(',');
                        List<string> cleanNoteArray = new List<string>();
                        foreach (string str in noteArray)
                        {
                            cleanNoteArray.Add(str.Replace('\r', ' ').Trim());
                        }

                        _chordNotes.Add(chordName, cleanNoteArray.ToArray());
                    }
                }

            }
            catch (IOException ex)
            {
                MessageBox.Show("Cannot read chords.txt file. " + ex.Message);
            }
        }

        private void SetInstrument(GeneralMidiInstrument generalMidiInstrument)
        {
            _instrument = generalMidiInstrument;
            _StatusLabelInstrument.Text = generalMidiInstrument.ToString();
        }

        private void SetBeatsPerMinute(int bpm)
        {
            _beatsPerMinute = bpm;
            _statusLabelBpm.Text = _beatsPerMinute.ToString() + " bpm";
        }

        void _sequencer_ChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
        {
            // actually send the midi data
            //_midiOutputDevice.Send(e.Message);

            //context.post(delegate(object dummy)
            //{
            //    string txt = e.message.command.tostring() + '\t' + '\t' +
            //                e.message.midichannel.tostring() + '\t' +
            //                e.message.data1.tostring() + '\t' +
            //                e.message.data2.tostring();

            //    _statuslabelchannelmessage.text = txt;
            //}, null);

        }


        private void SetMIDIDevice(int deviceID)
        {
            try
            {
                _midiLivePlayer.MidiOutputDeviceID = deviceID;
                _statusMidiDevice.Text = OutputDevice.GetDeviceCapabilities(deviceID).name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_playing)
            {
                ChordParser parser = new ChordParser( _chordNotes );
                List<SongItem> song = parser.ParseText(_textBox.Text);

                _midiLivePlayer.BeatsPerMinute                  = _beatsPerMinute;
                _midiLivePlayer.SongInstrument                  = (int)_instrument;
                _midiLivePlayer.MetronomeFirstBeatInstrument    = (int)_metronomeInstrument1;
                _midiLivePlayer.MetronomeInstrument             = (int)_metronomeInstrument2;

                _midiLivePlayer.SetSong(song);

                _midiLivePlayer.Start();

                EnterPlayingMode();
            }
        }

        private void EnterPlayingMode()
        {
            _toolStripStatusPlaying.Text = "Playing";
            _playing = true;
        }

        private void selectMIDIOutputDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OutputDeviceDialog dlg = new OutputDeviceDialog();
            dlg.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SetMIDIDevice(dlg.OutputDeviceID);
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_midiLivePlayer != null)
            {
                _midiLivePlayer.BeatTick   -= _midiLivePlayer_BeatTick;
                _midiLivePlayer.SongEnded  -= _midiLivePlayer_SongEnded;
                _midiLivePlayer.Stop();
                _midiLivePlayer.Delete();
                _midiLivePlayer = null;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveText(dlg.FileName);
            }
        }

        private bool SaveText(string filename)
        {
            bool bstatus = true;

            try
            {
                System.IO.TextWriter writeFile = new StreamWriter(filename);
                writeFile.Write(_textBox.Text);
                writeFile.Flush();
                writeFile.Close();
                writeFile = null;

                _saved = true;

                SetCurrentFilename(filename);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
                bstatus = false;
            }
            return bstatus;
        }

        private void SetCurrentFilename(string filename)
        {
            _filename = filename;
            this.Text = AppName + Path.GetFileName(_filename);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_filename == null)
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                SaveText(_filename);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadText(dlg.FileName);
            }
        }

        private bool LoadText(string filename)
        {
            bool bstatus = true;
            try
            {
                System.IO.TextReader readFile = new StreamReader(filename);
                string txt = readFile.ReadToEnd();
                _textBox.Text = txt;

                readFile.Close();
                readFile = null;

                _saved = true;

                SetCurrentFilename(filename);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
                bstatus = false;
            }
            return bstatus;
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_playing)
            {
                _midiLivePlayer.Stop();
                LeavePlayingMode(true);
            }
            else
            {
                _midiLivePlayer.Continue();
                EnterPlayingMode();
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _midiLivePlayer.Stop();
            LeavePlayingMode();

        }

        private void LeavePlayingMode(bool pauze=false)
        {
            _playing = false;
            if (pauze)
            {
                _toolStripStatusPlaying.Text = "Paused";
            }
            else
            {
                _toolStripStatusPlaying.Text = "Stopped";
            }
            _midiLivePlayer.Mute();
        }

        private void soundOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _midiLivePlayer.Mute();
        }

        private void setDefaultInstrumentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //InstrumentDialog dlg = new InstrumentDialog();

            //dlg.Instrument = _instrument;

            //if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    SetInstrument(dlg.Instrument);
            //}
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_saved)
            {
                if (MessageBox.Show("Do you want to loose the lastest changes?", "New file", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    _textBox.Text = string.Empty;
                    SetCurrentFilename(string.Empty);
                }
            }
            else
            {
                _textBox.Text = string.Empty;
                SetCurrentFilename(string.Empty);
            }
        }

        private void _textBox_TextChanged(object sender, EventArgs e)
        {
            _saved = false;
        }

        private void FormMidiChord_Load(object sender, EventArgs e)
        {
#if (DEBUG)
            const string default_file = @"test.txt";
            if (File.Exists(default_file))
            {
                LoadText(default_file);
            }
#endif
        }

        private void setBeatsPerMinuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //BeatDialog dlg = new BeatDialog();

            //dlg.BeatsPerMinute = _beatsPerMinute;

            //if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    SetBeatsPerMinute(dlg.BeatsPerMinute);
            //}
        }

        private void debugLoggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_chordPlayer.Logging);
        }

        private void exportMIDIFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChordParser parser = new ChordParser( _chordNotes );
            List<SongItem> song = parser.ParseText(_textBox.Text);

            // convert to track/sequence
            ChordToMidiConvertor midiConverter = new ChordToMidiConvertor(_chordNotes);
            midiConverter.SetSong(song);

            // save sequence
            midiConverter.Save("test.mid");
        }

        private void debugSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string txt = string.Empty;

            ChordParser parser = new ChordParser( _chordNotes );
            List<SongItem> song = parser.ParseText(_textBox.Text);

            foreach (var songchord in song)
            {
                txt += songchord.ToString() + " ";
            }

            MessageBox.Show(txt);
        }
    }
}
