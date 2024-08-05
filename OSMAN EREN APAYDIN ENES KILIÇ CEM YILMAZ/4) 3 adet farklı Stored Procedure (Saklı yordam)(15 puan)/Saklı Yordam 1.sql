--Saklý Yordam 1 (Kitaplar ve Yayin Evi toblosuna yeni bir veri eklemeye yarýyan Saklý Yordam)


Create Proc sp_KitapUrunTablosunaKitapEkleme
(
   @KitapID int,
   @KitapISBN nvarchar(17),
   @KitapAdi nvarchar(50),
   @KitapYazari nvarchar(50),
   @KitapTuru nvarchar(50),
   @KitapFiyati nchar(10),
   @KitapAdet int,
   @KitapYayinEviID int,
   @KitapBasimYili int,
   @KitapGelisTarih date,
   @KitapSayfaSayisi int

)
As
Begin
   Insert Into KitapUrun(KitapID,ISBN,KitapAdi,KitapYazari,KitapTuru,KitapFiyati,KitapAdet,KitapYayinEviID,KitapBasimYil,KitapGelisTarih,KitapSayfaSayisi)
   Values (@KitapID,@KitapISBN,@KitapAdi,@KitapYazari,@KitapTuru,@KitapFiyati,@KitapAdet,@KitapYayinEviID,@KitapBasimYili,@KitapGelisTarih,@KitapSayfaSayisi)

End


Create Proc sp_YayinEviTablosunaKitapBilgiEkleme
(
  @YayinEviID int,
  @YayinEviAd nvarchar(50),
  @YayinEviAdres text,
  @YayinEviTelNo nvarchar(50),
  @YayinEviMail nvarchar(50)

)
As
Begin
   Insert Into Yayinevleri(YayinEviID,YayinEviAd,YayinEviAdres ,YayinEviTelNo,YayineviMail)
   Values (@YayinEviID,@YayinEviAd,@YayinEviAdres ,@YayinEviTelNo,@YayinEviMail)

End

-- Test
SET IDENTITY_INSERT Yayinevleri ON
Exec sp_YayinEviTablosunaKitapBilgiEkleme 688,'HayalGücü Yayýncýlýk','Sinop',5438792671,'hayalgucu@gmail.com'
SET IDENTITY_INSERT Yayinevleri OFF
Exec sp_KitapUrunTablosunaKitapEkleme 17,'115-6-599-20573-1','Pamuk Prenses','John GANGSTER','Çocuk',10,35,682,2015,'2017-01-25',15

Select * from KitapUrun

