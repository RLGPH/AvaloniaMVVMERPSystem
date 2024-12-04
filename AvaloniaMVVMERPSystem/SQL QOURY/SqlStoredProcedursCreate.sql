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
    INSERT INTO EmployeeTB (EmployeePassword, Title, WorkMail, WorkTlf, AdminPassword, PersonId)
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

-- Create the GetAllLocations stored procedure
CREATE PROCEDURE GetAllLocations
AS
BEGIN
    SELECT LocationId, LocationName, LCountry, LCity, LStreet, LZipCode, StorageSpaceLeft
    FROM Location;
END;
GO

CREATE PROCEDURE AddItems
    @ItemName NVARCHAR(255),
    @Description NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Item (ItemName, ItemDescription)
    VALUES (@ItemName, @Description);

    -- Return the ID of the newly created item
    SELECT SCOPE_IDENTITY() AS NewItemId;
END;
GO


CREATE PROCEDURE AddCombinedLocation
    @LocationId INT,
    @ItemId INT
AS
BEGIN
    INSERT INTO CombinedItemLocation (LocationId, ItemId)
    VALUES (@LocationId, @ItemId);
END;
GO

CREATE PROCEDURE AddLocation
    @LocationName NVARCHAR(255),
    @LCountry NVARCHAR(255),
    @LStreet NVARCHAR(255),
    @LCity NVARCHAR(255),
    @LZipCode NVARCHAR(50),
    @StorageSpaceLeft FLOAT
AS
BEGIN
    INSERT INTO Location (LocationName, LCountry, LStreet, LCity, LZipCode, StorageSpaceLeft)
    VALUES (@LocationName, @LCountry, @LStreet, @LCity, @LZipCode, @StorageSpaceLeft);
END;


