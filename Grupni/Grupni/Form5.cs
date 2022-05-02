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

        string konekcioniString = Form6.konekcioniString;
        string kupacID = Form6.kupacID;

        private void PrikazTabele() 
        {
            try
            {
                string query = "select narudzbenica_id from narudzbenica where kupac_id='" + kupacID + "'";
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
                "where artikal.artikal_id=stavka_narudzbenice.artikal_id and stavka_narudzbenice.narudzbenica_id=narudzbenica.narudzbenica_id "+
                "and narudzbenica.narudzbenica_id='" + textBox1.Text + "' and kupac_id='" + kupacID + "'";
                MySqlConnection konekcija = new MySqlConnection(konekcioniString);
                konekcija.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand(query, konekcija);
                reader = cmd.ExecuteReader();
                reader.Read();
                if(reader.HasRows)
                {
                    reader.Close();
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, konekcija);
                    DataTable tabela = new DataTable();
                    dataAdapter.Fill(tabela);
                    dataGridView2.DataSource = tabela;
                    dataAdapter.Dispose();
                }
                else 
                {
                    reader.Close();
                    MessageBox.Show("Ukucajte ID vaše narudžbe!");
                }
                konekcija.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }
        private void Cijena()
        {
            try
            {
                string query = "select sum(s.količina*a.cijena) from artikal a,stavka_narudzbenice s,narudzbenica n " +
                "where a.artikal_id=s.artikal_id and s.narudzbenica_id=n.narudzbenica_id " +
                "and n.narudzbenica_id='" + textBox1.Text + "' and kupac_id='" + kupacID + "'";
                MySqlConnection konekcija = new MySqlConnection(konekcioniString);
                konekcija.Open();
                MySqlCommand cmd = new MySqlCommand(query, konekcija);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                reader.Read();
                textBox3.Text = reader[0].ToString();
                reader.Close();
                konekcija.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DodajUTabelu();
            Cijena();
        }

        private void kreiranjeNarudžbeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 fr4 = new Form4();
            fr4.Show();
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void izlazIzAplikacijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
