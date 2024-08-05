-- Trigger 2 (Kitap almýþ olan üyemizin aldýðý kitapý ödünç verme iþlemini iptal ettiðimizde kitaplar tablosundaki KitapAdet deðerini tekrar arttýrýyor..)
 

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
	 PRINT N'Üyenin aldýðý kitap iptal baþarýyla iptal edildi..'; 
	 