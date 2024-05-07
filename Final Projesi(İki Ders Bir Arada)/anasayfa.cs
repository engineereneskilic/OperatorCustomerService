using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient; // sql kütüphanesini çağırıyoruz
namespace Final_Projesi_İki_Ders_Bir_Arada_
{
    public partial class anasayfa : Form
    {
        public anasayfa()
        {
            InitializeComponent();


        }
         // giriş formumuzdan gerekli değişkenleri çekiyoruz ve bu formda rahat kullanabilmek için değişkenlere atıyoruz
      
        int ses_sayaci = giris.ses_sayaci ;
        int ses_sayaci_limit = giris.ses_sayaci_limit;
        char secim_ses;
         SqlConnection baglanti = new SqlConnection(giris.baglanticumlesi);
        string[] menu_id = new string[giris.menu_id_count]; // veritabanından gelen idler tutulcak
        string[] menu_isim = new string[giris.menu_isim_count]; // veritabanından gelen isimler tutulcak

        // Bu iki diziyi yapmamın sebebi veritabanındaki verileri çekip diziye atmak. Listele işlemini while döngüsü ile yapabilirdim fakat while döngüsü çok hızlı sorgu yapıyo yani indexlerini kontrol edemiyorum tabi bir yöntemi vardır da bilmiyorum.
        // Veritabanından çekip diziye attıktan sonra timera göre listelemesini istiyorum. Amaç seslerin zaman süresini bilmek ona göre yazıyı bekletmek istiyorum. Örneğin kadın sesi Enes diyosa sözünü bitirdikten sonra Enes kelimesi ekranda gözüksün istiyorum. Kadın söylemediği hiç bir şey ne erken yazılsın ne geç yazılsın tam lafı bittiğinde gelsin istiyorum..
        // bütün formlarda aynı mantık olduğu için hepsinde bu açıklamı yazmiyacam..

        private void Form1_Load(object sender, EventArgs e)
        {
            


            // Kullanıcı bilgilerini alalım ve sağ alttakı kullanıcı_label ımıza aktaralım hoş görünsün
            kullanici_label.Text = "Hoşgeldiniz "+giris.kullanici_isim;     

            timer.Enabled = true;
            timer.Start();

          
            Menu_haric_gorunmez();

            menu_id[0] = "";
            menu_isim[0] = "";
            baglanti.Open();
            SqlCommand sql = new SqlCommand("SELECT * FROM menuler WHERE menu_id LIKE 'anamenu%'", baglanti);// bu sorgu veritabanındaki menü listemden bu formda kullanmak istediğim menüleri seçmem için idlere bakarak istenilen menüleri alıyoruz
            SqlDataReader oku = sql.ExecuteReader();
            int index = 0;
            while (oku.Read()) { 
                
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
            seciminiz_label.Visible = false;
            seciminiz_kutusu.Visible = false;
            onayla_button.Visible = false;
        }


        private void timer1_Tick(object sender, EventArgs e)
        { 
            ses_sayaci++;
            if (ses_sayaci <= ses_sayaci_limit)
            {
                
                    giris.ne_calayim("ses_dosyalari", menu_id[ses_sayaci]); // ses dosyaları idye göre çalmaya başlıyor fakat timer her tetiklendiğinde
                    menu_label.Text += ses_sayaci + "." + menu_isim[ses_sayaci] + " için\n";
                    menu_label.Visible = true;
            }
            else
            {
               
                // Seçiminiz:

                seciminiz_label.Visible = true;
                seciminiz_kutusu.Visible = true;
                onayla_button.Visible = true;
                timer.Enabled = false;
                seciminiz_kutusu.Focus();
            }// else end




        } // Timer1_tic end


        // form geçişlerini ayarlıyoruz
        private void onayla_button_Click(object sender, EventArgs e)
        {

        if(seciminiz_kutusu.Text.Length == 1){ // switch char seçtiğimiz için fazla karakterde program çöküyo bunu önlemek için switche girmeden tek karakter hata mesajı versin
                Form3 form3 = new Form3();
                switch (Convert.ToChar(seciminiz_kutusu.Text)){

                case '1':
                        this.Close();
                        Form1 form1 = new Form1();
                        form1.Show();
                        break;
                case '2':
                    this.Close();
                    Form2 form2 = new Form2();
                    form2.Show();
                    break;
                case '3':
                    this.Close();
                    form3.Show();
                    break;
               case '4':  // from 3 ile aynı çünkü gerçek operatördede aynı
                   this.Close();
                    form3.Show();
                    break;
                    case '5':
                        this.Close();
                    Form5 form5 = new Form5();
                    form5.Show();
                    break;
                case '6':
                        this.Close();
                     Form6 form6 = new Form6();
                    form6.Show();
                    break;
                default:
                    MessageBox.Show("Lütfen doğru seçim yapınız !", karsilama_label.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;


             
         
             }
        } else MessageBox.Show("Lütfen doğru seçim yapınız !", karsilama_label.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

       
       
    }
}
