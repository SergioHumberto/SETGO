CREATE TABLE [dbo].[Element]
(
	IdElement		INT NOT NULL IDENTITY,

	IdInterface		INT NOT NULL,

	Code			VARCHAR(50) NOT NULL,

	IsSelect		BIT NOT NULL,

	IsInsert		BIT NOT NULL,

	IsUpdate		BIT NOT NULL,

	IsDelete		BIT NOT NULL,

	-- TODO complete element data
)
