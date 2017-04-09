IF EXISTS(SELECT * FROM [User] WHERE Username = 'Test')
BEGIN
DELETE FROM [User] WHERE Username = 'Test'
END