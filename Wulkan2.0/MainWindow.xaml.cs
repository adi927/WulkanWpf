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
using System.Data.SqlTypes;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Win32;

namespace uczenSredniaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int aktualnyUczenIndex = 0;
        public int iloscUczniow;
        static List<Uczen> listaUczniow = new List<Uczen>();
        public MainWindow()
        {
            InitializeComponent();


            string sciezka = @"daneUczniowPrzykladowe.json";

            UczenZPliku(sciezka);

            List<double> srednieUczniow = new List<double>();



            iloscUczniow = (listaUczniow.Count - 1);
            int liczbaUczniow = listaUczniow.Count;

            iloscUczniowLabel.Content = "Ilosc uczniow: " + liczbaUczniow.ToString();

            int uczenZnajniszaSrednia = 0, uczenZnajwyszaSrednia = 0;
            double najwyzszaSrednia = int.MinValue;
            double najnizszaSrednia = int.MaxValue;

            for (int i = 0; i < listaUczniow.Count; i++)
            {
                srednieUczniow.Add(listaUczniow[i].SredniaOcenUcznia());
            }

            for (int i = 0; i < srednieUczniow.Count; i++)
            {
                if (srednieUczniow[i] > najwyzszaSrednia)
                {
                    najwyzszaSrednia = srednieUczniow[i];
                    uczenZnajwyszaSrednia = i;
                }
            }

            for (int i = 0; i < srednieUczniow.Count; i++)
            {
                if (srednieUczniow[i] < najnizszaSrednia)
                {
                    najnizszaSrednia = srednieUczniow[i];
                    uczenZnajniszaSrednia = i;
                }
            }
            WyswietlDaneUcznia(aktualnyUczenIndex);

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (aktualnyUczenIndex > 0)
            {
                aktualnyUczenIndex--;
            }
            else
            {
                aktualnyUczenIndex = listaUczniow.Count - 1;
            }

            WyswietlDaneUcznia(aktualnyUczenIndex);
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            if (aktualnyUczenIndex < listaUczniow.Count - 1)
            {
                aktualnyUczenIndex++;
            }
            else
            {
                aktualnyUczenIndex = 0;
            }

            WyswietlDaneUcznia(aktualnyUczenIndex);
        }

        private void WyswietlDaneUcznia(int index)
        {
            imieLabel.Content = "IMIE: " + listaUczniow[index].Imie;
            nazwiskoLabel.Content = "NAZWISKO: " + listaUczniow[index].Nazwisko;
            dataLabel.Content = "DATA URODZENIA: " + listaUczniow[index].Data;
            klasaLabel.Content = "KLASA: " + listaUczniow[index].Klasa;
            semestrLabel.Content = "SEMESTR: " + listaUczniow[index].Semestr;
            peselBlock.Text = listaUczniow[index].Pesel;
            ocenyBlock.Text = listaUczniow[index].Oceny();
            sredniaBlock.Text = listaUczniow[index].Srednia;

            try
            {
                string imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ico", listaUczniow[index].Zdjecie);

                if (!File.Exists(imagePath))
                {
                    MessageBox.Show("Brak zdjęcia dla ucznia: " + listaUczniow[index].Imie + " " + listaUczniow[index].Nazwisko);
                    MessageBox.Show(imagePath);
                }
                else
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                    studentImage.Source = bitmap;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }


        }

        public static void UczenZPliku(string filePath)
        {
            var json = File.ReadAllText(filePath);
            listaUczniow = JsonConvert.DeserializeObject<List<Uczen>>(json);
        }


        private void OpenFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Wybierz plik json z uczniami",
                Filter = "Pliki json (*.json)|*.json",
                Multiselect = false
            };

            bool result = (bool)openFileDialog.ShowDialog();

            if (result)
            {
                string fileName = openFileDialog.FileName;
                UczenZPliku(fileName);

                iloscUczniow = (listaUczniow.Count - 1);
                int liczbaUczniow = listaUczniow.Count;

                iloscUczniowLabel.Content = "Ilosc uczniow: " + liczbaUczniow.ToString();
            }

        }
    }

}