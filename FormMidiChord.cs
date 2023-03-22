using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.UI;

namespace MidiChord
{
    public partial class FormMidiChord : Form
    {
        private const string AppName = @"MIDI Chord - ";

        private int _beatsPerMinute;
        private string _filename;
        private bool _saved = false;
        private GeneralMidiInstrument _instrument;
        private GeneralMidiInstrument _metronomeInstrument1;
        private GeneralMidiInstrument _metronomeInstrument2;

        private ChordLivePlayer _midiLivePlayer;
        private ChordList _chordList;

        private bool _playing = false;

        private Queue<string> MRUlist = new Queue<string>();

        public FormMidiChord()
        {
            InitializeComponent();

            // Load choard
            _chordList = new ChordList();

            // Midi sequence generator
            _midiLivePlayer = new ChordLivePlayer(_chordList);
            _midiLivePlayer.BeatTick += new ChordLivePlayer.delegateBeatTick(_midiLivePlayer_BeatTick);
            _midiLivePlayer.SongEnded += new Action(_midiLivePlayer_SongEnded);

            // Default Settings
            SetMIDIDevice(0);
            SetBeatsPerMinute(120);
            SetInstrument(GeneralMidiInstrument.AcousticGrandPiano);
            SetMetronomeInstrument(GeneralMidiInstrument.TaikoDrum, GeneralMidiInstrument.Woodblock);
        }

        #region Live Player Events

        void _midiLivePlayer_SongEnded()
        {
            _playing = false;
            _toolStripStatusPlaying.Text = "Completed.";
        }

        void _midiLivePlayer_BeatTick(int beat, int max, int position, string currentChord)
        {
            // todo cursos in text @ position
            int measure = (beat / 4) + 1;
            int beat_in_measure = (beat % 4) + 1;

            _statusBeatIndex.Text = measure.ToString() + "." + beat_in_measure.ToString();
            _statusChord.Text = currentChord;
        }

        #endregion

        #region Playback
        private void EnterPlayingMode()
        {
            _toolStripStatusPlaying.Text = "Playing";
            _playing = true;
        }

        private void LeavePlayingMode(bool pauze = false)
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
        #endregion

        #region Form events

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


        private void _textBox_TextChanged(object sender, EventArgs e)
        {
            _saved = false;
        }

        private void FormMidiChord_Load(object sender, EventArgs e)
        {
            LoadRecentList();

            foreach (string item in MRUlist)
            {
                ToolStripMenuItem fileRecent = new ToolStripMenuItem(item,
                   null, new EventHandler(this.OnRecentFileClicked));

                recentToolStripMenuItem.DropDownItems.Add(fileRecent);
            }

#if (DEBUG)
            const string default_file = @"test.txt";
            if (File.Exists(default_file))
            {
                LoadText(default_file);
            }
#endif
        }

        #endregion

        #region Menu items

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsFile();
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void soundOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _midiLivePlayer.Mute();
        }

        private void setDefaultInstrumentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangeInstrument();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void setBeatsPerMinuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeBeatsPerMinute();
        }

        private void debugLoggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChordParser parser = new ChordParser(_chordList);
            parser.ParseText(_textBox.Lines);

            var dlg = new StringListDialog("Logging");
            dlg.SetText(parser.Logging);
            dlg.ShowDialog();
        }

        private void exportMIDIFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChordParser parser = new ChordParser(_chordList);
            List<SongItem> song = parser.ParseText(_textBox.Lines);

            // convert to track/sequence
            ChordToMidiConvertor midiConverter = new ChordToMidiConvertor(_chordList);
            midiConverter.SetSong(song);

            midiConverter.BeatsPerMinute = _beatsPerMinute;
            midiConverter.SongInstrument = _instrument;
            midiConverter.MetronomeFirstBeatInstrument = _metronomeInstrument1;
            midiConverter.MetronomeInstrument = _metronomeInstrument2;

            // save sequence
            midiConverter.Save("test.mid");
        }

        private void debugSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = new List<string>();

            ChordParser parser = new ChordParser(_chordList);
            List<SongItem> song = parser.ParseText(_textBox.Lines);

            foreach (var songchord in song)
            {
                list.Add(songchord.ToString());
            }

            var dlg = new StringListDialog("Generated Chords");
            dlg.SetText(list.ToArray());
            dlg.ShowDialog();


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

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        #endregion

        #region Toolbar buttons

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void toolStripButtonPlay_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void toolStripButtonPause_Click(object sender, EventArgs e)
        {
            Pause();
        }
        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            Stop();
        }
        #endregion

        #region Song playback

        private void Play()
        {
            if (!_playing)
            {
                ChordParser parser = new ChordParser(_chordList);
                List<SongItem> song = parser.ParseText(_textBox.Lines);

                _midiLivePlayer.BeatsPerMinute = _beatsPerMinute;
                _midiLivePlayer.SongInstrument = _instrument;
                _midiLivePlayer.MetronomeFirstBeatInstrument = _metronomeInstrument1;
                _midiLivePlayer.MetronomeInstrument = _metronomeInstrument2;

                _midiLivePlayer.SetSong(song);

                _midiLivePlayer.Start();

                EnterPlayingMode();
            }
        }

        private void Pause()
        {
            if (_playing)
            {
                _midiLivePlayer.Pauze();
                LeavePlayingMode(true);
            }
            else
            {
                _midiLivePlayer.Continue();
                EnterPlayingMode();
            }
        }

        private void Stop()
        {
            _midiLivePlayer.Stop();
            LeavePlayingMode();
        }

        #endregion

        #region File Open/Save

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


        private void NewFile()
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

        private void OpenFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadText(dlg.FileName);
                SaveRecentFile(dlg.FileName);
            }
        }
        private void SaveFile()
        {
            if (_filename == null)
            {
                SaveAsFile();
            }
            else
            {
                SaveText(_filename);
            }
        }

        private void SaveAsFile()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveText(dlg.FileName);
            }
        }

        #endregion

        #region MRU
        private void OnRecentFileClicked(object sender, EventArgs e)
        {
            string filepath = sender.ToString();
            LoadText(filepath);
        }
        private void SaveRecentFile(string strPath)
        {
            recentToolStripMenuItem.DropDownItems.Clear();

            LoadRecentList();

            if (!(MRUlist.Contains(strPath)))

                MRUlist.Enqueue(strPath);

            while (MRUlist.Count > 5)

                MRUlist.Dequeue();

            foreach (string strItem in MRUlist)
            {
                ToolStripMenuItem tsRecent = new
                   ToolStripMenuItem(strItem, null);

                recentToolStripMenuItem.DropDownItems.Add(tsRecent);
            }

            StreamWriter stringToWrite = new
               StreamWriter(System.Environment.CurrentDirectory +
               @"\Recent.txt");

            foreach (string item in MRUlist)

                stringToWrite.WriteLine(item);

            stringToWrite.Flush();

            stringToWrite.Close();
        }

        private void LoadRecentList()
        {
            MRUlist.Clear();

            string filepath = Environment.CurrentDirectory + @"\Recent.txt";

            if (File.Exists(filepath)) 
            {
                StreamReader srStream = new StreamReader(filepath);
                string strLine = "";
                while ((InlineAssignHelper(ref strLine, srStream.ReadLine())) != null)
                {
                    MRUlist.Enqueue(strLine);
                }
                srStream.Close();
            }
        }

        private static T InlineAssignHelper<T>(ref T target, T value)
        {
            target = value;
            return value;
        }
        #endregion

        #region Song settings

        private void ChangeBeatsPerMinute()
        {
            BeatDialog dlg = new BeatDialog();

            dlg.BeatsPerMinute = _beatsPerMinute;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SetBeatsPerMinute(dlg.BeatsPerMinute);
            }
        }

        private void ChangeInstrument()
        {
            InstrumentDialog dlg = new InstrumentDialog();

            dlg.Instrument = _instrument;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SetInstrument(dlg.Instrument);
            }
        }

        private void SetInstrument(GeneralMidiInstrument generalMidiInstrument)
        {
            _instrument = generalMidiInstrument;
            _StatusLabelInstrument.Text = generalMidiInstrument.ToString();
        }

        private void SetMetronomeInstrument(GeneralMidiInstrument instrument1, GeneralMidiInstrument instrument2)
        {
            _metronomeInstrument1 = instrument1;
            _metronomeInstrument2 = instrument2;
        }

        private void SetBeatsPerMinute(int bpm)
        {
            _beatsPerMinute = bpm;
            _statusLabelBpm.Text = _beatsPerMinute.ToString() + " bpm";
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

        #endregion
    }
}
