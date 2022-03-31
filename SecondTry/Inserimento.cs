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
        private static String Path = "\\\\192.168.1.250\\Occupazioni Suolo\\Test Nuovo Programma\\";
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
                var pathNewDirectory = Path + textBox1.Text;
                Directory.CreateDirectory(pathNewDirectory);
                if (Context.Occupazione.Where(x => x.Indirizzo == textBox1.Text).ToList().Count() > 0)
                {
                    MessageBox.Show("Indirizzo Gia Presente nel DB");
                }
                else
                {
                    Context.Occupazione.Add(new Occupazione() { Indirizzo = textBox1.Text, Commessa = textBox2.Text + "", DataInserimento = DateTime.Now, Cartella_Destinazione = pathNewDirectory });
                    Context.SaveChanges();
                    this.Close();
                }
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

        private void Inserimento_Load(object sender, EventArgs e)
        {

        }
    }
}
