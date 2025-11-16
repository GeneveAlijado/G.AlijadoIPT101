CREATE PROCEDURE [dbo].[GetParts]
	@Part_ID int = NULL
AS
BEGIN
SELECT * FROM [dbo].[MotorParts] AS a WHERE a.[Part_ID] = @Part_ID;
END