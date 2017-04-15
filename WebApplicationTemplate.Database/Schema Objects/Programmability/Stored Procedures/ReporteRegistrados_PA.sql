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
	, P.Edad
	, P.Domicilio
	, P.Invitado
	, P.NumeroAccion
	, P.Telefono
	, P.Email
	, P.TelefonoEmergencia
	, P.Pagado
	FROM Participante P
	INNER JOIN ParticipanteXCarrera PxC ON PxC.IdParticipante = P.IdParticipante
	WHERE PxC.IdCarrera = @IdCarrera

END
