DECLARE @IdCarrera int = 1
MERGE INTO [ControlXCarrera] AS T
USING ( 
	--- IdControlASP | ICarrera | Etiqueta | Requerido | EtiquetaRequerido
	VALUES 
	--, ('datePickerEdad', 2, 'Fecha de nacimiento', 1)
	--, ('chkAcepto', 2, 'Acepto', 1)
	--, ('cusChkAcepto', 2, 'Se requiere aceptar los términos', 1)
	--, ('upTotal', 2, '', 1)
	--, 
	('phApellidoPaterno', @IdCarrera, 'Apellido paterno', 1, 'Se requiere apellido paterno', 0, null, null)
	, ('phApellidoMaterno', @IdCarrera, 'Apellido materno', 0, 'Se requiere apellido materno', 0, null, null)
	, ('phDatePickerEdad', @IdCarrera, 'Fecha nacimiento', 1, 'Se fecha de nacimiento', 0, null, null)
	, ('phNombres', @IdCarrera, 'Nombre(s)', 1, 'Se requiere nombre', 0, null, null)
	, ('phEmail', @IdCarrera, 'Email', 1, 'Se requiere email', 1, 'Debe insertar un email valido', '^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$')
	, ('phTelefonoPersonal', @IdCarrera, 'Telefono personal', 1, 'Se requiere telefono personal', 1, 'Debe insertar un telefono valido', '\d+')
	, ('phTelefonoEmergencia', @IdCarrera, 'Telefono emergencia', 0, 'Se requiere telefono emergencia', 0, 'Debe insertar un telefono valido', '\d+')
	, ('phDomicilio', @IdCarrera, 'Domicilio', 0, 'Se requiere domicilio', 0, null, null)
	, ('phRamas', @IdCarrera, 'Ramas', 1, 'Se requiere una rama', 0, null, null)
	, ('phCategoria', @IdCarrera, 'Categoria', 1, 'Se requiere una categoria', 0, null, null)
	, ('phAcepto',  @IdCarrera, 'Acepto', 1, 'Se requiere aceptar los terminos', 0, null, null)

	) AS S (IdControlASP, IdCarrera, Etiqueta, Requerido, EtiquetaRequerido, RegularExpression, RegularErrorMessage, ValidationExpression)
	INNER JOIN [Control] C ON C.IdControlASP = S.IdControlASP
	INNER JOIN [Carrera] Carr ON Carr.IdCarrera = S.IdCarrera
ON T.IdCarrera = Carr.IdCarrera AND T.IdControl = C.IdControl

	WHEN NOT MATCHED BY TARGET THEN 
		INSERT (IdControl, IdCarrera, Etiqueta, Requerido, EtiquetaRequerido) 
		VALUES (C.IdControl, S.IdCarrera, S.Etiqueta, S.Requerido, S.EtiquetaRequerido)

	WHEN MATCHED 
	--AND T.Etiqueta <> S.Etiqueta
	--OR T.Requerido <> S.Requerido
	--OR T.EtiquetaRequerido <> S.EtiquetaRequerido
	--OR T.RegularExpression <> S.RegularExpression
	--OR T.RegularErrorMessage <> S.RegularErrorMessage
	--OR T.ValidationExpression <> S.ValidationExpression
	THEN 
	UPDATE SET T.Requerido = S.Requerido
	, T.Etiqueta = S.Etiqueta
	, T.EtiquetaRequerido = S.EtiquetaRequerido
	, T.RegularExpression = S.RegularExpression
	, T.RegularErrorMessage = S.RegularErrorMessage
	, T.ValidationExpression = S.ValidationExpression
;