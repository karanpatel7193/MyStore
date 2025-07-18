/*
This SP delete record from table Role
Created By :: Rekansh Patel
Created On :: 04/23/2017
*/	
CREATE PROCEDURE [dbo].[Property_Delete]
	@Id int
AS
BEGIN
	DELETE FROM [Property] 
	WHERE [Id] = @Id;
END

