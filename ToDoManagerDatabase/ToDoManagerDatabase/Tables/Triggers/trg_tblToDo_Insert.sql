CREATE TRIGGER [trg_tblToDo_Insert]
	ON [dbo].[tblToDo]
	FOR INSERT
	AS
	INSERT tblAuditToDo
		SELECT GETDATE(), 'I', *
		FROM INSERTED