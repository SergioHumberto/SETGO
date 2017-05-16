CREATE FUNCTION [dbo].[GetClasificacionesXParticipante]
(
	@IdParticipante int
)
RETURNS VARCHAR(MAX)
AS
BEGIN
	
	DECLARE @Clasificacion VARCHAR(MAX) = ''

	SELECT  
		@Clasificacion = @Clasificacion + ' ' + Clas.Nombre + ': ' + VC.Etiqueta + '.'
	FROM ClasificacionXParticipante CxP
	INNER JOIN ValorClasificacion VC ON VC.IdValorClasificacion = CxP.IdValorClasificacion
	INNER JOIN Clasificacion Clas ON Clas.IdClasificacion = VC.IdClasificacion
	WHERE CxP.IdParticipante = @IdParticipante

	RETURN @Clasificacion

END
