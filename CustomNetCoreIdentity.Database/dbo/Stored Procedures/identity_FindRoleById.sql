
CREATE PROCEDURE [dbo].[identity_FindRoleById]
(
	@Id int
)
AS
BEGIN
	
SELECT TOP 1 * FROM [dbo].[SiteRole]
WHERE [Id] = @Id

END