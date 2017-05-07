CREATE TABLE [dbo].[tblAuditCategory]
(
	[dteAuditDateCreated] DATETIME NOT NULL,
	[chrAuditAction] CHAR NOT NULL,
	[intCategoryId] INT NOT NULL,
	[vchCategoryName] VARCHAR(255) NOT NULL,
	[dteDateCreated] DATETIME NOT NULL DEFAULT GETDATE(),
	[dteDateUpdated] DATETIME NULL,
	[vchUpdateUsername] VARCHAR(255) NOT NULL
)