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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        public static String kupacID;
      
        public static String konekcioniString = "Server=localhost; Port=3306; " +
            "Database=prodavnica; Uid=root; Pwd=";

        private void button1_Click(object sender, EventArgs e)
        {

            errorProvider1.Clear();

            String korisnickoIme = textBox1.Text;
            String sifra = textBox2.Text;


            String query = "SELECT password, CONCAT(ime, ' ', prezime), " +
                " kupac_id  FROM kupac WHERE username ='" + korisnickoIme + "' ";

            try
            {
                MySqlConnection konekcija = new MySqlConnection(konekcioniString);
                konekcija.Open();

                MySqlCommand cmd = new MySqlCommand(query, konekcija);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                reader.Read();

                if (!reader.HasRows)
                {
                    errorProvider1.SetError(textBox1, "Netačno korisničko ime !!!");
                }

                else
                {
                    kupacID = reader[2].ToString();
                }

              if (sifra == reader[0].ToString())
                {   


                    if (Convert.ToInt32(kupacID) == 1)
                    {

                        this.Hide();
                        Form1 fr1 = new Form1();
                        fr1.Show();

                    }
                     else
                    {
                        MessageBox.Show("Uspješno ste logovani " + reader[1].ToString());
                        this.Hide();
                        Form5 fr5 = new Form5();
                        fr5.Show();
                    }

                }
                else
                {
                    errorProvider1.SetError(textBox2, "Netačna šifra !!! ");
                }

                reader.Dispose();
                konekcija.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


    }
}
