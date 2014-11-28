using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace seblight
{
    public class SebLightScreenCapture
    {


        // Bitmap capture graphic objects
        Graphics graphics;
        Graphics smallGraphics;
        Thread screenCaptureThread;                     // Thread for screen capture
        ManualResetEvent pauseScreenCap;                // Reset event to pause screen capture
        ManualResetEvent shutdowsScreenCapEvent;        // Exit screen capture in a safe way


        SebLightCore core;                              // Serial communication core
        Size screenSize;                                // True pixel screen size

        // Refresh rate timer
        const int refreshRateMin = 40;                  // Minimum wait between screendumps
        ManualResetEvent refreshRateWaitEvent;          // Do not start new capture before released
        System.Windows.Forms.Timer refrestRateTimer;    // Timer for min refresh rate

        public SebLightScreenCapture(SebLightCore core, Size screenSize)
        {
            // Setup schreen capture thread                      
            this.core = core;
            this.screenSize = screenSize;
            this.pauseScreenCap = new ManualResetEvent(false);              // Do start paused!
            this.shutdowsScreenCapEvent = new ManualResetEvent(false);      // Do not start by shutting down - boring
            this.screenCaptureThread = new Thread(new ThreadStart(run));
            this.screenCaptureThread.Start();                               // This will pause rather fast, button must be clicked

            this.refreshRateWaitEvent = new ManualResetEvent(false);        // Block until refreshRateWaitTimer ticks
            this.refrestRateTimer = new System.Windows.Forms.Timer();       // Refresh rate timer
            this.refrestRateTimer.Tick += new EventHandler(refreshScreenEventProcessor);    // Set event proeessor
            this.refrestRateTimer.Interval = refreshRateMin;                // Set desired interval
            this.refrestRateTimer.Start();
        }


        public void run()
        {

            // Capture image bitmaps
            Bitmap screenCapture = new Bitmap(screenSize.Width, screenSize.Height, PixelFormat.Format32bppRgb);
            Bitmap ledCapture = new Bitmap(SebLightCore.leds_top, SebLightCore.leds_side, PixelFormat.Format32bppRgb);
            graphics = Graphics.FromImage(screenCapture);
            smallGraphics = Graphics.FromImage(ledCapture);


            while (true)
            {
                // Check if paused:
                pauseScreenCap.WaitOne(Timeout.Infinite);

                // Check if the thread should exit
                if (shutdowsScreenCapEvent.WaitOne(0))
                {
                    break;
                }

                refreshRateWaitEvent.WaitOne(); // Wait for refreshrate timer to reset
                refreshRateWaitEvent.Reset();   // Block next time
                refrestRateTimer.Start();

                graphics.CopyFromScreen(0, 0, 0, 0, screenSize, CopyPixelOperation.SourceCopy);
                smallGraphics.DrawImage(screenCapture, 0, 0, SebLightCore.leds_top, SebLightCore.leds_side);

                showImage(ledCapture);
            }
        }



        void showImage(Bitmap ledCapture) { 

            int ledNo = 0;
            int y = SebLightCore.leds_side - 1;
            int x = SebLightCore.leds_start;

            // Bottom start
            for (x = SebLightCore.leds_start; x >= 0; x--) {
                core.setColor(ledNo++, ledCapture.GetPixel(x, y).R, ledCapture.GetPixel(x, y).G, ledCapture.GetPixel(x, y).B);
            }
            x = 0;

            // Left
            for (y = SebLightCore.leds_side - 1; y >= 0; y--)
            {
                core.setColor(ledNo++, ledCapture.GetPixel(x, y).R, ledCapture.GetPixel(x, y).G, ledCapture.GetPixel(x, y).B);
            }
            y = 0;
            
            // top
            for (x = 0; x < SebLightCore.leds_top - 1; x++)
            {
                core.setColor(ledNo++, ledCapture.GetPixel(x, y).R, ledCapture.GetPixel(x, y).G, ledCapture.GetPixel(x, y).B);
            }
            x = SebLightCore.leds_top - 1;

            // Right
            for (y = 0; y < SebLightCore.leds_side; y++)
            {
                core.setColor(ledNo++, ledCapture.GetPixel(x, y).R, ledCapture.GetPixel(x, y).G, ledCapture.GetPixel(x, y).B);
            }
            y = SebLightCore.leds_side - 1;

            // Bottom End
            for (x = SebLightCore.leds_top - 1; ledNo < SebLightCore.nr_of_leds; x--)
            {
                core.setColor(ledNo++, ledCapture.GetPixel(x, y).R, ledCapture.GetPixel(x, y).G, ledCapture.GetPixel(x, y).B);
            }


     
        }
    


        public void start()
        {
            if (!pauseScreenCap.WaitOne(0))
            {
                pauseScreenCap.Set(); // Unpause the screen capture (It's all ready stareted!)
            }
        }

        public void pause()
        {
            pauseScreenCap.Reset(); // Pause screen capture
            Thread.Sleep(60);       // Wait for the thread to finish
            core.setAllColors(0, 0, 0);  // Go black
        }

        public bool isRunning()
        {
            return pauseScreenCap.WaitOne(0);
        }


        public void stop()
        {

            // Stop screen capture (and close serial com)
            shutdowsScreenCapEvent.Set();   // <- shutdown serial com as well
            screenCaptureThread.Abort();

        }
    

        // This is the method to run when the timer is raised. 
        private void refreshScreenEventProcessor(Object myObject,EventArgs myEventArgs)
        {
            refreshRateWaitEvent.Set();
        }


    }
}
