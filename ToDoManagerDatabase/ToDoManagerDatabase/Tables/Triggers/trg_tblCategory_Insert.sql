CREATE TRIGGER [trg_tblCategory_Insert]
	ON [dbo].[tblCategory]
	FOR INSERT
	AS
	SET NOCOUNT ON
	INSERT tblAuditCategory
		SELECT GETDATE(), 'I', *
		FROM INSERTED