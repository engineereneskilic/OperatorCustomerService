using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;  // media kütüphanesini çağırıyoruz
using System.Data.SqlClient; // sql kütüphanesini çağırıyoruz

namespace Final_Projesi_İki_Ders_Bir_Arada_
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }


        int ses_sayaci = giris.ses_sayaci;
        int ses_sayaci_limit = giris.ses_sayaci_limit;
        char secim_ses;
        SqlConnection baglanti1 = new SqlConnection(giris.baglanticumlesi);
        SqlConnection baglanti2 = new SqlConnection(giris.baglanticumlesi);
        string[] menu_id = new string[giris.menu_id_count]; // veritabanından gelen idler tutulcak
        string[] menu_isim = new string[giris.menu_isim_count]; // veritabanından gelen isimler tutulcak


        private void Form6_Load(object sender, EventArgs e)
        {
            // Kullanıcı bilgilerini alalım ve sağ alttakı kullanıcı_label ımıza aktaralım hoş görünsün
            kullanici_label.Text = "Hoşgeldiniz " + giris.kullanici_isim;

            timer6.Enabled = true;
            timer6.Start();

          
            Menu_haric_gorunmez();

            menu_id[0] = "";
            menu_isim[0] = "";
            baglanti1.Open();
            SqlCommand sql = new SqlCommand("SELECT * FROM menuler WHERE menu_id LIKE '6-%' AND LEN(menu_id)=3", baglanti1);
            SqlDataReader oku = sql.ExecuteReader();
            int index = 0;
            while (oku.Read())
            {

                index++;
                menu_id[index] = Convert.ToString(oku[0]).Trim(); menu_isim[index] = Convert.ToString(oku[1]).Trim();

            }
            ses_sayaci_limit = index;
            baglanti1.Close();

        }

        private void Menu_haric_gorunmez()
        {

            menu_label.Text = "";
            menu_label.Visible = false;
            seciminiz_label.Visible = false;
            seciminiz_kutusu.Visible = false;
            onayla_button.Visible = false;
        }

        private void timer6_Tick(object sender, EventArgs e)
        {

            ses_sayaci++;
            if (ses_sayaci <= ses_sayaci_limit)
            {

                giris.ne_calayim("ses_dosyalari", menu_id[ses_sayaci]);
                menu_label.Text += ses_sayaci + "." + menu_isim[ses_sayaci] + " için\n";
                menu_label.Visible = true;

            }
            else
            {
                // Bilgi: ses_sayaci_limit değişkeni aslında veritabanı limitidir.
                // Aşağıda işlemler veritabanından exta gerçekleşen işlemler o yüzden limit dışı.

                // ses sayacı limitinden iki fazla daha gittiği zaman bir üst menü seçeneği
                if (ses_sayaci == ses_sayaci_limit + 1)
                {
                    giris.ne_calayim("tekrar_edenler", "bir_ust_menu");
                    menu_label.Text += "#.Bir üst menüye dönmek için\n";
                }
                else
                // ses sayacı limitinden üç fazla daha gittiği zaman artık secim gurubu gözükebilir..
                if (ses_sayaci == ses_sayaci_limit + 3)
                {
                    // Seçiminiz:

                    seciminiz_label.Visible = true;
                    seciminiz_kutusu.Visible = true;
                    onayla_button.Visible = true;
                    timer6.Enabled = false;
                    seciminiz_kutusu.Focus(); // otomatik odak
                }

            }
        }

        public static string giden_menu_ismi;
        public static int giden_menu_id;

        private void onayla_button_Click(object sender, EventArgs e)
        {

            if (seciminiz_kutusu.Text.Length == 1)
            {
                Form6_icerik form6_icerik = new Form6_icerik();
                switch (Convert.ToChar(seciminiz_kutusu.Text))
                {

                    case '1':
                        giden_menu_ismi = menu_isim[1];
                        giden_menu_id = 1;
                        form6_icerik.Show();
                        break;
                    case '2':
                        giden_menu_ismi = menu_isim[2];
                        giden_menu_id = 2;
                        form6_icerik.Show();
                        break;
                    case '3':
                         // 3 seçilirse başka bir forma göndermemize gerek yok diğer bu formdan işlem görebiliriz
                        baglanti1.Open();
                            SqlCommand interaktif_sql = new SqlCommand("SELECT interaktif_sifresi FROM kullanicilar WHERE tel_no='"+giris.kullanici_tel+"'", baglanti1); // veritabanından interaktif
                            SqlDataReader interaktif_oku = interaktif_sql.ExecuteReader();
                            while(interaktif_oku.Read())
                            {
                                baglanti2.Open();
                                SqlCommand interaktif_menu_sql = new SqlCommand("SELECT * FROM menuler WHERE menu_id='6-3-0'", baglanti2); //  menülerden idye göre interaktif menüsüne gidiyoruz
                                SqlDataReader interaktif_menu = interaktif_menu_sql.ExecuteReader();
                                while (interaktif_menu.Read())
                                {
                                    giris.ne_calayim("ses_dosyalari","6-3-0"); // interaktif sesini çalışıyoruz
                                    MessageBox.Show(Convert.ToString(interaktif_menu[1]), karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    MessageBox.Show("İnteraktif şifreniz: " + interaktif_oku[0], karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                baglanti2.Close();
                            }

                        baglanti1.Close();

                        break;
                    case '4':
                        giden_menu_ismi = menu_isim[4];
                        giden_menu_id = 4;
                        form6_icerik.Show();
                        break;
                    case '#':
                        anasayfa anasayfa = new anasayfa();
                        this.Close();
                        anasayfa.Show();
                        break;
                    default:
                        MessageBox.Show("Lütfen doğru seçim yapınız !", karsilama_label.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;




                }
            }
            else MessageBox.Show("Lütfen doğru seçim yapınız !", karsilama_label.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
       
        }

     


    }
}
