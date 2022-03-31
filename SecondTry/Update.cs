using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecondTry
{
    public partial class Update : Form
    {
        private OccupazioneContext Context;
        private Progetto FormPrecedente;
        private Occupazione Record;
        private static String Path = "\\\\192.168.1.250\\Occupazioni Suolo\\Test Nuovo Programma\\";
        public Update(OccupazioneContext context, Progetto progetto, Occupazione oc)
        {
            this.Record = oc;
            this.FormPrecedente = progetto;
            this.Context = context;
            InitializeComponent();
        }

        private void Update_Load(object sender, EventArgs e)
        {
            textBox1.Text = Record.Indirizzo;
            textBox2.Text = Record.Commessa;
        }

        private void Update_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormPrecedente.UpdateList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String path = Record.Cartella_Destinazione;
                var aggiornato = Record;
                aggiornato.Commessa = textBox2.Text;
                aggiornato.Indirizzo = textBox1.Text;
                aggiornato.Cartella_Destinazione = Path + textBox1.Text;
                Context.Occupazione.Update(aggiornato);
                Context.SaveChanges();
                if (!path.Equals(Path + textBox1.Text))
                {
                    Directory.Move(path, Path + textBox1.Text);
                    MessageBox.Show("Modifica Effettuata");
                }
                else
                {
                    MessageBox.Show("Modifica Effettuata nel db. Non è stato possibile cambiare il nome della cartella");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossibile esguire l'aggiornamento dei valori");
            }
        }
    }
}
