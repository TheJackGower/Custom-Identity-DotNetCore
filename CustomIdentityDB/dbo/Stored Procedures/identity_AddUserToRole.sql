
CREATE PROCEDURE [dbo].[identity_AddUserToRole]
	
@RoleId int,
@UserId int

AS
BEGIN

IF NOT EXISTS
(
	SELECT 1 FROM [SiteUserRole] 
	WHERE [UserId] = @UserId
	AND [RoleId] = @RoleId
)

INSERT INTO [SiteUserRole]
(
	[UserId], 
	[RoleId]
) 
VALUES
(
	@UserId,
	@RoleId
)

 END