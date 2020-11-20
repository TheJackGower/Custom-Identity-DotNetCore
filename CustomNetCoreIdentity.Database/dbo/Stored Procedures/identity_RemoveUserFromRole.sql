
CREATE PROCEDURE [dbo].[identity_RemoveUserFromRole]
	
@RoleId int,
@UserId int

AS
BEGIN

DELETE FROM [SiteUserRole] 
WHERE [UserId] = @UserId
AND [RoleId] = @RoleId

END