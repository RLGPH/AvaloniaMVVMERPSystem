USE [PhoenixEnterpriseDB];
GO

CREATE PROCEDURE CreateEmployee
    @FirstName VARCHAR(28),
    @LastName VARCHAR(28),
    @Mail VARCHAR(60),
    @Tlf VARCHAR(14),
    @CPRNumber VARCHAR(20),
    @Adress VARCHAR(100),
    @PostalCode VARCHAR(10),
    @Road VARCHAR(100),
    @COUNTRY VARCHAR(50),
    @EmplyeePassword VARCHAR(50),
    @Title VARCHAR(50),
    @WorkMail VARCHAR(60),
    @WorkTlf VARCHAR(14),
    @AdminPassword VARCHAR(50) = NULL, -- Nullable for non-admin employees
    @IsAdmin BIT,
    @IsMod BIT
AS
BEGIN
    SET NOCOUNT ON;

    -- Step 1: Insert data into PersonInfoTB
    DECLARE @PersonInfoId INT;
    INSERT INTO PersonInfoTB (Mail, Tlf, CPRNumber, Adress, PostalCode, Road, COUNTRY)
    VALUES (@Mail, @Tlf, @CPRNumber, @Adress, @PostalCode, @Road, @COUNTRY);
    SET @PersonInfoId = SCOPE_IDENTITY();

    -- Step 2: Insert data into PersonTB
    DECLARE @PersonId INT;
    INSERT INTO PersonTB (FirstName, LastName)
    VALUES (@FirstName, @LastName);
    SET @PersonId = SCOPE_IDENTITY();

    -- Step 3: Insert data into EmployeeTB
    DECLARE @EmployeeId INT;
    INSERT INTO EmployeeTB (EmplyeePassword, Tital, WorkMail, WorkTlf, AdminPassword, PersonId)
    VALUES (@EmplyeePassword, @Title, @WorkMail, @WorkTlf, @AdminPassword, @PersonId);
    SET @EmployeeId = SCOPE_IDENTITY();

    -- Step 4: Insert into AdminTB and ModeratorTB with respective values
    INSERT INTO AdminTB (IsAdmin, EmployeeId)
    VALUES (@IsAdmin, @EmployeeId);

    INSERT INTO ModeratorTB (IsMod, EmployeeId)
    VALUES (@IsMod, @EmployeeId);
END;
GO

CREATE PROCEDURE GetEmployeeByName
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50)
AS
BEGIN
    SELECT e.EmployeeId, e.EmployeePassword, e.Title, e.WorkMail, e.WorkTlf,
           p.PersonId, p.FirstName, p.LastName, p.CPRNumber, p.PersonInfoId,
           pi.Mail, pi.Tlf, pi.Address, pi.PostalCode, pi.RoadName, pi.HouseNumber,
           pi.City, pi.Country,
           a.AdminId, a.IsAdmin,
           m.ModeratorId, m.IsMod
    FROM Employees e
    INNER JOIN Persons p ON e.PersonId = p.PersonId
    INNER JOIN PersonaLInfo pi ON p.PersonInfoId = pi.PersonalInfoId
    LEFT JOIN Admins a ON e.EmployeeId = a.AdminId
    LEFT JOIN Moderators m ON e.EmployeeId = m.ModeratorId
    WHERE p.FirstName = @FirstName AND p.LastName = @LastName;
END
