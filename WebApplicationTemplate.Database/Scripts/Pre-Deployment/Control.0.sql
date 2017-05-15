PRINT 'Control.0.sql...'

GO

IF NOT EXISTS(
	SELECT * FROM [Control]
	WHERE IdControlASP = 'phFolioOffline'
)
BEGIN
	INSERT INTO [Control] VALUES('phFolioOffline')
END

GO