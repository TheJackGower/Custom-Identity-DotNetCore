CREATE TABLE [dbo].[SiteUserRole] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_SiteUserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[SiteRole] ([Id]),
    CONSTRAINT [FK_SiteUserRole_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SiteUser] ([Id])
);

