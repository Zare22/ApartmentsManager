--Procedures script
CREATE PROCA AuthenticateAdmin
	@email nvarchar(256),
	@password nvarchar(max)
AS
BEGIN
	select*
	from AspNetUsers
	where Email = @email and PasswordHash = @password and IsAdmin = 1
END

GO

CREATE PROCEDURE GetApartments
AS
BEGIN
    SET NOCOUNT ON;

    SELECT A.*,
		(CASE WHEN S.Name = 'Any' THEN 6
          WHEN S.Name = 'Zauzeto' THEN 1
          WHEN S.Name = 'Rezervirano' THEN 2
          WHEN S.Name = 'Slobodno' THEN 3
     END) AS Status,
		C.Name AS City
    FROM Apartment A
    INNER JOIN ApartmentStatus S ON A.StatusID = S.Id
    left JOIN City C ON A.CityID = C.Id
	WHERE A.IsDeleted = 0
END

GO

CREATE PROCEDURE GetCities
AS
BEGIN
    SELECT*
    FROM City
END

GO

CREATE PROCEDURE GetTags
AS
BEGIN
    SELECT * FROM Tag
END

GO

CREATE PROCEDURE GetUsers
AS
BEGIN
    SELECT * FROM AspNetUsers
END

GO

CREATE PROCEDURE GetApartmentTags (@ApartmentId INT)
AS
BEGIN
    SELECT t.Id, t.Guid, t.Name, t.CreatedAt
    FROM TaggedApartment ta
    JOIN Tag t ON t.Id = ta.TagId
    WHERE ta.ApartmentId = @ApartmentId
END

GO

CREATE PROCEDURE GetApartmentImages (@ApartmentId INT)
AS
BEGIN
    SELECT Id, Guid, Base64Content, Name, IsRepresentative
    FROM ApartmentPicture
    WHERE ApartmentId = @ApartmentId
END

GO

CREATE PROCEDURE GetTagInUse (@tagId INT)
AS
BEGIN

    DECLARE @isInUse BIT

    SELECT @isInUse = COUNT(*)
    FROM TaggedApartment
    WHERE TagId = @tagId

    IF @isInUse > 0
        SET @isInUse = 1
    ELSE
        SET @isInUse = 0

    SELECT @isInUse as IsInUse
END

GO

CREATE PROCEDURE CreateTag (@tagName NVARCHAR(50))
AS
BEGIN
SET NOCOUNT ON
DECLARE @id INT, @guid UNIQUEIDENTIFIER, @createdAt DATETIME

SET @guid = NEWID()
SET @createdAt = GETDATE()

IF NOT EXISTS (SELECT * FROM Tag WHERE Name = @tagName)
BEGIN
    INSERT INTO Tag (Guid, CreatedAt, Name)
    VALUES (@guid, @createdAt, @tagName)

    SET @id = SCOPE_IDENTITY()
END
End

GO

CREATE PROCEDURE CreateApartment]
    (@Guid uniqueidentifier,
     @CreatedAt datetime2(7),
     @Status int,
     @Address nvarchar(250),
     @Name nvarchar(250),
     @Price money,
     @MaxAdults int,
     @MaxChildren int,
     @TotalRooms int,
     @BeachDistance int,
	 @Id int output)
AS
BEGIN
    INSERT INTO Apartment (Guid, CreatedAt, StatusId, Address, Name, Price, MaxAdults, MaxChildren, TotalRooms, BeachDistance, IsDeleted)
    VALUES (@guid, @createdAt, @Status, @address, @name, @price, @maxAdults, @maxChildren, @totalRooms, @beachDistance, 0)
	SET @Id = SCOPE_IDENTITY()
END

GO

GO PROCEDURE AddTagToApartment
    @ApartmentId INT,
    @TagId INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM TaggedApartment WHERE ApartmentId = @ApartmentId AND TagId = @TagId)
BEGIN

    INSERT INTO TaggedApartment (ApartmentId, TagId)
    VALUES (@ApartmentId, @TagId);
END
End

GO

CREATE PROCEDURE AddImageToApartment
    @ApartmentId INT,
	@CreatedAt datetime2,
    @Base64Content VARCHAR(MAX), 
    @Name NVARCHAR(250), 
    @IsRepresentative BIT
AS
BEGIN
    DECLARE @ImageId INT;

SELECT @ImageId = Id FROM ApartmentPicture WHERE Base64Content = @Base64Content AND Name = @Name;

IF @ImageId IS NULL
BEGIN
    INSERT INTO ApartmentPicture (Guid, CreatedAt, ApartmentId, Base64Content, Name, IsRepresentative)
    VALUES (NEWID(), @CreatedAt, @ApartmentId, @Base64Content, @Name, @IsRepresentative);
END

ELSE
BEGIN
    DECLARE @Exists INT;
    SELECT @Exists = COUNT(*) FROM ApartmentPicture WHERE Id = @ImageId AND ApartmentId = @ApartmentId;

    IF @Exists = 0
    BEGIN
        DECLARE @OtherAssociations INT;
        SELECT @OtherAssociations = COUNT(*) FROM ApartmentPicture WHERE Id = @ImageId;

        
        IF @OtherAssociations = 0
        BEGIN
            DELETE FROM ApartmentPicture WHERE Id = @ImageId;
        END
    END
END
end

GO

CREATE PROCEDURE AddCityToApartment (@CityName nvarchar(250), @ApartmentId int)
AS
BEGIN
    DECLARE @CityId int

    SELECT @CityId = Id FROM City WHERE Name = @CityName

    IF (@CityId IS NULL)
    BEGIN
        INSERT INTO City (Name) VALUES (@CityName)
        SET @CityId = SCOPE_IDENTITY()
    END

    UPDATE Apartment SET CityId = @CityId WHERE Id = @ApartmentId
END

GO

CREATE PROCEDURE DeleteApartment(@Id INT)
AS
BEGIN
    DELETE FROM ApartmentPicture WHERE ApartmentId = @Id
    DELETE FROM ApartmentReservation WHERE ApartmentId = @Id
    DELETE FROM ApartmentReview WHERE ApartmentId = @Id
    DELETE FROM TaggedApartment WHERE ApartmentId = @Id
        UPDATE Apartment
    SET IsDeleted = 1, DeletedAt = GETDATE()
    WHERE Id = @Id
END

GO

CREATE PROCEDURE DeleteImagesFromApartment
    @ApartmentId INT,
    @ImageIds ImageIdsType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM ApartmentPicture WHERE ApartmentId = @ApartmentId AND Id NOT IN (SELECT Id FROM @ImageIds);
END

GO

CREATE PROCEDURE DeleteTag
  @Id int
AS
BEGIN

  DELETE FROM Tag WHERE Id = @Id
END

GO

CREATE PROCEDURE UpdateApartment
(@Id int,
@Status int,
@Address nvarchar(250),
@Name nvarchar(250),
@Price money,
@MaxAdults int,
@MaxChildren int,
@TotalRooms int,
@BeachDistance int)
AS
BEGIN
UPDATE Apartment
SET 
StatusId = @Status,
Address = @address,
Name = @name,
Price = @price,
MaxAdults = @maxAdults,
MaxChildren = @maxChildren,
TotalRooms = @totalRooms,
BeachDistance = @beachDistance
WHERE Id = @Id
END
