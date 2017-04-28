CREATE TABLE [dbo].[User]
(
	[IdUser]		INT NOT NULL IDENTITY,
	[Username]		VARCHAR(50) NOT NULL,
	[Password]		VARCHAR(255) NOT NULL,
	[SessionToken]  VARBINARY(50) NULL,
	[IsSuperUser]	BIT NOT NULL DEFAULT 0
)
