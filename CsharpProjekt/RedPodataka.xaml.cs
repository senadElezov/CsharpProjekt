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
    /// Interaction logic for RedPodataka.xaml
    /// </summary>
    public partial class RedPodataka : UserControl
    {
        private static int brojac=1;
        public static readonly RoutedEvent Del = EventManager.RegisterRoutedEvent("Brisi", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(RedPodataka));

        // Provide CLR accessors for the event
        public event RoutedEventHandler Brisi
        {
            add { AddHandler(Del, value); }
            remove { RemoveHandler(Del, value); }
        }

        // This method raises the Tap event
        void RaiseBrisiEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(RedPodataka.Del);
            RaiseEvent(newEventArgs);
        }
        public RedPodataka()
        {
            InitializeComponent();
            musko.GroupName = "Spol" + brojac.ToString();
            Zensko.GroupName = "Spol" + brojac.ToString();
            ++brojac;
        }
        private void BrisiRed(object sender,RoutedEventArgs e)
        {
            RaiseBrisiEvent();
        }
    }
}
