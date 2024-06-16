using System;
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
using System.Windows.Shapes;

namespace Pansiyon
{
	/// <summary>
	/// Interaction logic for musteri_duzenleme.xaml
	/// </summary>
	public partial class musteri_duzenleme : Window
	{
		string musteri_id = "";
		public musteri_duzenleme(string veriler)
		{
			musteri_id = veriler;
			InitializeComponent();
		
		}
		sql sqlm = new sql();
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

			string[] veriler = sqlm.musteri_bilgileri_getir(musteri_id);
			k_tc_no.Text = veriler[0];
			k_adi.Text = veriler[1];	
			k_soyadi.Text = veriler[2];
			k_cep_no.Text = veriler[3];
			k_giris_tarihi.SelectedDate = DateTime.Parse(veriler[4]);
			k_cikis_tarihi.SelectedDate= DateTime.Parse(veriler[5]);
			k_adres.Text = veriler[6];
			TextRange textRange = new TextRange(k_aciklama.Document.ContentStart, k_aciklama.Document.ContentEnd);
			textRange.Text = veriler[7];
		}

		private void kaydet_Click(object sender, RoutedEventArgs e)
		{
			//oda boşmu diye kontrol edilecek
			string[] veriler = new string[9];
			veriler[0] = k_tc_no.Text;
			veriler[1] = k_adi.Text;
			veriler[2] = k_soyadi.Text;
			veriler[3] = k_cep_no.Text;
			veriler[4] = k_giris_tarihi.SelectedDate.Value.ToString("yyyy-MM-dd") + " 13:00:00 PM";
			veriler[5] = k_cikis_tarihi.SelectedDate.Value.ToString("yyyy-MM-dd") + " 12:00:00 AM";
			veriler[6] = k_adres.Text;
			veriler[7] = new TextRange(k_aciklama.Document.ContentStart, k_aciklama.Document.ContentEnd).Text;
			veriler[8] = musteri_id;

			sqlm.musteri_guncelle(veriler); //güncelleme
			MessageBox.Show("TC Kimlik Numarası : " + k_tc_no.Text + "\n Adı : " + k_adi.Text + "\n Soyadi :" + k_soyadi.Text + "\n Cep Telefonu : " + k_cep_no.Text + "\n Giriş Tarihi : " + k_giris_tarihi.SelectedDate.Value.ToString("yyyy-MM-dd") + "\n Çıkış Tarihi : " + k_cikis_tarihi.SelectedDate.Value.ToString("yyyy-MM-dd") + "\n Adres : " + k_adres.Text + "\n Aciklama : " + new TextRange(k_aciklama.Document.ContentStart, k_aciklama.Document.ContentEnd).Text + "\n Başarılı");
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			sabit_veriler.musteri_guncelle = true;
		}
	}
}
