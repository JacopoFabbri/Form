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
        private static String Path = "\\\\192.168.1.250\\Occupazioni Suolo\\Documenti per Occupazione\\_per programma Jacopo\\";
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
            button10.Enabled = false;
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
                var attr = File.GetAttributes(Lista[listView1.SelectedIndices[0]].Path);
                if (!attr.HasFlag(FileAttributes.Directory))
                {
                    var nFile = listView1.SelectedIndices.Count;
                    var trasferiti = 0;
                    foreach (int selectedIndex in listView1.SelectedIndices)
                    {
                        var fInfoArrivo = new FileInfo(Occupazione.Cartella_Destinazione + "\\" + Lista[selectedIndex].File_Name);
                        if (!fInfoArrivo.Exists)
                            File.Copy(Lista[selectedIndex].Path, Occupazione.Cartella_Destinazione + "\\" + Lista[selectedIndex].File_Name);
                        trasferiti++;
                        progressBar1.Value = trasferiti*100/nFile;
                    }
                }
                else
                {
                    var nFile = listView1.SelectedIndices.Count;
                    var trasferiti = 0;
                    foreach (int selectedIndex in listView1.SelectedIndices)
                    {
                        var dInfoT = new DirectoryInfo(Lista[selectedIndex].Path);
                        if (!dInfoT.Name.Equals("Ponteggi e Cesate"))
                        {
                            var partenza = Path + dInfoT.Name + "\\";
                            var arrivo = Occupazione.Cartella_Destinazione + "\\" + dInfoT.Name + "\\";
                            Directory.CreateDirectory(arrivo);
                            foreach (var d in Directory.GetDirectories(partenza))
                            {
                                var dInfo = new DirectoryInfo(d);
                                Directory.CreateDirectory(arrivo + dInfo.Name);
                            }
                            foreach (var f in Directory.GetFiles(partenza))
                            {
                                var fInfo = new FileInfo(f);
                                var fInfoArrivo = new FileInfo(arrivo + fInfo.Name);
                                if (!fInfoArrivo.Exists)
                                    File.Copy(f, arrivo + fInfo.Name);
                            }
                        }
                        else
                        {
                            var partenza = Path + dInfoT.Name + "\\";
                            var arrivo = Occupazione.Cartella_Destinazione + "\\";
                            Directory.CreateDirectory(arrivo);
                            foreach (var d in Directory.GetDirectories(partenza))
                            {
                                var dInfo = new DirectoryInfo(d);
                                Directory.CreateDirectory(arrivo + dInfo.Name);
                            }
                            foreach (var f in Directory.GetFiles(partenza))
                            {
                                var fInfo = new FileInfo(f);
                                var fInfoArrivo = new FileInfo(arrivo + fInfo.Name);
                                if (!fInfoArrivo.Exists)
                                    File.Copy(f, arrivo + fInfo.Name);
                            }
                        }
                        trasferiti++;
                        progressBar1.Value = trasferiti * 100 / nFile;
                    }
                }
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
        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                var partenza = Path + "Ponteggi e Cesate\\";
                var arrivo = Occupazione.Cartella_Destinazione + "\\";
                var listaDir = Directory.GetDirectories(partenza);
                var nFile = listaDir.Length;
                var trasferiti = 0;
                foreach (var d in listaDir)
                {
                    var dInfo = new DirectoryInfo(d);
                    Directory.CreateDirectory(arrivo + dInfo.Name);
                    trasferiti++;
                    progressBar1.Value = trasferiti * 100 / nFile;
                }
                var listaFile = Directory.GetFiles(partenza);
                nFile = listaFile.Length;
                trasferiti = 0;
                progressBar1.Value = 0;
                foreach (var f in listaFile)
                {
                    var fInfo = new FileInfo(f);
                    
                    File.Copy(f, arrivo + fInfo.Name);
                    trasferiti++;
                    progressBar1.Value = trasferiti * 100 / nFile;
                }
                MessageBox.Show("Inserito Correttamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                var partenza = Path + "Passo Carraio\\";
                var arrivo = Occupazione.Cartella_Destinazione + "\\Passo Carraio\\";
                Directory.CreateDirectory(arrivo);
                var listaDir = Directory.GetDirectories(partenza);
                var nFile = listaDir.Length;
                var trasferiti = 0;
                foreach (var d in listaDir)
                {
                    var dInfo = new DirectoryInfo(d);
                    Directory.CreateDirectory(arrivo + dInfo.Name);
                    trasferiti++;
                    progressBar1.Value = trasferiti * 100 / nFile;
                }
                var listaFile = Directory.GetFiles(partenza);
                nFile = listaFile.Length;
                trasferiti = 0;
                progressBar1.Value = 0;
                foreach (var f in listaFile)
                {
                    var fInfo = new FileInfo(f);

                    File.Copy(f, arrivo + fInfo.Name);
                    trasferiti++;
                    progressBar1.Value = trasferiti * 100 / nFile;
                }
                MessageBox.Show("Inserito Correttamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                var partenza = Path + "Modulo A\\";
                var arrivo = Occupazione.Cartella_Destinazione + "\\Modulo A\\";
                Directory.CreateDirectory(arrivo);
                var listaDir = Directory.GetDirectories(partenza);
                var nFile = listaDir.Length;
                var trasferiti = 0;
                foreach (var d in listaDir)
                {
                    var dInfo = new DirectoryInfo(d);
                    Directory.CreateDirectory(arrivo + dInfo.Name);
                    trasferiti++;
                    progressBar1.Value = trasferiti * 100 / nFile;
                }
                var listaFile = Directory.GetFiles(partenza);
                nFile = listaFile.Length;
                trasferiti = 0;
                progressBar1.Value = 0;
                foreach (var f in listaFile)
                {
                    var fInfo = new FileInfo(f);

                    File.Copy(f, arrivo + fInfo.Name);
                    trasferiti++;
                    progressBar1.Value = trasferiti * 100 / nFile;
                }
                MessageBox.Show("Inserito Correttamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateList();
                button10.Enabled = false;
                button11.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void UpdateListDirectory()
        {
            try
            {
                Lista = new List<VisualString>();
                var listOfPath = Directory.GetDirectories(Path);
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
        private void button11_Click(object sender, EventArgs e)
        {
            UpdateListDirectory();
            button10.Enabled = true;
            button11.Enabled = false;
        }
    }
    internal class VisualString
    {
        public String Path { get; set; }
        public String File_Name { get; set; }
    }
}
