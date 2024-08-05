--Sakl� Yordam 2 (�yeler tablosunda �ye ki�inin ki�isel bilgilerini g�ncelemeye yar�yan sakl� yordam.)

Create Proc sp_UyeBilgiGuncelle
(
  @KisiTc numeric(11,0),
  @Ad nvarchar(50),
  @SoyAd nvarchar(50),
  @Cinsiyet nvarchar(50),
  @Telefon_Numaras� numeric(10, 0),
  @Adres text,
  @E_Mail nvarchar(50)

)
As
Begin
Update Uyeler
Set  UyeAd = @Ad , UyeSoyad = @SoyAd , UyeCinsiyet = @Cinsiyet , UyeTel_No = @Telefon_Numaras� , UyeAdres = @Adres , UyeMail = @E_Mail
Where UyeTC = @KisiTc
End

-- Test

Exec sp_UyeBilgiGuncelle '90489498413','Ramiz','Kocat�rk','Erkek','5429479879','Edirne','ramizkocaturk@hotmail.com' -- (Burada Uyeler tablosunda, 90489498413 TC Numaral� ki�inin Adres ve E-Mail'ni g�nceledik.)

