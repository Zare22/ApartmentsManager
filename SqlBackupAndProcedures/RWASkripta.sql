--RWA SCRIPT
use RwaApartmani
go
--Drop constraint from Apartment
ALTER TABLE Apartment
DROP CONSTRAINT [FK_Apartment_ApartmentOwner]
go
--Drop constraint from Tag
ALTER TABLE Tag
DROP CONSTRAINT [FK_Tag_TagType]
go
--Drop TypeID from Tag
ALTER TABLE Tag
DROP COLUMN TypeID
go
--Drop TagType from Database
DROP TABLE TagType
go
--Drop columns from Apartment
ALTER TABLE Apartment
DROP COLUMN NameEng, TypeID, OwnerID
go
--Drop ApartmentOwner from DB
DROP TABLE ApartmentOwner
go
--Drop NameEng from Status
ALTER TABLE ApartmentStatus
DROP COLUMN NameEng
go
--Drop NameEng from Tag
ALTER TABLE Tag
DROP COLUMN NameEng
go
--Add ApartmentStatus
INSERT INTO ApartmentStatus (Guid, Name) 
VALUES (NEWID(), 'Any'), 
       (NEWID(), 'Occupied'), 
       (NEWID(), 'Reserved'), 
       (NEWID(), 'Vacant');
go
--Add Apartments
INSERT INTO Apartment (Guid, CreatedAt, StatusId, CityId, Address, Name, Price, MaxAdults, MaxChildren, TotalRooms, BeachDistance)
VALUES (NEWID(), GETDATE(), 1, 1, 'Trg bana Josipa Jelacica 1', 'Ocean View', 100.00, 2, 2, 2, 10),
       (NEWID(), GETDATE(), 2, 2, 'Maruliceva ulica 11', 'Cozy Retreat', 120.00, 4, 2, 3, 20),
       (NEWID(), GETDATE(), 3, 3, 'Korzo 12', 'City Centre Suite', 150.00, 6, 4, 4, 30),
       (NEWID(), GETDATE(), 6, 4, 'Tvrdavica bb', 'Luxury Escape', 200.00, 8, 4, 5, 40),
       (NEWID(), GETDATE(), 1, 5, 'Narodni trg 5', 'Beachfront Bliss', 250.00, 10, 6, 6, 50),
       (NEWID(), GETDATE(), 2, 6, 'Sv. Leopolda Mandica 1', 'Mountain Retreat', 300.00, 12, 6, 7, 60),
       (NEWID(), GETDATE(), 3, 7, 'Forum 1', 'Vintage Charm', 350.00, 14, 8, 8, 70),
       (NEWID(), GETDATE(), 6, 8, 'Trg Ante Starcevica 1', 'Modern Oasis', 400.00, 16, 8, 9, 80),
       (NEWID(), GETDATE(), 1, 9, 'Kralja Petra Kresimira IV 2', 'Rustic Retreat', 450.00, 18, 10, 10, 90),
       (NEWID(), GETDATE(), 2, 10, 'Trg Stjepana Radica 2', 'Coastal Escape', 500.00, 20, 10, 11, 100);
go
