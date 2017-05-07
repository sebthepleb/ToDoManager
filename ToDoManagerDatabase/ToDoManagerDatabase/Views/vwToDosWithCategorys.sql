CREATE VIEW [dbo].[vwToDosWithCategorys]
	AS 
	SELECT t.intToDoId, t.vchTitle, t.vchDetail, ISNULL(c.vchCategoryName, 'None') vchCategoryName, t.dteDateCreated, t.dteDateUpdated, t.vchUpdateUsername
	FROM [tblToDo] t
	LEFT JOIN [tblCategoryToDo] ct
		ON t.intToDoId = ct.intToDoId
	LEFT JOIN [tblCategory] c
		ON ct.intCategoryId = c.intCategoryId