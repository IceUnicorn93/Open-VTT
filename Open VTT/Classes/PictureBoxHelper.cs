using Open_VTT.Classes.Scenes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Open_VTT.Classes
{
    class PictureBoxHelper
    {
        public static (int pictureWidth, int pictureHeight, int offsetLeftRight, int offsetTopBottom, float ratio) GetPictureDimensions(FogOfWar fog, Size imageSize)
        {
            if (imageSize == null)
                return (0, 0, 0, 0, 0);

            int height = imageSize.Height;
            int width = imageSize.Width;
            int boxHeight = fog.BoxSize.Height;
            int boxWidth = fog.BoxSize.Width;
            float ratio;
            int pictureHeight;
            int pictureWidth;

            //Note: If you ever read this, i figured this out using excel. just as a fun fact
            var ratioWidth = (float)boxWidth / (float)width;
            var ratioHeight = (float)boxHeight / (float)height;

            if (ratioHeight < ratioWidth)
            {
                //Image is Docking on top and bottom

                ratio = (float)height / (float)boxHeight;
                pictureHeight = boxHeight;
                pictureWidth = (int)((double)width / (double)ratio);
            }
            else
            {
                //Image is Docking left and right

                ratio = (float)width / (float)boxWidth;
                pictureHeight = (int)((double)height / (double)ratio);
                pictureWidth = boxWidth;
            }
            int offsetLeftRight = Math.Abs((boxWidth - pictureWidth) / 2);
            int offsetTopBottom = Math.Abs((boxHeight - pictureHeight) / 2);
            return (pictureWidth, pictureHeight, offsetLeftRight, offsetTopBottom, ratio);
        }

        public static (int PositionX, int PositionY, int DrawWidth, int DrawHeight) Transform(FogOfWar fog, Size imageSize)
        {
            if (imageSize == null)
                return (0, 0, 0, 0);

            var (pictureWidth, pictureHeight, offsetLeftRight, offsetTopBottom, ratio) = GetPictureDimensions(fog, imageSize);
            (int PositionX, int PositionY, int DrawWidth, int DrawHeight) ret = (0, 0, 0, 0);

            ret.PositionX = Math.Abs((int)((double)(fog.Position.X - offsetLeftRight) * (double)ratio));
            ret.PositionY = Math.Abs((int)((double)(fog.Position.Y - offsetTopBottom) * (double)ratio));
            ret.DrawWidth = Math.Abs((int)((double)fog.DrawSize.Width * (double)ratio));
            ret.DrawHeight = Math.Abs((int)((double)fog.DrawSize.Height * (double)ratio));

            return ret;
        }
    }
}
