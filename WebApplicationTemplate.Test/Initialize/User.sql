IF NOT EXISTS(SELECT * FROM [User] WHERE Username = 'Test')
BEGIN
INSERT INTO [User] (Username, IsSuperUser)
             VALUES('Test', 1)
END