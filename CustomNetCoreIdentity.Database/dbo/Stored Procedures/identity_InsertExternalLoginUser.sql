
CREATE PROCEDURE [dbo].[identity_InsertExternalLoginUser]
	
@UserId int,
@LoginProvider nvarchar(450),
@ProviderKey nvarchar(450),
@ProviderDisplayName nvarchar(450),
@Created datetime

AS
BEGIN
	
INSERT INTO [dbo].[ExternalLogin]

(
	[UserId],
    [LoginProvider], 
    [ProviderKey],
    [ProviderDisplayName], 
    [Created]
 )

 VALUES
 (
	@UserId,
	@LoginProvider,
	@ProviderKey,
	@ProviderDisplayName,
	@Created
 )
 
 SELECT CAST(SCOPE_IDENTITY() as int)

 END