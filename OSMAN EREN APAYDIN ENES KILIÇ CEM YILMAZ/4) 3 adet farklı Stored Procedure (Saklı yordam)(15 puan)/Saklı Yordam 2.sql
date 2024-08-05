--Saklý Yordam 2 (Üyeler tablosunda üye kiþinin kiþisel bilgilerini güncelemeye yarýyan saklý yordam.)

Create Proc sp_UyeBilgiGuncelle
(
  @KisiTc numeric(11,0),
  @Ad nvarchar(50),
  @SoyAd nvarchar(50),
  @Cinsiyet nvarchar(50),
  @Telefon_Numarasý numeric(10, 0),
  @Adres text,
  @E_Mail nvarchar(50)

)
As
Begin
Update Uyeler
Set  UyeAd = @Ad , UyeSoyad = @SoyAd , UyeCinsiyet = @Cinsiyet , UyeTel_No = @Telefon_Numarasý , UyeAdres = @Adres , UyeMail = @E_Mail
Where UyeTC = @KisiTc
End

-- Test

Exec sp_UyeBilgiGuncelle '90489498413','Ramiz','Kocatürk','Erkek','5429479879','Edirne','ramizkocaturk@hotmail.com' -- (Burada Uyeler tablosunda, 90489498413 TC Numaralý kiþinin Adres ve E-Mail'ni günceledik.)

