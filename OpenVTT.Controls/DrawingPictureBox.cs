using OpenVTT.Common;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenVTT.Controls
{
    internal partial class DrawingPictureBox : PictureBox
    {
        internal RectangleDrawComplete RectangleComplete { get; set; }
        internal PointDrawComplete PointComplete { get; set; }
        internal PoligonDrawComplete PoligonComplete { get; set; }

        Rectangle drawingRectangle { get; set; }
        Point drawingRectangleMousePoint { get; set; }

        Point pingPoing { get; set; }

        List<Point> poligonPoints { get; set; }
        Point poligonMousePosition { get; set; }
        bool poligonIsDrawing { get; set; }


        PictureBoxMode _drawMode;
        public PictureBoxMode DrawMode
        {
            get => _drawMode;
            set
            {
                _drawMode = value;
                poligonPoints.Clear();
                Invalidate();
            }
        }

        public DrawingPictureBox()
        {
            InitializeComponent();

            poligonPoints = new List<Point>();

            this.DoubleBuffered = true;

            DrawMode = PictureBoxMode.Rectangle;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            switch (DrawMode)
            {
                case PictureBoxMode.Ping:

                    break;
                case PictureBoxMode.Rectangle:
                    if (drawingRectangle.X != 0 || drawingRectangle.Y != 0 || drawingRectangle.Height != 0 || drawingRectangle.Width != 0)
                        e.Graphics.DrawRectangle(new Pen(Color.Red, 4), drawingRectangle);

                    break;
                case PictureBoxMode.Poligon:

                    if (poligonIsDrawing == true && poligonPoints.Count > 0)
                    {
                        for (int i = 0; i < poligonPoints.Count; i++)
                        {
                            if (i > 0) e.Graphics.DrawLine(new Pen(Color.Red, 2), poligonPoints[i - 1], poligonPoints[i]);
                            e.Graphics.DrawEllipse(new Pen(Color.Red, 2), poligonPoints[i].X - 3, poligonPoints[i].Y - 3, 6, 6);
                        }
                        e.Graphics.DrawEllipse(new Pen(Color.Red, 2), poligonMousePosition.X - 3, poligonMousePosition.Y - 3, 6, 6);
                        e.Graphics.DrawLine(new Pen(Color.Red, 2), poligonPoints.Last(), poligonMousePosition);
                        e.Graphics.DrawLine(new Pen(Color.Red, 2), poligonPoints.First(), poligonMousePosition);
                    }
                    else if (poligonIsDrawing == false && poligonPoints.Count > 0)
                    {
                        poligonIsDrawing = true;
                        PoligonComplete?.Invoke(poligonPoints.ToArray());
                        poligonPoints.Clear();
                    }
                    break;
                default:
                    break;
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            switch (DrawMode)
            {
                case PictureBoxMode.Ping:
                    if (e.Button == MouseButtons.Left)
                    {
                        PointComplete?.Invoke(pingPoing);
                        new Task(() =>
                        {
                            Thread.Sleep(2_000);
                            pingPoing = new Point(-1, -1);
                            Invalidate();
                            PointComplete?.Invoke(pingPoing);
                        }).Start();
                    }
                    break;
                case PictureBoxMode.Rectangle:
                    if (e.Button == MouseButtons.Left)
                    {
                        RectangleComplete?.Invoke(drawingRectangle);

                        drawingRectangle = new Rectangle(0, 0, 0, 0);
                        Invalidate();
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        PointComplete?.Invoke(pingPoing);
                        new Task(() =>
                        {
                            Thread.Sleep(2_000);
                            pingPoing = new Point(-1, -1);
                            Invalidate();
                            PointComplete?.Invoke(pingPoing);
                        }).Start();
                    }
                    break;
                case PictureBoxMode.Poligon:
                    break;
                default:
                    break;
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            switch (DrawMode)
            {
                case PictureBoxMode.Ping:
                    if (e.Button == MouseButtons.Left)
                    {
                        pingPoing = new Point(e.X, e.Y);
                        Invalidate();
                    }
                    break;
                case PictureBoxMode.Rectangle:
                    if (e.Button == MouseButtons.Left)
                    {
                        drawingRectangleMousePoint = new Point(e.X, e.Y);
                        Invalidate();
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        pingPoing = new Point(e.X, e.Y);
                        Invalidate();
                    }
                    break;
                case PictureBoxMode.Poligon:
                    if (e.Button == MouseButtons.Left)
                    {
                        if (poligonIsDrawing == false)
                        {
                            poligonIsDrawing = true;
                            poligonPoints.Clear();
                        }

                        poligonPoints.Add(new Point(e.X, e.Y));
                        Invalidate();
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        poligonPoints.Add(new Point(e.X, e.Y));
                        poligonIsDrawing = false;
                        Invalidate();
                    }
                    break;
                default:
                    break;
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            switch (DrawMode)
            {
                case PictureBoxMode.Rectangle:
                    if (e.Button == MouseButtons.Left)
                    {
                        var MinX = (new int[] { drawingRectangleMousePoint.X, e.X }).Min();
                        var MinY = (new int[] { drawingRectangleMousePoint.Y, e.Y }).Min();
                        var MaxX = (new int[] { drawingRectangleMousePoint.X, e.X }).Max();
                        var MaxY = (new int[] { drawingRectangleMousePoint.Y, e.Y }).Max();

                        drawingRectangle = new Rectangle(MinX, MinY, MaxX - MinX, MaxY - MinY);

                        Invalidate();
                    }
                    break;
                case PictureBoxMode.Poligon:
                    poligonMousePosition = new Point(e.X, e.Y);

                    if (e.Button == MouseButtons.Left)
                        poligonPoints.Add(new Point(e.X, e.Y));

                    Invalidate();
                    break;
                default:
                    break;
            }
        }

        public void SetPingPoint(Point point)
        {
            pingPoing = point;
            Invalidate();
        }
    }
}
