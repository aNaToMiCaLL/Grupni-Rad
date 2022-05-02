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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TableUpdate();
        }

        string konekcioniString = Form6.konekcioniString;

        private void TableUpdate() 
        {
            try
            {
                string query = "select * from kupac";
                if (textBox1.Text != "" || textBox2.Text != "") query += " where ime=ime";
                if (textBox1.Text != "") query += " and ime LIKE '" + textBox1.Text + "%' ";
                if (textBox2.Text != "") query += " and prezime LIKE '" + textBox2.Text + "%' ";
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
                String upit = "INSERT INTO kupac(ime, prezime, grad, adresa, " +
                    " telefon, username, password) VALUES " +
                    " ('" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', " +
                    " '" + textBox6.Text + "', '" + textBox8.Text + "', " +
                    " '" + textBox9.Text + "', '" + textBox10.Text + "') ";
                if (textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "")
                {
                    MessageBox.Show("Niste unijeli sve podatke");
                    upit = "";
                }

                MySqlConnection konekcija = new MySqlConnection(konekcioniString);
                konekcija.Open();

                MySqlCommand cmd = new MySqlCommand(upit, konekcija);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Dodan novi korisnik !!!");

                konekcija.Close();
            }
            catch (Exception greska)
            {
                MessageBox.Show(greska.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DodavanjePodataka();
            TableUpdate();
        }

        private void AzuriranjePodataka()
        {
            try
            {
                String upit = "UPDATE kupac SET ";
                if(textBox3.Text!="") upit+=" ime='" + textBox3.Text + "', ";
                if (textBox4.Text != "") upit += " prezime='" + textBox4.Text + "', " ;
                if (textBox5.Text != "") upit +=    " grad='" + textBox5.Text + "', " ;
                if (textBox6.Text != "") upit +=    " adresa='" + textBox6.Text + "', " ;
                if (textBox8.Text != "") upit +=   " telefon='" + textBox8.Text + "', " ;
                if (textBox9.Text != "") upit += " username='" + textBox9.Text + "', ";
                if (textBox10.Text != "") upit += " password='" + textBox10.Text + "', ";
                upit += " kupac_id='"+textBox7.Text+"'";
                upit+= " WHERE kupac_id='" + textBox7.Text + "' ";

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

        private void button3_Click(object sender, EventArgs e)
        {
            AzuriranjePodataka();
            TableUpdate();
        }

        private void bToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 fr3 = new Form3();
            fr3.Show();
        }

        private void kreiranjeAžuriranjeArtikalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 fr2=new Form2();
            fr2.Show();
        }

     

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void izlazIzAplikacijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      

     

       
    }
}
