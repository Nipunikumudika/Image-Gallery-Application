using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for PlayWindowFirst.xaml
    /// </summary>
    public partial class PlayWindowFirst : Window
    {

        public PlayWindowFirst()
        {
            InitializeComponent();
            myList1 = new DoublyLinkedList();
            selectImage.Visibility = Visibility.Hidden;
            play.Visibility = Visibility.Hidden;
            delete.Visibility = Visibility.Hidden;
            Thumbnails.Visibility = Visibility.Hidden;
            other.Visibility=Visibility.Hidden;
            ImageList.Visibility = Visibility.Hidden;
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }


        DoublyLinkedList myList1;

        string pathimg;
        string[] extracted;
        string[] values;
        string[]? imagearray = null;
        int index = 0;

        public string[] Images
        {
            get
            {
                // If we have the array we return it
                if (imagearray != null)
                {
                    return imagearray;
                }
                else
                {
                    using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
                    {
                        System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            pathimg = dialog.SelectedPath;
                        }

                    }
                    if(pathimg == null)
                    {
                        return null;
                    }
                    else
                    {
                        string[] ext = new string[2] { "*.png", "*jpg" };
                        Thumbnails.Items.Clear();
                        int k = 0;
                        string[] imagearray = new string[50];
                        foreach (string found in ext)
                        {
                            extracted = Directory.GetFiles(pathimg, found, System.IO.SearchOption.AllDirectories);

                            for (int j = k, i = 0; j < extracted.Count(); j++, i++)
                            {
                                imagearray[j] = extracted[i];
                                Thumbnails.Items.Add(new BitmapImage(new Uri(extracted[i])));
                            }
                            k = extracted.Count();
                        }
                        return imagearray;
                    }
                    

                }
                // And return it
            }

        }


        private void Button_Open_Click(object sender, RoutedEventArgs e)
        {
            imagearray = null;
            values = Images;
            if (values == null)
            {
                return;
            }
            index = 0;
            selectImage.Visibility = Visibility.Visible;
            Thumbnails.Visibility = Visibility.Visible;
            open.Visibility = Visibility.Hidden;
            other.Visibility = Visibility.Visible;
            ImageList.Visibility = Visibility.Visible;
        }

        private void Button_Next_Click(object sender, RoutedEventArgs e)
        {
            index++;
            if (index == Thumbnails.Items.Count)
                index = 0;
            Thumbnails.SelectedItem = Thumbnails.Items[index];
            Thumbnails.ScrollIntoView(Thumbnails.Items[index]);
        }

        private void Button_Previous_Click(object sender, RoutedEventArgs e)
        {
            index--;

            if (index == -1)
                index = Thumbnails.Items.Count - 1;
            Thumbnails.SelectedItem = Thumbnails.Items[index];
            Thumbnails.ScrollIntoView(Thumbnails.Items[index]);
        }

        private void Button_Select_Click(object sender, RoutedEventArgs e)
        {
            myList1.insertlast(values[index]);
            ImageList.Items.Add(myList1.GetLast());
            play.Visibility = Visibility.Visible;
            delete.Visibility = Visibility.Visible;
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            if (ImageList.SelectedIndex >= 0)
            {
                myList1.deleteAt(ImageList.SelectedIndex);
                ImageList.Items.RemoveAt(ImageList.SelectedIndex);
            }
        }

        private void Button_Play_Click(object sender, RoutedEventArgs e)
        {

            PlayWindow playWindow = new PlayWindow(myList1);
            playWindow.Show();
            this.Hide();
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow(values[index]);
            editWindow.Show();
            this.Hide();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            index = Thumbnails.SelectedIndex;
        }

        private void return_Click(object sender, RoutedEventArgs e)
        {
            ImageGalleryFirstWindow imageGalleryFirstWindow = new ImageGalleryFirstWindow();
            imageGalleryFirstWindow.Show();
            this.Hide();
        }
    }
}
