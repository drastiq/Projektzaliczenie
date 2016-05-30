using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;




namespace Projektzaliczenie
{
    class screenshoter
    {
        public void screenCapture()
        {

            int screenX;
            int screenY;

            Graphics screen ;
            Screen = screen.CopyFromScreen;
            object PixelFormat = null;
            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                        Screen.PrimaryScreen.Bounds.Height,
                                        PixelFormat.Format32bppArgb);

            screenX = this.gp.DpiX();


            gp.CopyFromScreen(gp.)

        }




    }
}
