
CREATE PROCEDURE [dbo].[identity_IsUserInRole]
	
@RoleId int,
@UserId int

AS
BEGIN

SELECT COUNT(*) FROM [SiteUserRole] 
WHERE [UserId] = @UserId 
AND [RoleId] = @RoleId

END