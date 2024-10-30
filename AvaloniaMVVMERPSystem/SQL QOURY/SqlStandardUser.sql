USE [master];
GO
CREATE LOGIN [AddsUsers_User] WITH PASSWORD = 'Password123!';
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

EXEC CreateEmployee 
    @FirstName = 'AddsUsers',
    @LastName = 'User',
    @Mail = 'addsusers@example.com',
    @Tlf = '12345678',
    @CPRNumber = '121212-1212',
    @Address = '123 Admin St',
    @PostalCode = '1000',
    @RoadName = 'Admin Road',
    @HouseNumber = '42',
    @City = 'Admin City',
    @Country = 'Admin Country',
    @EmployeePassword = 'Password123!',
    @Title = 'Administrator',
    @WorkMail = 'admin@example.com',
    @WorkTlf = '87654321',
    @AdminPassword = 'AdminPassword123!',
    @IsAdmin = 1,
    @IsMod = 0;
GO
