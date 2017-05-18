PRINT 'ControlXCarrera.0.sql...'

GO

DECLARE @IdControl int = (SELECT IdControl FROM [Control] WHERE IdControlASP = 'phFolioOffline')

IF NOT EXISTS(
	SELECT * FROM ControlXCarrera
	WHERE IdControl = @IdControl
	AND Etiqueta = 'Código de Pago'
)
BEGIN
	INSERT INTO ControlXCarrera (IdControl, IdCarrera, Etiqueta, Requerido, EtiquetaRequerido, RegularExpression, RegularErrorMessage, ValidationExpression)
	VALUES (@IdControl, 1, 'Código de Pago', 1, 'Se requiere el Código de Pago', 0, null, null)
END

GO

DECLARE @IdControl int = (SELECT IdControl FROM [Control] WHERE IdControlASP = 'phRuta')

IF NOT EXISTS(
	SELECT * FROM ControlXCarrera
	WHERE IdControl = @IdControl
	AND Etiqueta = 'Ruta'
)
BEGIN
	INSERT INTO ControlXCarrera (IdControl, IdCarrera, Etiqueta, Requerido, EtiquetaRequerido, RegularExpression, RegularErrorMessage, ValidationExpression)
	VALUES (@IdControl, 1, 'Ruta', 1, 'Se requiere ruta', 0, null, null)
END

GO