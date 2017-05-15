PRINT 'ControlXCarrera.0.sql...'

GO

DECLARE @IdControl int = (SELECT IdControl FROM [Control] WHERE IdControlASP = 'phFolioOffline')

IF NOT EXISTS(
	SELECT * FROM ControlXCarrera
	WHERE IdControl = @IdControl
	AND Etiqueta = 'Folio'
)
BEGIN
	INSERT INTO ControlXCarrera VALUES
	(@IdControl, 1, 'Folio', 1, 'Se requiere folio', 0, null, null)
END

GO