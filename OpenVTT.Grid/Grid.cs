using OpenVTT.Logging;
using System.Drawing;

namespace OpenVTT.Grid
{
    internal class Grid
    {
        public static Image DrawGrid(Image img, bool IsDM)
        {
            Logger.Log("Class: Grid | DrawGrid");

            var con = (Settings.Settings.Values.DisplayGrid && Settings.Settings.Values.DisplayGridForDM && IsDM)
                        ||
                      (Settings.Settings.Values.DisplayGrid && IsDM == false);

            if (img == null) return img;

            if (!con)
                return img;

            if(Settings.Settings.Values.PlayerScreenSize == 0) return img;
            if(Settings.Settings.Values.PlayerScreenWidthInches == 0) return img;
            if(Settings.Settings.Values.PlayerScreenHeightInces == 0) return img;

            int countHowManyLinesLeftToRight = (int)Settings.Settings.Values.PlayerScreenWidthInches;
            int countHowManyLinesTopToBottom = (int)Settings.Settings.Values.PlayerScreenHeightInces;

            var spacingBetweenTopBottomLine = img.Width / countHowManyLinesLeftToRight; 
            var spacingBetweenLeftRightLine = img.Height / countHowManyLinesTopToBottom;

            var spacingLeftRight = (img.Width - (spacingBetweenTopBottomLine * countHowManyLinesLeftToRight)) / 2;
            var spacingTopBottom = (img.Height - (spacingBetweenLeftRightLine * countHowManyLinesTopToBottom)) / 2;

            using (Graphics graphics = Graphics.FromImage(img))
            {
                for (int i = 0; i < countHowManyLinesLeftToRight; i++)
                {
                    var posX = spacingLeftRight + (i * spacingBetweenTopBottomLine);
                    graphics.DrawLine(new Pen(new SolidBrush(Settings.Settings.Values.GridColor)), new Point(posX, 0), new Point(posX, img.Height - 1));
                }

                for (int i = 0; i < countHowManyLinesTopToBottom; i++)
                {
                    var posY = spacingTopBottom + (i * spacingBetweenLeftRightLine);
                    graphics.DrawLine(new Pen(new SolidBrush(Settings.Settings.Values.GridColor)), new Point(0, posY), new Point(img.Width - 1, posY));
                }
            }

            return img;
        }
    }
}
