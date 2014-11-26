using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace seblight
{
    public partial class BrightnessForm : Form
    {
       
        private SeblightForm parentSeb;

        public BrightnessForm(SeblightForm parentSebArg)
        {

            InitializeComponent();
            parentSeb = parentSebArg;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);    // Allow transparent control
            trackBar1.Value = (int)(parentSeb.core.brightness * 100);
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            parentSeb.core.setBrightness((double)trackBar1.Value / (double)100);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
