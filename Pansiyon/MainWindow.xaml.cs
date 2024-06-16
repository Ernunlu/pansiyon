using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Threading;

namespace Pansiyon
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
		sql sqls = new sql();
		string cnnstr = "";
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
		
			cnnstr = sqls.sql_gonder();
			ekran_doldur();
			ekran_doldur2();
			ekran_doldur3();
			System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
			dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
			dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
			dispatcherTimer.Start();

		}
		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				if (sabit_veriler.musteri_guncelle)
					ekran_doldur();
			}
			catch { }
		}
		private void DatePicker_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
		//iptal
		}
		private void ekran_doldur()
		{
			try
			{
				sql sqlm = new sql();
				DataSet data = new DataSet();
				data = sqlm.ekran_doldur("Musteri");
				m_ekrani.ItemsSource = data.Tables["Musteri"].DefaultView;
				m_ekrani.Columns[0].Header = "ID";
				m_ekrani.Columns[1].Header = "TC Kimlik Numarası";
				m_ekrani.Columns[2].Header = "Adı";
				m_ekrani.Columns[3].Header = "Soyadı";
				m_ekrani.Columns[4].Header = "Cep Telefonu";
				m_ekrani.Columns[5].Header = "Giriş Tarihi";
				m_ekrani.Columns[6].Header = "Çıkış Tarihi";
				m_ekrani.Columns[7].Header = "Adresi";
				m_ekrani.Columns[7].Width = 200;
				m_ekrani.Columns[8].Header = "Açıklama";
				m_ekrani.Columns[8].Width = 250;
				m_ekrani.Columns[9].Header = "Oda Numarası";
				m_ekrani.Columns[10].Header = "Kullanılan Servisler";
			}
			catch { }
		
	
			
		


		}
		private void ekran_doldur2()
		{
			try
			{
				sql sqlm = new sql();
				DataSet data = new DataSet();
				data = sqlm.ekran_doldur("musteri_arsiv");
				g_m_ekrani.ItemsSource = data.Tables["musteri_arsiv"].DefaultView;
				g_m_ekrani.Columns[0].Header = "ID";
				g_m_ekrani.Columns[1].Header = "TC Kimlik Numarası";
				g_m_ekrani.Columns[2].Header = "Adı";
				g_m_ekrani.Columns[3].Header = "Soyadı";
				g_m_ekrani.Columns[4].Header = "Cep Telefonu";
				g_m_ekrani.Columns[5].Header = "Giriş Tarihi";
				g_m_ekrani.Columns[6].Header = "Çıkış Tarihi";
				g_m_ekrani.Columns[7].Header = "Adresi";
				g_m_ekrani.Columns[7].Width = 200;
				g_m_ekrani.Columns[8].Header = "Açıklama";
				g_m_ekrani.Columns[8].Width = 250;
				g_m_ekrani.Columns[9].Header = "Oda Numarası";
				g_m_ekrani.Columns[10].Header = "Kullanılan Servisler";
			}
			catch { }






		}
		private void ekran_doldur3()
		{
			try
			{
				sql sqlm = new sql();
				DataSet data = new DataSet();
				data = sqlm.ekran_doldur("gelir_gider");
				ge_m_ekrani.ItemsSource = data.Tables["gelir_gider"].DefaultView;
				ge_m_ekrani.Columns[0].Header = "ID";
				ge_m_ekrani.Columns[1].Header = "Gün";
				ge_m_ekrani.Columns[2].Header = "Ay";
				ge_m_ekrani.Columns[3].Header = "Yıl";
				ge_m_ekrani.Columns[4].Header = "Gelir/Gider";
				ge_m_ekrani.Columns[5].Header = "Türü";
				ge_m_ekrani.Columns[6].Header = "Tekrarlıyormu";
				ge_m_ekrani.Columns[7].Header = "Miktar";

			}
			catch { }






		}
		public static void DoWork()
		{
			

		}
		private void k_giris_tarihi_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			//iptal
		}
		//Yeni Kayıt Sayfası ------------------------------------------------------------------------------------------------------

		private void k_giris_tarihi_CalendarClosed(object sender, RoutedEventArgs e)
		{
			if(k_cikis_tarihi.SelectedDate.HasValue && k_giris_tarihi.SelectedDate.HasValue && k_cikis_tarihi.SelectedDate != null && k_giris_tarihi != null)
			{
				if (k_giris_tarihi.SelectedDate <= k_cikis_tarihi.SelectedDate)
					odalari_getir();
				else
					MessageBox.Show("Giriş tarihi çıkış tarihinden büyük olamaz.", "Tarih hatası");
			}
		
		}
	
		private void k_cikis_tarihi_CalendarClosed(object sender, RoutedEventArgs e)
		{
			if(k_cikis_tarihi.SelectedDate.HasValue && k_giris_tarihi.SelectedDate.HasValue && k_cikis_tarihi.SelectedDate != null && k_giris_tarihi != null)
			{
				if (k_giris_tarihi.SelectedDate <= k_cikis_tarihi.SelectedDate)
					odalari_getir();
				else
					MessageBox.Show("Çıkış tarihi giriş tarihinden büyük olamaz.", "Tarih hatası");
			}
		
		}
		private void odalari_getir()
		{
			try
			{
				if (k_cikis_tarihi.SelectedDate.HasValue && k_giris_tarihi.SelectedDate.HasValue && k_cikis_tarihi.SelectedDate != null && k_giris_tarihi != null)
				{
					odalar.Children.Clear();
					string giris_tarihi = k_giris_tarihi.SelectedDate.Value.ToString("yyyy-MM-dd") + " 13:00:00 PM" ;
					string cikis_tarihi = k_cikis_tarihi.SelectedDate.Value.ToString("yyyy-MM-dd") + " 12:00:00 AM" ;
					ArrayList dolu_odalar = new ArrayList();
					dolu_odalar = sqls.dolu_odaları_getir(giris_tarihi, cikis_tarihi);
					ArrayList tum_odalar = new ArrayList();
					tum_odalar = sqls.odaları_getir();
					foreach (var item in dolu_odalar)
					{
						tum_odalar.Remove(item);
					}
					foreach (var item in tum_odalar)
					{
						Button btn = new Button();
						btn.Name = "_" + item.ToString();
						btn.Content = item.ToString();
						btn.Width = 90;
						btn.Height = 80;
						btn.Margin = new Thickness(15);
						btn.BorderBrush = Brushes.DarkSlateGray;
						btn.Background = Brushes.AliceBlue;
						btn.Click += Btn_Click;
						odalar.Children.Add(btn);
					}

				}
			}
			catch { }
		
		}
		public string? oda_h = "";
		private void Btn_Click(object sender, EventArgs e)
		{
			//Button btn = sender as Button;
			foreach (var item in odalar.Children.OfType<Button>())
			{
				item.Background = Brushes.AliceBlue;
			}
			Button btn = (Button)sender;
			btn.Background = Brushes.Green;
			
			oda_h = btn.Content.ToString();
		}

		private void kaydet_Click(object sender, RoutedEventArgs e)
		{
			string[] veriler = new string[10];
			veriler[0] = k_tc_no.Text;
			veriler[1] = k_adi.Text;
			veriler[2] = k_soyadi.Text;
			veriler[3] = k_cep_no.Text;
			veriler[4] = k_giris_tarihi.SelectedDate.Value.ToString("yyyy-MM-dd");
			veriler[4] = veriler[4] + " 13:00:00 PM";
			veriler[5] = k_cikis_tarihi.SelectedDate.Value.ToString("yyyy-MM-dd");
			veriler[5] = veriler[5] + " 12:00:00 AM";
			veriler[6] = k_adres.Text;
			veriler[7] = new TextRange(k_aciklama.Document.ContentStart, k_aciklama.Document.ContentEnd).Text;
			veriler[8] = oda_h;
			veriler[9] = "0";  //kayıtlı kullanılmış servis olmadığından sıfır yazılır

			sqls.sql_ekle("Musteri", veriler); //müşteriyi ekler

			sqls.oda_durumu(oda_h, "Dolu"); // oda durumunu değiştirir
			MessageBox.Show("TC Kimlik Numarası : " + k_tc_no.Text + "\n Adı : " + k_adi.Text + "\n Soyadi :" + k_soyadi.Text + "\n Cep Telefonu : " + k_cep_no.Text + "\n Giriş Tarihi : " + k_giris_tarihi.SelectedDate.Value.ToString("yyyy-MM-dd") + "\n Çıkış Tarihi : " + k_cikis_tarihi.SelectedDate.Value.ToString("yyyy-MM-dd") + "\n Adres : " + k_adres.Text + "\n Aciklama : " + new TextRange(k_aciklama.Document.ContentStart, k_aciklama.Document.ContentEnd).Text + "\n Oda Numarası : " + oda_h, "Başarılı");
			odalari_getir();
		}

		private void kaydet_temizle_Click(object sender, RoutedEventArgs e)
		{
			string[] veriler = new string[10];
			veriler[0] = k_tc_no.Text;
			veriler[1] = k_adi.Text;
			veriler[2] = k_soyadi.Text;
			veriler[3] = k_cep_no.Text;
			veriler[4] = k_giris_tarihi.SelectedDate.Value.ToString("yyyy-MM-dd") ;
			veriler[4] = veriler[4] + " 13:00:00 PM";
			veriler[5] = k_cikis_tarihi.SelectedDate.Value.ToString("yyyy-MM-dd") ;
			veriler[5] = veriler[5] + " 12:00:00 AM";
			veriler[6] = k_adres.Text;
			veriler[7] = new TextRange(k_aciklama.Document.ContentStart, k_aciklama.Document.ContentEnd).Text;
			veriler[8] = oda_h;
			veriler[9] = "0";  //kayıtlı kullanılmış servis olmadığından sıfır yazılır

			sqls.sql_ekle("Musteri", veriler); //müşteriyi ekler

			sqls.oda_durumu(oda_h, "Dolu"); // oda durumunu değiştirir
			MessageBox.Show("TC Kimlik Numarası : " + k_tc_no.Text + "\n Adı : " + k_adi.Text + "\n Soyadi :" + k_soyadi.Text + "\n Cep Telefonu : " + k_cep_no.Text + "\n Giriş Tarihi : " + k_giris_tarihi.SelectedDate.Value.ToString("yyyy-MM-dd") + "\n Çıkış Tarihi : " + k_cikis_tarihi.SelectedDate.Value.ToString("yyyy-MM-dd") + "\n Adres : " + k_adres.Text + "\n Aciklama : " + new TextRange(k_aciklama.Document.ContentStart, k_aciklama.Document.ContentEnd).Text + "\n Oda Numarası : " + oda_h, "Başarılı");
			odalari_getir();
			k_tc_no.Text = string.Empty;
			k_adi.Text = string.Empty;
			k_soyadi.Text = string.Empty;
			k_cep_no.Text = string.Empty;
			k_adres.Text = string.Empty;
			k_aciklama.Document.Blocks.Clear();


		}

		private void temizle_Click(object sender, RoutedEventArgs e)
		{
			odalari_getir();
			k_tc_no.Text = string.Empty;
			k_adi.Text = string.Empty;
			k_soyadi.Text = string.Empty;
			k_cep_no.Text = string.Empty;
			k_adres.Text = string.Empty;
			k_aciklama.Document.Blocks.Clear();
		}

	

		private void m_ekrani_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//var bilgiler = (musteri_bilgiler)m_ekrani.SelectedItem;
			//musteri_bilgiler bilgisa = new musteri_bilgiler();
			//bilgisa = bilgiler;
			try
			{
				DataRowView row = m_ekrani.SelectedItem as DataRowView;
				if(row != null)
				{
					musteri_duzenleme musteri_duzenlencek = new musteri_duzenleme(row.Row.ItemArray[0].ToString());
					musteri_duzenlencek.Show();
				}	
				
			}
		catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				if (tab_control.SelectedIndex == 1)
				{
					m_ekrani.Columns[0].Header = "ID";
					m_ekrani.Columns[1].Header = "TC Kimlik Numarası";
					m_ekrani.Columns[2].Header = "Adı";
					m_ekrani.Columns[3].Header = "Soyadı";
					m_ekrani.Columns[4].Header = "Cep Telefonu";
					m_ekrani.Columns[5].Header = "Giriş Tarihi";
					m_ekrani.Columns[6].Header = "Çıkış Tarihi";
					m_ekrani.Columns[7].Header = "Adresi";
					m_ekrani.Columns[7].Width = 200;
					m_ekrani.Columns[8].Header = "Açıklama";
					m_ekrani.Columns[8].Width = 250;
					m_ekrani.Columns[9].Header = "Oda Numarası";
					m_ekrani.Columns[10].Header = "Kullanılan Servisler";
				}
				else if(tab_control.SelectedIndex == 2)
				{
					odalari_Getir_hizmet();
				}
				else if(tab_control.SelectedIndex == 3)
				{
					musteri_cikis_ekrani_veri_getir();
				}else if(tab_control.SelectedIndex == 5)
				{
					servisleri_getir();
				}else if(tab_control.SelectedIndex == 6)
				{
					ayarlar_odalari_getir();
				}else if(tab_control.SelectedIndex==7)
				{
					ekran_doldur2();
				}
				else if (tab_control.SelectedIndex == 4)
				{
					ekran_doldur3();
				}
			}
			catch { }

		}

		private void TabItem_Loaded(object sender, RoutedEventArgs e)
		{
			
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ekran_doldur();
				m_ekrani.Columns[0].Header = "ID";
				m_ekrani.Columns[1].Header = "TC Kimlik Numarası";
				m_ekrani.Columns[2].Header = "Adı";
				m_ekrani.Columns[3].Header = "Soyadı";
				m_ekrani.Columns[4].Header = "Cep Telefonu";
				m_ekrani.Columns[5].Header = "Giriş Tarihi";
				m_ekrani.Columns[6].Header = "Çıkış Tarihi";
				m_ekrani.Columns[7].Header = "Adresi";
				m_ekrani.Columns[7].Width = 200;
				m_ekrani.Columns[8].Header = "Açıklama";
				m_ekrani.Columns[8].Width = 250;
				m_ekrani.Columns[9].Header = "Oda Numarası";
				m_ekrani.Columns[10].Header = "Kullanılan Servisler";
			}
			catch { }
			
			
		}
		

		private void TextBox_MouseEnter(object sender, MouseEventArgs e)
		{
			
		}

		private void arama_text_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			try
			{
				arama_text.Text = "";
			}catch { }	

		}

		private void arama_text_TextChanged(object sender, TextChangedEventArgs e)
		{
			try { 
			SqlConnection connection = new SqlConnection(cnnstr);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}
			SqlCommand sorgu = new SqlCommand("SELECT * FROM Musteri where adi LIKE '%" + arama_text.Text + "%'", connection);
			sorgu.ExecuteNonQuery();
			DataSet dataSet = new DataSet();
			SqlDataAdapter adp = new SqlDataAdapter(sorgu);
			adp.Fill(dataSet, "Musteri");
			m_ekrani.ItemsSource = dataSet.Tables["Musteri"].DefaultView;
			connection.Close();

			m_ekrani.Columns[0].Header = "ID";
			m_ekrani.Columns[1].Header = "TC Kimlik Numarası";
			m_ekrani.Columns[2].Header = "Adı";
			m_ekrani.Columns[3].Header = "Soyadı";
			m_ekrani.Columns[4].Header = "Cep Telefonu";
			m_ekrani.Columns[5].Header = "Giriş Tarihi";
			m_ekrani.Columns[6].Header = "Çıkış Tarihi";
			m_ekrani.Columns[7].Header = "Adresi";
			m_ekrani.Columns[7].Width = 200;
			m_ekrani.Columns[8].Header = "Açıklama";
				m_ekrani.Columns[8].Width = 250;
				m_ekrani.Columns[9].Header = "Oda Numarası";
			m_ekrani.Columns[10].Header = "Kullanılan Servisler";
		}
			catch { }
		}
		//hizmetler değşiştirme butonu
		private void hizmet_degistir_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				hizmetler musteri_duzenlencek = new hizmetler(Convert.ToInt32(musteri_hizmet_id)); // hizmetler_islem
				musteri_duzenlencek.Show();
			}
			catch { }
				
			
		}
		private void odalari_Getir_hizmet()
		{
			odalar_hizmet.Children.Clear();
			ArrayList dolu_odalar_hizmet = new ArrayList();
			dolu_odalar_hizmet = sqls.musteri_hizmetleri_aktif_odalar();
			
				foreach (var item in dolu_odalar_hizmet)
				{
					string[] parcala = item.ToString().Split('#');
					string id = parcala[0];
					string isim = parcala[1];
				string musteri_id = parcala[2];
				if(parcala.Count() > 3)
				{
					for (int i = 3; i < parcala.Count(); i++)
					{
						isim = isim + parcala[i];
					}
				}

					Button btn = new Button();
					btn.Name = "hizmetmusteriid_" + musteri_id;
					btn.Content = isim;
					btn.Width = 90;
					btn.Height = 80;
					btn.Margin = new Thickness(15);
					btn.BorderBrush = Brushes.DarkSlateGray;
					btn.Background = Brushes.AliceBlue;
					btn.Click += Btn_Click_hizmetler;
					odalar_hizmet.Children.Add(btn);
				}


			

		}
		string hizmet_secilen_oda = "";
		string musteri_hizmet_id = "";
		private void Btn_Click_hizmetler(object sender, EventArgs e)
		{
			//Button btn = sender as Button;
			foreach (var item in odalar_hizmet.Children.OfType<Button>())
			{
				item.Background = Brushes.AliceBlue;
			}
			Button btn = (Button)sender;
			btn.Background = Brushes.Green;

			string[] parcala =  btn.Name.Split('_');

			string[] veriler = sqls.musteri_bilgileri_getir(parcala[1]);

			string on_izleme_text_olusturma = "TC Kimlik Numarası : " + veriler[0] + "\n Adı : " + veriler[1] + "\n Soyadi :" + veriler[2] + "\n Cep Telefonu : " + veriler[3] + "\n Giriş Tarihi : " + veriler[4] + "\n Çıkış Tarihi : " + veriler[5] + "\n Adres : " + veriler[6] + "\n Aciklama : " + veriler[7] + "\n Oda Numarası : " + btn.Content + " \n Servisler :";


			musteri_hizmet_id = parcala[1];
			string serviler_musteri = sqls.musteri_hizmetleri_gonder(parcala[1]);
			string[] servisler_parcali = serviler_musteri.Split('+');
			if(servisler_parcali.Length != 0)
			{
				for (int i = 0; i < servisler_parcali.Length; i++)
				{
					if(servisler_parcali[i] != "" && servisler_parcali[i] != "0")
						on_izleme_text_olusturma = on_izleme_text_olusturma + servisler_parcali[i] + "\n";
				}
			}
			TextRange on_izleme_bilgileri = new TextRange(on_izle.Document.ContentStart, on_izle.Document.ContentEnd);
			on_izleme_bilgileri.Text = on_izleme_text_olusturma;
			musteri_aktif_hizmetleri_getir();




		}
		private void musteri_aktif_hizmetleri_getir()
		{
			aktif_hizmetler.Children.Clear();
			string musteri_servis = sqls.musteri_hizmetleri_gonder(musteri_hizmet_id);
			string[] muster_servisleri_aktif = musteri_servis.Split('+');
			foreach (var item in muster_servisleri_aktif)
			{
				if (item != "")
				{
					string[] servis_bilgileri_g = sqls.servis_adi_getir(item);

					Button btn = new Button();
					btn.Name = "musterihizmetler_" + item;
					btn.Content = servis_bilgileri_g[0] + "\n" + servis_bilgileri_g[1] + " TL";
					btn.Width = 90;
					btn.Height = 80;
					btn.Margin = new Thickness(15);
					btn.BorderBrush = Brushes.DarkSlateGray;
					btn.Background = Brushes.AliceBlue;
					aktif_hizmetler.Children.Add(btn);
				}
			}
		}
		//musteri cikis ekrani
		float odacikis_toplam_ucret = 0;
		string[] veriler = new string[10];
		string cikis_musteri_id = "";
		private void musteri_cikis_ekrani_veri_getir()
		{
			ArrayList cikis_olabilcek_odalar = new ArrayList();

			cikis_olabilcek_odalar = sqls.cikis_yapabilecek_odalari_getir();
		
			cikis_yapılabilcek_odalar.Children.Clear();
			foreach (var item in cikis_olabilcek_odalar)
			{
				if (item != "")
				{
					string[] parcala_cikis = item.ToString().Split('#');
					string[] ad_soyad = sqls.musteri_ad_soyad_getir(parcala_cikis[2]);
					Button btn = new Button();
					btn.Name = "cikissayfasi_" + parcala_cikis[2];
					btn.Content = parcala_cikis[0] + "\n" + ad_soyad[0] + " " + ad_soyad[1];
					btn.Width = 90;
					btn.Height = 80;
					btn.Click += cikis_butonlar_click;
					btn.Margin = new Thickness(15);
					btn.BorderBrush = Brushes.DarkSlateGray;
					btn.Background = Brushes.AliceBlue;
					try
					{
						if(DateTime.Now >= DateTime.Parse(parcala_cikis[3]))
							btn.Background = Brushes.Green;
						if(DateTime.Now >= DateTime.Parse(parcala_cikis[4]))
							btn.Background = Brushes.Red;
					}
					catch { }
					cikis_yapılabilcek_odalar.Children.Add(btn);
				}
			}

		}
		private void cikis_butonlar_click(object sender, EventArgs e)
		{
			try
			{

			foreach (var item in cikis_yapılabilcek_odalar.Children.OfType<Button>())
			{
				if(item.Background != Brushes.Red)
				item.Background = Brushes.AliceBlue;
			}
			Button btn = (Button)sender;
			btn.Background = Brushes.Green;

			string[] id_parcala = btn.Name.Split('_');
			string[] musteri_bilgileri_parca = sqls.musteri_bilgileri_getir_hepsi(id_parcala[1]);
				cikis_musteri_id = id_parcala[1];
				string on_izleme = "TC Kimlik Numarası : " + musteri_bilgileri_parca[0] + "\n Adı : " + musteri_bilgileri_parca[1] + "\n Soyadi :" + musteri_bilgileri_parca[2] + "\n Cep Telefonu : " + musteri_bilgileri_parca[3] + "\n Giriş Tarihi : " + musteri_bilgileri_parca[4] + "\n Çıkış Tarihi : " + musteri_bilgileri_parca[5] + "\n Adres : " + musteri_bilgileri_parca[6] + "\n Aciklama : " + musteri_bilgileri_parca[7] + "\n Oda Numarası : " + musteri_bilgileri_parca[8] + " \n Servisler :";
				string[] servislerm = musteri_bilgileri_parca[9].Split('+');
				float toplam_ucret = 0;
				if (servislerm.Length != 0)
				{
					for (int i = 0; i < servislerm.Length; i++)
					{

						if (servislerm[i] != "")
						{
							try
							{
								string ad_fiyat = sqls.fiyat_gonder(servislerm[i]);
								string[] ad_fiyat_parcala = ad_fiyat.Split('_');
								float price = float.Parse(ad_fiyat_parcala[1]);
  								toplam_ucret += price;
								on_izleme = on_izleme + "\n" + ad_fiyat_parcala[0] + " : " + price.ToString();
							}
							catch
							{

							}


						}
					}
					float oda_ucreti = sqls.sql_oda_ucret(musteri_bilgileri_parca[8]);
					TimeSpan gun_sayisi = DateTime.Parse(musteri_bilgileri_parca[5]) - DateTime.Parse(musteri_bilgileri_parca[4]);
					on_izleme = on_izleme + "\n" + "1 Günlük Oda Ücreti : " + oda_ucreti;
					oda_ucreti = oda_ucreti * gun_sayisi.Days;
					toplam_ucret = toplam_ucret + oda_ucreti;
					on_izleme = on_izleme + "\n" + "Toplam Ücret : " + toplam_ucret;
					TextRange cikis_onizleme_text = new TextRange(cikis_onizleme.Document.ContentStart, cikis_onizleme.Document.ContentEnd);
					cikis_onizleme_text.Text = on_izleme;
					odacikis_toplam_ucret = toplam_ucret;

					veriler = musteri_bilgileri_parca;
			

				}

			}
			catch { }

		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			musteri_cikis_ekrani_veri_getir();
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{// 4 5
			if (MessageBox.Show( veriler[1] + " " + veriler[2] + "isimli " + veriler [8] + " odasındaki müşterinin çıkış işlemi yapılsınmı ?", "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
			{
				veriler[4] =  DateTime.Parse(veriler[4]).ToString("yyyy-MM-dd HH:mm:ss");
				veriler[5] = DateTime.Parse(veriler[5]).ToString("yyyy-MM-dd HH:mm:ss");
				sqls.sql_ekle("musteri_arsiv", veriler);
				sqls.oda_durumu(veriler[8], "Boş");
				sqls.sql_silme("Musteri", "id", cikis_musteri_id);
				string[] verilerm = new string[7];
				verilerm[0] = DateTime.Now.Day.ToString();
				verilerm[1] = DateTime.Now.Month.ToString();
				verilerm[2] = DateTime.Now.Year.ToString();
				verilerm[3] = "gelir";
				verilerm[4] = "Oda Geliri";
				verilerm[5] = "Tekrarlamayan";
				verilerm[6] = odacikis_toplam_ucret.ToString();
				sqls.sql_ekle("gelir_gider", verilerm);
				MessageBox.Show("Çıkış işlemi başarılı", "Başarılı");
				musteri_cikis_ekrani_veri_getir();
			}
			else
			{
				MessageBox.Show("Bilinmeyen bir hata oluştu.","Uyarı");
			}

		
		}
		//servis ayarlari
		string servis_ayarlari_id = "";
		private void servisleri_getir()
		{
			ArrayList servisler = new ArrayList();
			servisler = sqls.servisleri_getir();
			s_servisler.Children.Clear();
			foreach (var item in servisler)
			{
				if (item != "")
				{
					string[] servis_bilgileri_parcali = item.ToString().Split('#');

					Button btn = new Button();
					btn.Name = "servisayar_" + servis_bilgileri_parcali[0];
					btn.Content = servis_bilgileri_parcali[1] + "\n" + servis_bilgileri_parcali[2] + " TL";
					btn.Width = 90;
					btn.Height = 80;
					btn.Margin = new Thickness(15);
					btn.BorderBrush = Brushes.DarkSlateGray;
					btn.Background = Brushes.AliceBlue;
					btn.Click += servis_buttonlar_tiklama;
					s_servisler.Children.Add(btn);
				}
			}


		}

		private void servis_buttonlar_tiklama(object sender, RoutedEventArgs e) {
			foreach (var item in s_servisler.Children.OfType<Button>())
			{
					item.Background = Brushes.AliceBlue;
			}
			Button btn = (Button)sender;
			btn.Background = Brushes.Green;
			string[] servis_parcala = btn.Name.Split('_');
			servis_ayarlari_id = servis_parcala[1];

			string[] secilen_servis_bilgileri = sqls.sercili_servis_getir(servis_parcala[1]);

			s_servis_adi.Text = secilen_servis_bilgileri[0];
			s_servis_ucreti.Text = secilen_servis_bilgileri[1];

			TextRange servis_onizleme_text = new TextRange(s_servis_aciklamasi.Document.ContentStart, s_servis_aciklamasi.Document.ContentEnd);
			servis_onizleme_text.Text = secilen_servis_bilgileri[2];

		}

		private void s_Yenile_Click(object sender, RoutedEventArgs e)
		{
			servisleri_getir();
		}

		private void s_Ekle_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				float test = float.Parse(s_servis_ucreti.Text);
				string[] veriler = new string[3];
				veriler[0] = s_servis_adi.Text;
				veriler[1] = s_servis_ucreti.Text;
				veriler[2] = new TextRange(s_servis_aciklamasi.Document.ContentStart, s_servis_aciklamasi.Document.ContentEnd).Text;
				sqls.sql_ekle("Servisler", veriler);
				servisleri_getir();
			}
			catch { MessageBox.Show("Servis fiyatı rakamlardan oluşmalıdır.", "Uyarı"); }
			
		}

		private void s_Değiştir_Click(object sender, RoutedEventArgs e)
		{
		if(servis_ayarlari_id != "")
			{
				float test = float.Parse(s_servis_ucreti.Text);
				sqls.servis_guncelle(servis_ayarlari_id, s_servis_adi.Text, float.Parse(s_servis_ucreti.Text), new TextRange(s_servis_aciklamasi.Document.ContentStart, s_servis_aciklamasi.Document.ContentEnd).Text);

				servisleri_getir();
			}
			
		}

		private void s_Sil_Click(object sender, RoutedEventArgs e)
		{
			if (servis_ayarlari_id != "")
			{
				sqls.sql_silme("Servisler", "id", servis_ayarlari_id);
				servisleri_getir();
			}
		}

		//oda ayarları
		private void ayarlar_odalari_getir()
		{
			ArrayList odalar = new ArrayList();
			odalar = sqls.oda_ayarlar_getir();
			o_ayarlar_odalar.Children.Clear();
			foreach (var item in odalar)
			{
				if (item != "")
				{
					string[] servis_bilgileri_parcali = item.ToString().Split('#');

					Button btn = new Button();
					btn.Name = "servisayar_" + servis_bilgileri_parcali[0];
					btn.Content = servis_bilgileri_parcali[1] + "\n" + servis_bilgileri_parcali[3] + " TL";
					btn.Width = 90;
					btn.Height = 80;
					btn.Margin = new Thickness(15);
					btn.BorderBrush = Brushes.DarkSlateGray;
					btn.Background = Brushes.AliceBlue;
					btn.Click += ayarlar_odalar_buttonlar_tiklama;
					o_ayarlar_odalar.Children.Add(btn);
				}
			}


		}
		string oda_ayarlari_secilen_oda = "";
		private void ayarlar_odalar_buttonlar_tiklama(object sender, RoutedEventArgs e)
		{
			foreach (var item in o_ayarlar_odalar.Children.OfType<Button>())
			{
				item.Background = Brushes.AliceBlue;
			}
			Button btn = (Button)sender;
			btn.Background = Brushes.Green;
			string[] oda_ayar_parcala = btn.Name.Split('_');
			oda_ayarlari_secilen_oda = oda_ayar_parcala[1];

			string[] secilen_oda_bilgileri = sqls.sercili_oda_getir(oda_ayar_parcala[1]);

			o_oda_adi.Text = secilen_oda_bilgileri[0];
			o_oda_kisi.Text = secilen_oda_bilgileri[1];
			o_oda_ucreti.Text = secilen_oda_bilgileri[2];
			TextRange servis_onizleme_text = new TextRange(o_oda_aciklamasi.Document.ContentStart, o_oda_aciklamasi.Document.ContentEnd);
			servis_onizleme_text.Text = secilen_oda_bilgileri[3];

		}

		private void o_Yenile_Click(object sender, RoutedEventArgs e)
		{
			ayarlar_odalari_getir();
		}

		private void o_Ekle_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				sqls.oda_ekle(o_oda_adi.Text.ToString(), Convert.ToInt32(o_oda_kisi.Text), float.Parse(o_oda_ucreti.Text), new TextRange(o_oda_aciklamasi.Document.ContentStart, o_oda_aciklamasi.Document.ContentEnd).Text.ToString());
				ayarlar_odalari_getir();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void o_Değiştir_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				sqls.oda_guncelleme(oda_ayarlari_secilen_oda, o_oda_adi.Text, Convert.ToInt32(o_oda_kisi.Text), float.Parse(o_oda_ucreti.Text), new TextRange(o_oda_aciklamasi.Document.ContentStart, o_oda_aciklamasi.Document.ContentEnd).Text.ToString());
				
				ayarlar_odalari_getir();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void o_Sil_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				sqls.sql_silme("Odalar", "id", oda_ayarlari_secilen_oda);
				ayarlar_odalari_getir();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
			ekran_doldur2();
		}

		private void g_arama_text_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				SqlConnection connection = new SqlConnection(cnnstr);
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}
				SqlCommand sorgu = new SqlCommand("SELECT * FROM musteri_arsiv where Tc_kimlik LIKE '%" + g_arama_text.Text + "%'", connection);
				sorgu.ExecuteNonQuery();
				DataSet dataSet = new DataSet();
				SqlDataAdapter adp = new SqlDataAdapter(sorgu);
				adp.Fill(dataSet, "musteri_arsiv");
				g_m_ekrani.ItemsSource = dataSet.Tables["musteri_arsiv"].DefaultView;
				connection.Close();

				g_m_ekrani.Columns[0].Header = "ID";
				g_m_ekrani.Columns[1].Header = "TC Kimlik Numarası";
				g_m_ekrani.Columns[2].Header = "Adı";
				g_m_ekrani.Columns[3].Header = "Soyadı";
				g_m_ekrani.Columns[4].Header = "Cep Telefonu";
				g_m_ekrani.Columns[5].Header = "Giriş Tarihi";
				g_m_ekrani.Columns[6].Header = "Çıkış Tarihi";
				g_m_ekrani.Columns[7].Header = "Adresi";
				g_m_ekrani.Columns[7].Width = 200;
				g_m_ekrani.Columns[8].Header = "Açıklama";
				g_m_ekrani.Columns[8].Width = 250;
				g_m_ekrani.Columns[9].Header = "Oda Numarası";
				g_m_ekrani.Columns[10].Header = "Kullanılan Servisler";
			}
			catch { }
		}

		private void Button_Click_4(object sender, RoutedEventArgs e)
		{
			ekran_doldur3();
		}

		private void ge_arama_text_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				SqlConnection connection = new SqlConnection(cnnstr);
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}
				SqlCommand sorgu = new SqlCommand("SELECT * FROM gelir_gider where yil LIKE '%" + ge_arama_text.Text + "%'", connection);
				sorgu.ExecuteNonQuery();
				DataSet dataSet = new DataSet();
				SqlDataAdapter adp = new SqlDataAdapter(sorgu);
				adp.Fill(dataSet, "gelir_gider");
				ge_m_ekrani.ItemsSource = dataSet.Tables["gelir_gider"].DefaultView;
				connection.Close();

				ge_m_ekrani.Columns[0].Header = "ID";
				ge_m_ekrani.Columns[1].Header = "Gün";
				ge_m_ekrani.Columns[2].Header = "Ay";
				ge_m_ekrani.Columns[3].Header = "Yıl";
				ge_m_ekrani.Columns[4].Header = "Gelir/Gider";
				ge_m_ekrani.Columns[5].Header = "Türü";
				ge_m_ekrani.Columns[6].Header = "Tekrarlıyormu";
				ge_m_ekrani.Columns[7].Header = "Miktar";
			}
			catch { }
	


		}
		
		
	}
}
