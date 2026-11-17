using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace CukraszdaWPF
{
    public partial class MainWindow : Window
    {
        private List<Sutemeny> sutemenyek = new List<Sutemeny>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Megnyitas_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Szöveges fájlok (*.txt)|*.txt|Minden fájl (*.*)|*.*";

            if (ofd.ShowDialog() == true)
            {
                try
                {
                    sutemenyek.Clear();
                    string[] sorok = File.ReadAllLines(ofd.FileName);

                    foreach (string sor in sorok)
                    {
                        if (!string.IsNullOrWhiteSpace(sor))
                        {
                            string[] adatok = sor.Split(';');
                            string nev = adatok[0].Trim();
                            int ar = int.Parse(adatok[1].Trim());
                            sutemenyek.Add(new Sutemeny(nev, ar));
                        }
                    }

                    MenuSutemenyek.IsEnabled = true;
                    ArlistaMegnyitasa();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba a fájl beolvasásakor: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Sutemenyek_Click(object sender, RoutedEventArgs e)
        {
            ArlistaMegnyitasa();
        }

        private void ArlistaMegnyitasa()
        {
            ArlistaWindow arlistaAblak = new ArlistaWindow(sutemenyek);
            arlistaAblak.ShowDialog();
        }

        private void Nevjegy_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cukrászda rendelő alkalmazás\nKészítette: Saját Név\n2026", "Névjegy", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Kilepes_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
