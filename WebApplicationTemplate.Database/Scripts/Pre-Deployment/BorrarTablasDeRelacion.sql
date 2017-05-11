PRINT 'BorrarTablasDeRelacion.sql...'
/****************************************************************************
BORRA ELEMENTOS
*****************************************************************************/
IF exists (select * from dbo.sysobjects where id = object_id('[dbo].[FK__Categoria__IdCar__48CFD27E]') and OBJECTPROPERTY(id, 'IsForeignKey') = 1)
BEGIN
	ALTER TABLE [dbo].[CategoriaXCarrera] DROP CONSTRAINT [FK__Categoria__IdCar__48CFD27E]
END
GO

IF exists (select * from dbo.sysobjects where id = object_id('[dbo].[FK__Categoria__IdCat__49C3F6B7]') and OBJECTPROPERTY(id, 'IsForeignKey') = 1)
BEGIN
	ALTER TABLE [dbo].[CategoriaXCarrera] DROP CONSTRAINT [FK__Categoria__IdCat__49C3F6B7]
END
GO

IF exists (select * from dbo.sysobjects where id = object_id('[dbo].[PK__Categori__824E86B2D2F1B872]') and OBJECTPROPERTY(id, 'IsPrimaryKey') = 1)
BEGIN
	ALTER TABLE [dbo].[CategoriaXCarrera] DROP CONSTRAINT [PK__Categori__824E86B2D2F1B872]
END
GO

IF exists (select * from dbo.sysobjects where id = object_id('[dbo].[CategoriaXCarrera]') and OBJECTPROPERTY(id, 'IsTable') = 1)
BEGIN
	DROP TABLE [dbo].[CategoriaXCarrera]
END
GO

IF exists (select * from dbo.sysobjects where id = object_id('[dbo].[FK__Categoria__IdCat__4AB81AF0]') and OBJECTPROPERTY(id, 'IsForeignKey') = 1)
BEGIN
	ALTER TABLE [dbo].[CategoriaXRuta] DROP CONSTRAINT [FK__Categoria__IdCat__4AB81AF0]
END
GO

IF exists (select * from dbo.sysobjects where id = object_id('[dbo].[FK__Categoria__IdRut__4BAC3F29]') and OBJECTPROPERTY(id, 'IsForeignKey') = 1)
BEGIN
	ALTER TABLE [dbo].[CategoriaXRuta] DROP CONSTRAINT [FK__Categoria__IdRut__4BAC3F29]
END
GO

IF exists (select * from dbo.sysobjects where id = object_id('[dbo].[PK__Categori__07C1A274C7CE7A79]') and OBJECTPROPERTY(id, 'IsPrimaryKey') = 1)
BEGIN
	ALTER TABLE [dbo].[CategoriaXRuta] DROP CONSTRAINT [PK__Categori__07C1A274C7CE7A79]
END
GO

IF exists (select * from dbo.sysobjects where id = object_id('[dbo].[CategoriaXRuta]') and OBJECTPROPERTY(id, 'IsTable') = 1)
BEGIN
	DROP TABLE [dbo].[CategoriaXRuta]
END
GO

IF exists (select * from dbo.sysobjects where id = object_id('[dbo].[FK__RamaXCarr__IdCar__5441852A]') and OBJECTPROPERTY(id, 'IsForeignKey') = 1)
BEGIN
	ALTER TABLE [dbo].[RamaXCarrera] DROP CONSTRAINT [FK__RamaXCarr__IdCar__5441852A]
END
GO

IF exists (select * from dbo.sysobjects where id = object_id('[dbo].[FK__RamaXCarr__IdRam__534D60F1]') and OBJECTPROPERTY(id, 'IsForeignKey') = 1)
BEGIN
	ALTER TABLE [dbo].[RamaXCarrera] DROP CONSTRAINT [FK__RamaXCarr__IdRam__534D60F1]
END
GO

IF exists (select * from dbo.sysobjects where id = object_id('[dbo].[PK__RamaXCar__BB83459FCFAA828E]') and OBJECTPROPERTY(id, 'IsPrimaryKey') = 1)
BEGIN
	ALTER TABLE [dbo].[RamaXCarrera] DROP CONSTRAINT [PK__RamaXCar__BB83459FCFAA828E]
END
GO

IF exists (select * from dbo.sysobjects where id = object_id('[dbo].[RamaXCarrera]') and OBJECTPROPERTY(id, 'IsTable') = 1)
BEGIN
	DROP TABLE [dbo].[RamaXCarrera]
END
GO
/****************************************************************************
CREA NUEVAS COLUMNAS FK
*****************************************************************************/
IF not exists (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'IdCarrera' AND TABLE_NAME = 'Categoria')
BEGIN
	ALTER TABLE [dbo].[Categoria]
	ADD [IdCarrera] INT NULL;	
END
GO

IF exists (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'IdCarrera' AND TABLE_NAME = 'Categoria'  AND IS_NULLABLE = 'YES')
BEGIN	
	UPDATE [dbo].[Categoria]
	SET [dbo].[Categoria].[IdCarrera] = (SELECT TOP 1 [dbo].[Carrera].[IdCarrera] FROM [dbo].[Carrera]); 
END
GO

IF not exists (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'IdCarrera' AND TABLE_NAME = 'Rama')
BEGIN
	ALTER TABLE [dbo].[Rama]
	ADD [IdCarrera] INT NULL;	
END
GO

IF exists (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'IdCarrera' AND TABLE_NAME = 'Rama' AND IS_NULLABLE = 'YES')
BEGIN	
	UPDATE [dbo].[Rama]
	SET [dbo].[Rama].[IdCarrera] = (SELECT TOP 1 [dbo].[Carrera].[IdCarrera] FROM [dbo].[Carrera]); 
END
GO

IF not exists (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'IdCategoria' AND TABLE_NAME = 'Ruta')
BEGIN
	ALTER TABLE [dbo].[Ruta]
	ADD [IdCategoria] INT NULL;	
END
GO

IF exists (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'IdCategoria' AND TABLE_NAME = 'Ruta' AND IS_NULLABLE = 'YES')
BEGIN	
	UPDATE [dbo].[Ruta]
	SET [dbo].[Ruta].[IdCategoria] = (SELECT TOP 1 [dbo].[Categoria].[IdCategoria] FROM [dbo].[Categoria]); 
END
GO

/****************************************************************************
CREA NUEVAS CONSTRAINTS FK
*****************************************************************************/
IF not exists (select * from dbo.sysobjects where id = object_id('[dbo].[FK_Rama_Carrera]') and OBJECTPROPERTY(id, 'IsForeignKey') = 1)
BEGIN	
	ALTER TABLE [dbo].[Rama] ADD CONSTRAINT [FK_Rama_Carrera] FOREIGN KEY([IdCarrera]) REFERENCES [dbo].[Carrera] ([IdCarrera])
END
GO

IF not exists (select * from dbo.sysobjects where id = object_id('[dbo].[FK_Categoria_Carrera]') and OBJECTPROPERTY(id, 'IsForeignKey') = 1)
BEGIN	
	ALTER TABLE [dbo].[Categoria] ADD CONSTRAINT [FK_Categoria_Carrera] FOREIGN KEY ([IdCarrera]) REFERENCES [Carrera]([IdCarrera])
END
GO

IF not exists (select * from dbo.sysobjects where id = object_id('[dbo].[FK_Ruta_Categoria]') and OBJECTPROPERTY(id, 'IsForeignKey') = 1)
BEGIN	
	ALTER TABLE [dbo].[Ruta] ADD CONSTRAINT [FK_Ruta_Categoria] FOREIGN KEY ([IdCategoria]) REFERENCES [Categoria]([IdCategoria])
END
GO

/****************************************************************************
CAMBIA A REQUERIDAS LAS COLUMNAS FK
*****************************************************************************/
IF exists (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'IdCarrera' AND TABLE_NAME = 'Categoria' AND IS_NULLABLE = 'YES')
BEGIN
	ALTER TABLE [dbo].[Categoria]
	ALTER COLUMN [IdCarrera] INT NOT NULL;
END
GO

IF exists (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'IdCarrera' AND TABLE_NAME = 'Rama' AND IS_NULLABLE = 'YES')
BEGIN
	ALTER TABLE [dbo].[Rama]
	ALTER COLUMN [IdCarrera] INT NOT NULL;
END
GO

IF exists (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'IdCategoria' AND TABLE_NAME = 'Ruta' AND IS_NULLABLE = 'YES')
BEGIN
	ALTER TABLE [dbo].[Ruta]
	ALTER COLUMN [IdCategoria] INT NOT NULL;
END
GO