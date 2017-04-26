CREATE TRIGGER [trg_tblToDo_Delete]
	ON [dbo].[tblToDo]
	FOR DELETE
	AS
	INSERT tblAuditToDo
		SELECT GETDATE(), 'D', *
		FROM DELETED