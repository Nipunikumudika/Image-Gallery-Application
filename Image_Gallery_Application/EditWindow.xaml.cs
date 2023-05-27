using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace Image_Gallery_Application
{
    public partial class EditWindow : Window
    {

        public EditWindow(string s)
        {
            if (s != null)
            {
                InitializeComponent();
                path = s;
                using (Bitmap b1 = new Bitmap(s))
                {
                    BitmapToImageSource(b1);
                    img.VerticalAlignment = VerticalAlignment.Center;
                    img.HorizontalAlignment = HorizontalAlignment.Center;
                }
                selectionPath.Visibility = Visibility.Hidden;
            }
            else
            {
                ImageGalleryFirstWindow imageGalleryFirstWindow = new ImageGalleryFirstWindow();
                imageGalleryFirstWindow.Show();
                this.Hide();
            }

        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        string path;
        System.Windows.Point mouse;
        private System.Windows.Point mousePos;

        void BitmapToImageSource(Bitmap bitmap)
        {
                using (MemoryStream memory = new MemoryStream())
                {
                    bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                    memory.Position = 0;
                    BitmapImage bitmapimage = new BitmapImage();
                    bitmapimage.BeginInit();
                    bitmapimage.StreamSource = memory;
                    bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapimage.EndInit();
                    bitmap.Dispose();
                    img.Source = bitmapimage;
                }
        }


        private void rotate_Click(object sender, RoutedEventArgs e)
        {
            selectionPath.Visibility = Visibility.Hidden;
            Bitmap b1 = new Bitmap(path);

            int hight = b1.Height;
            int width = b1.Width;

            System.Drawing.Color[][] colorMatrix = new System.Drawing.Color[width][];
            for (int i = 0; i < width; i++)
            {
                colorMatrix[i] = new System.Drawing.Color[hight];
                for (int j = 0; j < hight; j++)
                {
                    colorMatrix[i][j] = b1.GetPixel(i, j);

                }
            }


            Bitmap myBitmap = new Bitmap(hight, width);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < hight; j++)
                {
                    myBitmap.SetPixel(hight - j - 1, i, colorMatrix[i][j]);
                }

            }
            
           
            BitmapToImageSource(myBitmap);
        }



        private void crop_Click(object sender, RoutedEventArgs e)
        {
            selectionPath.Visibility = Visibility.Visible;
            if(a!=0 && b!=0 && c!=0 && d != 0)
            {
                using (Bitmap b1 = new Bitmap(path))
                {
                    double pixelWidth = img.Source.Width;
                    double pixelHeight = img.Source.Height;
                    a = Convert.ToInt32(a * 1.249);
                    b = Convert.ToInt32(b * 1.249);
                    c = Convert.ToInt32(c * 1.249);
                    d = Convert.ToInt32(d * 1.249);

                    int width2 = c - a;
                    int hight2 = d - b;

                    int hight = b1.Height;
                    int width = b1.Width;

                    if (c > width || d > hight || selectionPath == null || width2 == 0 || hight2 == 0)
                    {
                        return;
                    }

                    System.Drawing.Color[][] colorMatrix = new System.Drawing.Color[width2][];
                    for (int i = 0; i < width2; i++)
                    {
                        colorMatrix[i] = new System.Drawing.Color[hight2];
                        for (int j = 0; j < hight2; j++)
                        {
                            colorMatrix[i][j] = b1.GetPixel(i + a, j + b);
                        }
                    }

                    using (Bitmap myBitmap = new Bitmap(width2, hight2))
                    {

                        for (int Xcount = 0; Xcount < width2; Xcount++)
                        {
                            for (int Ycount = 0; Ycount < hight2; Ycount++)
                            {
                                myBitmap.SetPixel(Xcount, Ycount, colorMatrix[Xcount][Ycount]);
                            }
                        }
                        BitmapToImageSource(myBitmap);
                    }
                }
            }
        }
        int a, b, c, d;

        private void bw_Click(object sender, RoutedEventArgs e)
        {
            selectionPath.Visibility = Visibility.Hidden;
            Bitmap b1 = new Bitmap(path);

            int hight = b1.Height;
            int width = b1.Width;

            System.Drawing.Color[][] colorMatrix = new System.Drawing.Color[width][];
            for (int i = 0; i < width; i++)
            {
                colorMatrix[i] = new System.Drawing.Color[hight];
                for (int j = 0; j < hight; j++)
                {
                    colorMatrix[i][j] = b1.GetPixel(i, j);

                }
            }


            Bitmap myBitmap = new Bitmap(width, hight);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < hight; j++)
                {
                    int grayScale = (int)((colorMatrix[i][j].R * 0.3) + (colorMatrix[i][j].G * 0.59) + (colorMatrix[i][j].B * 0.11));
                    System.Drawing.Color nc = System.Drawing.Color.FromArgb(colorMatrix[i][j].A, grayScale, grayScale, grayScale);
                    myBitmap.SetPixel(i, j, nc);
                }
            }
           BitmapToImageSource(myBitmap);

        }

        private void ImageMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var image = (Image)sender;
            image.CaptureMouse();
            mousePos = e.GetPosition(LoadingCanvas);
            a = Convert.ToInt32(mousePos.X);
            b = Convert.ToInt32(mousePos.Y);
            selectionPath.Data = new RectangleGeometry(new Rect(mousePos, mousePos));
        }

        private void ImageMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var rect = selectionPath.Data as RectangleGeometry;

            if (rect != null)
            {
                var image = (Image)sender;

                rect.Rect = new Rect(mousePos, e.GetPosition(LoadingCanvas));
            }
        }

        private void ImageMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var image = (Image)sender;
            mousePos = e.GetPosition(LoadingCanvas);
            c = Convert.ToInt32(mousePos.X);
            d = Convert.ToInt32(mousePos.Y);
            image.ReleaseMouseCapture();
            selectionPath.Data = null;
        }

        private void return_Click(object sender, RoutedEventArgs e)
        {
            img.Source = null;
            ImageGalleryFirstWindow imageGalleryFirstWindow = new ImageGalleryFirstWindow();
            imageGalleryFirstWindow.Show();
            this.Hide();
        }

        private void red_Click(object sender, RoutedEventArgs e)
        {
            selectionPath.Visibility = Visibility.Hidden;
            Bitmap b1 = new Bitmap(path);

            int hight = b1.Height;
            int width = b1.Width;

            System.Drawing.Color[][] colorMatrix = new System.Drawing.Color[width][];
            for (int i = 0; i < width; i++)
            {
                colorMatrix[i] = new System.Drawing.Color[hight];
                for (int j = 0; j < hight; j++)
                {
                    colorMatrix[i][j] = b1.GetPixel(i, j);

                }
            }


            Bitmap myBitmap = new Bitmap(width, hight);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < hight; j++)
                {
                    System.Drawing.Color newColor = System.Drawing.Color.FromArgb(colorMatrix[i][j].R, 0, 0);
                    myBitmap.SetPixel(i, j, newColor);
                }
            }
            BitmapToImageSource(myBitmap);
        }

        private void green_Click(object sender, RoutedEventArgs e)
        {
            selectionPath.Visibility = Visibility.Hidden;
            Bitmap b1 = new Bitmap(path);

            int hight = b1.Height;
            int width = b1.Width;

            System.Drawing.Color[][] colorMatrix = new System.Drawing.Color[width][];
            for (int i = 0; i < width; i++)
            {
                colorMatrix[i] = new System.Drawing.Color[hight];
                for (int j = 0; j < hight; j++)
                {
                    colorMatrix[i][j] = b1.GetPixel(i, j);

                }
            }


            Bitmap myBitmap = new Bitmap(width, hight);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < hight; j++)
                {
                    System.Drawing.Color newColor = System.Drawing.Color.FromArgb(0, colorMatrix[i][j].G, 0);
                    myBitmap.SetPixel(i, j, newColor);
                }
            }
            BitmapToImageSource(myBitmap);

        }

        private void blue_Click(object sender, RoutedEventArgs e)
        {
            selectionPath.Visibility = Visibility.Hidden;
            using (Bitmap b1 = new Bitmap(path))
            {
                int hight = b1.Height;
                int width = b1.Width;

                System.Drawing.Color[][] colorMatrix = new System.Drawing.Color[width][];
                for (int i = 0; i < width; i++)
                {
                    colorMatrix[i] = new System.Drawing.Color[hight];
                    for (int j = 0; j < hight; j++)
                    {
                        colorMatrix[i][j] = b1.GetPixel(i, j);

                    }
                }
                using (Bitmap myBitmap = new Bitmap(width, hight))
                {
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < hight; j++)
                        {
                            System.Drawing.Color newColor = System.Drawing.Color.FromArgb(0, 0, colorMatrix[i][j].R);
                            myBitmap.SetPixel(i, j, newColor);
                        }
                    }
                    BitmapToImageSource(myBitmap);
                } 
            }
        }
    }
}
