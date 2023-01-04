using System.Drawing;

namespace Open_VTT.Other
{
    //Fog of War Delegates
    internal delegate void Notify(Image img);

    //DrawingPictureBox Delegates
    internal delegate void RectangleDrawComplete(Rectangle rect);
    internal delegate void PointDrawComplete(Point point);
    internal delegate void PoligonDrawComplete(Point[] point);

    //Open Recently Delegates
    internal delegate void SessionLoad(string Path);
}
