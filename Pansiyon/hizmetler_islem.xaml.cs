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
using System.Windows.Shapes;

namespace Pansiyon
{
	/// <summary>
	/// Interaction logic for hizmetler.xaml
	/// </summary>
	public partial class hizmetler : Window
	{
		int musteri_id = 0;
		public hizmetler(int musteri_id)
		{
			InitializeComponent();
			this.musteri_id=musteri_id;
		}
		sql sqlm = new sql(); 
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ArrayList tum_servisler = new ArrayList();
			tum_servisler = sqlm.servisleri_getir();
			if(tum_servisler.Count != 0)
			{
				foreach (var item in tum_servisler)
				{
					string[] parcala = item.ToString().Split('#');

					Button btn = new Button();
					btn.Name = "tumhizmetler_" + parcala[0];
					btn.Content =  parcala[1] + "\n" + parcala[2] + " TL";
					btn.Width = 90;
					btn.Height = 80;
					btn.Margin = new Thickness(15);
					btn.BorderBrush = Brushes.DarkSlateGray;
					btn.Background = Brushes.Gray;
					btn.Click += btn_servisler;
					hizmet_sol.Children.Add(btn);
				}
			}
			musteri_aktif_hizmetleri_getir();
		}
		string musteri_servis = "";
		private void musteri_aktif_hizmetleri_getir()
		{
			hizmet_sag.Children.Clear();
			musteri_servis = sqlm.musteri_hizmetleri_gonder(musteri_id.ToString());
			string[] muster_servisleri_aktif = musteri_servis.Split('+');
			foreach (var item in muster_servisleri_aktif)
			{
				if(item != "")
				{
					string[] servis_bilgileri_g = sqlm.servis_adi_getir(item);

					Button btn = new Button();
					btn.Name = "musterihizmetler_" + item;
					btn.Content = servis_bilgileri_g[0] + "\n" + servis_bilgileri_g[1] + " TL";
					btn.Width = 90;
					btn.Height = 80;
					btn.Margin = new Thickness(15);
					btn.BorderBrush = Brushes.DarkSlateGray;
					btn.Background = Brushes.Gray;
					btn.Click += btn_musteriservisler;
					hizmet_sag.Children.Add(btn);

				}
			


			}
		}
		private void btn_musteriservisler(object sender, EventArgs e) //servis_iptal etme
		{
			
			try
			{
				Button btn = (Button)sender;
		
				bool tek_kont = false;
				string yenileme = "";
				string[] servislerm = musteri_servis.Split('+');
				string[] id_parcala = btn.Name.Split('_');
				if (servislerm.Length != 0)
				{
					foreach (var item in servislerm)
					{
						if (item != id_parcala[1] || tek_kont)
						{
							if (yenileme == "")
								yenileme = item;
							else
								yenileme = yenileme + "+" + item;
						}
						else
							tek_kont = true;

					}
					musteri_servis = yenileme;
					sqlm.servis_ekleme(yenileme, musteri_id.ToString());
				}
				musteri_aktif_hizmetleri_getir();


			}
			catch(Exception ex) { MessageBox.Show(ex.ToString()); }

		

		}
		private void btn_servisler(object sender, EventArgs e) //servis ekleme
		{
		
			try
			{
				Button btn = (Button)sender;
				string[] id_parcala = btn.Name.Split('_');
				string eklenmis = "";
				if (musteri_servis == "")
				{
					eklenmis = id_parcala[1];
				}
				else
				{
					eklenmis = musteri_servis + "+" + id_parcala[1];

				}
				musteri_servis = eklenmis;
				sqlm.servis_ekleme(eklenmis,musteri_id.ToString());
				musteri_aktif_hizmetleri_getir();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}

		}
	}
}
