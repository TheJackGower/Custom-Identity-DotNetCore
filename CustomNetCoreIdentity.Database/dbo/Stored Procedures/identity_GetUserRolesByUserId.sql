
CREATE PROCEDURE [dbo].[identity_GetUserRolesByUserId]
(
	@UserId int
)
AS
BEGIN

SELECT r.[Name] FROM [SiteRole] r 
INNER JOIN [SiteUserRole] ur ON ur.[RoleId] = r.Id 
WHERE ur.UserId = @UserId

END