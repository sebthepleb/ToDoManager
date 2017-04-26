CREATE TABLE [dbo].[tblAuditToDo]
(
	[dteAuditDateCreated] DATETIME NOT NULL,
	[chrAuditAction] CHAR NOT NULL,
	[intToDoId] INT NOT NULL,
	[vchTitle] VARCHAR(255) NOT NULL,
	[vchDetail] VARCHAR(MAX) NULL,
	[dteDateCreated] DATETIME NOT NULL,
	[dteDateUpdated] DATETIME NULL,
	[vchUpdateUsername] VARCHAR(255) NOT NULL
)