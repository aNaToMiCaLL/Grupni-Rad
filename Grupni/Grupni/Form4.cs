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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            PrikaziTabelu();
            PrikaziTabelu2();
        }
        public static String konekcioniString = "Server=localhost; Port=3306; " +
            "Database=prodavnica; Uid=root; Pwd=";

        private void PrikaziTabelu()
        {
            try
            {
                string query = "select artikal.artikal_id,naziv_artikla,vrsta_artikla,cijena,kolicina_stanje from artikal,skladiste " +
                    "where artikal.artikal_id=skladiste.artikal_id";
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
        private void PrikaziTabelu2() 
        {
            try
            {
            string query1 = "Select a.artikal_id,a.naziv_artikla,a.cijena,n.kolicina " +
                "from artikal a,stavka_narudzbenice n,narudzbenica b " +
                "where a.artikal_id=n.artikal_id and n.narudzebnica_id=b.narudzbenica_id and b.narudzbenica_id=(select max(narudzbenica_id) from narudzbenica); "; // Prikazuje drugu tabelu
            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            konekcija.Open();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query1, konekcija);
            DataTable tabela = new DataTable();
            dataAdapter.Fill(tabela);
            dataGridView2.DataSource = tabela;
            dataAdapter.Dispose();
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

                string query = "UPDATE skladiste s,stavka_narudzbenice n " +
                "SET s.kolicina_stanje=s.kolicina_stanje-'" + Convert.ToInt32(textBox2.Text) + "' " +
                "where s.artikal_id=n.artikal_id and n.artikal_id='" + Convert.ToInt32(textBox1.Text) + "'; "; //Update vrijednost u tabeli

                string upit = "INSERT INTO stavka_narudzbenice(artikal_id,narudzebnica_id,kolicina)VALUES('" + textBox1.Text + "',(select max(narudzbenica_id) from narudzbenica),'" + textBox2.Text + "'); " +
                "select kolicina,kolicina_stanje,s.artikal_id from skladiste s,stavka_narudzbenice n where s.artikal_id=n.artikal_id"; 
                // Dodaje vrijednost i uzima vrijednosti kolicine i koliko je na stanju

                string update = "UPDATE stavka_narudzbenice,narudzbenica SET kolicina=kolicina+'"+Convert.ToInt32(textBox2.Text)+"' where artikal_id='"+Convert.ToInt32(textBox1.Text)+"'"+
                    " AND stavka_narudzbenice.narudzebnica_id=(select max(narudzbenica_id) from narudzbenica)";

                string upit1 = "DELETE FROM stavka_narudzbenice ORDER BY stavka_id DESC LIMIT 1"; // brise ukoliko je a>b

                MySqlConnection konekcija = new MySqlConnection(konekcioniString);
                konekcija.Open();

                MySqlCommand insel = new MySqlCommand(upit, konekcija);
                MySqlDataReader reader = insel.ExecuteReader();
                MySqlCommand upd1 = new MySqlCommand(query, konekcija);
                reader.Read();
                string a = reader[0].ToString();
                string b = reader[1].ToString();
                string id = reader[2].ToString();
                reader.Close();
                MySqlCommand del = new MySqlCommand(upit1, konekcija);
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    query = "";
                    MessageBox.Show("Niste unijeli sve podatke");
                }
                else if (Convert.ToInt32(textBox2.Text) > Convert.ToInt32(b))
                {
                    MessageBox.Show("Nema dovoljno u skladištu");
                    del.ExecuteNonQuery();
                }
                else if (Convert.ToInt32(textBox2.Text) <= 0)
                {
                    MessageBox.Show("Količina ne smije biti manja od 1");
                    del.ExecuteNonQuery();
                }
                else
                {
                    MySqlCommand upd = new MySqlCommand(update, konekcija);
                    int x = upd.ExecuteNonQuery();
                    upd1.ExecuteNonQuery();
                    del.ExecuteNonQuery();
                    if (x == 1)
                    {
                        insel.ExecuteNonQuery();
                        MessageBox.Show("Dodan novi artikal");
                    }
                }
                
                konekcija.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DodajUTabelu();
            PrikaziTabelu();
            PrikaziTabelu2();
        }

        private void IzbrisiIzTabele() 
        {
            try
            {

                string update = "UPDATE skladiste s,stavka_narudzbenice n, narudzbenica b " +
                "SET s.kolicina_stanje=s.kolicina_stanje+'" + Convert.ToInt32(textBox2.Text)+ "',n.kolicina=n.kolicina-'"+Convert.ToInt32(textBox2.Text)+"' " +
                "where s.artikal_id=n.artikal_id and n.artikal_id='" + Convert.ToInt32(textBox1.Text) + "' and n.narudzebnica_id=(select max(narudzbenica_id) from narudzbenica)";

                string delete="DELETE From stavka_narudzbenice where artikal_id='"+Convert.ToInt32(textBox1.Text)+"' and stavka_narudzbenice.narudzebnica_id=(select max(narudzbenica_id) from narudzbenica)";

                string reader = "select s.artikal_id,n.kolicina from skladiste s,stavka_narudzbenice n,narudzbenica where s.artikal_id=n.artikal_id and s.artikal_id='"+Convert.ToInt32(textBox1.Text)+"' "+
                    "and n.narudzebnica_id=(select max(narudzbenica_id) from narudzbenica)"; 

                MySqlConnection konekcija = new MySqlConnection(konekcioniString);
                konekcija.Open();
                MySqlCommand read = new MySqlCommand(reader, konekcija);
                MySqlCommand upd = new MySqlCommand(update, konekcija);
                MySqlCommand del = new MySqlCommand(delete, konekcija);
                MySqlDataReader Reader = read.ExecuteReader();
                Reader.Read();
                int a = Convert.ToInt32(Reader[0].ToString());
                int b = Convert.ToInt32(Reader[1].ToString());
                Reader.Close();
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    delete = "";
                    MessageBox.Show("Niste unijeli sve podatke"); 
                }
                else if (b < Convert.ToInt32(textBox2.Text))
                {
                    MessageBox.Show("Ne mozete ukloniti vise artikala nego sto imate");
                }
                else if (Convert.ToInt32(textBox2.Text) <= 0)
                {
                    MessageBox.Show("Količina ne smije biti manja od 1");
                    del.ExecuteNonQuery();
                }
                else
                {
                   MySqlCommand cmd = new MySqlCommand(delete, konekcija);
                    upd.ExecuteNonQuery();
                    MySqlDataReader cmd1 = read.ExecuteReader();
                    cmd1.Read();
                    int y = Convert.ToInt32(cmd1[1].ToString());
                    cmd1.Close();
                    if (y == 0)
                        {
                         del.ExecuteNonQuery();
                        }
                }
                konekcija.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            IzbrisiIzTabele();
            PrikaziTabelu();
            PrikaziTabelu2();
        }

        private void KreirajNarudzbu()
        {
            DateTime now = DateTime.Now;
            string query = "INSERT INTO narudzbenica(datum_narudzbe) VALUES('" + now.ToString("yyyy-MM-dd") + "')";
            string nar = "select count(s.narudzebnica_id) from stavka_narudzbenice s,narudzbenica n where s.narudzebnica_id=(select max(narudzbenica_id) from narudzbenica)";
            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            konekcija.Open();
            MySqlCommand cmd1 = new MySqlCommand(nar, konekcija);
            MySqlDataReader x = cmd1.ExecuteReader();
            x.Read();
            int br = Convert.ToInt32(x[0].ToString());
            x.Close();
            if (br == 0)
            {
                MessageBox.Show("Dodajte nešta u trenutnu korpu");
            }
            else {
                MessageBox.Show("Zapoceli ste novu narudzbu");
            MySqlCommand cmd = new MySqlCommand(query, konekcija);
            cmd.ExecuteNonQuery();
            }
            konekcija.Dispose();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            KreirajNarudzbu();
            PrikaziTabelu();
            PrikaziTabelu2();
        }

        private void prikazNarudžbiIStavkiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 fr5=new Form5();
            fr5.Show();
        }
    }
}
