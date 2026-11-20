using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Futoverseny
{
    public partial class Form1 : Form
    {
        // Ebben a listában tároljuk a beolvasott versenyzőket
        private List<Versenyzo> versenyzok = new List<Versenyzo>();

        public Form1()
        {
            InitializeComponent();
        }

        // Fájl / Megnyit menüpont eseménykezelője
        private void megnyitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdatokBeolvasasa();
        }

        // Ha az "adatBe" gombra is kattintanak, ugyanazt hajtsa végre
        private void btnAdatBe_Click(object sender, EventArgs e)
        {
            AdatokBeolvasasa();
        }

        private void AdatokBeolvasasa()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Szöveges fájlok (*.txt)|*.txt|Minden fájl (*.*)|*.*";
            ofd.FileName = "futok.txt";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                versenyzok.Clear();
                listBoxVersenyzok.Items.Clear();

                try
                {
                    string[] sorok = File.ReadAllLines(ofd.FileName);
                    foreach (string sor in sorok)
                    {
                        if (!string.IsNullOrWhiteSpace(sor))
                        {
                            Versenyzo v = new Versenyzo(sor);
                            versenyzok.Add(v);
                            listBoxVersenyzok.Items.Add(v.Nev);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba a fájl beolvasásakor: " + ex.Message);
                }
            }
        }

        // Ha kiválasztunk egy versenyzőt a listából, jelenjenek meg az adatai
        private void listBoxVersenyzok_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBoxVersenyzok.SelectedIndex;
            if (index >= 0 && index < versenyzok.Count)
            {
                Versenyzo kivalasztott = versenyzok[index];
                txtRajtszam.Text = kivalasztott.Rajtszam;
                txtOrszag.Text = kivalasztott.Orszag;
                txtIdo.Text = kivalasztott.Idoeredmeny.ToString();
                txtEletkor.Text = kivalasztott.Eletkor.ToString();
            }
        }

        // Eredménylista menüpontra kattintva új ablak megnyitása
        private void eredmenylistaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (versenyzok.Count == 0)
            {
                MessageBox.Show("Előbb olvasd be a versenyzők adatait!");
                return;
            }

            FormEredmeny eredmenyAblak = new FormEredmeny(versenyzok);
            eredmenyAblak.ShowDialog();
        }

        // Bezárás gomb
        private void btnBezar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}