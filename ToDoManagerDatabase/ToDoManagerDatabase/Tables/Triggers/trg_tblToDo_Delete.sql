﻿CREATE TRIGGER [trg_tblToDo_Delete]
	ON [dbo].[tblToDo]
	FOR DELETE
	AS
	SET NOCOUNT ON
	INSERT tblAuditToDo
		SELECT GETDATE(), 'D', *
		FROM DELETED