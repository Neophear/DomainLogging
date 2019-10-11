CREATE TABLE [dbo].[Log]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TimeStamp] DATETIME NOT NULL DEFAULT getdate(), 
    [ComputerRefId] INT NULL, 
    [SerialNumber] VARCHAR(50) NULL, 
    [Username] NVARCHAR(50) NOT NULL, 
    [Parameters] NVARCHAR(255) NOT NULL
)
