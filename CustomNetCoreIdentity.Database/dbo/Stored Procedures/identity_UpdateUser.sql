
CREATE PROCEDURE [dbo].[identity_UpdateUser]
(
	@Id int,
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
	@TwoFactorEnabled bit
)

AS
BEGIN
	
UPDATE [SiteUser] SET

	[UserName] = @Username,
    [NormalizedUserName] = @NormalizedUserName,
    [Email] = @Email,
    [NormalizedEmail] = @NormalizedEmail, 
    [EmailConfirmed] =@EmailConfirmed,
    [Forename] = @Forename,
    [Surname] = @Surname,
    [PasswordHash] = @PasswordHash,
    [PhoneNumber] = @PhoneNumber,
    [PhoneNumberConfirmed] = @PhoneNumberConfirmed,
    [TwoFactorEnabled] = @TwoFactorEnabled 

WHERE Id = @Id

END