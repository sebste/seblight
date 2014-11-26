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

        public SebLightScreenCapture(SebLightCore core, Size screenSize)
        {
            // Setup schreen capture thread                      
            this.core = core;
            this.screenSize = screenSize;
            this.pauseScreenCap = new ManualResetEvent(false);       // Do start paused!
            this.shutdowsScreenCapEvent = new ManualResetEvent(false);   // Do not start by shutting down - boring
            this.screenCaptureThread = new Thread(new ThreadStart(run));
            this.screenCaptureThread.Start();                       // This will pause rather fast, button must be clicked



        }


        public void run()
        {

            // Capture image bitmaps
            Bitmap screenCapture = new Bitmap(screenSize.Width, screenSize.Height, PixelFormat.Format32bppRgb);
            Bitmap ledCapture = new Bitmap(SebLightCore.leds_top, SebLightCore.leds_side, PixelFormat.Format32bppRgb);
            graphics = Graphics.FromImage(screenCapture);
            smallGraphics = Graphics.FromImage(ledCapture);


            Stopwatch timer = new Stopwatch();      // New stopwatch
            timer.Start();                          // Start timer

            while (true)
            {
                // Check if paused:
                pauseScreenCap.WaitOne(Timeout.Infinite);

                // Check if the thread should exit
                if (shutdowsScreenCapEvent.WaitOne(0))
                    break;


                timer.Restart();        // Restart timer

                graphics.CopyFromScreen(0, 0, 0, 0, screenSize, CopyPixelOperation.SourceCopy);
                smallGraphics.DrawImage(screenCapture, 0, 0, SebLightCore.leds_top, SebLightCore.leds_side);

                //screenCapture.Save("screenCapture.png", ImageFormat.Png);
                //ledCapture.Save("ledCapture.png", ImageFormat.Png);

                int x = SebLightCore.leds_start;
                int y = SebLightCore.leds_side - 1;
                for (int i = 0; i < SebLightCore.nr_of_leds; i++)
                {

                    core.setColor(i, ledCapture.GetPixel(x, y).R, ledCapture.GetPixel(x, y).G, ledCapture.GetPixel(x, y).B);

                    if (i < SebLightCore.leds_start)
                        x--;
                    else if (i < SebLightCore.leds_start + SebLightCore.leds_side - 1)
                        y--;
                    else if (i < SebLightCore.leds_start + SebLightCore.leds_side + SebLightCore.leds_top - 2)
                        x++;
                    else if (i < SebLightCore.leds_start + SebLightCore.leds_side + SebLightCore.leds_top + SebLightCore.leds_bottom_end + 1)
                        y++;
                    else
                        x--;
                }
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
            return !pauseScreenCap.WaitOne(0);
        }


        public void stop()
        {

            // Stop screen capture (and close serial com)
            shutdowsScreenCapEvent.Set();   // <- shutdown serial com as well
            screenCaptureThread.Abort();

        }
    
    }
}
