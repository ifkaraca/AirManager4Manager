using System.Data;
using System.Data.OleDb;

namespace airm4
{
    internal class DbHelper
    {
        OleDbConnection baglanti = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=manager.mdb");

        public void Ekle(string tablo_ad, string alan, string deger)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand($@"INSERT INTO {tablo_ad}({alan}) VALUES('{deger}')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        public void TumunuSil(string tablo_ad, string sart, string deger)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand($"DELETE FROM {tablo_ad} WHERE {sart}='{deger}'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        public void Rotakyt(string ucak_ad, string ucak_model, string merkez, string ulke, string havaalani, string rota_ad, int mesafe, int ekonomi, int bussines, int firstclass)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand($@"INSERT INTO rota(ucak_ad, ucak_model, merkez, ulke, havaalanı, rota_ad, mesafe, ekonomi, bussines, firstclass) 
                                                    VALUES('{ucak_ad}', '{ucak_model}', '{merkez}', '{ulke}', '{havaalani}', '{rota_ad}', {mesafe}, {ekonomi}, {bussines}, {firstclass})", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        public DataTable TumunuFiltrele(string deger, string prmtr)
        {
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter($@"SELECT * FROM rota WHERE {deger} = '{prmtr}'", baglanti);
            DataTable tablo = new DataTable();
            adtr.Fill(tablo);
            baglanti.Close();
            return tablo;
        }

        public DataTable TumunuAra(string secilen, string ad)
        {
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter($@"SELECT * FROM rota WHERE {secilen} LIKE '%{ad}%'", baglanti);
            DataTable tablo = new DataTable();
            adtr.Fill(tablo);
            baglanti.Close();
            return tablo;
        }

        public DataTable ListAllCalistir(string tablo_ad)
        {
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter($"SELECT * FROM {tablo_ad}", baglanti);
            DataTable tablo = new DataTable();
            adtr.Fill(tablo);
            baglanti.Close();
            return tablo;
        }
    }
}
