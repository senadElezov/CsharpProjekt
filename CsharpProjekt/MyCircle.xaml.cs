﻿using System;
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
    /// Interaction logic for MyCircle.xaml
    /// </summary>
    public partial class MyCircle : UserControl
    {
        public static readonly RoutedEvent Selekt = EventManager.RegisterRoutedEvent("SelektiranKrug", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MyCircle));

        // Provide CLR accessors for the event
        public event RoutedEventHandler SelektiranKrug
        {
            add { AddHandler(Selekt, value); }
            remove { RemoveHandler(Selekt, value); }
        }

        // This method raises the Tap event
        void RaiseSelektiranEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(MyCircle.Selekt);
            RaiseEvent(newEventArgs);
        }

        public bool selected = false;
        public MyCircle()
        {
            InitializeComponent();
        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            Pozadina.Fill = new SolidColorBrush(Color.FromArgb(255, (byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256)));
        }
        private void Clicknut(object sender, RoutedEventArgs e)
        {
            selected = !selected;
            Selektiran.Visibility = selected ? Visibility.Visible : Visibility.Hidden;
            RaiseSelektiranEvent();
        }
        public void Deselect()
        {
            Selektiran.Visibility = Visibility.Hidden;
            selected = false;
        }
        public void selektiraj()
        {
            Selektiran.Visibility = Visibility.Visible;
            selected = true;
        }
    }
}
