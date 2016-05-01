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
    public partial class MainForm : Form
    {
        private Capture capture;
        Image<Bgr, Byte> currentFrame;
        Boolean usingCamera = false;
        SudokuDetector detector = new SudokuDetector();

        public MainForm()
        {
            InitializeComponent();
            currentFrame = new Image<Bgr, byte>("C:\\Users\\wind\\Desktop\\sudoku-original.jpg");
            imageBox.Image = currentFrame;
            detector.SetGrayImage(currentFrame.Convert<Gray, byte>());
        }

        private void browseImageBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Image Files (*.bmp, *.jpg)|*.bmp;*.jpg|All files (*.*)|*.*",
                FilterIndex = 0,
                Title = "Choose an image file"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    currentFrame = new Image<Bgr, byte>(dialog.FileName);
                    imageBox.Image = currentFrame;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Cannot open file {0}/r/n{1}", dialog.SafeFileName, ex));
                }
            }
        }

        private void stopCamera()
        {
            if (capture != null)
            {
                useWebcamBtn.Text = "Use camera";
                usingCamera = false;
                Application.Idle -= ProcessFrame;
                capture.Dispose();
                capture = null;
            }
        }

        private void useWebcamBtn_Click(object sender, EventArgs e)
        {
            if (!usingCamera)
                startCamera();
            else
                stopCamera();
        }

        private void startCamera()
        {
            capture = new Capture();
            capture.FlipHorizontal = true;
            Application.Idle += ProcessFrame;
            usingCamera = true;
            useWebcamBtn.Text = "Stop using camera";
        }

        private void ProcessFrame(object sender, EventArgs arg)
        {
            currentFrame = capture.QueryFrame().ToImage<Bgr, byte>();
            imageBox.Image = currentFrame;
            detector.SetGrayImage(currentFrame.Convert<Gray, byte>());
            captureBtn.PerformClick();
        }

        private void captureBtn_Click(object sender, EventArgs e)
        {
            detector.GridDetection();
        }
    }
}
