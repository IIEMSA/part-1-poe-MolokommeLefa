--Database creation
Use master
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'EventBaseSystem')

 DROP DATABASE EventBaseSystem
 CREATE DATABASE EventBaseSystem

 USE EventBaseSystem

 --Table creation

 CREATE TABLE Venue (

VenueID int identity(1,1) Primary key,
Name NvarCHAR(255) NOT NULL,
Location nvarchar(255) NOT NULL,
Capacity int NOT NULL,
ImageURL nvarchar(MAX),
CreatedAt DateTime Default GetDate()


);


CREATE TABLE Event (
    EventID INT IDENTITY(1,1) PRIMARY KEY,
    EventName NVARCHAR(255) NOT NULL,
    Description TEXT,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    VenueID INT FOREIGN KEY REFERENCES Venue(VenueID) ON DELETE CASCADE,
    CreatedAt DATETIME DEFAULT GETDATE()
	
);

CREATE TABLE Booking (
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
    VenueID INT FOREIGN KEY REFERENCES Venue(VenueID) ON DELETE CASCADE,
    EventID INT FOREIGN KEY REFERENCES Event(EventID), 
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    CreatedBy NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);



--table insertion
INSERT INTO Venue (Name, Location, Capacity, ImageURL)
VALUES 
    ('Grand Hall', '123 Main St, City Center', 500, 'grandhall.jpg');

	INSERT INTO Event (EventName, Description, StartDate, EndDate, VenueID)
VALUES 
    ('Tech Conference 2025', 'Annual technology meetup.', '2025-06-10 09:00:00', '2025-06-10 18:00:00', 1);

	INSERT INTO Booking (VenueID, EventID, StartDate, EndDate, CreatedBy)
VALUES 
    (1, 1, '2025-06-10 09:00:00', '2025-06-10 18:00:00', 'JohnDoe');