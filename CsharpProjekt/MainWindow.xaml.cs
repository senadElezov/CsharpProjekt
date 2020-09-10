using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;


namespace CsharpProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try {
                InitializeComponent();
           
            }
            
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
               
            }

        }
        private void onLoad(object sender,EventArgs e )
        {
            for (int i = 0; i < grid.Children.Count; ++i)
            {
                if (grid.Children[i] is MyRadioButton)
                {
                    (grid.Children[i] as MyRadioButton).MouseEnter += SeminariMouseEnter;
                    (grid.Children[i] as MyRadioButton).MouseLeave += SeminariMouseLeave;
                    (grid.Children[i] as MyRadioButton).Unchecked += SeminariMouseLeave;
                    (grid.Children[i] as MyRadioButton).Checked += SeminariMouseChecked;

                }
            }
            DoubleAnimation anim = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 1000));
            DoubleAnimation anim2 = new DoubleAnimation(1, new TimeSpan(0, 0, 0, 0, 1000));
            CrniBorder.BeginAnimation(OpacityProperty, anim);
            Slika.BeginAnimation(OpacityProperty, anim2);
        }
        private void SeminariMouseEnter(object sender, MouseEventArgs e) {
            MyRadioButton btn = sender as MyRadioButton;
            Panel.SetZIndex(btn, 2);
            Thickness novaMargina = btn.Margin;
            novaMargina.Bottom = 0;
            ThicknessAnimation anim = new ThicknessAnimation(novaMargina, new TimeSpan(0, 0, 0, 0, 250));
            DoubleAnimation anim2 = new DoubleAnimation(340, new TimeSpan(0, 0, 0, 0, 250));
            btn.BeginAnimation(MarginProperty, anim);
            btn.BeginAnimation(WidthProperty, anim2);
        }
        private void SeminariMouseLeave(object sender, EventArgs e)
        {
            MyRadioButton btn = sender as MyRadioButton;
            if (!(bool)btn.IsChecked) {
                Panel.SetZIndex(btn, 0);

                Thickness novaMargina = btn.Margin;
            novaMargina.Bottom = 10;
            ThicknessAnimation anim = new ThicknessAnimation(novaMargina, new TimeSpan(0, 0, 0, 0, 250));
            DoubleAnimation anim2 = new DoubleAnimation(300, new TimeSpan(0, 0, 0, 0, 250));
            btn.BeginAnimation(MarginProperty, anim);
            btn.BeginAnimation(WidthProperty, anim2);
            }
        }
        private void SeminariMouseChecked(object sender, EventArgs e) {
            MyRadioButton btn = sender as MyRadioButton;
            Panel.SetZIndex(btn, 1);
            SakrijSve();

            if (btn.Content.ToString() == "Kruznice")
            {
                GridKruznice.Visibility = Visibility.Visible;
            }
            else if (btn.Content.ToString() == "Kvadrati")
            {
                gridKvadrati.Visibility = Visibility.Visible;
            }
            else if (btn.Content.ToString() == "Tablica")
            {
                GridImena.Visibility = Visibility.Visible;
            }
        }
        private void SakrijSve()
        {
            PocetniGrid.Visibility = Visibility.Hidden;
            gridKvadrati.Visibility = Visibility.Hidden;
            GridKruznice.Visibility = Visibility.Hidden;
            GridImena.Visibility = Visibility.Hidden;

        }
        private void dodajClick(object sender,RoutedEventArgs e)
        {
            Random rand=new Random();
            double left, top, right, bottom;
            left = rand.NextDouble() * (Platno.ActualWidth-10);
            right = rand.NextDouble() * (Platno.ActualWidth - left) + left;
            top = rand.NextDouble() * (Platno.ActualHeight - 10);
            bottom = rand.NextDouble() * (Platno.ActualHeight - top) + top;
            dodajRect(left, top, right, bottom);
            IzracunajPovrsinu();

        }
        private void dodajRect(double left,double top,double right,double bottom)
        {
            MyRectangle rect = new MyRectangle();
            Canvas.SetLeft(rect, left);
            Canvas.SetTop(rect, top);
            rect.Width = right - left;
            rect.Height = bottom - top;
            if(rect.Height>20 && rect.Width > 20) { 
            Platno.Children.Add(rect);
            rect.MouseLeftButtonDown += RectSelektiran;
            IzracunajPovrsinu();
            }
        }
        private void LijeviClick(object sender,RoutedEventArgs e) {
            Platno.Tag = Mouse.GetPosition(Platno);
        }
        private void MouseUped(object sender, RoutedEventArgs e)
        {
            if(Platno.Tag!=null && Platno.Tag is Point)
            {
                Point koordinata1, koordinata2;
                koordinata1 = (Point)Platno.Tag;
                koordinata2 = Mouse.GetPosition(Platno);
                dodajRect(koordinata1.X<koordinata2.X?koordinata1.X:koordinata2.X, koordinata1.Y < koordinata2.Y ? koordinata1.Y : koordinata2.Y, koordinata1.X > koordinata2.X ? koordinata1.X : koordinata2.X, koordinata1.Y > koordinata2.Y ? koordinata1.Y : koordinata2.Y);
            }
            Platno.Tag = null;

        }
        private void RectSelektiran(object sender,RoutedEventArgs e) {
            if (!Keyboard.IsKeyDown(Key.LeftShift)) {
                for (int i=0; i < Platno.Children.Count; ++i) {
                    if (Platno.Children[i] is MyRectangle && (Platno.Children[i] as MyRectangle)!=sender as MyRectangle) {
                        (Platno.Children[i] as MyRectangle).Deselect();
                    }
                        }
            }
        }
        private void BrisiSve(object sender, RoutedEventArgs e)
        {
            int i = 0;
            while (i < Platno.Children.Count)
            {
                if (Platno.Children[i] is MyRectangle && (Platno.Children[i] as MyRectangle).selected)
                {
                    Platno.Children.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }
            IzracunajPovrsinu();
        }
        private void IzracunajPovrsinu() {
            double suma = 0;
            for (int i = 0; i < Platno.Children.Count; ++i) {
                if (Platno.Children[i] is MyRectangle) {
                    suma += (Platno.Children[i] as MyRectangle).Height * (Platno.Children[i] as MyRectangle).Width;
                }
            }
            povrsinaLabel.Content = Math.Round(suma, 2);
        }
        private void dodajCircle(Point center,Point onCircle) {
            MyCircle circle = new MyCircle();
            double radius = Math.Sqrt(Math.Pow(Math.Sqrt((center.X - onCircle.X) * (center.X - onCircle.X)), 2) + Math.Pow(Math.Sqrt((center.Y - onCircle.Y) * (center.Y - onCircle.Y)), 2));
            if(center.X-radius<0 || center.Y-radius<0 || center.X+radius>PlatnoKruznice.ActualWidth || center.Y + radius > PlatnoKruznice.ActualHeight)
            {
                MessageBox.Show("Nije moguće dodati kružnicu koja prelazi okvire prostora za crtanje", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(radius > 15) {
                for (int i = 0; i < PlatnoKruznice.Children.Count; ++i)
                {
                    if (PlatnoKruznice.Children[i] is MyCircle)
                    {
                        (PlatnoKruznice.Children[i] as MyCircle).Deselect();
                    }
                }
                Canvas.SetLeft(circle,center.X - radius);
            Canvas.SetTop(circle,center.Y - radius);
            circle.Width = (2 * radius);
            circle.Height = (2 * radius);
            PlatnoKruznice.Children.Add(circle);
            circle.selektiraj();
                circle.SelektiranKrug += CircSelektiran;
                IzracunajPovrsinuKruga();

            }

        }
        private void CircSelektiran(object sender,RoutedEventArgs e)
        {
            
            for (int i = 0; i < PlatnoKruznice.Children.Count; ++i)
            {
                if (PlatnoKruznice.Children[i] is MyCircle && (PlatnoKruznice.Children[i] as MyCircle)!=(sender as MyCircle))
                {
                    (PlatnoKruznice.Children[i] as MyCircle).Deselect();
                }
            }
            IzracunajPovrsinuKruga();
        }
        private void KruzniceClick(object sender,RoutedEventArgs e)
        {
            Platno.Tag = Mouse.GetPosition(PlatnoKruznice);
        }
        private void KruzniceMouseUp(object sender,RoutedEventArgs e)
        {
            if(Platno.Tag is Point)
            {
                dodajCircle((Point)Platno.Tag, Mouse.GetPosition(PlatnoKruznice));
            }
        }
       private void IzracunajPovrsinuKruga()
        {
            for (int i = 0; i < PlatnoKruznice.Children.Count; ++i)
            {
                if (PlatnoKruznice.Children[i] is MyCircle && (PlatnoKruznice.Children[i] as MyCircle).selected)
                {
                    MyCircle circ = (PlatnoKruznice.Children[i] as MyCircle);
                    OpsegLabelKruznica.Content = Math.Round(circ.Width * Math.PI, 2);
                    povrsinaLabelKruznica.Content= Math.Round(circ.Width*circ.Width/4 * Math.PI, 2);
                    return;
                }
            }
        }
        private void BrisiSveKruznice(object sender,RoutedEventArgs e)
        {
            int i = 0;
            povrsinaLabelKruznica.Content = null;
            OpsegLabelKruznica.Content = null;
            while (i < PlatnoKruznice.Children.Count)
            {
                if (PlatnoKruznice.Children[i] is MyCircle)
                {
                    PlatnoKruznice.Children.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }
        }
        private void dodajRed(object sender,RoutedEventArgs e)
        {
            RedPodataka mojred = new RedPodataka();
            Grid.SetRow(mojred, TablicaGrid.RowDefinitions.Count - 1);
            Grid.SetColumnSpan(mojred, 7);
            RowDefinition rowdef = new RowDefinition();
            rowdef.Height = new GridLength(80);
            TablicaGrid.RowDefinitions[TablicaGrid.RowDefinitions.Count - 1].Height = new GridLength(50);
            TablicaGrid.RowDefinitions.Add(rowdef);
            TablicaGrid.Children.Add(mojred);
            Grid.SetRow(sender as Button, TablicaGrid.RowDefinitions.Count - 1);
            mojred.Brisi += BrisiRed;
        }
        private void BrisiRed(object sender,RoutedEventArgs e)
        {
            RedPodataka mojred = sender as RedPodataka;
            int red = Grid.GetRow(mojred);
            for(int i = 0; i < TablicaGrid.Children.Count; ++i)
            {
                if (Grid.GetRow(TablicaGrid.Children[i]) > red)
                {
                    Grid.SetRow(TablicaGrid.Children[i], Grid.GetRow(TablicaGrid.Children[i]) - 1);
                }

            }
            TablicaGrid.Children.Remove(mojred);
            TablicaGrid.RowDefinitions.RemoveAt(TablicaGrid.RowDefinitions.Count-1);
            TablicaGrid.RowDefinitions[TablicaGrid.RowDefinitions.Count - 1].Height = new GridLength(80);

        }
        private void tablicaBtnChecked(object sender, RoutedEventArgs e)
        {
            sortiraj((sender as MyRadioButton).Content.ToString());
        }
        private void sortiraj(string naziv)
        {
            for(int i = 0; i < TablicaGrid.Children.OfType<RedPodataka>().Count(); ++i)
            {
               bool prvi = true;
               RedPodataka najmanji=new RedPodataka();
               RedPodataka zaZamjenu = new RedPodataka();
               for(int j=0; j < TablicaGrid.Children.OfType<RedPodataka>().Count(); ++j)
               {
                    if (Grid.GetRow(TablicaGrid.Children.OfType<RedPodataka>().ElementAt(j)) > i)
                    {
                        if (Grid.GetRow(TablicaGrid.Children.OfType<RedPodataka>().ElementAt(j))== i + 1) {
                        zaZamjenu= TablicaGrid.Children.OfType<RedPodataka>().ElementAt(j);
                        }
                        if (prvi)
                        {
                            najmanji = TablicaGrid.Children.OfType<RedPodataka>().ElementAt(j);
                            prvi = false;
                        }
                        else if (!Uvjet(TablicaGrid.Children.OfType<RedPodataka>().ElementAt(j), najmanji, naziv))
                        {
                                najmanji = TablicaGrid.Children.OfType<RedPodataka>().ElementAt(j);
                        }
                        
                    }
                  
                }
                Grid.SetRow(zaZamjenu, Grid.GetRow(najmanji));
                Grid.SetRow(najmanji, i + 1);
            }
        }
                private bool Uvjet(RedPodataka prvi,RedPodataka drugi,string naziv) {
            if (naziv == "Ime")
            {
                return prvi.ime.Text.CompareTo(drugi.ime.Text) > 0;
            }
            else if (naziv == "Prezime")
            {
                return prvi.prezime.Text.CompareTo(drugi.prezime.Text) > 0;

            }
            else if (naziv == "Spol")
            {
                return (bool)prvi.Zensko.IsChecked && (bool)drugi.musko.IsChecked || (!(bool)prvi.Zensko.IsChecked && !(bool)prvi.musko.IsChecked && ((bool)drugi.musko.IsChecked || (bool)drugi.Zensko.IsChecked));

            }
            else if (naziv == "Datum rođenja")
            {
                return (prvi.datum.SelectedDate != null ? (DateTime)prvi.datum.SelectedDate : DateTime.MinValue) > (drugi.datum.SelectedDate != null ? (DateTime)drugi.datum.SelectedDate : DateTime.MinValue);
            }
            else if (naziv == "Država")
            {
                return prvi.drzava.Text.CompareTo(drugi.drzava.Text) > 0;

            }
            else if (naziv == "Mjesto")
            {
                return prvi.mjesto.Text.CompareTo(drugi.mjesto.Text) > 0;
            }
            return false;
        }
    }

}
