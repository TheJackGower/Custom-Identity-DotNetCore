
CREATE PROCEDURE [dbo].[identity_UpdateRole]
(
	@Id int,
	@Name nvarchar(256),
	@NormalizedName nvarchar(256),
	@Description nvarchar(256)
)

AS
BEGIN
	
UPDATE [SiteRole] SET

    [Name] = @Name,
    [NormalizedName] = @NormalizedName,
    [Description] = @Description

WHERE Id = @Id

END