PRINT 'ConfiguracionResultados.0.sql...'

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