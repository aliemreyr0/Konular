using Microsoft.Data.SqlClient;
using System.Data;
namespace BenimsiteMvc.Models
{
    public class OgrenciDbIsle
    {
        //Öğrenciler isimli tablodaki CRUD işlemlerini yapan metotları içerir.
        private readonly string _baglanticumlesi;

        public OgrenciDbIsle()
        {
            //Oluşturucu metot
            //appsettings.json'dan bağlantıyı al
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();
            _baglanticumlesi = config.GetConnectionString("Baglanti1");
        }

        //Yeni Kayıt Ekleme
        public bool Kayitekle(OgrenciModel liste1)
        {
            using (SqlConnection baglanti = new SqlConnection(_baglanticumlesi))
            {
                baglanti.Open();
                string sql = "Insert Into Ogrenciler (Ogrencino,Adsoyad,Bolum) Values (@Ogrencino,@Adsoyad,@Bolum);";
                SqlCommand komut = new SqlCommand(sql, baglanti);
                komut.Parameters.AddWithValue("@Ogrencino", liste1.Ogrencino);
                komut.Parameters.AddWithValue("@Adsoyad", liste1.Adsoyad);
                komut.Parameters.AddWithValue("@Bolum", liste1.Bolum);
                return komut.ExecuteNonQuery() >= 1;
            }            
        }

        //Öğrencileri listeleme
        public List<OgrenciModel> Ogrencilerigetir()
        {
            using (SqlConnection baglanti =  new SqlConnection(_baglanticumlesi))
            {
                baglanti.Open();
                //Veritabanı (veriler1) yoksa oluştur
                string sqlvtolustur = @"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'veriler1')
                BEGIN CREATE DATABASE veriler1; END";
                new SqlCommand(sqlvtolustur, baglanti).ExecuteNonQuery();

                // Tablo yoksa oluştur
                string sqltabloolustur = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Ogrenciler' AND xtype='U') " +
                    "CREATE TABLE Ogrenciler (" +
                    "Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, " +
                    "Ogrencino NVARCHAR(9) COLLATE Latin1_General_100_CI_AS_SC_UTF8 NULL, " +
                    "Adsoyad NVARCHAR(25) COLLATE Latin1_General_100_CI_AS_SC_UTF8 NULL, " +
                    "Bolum NVARCHAR(50) COLLATE Latin1_General_100_CI_AS_SC_UTF8 NULL," +
                    "Kayittarihi DATETIME NOT NULL DEFAULT GETDATE());";
                new SqlCommand(sqltabloolustur, baglanti).ExecuteNonQuery();
                //Listeleme
                List<OgrenciModel> liste1 = new List<OgrenciModel>();
                string sql = "Select * from Ogrenciler Order By Ogrencino";
                SqlDataAdapter adaptor = new SqlDataAdapter(sql, baglanti);
                DataTable tablo = new DataTable();
                adaptor.Fill(tablo);
                //Döngü yapalım
                foreach (DataRow kayit in tablo.Rows)
                {
                    liste1.Add(new OgrenciModel
                    {
                        Id = Convert.ToInt32(kayit["Id"]),
                        Ogrencino = Convert.ToString(kayit["Ogrencino"]),
                        Adsoyad = Convert.ToString(kayit["Adsoyad"]),
                        Bolum = Convert.ToString(kayit["Bolum"])
                    });
                }//foreach
                return liste1;//Liste1'i döndürelim
            }
        }
    }
}
