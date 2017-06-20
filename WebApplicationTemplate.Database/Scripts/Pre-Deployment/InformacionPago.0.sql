PRINT 'InformacionPago.0.sql...'

GO

-----------------------------------------------------------------------------------------------------
-- Agrega las columnas a la tabla de ParticipantesXCarrera --
IF NOT EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'ParticipanteXCarrera' AND COLUMN_NAME = 'TransactionNumber'
)
BEGIN
	ALTER TABLE ParticipanteXCarrera
	ADD TransactionNumber VARCHAR(MAX) NULL
END

GO

IF NOT EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'ParticipanteXCarrera' AND COLUMN_NAME = 'StatusPaypal'
)
BEGIN
	ALTER TABLE ParticipanteXCarrera
	ADD StatusPaypal VARCHAR(MAX) NULL
END

GO

IF NOT EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'ParticipanteXCarrera' AND COLUMN_NAME = 'Folio'
)
BEGIN
	ALTER TABLE ParticipanteXCarrera
	ADD Folio INT NULL
END

GO

IF NOT EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'ParticipanteXCarrera' AND COLUMN_NAME = 'FechaPago'
)
BEGIN
	ALTER TABLE ParticipanteXCarrera
	ADD FechaPago DATETIME NULL
END

GO

IF NOT EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'ParticipanteXCarrera' AND COLUMN_NAME = 'FolioOffline'
)
BEGIN
	ALTER TABLE ParticipanteXCarrera
	ADD FolioOffline VARCHAR(255) NULL
END
-----------------------------------------------------------------------------------------------------

GO

-----------------------------------------------------------------------------------------------------
-- Cambia los datos (si los hay) de Participante a ParticipanteXCarrera --
IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Participante' AND COLUMN_NAME = 'TransactionNumber'
)
AND EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'ParticipanteXCarrera' AND COLUMN_NAME = 'TransactionNumber'
)
BEGIN
	EXEC('
		UPDATE ParticipanteXCarrera
		SET TransactionNumber = P.TransactionNumber
		FROM ParticipanteXCarrera PXC, Participante P
		WHERE PXC.IdParticipante = P.IdParticipante
	')
END

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Participante' AND COLUMN_NAME = 'StatusPaypal'
)
AND EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'ParticipanteXCarrera' AND COLUMN_NAME = 'StatusPaypal'
)
BEGIN
	EXEC('
		UPDATE ParticipanteXCarrera
		SET StatusPaypal = P.StatusPaypal
		FROM ParticipanteXCarrera PXC, Participante P
		WHERE PXC.IdParticipante = P.IdParticipante
	')
END

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Participante' AND COLUMN_NAME = 'Folio'
)
AND EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'ParticipanteXCarrera' AND COLUMN_NAME = 'Folio'
)
BEGIN
	EXEC('
		UPDATE ParticipanteXCarrera
		SET Folio = P.Folio
		FROM ParticipanteXCarrera PXC, Participante P
		WHERE PXC.IdParticipante = P.IdParticipante
	')
END

GO

-- Se cambia el campo Folio a NOT NULL ya que en este punto ya se le asignó un dato
IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'ParticipanteXCarrera' AND COLUMN_NAME = 'Folio' AND IS_NULLABLE = 'YES'
)
BEGIN
	ALTER TABLE ParticipanteXCarrera ALTER COLUMN Folio INT NOT NULL
END

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Participante' AND COLUMN_NAME = 'FechaPago'
)
AND EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'ParticipanteXCarrera' AND COLUMN_NAME = 'FechaPago'
)
BEGIN
	EXEC('
		UPDATE ParticipanteXCarrera
		SET FechaPago = P.FechaPago
		FROM ParticipanteXCarrera PXC, Participante P
		WHERE PXC.IdParticipante = P.IdParticipante
	')
END

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Participante' AND COLUMN_NAME = 'FolioOffline'
)
AND EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'ParticipanteXCarrera' AND COLUMN_NAME = 'FolioOffline'
)
BEGIN
	EXEC('
		UPDATE ParticipanteXCarrera
		SET FolioOffline = P.FolioOffline
		FROM ParticipanteXCarrera PXC, Participante P
		WHERE PXC.IdParticipante = P.IdParticipante
	')
END
-----------------------------------------------------------------------------------------------------

GO

-----------------------------------------------------------------------------------------------------
-- Eliminar las columnas de la tabla Paricipante --
IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Participante' AND COLUMN_NAME = 'TransactionNumber'
)
BEGIN
	EXEC('
		ALTER TABLE Participante
		DROP COLUMN TransactionNumber
	')
END

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Participante' AND COLUMN_NAME = 'StatusPaypal'
)
BEGIN
	EXEC('
		ALTER TABLE Participante
		DROP COLUMN StatusPaypal
	')
END

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Participante' AND COLUMN_NAME = 'Folio'
)
BEGIN
	EXEC('
		ALTER TABLE Participante
		DROP COLUMN Folio
	')
END

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Participante' AND COLUMN_NAME = 'FechaPago'
)
BEGIN
	EXEC('
		ALTER TABLE Participante
		DROP COLUMN FechaPago
	')
END

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Participante' AND COLUMN_NAME = 'FolioOffline'
)
BEGIN
	EXEC('
		ALTER TABLE Participante
		DROP COLUMN FolioOffline
	')
END
-----------------------------------------------------------------------------------------------------