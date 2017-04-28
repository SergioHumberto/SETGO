--- Controles que se encuentran en la pagina de registrarParticipantes.aspx

MERGE INTO [Control] AS T
USING ( 
	--- IdControlASP
	VALUES 
	--('lblEtiquetaNombre')
	--, ('txtNombres')
	--, ('reqNombres')
	--, ('lblApellidoPaterno')
	--, ('txtApellidoPaterno')
	--, ('reqApellidoPaterno')
	--, ('lblApellidoMaterno')
	--, ('txtApellidoMaterno')
	--, ('reqApellidoMaterno')
	--, ('datePickerEdad')
	--, ('lblEmail')
	--, ('txtEmail')
	--, ('reqEmail')
	--, ('revEmail')
	--, ('lblTelefonoPersonal')
	--, ('txtTelefonoPersonal')
	--, ('reqTelefonoPersonal')
	--, ('revtxtTelefonoPersonal')
	--, ('lblTelefonoEmergencia')
	--, ('txtTelefonoEmergencia')
	--, ('reqTelefonoEmergencia')
	--, ('revtxtTelefonoEmergencia')
	--, ('lblDomicilio')
	--, ('txtDomicilio')
	--, ('reqDomicilio')
	--, ('lblRama')
	--, ('rblRamas')
	--, ('reqRama')
	--, ('lblCategoria')
	--, ('rblCategoria')
	--, ('reqCategoria')
	--, ('chkAcepto')
	--, ('cusChkAcepto')
	--, ('upTotal')
	('phApellidoPaterno')
	, ('phApellidoMaterno')
	, ('phNombres')
	, ('phDatePickerEdad')
	, ('phEmail')
	, ('phTelefonoPersonal')
	, ('phTelefonoEmergencia')
	, ('phDomicilio')
	, ('phRamas')
	, ('phCategoria')
	, ('phAcepto')

	) AS S (NameControl)  
ON T.IdControlASP = S.NameControl 

WHEN NOT MATCHED THEN 
	INSERT (IdControlASP) VALUES (NameControl)
;
