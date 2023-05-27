﻿using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Image_Gallery_Application
{
    /// <summary>
    /// Interaction logic for ImageGalleryFirstWindow.xaml
    /// </summary>
    public partial class ImageGalleryFirstWindow : Window
    {
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        string fileName=null;
        public ImageGalleryFirstWindow()
        {
            InitializeComponent();
        }

        private void View_Images_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void Edit_Image_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "";

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            string sep = string.Empty;

            foreach (var c in codecs)
            {
                string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                dlg.Filter = String.Format("{0}{1}{2} ({3})|{3}", dlg.Filter, sep, codecName, c.FilenameExtension);
                sep = "|";
            }

            dlg.Filter = String.Format("{0}{1}{2} ({3})|{3}", dlg.Filter, sep, "All Files", "*.*");

            dlg.DefaultExt = ".png";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                fileName = dlg.FileName;
            }
            if(fileName != null)
            {
                EditWindow editWindow = new EditWindow(fileName);
                editWindow.Show();
                this.Hide();
            }
            

        }

        private void Animations_Click(object sender, RoutedEventArgs e)
        {
            PlayWindowFirst playWindowFirst = new PlayWindowFirst();
            playWindowFirst.Show();
            this.Hide();
        }

    }
}
