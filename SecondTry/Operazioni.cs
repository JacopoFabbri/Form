using System;
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
    public partial class Operazioni : Form
    {
        private static String Path = "\\\\192.168.1.250\\Occupazioni Suolo\\Documenti per Occupazione\\";
        private List<VisualString> Lista = new List<VisualString>();
        private Occupazione Occupazione;
        public Operazioni(Occupazione oc)
        {
            this.Occupazione = oc;
            InitializeComponent();
        }
        private void Operazioni_Load(object sender, EventArgs e)
        {
            UpdateList();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            UpdateFilterList(textBox1.Text);
        }
        public void UpdateList()
        {
            try
            {
                Lista = new List<VisualString>();
                var listOfPath = Directory.GetFiles(Path);
                foreach (var file in listOfPath)
                {
                    var elem = file.Remove(0, Path.Length);
                    Lista.Add(new VisualString() { File_Name = elem, Path = file });
                }
                listView1.Items.Clear();
                foreach (var file in Lista)
                {
                    listView1.Items.Add(file.File_Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void UpdateFilterList(String FileName)
        {
            try
            {
                Lista = new List<VisualString>();
                var listOfPath = Directory.GetFiles(Path).Where(x => x.Contains(FileName));
                foreach (var file in listOfPath)
                {
                    var elem = file.Remove(0, Path.Length);
                    Lista.Add(new VisualString() { File_Name = elem, Path = file });
                }
                listView1.Items.Clear();
                foreach (var file in Lista)
                {
                    listView1.Items.Add(file.File_Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            UpdateList();
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
            var pathNewDirectory = Path;
            OpenFolder(pathNewDirectory);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var partenza = Lista[listView1.SelectedIndices[0]].Path;
                var arrivo = Occupazione.Cartella_Destinazione + "\\" + Lista[listView1.SelectedIndices[0]].File_Name;
                File.Copy(partenza, arrivo);
                MessageBox.Show("Inserito Correttamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            OpenFolder(Occupazione.Cartella_Destinazione);
        }
    }
    internal class VisualString
    {
        public String Path { get; set; }
        public String File_Name { get; set; }
    }
}
