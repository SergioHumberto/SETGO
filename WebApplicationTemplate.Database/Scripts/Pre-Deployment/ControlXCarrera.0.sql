PRINT 'ControlXCarrera.0.sql...'

GO

DECLARE @IdControl int = (SELECT IdControl FROM [Control] WHERE IdControlASP = 'phFolioOffline')

IF EXISTS(
	SELECT * FROM ControlXCarrera
	WHERE IdControl = @IdControl
	AND Etiqueta = 'Folio'
)
BEGIN
	DELETE FROM ControlXCarrera
	WHERE IdControl = @IdControl
	AND Etiqueta = 'Folio'
END

GO