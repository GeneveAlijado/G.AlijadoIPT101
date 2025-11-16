CREATE PROCEDURE [dbo].[CreateParts]
	@Part_ID INT = NULL, 
    @Part_Name NCHAR(100) = NULL, 
    @Part_Number NCHAR(100) = NULL, 
    @Descriptions NCHAR(100) = NULL, 
    @Price NCHAR(100) = NULL
AS
 BEGIN

  INSERT INTO [dbo].[MotorParts] ([Part_ID], [Part_Name],[Part_Number], [Descriptions], [Price])
  VALUES (@Part_ID,@Part_Name,@Part_Number,@Descriptions,@Price);
 END

