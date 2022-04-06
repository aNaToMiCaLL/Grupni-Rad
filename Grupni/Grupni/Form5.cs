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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            PrikazTabele(); 
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        public static String konekcioniString = "Server=localhost; Port=3306; " +
            "Database=prodavnica; Uid=root; Pwd=";

        private void PrikazTabele() 
        {
            try
            {
                string query = "select narudzbenica_id from narudzbenica";
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
        private void DodajUTabelu()
        {
            try
            {
                string query = "select artikal.naziv_artikla,artikal.vrsta_artikla,artikal.cijena,stavka_narudzbenice.kolicina from artikal,stavka_narudzbenice,narudzbenica "+
                "where artikal.artikal_id=stavka_narudzbenice.artikal_id and stavka_narudzbenice.narudzebnica_id=narudzbenica.narudzbenica_id "+
                "and narudzbenica.narudzbenica_id='"+textBox1.Text+"'";
                MySqlConnection konekcija = new MySqlConnection(konekcioniString);
                konekcija.Open();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, konekcija);
                DataTable tabela = new DataTable();
                dataAdapter.Fill(tabela);
                dataGridView2.DataSource = tabela;
                dataAdapter.Dispose();
                konekcija.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DodajUTabelu();
        }

        private void kreiranjeNarudžbeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 fr4 = new Form4();
            fr4.Show();
        }
    }
}
