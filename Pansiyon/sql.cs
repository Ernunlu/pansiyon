using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using System.Windows;

namespace Pansiyon
{
	internal class sql
	{
		string? cnnstr;
		private void cnnstr_getir()
		{
		
			string ExeDosyaYolu = AppDomain.CurrentDomain.BaseDirectory.ToString();
			StreamReader oku = new StreamReader(ExeDosyaYolu + "\\sqlconnectionstring.txt");
			string? satir = oku.ReadLine();
			while (satir != null)
			{
				cnnstr = satir;
				satir = oku.ReadLine();
			}
		}
		public ArrayList dolu_odaları_getir(string giris_tarihi,string cikis_tarihi)
		{
			ArrayList odalar = new ArrayList();
			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("SELECT DISTINCT oda_no FROM Musteri WHERE giris_t between '" + giris_tarihi + "' and '" + cikis_tarihi + "' AND cikis_t between '" + giris_tarihi + "' and '" + cikis_tarihi + "'", connection);

			sorgu.ExecuteNonQuery();

			DataTable dt = new DataTable();
			SqlDataAdapter adp = new SqlDataAdapter(sorgu);
			adp.Fill(dt);

			foreach (DataRow dr in dt.Rows)
			{
				odalar.Add(dr["oda_no"].ToString());
			}
			connection.Close();

			return odalar;
		}
		public ArrayList odaları_getir()
		{
			ArrayList odalar = new ArrayList();
			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("SELECT oda_adi FROM odalar", connection);

			sorgu.ExecuteNonQuery();

			DataTable dt = new DataTable();
			SqlDataAdapter adp = new SqlDataAdapter(sorgu);
			adp.Fill(dt);

			foreach (DataRow dr in dt.Rows)
			{
				odalar.Add(dr["oda_adi"].ToString());
			}
			connection.Close();

			return odalar;
		}
		public string[] oda_bilgileri_getir(string oda)
		{
			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("SELECT oda_adi,k_kisi_sayi,gun_ucret,aciklama FROM odalar WHERE oda_adi='" + oda + "'", connection);
			SqlDataReader dr = sorgu.ExecuteReader();
			string[] oda_bilgileri = new string[4];
			if (dr.Read())
			{
				oda_bilgileri[0] = dr["oda_adi"].ToString();
				oda_bilgileri[1] = dr["k_kisi_sayi"].ToString();
				oda_bilgileri[2] = dr["gun_ucret"].ToString();
				oda_bilgileri[3] = dr["aciklama"].ToString();
			}
			connection.Close();
			return oda_bilgileri;
		}
		public string[] musteri_bilgileri_getir(string id)
		{
			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("SELECT Tc_kimlik,adi,soyadi,cep,giris_t,cikis_t,adres,aciklama FROM Musteri WHERE id='" + id + "'", connection);
			SqlDataReader dr = sorgu.ExecuteReader();
			string[] oda_bilgileri = new string[8];
			if (dr.Read())
			{
				oda_bilgileri[0] = dr["Tc_kimlik"].ToString();
				oda_bilgileri[1] = dr["adi"].ToString();
				oda_bilgileri[2] = dr["soyadi"].ToString();
				oda_bilgileri[3] = dr["cep"].ToString();
				oda_bilgileri[4] = dr["giris_t"].ToString();
				oda_bilgileri[5] = dr["cikis_t"].ToString();
				oda_bilgileri[6] = dr["adres"].ToString();
				oda_bilgileri[7] = dr["aciklama"].ToString();
			}
			connection.Close();
			return oda_bilgileri;
		}
		public string[] musteri_bilgileri_getir_hepsi(string id)
		{
			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("SELECT Tc_kimlik,adi,soyadi,cep,giris_t,cikis_t,adres,aciklama,oda_no,servisler FROM Musteri WHERE id='" + id + "'", connection);
			SqlDataReader dr = sorgu.ExecuteReader();
			string[] oda_bilgileri = new string[10];
			if (dr.Read())
			{
				oda_bilgileri[0] = dr["Tc_kimlik"].ToString();
				oda_bilgileri[1] = dr["adi"].ToString();
				oda_bilgileri[2] = dr["soyadi"].ToString();
				oda_bilgileri[3] = dr["cep"].ToString();
				oda_bilgileri[4] = dr["giris_t"].ToString();
				oda_bilgileri[5] = dr["cikis_t"].ToString();
				oda_bilgileri[6] = dr["adres"].ToString();
				oda_bilgileri[7] = dr["aciklama"].ToString();
				oda_bilgileri[8] = dr["oda_no"].ToString();
				oda_bilgileri[9] = dr["servisler"].ToString();
			}
			connection.Close();
			return oda_bilgileri;
		}
		public string[] musteri_ad_soyad_getir(string id)
		{
			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("SELECT adi,soyadi FROM Musteri WHERE id='" + id + "'", connection);
			SqlDataReader dr = sorgu.ExecuteReader();
			string[] oda_bilgileri = new string[8];
			if (dr.Read())
			{
				oda_bilgileri[0] = dr["adi"].ToString();
				oda_bilgileri[1] = dr["soyadi"].ToString();
			}
			connection.Close();
			return oda_bilgileri;
		}
		public string[] servis_adi_getir(string id)
		{
			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("SELECT Servis_adi,Servis_fiyati FROM Servisler WHERE id='" + id + "'", connection);
			SqlDataReader dr = sorgu.ExecuteReader();
			string[] servis_adı_fiyati = new string[2];
			if (dr.Read())
			{
				servis_adı_fiyati[0] = dr["Servis_adi"].ToString();
				servis_adı_fiyati[1] = dr["Servis_fiyati"].ToString();
			}
			connection.Close();
			return servis_adı_fiyati;
		}
		public ArrayList musteri_hizmetleri_aktif_odalar()
		{
			string tarih = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			ArrayList odalar = new ArrayList();
			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("SELECT DISTINCT odalar.oda_adi, odalar.id, Musteri.id FROM Musteri INNER JOIN odalar ON odalar.oda_adi = Musteri.oda_no WHERE Musteri.giris_t <= '" + tarih + "' AND Musteri.cikis_t >= '" + tarih + "'", connection);

			sorgu.ExecuteNonQuery();

			DataTable dt = new DataTable();
			SqlDataAdapter adp = new SqlDataAdapter(sorgu);
			adp.Fill(dt);

			foreach (DataRow dr in dt.Rows)
			{
				odalar.Add(dr[0].ToString() + "#" + dr[1].ToString() + "#" + dr[2].ToString());
			}
			connection.Close();

			return odalar;
		}
		public ArrayList servisleri_getir()
		{
	
			ArrayList odalar = new ArrayList();
			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("SELECT id,Servis_adi,Servis_fiyati,Servis_aciklamasi FROM Servisler", connection);

			sorgu.ExecuteNonQuery();

			DataTable dt = new DataTable();
			SqlDataAdapter adp = new SqlDataAdapter(sorgu);
			adp.Fill(dt);

			foreach (DataRow dr in dt.Rows)
			{
				odalar.Add(dr[0].ToString() + "#" + dr[1].ToString() + "#" + dr[2].ToString() + "#" + dr[3].ToString());
			}
			connection.Close();

			return odalar;
		}
		
		public string[] sercili_oda_getir(string id)
		{
			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			string[] oda_bilgileri = new string[4];
			SqlCommand sorgu = new SqlCommand("SELECT oda_adi,k_kisi_sayi,gun_ucret,aciklama FROM odalar WHERE id = '" + id + "'", connection);
			string servisler = "";
			SqlDataReader dr = sorgu.ExecuteReader();

			if (dr.Read())
			{
				oda_bilgileri[0] = dr["oda_adi"].ToString();
				oda_bilgileri[1] = dr["k_kisi_sayi"].ToString();
				oda_bilgileri[2] = dr["gun_ucret"].ToString();
				oda_bilgileri[3] = dr["aciklama"].ToString();

			}
			connection.Close();
			return oda_bilgileri;
		}
		public ArrayList oda_ayarlar_getir()
		{

			ArrayList odalar = new ArrayList();
			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("SELECT id,oda_adi,k_kisi_sayi,gun_ucret FROM odalar", connection);

			sorgu.ExecuteNonQuery();

			DataTable dt = new DataTable();
			SqlDataAdapter adp = new SqlDataAdapter(sorgu);
			adp.Fill(dt);

			foreach (DataRow dr in dt.Rows)
			{
				odalar.Add(dr[0].ToString() + "#" + dr[1].ToString() + "#" + dr[2].ToString() + "#" + dr[3].ToString());
			}
			connection.Close();

			return odalar;
		}
		public string[] sercili_servis_getir(string id)
		{

			string[] servisler = new string[3];
			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("SELECT Servis_adi,Servis_fiyati,Servis_aciklamasi FROM Servisler WHERE id='" + id + "'", connection);

			sorgu.ExecuteNonQuery();

			DataTable dt = new DataTable();
			SqlDataAdapter adp = new SqlDataAdapter(sorgu);
			adp.Fill(dt);

			foreach (DataRow dr in dt.Rows)
			{
				servisler[0] = dr[0].ToString();
				servisler[1] = dr[1].ToString();
				servisler[2] =  dr[2].ToString();
			}
			connection.Close();

			return servisler;
		}
		public ArrayList cikis_yapabilecek_odalari_getir()
		{

			string tarih = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			ArrayList odalar = new ArrayList();
			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("SELECT DISTINCT odalar.oda_adi, odalar.id, Musteri.id, Musteri.giris_t, Musteri.cikis_t FROM Musteri INNER JOIN odalar ON odalar.oda_adi = Musteri.oda_no WHERE Musteri.giris_t <= '" + tarih + "'", connection);

			sorgu.ExecuteNonQuery();

			DataTable dt = new DataTable();
			SqlDataAdapter adp = new SqlDataAdapter(sorgu);
			adp.Fill(dt);

			foreach (DataRow dr in dt.Rows)
			{
				odalar.Add(dr[0].ToString() + "#" + dr[1].ToString() + "#" + dr[2].ToString() + "#" + dr[3].ToString() + "#" + dr[4].ToString());
			}
			connection.Close();

			return odalar;
		}
		public void oda_ekle(string adi, int k_sayisi, float gun_ucret, string aciklama)    //oda ekleme
		{

			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			string sql = "insert into odalar (oda_adi,k_kisi_sayi,gun_ucret,aciklama)values('" + adi + "','" + k_sayisi + "','" + gun_ucret + "','" + aciklama + "')";
			SqlCommand komut = new SqlCommand(sql, connection);
			komut.ExecuteNonQuery();
			connection.Close();
		}

		public void sql_ekle(string tablo_adi, string[] veriler) //sql ekleme kodu
		{
			string sql = "insert into " + tablo_adi + " (";
			string[] sql_isim = sql_tablo_column(tablo_adi);
			for (int i = 1; i < sql_isim.Length; i++)
			{
				if (i == 1)
					sql = sql + sql_isim[i];
				else
					sql = sql + "," + sql_isim[i];
			}
			sql = sql + ")values('";
			for (int i = 0; i < veriler.Length; i++)
			{
				if (i == 0)
					sql = sql + veriler[i];
				else if (i == veriler.Length - 1)
					sql = sql + "','" + veriler[i] + "')";
				else
					sql = sql + "','" + veriler[i];
			}
			string ExeDosyaYolu = AppDomain.CurrentDomain.BaseDirectory.ToString();
			StreamReader oku = new StreamReader(ExeDosyaYolu + "\\sqlconnectionstring.txt");
			string? satir = oku.ReadLine();
			while (satir != null)
			{
				cnnstr = satir;
				satir = oku.ReadLine();
			}

			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}

			SqlCommand komut = new SqlCommand(sql, connection);
			komut.ExecuteNonQuery();
			connection.Close();

		}
		public string[] sql_tablo_column(string tablo_adi)      //tablo başlıkları
		{
			cnnstr_getir();

			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("use oteloto SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME ='" + tablo_adi + "' ", connection);

			sorgu.ExecuteNonQuery();

			DataTable dt = new DataTable();
			SqlDataAdapter adp = new SqlDataAdapter(sorgu);
			adp.Fill(dt);
			Stack tablo_isim = new Stack();

			foreach (DataRow dr in dt.Rows)
			{
				tablo_isim.Push(dr["COLUMN_NAME"].ToString());
			}
			connection.Close();

			string?[] dizi = new string[tablo_isim.Count];
			int x = tablo_isim.Count;
			for (int i = x - 1; i >= 0; i--)
			{
				dizi[i] = (string?)tablo_isim.Pop();
			}
			return dizi;
		}

		public void oda_durumu(string oda_adi, string durum) // oda durumunu güncelleme
		{
			cnnstr_getir();

			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}

			string yenileme = "update odalar set durumu='" + durum + "' where oda_adi='" + oda_adi + "'";
			SqlCommand komutt = new SqlCommand(yenileme, connection);
			komutt.ExecuteNonQuery();
			connection.Close();
		}
		
				public void musteri_guncelle(string[] veriler) // oda durumunu güncelleme
		{
			cnnstr_getir();

			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}

			string yenileme = "update Musteri set Tc_kimlik='" + veriler[0] + "', adi='" + veriler[1] + "', soyadi='" + veriler[2] + "', cep='" + veriler[3] + "', giris_t='" + veriler[4] + "', cikis_t='" + veriler[5] + "', adres='" + veriler[6] + "', aciklama='" + veriler[7] + "' where id='" + veriler[8] + "'";
			SqlCommand komutt = new SqlCommand(yenileme, connection);
			komutt.ExecuteNonQuery();
			connection.Close();
		}
		public void servis_ekleme(string servis, string id) // oda durumunu güncelleme
		{
			cnnstr_getir();

			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}

			string yenileme = "update Musteri set servisler='" + servis + "' where id='" + id + "'";
			SqlCommand komutt = new SqlCommand(yenileme, connection);
			komutt.ExecuteNonQuery();
			connection.Close();
		}
		public void oda_guncelleme(string id,string oda_adi, int k_sayi, float gun_ucret, string aciklama) // oda durumunu güncelleme
		{
			cnnstr_getir();

			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}

			string yenileme = "update odalar set oda_adi='"+ oda_adi + "',k_kisi_sayi='" + k_sayi + "',gun_ucret='" + gun_ucret + "',aciklama='" + aciklama + "' where id='" + id + "'";
			SqlCommand komutt = new SqlCommand(yenileme, connection);
			komutt.ExecuteNonQuery();
			connection.Close();
		}


		public void sql_silme(string nereden, string neye_gore, string hangisi)  //bir koşula göre silme
		{
			cnnstr_getir();

			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}

			string yenileme = "Delete from " + nereden + " where " + neye_gore + "='" + hangisi + "'";
			SqlCommand komutt = new SqlCommand(yenileme, connection);
			komutt.ExecuteNonQuery();
			connection.Close();
		}

		public float sql_oda_ucret(string oda_no)
		{
			try
			{
				SqlConnection connection = new SqlConnection(cnnstr);
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}
				SqlCommand sorgu = new SqlCommand("SELECT gun_ucret FROM odalar WHERE oda_adi='" + oda_no + "'", connection);

				SqlDataReader dr = sorgu.ExecuteReader();
				float ucret = 0;
				if (dr.Read())
				{
					ucret = float.Parse(dr["gun_ucret"].ToString());
				}
				connection.Close();
				return ucret;
			}
			catch
			{
				return 0;
			}

		}

		public DataSet ekran_doldur(string hangi_tablo)
		{
			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("SELECT * FROM " + hangi_tablo + "", connection);
			sorgu.ExecuteNonQuery();
			DataSet dataSet = new DataSet();
			SqlDataAdapter adp = new SqlDataAdapter(sorgu);
			adp.Fill(dataSet, hangi_tablo);
			return dataSet;
		}

		public DataSet servis_yardimci(string hangi_tablo, string hangisi)
		{
			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("SELECT * FROM " + hangi_tablo + " WHERE oda_no='" + hangisi + "'", connection);
			sorgu.ExecuteNonQuery();
			DataSet dataSet = new DataSet();
			SqlDataAdapter adp = new SqlDataAdapter(sorgu);
			adp.Fill(dataSet, hangi_tablo);
			return dataSet;
		}

		public void servis_guncelle(string id,string servis_adi, float servis_ucret, string aciklama)
		{
			cnnstr_getir();

			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}

			string yenileme = "update odalar set Servis_adi='"+ servis_adi + "',Servis_fiyati='" + servis_ucret + "',Servis_aciklamasi='" + aciklama + "' where id='" + id + "'";
			SqlCommand komutt = new SqlCommand(yenileme, connection);
			komutt.ExecuteNonQuery();
			connection.Close();
		}
		public string musteri_hizmetleri_gonder(string id)
		{
			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("SELECT servisler FROM Musteri WHERE id = '" + id + "'", connection);
			string servisler = "";
			SqlDataReader dr = sorgu.ExecuteReader();

			if (dr.Read())
			{
				servisler = dr["servisler"].ToString();
			}
			connection.Close();
			return servisler;
		}
		public string fiyat_gonder(string hangi_servis)
		{
			cnnstr_getir();
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("SELECT Servis_fiyati,Servis_adi FROM Servisler WHERE id = '" + hangi_servis + "'", connection);
			string yazilabilirmiktar = "";
			SqlDataReader dr = sorgu.ExecuteReader();

			if (dr.Read())
			{
				yazilabilirmiktar = dr["Servis_adi"].ToString() + "_" + dr["Servis_fiyati"].ToString();
			}
			connection.Close();
			return yazilabilirmiktar;
		}
		public string sql_gonder()
		{
			string ExeDosyaYolu = AppDomain.CurrentDomain.BaseDirectory.ToString();
			StreamReader oku = new StreamReader(ExeDosyaYolu + "\\sqlconnectionstring.txt");
			string? satir = oku.ReadLine();
			while (satir != null)
			{
				cnnstr = satir;
				satir = oku.ReadLine();
			}
			return cnnstr;
		}
		public float sql_ay_gelir(string ay)
		{
			try
			{
				cnnstr_getir();
				SqlConnection connection = new SqlConnection(cnnstr);
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}
				SqlCommand sorgu = new SqlCommand("SELECT SUM(ucret)AS 'gelir' FROM gelir_gider WHERE ay='" + ay + "' ", connection);
				float yazilabilirmiktar = 0;
				SqlDataReader dr = sorgu.ExecuteReader();

				if (dr.Read())
				{
					if (dr[0] != null && dr[0].ToString() != "NULL")
					{
						yazilabilirmiktar = float.Parse(dr[0].ToString());
					}
					else
						yazilabilirmiktar = 0;
				}
				connection.Close();
				return yazilabilirmiktar;
			}
			catch
			{
				return 0;
			}

		}
		public float sql_ay_gider(string ay)
		{
			try
			{
				cnnstr_getir();
				SqlConnection connection = new SqlConnection(cnnstr);
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}
				SqlCommand sorgu = new SqlCommand("SELECT SUM(ucret)AS 'gider' FROM gider WHERE ay='" + ay + "'", connection);
				float yazilabilirmiktar = 0;
				SqlDataReader dr = sorgu.ExecuteReader();

				if (dr.Read())
				{
					yazilabilirmiktar = float.Parse(dr[0].ToString());
				}
				connection.Close();
				return yazilabilirmiktar;
			}
			catch
			{
				return 0;
			}

		}
		public float sql_ay_ortalama(string ay)
		{
			try
			{
				cnnstr_getir();
				SqlConnection connection = new SqlConnection(cnnstr);
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}
				SqlCommand sorgu = new SqlCommand("SELECT AVG(ucret)AS 'ortalama' FROM gider WHERE ay='" + ay + "'", connection);
				float yazilabilirmiktar = 0;
				SqlDataReader dr = sorgu.ExecuteReader();

				if (dr.Read())
				{
					yazilabilirmiktar = float.Parse(dr[0].ToString());
				}
				connection.Close();
				return yazilabilirmiktar;
			}
			catch
			{
				return 0;
			}

		}
	}
}
