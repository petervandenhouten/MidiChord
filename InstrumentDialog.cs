using System;
using System.Windows.Forms;

namespace Sanford.Multimedia.Midi.UI
{
    public partial class InstrumentDialog : Form
    {
        public InstrumentDialog()
        {
            InitializeComponent();

            FillInstrumentComboBox();

            instrumentComboBox.SelectedIndex = 0;
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
    }
}
