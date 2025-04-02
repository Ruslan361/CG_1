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
        bool isActiveSelectColor;
        Color selectedColor;
        CorrectionWithReferenceColor deferredAction;
        public Form1()
        {
            InitializeComponent();
            isActiveSelectColor = false;
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

        private void medianFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MedianBlur linearExtension = new MedianBlur(10);
            backgroundWorker.RunWorkerAsync(linearExtension);
        }

        private void grayWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GrayWorld filter = new GrayWorld();
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void расширениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool[,] mask = { { false, true, false },
                             { true, true, true },
                             { false, true, false } };
            Dilatation filter = new Dilatation(mask);
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void erosionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool[,] mask = { { false, true, false },
                             { true, true, true },
                             { false, true, false } };
            Erosion filter = new Erosion(mask);
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void openingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool[,] mask = { { false, true, false },
                             { true, true, true },
                             { false, true, false } };
            Opening filter = new Opening(mask, mask);
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void закрытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool[,] mask = { { false, true, false },
                             { true, true, true },
                             { false, true, false } };
            Closing filter = new Closing(mask, mask);
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void gradToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool[,] mask = { { false, true, false },
                             { true, true, true },
                             { false, true, false } };
            Grad filter = new Grad(mask);
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void perfectReflectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerfectReflector filter = new PerfectReflector();
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void correctionWithReferenceСolorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    isActiveSelectColor = true;
                    CorrectionWithReferenceColor correction = new CorrectionWithReferenceColor();
                    correction.DistColor = colorDialog.Color;
                    deferredAction = correction;
                }
            }
        }

        private void correctionWithReferenceСolorToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!isActiveSelectColor) return;
            if (pictureBox.Image == null) return;

            isActiveSelectColor = false;

            // Размеры pictureBox и изображения
            int pbWidth = pictureBox.ClientSize.Width;
            int pbHeight = pictureBox.ClientSize.Height;
            int imgWidth = selectedImage.Width;
            int imgHeight = selectedImage.Height;

            // Вычисление масштабирования изображения
            float scaleX = (float)pbWidth / imgWidth;
            float scaleY = (float)pbHeight / imgHeight;
            float scale = Math.Min(scaleX, scaleY);

            // Вычисление размеров отображаемого изображения
            int displayWidth = (int)(imgWidth * scale);
            int displayHeight = (int)(imgHeight * scale);

            // Определение отступов (если изображение не заполняет весь pictureBox)
            int offsetX = (pbWidth - displayWidth) / 2;
            int offsetY = (pbHeight - displayHeight) / 2;

            // Преобразование координат клика
            if (e.X >= offsetX && e.X < offsetX + displayWidth &&
                e.Y >= offsetY && e.Y < offsetY + displayHeight)
            {
                int imgX = (int)((e.X - offsetX) / scale);
                int imgY = (int)((e.Y - offsetY) / scale);

                // Получаем цвет пикселя
                Color pixelColor = selectedImage.GetPixel(imgX, imgY);
                using (ColorDialog colorDialog = new ColorDialog())
                {
                    colorDialog.Color = pixelColor;
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        isActiveSelectColor = false;
                        deferredAction.SourceColor = colorDialog.Color;
                        backgroundWorker.RunWorkerAsync(deferredAction);
                    }
                }

                //MessageBox.Show($"Цвет пикселя: {pixelColor}");
            }
        }
    }
}
/*
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */