IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(450) NULL,
    [TurkishName] nvarchar(max) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Englishes] (
    [Id] int NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [Word] nvarchar(max) NULL,
    [NormalizedWord] nvarchar(450) NULL,
    [Status] tinyint NOT NULL,
    CONSTRAINT [PK_Englishes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Turkishes] (
    [Id] int NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [Word] nvarchar(max) NULL,
    [NormalizedWord] nvarchar(450) NULL,
    [Status] tinyint NOT NULL,
    CONSTRAINT [PK_Turkishes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserRefreshTokens] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [Expiration] datetime2 NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_UserRefreshTokens] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] nvarchar(450) NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [ProfilePicture] nvarchar(max) NOT NULL,
    [Level] int NOT NULL,
    [ExperiencePoints] real NOT NULL,
    [RequiredExperiencePoints] real NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UserName] nvarchar(max) NULL,
    [NormalizedUserName] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [NormalizedEmail] nvarchar(max) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CategoryEnglish] (
    [CategoriesId] int NOT NULL,
    [EnglishesId] int NOT NULL,
    CONSTRAINT [PK_CategoryEnglish] PRIMARY KEY ([CategoriesId], [EnglishesId]),
    CONSTRAINT [FK_CategoryEnglish_Categories_CategoriesId] FOREIGN KEY ([CategoriesId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CategoryEnglish_Englishes_EnglishesId] FOREIGN KEY ([EnglishesId]) REFERENCES [Englishes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [CategoryTurkish] (
    [CategoriesId] int NOT NULL,
    [TurkishesId] int NOT NULL,
    CONSTRAINT [PK_CategoryTurkish] PRIMARY KEY ([CategoriesId], [TurkishesId]),
    CONSTRAINT [FK_CategoryTurkish_Categories_CategoriesId] FOREIGN KEY ([CategoriesId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CategoryTurkish_Turkishes_TurkishesId] FOREIGN KEY ([TurkishesId]) REFERENCES [Turkishes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [EnglishTurkishTranslations] (
    [TranslationsId] int NOT NULL,
    [TranslationsId1] int NOT NULL,
    CONSTRAINT [PK_EnglishTurkishTranslations] PRIMARY KEY ([TranslationsId], [TranslationsId1]),
    CONSTRAINT [FK_EnglishTurkishTranslations_Englishes_TranslationsId] FOREIGN KEY ([TranslationsId]) REFERENCES [Englishes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EnglishTurkishTranslations_Turkishes_TranslationsId1] FOREIGN KEY ([TranslationsId1]) REFERENCES [Turkishes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserWord] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [WordId] int NOT NULL,
    [LastCorrectAnswerDate] datetime2 NOT NULL,
    [WrongAnswersCount] int NOT NULL,
    [CorrectAnswersCount] int NOT NULL,
    [UserId1] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_UserWord] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserWord_Englishes_WordId] FOREIGN KEY ([WordId]) REFERENCES [Englishes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserWord_Users_UserId1] FOREIGN KEY ([UserId1]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Categories_Name] ON [Categories] ([Name]) WHERE [Name] IS NOT NULL;
GO

CREATE INDEX [IX_CategoryEnglish_EnglishesId] ON [CategoryEnglish] ([EnglishesId]);
GO

CREATE INDEX [IX_CategoryTurkish_TurkishesId] ON [CategoryTurkish] ([TurkishesId]);
GO

CREATE UNIQUE INDEX [IX_Englishes_NormalizedWord] ON [Englishes] ([NormalizedWord]) WHERE [NormalizedWord] IS NOT NULL;
GO

CREATE INDEX [IX_EnglishTurkishTranslations_TranslationsId1] ON [EnglishTurkishTranslations] ([TranslationsId1]);
GO

CREATE UNIQUE INDEX [IX_Turkishes_NormalizedWord] ON [Turkishes] ([NormalizedWord]) WHERE [NormalizedWord] IS NOT NULL;
GO

CREATE INDEX [IX_UserWord_UserId1] ON [UserWord] ([UserId1]);
GO

CREATE INDEX [IX_UserWord_WordId] ON [UserWord] ([WordId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230416210428_UserIdentity', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [UserWord] DROP CONSTRAINT [FK_UserWord_Users_UserId1];
GO

DROP INDEX [IX_UserWord_UserId1] ON [UserWord];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserWord]') AND [c].[name] = N'UserId1');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [UserWord] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [UserWord] DROP COLUMN [UserId1];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserWord]') AND [c].[name] = N'UserId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [UserWord] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [UserWord] ALTER COLUMN [UserId] nvarchar(450) NOT NULL;
GO

CREATE INDEX [IX_UserWord_UserId] ON [UserWord] ([UserId]);
GO

ALTER TABLE [UserWord] ADD CONSTRAINT [FK_UserWord_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230416213457_UserWordRelations', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'UserName');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Users] ALTER COLUMN [UserName] nvarchar(450) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Email');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Users] ALTER COLUMN [Email] nvarchar(450) NULL;
GO

CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]) WHERE [Email] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_Users_UserName] ON [Users] ([UserName]) WHERE [UserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230622170644_user_properties_updated', N'7.0.2');
GO

COMMIT;
GO

