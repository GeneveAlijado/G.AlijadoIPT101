CREATE TABLE [dbo].[MotorParts]
(
	[Part_ID] INT  IDENTITY (1,1) NOT NULL PRIMARY KEY, 
    [Part_Name] NCHAR(100) NULL, 
    [Part_Number] NCHAR(100) NULL, 
    [Descriptions] NCHAR(100) NULL, 
    [Price] NCHAR(100) NULL
)
