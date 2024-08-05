--Sakl� Yordam 3 (Odunc_Verme ve �ade tablosundan bir veriyi silmeye yar�yan sakl� yordam...)

Create Proc sp_YayineviIleKitapSil
(
  @kitapID int,
  @yayineviID int

)
As
Begin
  Delete from KitapUrun Where KitapID=@kitapID;
  Delete from Yayinevleri Where YayineviID=@yayineviID
End

--Test
Exec sp_YayineviIleKitapSil 18,690 -- 18 Numaral� kitab ve ba�l� oldu�u yay�nevini sil
