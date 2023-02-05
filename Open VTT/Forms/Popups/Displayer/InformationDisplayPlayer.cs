﻿using Open_VTT.Classes;
using Open_VTT.Controls;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Open_VTT.Forms.Popups.Displayer
{
    public partial class InformationDisplayPlayer : Form
    {
        // Singleton instance
        private static InformationDisplayPlayer currentPlayerWindow = null;

        internal InformationDisplayPlayer()
        {
            InitializeComponent();

            var screen = Settings.Values.Screens.SingleOrDefault(n => n.Display == Other.DisplayType.InformationDisplayPlayer);
            if (screen == null) return;
            this.Location = new Point(screen.PositionX, screen.PositionY);
            this.Size = new Size(screen.Width, screen.Height);

            displayImagePictureBox.DrawMode = Other.PictureBoxMode.Ping;
        }

        internal DrawingPictureBox GetPictureBox()
        {
            return displayImagePictureBox;
        }
    }
}
