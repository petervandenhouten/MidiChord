using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidiChord
{
    public partial class StringListDialog : Form
    {
        public StringListDialog(string caption)
        {
            InitializeComponent();
            Text = caption;
        }

        public void SetText(string[] text)
        {
            _listBox.Items.Clear();
            _listBox.Items.AddRange(text);
        }

    }
}
