CREATE PROCEDURE [dbo].[DeleteParts]
	@Part_ID int = NULL
AS
BEGIN
	DELETE FROM [dbo].[MotorParts] WHERE Part_ID = @Part_ID;
END
