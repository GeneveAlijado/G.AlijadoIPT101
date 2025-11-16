CREATE PROCEDURE [dbo].[UpdateParts]
	@Part_ID INT = NULL, 
    @Part_Name NCHAR(100) = NULL, 
    @Part_Number NCHAR(100) = NULL, 
    @Descriptions NCHAR(100) = NULL, 
    @Price NCHAR(100) = NULL
AS
	BEGIN
    UPDATE AlijadoDatabase
    SET 
    [Part_Name] = @Part_Name,
    [Part_Number] = @Part_Number,
    [Descriptions] = @Descriptions,
    [Price] = @Price
    WHERE [Part_ID] = @Part_ID;

    END

