using OpenVTT.Logging;
using System;
using System.Drawing;
using System.Linq;

namespace OpenVTT.Grid
{
    internal class Grid
    {
        public static Image DrawGrid(Image img, bool IsDM)
        {
            Logger.Log("Class: Grid | DrawGrid");

            var condition =
                    (Settings.Settings.Values.DisplayGrid && Settings.Settings.Values.DisplayGridForDM && IsDM)
                        ||
                    (Settings.Settings.Values.DisplayGrid && IsDM == false);

            if (img == null) return img;

            if (!condition)
                return img;

            var playerScreen = Settings.Settings.Values.Screens.First(n => n.Display == Common.DisplayType.Player);

            //Calculate Display Values
            var displayWidth = playerScreen.Width;
            var displayHeight = playerScreen.Height;
            var displaySize = Settings.Settings.Values.PlayerScreenSize;

            var diagonalPixel = Math.Sqrt(Math.Pow(displayWidth, 2) + Math.Pow(displayHeight, 2));
            var oneInch = diagonalPixel / displaySize;

            //Get Scaled Size of the Image for the Display
            var imageWidth = img.Width;
            var imageHeight = img.Height;

            var newSize = ScaleImageSize(new Size(imageWidth, imageHeight), displayWidth, displayHeight);

            //Caluclate Offset left and top for even Spacing of lines
            var calcHowManyFromLeftToRight = (int)Math.Floor(newSize.Width / oneInch);
            var calcHowManyFromTopToBottom = (int)Math.Floor(newSize.Height / oneInch);

            var spacingLeftAndRight = (int)Math.Floor((newSize.Width - calcHowManyFromLeftToRight * Math.Floor(oneInch)) / 2);
            var spacingTopAndBottom = (int)Math.Floor((newSize.Height - calcHowManyFromTopToBottom * Math.Floor(oneInch)) / 2);

            //Bring Image to the SIze of the Monitor
            img = ScaleImage((Bitmap)img, displayWidth, displayHeight);

            //Draw Grid in the Size for the Monitor
            img = DrawGridOnBitmap((Bitmap)img, newSize, spacingLeftAndRight, spacingTopAndBottom, calcHowManyFromLeftToRight, calcHowManyFromTopToBottom, (int)Math.Floor(oneInch));

            //Scale it back down to original Size
            img = ScaleImage((Bitmap)img, imageWidth, imageHeight);

            return img;
        }

        //Adjusted it to my needs for just scaling
        private static Size ScaleImageSize(Size imageSize, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / imageSize.Width;
            var ratioY = (double)maxHeight / imageSize.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(imageSize.Width * ratio);
            var newHeight = (int)(imageSize.Height * ratio);

            return new Size(newWidth, newHeight);
        }

        //Thanks to this website: https://efundies.com/scale-an-image-in-c-sharp-preserving-aspect-ratio/
        private static Bitmap ScaleImage(Bitmap bmp, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / bmp.Width;
            var ratioY = (double)maxHeight / bmp.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(bmp.Width * ratio);
            var newHeight = (int)(bmp.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(bmp, 0, 0, newWidth, newHeight);

            return newImage;
        }

        //Draw the Grid on the up scaled Image
        private static Bitmap DrawGridOnBitmap(Bitmap bmp, Size size, int spacingLeft, int spacingTop, int countColumns, int countRows, int inchSize)
        {
            using (var g = Graphics.FromImage(bmp))
            {
                //draw Rows
                for (int i = 0; i < countRows + 1; i++)
                {
                    var pos = spacingTop + (i * inchSize);
                    g.DrawLine(new Pen(new SolidBrush(Settings.Settings.Values.GridColor)), 0, pos, size.Width, pos);
                }

                //draw Columns
                for (int i = 0; i < countColumns + 1; i++)
                {
                    var pos = spacingLeft + (i * inchSize);
                    g.DrawLine(new Pen(new SolidBrush(Settings.Settings.Values.GridColor)), pos, 0, pos, size.Height);
                }
            }

            return bmp;
        }

    }
}
