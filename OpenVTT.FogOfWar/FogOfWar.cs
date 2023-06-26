using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using OpenVTT.Common;

namespace OpenVTT.FogOfWar
{
    public class FogOfWar
    {
        public FogState state { get; set; }

        public Point Position { get; set; }
        public Size DrawSize { get; set; }
        public Size BoxSize { get; set; }

        //Poligon removal Data
        public List<Point> PoligonData { get; set; }

        //Imidiate or Toggle Fog
        public bool IsToggleFog = false;
        public bool IsHidden = true;
        public string Name = "";

        public Image DrawFogOfWarComplete(string imagePath, List<FogOfWar> fogs, Color fogColor, bool IsPlayer)
        {
            var img = Image.FromFile(imagePath);

            foreach (var fog in fogs)
            {
                if (fog.IsToggleFog == true)
                {
                    var drawPoint = new Point();
                    if (fog.PoligonData != null && fog.PoligonData.Count > 0)
                        drawPoint = GetCentroid(fog.PoligonData, fog, img.Size);
                    else
                        drawPoint = GetCentroid(fog, img.Size);

                    if (IsPlayer == false)
                    {
                        using (Graphics graphics = Graphics.FromImage(img))
                        {
                            var stringSize = graphics.MeasureString(fog.Name, new Font("Arial", 12));
                            var point = new Point(drawPoint.X - (int)stringSize.Width / 2, drawPoint.Y - (int)stringSize.Height / 2);
                            graphics.DrawString(fog.Name, new Font("Arial", 12), new SolidBrush(Color.Red), point);
                            graphics.DrawPolygon(new Pen(new SolidBrush(Color.Gray)), TransformPolygonData(FogToPolygonData(fog), fog, img.Size));
                        } 
                    }

                    if(fog.IsHidden == true) continue;
                }

                if (fog.state == FogState.Add)
                {
                    if (fog.PoligonData == null || fog.PoligonData.Count == 0) // Rectangle
                    {
                        DrawFogOfWar(img, fogColor, fog);
                    }
                    else // Poligon
                    {
                        DrawFogOfWarPoligon(img, fogColor, fog);
                    }
                }
                else if (fog.state == FogState.Remove)
                {
                    if (fog.PoligonData == null || fog.PoligonData.Count == 0) // Rectangle
                    {
                        RemoveFogOfWar(img, imagePath, fog);
                    }
                    else // Poligon
                    {
                        RemoveFogOfWarPoligon(img, imagePath, fog);
                    }
                }

                if(IsPlayer == false && fog.IsToggleFog && fog.IsHidden == false)
                {
                    var drawPoint = new Point();
                    if (fog.PoligonData != null && fog.PoligonData.Count > 0)
                        drawPoint = GetCentroid(fog.PoligonData, fog, img.Size);
                    else
                        drawPoint = GetCentroid(fog, img.Size);

                    using (Graphics graphics = Graphics.FromImage(img))
                    {
                        var stringSize = graphics.MeasureString(fog.Name, new Font("Arial", 12));
                        var point = new Point(drawPoint.X - (int)stringSize.Width / 2, drawPoint.Y - (int)stringSize.Height / 2);
                        graphics.DrawString(fog.Name, new Font("Arial", 12), new SolidBrush(Color.Red), point);
                        graphics.DrawPolygon(new Pen(new SolidBrush(Color.Gray)), TransformPolygonData(FogToPolygonData(fog), fog, img.Size));
                    }
                }
            }

            img = Grid.Grid.DrawGrid(img, IsPlayer);

            return img;
        }

        private void DrawFogOfWar(Image image, Color drawColor, FogOfWar fog = null)
        {
            var ret = PictureBoxHelper.Transform(fog ?? this, new Size(image.Width, image.Height));
            var action = (Action)(() =>
            {
                if (image != null)
                    using (Graphics graphics = Graphics.FromImage(image))
                    {
                        SolidBrush solidBrush = new SolidBrush(drawColor);
                        graphics.FillRectangle(solidBrush, new Rectangle(ret.PositionX, ret.PositionY, ret.DrawWidth, ret.DrawHeight));
                    }
            });
            action();
        }

        private void RemoveFogOfWar(Image image, string originalImage, FogOfWar fog = null)
        {
            var ret = PictureBoxHelper.Transform(fog ?? this, new Size(image.Width, image.Height));
            var action = (Action)(() =>
            {
                if (image != null)
                    using (Graphics graphics = Graphics.FromImage(image))
                    {
                        Rectangle rectangle = new Rectangle(ret.PositionX, ret.PositionY, ret.DrawWidth, ret.DrawHeight);
                        using (Image image1 = Image.FromFile(originalImage))
                            graphics.DrawImage(image1, rectangle, rectangle, GraphicsUnit.Pixel);
                    }
            });

            action();
        }

        private void DrawFogOfWarPoligon(Image image, Color drawColor, FogOfWar fog = null)
        {
            Rectangle outRect = new Rectangle(0, 0, image.Width, image.Height);

            var newPoints = new List<Point>();
            foreach (var p in fog == null ? PoligonData : fog.PoligonData)
            {
                var f = new FogOfWar
                {
                    Position = new Point(p.X, p.Y),
                    BoxSize = BoxSize,
                    DrawSize = DrawSize
                };
                var (PositionX, PositionY, _, _) = PictureBoxHelper.Transform(f, new Size(image.Width, image.Height));
                newPoints.Add(new Point(PositionX, PositionY));
            }


            var action = new Action(() =>
            {
                try
                {
                    using (var myGraphic = Graphics.FromImage(image))
                    {
                        SolidBrush solidBrush = new SolidBrush(drawColor);
                        myGraphic.FillPolygon(solidBrush, newPoints.ToArray());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            action();
        }

        private void RemoveFogOfWarPoligon(Image image, string originalImage, FogOfWar fog = null)
        {
            Rectangle outRect = new Rectangle(0, 0, image.Width, image.Height);

            var newPoints = new List<Point>();
            foreach (var p in fog == null ? PoligonData : fog.PoligonData)
            {
                var f = new FogOfWar
                {
                    Position = new Point(p.X, p.Y),
                    BoxSize = fog.BoxSize,
                    DrawSize = fog.DrawSize
                };
                var (PositionX, PositionY, _, _) = PictureBoxHelper.Transform(f, new Size(image.Width, image.Height));
                newPoints.Add(new Point(PositionX, PositionY));
            }


            var action = new Action(() =>
            {
                // Create clipping region from points
                GraphicsPath clipPath = new GraphicsPath();
                clipPath.AddPolygon(newPoints.ToArray());

                try
                {
                    using (var imgF = Image.FromFile(originalImage))
                    {
                        using (var myGraphic = Graphics.FromImage(image))
                        {
                            myGraphic.SmoothingMode = SmoothingMode.HighQuality;
                            myGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            myGraphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                            // Draw foreground image into clipping region
                            myGraphic.SetClip(clipPath, CombineMode.Replace);
                            myGraphic.DrawImage(imgF, outRect);
                            myGraphic.ResetClip();

                            myGraphic.Save();
                        }
                    }
                }
                catch { }
            });

            action();
        }

        public void DrawCircle(Image image)
        {
            var ret = PictureBoxHelper.Transform(this, new Size(image.Width, image.Height));
            var dimensions = PictureBoxHelper.GetPictureDimensions(this, new Size(image.Width, image.Height));

            var action = (Action)(() =>
            {
                if (image != null)
                    using (Graphics graphics = Graphics.FromImage(image))
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            var steps = 15;
                            graphics.DrawEllipse(new Pen(Color.Purple, 4 * dimensions.ratio), ret.PositionX - ((steps * i * dimensions.ratio) / 2), ret.PositionY - ((steps * i * dimensions.ratio) / 2), steps * i * dimensions.ratio, steps * i * dimensions.ratio);
                        }
                    }
            });
            action();
        }

        private Point GetCentroid(FogOfWar fog, Size img)
        {
            var x = PictureBoxHelper.GetPictureDimensions(fog, img);

            var points = FogToPolygonData(fog).ToList();

            return GetCentroid(points, fog, img);
        }

        private Point GetCentroid(List<Point> points, FogOfWar fog, Size img)
        {
            Point GetMiddle(List<Point> pointList)
            {
                var minX = pointList.Select(n => n.X).Min();
                var maxX = pointList.Select(n => n.X).Max();

                var minY = pointList.Select(n => n.Y).Min();
                var maxY = pointList.Select(n => n.Y).Max();

                var middleX = (maxX - minX) / 2 + minX;
                var middleY = (maxY - minY) / 2 + minY;

                return new Point(middleX, middleY);
            }

            var newPoints = new List<Point>();
            foreach (var p in points)
            {
                var f = new FogOfWar
                {
                    Position = new Point(p.X, p.Y),
                    BoxSize = fog.BoxSize,
                    DrawSize = fog.DrawSize
                };
                var (PositionX, PositionY, _, _) = PictureBoxHelper.Transform(f, new Size(img.Width, img.Height));
                newPoints.Add(new Point(PositionX, PositionY));
            }

            var middle = GetMiddle(newPoints);

            var distances = newPoints.Select(n =>
            new
            {
                Distance = Math.Sqrt(Math.Pow(Math.Abs(n.X - middle.X), 2) + Math.Pow(Math.Abs(n.Y - middle.Y), 2)),
                DistanceX = Math.Abs(n.X - middle.X),
                DistanceY = Math.Abs(n.Y - middle.Y),
                P = n
            }).OrderBy(n => n.DistanceX + n.DistanceY)//.ThenBy(n => n.DistanceY) // -> DistanceX + n.DistanceY
            .Select(n => n.P).Take(2).ToList();

            middle = GetMiddle(distances);

            distances = newPoints.Select(n =>
            new
            {
                Distance = Math.Sqrt(Math.Pow(Math.Abs(n.X - middle.X), 2) + Math.Pow(Math.Abs(n.Y - middle.Y), 2)),
                DistanceX = Math.Abs(n.X - middle.X),
                DistanceY = Math.Abs(n.Y - middle.Y),
                P = n
            }).OrderBy(n => n.DistanceX + n.DistanceY)
            .Select(n => n.P).Take(8).ToList();

            middle = GetMiddle(distances);

            return middle;
        }

        private Point[] FogToPolygonData(FogOfWar fog)
        {
            var points = new Point[0];

            if (fog.PoligonData != null && fog.PoligonData.Count > 0)
                points = fog.PoligonData.ToArray();
            else
            {
                points = new List<Point>()
                {
                    new Point(fog.Position.X, fog.Position.Y),
                    new Point(fog.Position.X, fog.Position.Y + fog.DrawSize.Height),
                    new Point(fog.Position.X + fog.DrawSize.Width, fog.Position.Y + fog.DrawSize.Height),
                    new Point(fog.Position.X + fog.DrawSize.Width, fog.Position.Y),
                }.ToArray();
            }

            return points;
        }

        private Point[] TransformPolygonData(Point[] points, FogOfWar fog, Size img)
        {
            var newPoints = new List<Point>();
            foreach (var p in points)
            {
                var f = new FogOfWar
                {
                    Position = new Point(p.X, p.Y),
                    BoxSize = fog.BoxSize,
                    DrawSize = fog.DrawSize
                };
                var (PositionX, PositionY, _, _) = PictureBoxHelper.Transform(f, new Size(img.Width, img.Height));
                newPoints.Add(new Point(PositionX, PositionY));
            }

            return newPoints.ToArray();
        }
    }
}
