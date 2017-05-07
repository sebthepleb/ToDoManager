CREATE TRIGGER [trg_tblCategory_Delete]
	ON [dbo].[tblCategory]
	FOR DELETE
	AS
	SET NOCOUNT ON
	INSERT tblAuditCategory
		SELECT GETDATE(), 'D', *
		FROM DELETED