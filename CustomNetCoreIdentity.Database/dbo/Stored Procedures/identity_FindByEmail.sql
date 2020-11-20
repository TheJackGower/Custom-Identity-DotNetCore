
CREATE PROCEDURE [dbo].[identity_FindByEmail]
(
	@NormalizedUserName nvarchar(256)
)
AS
BEGIN
	
SELECT * FROM [dbo].[SiteUser]
WHERE [NormalizedEmail] = @NormalizedUserName
END