-- 0. Create Database
CREATE DATABASE RentalSystemDB;
GO

USE RentalSystemDB;
GO

------------------------------------------------------
-- 1. Customer Table (Rent Seekers)
------------------------------------------------------
CREATE TABLE Customer (
    CustomerId INT PRIMARY KEY IDENTITY(1000,1),
    EmailId VARCHAR(50) UNIQUE NOT NULL,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    UserPassword VARCHAR(50) NOT NULL,
    ContactNumber VARCHAR(15) NOT NULL,
);

INSERT INTO Customer (EmailId, FirstName, LastName, UserPassword, ContactNumber)
VALUES 
('seeker1@example.com', 'Riya', 'Verma', 'riya@123', '9876543210'),
('seeker2@example.com', 'Aman', 'Shah', 'aman@123', '9876543211');

------------------------------------------------------
-- 2. Owner Table
------------------------------------------------------
CREATE TABLE Owner (
    OwnerId INT PRIMARY KEY IDENTITY(2000,1),
    EmailId VARCHAR(50) UNIQUE NOT NULL,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    UserPassword VARCHAR(50) NOT NULL,
    ContactNumber VARCHAR(15) NOT NULL,
);

INSERT INTO Owner (EmailId, FirstName, LastName, UserPassword, ContactNumber)
VALUES 
('owner1@example.com', 'Aditya', 'Singh', 'owner@123', '9999999999'),
('owner2@example.com', 'Neha', 'Kumar', 'neha@123', '8888888888');

------------------------------------------------------
-- 3. PropertyType Table
------------------------------------------------------
CREATE TABLE PropertyType (
    PropertyTypeId INT PRIMARY KEY IDENTITY(1,1),
    TypeName VARCHAR(50) NOT NULL UNIQUE
);

INSERT INTO PropertyType (TypeName)
VALUES ('Apartment'), ('House');

------------------------------------------------------
-- 4. FurnishingType Table
------------------------------------------------------
CREATE TABLE FurnishingType (
    FurnishingTypeId INT PRIMARY KEY IDENTITY(1,1),
    FurnishingStatus VARCHAR(50) NOT NULL UNIQUE
);

INSERT INTO FurnishingType (FurnishingStatus)
VALUES ('None'), ('Semi'), ('Fully');

------------------------------------------------------
-- 5. AvailabilityType Table
------------------------------------------------------
CREATE TABLE AvailabilityType (
    AvailabilityTypeId INT PRIMARY KEY IDENTITY(1,1),
    AvailabilityStatus VARCHAR(50) NOT NULL UNIQUE
);

INSERT INTO AvailabilityType (AvailabilityStatus)
VALUES ('Immediate'), ('Later');

------------------------------------------------------
-- 6. Properties Table
------------------------------------------------------
CREATE TABLE Properties (
    PropertyId INT PRIMARY KEY IDENTITY(3000,1),
    OwnerId INT FOREIGN KEY REFERENCES Owner(OwnerId),
    Country VARCHAR(100) NOT NULL,
    State VARCHAR(100) NOT NULL,
    City VARCHAR(100) NOT NULL,
    PropertyTypeId INT FOREIGN KEY REFERENCES PropertyType(PropertyTypeId),
    Price DECIMAL(10,2) NOT NULL,
    Bedrooms INT NOT NULL,
    Bathrooms INT NOT NULL,
    ParkingIncluded BIT NOT NULL,
    PetsAllowed BIT NOT NULL,
    FurnishingTypeId INT FOREIGN KEY REFERENCES FurnishingType(FurnishingTypeId),
    AvailabilityTypeId INT FOREIGN KEY REFERENCES AvailabilityType(AvailabilityTypeId),
    AdditionalNotes VARCHAR(500),
    Status VARCHAR(20) CHECK(Status IN ('Available', 'Rented')) DEFAULT 'Available'
);

INSERT INTO Properties (
    OwnerId, Country, State, City, PropertyTypeId, Price, Bedrooms,
    Bathrooms, ParkingIncluded, PetsAllowed, FurnishingTypeId, AvailabilityTypeId,
    AdditionalNotes, Status
)
VALUES
(2000, 'India', 'Maharashtra', 'Mumbai', 1, 25000, 2, 2, 1, 1, 2, 1, 'Near Metro Station', 'Available'),
(2000, 'India', 'Delhi', 'New Delhi', 2, 35000, 3, 2, 0, 0, 3, 2, 'Corner flat', 'Rented'),
(2001, 'India', 'Karnataka', 'Bangalore', 1, 30000, 2, 1, 1, 1, 1, 1, 'Fully furnished', 'Available');

------------------------------------------------------
-- 7. PropertyInterest Table
------------------------------------------------------
CREATE TABLE PropertyInterest (
    InterestId INT PRIMARY KEY IDENTITY(4000,1),
    PropertyId INT FOREIGN KEY REFERENCES Properties(PropertyId),
    CustomerId INT FOREIGN KEY REFERENCES Customer(CustomerId),
    SharedDate DATETIME NOT NULL,
    LastFollowUpDate DATETIME NULL,
    CONSTRAINT UC_PropertyCustomer UNIQUE (PropertyId, CustomerId)
);

-- Dummy interests
INSERT INTO PropertyInterest (PropertyId, CustomerId, SharedDate)
VALUES
(3000, 1000, GETDATE());

INSERT INTO PropertyInterest (PropertyId, CustomerId, SharedDate, LastFollowUpDate)
VALUES
(3002, 1001, GETDATE(), DATEADD(DAY, 1, GETDATE()));
