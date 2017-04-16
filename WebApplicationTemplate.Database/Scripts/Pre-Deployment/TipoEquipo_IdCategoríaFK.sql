IF not exists
(
SELECT *
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME = 'IdCategoria' AND TABLE_NAME = 'TipoEquipo'
)
	BEGIN
		ALTER TABLE [dbo].[TipoEquipo]
		ADD [IdCategoria] INT NULL;

		UPDATE [dbo].[TipoEquipo]
		SET [dbo].[TipoEquipo].[IdCategoria] = (SELECT TOP 1 [dbo].[Categoria].[idCategoria] FROM [dbo].[Categoria]);
	END
ELSE
	BEGIN 
		ALTER TABLE [dbo].[TipoEquipo]
		ALTER COLUMN [IdCategoria] INT NULL;
	END
GO

IF not exists (select * from dbo.sysobjects where id = object_id('[dbo].[FK_TipoEquipo_Categoria]') and OBJECTPROPERTY(id, 'IsForeignKey') = 1)
BEGIN
	ALTER TABLE [dbo].[TipoEquipo]
	ADD CONSTRAINT [FK_TipoEquipo_Categoria] FOREIGN KEY (IdCategoria) REFERENCES Categoria(IdCategoria);
END

GO

ALTER TABLE [dbo].[TipoEquipo]
ALTER COLUMN [IdCategoria] INT NOT NULL;