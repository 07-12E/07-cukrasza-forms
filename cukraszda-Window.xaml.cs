using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CukraszdaWPF
{
    public partial class ArlistaWindow : Window
    {
        private List<Sutemeny> sutik;
        private List<CheckBox> checkBoxok = new List<CheckBox>();
        private List<TextBox> textBoxok = new List<TextBox>();

        public ArlistaWindow(List<Sutemeny> sutemenyek)
        {
            InitializeComponent();
            this.sutik = sutemenyek;
            FeluletEpites();
        }

        private void FeluletEpites()
        {
            foreach (var suti in sutik)
            {
                Grid sorGrid = new Grid { Margin = new Thickness(0, 5, 0, 5) };
                sorGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                sorGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                sorGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                CheckBox cb = new CheckBox
                {
                    Content = $"{suti.Nev} ({suti.Ar} Ft)",
                    VerticalAlignment = VerticalAlignment.Center,
                    IsChecked = suti.RendeltMennyiseg > 0
                };
                Grid.SetColumn(cb, 0);

                TextBox tb = new TextBox
                {
                    Width = 40,
                    Margin = new Thickness(10, 0, 5, 0),
                    Text = suti.RendeltMennyiseg > 0 ? suti.RendeltMennyiseg.ToString() : ""
                };
                Grid.SetColumn(tb, 1);

                TextBlock lblAdag = new TextBlock
                {
                    Text = "adag",
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(lblAdag, 2);

                sorGrid.Children.Add(cb);
                sorGrid.Children.Add(tb);
                sorGrid.Children.Add(lblAdag);

                SpSutemenyek.Children.Add(sorGrid);
                checkBoxok.Add(cb);
                textBoxok.Add(tb);
            }
        }

        private bool RendelesFeldolgozasa()
        {
            bool vanKijelolve = false;

            for (int i = 0; i < sutik.Count; i++)
            {
                if (checkBoxok[i].IsChecked == true)
                {
                    if (int.TryParse(textBoxok[i].Text, out int adag) && adag > 0)
                    {
                        sutik[i].RendeltMennyiseg = adag;
                        vanKijelolve = true;
                    }
                    else
                    {
                        MessageBox.Show($"Kérem adjon meg érvényes adagszámot a következőhöz: {sutik[i].Nev}!", "Hibás adat", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }
                }
                else
                {
                    sutik[i].RendeltMennyiseg = 0;
                }
            }

            if (!vanKijelolve)
            {
                MessageBox.Show("Nincs semmi kijelölve a rendeléshez!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void BtnRendel_Click(object sender, RoutedEventArgs e)
        {
            if (RendelesFeldolgozasa())
            {
                MessageBox.Show("Rendelését sikeresen rögzítettük!", "Rendelés", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SzamlatKer_Click(object sender, RoutedEventArgs e)
        {
            if (RendelesFeldolgozasa())
            {
                SzamlaWindow szamlaAblak = new SzamlaWindow(sutik);
                szamlaAblak.ShowDialog();
            }
        }
    }
}