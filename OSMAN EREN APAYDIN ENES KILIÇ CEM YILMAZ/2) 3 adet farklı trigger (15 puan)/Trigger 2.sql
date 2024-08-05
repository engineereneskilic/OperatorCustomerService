-- Trigger 2 (Kitap alm�� olan �yemizin ald��� kitap� �d�n� verme i�lemini iptal etti�imizde kitaplar tablosundaki KitapAdet de�erini tekrar artt�r�yor..)
 

Create Trigger TRG_SatisSil_StokArttir
On Odunc_Verme
After Delete
As

	 DECLARE @Alinan_KitapID int
	 DECLARE @Alinan_KisiTC numeric(11,0)

	 Select @Alinan_KitapID=Alinan_KitapID, @Alinan_KisiTC=alinan_UyeTC from deleted

	 Update KitapUrun set KitapAdet=KitapAdet+1

	 Where KitapID=@Alinan_KitapID

-- Test
	
	delete from Odunc_Verme where OduncID=22 --Sonra bu..
	 PRINT N'�yenin ald��� kitap iptal ba�ar�yla iptal edildi..'; 
	 