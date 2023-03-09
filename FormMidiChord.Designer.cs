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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exportMIDIFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instrumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.songToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.soundOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.setBeatsPerMinuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this._StatusLabelInstrument = new System.Windows.Forms.ToolStripStatusLabel();
            this._statusLabelBpm = new System.Windows.Forms.ToolStripStatusLabel();
            this._statusMidiDevice = new System.Windows.Forms.ToolStripStatusLabel();
            this._textBox = new System.Windows.Forms.TextBox();
            this._statusBeatIndex = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this._statusStrip.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(337, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripMenuItem2,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exportMIDIFileToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(35, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(169, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(169, 6);
            // 
            // exportMIDIFileToolStripMenuItem
            // 
            this.exportMIDIFileToolStripMenuItem.Name = "exportMIDIFileToolStripMenuItem";
            this.exportMIDIFileToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.exportMIDIFileToolStripMenuItem.Text = "Export MIDI file...";
            this.exportMIDIFileToolStripMenuItem.Click += new System.EventHandler(this.exportMIDIFileToolStripMenuItem_Click);
            // 
            // insertToolStripMenuItem
            // 
            this.insertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chordToolStripMenuItem,
            this.instrumentToolStripMenuItem});
            this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
            this.insertToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.insertToolStripMenuItem.Text = "Insert";
            // 
            // chordToolStripMenuItem
            // 
            this.chordToolStripMenuItem.Name = "chordToolStripMenuItem";
            this.chordToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.chordToolStripMenuItem.Text = "Chord...";
            // 
            // instrumentToolStripMenuItem
            // 
            this.instrumentToolStripMenuItem.Name = "instrumentToolStripMenuItem";
            this.instrumentToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.instrumentToolStripMenuItem.Text = "Instrument...";
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
            this.setBeatsPerMinuteToolStripMenuItem});
            this.songToolStripMenuItem.Name = "songToolStripMenuItem";
            this.songToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.songToolStripMenuItem.Text = "Song";
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.playToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.playToolStripMenuItem.Text = "Play";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.pauseToolStripMenuItem.Text = "Pause";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(194, 6);
            // 
            // soundOffToolStripMenuItem
            // 
            this.soundOffToolStripMenuItem.Name = "soundOffToolStripMenuItem";
            this.soundOffToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.soundOffToolStripMenuItem.Text = "Sound off!";
            this.soundOffToolStripMenuItem.Click += new System.EventHandler(this.soundOffToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(194, 6);
            // 
            // setBeatsPerMinuteToolStripMenuItem
            // 
            this.setBeatsPerMinuteToolStripMenuItem.Name = "setBeatsPerMinuteToolStripMenuItem";
            this.setBeatsPerMinuteToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.setBeatsPerMinuteToolStripMenuItem.Text = "Set beats per minute...";
            this.setBeatsPerMinuteToolStripMenuItem.Click += new System.EventHandler(this.setBeatsPerMinuteToolStripMenuItem_Click);
            // 
            // midiToolStripMenuItem
            // 
            this.midiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectMIDIOutputDeviceToolStripMenuItem,
            this.setDefaultInstrumentToolStripMenuItem1,
            this.mIDIChannelsToolStripMenuItem,
            this.metronomeToolStripMenuItem});
            this.midiToolStripMenuItem.Name = "midiToolStripMenuItem";
            this.midiToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.midiToolStripMenuItem.Text = "Midi";
            // 
            // selectMIDIOutputDeviceToolStripMenuItem
            // 
            this.selectMIDIOutputDeviceToolStripMenuItem.Name = "selectMIDIOutputDeviceToolStripMenuItem";
            this.selectMIDIOutputDeviceToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.selectMIDIOutputDeviceToolStripMenuItem.Text = "Select MIDI output device...";
            this.selectMIDIOutputDeviceToolStripMenuItem.Click += new System.EventHandler(this.selectMIDIOutputDeviceToolStripMenuItem_Click);
            // 
            // setDefaultInstrumentToolStripMenuItem1
            // 
            this.setDefaultInstrumentToolStripMenuItem1.Name = "setDefaultInstrumentToolStripMenuItem1";
            this.setDefaultInstrumentToolStripMenuItem1.Size = new System.Drawing.Size(221, 22);
            this.setDefaultInstrumentToolStripMenuItem1.Text = "Select instrument...";
            this.setDefaultInstrumentToolStripMenuItem1.Click += new System.EventHandler(this.setDefaultInstrumentToolStripMenuItem1_Click);
            // 
            // mIDIChannelsToolStripMenuItem
            // 
            this.mIDIChannelsToolStripMenuItem.Name = "mIDIChannelsToolStripMenuItem";
            this.mIDIChannelsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.mIDIChannelsToolStripMenuItem.Text = "MIDI channels...";
            // 
            // metronomeToolStripMenuItem
            // 
            this.metronomeToolStripMenuItem.Name = "metronomeToolStripMenuItem";
            this.metronomeToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
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
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            this.manualToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.manualToolStripMenuItem.Text = "Manual";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.aboutToolStripMenuItem.Text = "About";
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
            this._StatusLabelInstrument,
            this._statusLabelBpm,
            this._statusBeatIndex,
            this._statusMidiDevice});
            this._statusStrip.Location = new System.Drawing.Point(0, 251);
            this._statusStrip.Name = "_statusStrip";
            this._statusStrip.ShowItemToolTips = true;
            this._statusStrip.Size = new System.Drawing.Size(337, 22);
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
            this._toolStripStatusPlaying.Size = new System.Drawing.Size(60, 17);
            this._toolStripStatusPlaying.Text = "-";
            // 
            // _StatusLabelInstrument
            // 
            this._StatusLabelInstrument.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._StatusLabelInstrument.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this._StatusLabelInstrument.Name = "_StatusLabelInstrument";
            this._StatusLabelInstrument.Size = new System.Drawing.Size(113, 17);
            this._StatusLabelInstrument.Text = "toolStripStatusLabel1";
            // 
            // _statusLabelBpm
            // 
            this._statusLabelBpm.AutoSize = false;
            this._statusLabelBpm.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._statusLabelBpm.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this._statusLabelBpm.Name = "_statusLabelBpm";
            this._statusLabelBpm.Size = new System.Drawing.Size(50, 17);
            this._statusLabelBpm.Text = "toolStripStatusLabel1";
            // 
            // _statusMidiDevice
            // 
            this._statusMidiDevice.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._statusMidiDevice.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this._statusMidiDevice.Name = "_statusMidiDevice";
            this._statusMidiDevice.Size = new System.Drawing.Size(58, 17);
            this._statusMidiDevice.Text = "No device";
            // 
            // _textBox
            // 
            this._textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._textBox.Location = new System.Drawing.Point(0, 27);
            this._textBox.Multiline = true;
            this._textBox.Name = "_textBox";
            this._textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textBox.Size = new System.Drawing.Size(337, 221);
            this._textBox.TabIndex = 4;
            this._textBox.TextChanged += new System.EventHandler(this._textBox_TextChanged);
            // 
            // _statusBeatIndex
            // 
            this._statusBeatIndex.AutoSize = false;
            this._statusBeatIndex.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._statusBeatIndex.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this._statusBeatIndex.Name = "_statusBeatIndex";
            this._statusBeatIndex.Size = new System.Drawing.Size(36, 17);
            this._statusBeatIndex.Text = "../..";
            // 
            // FormMidiChord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 273);
            this.Controls.Add(this._textBox);
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
        private System.Windows.Forms.TextBox _textBox;
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
    }
}

