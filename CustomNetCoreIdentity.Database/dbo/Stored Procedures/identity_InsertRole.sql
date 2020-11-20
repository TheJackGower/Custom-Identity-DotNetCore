
CREATE PROCEDURE [dbo].[identity_InsertRole]
	
@Name nvarchar(256),
@NormalizedName nvarchar(256),
@Description nvarchar(256),
@Created datetime

AS
BEGIN
	
INSERT INTO [SiteRole] 
(
	[Name], 
    [NormalizedName], 
    [Description],
	[Created]
 )
 VALUES
 (
	@Name,
	@NormalizedName,
	@Description,
	@Created 
 )


 SELECT CAST(SCOPE_IDENTITY() as int)

 END