
CREATE PROCEDURE [dbo].[identity_FindById]
	(
@Id int
)
AS
BEGIN
	
SELECT * FROM [dbo].[SiteUser]
WHERE Id = @Id

END