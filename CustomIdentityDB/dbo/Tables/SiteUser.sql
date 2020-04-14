CREATE TABLE [dbo].[SiteUser] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [Username]             NVARCHAR (256) NOT NULL,
    [Forename]             NVARCHAR (75)  NOT NULL,
    [Surname]              NVARCHAR (75)  NULL,
    [NormalizedUserName]   NVARCHAR (256) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [NormalizedEmail]      NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (50)  NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [Created]              DATETIME       NOT NULL,
    CONSTRAINT [PK__Applicat__3214EC079083AE7D] PRIMARY KEY CLUSTERED ([Id] ASC)
);



