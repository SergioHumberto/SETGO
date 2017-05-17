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

IF NOT EXISTS(
	SELECT * FROM [Control]
	WHERE IdControlASP = 'phRuta'
)
BEGIN
	INSERT INTO [Control] VALUES('phRuta')
END

GO