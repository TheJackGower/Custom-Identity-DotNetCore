
CREATE PROCEDURE [dbo].[identity_GetUsersInRoleByRoleName]
(
	@NormalizedRoleName nvarchar(256)
)

AS
BEGIN

SELECT u.* FROM [SiteUser] u
INNER JOIN [SiteUserRole] ur ON ur.[UserId] = u.[Id] 
INNER JOIN [SiteRole] r ON r.[Id] = ur.[RoleId] 
WHERE r.[NormalizedName] = @NormalizedRoleName

END