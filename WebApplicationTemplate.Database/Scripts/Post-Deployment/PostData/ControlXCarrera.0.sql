MERGE INTO [ControlXCarrera] AS T
USING ( 
	--- IdControlASP | ICarrera | Etiqueta | Requerido
	VALUES 
	('lblEtiquetaNombre', 2, 'Nombre(s)', 1)
	, ('txtNombres', 2, '', 1)
	, ('reqNombres', 2, 'Se requiere nombre', 1)
	, ('lblApellidoPaterno', 2, 'Apellido paterno', 1)
	, ('txtApellidoPaterno', 2, '', 1)
	, ('reqApellidoPaterno', 2, 'Se requiere apellido paterno', 1)
	, ('lblApellidoMaterno', 2, 'Apellido materno', 1)
	, ('txtApellidoMaterno', 2, '', 1)
	, ('reqApellidoMaterno', 2, 'Se requiere apellido materno', 0)
	, ('datePickerEdad', 2, 'Fecha de nacimiento', 1)
	, ('lblEmail', 2, 'Correo electronico', 1)
	, ('txtEmail', 2, '', 1)
	, ('reqEmail', 2, 'Se requiere correo electronico', 1)
	, ('revEmail', 2, 'Debe insertar un correo electronico válido', 1)
	, ('lblTelefonoPersonal', 2, 'Telefono (Celular para recibir resultados en tiempo real)', 1)
	, ('txtTelefonoPersonal', 2, '', 1)
	, ('reqTelefonoPersonal', 2, 'Se requiere un número celular', 1)
	, ('revtxtTelefonoPersonal', 2, 'Debe de ser un valor numérico', 1)
	, ('lblTelefonoEmergencia', 2, 'Telefono contacto emergencia', 1)
	, ('txtTelefonoEmergencia', 2, '', 1)
	, ('reqTelefonoEmergencia', 2, 'Se requiere telefono de emergencia', 0)
	, ('revtxtTelefonoEmergencia', 2, 'Debe ser un valor numérico', 1)
	, ('lblDomicilio', 2, 'Domicilio', 1)
	, ('txtDomicilio', 2, '', 1)
	, ('reqDomicilio', 2, 'Se requiere domicilio', 0)
	, ('lblRama', 2, 'Rama', 1)
	, ('rblRamas', 2, '', 1)
	, ('reqRama', 2, 'Se requiere rama', 1)
	, ('lblCategoria', 2, 'Categoria', 1)
	, ('rblCategoria', 2, '', 1)
	, ('reqCategoria', 2, 'Se requiere categoria', 1)
	, ('chkAcepto', 2, 'Acepto', 1)
	, ('cusChkAcepto', 2, 'Se requiere aceptar los términos', 1)
	, ('upTotal', 2, '', 1)

	) AS S (NameControl, IdCarrera, Etiqueta, Requerido)
	INNER JOIN [Control] C ON C.IdControlASP = S.NameControl
	INNER JOIN [Carrera] Carr ON Carr.IdCarrera = S.IdCarrera
ON T.IdCarrera = Carr.IdCarrera AND C.IdControl = T.IdControl

	WHEN NOT MATCHED BY TARGET THEN 
		INSERT (IdControl, IdCarrera, Etiqueta, Requerido) 
		VALUES (C.IdControl, S.IdCarrera, S.Etiqueta, S.Requerido)


	WHEN MATCHED 
	AND ((T.Etiqueta <> S.Etiqueta) 
			OR (T.Requerido <> S.Requerido)
		) THEN 
		UPDATE SET T.Requerido = S.Requerido
		, T.Etiqueta = S.Etiqueta
;
