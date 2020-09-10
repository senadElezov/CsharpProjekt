using System;
using System.Collections.Generic;
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

namespace CsharpProjekt
{
    /// <summary>
    /// Interaction logic for MyRectangle.xaml
    /// </summary>
    public partial class MyRectangle : UserControl
    {
        public bool selected = false;
        public MyRectangle()
        {
            InitializeComponent();
        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            Pozadina.Background = new SolidColorBrush(Color.FromArgb(255, (byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256)));
        }
        private void Clicknut(object sender,RoutedEventArgs e)
        {
            selected = !selected;
            Selektiran.Visibility = selected ? Visibility.Visible : Visibility.Hidden;
        }
        public void Deselect()
        {
            Selektiran.Visibility =  Visibility.Hidden;
            selected = false;
        }
    }
}
