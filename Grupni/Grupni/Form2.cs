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
    public partial class Form2 : Form
    {
        public Form2()
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
                string query = "select artikal.artikal_id,artikal.naziv_artikla,artikal.vrsta_artikla,artikal.cijena,skladiste.kolicina_stanje"+
                    " from artikal,skladiste where artikal.artikal_id=skladiste.artikal_id";
                if (textBox1.Text != "") query += " and artikal.artikal_id LIKE'" + textBox1.Text + "%' ";
                if (textBox2.Text != "") query += " and artikal.naziv_artikla LIKE'" + textBox2.Text + "%' ";
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

        private void button1_Click(object sender, EventArgs e)
        {
            TableUpdate();
        }

        private void DodavanjePodataka()
        {
            try
            {

                String upit = "INSERT INTO artikal(naziv_artikla, vrsta_artikla, cijena ) VALUES " +
                    " ('" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "');" +
                    "INSERT INTO skladiste(kolicina_stanje,artikal_id) VALUES ('" + textBox6.Text + "',(select max(artikal_id) from artikal));";
                if (textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
                {
                    MessageBox.Show("Niste unijeli sve podatke");
                    upit = "";
                }
                MySqlConnection konekcija = new MySqlConnection(konekcioniString);
                konekcija.Open();

                MySqlCommand cmd = new MySqlCommand(upit, konekcija);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Dodan novi artikal !!!");

                konekcija.Close();
            }
            catch (Exception greska)
            {
                MessageBox.Show(greska.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DodavanjePodataka();
            TableUpdate();
        }

        private void AzuriranjePodataka()
        {
            try
            {
                String upit = "UPDATE artikal,skladiste SET ";
               // if (textBox4.Text != "") upit += " artikal.vrsta_artikla='" + textBox4.Text + "', ";
               // if (textBox5.Text != "") upit += " artikal.cijena='" + textBox5.Text + "', ";
               // if (textBox6.Text != "") upit += " skladiste.kolicina_stanje='" + textBox6.Text + "', ";
                if (numericUpDown1.Value != 0) upit += " skladiste.kolicina_stanje=skladiste.kolicina_stanje+'" + numericUpDown1.Value + "', ";
                upit += " artikal.artikal_id='"+textBox7.Text+"'";
                upit += " WHERE artikal.artikal_id=skladiste.artikal_id and artikal.artikal_id='" + textBox7.Text + "' ";
                //if (textBox6.Text != "" && numericUpDown1.Value != 0)
                //{
                    //MessageBox.Show("Ne mozete azurirati i dodati kolicinu u isto vrijeme");
                    //upit = "";
                //}


                MySqlConnection konekcija = new MySqlConnection(konekcioniString);
                konekcija.Open();

                MySqlCommand cmd = new MySqlCommand(upit, konekcija);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Podaci za korisnika ID=" + textBox7.Text + " su ažurirani!!!");

                konekcija.Close();
            }
            catch (Exception greska)
            {
                MessageBox.Show(greska.Message);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            AzuriranjePodataka();
            TableUpdate();
        }

        private void kreiranjeAžuriranjeKorisnikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 fr1 = new Form1();
            fr1.Show();
        }

        private void prikazBrisanjeNarudžbiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 fr3 = new Form3();
            fr3.Show();
        }

    }
}
