---------------------------------------------------------------------------
------------------------------creating tables-------------------------------
--------------------------------------------------------------------------- 
IF not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Person]') AND type in (N'U'))
 CREATE TABLE [dbo].[Person](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Gender] [smallint] NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]

GO
-----------------------------------------------------------------------------------------------------
IF not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hobby]') AND type in (N'U'))

CREATE TABLE [dbo].[Hobby](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Hobby] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]

GO
-----------------------------------------------------------------------------------------------------

IF not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonHobbies]') AND type in (N'U'))
CREATE TABLE [dbo].[PersonHobbies](
	[Id] [int] NOT NULL  IDENTITY(1,1) PRIMARY KEY,
	[PersonID] [int]  NOT NULL FOREIGN KEY REFERENCES Person(id),
	[HobbyID] [int] NOT NULL  FOREIGN KEY REFERENCES Hobby(id),
) ON [PRIMARY]
GO

---------------------------------------------------------------------------
------------------------------creating views-------------------------------
--------------------------------------------------------------------------- 
IF EXISTS(select * FROM sys.views where name = 'vPersonHobbies')
drop view vPersonHobbies
go
 create view vPersonHobbies as
 select p.* ,h.Id as HobbyID,h.name as HobbyName from person p inner join PersonHobbies ph on p.Id=ph.PersonID
 inner join Hobby h on h.Id=ph.HobbyID
 go


---------------------------------------------------------------------------
-------------------------creating stored procedures------------------------
--------------------------------------------------------------------------- 
IF OBJECT_ID('sp_getPersonsByHobbies', 'P') IS NOT NULL
drop PROCEDURE sp_getPersonsByHobbies 
go
CREATE PROCEDURE sp_getPersonsByHobbies @HobbiesList NVARCHAR(MAX) 
AS
BEGIN
  CREATE TABLE #HobbyList (Hobby NVARCHAR(40));
  INSERT INTO #HobbyList (Hobby)
    SELECT value FROM STRING_SPLIT(@HobbiesList,',');
  select distinct p.* from (select id from(select id ,count(hobbyName ) hobs from vPersonHobbies where hobbyName in(select Hobby from #HobbyList) group by(id))t 
   where t.hobs>(LEN(@HobbiesList) - LEN(REPLACE(@HobbiesList, ',', ''))))l inner join Person p on p.id=l.id
END
GO

 


-----
IF OBJECT_ID('sp_getPersons', 'P') IS NOT NULL
drop PROCEDURE sp_getPersons
go
CREATE PROCEDURE sp_getPersons
AS
select id,Name,Gender from person
GO

------------------------------------------------------------------------------

IF OBJECT_ID('sp_getPersonHobbiesList', 'P') IS NOT NULL
drop PROCEDURE sp_getPersonHobbiesList
go
CREATE PROCEDURE sp_getPersonHobbiesList @PersonId int
AS
SELECT  HobbyID ,HobbyName from vPersonHobbies
where id=@PersonId

GO


---------------------------------------------------------------------------
------------------------------ inserting data------------------------------
---------------------------------------------------------------------------
--person table--

 IF ( SELECT COUNT(1) FROM Person WHERE id=1) =0	
 INSERT INTO [dbo].[Person]([Id],[Name],[Gender])VALUES (1,'Jimmy Lee',1)
 go

  IF ( SELECT COUNT(1) FROM Person WHERE id=2) =0 	 
  INSERT INTO [dbo].[Person]([Id],[Name],[Gender])VALUES (2,'Kathy Shure',2)  
  go

 IF ( SELECT COUNT(1) FROM Person WHERE id=3) =0 	 
 INSERT INTO [dbo].[Person]([Id],[Name],[Gender])VALUES (3,'Ellen Mark',3) 
 go

 IF ( SELECT COUNT(1) FROM Person WHERE id=4) =0 	 
 INSERT INTO [dbo].[Person]([Id],[Name],[Gender])VALUES (4,'Maya Agda',1) 
 go

 IF ( SELECT COUNT(1) FROM Person WHERE id=5) =0 	 
 INSERT INTO [dbo].[Person]([Id],[Name],[Gender])VALUES (5,'Jen Hoper',1) 
 go

 IF ( SELECT COUNT(1) FROM Person WHERE id=7) =0 	 
 INSERT INTO [dbo].[Person]([Id],[Name],[Gender])VALUES (7,'John Wick',2) 
 go

 IF ( SELECT COUNT(1) FROM Person WHERE id=8)  =0	 
 INSERT INTO [dbo].[Person]([Id],[Name],[Gender])VALUES (8,'Jonathan Calm',2) 
 go

 IF ( SELECT COUNT(1) FROM Person WHERE id=9) =0 	
 INSERT INTO [dbo].[Person]([Id],[Name],[Gender])VALUES (9,'Natasha Bloom',1) 
 go

 IF ( SELECT COUNT(1) FROM Person WHERE id=10) =0 	 
 INSERT INTO [dbo].[Person]([Id],[Name],[Gender])VALUES (10,'Noel Bow',2) 
 go


 ----------Hobby table-------

 
 IF ( SELECT COUNT(1) FROM Hobby WHERE id=1) = 0 	 
 INSERT INTO [dbo].[Hobby]([Id],[Name])VALUES (1,'Music') 
 go
 IF ( SELECT COUNT(1) FROM Hobby WHERE id=2) = 0 	 	 
 INSERT INTO [dbo].[Hobby]([Id],[Name])VALUES (2,'Sleeping') 
 go
 IF ( SELECT COUNT(1) FROM Hobby WHERE id=3) = 0 	 	 
 INSERT INTO [dbo].[Hobby]([Id],[Name])VALUES (3,'Animation') 
 go
 IF ( SELECT COUNT(1) FROM Hobby WHERE id=4) = 0 	 	 
 INSERT INTO [dbo].[Hobby]([Id],[Name])VALUES (4,'Video Games') 
 go
 IF ( SELECT COUNT(1) FROM Hobby WHERE id=5) = 0 	 	 
 INSERT INTO [dbo].[Hobby]([Id],[Name])VALUES (5,'Cooking') 
 go



 ---------

 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=1 and HobbyID =1) = 0	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (1,1)
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=1 and HobbyID =2) = 0	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (1,2) 
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=1 and HobbyID =3) = 0	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (1,3)
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=1 and HobbyID =4) = 0
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (1,4) 
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=2 and HobbyID =1) = 0	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (2,1) 
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=2 and HobbyID =2) = 0	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (2,2) 
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=3 and HobbyID =3) = 0	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (3,3) 
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=3 and HobbyID =4) = 0	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (3,4) 
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=3 and HobbyID =5) = 0	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (3,5) 
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=4 and HobbyID =1) = 0	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (4,1) 
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=4 and HobbyID =3) = 0	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (4,3) 
 go
  IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=4 and HobbyID =5) = 0
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (4,5) 
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=5 and HobbyID =1) = 0	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (5,1) 
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=5 and HobbyID =3) = 0	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (5,3) 
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=5 and HobbyID =4) = 0	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (5,4) 
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=7 and HobbyID =1) = 0	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (7,1) 
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=8 and HobbyID =2) = 0	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (8,2) 
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=9 and HobbyID =3) =0 	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (9,3) 
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=10 and HobbyID =4) =0 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (10,4) 
 go
 IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=10 and HobbyID =1) =0 	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (10,1) 
 go
  IF ( SELECT COUNT(1) FROM PersonHobbies WHERE PersonID=10 and HobbyID =5) = 0	 
 INSERT INTO [dbo].[PersonHobbies]([PersonID],[HobbyID])VALUES (10,5) 
 go




