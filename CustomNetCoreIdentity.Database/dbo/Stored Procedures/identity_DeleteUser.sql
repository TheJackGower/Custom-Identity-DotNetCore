
CREATE PROCEDURE [dbo].[identity_DeleteUser]
	
@Id int

AS
BEGIN
	
	DELETE FROM [SiteUser] WHERE [Id] = @Id

 END