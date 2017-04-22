CREATE TABLE [dbo].[SMTPConfig]
(
	[IdSMTPConfig] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Server] VARCHAR(255) NOT NULL, 
    [Port] INT NOT NULL, 
    [User] VARCHAR(100) NOT NULL, 
    [Password] VARCHAR(100) NOT NULL
)
