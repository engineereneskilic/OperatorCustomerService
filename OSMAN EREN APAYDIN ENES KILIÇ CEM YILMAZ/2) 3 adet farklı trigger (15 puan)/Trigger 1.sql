
-- Trigger 1 (Kitap alan üyemizin bilgilerini ödünç verme tablosuna ekliyor. Kitaplar tablosundaki aldiði kitabýn adet deðerini düþürüyor..)
Create Trigger TRG_StokAzalt
On Odunc_Verme
After Insert
As
     DECLARE @Alinan_KitapID int
	 DECLARE @Alinan_KisiTC numeric(11,0)

	 Select @Alinan_KitapID=Alinan_KitapID, @Alinan_KisiTC=alinan_UyeTC from inserted

	 Update KitapUrun set KitapAdet=KitapAdet-1

	 Where KitapID=@Alinan_KitapID

-- Test
	
	 INSERT INTO Odunc_Verme VALUES(25,2,30756561292,'2017-01-25')
	 PRINT N'Kitabý alan üyemizin bilgileri ödünç verme tablosuna eklendi..';  



