
IF NOT EXISTS
(
	SELECT *
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE COLUMN_NAME = 'FechaNacimiento' AND TABLE_NAME = 'Participante'
)
BEGIN 
	ALTER TABLE [Participante] ADD FechaNacimiento DATE NULL
END 
GO


IF EXISTS(
SELECT *
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Participante' AND COLUMN_NAME = 'FechaNacimiento' AND IS_NULLABLE = 'YES'
)
BEGIN 
	DECLARE @DateDefault DATE = '01-01-0001'

	UPDATE [Participante] 
	SET FechaNacimiento = @DateDefault
	WHERE FechaNacimiento IS NULL

END 
GO

IF EXISTS(
SELECT *
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE COLUMN_NAME = 'FechaNacimiento' AND TABLE_NAME = 'Participante' AND IS_NULLABLE = 'YES'
)
BEGIN 
	ALTER TABLE [Participante] ALTER COLUMN [FechaNacimiento] DATE NOT NULL
END 
GO