using Open_VTT.Other;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Open_VTT.Classes.Scenes
{
    public class FogOfWar
    {
        public FogState state { get; set; }

        public Point Position { get; set; }
        public Size DrawSize { get; set; }
        public Size BoxSize { get; set; }

        //Poligon removal Data
        public List<Point> PoligonData { get; set; }

        public Image DrawFogOfWarComplete(string imagePath, List<FogOfWar> fogs, Color fogColor)
        {
            var img = Image.FromFile(imagePath);

            foreach (var fog in fogs)
            {
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
            }

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
            var pbImage = image;
            Rectangle outRect = new Rectangle(0, 0, pbImage.Width, pbImage.Height);

            var newPoints = new List<Point>();
            foreach (var p in fog == null ? PoligonData : fog.PoligonData)
            {
                var f = new FogOfWar
                {
                    Position = new Point(p.X, p.Y),
                    BoxSize = BoxSize,
                    DrawSize = DrawSize
                };
                var (PositionX, PositionY, _, _) = PictureBoxHelper.Transform(f, new Size(pbImage.Width, pbImage.Height));
                newPoints.Add(new Point(PositionX, PositionY));
            }


            var action = new Action(() =>
            {
                try
                {
                    using (var myGraphic = Graphics.FromImage(pbImage))
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

        private void RemoveFogOfWarPoligon(Image image, string originalImage, FogOfWar fog = null, bool sync = true)
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

            if (sync)
                action();
            else
                new Task(action).Start();
        }

        public void DrawCircle(Image image)
        {
            var ret = PictureBoxHelper.Transform(this, new Size(image.Width, image.Height));
            var action = (Action)(() =>
            {
                if (image != null)
                    using (Graphics graphics = Graphics.FromImage(image))
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            var steps = 15;
                            graphics.DrawEllipse(new Pen(Color.Purple, 4), ret.PositionX - ((steps * i) / 2), ret.PositionY - ((steps * i) / 2), steps * i, steps * i);
                        }
                    }
            });
            action();
        }
    }
}
