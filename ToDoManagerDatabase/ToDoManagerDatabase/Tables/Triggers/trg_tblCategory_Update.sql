CREATE TRIGGER [trg_tblCategory_Update]
	ON [dbo].[tblCategory]
	FOR UPDATE
	AS
	SET NOCOUNT ON
	INSERT tblAuditCategory
		SELECT GETDATE(), 'U', *
		FROM INSERTED