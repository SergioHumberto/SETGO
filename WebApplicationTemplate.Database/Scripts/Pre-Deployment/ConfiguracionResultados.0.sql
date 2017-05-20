PRINT 'ConfiguracionResultados.0.sql...'

GO

IF NOT EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'ConfiguracionResultados' AND COLUMN_NAME = 'IdCategoria'
)
BEGIN
	ALTER TABLE [ConfiguracionResultados]
	ADD IdCategoria INT NULL
END

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'ConfiguracionResultados' AND COLUMN_NAME = 'IdCategoria' AND IS_NULLABLE = 'YES'
)
BEGIN
	EXEC('
		UPDATE ConfiguracionResultados
		SET IdCategoria = (SELECT TOP(1) IdCategoria FROM Categoria)
		WHERE IdCategoria IS NULL
	')
END

GO

IF EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'ConfiguracionResultados' AND COLUMN_NAME = 'IdCategoria' AND IS_NULLABLE = 'YES'
)
BEGIN
	EXEC('
		ALTER TABLE [ConfiguracionResultados] ALTER COLUMN [IdCategoria] INT NOT NULL
	')
END

GO

IF NOT EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
	WHERE TABLE_NAME = 'ConfiguracionResultados' AND CONSTRAINT_TYPE = 'FOREIGN KEY' 
	AND CONSTRAINT_NAME = 'FK_Categoria_ConfiguracionResultados'
)
BEGIN
	EXEC('
		ALTER TABLE [ConfiguracionResultados]
		ADD CONSTRAINT [FK_Categoria_ConfiguracionResultados]
		FOREIGN KEY (IdCategoria)
		REFERENCES [Categoria](IdCategoria)
	')
END

GO

IF NOT EXISTS(
	SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
	WHERE TABLE_NAME = 'ConfiguracionResultados' AND CONSTRAINT_TYPE = 'FOREIGN KEY' 
	AND CONSTRAINT_NAME = 'FK_Carrera_ConfiguracionResultados'
)
BEGIN
	EXEC('
		ALTER TABLE [ConfiguracionResultados]
		ADD CONSTRAINT [FK_Carrera_ConfiguracionResultados]
		FOREIGN KEY (IdCarrera)
		REFERENCES [Carrera](IdCarrera)
	')
END