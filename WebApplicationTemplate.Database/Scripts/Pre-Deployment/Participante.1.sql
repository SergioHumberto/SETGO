﻿PRINT 'Participante.1.sql...'
	
IF exists (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'Pagado' AND TABLE_NAME = 'Participante'  AND IS_NULLABLE = 'NO')
BEGIN	
	ALTER TABLE Participante
	ALTER COLUMN Pagado BIT NULL
END
GO
IF exists (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'Pagado' AND TABLE_NAME = 'Participante'  AND IS_NULLABLE = 'YES')
BEGIN	
	EXEC(
	'UPDATE Participante
	SET Pagado = null'
	)
END
GO
IF exists (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'Pagado' AND TABLE_NAME = 'Participante'  AND IS_NULLABLE = 'YES')
BEGIN	
EXEC(
	'ALTER TABLE Participante
	DROP COLUMN Pagado'
	)
END
GO