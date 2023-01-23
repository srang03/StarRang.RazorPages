CREATE TABLE [dbo].[RolesType] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (MAX) NOT NULL,
    [Active] BIT            NOT NULL,
    CONSTRAINT [PK_RolesType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

