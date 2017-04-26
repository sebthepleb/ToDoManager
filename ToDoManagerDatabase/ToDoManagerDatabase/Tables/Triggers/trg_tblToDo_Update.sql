CREATE TRIGGER [trg_tblToDo_Update]
	ON [dbo].[tblToDo]
	FOR UPDATE
	AS
	INSERT tblAuditToDo
		SELECT GETDATE(), 'U', *
		FROM INSERTED