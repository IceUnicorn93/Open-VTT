using OpenVTT.UiDesigner.Classes;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OpenVTT.UiDesigner.UserControls
{
    public partial class ArtworkInformation : UserControl
    {
        private string _path = "";
        public string path
        {
            get => _path;
            set
            {
                _path = value;
                ArtworkInformation_Load(null, null);
            }
        }

        private ArtInfo _data;
        public ArtInfo data
        {
            get => _data;
            set
            {
                _data = value;
                if (_data == null) return;
                tbArtworkName.DataBindings.Clear();
                tbArtworkName.DataBindings.Add(nameof(tbArtworkName.Text), _data, nameof(_data.Name));

                path = _data.Path;
            }
        }

        private Image image;

        public ArtworkInformation()
        {
            InitializeComponent();
        }

        ~ArtworkInformation()
        {
            if (image != null)
            {
                image.Dispose();
                image = null;

                GC.Collect();
            }
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() != DialogResult.OK) return;
                var file = ofd.FileName;
                if (path == "") return;

                if (image != null)
                {
                    image.Dispose();
                    image = null;

                    GC.Collect();
                }

                var fi = new FileInfo(path);
                var fileNameWithoutExtension = fi.FullName.Replace(fi.Extension, "");
                var fileNameWithPng = fileNameWithoutExtension + ".png";

                File.Copy(file, fileNameWithPng, true);

                image = Image.FromFile(fileNameWithPng);
                pbArtwork.Image = image;
            }
        }

        private void ArtworkInformation_Load(object sender, EventArgs e)
        {
            GC.Collect();

            if (path == "") return;

            var fi = new FileInfo(path);
            var fileNameWithoutExtension = fi.FullName.Replace(fi.Extension, "");
            var fileNameWithPng = fileNameWithoutExtension + ".png";

            if (!File.Exists(fileNameWithPng)) return;

            if (image != null)
            {
                image.Dispose();
                image = null;

                GC.Collect();
            }

            image = Image.FromFile(fileNameWithPng);
            pbArtwork.Image = image;
        }
    }
}
