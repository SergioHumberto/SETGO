PRINT 'InformacionPago.1.sql...'

GO

IF NOT EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'ParticipanteXCarrera' AND COLUMN_NAME = 'FechaRegistro'
)
BEGIN
	ALTER TABLE ParticipanteXCarrera
	ADD FechaRegistro DATETIME NULL
END

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'ParticipanteXCarrera' AND COLUMN_NAME = 'FechaRegistro' AND IS_NULLABLE = 'YES'
)
BEGIN
	-- Le asigna la fecha de registro de la tabla de participante
	EXEC('
		UPDATE ParticipanteXCarrera
		SET FechaRegistro = P.FechaRegistro
		FROM ParticipanteXCarrera PXC, Participante P
		WHERE PXC.IdParticipante = P.IdParticipante
	')
END

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'ParticipanteXCarrera' AND COLUMN_NAME = 'FechaRegistro' AND IS_NULLABLE = 'YES'
)
BEGIN
	EXEC('
		ALTER TABLE ParticipanteXCarrera
		ALTER COLUMN FechaRegistro DATETIME NOT NULL
	')
END