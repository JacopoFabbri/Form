using System.Data.SqlClient;

namespace SecondTry
{
    public partial class Operazioni : Form
    {
        public Occupazione occupazione = null;
        public Operazioni operazione = null;
        public Operazioni(Occupazione oc)
        {
            InitializeComponent();
            occupazione = oc;
            operazione = this;
        }

        private void Operazioni_Load(object sender, EventArgs e)
        {
            if (occupazione == null)
            {
                MessageBox.Show("Elemento Selezionato non presente");
            }
            else
            {
                try
                {
                    var pathCartelle = Directory.GetDirectories(occupazione.Cartella_Destinazione);
                    foreach (var item in pathCartelle)
                    {
                        var split = item.Split('\\');
                        listView1.Items.Add(new ListViewItem()
                        {
                            Name = split.Last(),
                            Text = "" + split.Last(),
                            //BackColor = Color.Green,
                            //ForeColor = Color.White,
                            Checked = true
                        });
                    }
                }catch (Exception)
                {
                    MessageBox.Show("Percorson non trovato");
                    this.Close();

                }
            }
        }
    }
}