CREATE PROCEDURE [dbo].[ReporteRegistrados_PA]
(
	@IdCarrera INT
)
AS
BEGIN

	SELECT 
	P.Nombre
	, P.ApellidoPaterno
	, P.ApellidoMaterno
	, P.FechaNacimiento
	, P.Domicilio
	, P.Invitado
	, P.NumeroAccion
	, P.Telefono
	, P.Email
	, P.TelefonoEmergencia
	, C.Nombre AS [Categoria]
	, R.Nombre AS [Rama]
	, P.TransactionNumber
	, P.StatusPaypal
	, P.Folio
	, P.Generic01
	, P.Generic02
	, P.Generic03
	, P.Generic04
	, P.Generic05
	, P.Generic06
	, P.Generic07
	, P.Generic08
	, P.Generic09
	, P.Generic10
	, dbo.GetClasificacionesXParticipante(P.IdParticipante) AS [Clasificaciones]
	FROM Participante P
	INNER JOIN ParticipanteXCarrera PxC ON PxC.IdParticipante = P.IdParticipante
	INNER JOIN Categoria C ON C.IdCategoria = PxC.IdCategoria
	INNER JOIN Rama R ON R.IdRama = PxC.IdRama
	WHERE PxC.IdCarrera = @IdCarrera

END
