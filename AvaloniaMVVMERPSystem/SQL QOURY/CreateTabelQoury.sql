DROP DATABASE IF EXISTS PhoenixEnterpriseDB;
GO

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