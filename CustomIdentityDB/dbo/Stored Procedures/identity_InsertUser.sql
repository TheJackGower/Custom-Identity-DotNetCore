
CREATE PROCEDURE [dbo].[identity_InsertUser]
	
@Username nvarchar(256),
@NormalizedUserName nvarchar(256),
@Forename nvarchar(75),
@Surname nvarchar(75),
@Email nvarchar(256),
@NormalizedEmail nvarchar(256),
@EmailConfirmed bit,
@PasswordHash nvarchar(MAX),
@PhoneNumber nvarchar(50),
@PhoneNumberConfirmed bit,
@TwoFactorEnabled bit,
@Created datetime

AS
BEGIN
	
INSERT INTO [SiteUser] 

(
	[UserName], 
    [NormalizedUserName], 
    [Email],
    [NormalizedEmail], 
    [EmailConfirmed],
    [Forename],
    [Surname],
    [PasswordHash],
    [PhoneNumber], 
    [PhoneNumberConfirmed], 
    [TwoFactorEnabled],
	[Created]
 )

 VALUES
 (
	@Username,
	@NormalizedUserName,
	@Email,
	@NormalizedEmail,
	@EmailConfirmed,
	@Forename,
	@Surname,
	@PasswordHash,
	@PhoneNumber,
	@PhoneNumberConfirmed,
	@TwoFactorEnabled,
	@Created
 )


 SELECT CAST(SCOPE_IDENTITY() as int)

 END