--Saklý Yordam 3 (Odunc_Verme ve Ýade tablosundan bir veriyi silmeye yarýyan saklý yordam...)

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
Exec sp_YayineviIleKitapSil 18,690 -- 18 Numaralý kitab ve baðlý olduðu yayýnevini sil
