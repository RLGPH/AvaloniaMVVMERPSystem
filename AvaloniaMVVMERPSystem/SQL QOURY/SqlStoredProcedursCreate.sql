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
