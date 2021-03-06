﻿PRINT 'Participante.2.sql...'

GO

IF NOT EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Participante' AND COLUMN_NAME = 'FechaRegistro'
)
BEGIN
	ALTER TABLE [Participante] ADD FechaRegistro DATETIME NULL
END

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Participante' AND COLUMN_NAME = 'FechaRegistro' AND IS_NULLABLE = 'YES'
)
BEGIN 
	DECLARE @FechaDefault DATETIME = '2000-01-01 00:00:00.000'

	UPDATE [Participante] 
	SET FechaRegistro = @FechaDefault
	WHERE FechaRegistro IS NULL
END

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Participante' AND COLUMN_NAME = 'FechaRegistro' AND IS_NULLABLE = 'YES'
)
BEGIN	
	ALTER TABLE [Participante] ALTER COLUMN [FechaRegistro] DATETIME NOT NULL
END

GO

-- Fecha de Pago

IF NOT EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Participante' AND COLUMN_NAME = 'FechaPago'
)
BEGIN
	ALTER TABLE [Participante] ADD FechaPago DATETIME NULL
END

GO