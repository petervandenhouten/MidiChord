using MidiChord;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Sanford.Multimedia.Midi.UI
{
    public partial class InstrumentDialog : Form
    {
        private ChordLivePlayer _midiPlayer;

        public InstrumentDialog(ChordLivePlayer midiPlayer)
        {
            InitializeComponent();

            FillInstrumentComboBox();

            instrumentComboBox.SelectedIndex = 0;
            _midiPlayer = midiPlayer;
        }

        private void FillInstrumentComboBox()
        {
            instrumentComboBox.Items.Clear();
            foreach (GeneralMidiInstrument instrument in Enum.GetValues(typeof(GeneralMidiInstrument)))
            {
                instrumentComboBox.Items.Add(instrument.ToString());
            }
        }

        public GeneralMidiInstrument Instrument
        {
            get
            {
                #region Require

                if (instrumentComboBox.SelectedIndex < 0)
                {
                    throw new InvalidOperationException();
                }

                #endregion

                return (GeneralMidiInstrument)instrumentComboBox.SelectedIndex;
            }
            set
            {
                instrumentComboBox.SelectedIndex = (int)value;
            }
        }

        private void buttonPreviewSound_Click(object sender, EventArgs e)
        {
            PreviewInstrument();
        }

        private void PreviewInstrument()
        {
            if (_midiPlayer != null)
            {
                _midiPlayer.Mute();
            }

            var i = instrumentComboBox.Text;
            foreach (GeneralMidiInstrument instrument in Enum.GetValues(typeof(GeneralMidiInstrument)))
            {
                if (instrument.ToString() == i)
                {
                    if (_midiPlayer != null)
                    {
                        _midiPlayer.PlayNote(instrument);
                    }
                }
            }

        }

        private void InstrumentDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_midiPlayer != null)
            {
                _midiPlayer.Mute();
            }
        }

        private void instrumentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // PreviewInstrument();
        }
    }
}
