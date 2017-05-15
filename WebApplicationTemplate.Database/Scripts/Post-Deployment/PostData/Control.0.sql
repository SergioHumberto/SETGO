﻿--- Controles que se encuentran en la pagina de registrarParticipantes.aspx

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
	-- , ('phDatePickerEdad')
	, ('phEmail')
	, ('phTelefonoPersonal')
	, ('phTelefonoEmergencia')
	, ('phDomicilio')
	, ('phRamas')
	, ('phCategoria')
	, ('phAcepto')
	 , ('phClub')
	 , ('phGeneric01')
	 , ('phGeneric02')
	 , ('phGeneric03')
	 , ('phGeneric04')
	 , ('phGeneric05')
	 , ('phGeneric06')
	 , ('phGeneric07')
	 , ('phGeneric08')
	 , ('phGeneric09')
	 , ('phGeneric10')
	,('phFechaNacimiento')
	,('phFolioOffline')
	) AS S (NameControl)  
ON T.IdControlASP = S.NameControl 

WHEN NOT MATCHED THEN 
	INSERT (IdControlASP) VALUES (NameControl)
;
