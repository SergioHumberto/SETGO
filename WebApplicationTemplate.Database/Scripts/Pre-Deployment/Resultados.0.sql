PRINT 'Resultados.0.sql...'

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
	WHERE TABLE_NAME = 'Resultados' AND CONSTRAINT_TYPE = 'FOREIGN KEY'
	AND CONSTRAINT_NAME = 'FK__Resultado__IdCar__4F47C5E3'
)
BEGIN
	ALTER TABLE Resultados 
	DROP CONSTRAINT FK__Resultado__IdCar__4F47C5E3
END

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Resultados' AND COLUMN_NAME = 'IdCarrera'
)
BEGIN
	EXEC('
		ALTER TABLE Resultados 
		DROP COLUMN [IdCarrera]
	')
END

GO

IF NOT EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Resultados' AND COLUMN_NAME = 'IdConfiguracionResultados'
)
BEGIN
	ALTER TABLE [Resultados]
	ADD IdConfiguracionResultados INT NULL
END

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Resultados' AND COLUMN_NAME = 'IdConfiguracionResultados' AND IS_NULLABLE = 'YES'
)
BEGIN
	EXEC('
		UPDATE Resultados
		SET IdConfiguracionResultados = (SELECT TOP(1) IdConfiguracionResultados FROM ConfiguracionResultados)
		WHERE IdConfiguracionResultados IS NULL
	')
END

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Resultados' AND COLUMN_NAME = 'IdConfiguracionResultados' AND IS_NULLABLE = 'YES'
)
BEGIN
	EXEC('
		ALTER TABLE [Resultados] ALTER COLUMN [IdConfiguracionResultados] INT NOT NULL
	')
END

GO

IF NOT EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
	WHERE TABLE_NAME = 'Resultados' AND CONSTRAINT_TYPE = 'FOREIGN KEY' 
	AND CONSTRAINT_NAME = 'FK_ConfiguracionResultados_Resultados'
)
BEGIN
	EXEC('
		ALTER TABLE [Resultados]
		ADD CONSTRAINT [FK_ConfiguracionResultados_Resultados]
		FOREIGN KEY (IdConfiguracionResultados)
		REFERENCES [ConfiguracionResultados](IdConfiguracionResultados)
	')
END

GO