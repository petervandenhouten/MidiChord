using System;
using System.Windows.Forms;

namespace Sanford.Multimedia.Midi.UI
{
    public partial class BeatDialog : Form
    {
        private int _tapping = 0;
        private long _tapTick = 0;

        public BeatDialog()
        {
            InitializeComponent();
        }

        private void buttonTap_Click(object sender, EventArgs e)
        {
            if (_tapping == 0)
            {
                _tapTick = DateTime.Now.Ticks;
            }

            _tapping++;

            if ( _tapping == 4 )
            {
                // calculate bpm over four taps
                long tapDelay = DateTime.Now.Ticks - _tapTick;

                TimeSpan elapsedSpan = new TimeSpan(tapDelay);

                _numericBpm.Value = Convert.ToDecimal((60 * 1000) / ( elapsedSpan.TotalMilliseconds/ 4));

                // stop tapping
                _tapping = 0;
            }
        }

        public int BeatsPerMinute
        {
            get
            {
                #region Require

                if (_numericBpm == null )
                {
                    throw new InvalidOperationException();
                }

                #endregion

                return Convert.ToInt32(_numericBpm.Value);
            }
            set
            {
                _numericBpm.Value = Convert.ToInt32(value);
            }
        }
    }
}
