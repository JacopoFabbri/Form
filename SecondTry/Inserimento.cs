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
    public partial class Inserimento : Form
    {
        private OccupazioneContext Context;
        private Progetto FormPrecedente;
        public Inserimento(OccupazioneContext context, Progetto progetto)
        {
            this.FormPrecedente = progetto;
            this.Context = context;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var pathNewDirectory = "\\\\192.168.1.250\\Occupazioni Suolo\\Test Nuovo Programma\\" + textBox1.Text;
                Directory.CreateDirectory(pathNewDirectory);
                Context.Occupaziones.Add(new Occupazione() { Indirizzo = textBox1.Text, Commessa = textBox2.Text + "", DataInserimento = DateTime.Now, Cartella_Destinazione = pathNewDirectory });
                Context.SaveChanges();
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Impossibile creare la cartella di destinazione");
            }
        }

        private void Inserimento_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormPrecedente.UpdateList();
        }
    }
}
