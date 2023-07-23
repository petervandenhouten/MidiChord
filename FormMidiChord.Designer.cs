namespace MidiChord
{
    partial class FormMidiChord
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMidiChord));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.recentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exportMIDIFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instrumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drumPatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repeatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tempoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.songToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.soundOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.setBeatsPerMinuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.transposeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.midiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectMIDIOutputDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDefaultInstrumentToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mIDIChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metronomeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.debugLoggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugSongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._statusStrip = new System.Windows.Forms.StatusStrip();
            this._toolStripStatusPlaying = new System.Windows.Forms.ToolStripStatusLabel();
            this._statusPlayingPart = new System.Windows.Forms.ToolStripStatusLabel();
            this._statusChord = new System.Windows.Forms.ToolStripStatusLabel();
            this._statusBeatIndex = new System.Windows.Forms.ToolStripStatusLabel();
            this._statusLabelBpm = new System.Windows.Forms.ToolStripStatusLabel();
            this._StatusLabelInstrument = new System.Windows.Forms.ToolStripStatusLabel();
            this._statusMidiDevice = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._textBox = new ScintillaNET.Scintilla();
            this.toolStripButtonNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCountDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMetronome = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPlay = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPause = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripTempo = new System.Windows.Forms.ToolStripButton();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this._statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.insertToolStripMenuItem,
            this.songToolStripMenuItem,
            this.midiToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(376, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.recentToolStripMenuItem,
            this.toolStripMenuItem2,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exportMIDIFileToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // recentToolStripMenuItem
            // 
            this.recentToolStripMenuItem.Name = "recentToolStripMenuItem";
            this.recentToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.recentToolStripMenuItem.Text = "Recent files";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(161, 6);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(161, 6);
            // 
            // exportMIDIFileToolStripMenuItem
            // 
            this.exportMIDIFileToolStripMenuItem.Name = "exportMIDIFileToolStripMenuItem";
            this.exportMIDIFileToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.exportMIDIFileToolStripMenuItem.Text = "Export MIDI file...";
            this.exportMIDIFileToolStripMenuItem.Click += new System.EventHandler(this.exportMIDIFileToolStripMenuItem_Click);
            // 
            // insertToolStripMenuItem
            // 
            this.insertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chordToolStripMenuItem,
            this.instrumentToolStripMenuItem,
            this.labelToolStripMenuItem,
            this.drumPatternToolStripMenuItem,
            this.repeatToolStripMenuItem,
            this.keyToolStripMenuItem,
            this.tempoToolStripMenuItem});
            this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
            this.insertToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.insertToolStripMenuItem.Text = "Insert";
            // 
            // chordToolStripMenuItem
            // 
            this.chordToolStripMenuItem.Name = "chordToolStripMenuItem";
            this.chordToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.chordToolStripMenuItem.Text = "Chord...";
            // 
            // instrumentToolStripMenuItem
            // 
            this.instrumentToolStripMenuItem.Name = "instrumentToolStripMenuItem";
            this.instrumentToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.instrumentToolStripMenuItem.Text = "Instrument...";
            this.instrumentToolStripMenuItem.Click += new System.EventHandler(this.instrumentToolStripMenuItem_Click);
            // 
            // labelToolStripMenuItem
            // 
            this.labelToolStripMenuItem.Name = "labelToolStripMenuItem";
            this.labelToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.labelToolStripMenuItem.Text = "Label...";
            // 
            // drumPatternToolStripMenuItem
            // 
            this.drumPatternToolStripMenuItem.Name = "drumPatternToolStripMenuItem";
            this.drumPatternToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.drumPatternToolStripMenuItem.Text = "Drum pattern...";
            // 
            // repeatToolStripMenuItem
            // 
            this.repeatToolStripMenuItem.Name = "repeatToolStripMenuItem";
            this.repeatToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.repeatToolStripMenuItem.Text = "Repeat...";
            // 
            // keyToolStripMenuItem
            // 
            this.keyToolStripMenuItem.Name = "keyToolStripMenuItem";
            this.keyToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.keyToolStripMenuItem.Text = "Key...";
            // 
            // tempoToolStripMenuItem
            // 
            this.tempoToolStripMenuItem.Name = "tempoToolStripMenuItem";
            this.tempoToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.tempoToolStripMenuItem.Text = "Tempo...";
            // 
            // songToolStripMenuItem
            // 
            this.songToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripMenuItem5,
            this.soundOffToolStripMenuItem,
            this.toolStripMenuItem4,
            this.setBeatsPerMinuteToolStripMenuItem,
            this.toolStripMenuItem7,
            this.transposeToolStripMenuItem});
            this.songToolStripMenuItem.Name = "songToolStripMenuItem";
            this.songToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.songToolStripMenuItem.Text = "Song";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(188, 6);
            // 
            // soundOffToolStripMenuItem
            // 
            this.soundOffToolStripMenuItem.Name = "soundOffToolStripMenuItem";
            this.soundOffToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.soundOffToolStripMenuItem.Text = "Sound off!";
            this.soundOffToolStripMenuItem.Click += new System.EventHandler(this.soundOffToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(188, 6);
            // 
            // setBeatsPerMinuteToolStripMenuItem
            // 
            this.setBeatsPerMinuteToolStripMenuItem.Name = "setBeatsPerMinuteToolStripMenuItem";
            this.setBeatsPerMinuteToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.setBeatsPerMinuteToolStripMenuItem.Text = "Set beats per minute...";
            this.setBeatsPerMinuteToolStripMenuItem.Click += new System.EventHandler(this.setBeatsPerMinuteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(188, 6);
            // 
            // transposeToolStripMenuItem
            // 
            this.transposeToolStripMenuItem.Name = "transposeToolStripMenuItem";
            this.transposeToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.transposeToolStripMenuItem.Text = "Transpose...";
            // 
            // midiToolStripMenuItem
            // 
            this.midiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectMIDIOutputDeviceToolStripMenuItem,
            this.setDefaultInstrumentToolStripMenuItem1,
            this.mIDIChannelsToolStripMenuItem,
            this.metronomeToolStripMenuItem});
            this.midiToolStripMenuItem.Name = "midiToolStripMenuItem";
            this.midiToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.midiToolStripMenuItem.Text = "Midi";
            // 
            // selectMIDIOutputDeviceToolStripMenuItem
            // 
            this.selectMIDIOutputDeviceToolStripMenuItem.Name = "selectMIDIOutputDeviceToolStripMenuItem";
            this.selectMIDIOutputDeviceToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.selectMIDIOutputDeviceToolStripMenuItem.Text = "Select MIDI output device...";
            this.selectMIDIOutputDeviceToolStripMenuItem.Click += new System.EventHandler(this.selectMIDIOutputDeviceToolStripMenuItem_Click);
            // 
            // setDefaultInstrumentToolStripMenuItem1
            // 
            this.setDefaultInstrumentToolStripMenuItem1.Name = "setDefaultInstrumentToolStripMenuItem1";
            this.setDefaultInstrumentToolStripMenuItem1.Size = new System.Drawing.Size(218, 22);
            this.setDefaultInstrumentToolStripMenuItem1.Text = "Select instrument";
            this.setDefaultInstrumentToolStripMenuItem1.Click += new System.EventHandler(this.setDefaultInstrumentToolStripMenuItem1_Click);
            // 
            // mIDIChannelsToolStripMenuItem
            // 
            this.mIDIChannelsToolStripMenuItem.Name = "mIDIChannelsToolStripMenuItem";
            this.mIDIChannelsToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.mIDIChannelsToolStripMenuItem.Text = "MIDI channels...";
            // 
            // metronomeToolStripMenuItem
            // 
            this.metronomeToolStripMenuItem.Name = "metronomeToolStripMenuItem";
            this.metronomeToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.metronomeToolStripMenuItem.Text = "Metronome...";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.toolStripMenuItem6,
            this.debugLoggingToolStripMenuItem,
            this.debugSongToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            this.manualToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.manualToolStripMenuItem.Text = "Manual";
            this.manualToolStripMenuItem.Click += new System.EventHandler(this.manualToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(150, 6);
            // 
            // debugLoggingToolStripMenuItem
            // 
            this.debugLoggingToolStripMenuItem.Name = "debugLoggingToolStripMenuItem";
            this.debugLoggingToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.debugLoggingToolStripMenuItem.Text = "Debug logging";
            this.debugLoggingToolStripMenuItem.Click += new System.EventHandler(this.debugLoggingToolStripMenuItem_Click);
            // 
            // debugSongToolStripMenuItem
            // 
            this.debugSongToolStripMenuItem.Name = "debugSongToolStripMenuItem";
            this.debugSongToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.debugSongToolStripMenuItem.Text = "Debug song";
            this.debugSongToolStripMenuItem.Click += new System.EventHandler(this.debugSongToolStripMenuItem_Click);
            // 
            // _statusStrip
            // 
            this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripStatusPlaying,
            this._statusPlayingPart,
            this._statusChord,
            this._statusBeatIndex,
            this._statusLabelBpm,
            this._StatusLabelInstrument,
            this._statusMidiDevice});
            this._statusStrip.Location = new System.Drawing.Point(0, 265);
            this._statusStrip.Name = "_statusStrip";
            this._statusStrip.ShowItemToolTips = true;
            this._statusStrip.Size = new System.Drawing.Size(376, 24);
            this._statusStrip.SizingGrip = false;
            this._statusStrip.TabIndex = 2;
            this._statusStrip.Text = "statusStrip1";
            // 
            // _toolStripStatusPlaying
            // 
            this._toolStripStatusPlaying.AutoSize = false;
            this._toolStripStatusPlaying.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._toolStripStatusPlaying.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this._toolStripStatusPlaying.Name = "_toolStripStatusPlaying";
            this._toolStripStatusPlaying.Size = new System.Drawing.Size(60, 19);
            this._toolStripStatusPlaying.Text = "-";
            // 
            // _statusPlayingPart
            // 
            this._statusPlayingPart.AutoSize = false;
            this._statusPlayingPart.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._statusPlayingPart.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this._statusPlayingPart.Name = "_statusPlayingPart";
            this._statusPlayingPart.Size = new System.Drawing.Size(60, 19);
            // 
            // _statusChord
            // 
            this._statusChord.AutoSize = false;
            this._statusChord.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._statusChord.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this._statusChord.Name = "_statusChord";
            this._statusChord.Size = new System.Drawing.Size(48, 19);
            // 
            // _statusBeatIndex
            // 
            this._statusBeatIndex.AutoSize = false;
            this._statusBeatIndex.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._statusBeatIndex.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this._statusBeatIndex.Name = "_statusBeatIndex";
            this._statusBeatIndex.Size = new System.Drawing.Size(36, 19);
            this._statusBeatIndex.Text = "../..";
            // 
            // _statusLabelBpm
            // 
            this._statusLabelBpm.AutoSize = false;
            this._statusLabelBpm.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._statusLabelBpm.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this._statusLabelBpm.Name = "_statusLabelBpm";
            this._statusLabelBpm.Size = new System.Drawing.Size(50, 19);
            this._statusLabelBpm.Text = "toolStripStatusLabel1";
            // 
            // _StatusLabelInstrument
            // 
            this._StatusLabelInstrument.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._StatusLabelInstrument.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this._StatusLabelInstrument.Name = "_StatusLabelInstrument";
            this._StatusLabelInstrument.Size = new System.Drawing.Size(122, 19);
            this._StatusLabelInstrument.Text = "toolStripStatusLabel1";
            // 
            // _statusMidiDevice
            // 
            this._statusMidiDevice.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._statusMidiDevice.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this._statusMidiDevice.Name = "_statusMidiDevice";
            this._statusMidiDevice.Size = new System.Drawing.Size(64, 19);
            this._statusMidiDevice.Text = "No device";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNew,
            this.toolStripButtonOpen,
            this.toolStripButton3,
            this.toolStripSeparator1,
            this.toolStripButtonCountDown,
            this.toolStripButtonMetronome,
            this.toolStripTempo,
            this.toolStripSeparator2,
            this.toolStripButtonPlay,
            this.toolStripButtonPause,
            this.toolStripButtonStop});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(376, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _textBox
            // 
            this._textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._textBox.Location = new System.Drawing.Point(0, 52);
            this._textBox.Name = "_textBox";
            this._textBox.Size = new System.Drawing.Size(375, 210);
            this._textBox.TabIndex = 6;
            // 
            // toolStripButtonNew
            // 
            this.toolStripButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNew.Image = global::MidiChord.Properties.Resources.NewDocument;
            this.toolStripButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNew.Name = "toolStripButtonNew";
            this.toolStripButtonNew.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNew.Text = "New file";
            this.toolStripButtonNew.ToolTipText = "New file";
            this.toolStripButtonNew.Click += new System.EventHandler(this.toolStripButtonNew_Click);
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpen.Image = global::MidiChord.Properties.Resources.OpenFile;
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOpen.Text = "Open file";
            this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::MidiChord.Properties.Resources.Save;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Save";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButtonCountDown
            // 
            this.toolStripButtonCountDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCountDown.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCountDown.Image")));
            this.toolStripButtonCountDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCountDown.Name = "toolStripButtonCountDown";
            this.toolStripButtonCountDown.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonCountDown.Text = "Count down";
            this.toolStripButtonCountDown.Click += new System.EventHandler(this.toolStripButtonCountDown_Click);
            // 
            // toolStripButtonMetronome
            // 
            this.toolStripButtonMetronome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMetronome.Image = global::MidiChord.Properties.Resources.Metronome;
            this.toolStripButtonMetronome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMetronome.Name = "toolStripButtonMetronome";
            this.toolStripButtonMetronome.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonMetronome.Text = "Metronome";
            this.toolStripButtonMetronome.Click += new System.EventHandler(this.toolStripButtonMetronome_Click);
            // 
            // toolStripButtonPlay
            // 
            this.toolStripButtonPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPlay.Image = global::MidiChord.Properties.Resources.Play;
            this.toolStripButtonPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPlay.Name = "toolStripButtonPlay";
            this.toolStripButtonPlay.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPlay.Text = "Play";
            this.toolStripButtonPlay.Click += new System.EventHandler(this.toolStripButtonPlay_Click);
            // 
            // toolStripButtonPause
            // 
            this.toolStripButtonPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPause.Image = global::MidiChord.Properties.Resources.Pause;
            this.toolStripButtonPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPause.Name = "toolStripButtonPause";
            this.toolStripButtonPause.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPause.Text = "Pause";
            this.toolStripButtonPause.ToolTipText = "Pause";
            this.toolStripButtonPause.Click += new System.EventHandler(this.toolStripButtonPause_Click);
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStop.Image = global::MidiChord.Properties.Resources.Stop;
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonStop.Text = "Stop";
            this.toolStripButtonStop.Click += new System.EventHandler(this.toolStripButtonStop_Click);
            // 
            // toolStripTempo
            // 
            this.toolStripTempo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripTempo.Image = global::MidiChord.Properties.Resources.Timer;
            this.toolStripTempo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripTempo.Name = "toolStripTempo";
            this.toolStripTempo.Size = new System.Drawing.Size(23, 22);
            this.toolStripTempo.Text = "Tempo (beats per minute)";
            this.toolStripTempo.Click += new System.EventHandler(this.toolStripTempo_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::MidiChord.Properties.Resources.NewDocument;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::MidiChord.Properties.Resources.OpenFile;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::MidiChord.Properties.Resources.Save;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Image = global::MidiChord.Properties.Resources.Play;
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.playToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.playToolStripMenuItem.Text = "Play";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Image = global::MidiChord.Properties.Resources.Pause;
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.pauseToolStripMenuItem.Text = "Pause";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Image = global::MidiChord.Properties.Resources.Stop;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // FormMidiChord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 289);
            this.Controls.Add(this._textBox);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this._statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMidiChord";
            this.Text = "MIDI Chord";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.FormMidiChord_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this._statusStrip.ResumeLayout(false);
            this._statusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem songToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem midiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectMIDIOutputDeviceToolStripMenuItem;
        private System.Windows.Forms.StatusStrip _statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel _toolStripStatusPlaying;
        private System.Windows.Forms.ToolStripStatusLabel _statusMidiDevice;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exportMIDIFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instrumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDefaultInstrumentToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem soundOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel _statusLabelBpm;
        private System.Windows.Forms.ToolStripStatusLabel _StatusLabelInstrument;
        private System.Windows.Forms.ToolStripMenuItem mIDIChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem setBeatsPerMinuteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metronomeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem debugLoggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugSongToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel _statusBeatIndex;
        private System.Windows.Forms.ToolStripMenuItem recentToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel _statusChord;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonNew;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonPlay;
        private System.Windows.Forms.ToolStripButton toolStripButtonPause;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.ToolStripStatusLabel _statusPlayingPart;
        private System.Windows.Forms.ToolStripMenuItem labelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drumPatternToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repeatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tempoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem transposeToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonMetronome;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private ScintillaNET.Scintilla _textBox;
        private System.Windows.Forms.ToolStripButton toolStripButtonCountDown;
        private System.Windows.Forms.ToolStripButton toolStripTempo;
    }
}

