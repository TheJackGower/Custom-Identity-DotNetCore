
CREATE PROCEDURE [dbo].[identity_FindByName]
(
	@NormalizedUserName  nvarchar(256)
)
AS
BEGIN
	
SELECT * FROM [dbo].[SiteUser]
WHERE [NormalizedUserName] = @NormalizedUserName
END