using System;
using System.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace Image_Gallery_Application
{
    
    public partial class PlayWindow : Window
    {

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        Node node;
        DoublyLinkedList imageList;
        private DispatcherTimer timer = new DispatcherTimer();
        public PlayWindow(DoublyLinkedList list)
        {
            InitializeComponent();
            imageList = list;
            node = imageList.head;
            previous.Visibility = Visibility.Hidden;
            next.Visibility = Visibility.Hidden;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Asterisk.Play();
            if (node.next == null)
                node.next = imageList.head;
            node = node.next;
            img.Source = new BitmapImage(new Uri(node.elem, UriKind.Absolute));
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Asterisk.Play();

            if (node.prev == null)
                node.prev = imageList.tail;

            node = node.prev;
            img.Source = new BitmapImage(new Uri(node.elem, UriKind.Absolute));
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if (node == null)
                return;
            node.prev = imageList.tail;
            img.Source = new BitmapImage(new Uri(node.elem, UriKind.Absolute));
            img.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            previous.Visibility = Visibility.Visible;
            next.Visibility = Visibility.Visible;
            playbtn.Visibility = Visibility.Hidden;
            timerplay.Visibility = Visibility.Hidden;
            textbox.Visibility = Visibility.Hidden;
            tbslideshow.Visibility = Visibility.Hidden;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            next.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void Timer_Click(object sender, RoutedEventArgs e)
        {
            playbtn.Visibility = Visibility.Hidden;
            tbslideshow.Visibility= Visibility.Hidden;
            img.Source = new BitmapImage(new Uri(node.elem, UriKind.Absolute));

            int timespan = 3;
            if (textbox.Text != "3")
            {
                timespan = Convert.ToInt32(textbox.Text);
            }
            timer.Interval = new TimeSpan(0, 0, timespan);
            timer.Tick += Timer_Tick;
            timer.Start();
        }


        private void return_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled == true)
                timer.Stop();
            img.Source = null;
            ImageGalleryFirstWindow imageGalleryFirstWindow = new ImageGalleryFirstWindow();
            imageGalleryFirstWindow.Show();
            this.Hide();
        }
    }
}
