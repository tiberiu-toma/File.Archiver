CREATE TABLE [dbo].[Archive]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FileName] NVARCHAR(50) NOT NULL, 
    [TimeStamp] DATETIME NOT NULL, 
    [OperationDuration] TIME NOT NULL, 
    [OperationStatus] BIT NOT NULL
)
