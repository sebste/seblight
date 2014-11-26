using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;      // Serial
using System.Diagnostics;   // Stopwatchw
using System.Windows.Forms;

namespace seblight
{

    public class SebLightCore
    {

        //*****************************************************************************
        // SebLight variables
        //*****************************************************************************

        // LED nr and such
        public const int nr_of_leds = 103;               // Total number of leds
        public const int leds_side = 19;                 // Leds at each side
        public const int leds_top = 33;                  // Leds at the top
        public const int leds_start = 16;                // Leds at the bottom start
        public const int leds_bottom_end = 15;           // Leds at the bottom end

        // Serial comm stuff
        string comPort = "COM3";            // Com port, can be changed
        byte updateByte = (byte)(230);      // UpdateByte
        byte controlByte = (byte)(240);     // Control byte
        byte[] code = new byte[5];          // Storage for sending
        byte[] updateCode = new byte[1];    // Storage for sending

        // Color storage
        int[] red = new int[nr_of_leds];
        int[] green = new int[nr_of_leds];
        int[] blue = new int[nr_of_leds];
        public double brightness;                       // Easy access from shild :)

        // Serial communications stuff
        static SerialPort serial;                       // Serial com

        // Update timer
        Timer updateTimer;
        int updateInterval = 25;                        // 40Hz             

        //*****************************************************************************
        // SebLight Constructor
        //*****************************************************************************
        public SebLightCore()
        {
            // Prepare update timer
            updateTimer = new Timer();

            // Start serial
            serial = new SerialPort();
            serial.PortName = comPort;
            startSerial();

            // Define handy variables
            code[0] = controlByte;          // First byte is allways controlbyte
            updateCode[0] = updateByte;     // Update LEDS when this byte is received
            brightness = 1.0;                 // We'll start with full (1.00) in brightness
        }


        //*****************************************************************************
        // SebLight functions
        //*****************************************************************************

        public void startSerial()
        {

            // start serial 
            try
            {
                serial.Open();
                lightsOff();        // Sets color to 0;


                // Start update timer if serial ok
                updateTimer.Tick += new EventHandler(updateTimerEvent);
                updateTimer.Interval = updateInterval; // ms
                updateTimer.Start();


            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);
            }


        }

        public void sendColor(int led_nr)
        {
            code[1] = (byte)(led_nr + 1);	        // Led nr
            code[2] = (byte)Math.Floor(((red[led_nr] + 1) * brightness));
            code[3] = (byte)Math.Floor(((green[led_nr] + 1) * brightness));
            code[4] = (byte)Math.Floor(((blue[led_nr] + 1) * brightness));
            serial.Write(code, 0, 5);           // Send to AVR
        }

        public void sendAllColors()                 // Send all, and update
        {
            for (int i = 0; i < nr_of_leds; i++)
            {
                sendColor(i);
            }
            updateColors();
        }

        public void updateColors()
        {            // Update all colors
            serial.Write(updateCode, 0, 1);
        }

        public void setColor(int led_nr, int redIn, int greenIn, int blueIn)
        {
            if (redIn == 255)
                redIn--;
            if (greenIn == 255)
                greenIn--;
            if (blueIn == 255)
                blueIn--;
            red[led_nr] = redIn;        // (byte)Math.Floor((redIn * brightness) + 0.5);
            green[led_nr] = greenIn;    //(byte)Math.Floor((greenIn * brightness) + 0.5);
            blue[led_nr] = blueIn;      // (byte)Math.Floor((blueIn * brightness) + 0.5);
        }

        public void setAllColors(int redIn, int greenIn, int blueIn)
        {
            if (redIn == 255)
                redIn--;
            if (greenIn == 255)
                greenIn--;
            if (blueIn == 255)
                blueIn--;
            for (int i = 0; i < nr_of_leds; i++)
            {
                red[i] = redIn;         // (byte)Math.Floor((redIn) + 0.5);
                green[i] = greenIn;      // (byte)Math.Floor((greenIn) + 0.5);
                blue[i] = blueIn;      // (byte)Math.Floor((blueIn) + 0.5);
            }
            //   sendAllColors();
        }

        public void testColors()
        {
            //Red
            for (int i = 1; i < nr_of_leds; i++)
            {
                setColor(i, 255, 0, 0);
                sendColor(i);
                updateColors();
            }
            //Green
            for (int i = 1; i < nr_of_leds; i++)
            {
                setColor(i, 0, 255, 0);
                sendColor(i);
                updateColors();
            }
            //Blue
            for (int i = 1; i < nr_of_leds; i++)
            {
                setColor(i, 0, 0, 255);
                sendColor(i);
                updateColors();
            }
            //Off
            for (int i = 1; i < nr_of_leds; i++)
            {
                setColor(i, 0, 0, 0);
                sendColor(i);
                updateColors();
            }
        }

        public void lightsOff()
        {
            for (int i = 0; i < nr_of_leds; i++)	// Set colors to 0
            {
                red[i] = (byte)(0);
                green[i] = (byte)(0);
                blue[i] = (byte)(0);
            }
            updateColors();
        }

        public void floatColors()
        {

        }

        public void setBrightness(double brightnessIn)
        {
            this.brightness = brightnessIn;
        }


        public void close()
        {
            if (serial.IsOpen)
            {
                lightsOff();
                System.Threading.Thread.Sleep(200);
                updateTimer.Stop();
                serial.Close();
            }
        }

        // This is the method to run when the timer is raised. 
        private void updateTimerEvent(Object myObject, EventArgs myEventArgs)
        {
            sendAllColors();
        }
    }
}
