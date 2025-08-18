USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'TP06')
BEGIN
    CREATE DATABASE TP06;
END
GO

USE TP06;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuario')
BEGIN
    CREATE TABLE Usuario (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(100) NOT NULL,
        Password NVARCHAR(100) NOT NULL,
        Email NVARCHAR(100) NOT NULL UNIQUE
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Tarea')
BEGIN
    CREATE TABLE Tarea (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        Titulo NVARCHAR(200) NOT NULL,
        Descripcion NVARCHAR(MAX),
        FechaDeEntrega DATETIME,
        Prioridad NVARCHAR(50)
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'UsuarioxTarea')
BEGIN
    CREATE TABLE UsuarioxTarea (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        IDTarea INT NOT NULL,
        IDUsuario INT NOT NULL,
        FOREIGN KEY (IDTarea) REFERENCES Tarea(ID),
        FOREIGN KEY (IDUsuario) REFERENCES Usuario(ID)
    );
END
GO

IF NOT EXISTS (SELECT * FROM Usuario WHERE Email = 'admin@test.com')
BEGIN
    INSERT INTO Usuario (Username, Password, Email) 
    VALUES ('Admin', '123456', 'admin@test.com');
END
GO

IF NOT EXISTS (SELECT * FROM Tarea WHERE Titulo = 'Tarea de prueba')
BEGIN
    INSERT INTO Tarea (Titulo, Descripcion, FechaDeEntrega, Prioridad) 
    VALUES ('Tarea de prueba', 'Esta es una tarea de prueba', GETDATE() + 7, 'Media');
END
GO

PRINT 'Base de datos TP06 creada exitosamente con todas las tablas necesarias.';
