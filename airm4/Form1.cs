using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace airm4
{
    public partial class Form1 : Form
    {
        private DbHelper dbHelper = new DbHelper();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
            ComboboxDoldur("merkez", "merkez", merkez);
            ComboboxDoldur("ucaklar", "ucak_model", ucak_model);
            ComboboxDoldur("ucaklar", "ucak_model", filtucak);
            ComboboxDoldur("merkez", "merkez", filtrmerkez);
        }

        private void ComboboxDoldur(string tabloAd, string kolonAd, ComboBox comboBox)
        {
            using (OleDbConnection connection = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=manager.mdb"))
            {
                OleDbCommand command = new OleDbCommand($"SELECT {kolonAd} FROM {tabloAd}", connection);
                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox.Items.Add(reader[kolonAd].ToString());
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Listele()
        {
            dataGridView1.DataSource = dbHelper.ListAllCalistir("rota");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(duz_merkez.Text) && string.IsNullOrEmpty(duz_ucak.Text))
            {
                dbHelper.Ekle("merkez", "merkez", duz_merkez.Text);
            }
            else if (string.IsNullOrEmpty(duz_merkez.Text) && !string.IsNullOrEmpty(duz_ucak.Text))
            {
                dbHelper.Ekle("ucaklar", "ucak_model", duz_ucak.Text);
            }
            MessageBox.Show("Başarıyla eklendi");
            Listele();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(duz_merkez.Text) && string.IsNullOrEmpty(duz_ucak.Text))
            {
                dbHelper.TumunuSil("merkez", "merkez", duz_merkez.Text);
            }
            else if (string.IsNullOrEmpty(duz_merkez.Text) && !string.IsNullOrEmpty(duz_ucak.Text))
            {
                dbHelper.TumunuSil("ucaklar", "ucak_model", duz_ucak.Text);
            }
            MessageBox.Show("Başarıyla silindi");
            Listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int uzaklik, ucuz, orta, pahali;
            if (int.TryParse(mesafe.Text, out uzaklik) && int.TryParse(ekonomi.Text, out ucuz) &&
                int.TryParse(bussines.Text, out orta) && int.TryParse(firstclass.Text, out pahali))
            {
                dbHelper.Rotakyt(ucak_ad.Text, ucak_model.Text, merkez.Text, ulke.Text, inis.Text, rota_ad.Text, uzaklik, ucuz, orta, pahali);
                MessageBox.Show("Kayıt işlemi başarılı");
                Listele();
            }
            else
            {
                MessageBox.Show("Lütfen geçerli sayısal değerler girin");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dbHelper.TumunuSil("rota", "ucak_ad", ucak_ad.Text);
            MessageBox.Show("Başarıyla silindi");
            Listele();
        }

        private void filtucak_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filtucak.SelectedItem.ToString()))
            {
                DataTable doluTablo = dbHelper.TumunuFiltrele("ucak_model", filtucak.SelectedItem.ToString());
                dataGridView1.DataSource = doluTablo;
            }
        }

        private void filtrmerkez_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filtrmerkez.SelectedItem.ToString()))
            {
                DataTable doluTablo = dbHelper.TumunuFiltrele("merkez", filtrmerkez.SelectedItem.ToString());
                dataGridView1.DataSource = doluTablo;
            }
        }

        private void arama_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(arama.Text) && arama.SelectedItem != null)
            {
                DataTable doluTablo = dbHelper.TumunuAra(arama.SelectedItem.ToString(), arama.Text);
                dataGridView1.DataSource = doluTablo;
            }
            else
            {
                Listele();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (double.TryParse(ekonomi.Text, out double number))
            {
                double result = number * 1.1;
                ekonomi.Text = ((int)Math.Floor(result)).ToString();
            }
            if (double.TryParse(bussines.Text, out double number2))
            {
                double result = number2 * 1.08;
                bussines.Text = ((int)Math.Floor(result)).ToString();
            }
            if (double.TryParse(firstclass.Text, out double number3))
            {
                double result = number3 * 1.06;
                firstclass.Text = ((int)Math.Floor(result)).ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ucak_ad.Text = "";
            ucak_model.Text = "";
            merkez.Text = "";
            ulke.Text = "";
            inis.Text = "";
            rota_ad.Text = "";
            mesafe.Text = "";
            ekonomi.Text = "";
            bussines.Text = "";
            firstclass.Text = "";
            filtrmerkez.Text = "";
            filtucak.Text = "";
            duz_merkez.Text = "";
            duz_ucak.Text = "";
            filtrmerkez.Text = "";
            filtucak.Text = "";
            textBox1.Text = "";
            arama.Text = "";
            Listele();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (arama.SelectedItem != null)
            {
                if (arama.SelectedItem.ToString() == "Uçak Adı")
                {
                    DataTable doluTablo = dbHelper.TumunuAra("ucak_ad", textBox1.Text);
                    dataGridView1.DataSource = doluTablo;
                }
                else if (arama.SelectedItem.ToString() == "Uçak Modeli")
                {
                    DataTable doluTablo = dbHelper.TumunuAra("ucak_model", textBox1.Text);
                    dataGridView1.DataSource = doluTablo;
                }
                else if (arama.SelectedItem.ToString() == "İniş Ülke")
                {
                    DataTable doluTablo = dbHelper.TumunuAra("ulke", textBox1.Text);
                    dataGridView1.DataSource = doluTablo;
                }
                else if (arama.SelectedItem.ToString() == "İniş Havaalanı")
                {
                    DataTable doluTablo = dbHelper.TumunuAra("havaalanı", textBox1.Text);
                    dataGridView1.DataSource = doluTablo;
                }
                else if (arama.SelectedItem.ToString() == "Rota Adı")
                {
                    DataTable doluTablo = dbHelper.TumunuAra("rota_ad", textBox1.Text);
                    dataGridView1.DataSource = doluTablo;
                }
            }
        }
    }
}
