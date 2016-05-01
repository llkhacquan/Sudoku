using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_grabber
{
    public partial class ImageShowCase : Form
    {
        static List<ImageShowCase> instances = new List<ImageShowCase>();

        public ImageShowCase()
        {
            InitializeComponent();
            this.Text = "ImageShowcase";
        }

        static public void ShowImage(Image<Bgr, byte> image, string title = null)
        {

            ShowImage(image.Mat, title);
        }

        internal static void ShowImage(Mat image, string title = null)
        {
            if (title == null)
            {
                title = "ImageShowcase";
            }

            ImageShowCase imageShowCase = null;
            foreach (var showcase in instances)
            {
                if (showcase.Text == title)
                {
                    imageShowCase = showcase;
                    break;
                }
            }
            if (imageShowCase == null)
            {
                imageShowCase = new ImageShowCase();
                imageShowCase.Text = title;
                instances.Add(imageShowCase);
            }
            imageShowCase.imageBox.Image = image;
            if (!imageShowCase.Visible)
                imageShowCase.Show();
        }

        private void ImageShowCase_FormClosing(object sender, FormClosingEventArgs e)
        {
            instances.Remove(this);
        }
    }
}
