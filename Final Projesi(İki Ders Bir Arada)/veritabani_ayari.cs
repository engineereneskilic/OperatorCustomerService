using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Final_Projesi_İki_Ders_Bir_Arada_
{
    public partial class veritabani_ayari : Form
    {
        public veritabani_ayari()
        {
            InitializeComponent();
        }
        // Bu giriş sayfasını yapmamın nedeni bazı pclerde test ettiğimde server namenin değişik olması iki çeşit tespit ettim kullanıcı girmeden hangisi olduğunu işaretleyip girerse çok daha güzel olur diye düşündüm..

        // burada seçilen servername belirliyoruz
        public static string secilen_server_name = "";

        private void veritabani_ayari_Load(object sender, EventArgs e)
        {
            string bilgisayar_kullanicisi = System.Environment.MachineName; // Bilgisayarın ismini öğreniyoruz benimki Enes sizinki örneğin HİLAL

            radioButton1.Text = bilgisayar_kullanicisi; // birinci radio butonumuza sadece bilgisayar ismi yazıyoruz
            radioButton2.Text = bilgisayar_kullanicisi + @"\SQLEXPRESS"; // ikinci radio buttonumuz ise /SQLEXPRESS ekini ekliyoruz
        }



        private void giris_buton_Click(object sender, EventArgs e)
        {
            // seçim yapıldıktan sonra giriş butonuna basılarak giriş formuna gidiyoruz...     
            giris giris_formu = new giris();
            giris_formu.Show();
            this.Hide();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           
            secilen_server_name = radioButton1.Text; 
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            secilen_server_name = radioButton2.Text;
        }

        
      
    }
}
