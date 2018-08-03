CREATE DATABASE CollegeManagement
GO
USE CollegeManagement
GO

--independent table
CREATE TABLE Users
(
Login_Id int not null,
Login_Password varchar(30) not null,
User_Type varchar(20),
Registration_Date datetime
)
GO

INSERT INTO Users VALUES (1, 'admin', 'admin', GETDATE())
GO


-- Independent table
CREATE TABLE Courses(
Course_Id int Primary Key,
Course_Name varchar(20) not null,
Fee_Amount float 
)
GO
ALTER TABLE Courses ADD Semester_Count int not null
GO
-- Initial Courses values
INSERT INTO Courses VALUES (101, 'B.Tech CSE', 25000, 8)
INSERT INTO Courses VALUES (102, 'B.Tech ECE', 23000, 8)
INSERT INTO Courses VALUES (103, 'BBA', 15000, 6)
INSERT INTO Courses VALUES (104, 'MBA - Finance', 33000, 4)
GO

-- Faculty Table
CREATE TABLE Faculty
(
Faculty_Id int Primary Key identity(5000,1),
Faculty_Name varchar(100) not null,
Phone_Number bigint not null,
Email_Address varchar(100) not null,
Gender varchar(10),
DateOfBirth date,
Date_Of_Joining DATE not null,
Salary float not null,
Course_Id int, --foreign key
constraint FK_CoursesFaculty Foreign Key (Course_Id) REFERENCES Courses(Course_Id) ON DELETE CASCADE,
)
GO

-- Student Table
CREATE TABLE StudentDetails
(
Student_Id int Primary Key identity(1000,1),
Student_Name varchar(100) not null,
Email_Address varchar(100) not null,
Gender varchar(10),
DateOfBirth date,
Phone_Number bigint not null,
Student_Address varchar(100) not null,
Father_Name varchar(100) not null,
Father_Phone bigint not null,
Father_Email varchar(100) not null,
Course_Id int, -- Foreign Key
constraint FK_CoursesStudentDetails Foreign Key (Course_Id) REFERENCES Courses(Course_Id) ON DELETE CASCADE
)
GO
ALTER TABLE StudentDetails ADD Faculty_Id int
ALTER TABLE StudentDetails ADD CONSTRAINT FK_FacultyStudentDetials Foreign Key (Faculty_Id) REFERENCES Faculty(Faculty_Id) ON DELETE NO ACTION
GO

CREATE TABLE FeePayment(
Date_Of_Payment DATE not null,
Last_Date DATE not null,
IsPaid bit,
Course_Id int, -- foreign key
Student_Id int, -- foreign key
constraint FK_CoursesFeePayment Foreign Key (Course_Id) REFERENCES Courses(Course_Id),
constraint FK_StudentDetailsFeePayment Foreign Key (Student_Id) REFERENCES StudentDetails(Student_Id) ON DELETE CASCADE
)
GO


-- Independent table
CREATE TABLE CollegeDetailsAndEvents(
Event_Name varchar(50) not null,
Event_Description varchar(1000) not null,
Event_Date DATE not null,
Date_Of_Creation DATE not null
)
GO


-- Semester Details Table
CREATE TABLE SemesterDetails
(
semester int not null,
marks float not null,
Course_Id int not null, --foreign key
Student_Id int not null,
constraint FK_CoursesSemesterDetails Foreign Key (Course_Id) REFERENCES Courses(Course_Id),
constraint FK_StudentDetailsSemesterDetails  FOREIGN KEY (Student_Id)  REFERENCES StudentDetails(Student_Id) ON DELETE CASCADE
)
GO


-------------------------------------

-- Add Login ID and password to Users table
CREATE PROCEDURE usp_SetLoginIdPassword(@id int, @name varchar(30), @type varchar(20))
AS
BEGIN
INSERT INTO Users Values(@id, REPLACE(@name,' ', '')+CONVERT(varchar, @id), @type, GETDATE())
END
GO

--Initial Fee Payment (Date = Today's date)
CREATE PROCEDURE usp_AddStudentFeePayment(@student_id int, @course_id int)
AS
BEGIN
INSERT INTO FeePayment VALUES (GETDATE(), DATEADD(day,2,GETDATE()), 1, @course_id,@student_id)
END
GO


--Initial Semester details Update
CREATE PROCEDURE usp_AddInitialSemesterDetails(@student_id int, @course_id int)
AS
BEGIN
declare @sem_count int
SET @sem_count = (SELECT Semester_Count FROM Courses WHERE Course_Id = @course_id)
declare @i int = 1
WHILE @i <= @sem_count
BEGIN
INSERT INTO SemesterDetails VALUES (@i, CONVERT(int, RAND()*100), @course_id, @student_id)
SET @i = @i + 1
END
END
GO


-- Add Student
CREATE PROCEDURE usp_AdminAddStudent
(
@Name varchar(100),
@Email varchar(100),
@Gender varchar(10),
@DOB date,
@Phone bigint,
@Address varchar(100),
@FatherName varchar(100),
@FatherPhone bigint,
@FatherEmail varchar(100),
@CourseId int
)
AS
BEGIN
BEGIN TRY
INSERT INTO StudentDetails (Student_Name,Email_Address,Gender,DateOfBirth,Phone_Number,Student_Address,Father_Name,Father_Phone,Father_Email,Course_Id) VALUES
(
@Name,
@Email,
@Gender,
@DOB,
@Phone,
@Address,
@FatherName,
@FatherPhone,
@FatherEmail,
@courseid
)
declare @status int
SET @status = 1
END TRY
BEGIN CATCH
END CATCH
if(@status = 1)
BEGIN
declare @id int
SET @Id=SCOPE_IDENTITY()
exec usp_SetLoginIdPassword @Id, @Name, 'student'
exec usp_AddStudentFeePayment @Id, @CourseId
exec usp_AddInitialSemesterDetails @Id, @CourseId
UPDATE StudentDetails SET Faculty_Id = (SELECT TOP 1 Faculty_Id FROM Faculty WHERE Course_Id = @courseid) WHERE Student_Id = @Id
END
END
GO

--exec usp_AdminAddStudent 'mehul600','abc@yahoo.com','m','12 june 1996',123456789,'abc city', 'rohan',997766446,'dsdg@gmail.com', 102
--exec usp_AdminAddStudent 'ascasc6','abc@yahoo.com','m','12 june 1996',123456789,'abc city', 'rohan',997766446,'dsdg@gmail.com', 103
--exec usp_AdminAddStudent 'ascasc6123123','abc@yahoo.com','m','12 june 1996',123456789,'abc city', 'rohan',997766446,'dsdg@gmail.com', 103
--exec usp_AdminAddStudent 'dfhdh635453','abc@yahoo.com','m','12 june 1996',123456789,'abc city', 'rohan',997766446,'dsdg@gmail.com', 103
--exec usp_AdminAddStudent 'fgfgn1233','abc@yahoo.com','m','12 june 1996',123456789,'abc city', 'rohan',997766446,'dsdg@gmail.com', 104

-- Add Faculty
CREATE PROCEDURE usp_AdminAddFaculty
(
@Name varchar(100),
@Phone bigint,
@Email varchar(100),
@Gender varchar(10),
@DOB date,
@DOJ date,
@Salary float,
@CourseId int
)
AS
BEGIN
BEGIN TRY
INSERT INTO Faculty (
Faculty_Name,
Phone_Number,
Email_Address,
Gender,
DateOfBirth,
Date_Of_Joining,
Salary,
Course_Id) VALUES
(
@Name,
@Phone,
@Email,
@Gender,
@DOB,
@DOJ,
@Salary,
@CourseId
)
DECLARE @status int
SET @status = 1
END TRY
BEGIN CATCH
END CATCH
if(@status = 1)
BEGIN
declare @Id int
SET @Id=SCOPE_IDENTITY()
exec usp_SetLoginIdPassword @Id, @Name, 'faculty'
IF EXISTS(SELECT * FROM StudentDetails WHERE Course_Id = @CourseId AND Faculty_Id IS NULL)
BEGIN
UPDATE StudentDetails SET Faculty_Id = @Id WHERE Course_Id = @CourseId AND Faculty_Id IS NULL
END
END
END
GO


--exec usp_AdminAddFaculty 'teach456',12344543,'zdsg','f','12 june 2016','12 may 2018',12000,102

-- Modify Student by Admin
CREATE PROCEDURE usp_AdminModifyStudentDetails (
@Id int,
@Name varchar(100),
@Email varchar(100),
@Gender varchar(10),
@DOB date,
@Phone bigint,
@Address varchar(100),
@FatherName varchar(100),
@FatherPhone bigint,
@FatherEmail varchar(100),
@Password varchar(30)
)
AS
BEGIN
UPDATE StudentDetails SET
Student_Name=@Name,
Email_Address=@Email,
Gender=@Gender,
DateOfBirth=@DOB,
Phone_Number=@Phone,
Student_Address=@Address,
Father_Name=@FatherName,
Father_Phone=@FatherPhone,
Father_Email=@FatherEmail
WHERE Student_Id = @Id
UPDATE Users SET
Login_Password = @Password WHERE Login_Id = @Id
END
GO



--Modify Faculty By Admin
CREATE PROCEDURE usp_ModifyFacultyByAdmin
(
@Id int,
@Name varchar(100),
@Phone bigint,
@Email varchar(100),
@Gender varchar(10),
@DOB date,
@DOJ date,
@Salary float,
@Password varchar(100)
)
AS
BEGIN
UPDATE Faculty SET
Faculty_Name=@Name,
Phone_Number=@Phone,
Email_Address=@Email,
Gender=@Gender,
DateOfBirth=@DOB,
Date_Of_Joining=@DOJ,
Salary=@Salary
WHERE 
Faculty_Id = @Id
Update Users Set Login_Password = @Password WHERE Login_Id = @Id
END
GO


-- Admin Delete Faculty Details
CREATE PROCEDURE usp_DeleteFacultyDetails(@Id int)
AS
BEGIN
BEGIN TRY
UPDATE StudentDetails SET Faculty_Id=null Where Faculty_Id=@Id
DECLARE @CourseId int
SET @CourseId = (SELECT Course_Id FROM Faculty WHERE Faculty_Id = @Id)
DELETE FROM Faculty Where Faculty_Id=@Id
DECLARE @status int
SET @status=1
END TRY
BEGIN CATCH
END CATCH
If(@status=1)
BEGIN
DELETE FROM Users WHERE Login_Id=@Id
IF EXISTS(SELECT * FROM StudentDetails WHERE Course_Id = @CourseId AND Faculty_Id IS NULL)
BEGIN
UPDATE StudentDetails SET Faculty_Id = (SELECT TOP 1 Faculty_Id FROM Faculty WHERE Course_Id = @CourseId)
END
END
ELSE
BEGIN
UPDATE StudentDetails SET Faculty_Id=@Id
END
END
GO

-- Admin Delete student details
CREATE PROCEDURE usp_DeleteStudentDetails (@Id int)
AS
BEGIN
BEGIN TRY
DELETE FROM StudentDetails WHERE Student_Id=@Id
DECLARE @status int
SET @status=1
END TRY
BEGIN CATCH
END CATCH
If(@status=1)
BEGIN
DELETE FROM Users WHERE Login_Id=@Id
END
END
GO


--Check Student/Faculty/Admin Login Credentials
CREATE PROCEDURE usp_CheckLoginCredentials(@id int, @password varchar(30), @type varchar(10) output)
AS
BEGIN
IF EXISTS(SELECT * FROM Users WHERE Login_Id = @Id AND Login_Password = @password)
BEGIN
SET @type = (SELECT User_Type FROM Users WHERE Login_Id = @id)
END
ELSE
BEGIN
SET @type = '0'
END
END
GO



-- Get courses from courses table (Course ID and Course name)
CREATE PROCEDURE usp_GetCourses
AS
BEGIN
SELECT * FROM Courses
END
GO


--Needed
-- Get aggregate marks
CREATE PROCEDURE usp_GetAggregateMarks(@Id int)
AS
BEGIN
SELECT ROUND(AVG(marks), 2) FROM SemesterDetails WHERE Student_Id = @Id
END
GO


-- Admin update student - show results before update
CREATE PROCEDURE usp_AdminGetStudentDetailsBeforeupdate (@id int)
AS
BEGIN
SELECT * FROM StudentDetails, Courses, Users WHERE Student_Id = @Id AND Login_Id = @Id AND Courses.Course_Id = StudentDetails.Course_Id
END
GO



--View All Personal Details By Student
CREATE PROCEDURE usp_StudentViewPersonalDetails(@Id int)
AS
BEGIN
SELECT * FROM StudentDetails WHERE Student_Id=@Id
END
GO



--View All Student Details By Admin
CREATE PROCEDURE usp_AdminViewAllStudentDetails 
As
BEGIN
SELECT * FROM StudentDetails ,Courses, FeePayment
END
GO



--View All Faculty Details By Admin
CREATE PROCEDURE usp_AdminViewAllFacultyDetails 
As
BEGIN
SELECT * FROM Faculty 
END
GO


--View Fee Payment Details By Student
CREATE PROCEDURE usp_StudentViewFeePayment(@Id int)
AS
BEGIN
SELECT Courses.Course_Id, Course_Name, Fee_Amount, Date_Of_Payment, Last_Date, IsPaid FROM FeePayment, Courses Where Courses.Course_Id = FeePayment.Course_Id AND FeePayment.Student_Id=@Id
END
GO


--View Student Details By Faculty Before Update
CREATE PROCEDURE usp_FacultyViewStudentDetailsBeforeUpdate(@Id int)
AS
BEGIN
SELECT TOP 1 StudentDetails.Student_Id, Student_Name, Email_Address,Gender,DateOfBirth,Father_Name,Father_Phone, Father_Email, Courses.Course_Id,Course_Name, marks,semester FROM StudentDetails,Courses,SemesterDetails WHERE StudentDetails.Student_Id=@Id  AND SemesterDetails.Student_Id=@Id AND Courses.Course_Id = StudentDetails.Course_Id
END
GO 


--Update Semester Details by Faculty
CREATE PROCEDURE usp_UpdateSemesterDetailsByFaculty(@Id int, @Semester varchar(20) , @marks float)
AS
BEGIN
UPDATE SemesterDetails SET marks=@marks WHERE Student_id=@id AND semester=CONVERT(int, @Semester)
END
GO


--View Student Semester Details By Faculty
CREATE PROCEDURE usp_ViewStudentSemesterDetailsByFaculty (@Id int)
AS
BEGIN
SELECT StudentDetails.Student_Id, Student_Name, Courses.Course_Id, Course_Name, semester, marks FROM StudentDetails, Courses, SemesterDetails WHERE StudentDetails.Student_Id=@Id AND Courses.Course_Id=StudentDetails.Course_Id AND StudentDetails.Student_id=SemesterDetails.Student_Id
END
GO


-- Fetch Courses table
CREATE PROCEDURE usp_GetCoursesTableContents (@CourseId int)
AS
BEGIN
SELECT * FROM Courses WHERE Course_Id = @CourseId
END
GO




-- Get Details of newly added user
CREATE PROCEDURE usp_GetNewUserCredentials(@type varchar(20))
AS
BEGIN
SELECT TOP 1 * FROM Users WHERE User_Type=@type ORDER BY Registration_Date desc
END
GO



--View All Student Academic Details for Faculty
CREATE PROCEDURE usp_ViewAllStudentsAcademicDetails
AS
BEGIN
SELECT * FROM SemesterDetails 
END
GO



CREATE PROCEDURE usp_AdminStudentDashboard
AS
BEGIN
select StudentDetails.Student_Id, Student_Name, Course_Name from StudentDetails,Courses WHERE Courses.Course_Id= StudentDetails.Course_Id
END
GO



CREATE PROCEDURE usp_GetUserLoginDetails (@id int, @type varchar(20))
AS 
BEGIN
SELECT * FROM Users WHERE Login_Id = @id AND User_Type = @type
END
GO



CREATE PROCEDURE usp_FetchSemesterMarks(@Id int, @Semester varchar(20))
AS
BEGIN
SELECT marks FROM SemesterDetails WHERE semester=CONVERT(int,@Semester) AND SemesterDetails.Student_Id=@Id
END
GO


--View All Semester Details By Student
CREATE PROCEDURE usp_StudentViewSemesterDetails(@StudentId int, @CourseId int)
AS
BEGIN
SELECT Courses.Course_Id,Course_Name,semester,marks FROM SemesterDetails, Courses WHERE Student_Id=@StudentId AND Courses.Course_Id = @CourseId
END
GO


-- Get semester count for course id
CREATE PROCEDURE usp_GetSemesterCount (@CourseId int)
AS
BEGIN
SELECT Semester_Count FROM Courses WHERE Course_Id = @CourseId
END
GO



-- Faculty Get student list
CREATE PROCEDURE usp_FacultyStudentList (@FacultyId int)
AS
BEGIN
select StudentDetails.Student_Id, Student_Name, Course_Name from StudentDetails, Courses WHERE StudentDetails.Faculty_Id = @FacultyId AND Courses.Course_Id = (SELECT Course_Id FROM Faculty WHERE Faculty_Id = @FacultyId)
END
GO



--View Event in Student and Teacher
CREATE PROCEDURE usp_FetchEventDetails
AS
BEGIN
SELECT * FROM CollegeDetailsAndEvents
END
GO


CREATE PROCEDURE usp_AdminFacultyDashboard
AS
BEGIN
SELECT Faculty_Id, Faculty_Name, Course_Name FROM Faculty, Courses WHERE Faculty.Course_Id = Courses.Course_ID
END
GO


CREATE PROCEDURE usp_GetUsers
AS
BEGIN
SELECT * FROM Users
END
GO

--View Faculty Personal Details By Faculty
CREATE PROCEDURE usp_FacultyViewPersonalDetails(@Id int)
AS
BEGIN
SELECT * FROM Faculty, Courses WHERE Faculty_Id=@Id AND Faculty.Course_Id = Courses.Course_Id 
END
GO



-- Admin Search Student by ID
CREATE PROCEDURE usp_AdminSearchStudentDashboard (@Id int)
AS
BEGIN
select StudentDetails.Student_Id, Student_Name, Course_Name from StudentDetails,Courses WHERE Courses.Course_Id= StudentDetails.Course_Id AND StudentDetails.Student_Id = @Id
END
GO



-- Admin Search Faculty by ID
CREATE PROCEDURE usp_AdminSearchFacultyDashboard (@Id int)
AS
BEGIN
SELECT Faculty_Id, Faculty_Name, Course_Name FROM Faculty, Courses WHERE Faculty.Course_Id = Courses.Course_ID AND Faculty_Id = @Id
END
GO


-- Faculty Get student list
CREATE PROCEDURE usp_FacultySearchStudent (@FacultyId int, @StudentId int)
AS
BEGIN
select StudentDetails.Student_Id, Student_Name, Course_Name from StudentDetails, Courses WHERE StudentDetails.Faculty_Id = @FacultyId AND Courses.Course_Id = (SELECT Course_Id FROM Faculty WHERE Faculty_Id = @FacultyId) AND StudentDetails.Student_Id = @StudentId
END
GO


-- Admin Get Faculty Details before update
CREATE PROCEDURE usp_AdminGetFacultyDetailsBeforeUpdate (@Id int)
AS
BEGIN
SELECT * FROM Faculty, Courses, Users WHERE Faculty_Id = @Id AND Courses.Course_Id = Faculty.Course_Id AND Faculty_Id = Login_Id
END
GO




--Add college event Details
CREATE PROCEDURE usp_AddEventDetails(@Name varchar(50),@Description varchar(1000), @DOE Date, @DOC Date)
AS
BEGIN
INSERT INTO CollegeDetailsAndEvents (Event_Name,Event_Description,Event_Date,Date_Of_Creation) VALUES (@Name,@Description, @DOE, @DOC)
END
GO



--Fetch Event Details
CREATE PROCEDURE usp_AdminGetEventDetails(@Name varchar(50))
AS
BEGIN
SELECT * FROM CollegeDetailsAndEvents WHERE Event_Name=@Name
END
GO



--Modify Event by Admin
CREATE PROCEDURE usp_ModifyEventByAdmin(@Name varchar(50),@Description varchar(1000) , @DOE Date, @DOC Date)
AS
BEGIN
UPDATE CollegeDetailsAndEvents 
SET
Event_Description=@Description,
Event_Date=@DOE,
Date_Of_Creation=@DOC
WHERE Event_Name=@Name
END
GO



--Delete Event By Admin
CREATE PROCEDURE usp_DeleteEventDetails (@Name varchar(50))
AS
BEGIN
DELETE FROM CollegeDetailsAndEvents WHERE Event_Name=@Name
END
GO



--View Event by Admin in gridview
CREATE PROCEDURE usp_EventView
AS
BEGIN
SELECT Event_Name ,Event_Date FROM CollegeDetailsAndEvents
END
GO


--View All Event By Student and Faculty
CREATE PROCEDURE usp_StudentFacultyViewEvent 
AS
BEGIN
SELECT * FROM CollegeDetailsAndEvents
END
GO

-- Get Faculty_Id and Faculty_Name from student id
CREATE PROCEDURE usp_StudentGetCourseDetails(@StudentId int)
AS
BEGIN
DECLARE @CourseId int
SET @CourseId = (SELECT Course_Id FROM StudentDetails WHERE Student_Id = @StudentId)
SELECT * FROM (Select Courses.Course_Id, Courses.Course_Name, Faculty_Id, Faculty_Name FROM Courses LEFT OUTER JOIN Faculty ON Courses.Course_Id = @CourseId AND Courses.Course_Id = Faculty.Course_Id) AS Result WHERE Result.Course_Id = @CourseId
END
GO
-- END PROCEDURES


