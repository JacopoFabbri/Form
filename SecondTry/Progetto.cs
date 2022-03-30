﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecondTry
{
    public partial class Progetto : Form
    {
        public static OccupazioneContext Context = new OccupazioneContext();
        public static List<Occupazione> ListaOccupazione = new List<Occupazione>();
        public static Operazioni Operazione;
        public Progetto()
        {
            InitializeComponent();
            button1.Enabled = false;
            button4.Enabled = false;
        }

        private void Progetto_Load(object sender, EventArgs e)
        {
            try
            {
                ListaOccupazione = Context.Occupaziones.Where(x => x.Id != 0).ToList();
                foreach (var occupazione in ListaOccupazione)
                {
                    listView1.Items.Add(occupazione.Indirizzo);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Controllare la connessione al Database");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Operazione = new Operazioni(ListaOccupazione[listView1.SelectedIndices[0]]);
            Operazione.ShowDialog();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button4.Enabled = true;
            Operazione = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ins = new Inserimento(Context, this);
            ins.ShowDialog();
        }
        public void UpdateList()
        {
            try
            {
                ListaOccupazione = Context.Occupaziones.Where(x => x.Id != 0).ToList();
                listView1.Items.Clear();
                foreach (var occupazione in ListaOccupazione)
                {
                    listView1.Items.Add(occupazione.Indirizzo);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Controllare la connessione al Database");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Context.Occupaziones.Remove(Context.Occupaziones.Where((x) => x.Indirizzo == ListaOccupazione[listView1.SelectedIndices[0]].Indirizzo).ToList()[0]);
                Context.SaveChanges();
                UpdateList();
                MessageBox.Show("ho cancellato il record dal db. Se si vuole cancellare anche i file procedi manualmente");
            }
            catch (Exception)
            {
                MessageBox.Show("Impossibile rimuovere l'elemento");
            }
        }
        public void UpdateFilterList(String s, bool flag)
        {
            try
            {
                if (flag)
                    ListaOccupazione = Context.Occupaziones.Where(x => x.Indirizzo.Contains(s)).ToList();
                else
                    ListaOccupazione = Context.Occupaziones.Where(x => x.Commessa.Contains(s)).ToList();

                listView1.Items.Clear();
                foreach (var occupazione in ListaOccupazione)
                {
                    listView1.Items.Add(occupazione.Indirizzo);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Controllare la connessione al Database");
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            UpdateFilterList(textBox1.Text, true);
        }
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            var pathNewDirectory =  Context.Occupaziones.Where((x) => x.Indirizzo == ListaOccupazione[listView1.SelectedIndices[0]].Indirizzo).ToList()[0].Cartella_Destinazione;
            OpenFolder(pathNewDirectory);
        }
        private void OpenFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = folderPath,
                    FileName = "explorer.exe"
                };
                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show("Cartella non esistente");
            }

        }
        private void button6_Click(object sender, EventArgs e)
        {
            var pathNewDirectory = "\\\\192.168.1.250\\Occupazioni Suolo\\Test Nuovo Programma\\";
            OpenFolder(pathNewDirectory);
        }
    }

}
