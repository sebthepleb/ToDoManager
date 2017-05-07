CREATE TABLE [dbo].[tblCategory]
(
	[intCategoryId] INT NOT NULL PRIMARY KEY IDENTITY,
	[vchCategoryName] VARCHAR(255) NOT NULL,
	[dteDateCreated] DATETIME NOT NULL DEFAULT GETDATE(),
	[dteDateUpdated] DATETIME NULL,
	[vchUpdateUsername] VARCHAR(255)NOT NULL
)