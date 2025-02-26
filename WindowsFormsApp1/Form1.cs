using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1: Form
    {
        private Bitmap selectedImage;
        public Form1()
        {
            InitializeComponent();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files|*.png;*.jpg;*.bmp|All files(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap image = new Bitmap(fileDialog.FileName);
                selectedImage = image;
                pictureBox.Image = selectedImage;

                pictureBox.Refresh();
            }
        }

        private void inverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvertFilter filter = new InvertFilter();
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (selectedImage != null)
            {
                Bitmap newImage = ((Action)e.Argument).processImage(selectedImage, backgroundWorker);
                if (backgroundWorker.CancellationPending != true)
                    selectedImage = newImage;
            }
            else
                MessageBox.Show("Выберите изображение");
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox.Image = selectedImage;
                pictureBox.Refresh();
            }
            progressBar.Value = 0;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
        }

        private void blurToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void meanBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BlurFilter filter = new BlurFilter();
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void gaussianBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GaussianFilter filter = new GaussianFilter();
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void grayScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GrayScaleFilter filter = new GrayScaleFilter();
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SepiaFilter filter = new SepiaFilter(20);
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void horizontalSobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SobelFilter filter = new SobelFilter(SobelFilter.horizontal);
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void verticalSobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SobelFilter filter = new SobelFilter(SobelFilter.vertical);
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void increaseLuminanceBy20ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            IncreaseLuminanceFilter filter = new IncreaseLuminanceFilter(20);
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void increaseSharpnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IncreaseSharpnessFilter filter = new IncreaseSharpnessFilter();
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void waweToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WaweFilter filter = new WaweFilter();
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void glassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlassFilter filter = new GlassFilter();
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void linearExtendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearExtension linearExtension = new LinearExtension();
            backgroundWorker.RunWorkerAsync(linearExtension);
        }
    }
}
