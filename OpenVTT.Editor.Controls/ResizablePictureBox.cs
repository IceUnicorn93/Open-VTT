using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpenVTT.Editor.Controls
{
    public partial class ResizablePictureBox : PictureBox
    {
        private bool _IsEditable = true;
        public bool IsEditable
        {
            get => _IsEditable;
            set
            {
                _IsEditable = value;
                Controls.OfType<Button>().First().Visible = value;
            }
        }

        public ResizablePictureBox()
        {
            this.ResizeRedraw = true;

            this.ResizeRedraw = true;

            int buttonSize = 15;
            bool isGrabed = false;

            var btn = new Button()
            {
                Size = new Size(buttonSize, buttonSize),
                Location = new Point(this.Width - buttonSize, this.Height - buttonSize),
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom
            };

            btn.MouseDown += (object s, MouseEventArgs e) =>
            {
                isGrabed = true;
                Cursor = Cursors.Hand;
            };
            btn.MouseMove += (object s, MouseEventArgs e) =>
            {
                if (!IsEditable || !isGrabed) return;

                Size = new Size(e.Location.X + Size.Width, e.Location.Y + Size.Height);
            };
            btn.MouseUp += (object s, MouseEventArgs e) =>
            {
                isGrabed = false;
                Cursor = Cursors.Default;
            };
            this.Controls.Add(btn);
        }
    }
}
