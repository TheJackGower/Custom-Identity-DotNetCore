
CREATE PROCEDURE [dbo].[identity_FindByExternalLogin]
(
	@LoginProvider  nvarchar(450),
	@ProviderKey    nvarchar(450)
)
AS
BEGIN
	
SELECT UserID FROM [ExternalLogin]
WHERE LoginProvider = @LoginProvider 
AND ProviderKey = @ProviderKey

END