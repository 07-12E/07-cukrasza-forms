using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Futoverseny
{
    public partial class FormEredmeny : Form
    {
        private List<Versenyzo> rendezettLista;

        public FormEredmeny(List<Versenyzo> versenyzok)
        {
            InitializeComponent();

            rendezettLista = versenyzok.OrderByDescending(v => v.Idoeredmeny).ToList();

            foreach (var v in rendezettLista)
            {
                listBoxEredmenyek.Items.Add($"{v.Nev} - {v.Idoeredmeny}");
            }
        }

        private void mentesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> kiirandoSorok = new List<string>();
                foreach (var v in rendezettLista)
                {
                    kiirandoSorok.Add($"{v.Nev};{v.Idoeredmeny}");
                }

                File.WriteAllLines("EREDMENYEK.txt", kiirandoSorok);
                MessageBox.Show("Sikeres mentés az EREDMENYEK.txt fájlba!", "Mentés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a mentés során: " + ex.Message);
            }
        }
    }
}