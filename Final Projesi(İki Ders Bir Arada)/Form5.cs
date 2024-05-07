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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        int ses_sayaci = giris.ses_sayaci - 1;
        int ses_sayaci_limit = giris.ses_sayaci_limit;
        char secim_ses;
        SqlConnection baglanti = new SqlConnection(giris.baglanticumlesi);
        string[] menu_id = new string[giris.menu_id_count]; // veritabanından gelen idler tutulcak
        string[] menu_isim = new string[giris.menu_isim_count]; // veritabanından gelen isimler tutulcak



        private void Form5_Load(object sender, EventArgs e)
        {
            // Kullanıcı bilgilerini alalım ve sağ alttakı kullanıcı_label ımıza aktaralım hoş görünsün
            kullanici_label.Text = "Hoşgeldiniz " + giris.kullanici_isim;     

            timer5.Enabled = true;
            timer5.Start();

            Menu_haric_gorunmez();

            baglanti.Open();
            SqlCommand sql = new SqlCommand("SELECT * FROM menuler WHERE menu_id LIKE '5-%' AND LEN(menu_id)=3", baglanti);
            SqlDataReader oku = sql.ExecuteReader();
            
            int index = -1;
            while (oku.Read())
            {

                index++;
                menu_id[index] = Convert.ToString(oku[0]).Trim(); menu_isim[index] = Convert.ToString(oku[1]).Trim();

            }
            baglanti.Close();
            ses_sayaci_limit = index;
        }
        private void Menu_haric_gorunmez()
        {

            menu_label.Text = "";
            menu_label.Visible = false;
            seciminiz_label5.Visible = false;
            seciminiz_kutusu.Visible = false;
            onayla_button.Visible = false;
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            ses_sayaci++;
            if (ses_sayaci <= ses_sayaci_limit)
            {

                if (ses_sayaci == 0)
                {
                    // ön bir açıklama ekleyelim
                    giris.ne_calayim("ses_dosyalari", menu_id[ses_sayaci]);
                    menu_label.Text += "Açıklama: " + menu_isim[ses_sayaci] + "\n";
                    menu_label.Visible = true;

                }
                else
                {
                    giris.ne_calayim("ses_dosyalari", menu_id[ses_sayaci]);
                    menu_label.Text += ses_sayaci + "." + menu_isim[ses_sayaci] + " için\n";
                    menu_label.Visible = true;
                }
            }
            else
            {
                // Bilgi: ses_sayaci_limit değişkeni aslında veritabanı limitidir.
                // Aşağıda işlemler veritabanından exta gerçekleşen işlemler o yüzden limit dışı.


                giris.ne_calayim("tekrar_edenler", "bir_ust_menu");
                menu_label.Text += "#.Bir üst menüye dönmek için\n";

                // ses sayacı limitinden iki fazla daha gittiği zaman artık secim gurubu gözükebilir..
                if (ses_sayaci <= ses_sayaci_limit + 2)
                {
                    // Seçiminiz:

                    seciminiz_label5.Visible = true;
                    seciminiz_kutusu.Visible = true;
                    onayla_button.Visible = true;
                    timer5.Enabled = false;
                    seciminiz_kutusu.Focus(); // otomatik odak
                }

            }


        }

        private void onayla_button_Click(object sender, EventArgs e)
        {
            if (seciminiz_kutusu.Text.Length == 1)
            {

                switch (Convert.ToChar(seciminiz_kutusu.Text))
                {

                    case '1':
                        button_islemi("5-1-1");
                        break;
                    case '2':
                        button_islemi("5-2-1");  
                        break;
                    case '3':
                        button_islemi("5-3-1");
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

        private void button_islemi(string menu_id)
        {
            baglanti.Open();
            SqlCommand sql1 = new SqlCommand("SELECT * FROM menuler WHERE menu_id='"+menu_id+"' ", baglanti);
            SqlDataReader oku1 = sql1.ExecuteReader();
            while (oku1.Read())
            {
                giris.ne_calayim("ses_dosyalari", Convert.ToString(oku1[0]).Trim());
                MessageBox.Show(Convert.ToString(oku1[1]).Trim(), karsilama_label.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            baglanti.Close();

            baglanti.Open();
            SqlCommand sql2 = new SqlCommand("SELECT sim_3G,tel_3G,[sim_4.5G],[tel_4.5G] FROM kullanicilar WHERE tel_no='" + giris.kullanici_tel + "'", baglanti);
            SqlDataReader oku2 = sql2.ExecuteReader();
            while (oku2.Read())
            {
                bool sim_3_B=Convert.ToBoolean(oku2[0]);
                bool tel_3_B=Convert.ToBoolean(oku2[1]);
                bool sim_4_5_B=Convert.ToBoolean(oku2[2]);
                bool tel_4_5_B=Convert.ToBoolean(oku2[3]);

                 // 4.5G veritabanından bool olarak alınıp stringe dökülmesi
                string sim_4_5G="";
                if (sim_4_5_B) sim_4_5G = "uyumlu"; else sim_4_5G = "uyumlu değildir";
                string tel_4_5G = "";
                if (tel_4_5_B) tel_4_5G = "uyumlu"; else tel_4_5G = "uyumlu değildir";
                // bitiş
                 // gelen 4.5G ise
                if (menu_id == "5-1-1"){
                    
                    MessageBox.Show("Simkartınız 4.5G "+sim_4_5G+", telefonunuz "+tel_4_5G+".", karsilama_label.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                //biti

                 // 3G veritabanından bool olarak alınıp stringe dökülmesi
                string sim_3G="";
                if (sim_3_B) sim_3G = "uyumlu"; else sim_3G = "uyumlu değildir";
                string tel_3G = "";
                if (tel_3_B) tel_3G = "uyumlu"; else tel_3G = "uyumlu değildir";
                // bitiş
                 // gelen 3G ise
                if (menu_id == "5-2-1"){
                    
                    MessageBox.Show("Simkartınız 3G "+sim_3G+", telefonunuz "+tel_3G+".", karsilama_label.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                //bitiş
            }
            baglanti.Close();


        }

    }
}
