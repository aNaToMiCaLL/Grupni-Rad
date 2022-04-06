using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            TableUpdate();
        }

        public static String konekcioniString = "Server=localhost; Port=3306; " +
            "Database=prodavnica; Uid=root; Pwd=";
        private void TableUpdate()
        {
            try
            {
                string query = "select narudzbenica.narudzbenica_id,narudzbenica.kupac_id,narudzbenica.datum_narudzbe,"+
                    "kupac.ime,kupac.prezime from narudzbenica,kupac where narudzbenica.kupac_id=kupac.kupac_id";
                MySqlConnection konekcija = new MySqlConnection(konekcioniString);
                konekcija.Open();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, konekcija);
                DataTable tabela = new DataTable();
                dataAdapter.Fill(tabela);
                dataGridView1.DataSource = tabela;
                dataAdapter.Dispose();
                konekcija.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Brisanje() 
        {
            try
            {
                string query = "DELETE from stavka_narudzbenice where narudzebnica_id='" + textBox1.Text + "';" + 
                    "DELETE from narudzbenica where narudzbenica_id='" + textBox1.Text + "';";
                MySqlConnection konekcija = new MySqlConnection(konekcioniString);
                konekcija.Open();

                MySqlCommand cmd = new MySqlCommand(query, konekcija);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Izbrisana narudzba !!!");

                konekcija.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Brisanje();
            TableUpdate();
        }

        private void kreiranjeAžuriranjeKorisnikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 fr1 = new Form1();
            fr1.Show();
        }

        private void dodavanjeAžuriranjeArtikalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 fr2 = new Form2();
            fr2.Show();
        }
    }
}
