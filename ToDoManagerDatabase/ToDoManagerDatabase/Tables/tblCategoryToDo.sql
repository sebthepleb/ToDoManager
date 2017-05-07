CREATE TABLE [dbo].[tblCategoryToDo]
(
	[intToDoId] INT NOT NULL,
	[intCategoryId] INT NOT NULL,
	[dteDateCreated] DATETIME NOT NULL DEFAULT GETDATE(),
	[dteDateUpdated] DATETIME NULL,
	[vchUpdateUsername] VARCHAR(255) NOT NULL,
	FOREIGN KEY ([intToDoId]) REFERENCES tblToDo([intToDoId]),
	FOREIGN KEY ([intCategoryId]) REFERENCES tblCategory([intCategoryId]),
	UNIQUE([intToDoId], [intCategoryId])
)