DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Suppliers]') AND [c].[name] = N'PlaceOfReception');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Suppliers] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Suppliers] DROP COLUMN [PlaceOfReception];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Suppliers]') AND [c].[name] = N'TrustLevel');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Suppliers] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Suppliers] DROP COLUMN [TrustLevel];

GO

ALTER TABLE [Suppliers] ADD [IdConfidenceLevel] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Suppliers] ADD [IdOperatingBase] int NOT NULL DEFAULT 0;

GO

CREATE TABLE [ConfidenceLevels] (
    [Id] int NOT NULL IDENTITY,
    [Key] nvarchar(max) NULL,
    [CreationDate] datetime2 NOT NULL,
    [ConfidenceLevelName] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ConfidenceLevels] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [OperatingBases] (
    [Id] int NOT NULL IDENTITY,
    [Key] nvarchar(max) NULL,
    [CreationDate] datetime2 NOT NULL,
    [OperatingBaseName] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_OperatingBases] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [SpecificationTypes] (
    [Id] int NOT NULL IDENTITY,
    [Key] nvarchar(max) NULL,
    [CreationDate] datetime2 NOT NULL,
    [SpecificationTypeName] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_SpecificationTypes] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Specifications] (
    [Id] int NOT NULL IDENTITY,
    [Key] nvarchar(max) NULL,
    [CreationDate] datetime2 NOT NULL,
    [SpecificationName] nvarchar(max) NULL,
    [IdSpecificationType] int NOT NULL,
    [IsActive] bit NOT NULL,
    [SpecificationTypeId] int NULL,
    CONSTRAINT [PK_Specifications] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Specifications_SpecificationTypes_SpecificationTypeId] FOREIGN KEY ([SpecificationTypeId]) REFERENCES [SpecificationTypes] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Suppliers_IdConfidenceLevel] ON [Suppliers] ([IdConfidenceLevel]);

GO

CREATE INDEX [IX_Suppliers_IdOperatingBase] ON [Suppliers] ([IdOperatingBase]);

GO

CREATE INDEX [IX_Specifications_SpecificationTypeId] ON [Specifications] ([SpecificationTypeId]);

GO

ALTER TABLE [Suppliers] ADD CONSTRAINT [FK_Suppliers_ConfidenceLevels_IdConfidenceLevel] FOREIGN KEY ([IdConfidenceLevel]) REFERENCES [ConfidenceLevels] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [Suppliers] ADD CONSTRAINT [FK_Suppliers_OperatingBases_IdOperatingBase] FOREIGN KEY ([IdOperatingBase]) REFERENCES [OperatingBases] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200511072521_AddNewCatalogs', N'3.1.3');

GO

