CREATE DATABASE PhoenixEnterpriseDB
GO

USE [PhoenixEnterpriseDB];
GO

CREATE TABLE PersonInfoTB(
    PAddressId INT IDENTITY(1,1) PRIMARY KEY,
    Mail VARCHAR(60) NOT NULL,
    Tlf VARCHAR(14),
    CPRNumber VARCHAR(20) NOT NULL,
    Address VARCHAR(100) NOT NULL,
    PostalCode VARCHAR(10) NOT NULL,
    Country VARCHAR(50) NOT NULL,
    RoadName VARCHAR(100) NOT NULL,
    HouseNumber VARCHAR(10) NOT NULL,
    City VARCHAR(50) NOT NULL
);

CREATE TABLE PersonTB(
    PersonId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(28) NOT NULL,
    LastName VARCHAR(28) NOT NULL,
    PAddressId INT NOT NULL,

    FOREIGN KEY (PAddressId) REFERENCES PersonInfoTB(PAddressId) ON DELETE CASCADE
);

CREATE TABLE UsersTB(
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    UPassWord VARCHAR(255) NOT NULL,
    Balance DECIMAL(18, 2),
    PersonId INT NOT NULL,

    FOREIGN KEY (PersonId) REFERENCES PersonTB(PersonId) ON DELETE CASCADE
);

CREATE TABLE EmployeeTB(
    EmplyeeId INT IDENTITY(1,1) PRIMARY KEY,
    EmplyeePassword VARCHAR(255) NOT NULL,
    Tital VARCHAR(50) NOT NULL,
    WorkMail VARCHAR(60) NOT NULL,
    WorkTlf VARCHAR(14) NOT NULL,
    AdminPassword VARCHAR(255),
    PersonId INT NOT NULL,

    FOREIGN KEY (PersonId) REFERENCES PersonTB(PersonId) ON DELETE CASCADE
);

CREATE TABLE ModeratorTB(
    ModeratorId INT IDENTITY(1,1) PRIMARY KEY,
    IsMod BIT NOT NULL,
    EmployeeId INT NOT NULL,

    FOREIGN KEY (EmployeeId) REFERENCES EmployeeTB(EmplyeeId) ON DELETE CASCADE
);

CREATE TABLE AdminTB(
    AdminId INT IDENTITY(1,1) PRIMARY KEY,
    IsAdmin BIT NOT NULL,
    EmployeeId INT NOT NULL,

    FOREIGN KEY (EmployeeId) REFERENCES EmployeeTB(EmplyeeId) ON DELETE CASCADE
);
GO

-- Table for Location
CREATE TABLE Location (
    LocationId INT IDENTITY(1,1) PRIMARY KEY,
    LocationName VARCHAR(255) NOT NULL,
    LCountry VARCHAR(255) NOT NULL,
    LCity VARCHAR(255) NOT NULL,
    LStreet VARCHAR(255) NOT NULL,
    LZipCode VARCHAR(50) NOT NULL,
    StorageSpaceLeft FLOAT NOT NULL,
    MaxStorageSpaceLeft FLOAT NOT NULL
);

-- Table for Item
CREATE TABLE Item (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ItemName VARCHAR(255) NOT NULL,
    ItemDescription VARCHAR(255) NOT NULL,
    StorageSpaceTakken FLOAT NOT NULL
);

-- Table for CombinedItemLocation
CREATE TABLE CombinedItemLocation (
    CombinedID INT IDENTITY(1,1) PRIMARY KEY,
    LocationId INT NOT NULL,
    ItemId INT NOT NULL,
    FOREIGN KEY (LocationId) REFERENCES Location(LocationId),
    FOREIGN KEY (ItemId) REFERENCES Item(Id)
);
GO
USE [PhoenixEnterpriseDB];
GO

-- CreateEmployee procedure
CREATE PROCEDURE CreateEmployee
    @FirstName VARCHAR(28),
    @LastName VARCHAR(28),
    @Mail VARCHAR(60),
    @Tlf VARCHAR(14),
    @CPRNumber VARCHAR(20),
    @Address VARCHAR(100),
    @PostalCode VARCHAR(10),
    @Country VARCHAR(50),
    @RoadName VARCHAR(100),
    @HouseNumber VARCHAR(10),
    @City VARCHAR(50),
    @EmployeePassword VARCHAR(255),
    @Title VARCHAR(50),
    @WorkMail VARCHAR(60),
    @WorkTlf VARCHAR(14),
    @AdminPassword VARCHAR(255) = NULL, -- Nullable for non-admin employees
    @IsAdmin BIT,
    @IsMod BIT
AS
BEGIN
    SET NOCOUNT ON;

    -- Step 1: Insert data into PersonInfoTB
    DECLARE @PersonInfoId INT;
    INSERT INTO PersonInfoTB (Mail, Tlf, CPRNumber, Address, PostalCode, Country, RoadName, HouseNumber, City)
    VALUES (@Mail, @Tlf, @CPRNumber, @Address, @PostalCode, @Country, @RoadName, @HouseNumber, @City);
    SET @PersonInfoId = SCOPE_IDENTITY();

    -- Step 2: Insert data into PersonTB
    DECLARE @PersonId INT;
    INSERT INTO PersonTB (FirstName, LastName, PAddressId)
    VALUES (@FirstName, @LastName, @PersonInfoId);
    SET @PersonId = SCOPE_IDENTITY();

    -- Step 3: Insert data into EmployeeTB
    DECLARE @EmployeeId INT;
    INSERT INTO EmployeeTB (EmplyeePassword, Tital, WorkMail, WorkTlf, AdminPassword, PersonId)
    VALUES (@EmployeePassword, @Title, @WorkMail, @WorkTlf, @AdminPassword, @PersonId);
    SET @EmployeeId = SCOPE_IDENTITY();

    -- Step 4: Insert into AdminTB and ModeratorTB with respective values
    IF @IsAdmin = 1
    BEGIN
        INSERT INTO AdminTB (IsAdmin, EmployeeId)
        VALUES (@IsAdmin, @EmployeeId);
    END

    IF @IsMod = 1
    BEGIN
        INSERT INTO ModeratorTB (IsMod, EmployeeId)
        VALUES (@IsMod, @EmployeeId);
    END
END;
GO


CREATE PROCEDURE GetEmployeeByName
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT e.EmplyeeId, e.EmplyeePassword, e.Tital, e.WorkMail, e.WorkTlf,
           p.PersonId, p.FirstName, p.LastName,
           pi.Mail, pi.Tlf, pi.CPRNumber, pi.Address, pi.PostalCode, pi.Country, pi.RoadName, pi.HouseNumber, pi.City,
           a.AdminId, a.IsAdmin,
           m.ModeratorId, m.IsMod
    FROM EmployeeTB e
    INNER JOIN PersonTB p ON e.PersonId = p.PersonId
    INNER JOIN PersonInfoTB pi ON p.PAddressId = pi.PAddressId
    LEFT JOIN AdminTB a ON e.EmplyeeId = a.EmployeeId
    LEFT JOIN ModeratorTB m ON e.EmplyeeId = m.EmployeeId
    WHERE p.FirstName = @FirstName AND p.LastName = @LastName;
END;
;

GO
CREATE PROCEDURE [dbo].[GetEmployee]
    @EmployeeId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        e.EmplyeeId AS EmployeeId, 
        e.EmplyeePassword AS EmployeePassword, 
        e.Tital AS Title, 
        e.WorkMail, 
        e.WorkTlf, 
        e.AdminPassword,
        p.PersonId, 
        p.FirstName, 
        p.LastName,
        pi.PAddressId, 
        pi.Mail, 
        pi.Tlf, 
        pi.CPRNumber, 
        pi.Address, 
        pi.PostalCode, 
        pi.RoadName, 
        pi.HouseNumber, 
        pi.City, 
        pi.Country,
        a.AdminId, 
        a.IsAdmin,
        m.ModeratorId, 
        m.IsMod
    FROM EmployeeTB e
    INNER JOIN PersonTB p ON e.PersonId = p.PersonId
    INNER JOIN PersonInfoTB pi ON p.PAddressId = pi.PAddressId
    LEFT JOIN AdminTB a ON e.EmplyeeId = a.EmployeeId
    LEFT JOIN ModeratorTB m ON e.EmplyeeId = m.EmployeeId
    WHERE e.EmplyeeId = @EmployeeId;
END;
GO

CREATE PROCEDURE [dbo].[CreateAUser]
    @LoginName NVARCHAR(128),
    @Password NVARCHAR(255)
AS
BEGIN
    BEGIN TRY
        -- Step 1: Create the login
        DECLARE @Sql NVARCHAR(MAX);
        SET @Sql = 'CREATE LOGIN [' + @LoginName + '] WITH PASSWORD = ''' + @Password + ''';';
        EXEC sp_executesql @Sql;

        -- Step 2: Create the user in the specified database
        SET @Sql = 'USE [AutoAuctionDB]; CREATE USER [' + @LoginName + '] FOR LOGIN [' + @LoginName + '];';
        EXEC sp_executesql @Sql;

        -- Step 3: Grant EXECUTE permission
        SET @Sql = 'GRANT EXECUTE TO [' + @LoginName + '];';
        EXEC sp_executesql @Sql;

        -- Step 4: Deny SELECT on tables
        SET @Sql = 'DENY SELECT ON SCHEMA::dbo TO [' + @LoginName + '];';
        EXEC sp_executesql @Sql;

        -- Step 5: Deny INSERT on tables
        SET @Sql = 'DENY INSERT ON SCHEMA::dbo TO [' + @LoginName + '];';
        EXEC sp_executesql @Sql;

        -- Step 6: Deny UPDATE on tables
        SET @Sql = 'DENY UPDATE ON SCHEMA::dbo TO [' + @LoginName + '];';
        EXEC sp_executesql @Sql;

        -- Step 7: Deny DELETE on tables
        SET @Sql = 'DENY DELETE ON SCHEMA::dbo TO [' + @LoginName + '];';
        EXEC sp_executesql @Sql;

        PRINT 'User created successfully.';
    END TRY
    BEGIN CATCH
        PRINT 'Error occurred: ' + ERROR_MESSAGE();
    END CATCH;
END;
GO

CREATE PROCEDURE GetAllEmployees
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        e.EmplyeeId AS EmployeeId, 
        e.EmplyeePassword AS EmployeePassword, 
        e.Tital AS Title, 
        e.WorkMail, 
        e.WorkTlf, 
        e.AdminPassword,
        p.PersonId, 
        p.FirstName, 
        p.LastName,
        pi.CPRNumber,
        pi.PAddressId, 
        pi.Mail, 
        pi.Tlf, 
        pi.Address, 
        pi.PostalCode, 
        pi.RoadName, 
        pi.HouseNumber, 
        pi.City, 
        pi.Country,
        a.AdminId, 
        a.IsAdmin,
        m.ModeratorId, 
        m.IsMod
    FROM EmployeeTB e
    INNER JOIN PersonTB p ON e.PersonId = p.PersonId
    INNER JOIN PersonInfoTB pi ON p.PAddressId = pi.PAddressId
    LEFT JOIN AdminTB a ON e.EmplyeeId = a.EmployeeId
    LEFT JOIN ModeratorTB m ON e.EmplyeeId = m.EmployeeId;
END;
GO
CREATE PROCEDURE DeleteEmployee
    @EmployeeId INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM EmployeeTB
    WHERE EmplyeeId = @EmployeeId;
END;
GO

-- GetAllLocations
CREATE PROCEDURE GetAllLocations
AS
BEGIN
    SELECT LocationId, LocationName, LCountry, LCity, LStreet, LZipCode, StorageSpaceLeft, MaxStorageSpaceLeft
    FROM Location;
END;
GO

-- AddItems
CREATE PROCEDURE AddItems
    @ItemName NVARCHAR(255),
    @Description NVARCHAR(255),
    @StorageSpaceTakken FLOAT
AS
BEGIN
    INSERT INTO Item (ItemName, ItemDescription, StorageSpaceTakken)
    VALUES (@ItemName, @Description, @StorageSpaceTakken);

    -- Return the ID of the newly created item
    SELECT SCOPE_IDENTITY() AS NewItemId;
END;
GO

-- AddCombinedLocation
CREATE PROCEDURE AddCombinedLocation
    @LocationId INT,
    @ItemId INT
AS
BEGIN
    -- Validate storage space availability
    DECLARE @StorageSpaceLeft FLOAT;
    DECLARE @StorageSpaceTakken FLOAT;

    SELECT @StorageSpaceLeft = StorageSpaceLeft
    FROM Location
    WHERE LocationId = @LocationId;

    SELECT @StorageSpaceTakken = StorageSpaceTakken
    FROM Item
    WHERE Id = @ItemId;

    IF @StorageSpaceLeft >= @StorageSpaceTakken
    BEGIN
        INSERT INTO CombinedItemLocation (LocationId, ItemId)
        VALUES (@LocationId, @ItemId);

        -- Update the remaining storage space in the location
        UPDATE Location
        SET StorageSpaceLeft = StorageSpaceLeft - @StorageSpaceTakken
        WHERE LocationId = @LocationId;
    END
    ELSE
    BEGIN
        RAISERROR ('Not enough storage space available.', 16, 1);
    END
END;
GO

-- AddLocation
CREATE PROCEDURE AddLocation
    @LocationName NVARCHAR(255),
    @LCountry NVARCHAR(255),
    @LStreet NVARCHAR(255),
    @LCity NVARCHAR(255),
    @LZipCode NVARCHAR(50),
    @StorageSpaceLeft FLOAT,
    @MaxStorageSpaceLeft FLOAT
AS
BEGIN
    INSERT INTO Location (LocationName, LCountry, LStreet, LCity, LZipCode, StorageSpaceLeft, MaxStorageSpaceLeft)
    VALUES (@LocationName, @LCountry, @LStreet, @LCity, @LZipCode, @StorageSpaceLeft, @MaxStorageSpaceLeft);
END;
GO

-- GetItems
CREATE PROCEDURE GetItems
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        cil.CombinedID AS CombinedId,
        l.LocationId,
        l.LocationName,
        l.LCountry,
        l.LCity,
        l.LStreet,
        l.LZipCode,
        l.StorageSpaceLeft,
        l.MaxStorageSpaceLeft,
        i.Id AS ItemId,
        i.ItemName,
        i.ItemDescription,
        i.StorageSpaceTakken
    FROM 
        CombinedItemLocation cil
    INNER JOIN 
        Location l ON cil.LocationId = l.LocationId
    INNER JOIN 
        Item i ON cil.ItemId = i.Id;
END;
GO

-- UpdateItem
CREATE PROCEDURE UpdateItem
    @ItemId INT,
    @ItemName NVARCHAR(255),
    @ItemDescription NVARCHAR(255),
    @StorageSpaceTakken FLOAT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Item
    SET ItemName = @ItemName,
        ItemDescription = @ItemDescription,
        StorageSpaceTakken = @StorageSpaceTakken
    WHERE Id = @ItemId;
END;
GO

CREATE PROCEDURE AddUser
    @FirstName VARCHAR(28),
    @LastName VARCHAR(28),
    @Password VARCHAR(255),
    @CPR VARCHAR(20),
    @PostalCode VARCHAR(10),
    @Country VARCHAR(50),
    @Address VARCHAR(100),
    @Mail VARCHAR(60),
    @City VARCHAR(50),
    @HouseNumber VARCHAR(10),
    @Tlf VARCHAR(14),
    @RoadName VARCHAR(100),
    @Balance DECIMAL(18, 2)
AS
BEGIN
    SET NOCOUNT ON;

    -- Insert into PersonInfoTB
    DECLARE @PAddressId INT;
    INSERT INTO PersonInfoTB (Mail, Tlf, CPRNumber, Address, PostalCode, Country, RoadName, HouseNumber, City)
    VALUES (@Mail, @Tlf, @CPR, @Address, @PostalCode, @Country, @RoadName, @HouseNumber, @City);
    SET @PAddressId = SCOPE_IDENTITY();

    -- Insert into PersonTB
    DECLARE @PersonId INT;
    INSERT INTO PersonTB (FirstName, LastName, PAddressId)
    VALUES (@FirstName, @LastName, @PAddressId);
    SET @PersonId = SCOPE_IDENTITY();

    -- Insert into UsersTB
    INSERT INTO UsersTB (UPassWord, Balance, PersonId)
    VALUES (@Password, @Balance, @PersonId);
END;
GO

CREATE PROCEDURE GetUserByID
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.UserId AS UId,
        u.UPassWord AS UserPassword,
        u.Balance,
        p.PersonId,
        p.FirstName,
        p.LastName,
        pi.PAddressId AS Id,
        pi.Mail,
        pi.Tlf,
        pi.Address,
        pi.PostalCode,
        pi.RoadName,
        pi.HouseNumber,
        pi.City,
        pi.Country,
        pi.CPRNumber
    FROM UsersTB u
    INNER JOIN PersonTB p ON u.PersonId = p.PersonId
    INNER JOIN PersonInfoTB pi ON p.PAddressId = pi.PAddressId
    WHERE u.UserId = @UserId;
END;
GO

CREATE PROCEDURE UpdateUser
    @UserId INT,
    @FirstName VARCHAR(28),
    @LastName VARCHAR(28),
    @Password VARCHAR(255),
    @CPR VARCHAR(20),
    @PostalCode VARCHAR(10),
    @Country VARCHAR(50),
    @Address VARCHAR(100),
    @Mail VARCHAR(60),
    @City VARCHAR(50),
    @HouseNumber VARCHAR(10),
    @Tlf VARCHAR(14),
    @RoadName VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- Get the PersonId and PAddressId from the UsersTB
    DECLARE @PersonId INT;
    DECLARE @PAddressId INT;

    SELECT @PersonId = PersonId FROM UsersTB WHERE UserId = @UserId;
    SELECT @PAddressId = PAddressId FROM PersonTB WHERE PersonId = @PersonId;

    -- Update PersonInfoTB
    UPDATE PersonInfoTB
    SET 
        Mail = @Mail,
        Tlf = @Tlf,
        CPRNumber = @CPR,
        Address = @Address,
        PostalCode = @PostalCode,
        Country = @Country,
        RoadName = @RoadName,
        HouseNumber = @HouseNumber,
        City = @City
    WHERE PAddressId = @PAddressId;

    -- Update PersonTB
    UPDATE PersonTB
    SET 
        FirstName = @FirstName,
        LastName = @LastName
    WHERE PersonId = @PersonId;

    -- Update UsersTB
    UPDATE UsersTB
    SET 
        UPassWord = @Password
    WHERE UserId = @UserId;
END;
GO

CREATE PROCEDURE DeleteUser
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Get the PersonId associated with the UserId
    DECLARE @PersonId INT;
    SELECT @PersonId = PersonId FROM UsersTB WHERE UserId = @UserId;

    -- Delete from UsersTB
    DELETE FROM UsersTB WHERE UserId = @UserId;

    -- Delete from PersonTB (CASCADE will handle the related PersonInfoTB)
    DELETE FROM PersonTB WHERE PersonId = @PersonId;
END;
GO

USE [PhoenixEnterpriseDB];
GO
CREATE USER [AddsUsers_User] FOR LOGIN [AddsUsers_User];
GO

GRANT EXECUTE TO [AddsUsers_User];
GO

-- Deny SELECT on tables
DENY SELECT ON SCHEMA::dbo TO [AddsUsers_User];
-- Deny INSERT on tables
DENY INSERT ON SCHEMA::dbo TO [AddsUsers_User];
-- Deny UPDATE on tables
DENY UPDATE ON SCHEMA::dbo TO [AddsUsers_User];
-- Deny DELETE on tables
DENY DELETE ON SCHEMA::dbo TO [AddsUsers_User];
GO