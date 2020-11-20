
CREATE PROCEDURE [dbo].[identity_FindRoleByName]
(
	@NormalizedRoleName nvarchar(256)
)
AS
BEGIN
	
SELECT TOP 1 * FROM [dbo].[SiteRole]
WHERE [NormalizedName] = @NormalizedRoleName
END