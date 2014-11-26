



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;           // Ip address and stuff
using System.Net.Sockets;   // TCP listener
using System.Threading;     // Threading
using System.Threading.Tasks;
using System.IO.Ports;      // Serial
using System.Diagnostics;   // Stopwatch
using System.Drawing.Imaging;
using System.Windows.Input; // Mouse button behavior





namespace seblight
{
    public partial class SeblightForm : Form
    {




        //Brightnessform
        BrightnessForm brightnessForm;                  // Create instance of form

        // Move form by mouse
        bool mouseButtonDown;                           // Move form if true;

        // Make new SebLight class, this has all the good stuff!
        public SebLightCore core;
        private SebLightScreenCapture screenCapture;

        //---------------------------------------------------------------------------
        // Seblightform (Class)
        public SeblightForm()
        {

            //*****************************************************************************
            // Initialize SebLights
            //*****************************************************************************
            InitializeComponent();      // Make instance
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);    // Allow transparent control

            core = new SebLightCore();
            screenCapture = new SebLightScreenCapture(core);

            mouseButtonDown = false;   // We do not start by moving the mouse

        }   // End SebLight Class constructor





        public void closeSeblightForm()
        {

            // Close window and delete stuff
            screenCapture.stop();
            core.close();
            Application.Exit();
        }



        //*****************************************************************************
        // Form functions
        //*****************************************************************************
        private void testLedButton_Click(object sender, EventArgs e)
        {

            Stopwatch timer = new Stopwatch();      // New stopwatch
            timer.Start();                          // Start timer
            try
            {
                core.testColors();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);
            }


            timer.Stop();   // Stop timer

        }

        private void close_button_Click(object sender, EventArgs e)
        {
            closeSeblightForm();
        }

        private void set_color_button_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.AnyColor = true;

            if (screenCapture.isRunning())
            {
                screenCapture.pause();
                capture_button.Text = "Start screen capture";
                Thread.Sleep(60);           // Give the screen capture thread some time to finish

                // Set colors
                core.setAllColors(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
            }
        }

        private void capture_button_Click(object sender, EventArgs e)
        {
            if (screenCapture.isRunning())
            {
                screenCapture.start();   // Unpause the screen capture (It's all ready stareted!)
                capture_button.Text = "Stop screen capture";
            }
            else
            {
                screenCapture.pause();   // Pause screen capture
                capture_button.Text = "Start screen capture";
            }
        }

        private void brightness_button_Click_1(object sender, EventArgs e)
        {
            // New form
            brightnessForm = new BrightnessForm(this);
            brightnessForm.Show(this);
        }

        private void SeblightForm_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left))
            {
                mouseButtonDown = true;     // Form can be moved
            }
        }

        private void SeblightForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseButtonDown)
            {
                Point mousePos;     // A point relative to the screen¨¨
                mousePos = PointToScreen(e.Location);
                SeblightForm.ActiveForm.Top = mousePos.Y;// +mousePosClient.Y;
                SeblightForm.ActiveForm.Left = mousePos.X;
            }
        }

        private void SeblightForm_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left))
            {
                mouseButtonDown = false;     // Form can be moved
            }
        }

        private void SeblightForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeSeblightForm();
        }

    }   
}   





