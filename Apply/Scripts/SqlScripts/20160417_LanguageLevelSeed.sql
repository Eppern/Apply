/**
* Seed LanguageCompetenceLevel
*/

Delete from [LanguageCompetenceLevel]

GO

DBCC CHECKIDENT ([LanguageCompetenceLevel], RESEED, 0);
GO

Declare @user nvarchar(128);
Set @user = (select top 1 Id from AspNetUsers);

INSERT INTO [dbo].[LanguageCompetenceLevel] 
VALUES
('Anfänger', @user, @user, GetDate(), GETDATE()),
('Grundlegende Kenntnisse', @user, @user, GetDate(), GETDATE()),
('Fortgeschrittene Sprachverwendung', @user, @user, GetDate(), GETDATE()),
('Selbständige Sprachverwendung', @user, @user, GetDate(), GETDATE()),
('Fachkundige Sprachkenntnisse', @user, @user, GetDate(), GETDATE()),
('Annähernd muttersprachliche Kenntnisse', @user, @user, GetDate(), GETDATE()),
('Muttersprache', @user, @user, GetDate(), GETDATE());

/*Test*/
Select * from [LanguageCompetenceLevel]