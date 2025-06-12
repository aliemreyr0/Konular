using Microsoft.Data.SqlClient;
using System.Data;

namespace BenimsiteMvc.Models
{
    public class UrunDbIsle
    {
        private readonly string _baglantiCumlesi;

        public UrunDbIsle()
        {
            // appsettings.json'dan bağlantı cümlesini al
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();
            _baglantiCumlesi = config.GetConnectionString("Baglanti1");//Bağlantı cümlesinin adı doğru olmalıdır.
        }

        // Yeni kayıt ekleme
        public bool Kayitekle(UrunlerModel liste1)
        {
            using (SqlConnection baglanti = new SqlConnection(_baglantiCumlesi))
            {
                baglanti.Open();
                string sql = "INSERT INTO urunler (Urunadi, Kategori, Fiyat) VALUES (@Urunadi, @Kategori, @Fiyat)";
                SqlCommand komut = new SqlCommand(sql, baglanti);
                komut.Parameters.AddWithValue("@Urunadi", liste1.Urunadi);
                komut.Parameters.AddWithValue("@Kategori", liste1.Kategori);
                komut.Parameters.AddWithValue("@Fiyat", liste1.Fiyat);
                return komut.ExecuteNonQuery() >= 1;
            }
        }

        // Ürünleri listeleme
        public List<UrunlerModel> Urunlerigetir()
        {
            using (SqlConnection baglanti = new SqlConnection(_baglantiCumlesi))
            {
                baglanti.Open();

                //Veritabanı (veriler1) yoksa oluştur
                string sqlvtolustur = @"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'veriler1')
                BEGIN CREATE DATABASE veriler1; END";
                new SqlCommand(sqlvtolustur, baglanti).ExecuteNonQuery();

                // Tablo yoksa oluştur
                string sqltabloolustur = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Urunler' AND xtype='U') " +
                    "CREATE TABLE Urunler (" +
                    "Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, " +
                    "Urunadi NVARCHAR(100) COLLATE Latin1_General_100_CI_AS_SC_UTF8 NULL, " +
                    "Kategori NVARCHAR(50) COLLATE Latin1_General_100_CI_AS_SC_UTF8 NULL, " +
                    "Fiyat DECIMAL(18,2) NULL);";
                new SqlCommand(sqltabloolustur, baglanti).ExecuteNonQuery();

                // Listeleme
                List<UrunlerModel> liste = new List<UrunlerModel>();
                string sql = "SELECT * FROM urunler ORDER BY Urunadi";
                SqlDataAdapter adaptor = new SqlDataAdapter(sql, baglanti);
                DataTable tablo = new DataTable();
                adaptor.Fill(tablo);
                //Kayıtları dizi listesine ekle
                foreach (DataRow kayit in tablo.Rows)
                {
                    liste.Add(new UrunlerModel
                    {
                        Id = Convert.ToInt32(kayit["Id"]),
                        Urunadi = Convert.ToString(kayit["Urunadi"]),
                        Kategori = Convert.ToString(kayit["Kategori"]),
                        Fiyat = Convert.ToDecimal(kayit["Fiyat"])
                    });
                }
                return liste;
            }
        }

        // Ürün bilgilerini değiştirme
        public bool Urunbilgidegis(UrunlerModel liste1)
        {
            using (SqlConnection baglanti = new SqlConnection(_baglantiCumlesi))
            {
                baglanti.Open();
                string sql = "UPDATE urunler SET Urunadi=@Urunadi, Kategori=@Kategori, Fiyat=@Fiyat WHERE Id=@Id";
                SqlCommand komut = new SqlCommand(sql, baglanti);
                komut.Parameters.AddWithValue("@Urunadi", liste1.Urunadi);
                komut.Parameters.AddWithValue("@Kategori", liste1.Kategori);
                komut.Parameters.AddWithValue("@Fiyat", liste1.Fiyat);
                komut.Parameters.AddWithValue("@Id", liste1.Id);
                return komut.ExecuteNonQuery() >= 1;
            }
        }

        // Ürün silme
        public bool Urunsil(int Id)
        {
            using (SqlConnection baglanti = new SqlConnection(_baglantiCumlesi))
            {
                baglanti.Open();
                string sql = "DELETE FROM urunler WHERE Id=@Id";
                SqlCommand komut = new SqlCommand(sql, baglanti);
                komut.Parameters.AddWithValue("@Id", Id);
                return komut.ExecuteNonQuery() >= 1;
            }
        }
    }
}
