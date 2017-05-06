WITH [DATA] AS ( 
	SELECT CxC.* FROM (VALUES ('lblEtiquetaNombre')
	, ('txtNombres')
	, ('reqNombres')
	, ('lblApellidoPaterno')
	, ('txtApellidoPaterno')
	, ('reqApellidoPaterno')
	, ('lblApellidoMaterno')
	, ('txtApellidoMaterno')
	, ('reqApellidoMaterno')
	, ('datePickerEdad')
	, ('lblEmail')
	, ('txtEmail')
	, ('reqEmail')
	, ('revEmail')
	, ('lblTelefonoPersonal')
	, ('txtTelefonoPersonal')
	, ('reqTelefonoPersonal')
	, ('revtxtTelefonoPersonal')
	, ('lblTelefonoEmergencia')
	, ('txtTelefonoEmergencia')
	, ('reqTelefonoEmergencia')
	, ('revtxtTelefonoEmergencia')
	, ('lblDomicilio')
	, ('txtDomicilio')
	, ('reqDomicilio')
	, ('lblRama')
	, ('rblRamas')
	, ('reqRama')
	, ('lblCategoria')
	, ('rblCategoria')
	, ('reqCategoria')
	, ('chkAcepto')
	, ('cusChkAcepto')
	, ('upTotal')
	, ('phDatePickerEdad')
) AS D(IdControlASP)
  INNER JOIN Control C ON C.IdControlASP = D.IdControlASP
  INNER JOIN ControlXCarrera CxC ON CxC.IdControl = C.IdControl
)
DELETE CxC FROM ControlXCarrera	CxC
INNER JOIN DATA ON DATA.IdControlXCarrera = CxC.IdControlXCarrera

GO

DELETE C FROM [Control] C
LEFT JOIN ControlXCarrera CxC ON CxC.IdControl = C.IdControl
WHERE CxC.IdControlXCarrera IS NULL
GO