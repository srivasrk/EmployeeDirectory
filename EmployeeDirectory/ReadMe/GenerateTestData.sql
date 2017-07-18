
Insert Into [EmployeeDirectory].[dbo].[Employees]
(FirstName, LastName, JobTitle, Location, Email, IsAdmin)
VALUES('Rahul', 'Srivastava', 'Software Developer', 'Cincinnati', 'rahul@srivastava.com', 0),
('Leonardo', 'DiCaprio', 'HR', 'Cincinnati', 'leonardo@dicaprio.com', 0),
('Michael', 'Jakson', 'HR', 'Cincinnati', 'michael@jackson.com', 1),
('Robert', 'DeNiro', 'HR', 'Cincinnati', 'robert@deniro.com', 1),
('Morgan', 'Freeman', 'CEO', 'Cincinnati', 'morgan@freeman.com', 1),
('Will', 'Smith', 'Manager', 'Cincinnati', 'will@smith.com', 0),
('Ben', 'Affleck', 'QA', 'Cincinnati', 'ben@affleck.com', 1),
('Zooey', 'Deschanel', 'Software Developer', 'Cincinnati', 'zooey@deschanel.com', 0),
('Conan', 'OBrien', 'QA', 'Cincinnati', 'conan@obrien.com', 0),

/****** Script to Generate test data  ******/

DECLARE @FirstName VARCHAR(50)
DECLARE @LastName VARCHAR(50)
DECLARE @JobTitle VARCHAR(50)
DECLARE @Location VARCHAR(50)
DECLARE @Email VARCHAR(50)
DECLARE @IsAdmin bit

DECLARE @Random INT
DECLARE @Upper INT
DECLARE @Lower INT
DECLARE @RowCount INT

SET @Lower = -730
SET @Upper = -1
SET @RowCount = 1

/**update this number for number of records**/
WHILE @RowCount < 3
BEGIN
SET @FirstName = CAST(@RowCount AS VARCHAR(20))
SET @LastName = CAST(@RowCount AS VARCHAR(20))
SET @JobTitle = CAST(@RowCount AS VARCHAR(20))
SET @Location = CAST(@RowCount AS VARCHAR(20))
SET @Email = CAST(@RowCount AS VARCHAR(20)) 
SET @IsAdmin = CAST((@RowCount % 2) AS bit)

INSERT INTO [EmployeeDirectory].[dbo].[Employees]
(FirstName
,LastName
,JobTitle 
,Location
,Email
,IsAdmin)
VALUES
(@FirstName, @LastName, @JobTitle, @Location, @Email, @IsAdmin)

SET @RowCount = @RowCount + 1
END