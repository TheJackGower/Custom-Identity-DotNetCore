CREATE TABLE [dbo].[SiteRole] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (256) NOT NULL,
    [NormalizedName] NVARCHAR (256) NOT NULL,
    [Description]    NVARCHAR (256) NOT NULL,
    [Created]        DATETIME       NOT NULL,
    CONSTRAINT [PK__SiteRole__3214EC076D185A2A] PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_SiteRole_NormalizedName]
    ON [dbo].[SiteRole]([NormalizedName] ASC);

