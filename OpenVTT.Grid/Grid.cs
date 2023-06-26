using System.Drawing;

namespace OpenVTT.Grid
{
    internal class Grid
    {
        public static Image DrawGrid(Image img, bool isPlayer)
        {
            if (!(Settings.Settings.Values.DisplayGrid && isPlayer))
                return img;

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
                    graphics.DrawLine(new Pen(new SolidBrush(Color.Gray)), new Point(posX, 0), new Point(posX, img.Height - 1));
                }

                for (int i = 0; i < countHowManyLinesTopToBottom; i++)
                {
                    var posY = spacingTopBottom + (i * spacingBetweenLeftRightLine);
                    graphics.DrawLine(new Pen(new SolidBrush(Color.Gray)), new Point(0, posY), new Point(img.Width - 1, posY));
                }

                graphics.DrawLine(new Pen(new SolidBrush(Color.Gray)), new Point(0, 0), new Point(0, 0));
            }

            return img;
        }
    }
}
