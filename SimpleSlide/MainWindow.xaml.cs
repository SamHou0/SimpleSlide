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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SimpleSlide
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] pictures;
        int index = -1;
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += NextPic_Tick;
            timer.Interval = new TimeSpan(0, 0, 5);
        }

        private void NextPic_Tick(object? sender, EventArgs e)
        {
            if (pictures != null)
            {
                index++;
                RefreshPic();
            }
        }

        private void RefreshPic()
        {
            if (index < 0 || index >= pictures.Length)
            {
                index = 0;
                return;
            }
            imageBox.Source = new BitmapImage(new Uri(pictures[index]));
        }

        private void setPathButton_Click(object sender, RoutedEventArgs e)
        {
            pictures = Directory.GetFiles(pathBox.Text);
            index = -1;
            timer.Start();
        }

        private void setIntervalButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(intervalBox.Text, out int interval) && interval > 0)
            {
                timer.Interval = new TimeSpan(0, 0, interval);
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            index++;
            RefreshPic();
        }

        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            index--;
            RefreshPic();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            File.Delete(pictures[index]);
            pictures = Directory.GetFiles(pathBox.Text);
            RefreshPic();
        }
    }
}
