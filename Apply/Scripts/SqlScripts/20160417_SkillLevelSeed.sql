/**
* Seed SkillLevel
*/

Declare @user nvarchar(128);
Set @user = (select top 1 Id from AspNetUsers);

INSERT INTO [dbo].[SkillLevel] 
VALUES
('10', @user, @user, GetDate(), GETDATE()),
('20', @user, @user, GetDate(), GETDATE()),
('30', @user, @user, GetDate(), GETDATE()),
('40', @user, @user, GetDate(), GETDATE()),
('50', @user, @user, GetDate(), GETDATE()),
('60', @user, @user, GetDate(), GETDATE()),
('70', @user, @user, GetDate(), GETDATE()),
('80', @user, @user, GetDate(), GETDATE()),
('90', @user, @user, GetDate(), GETDATE()),
('100', @user, @user, GetDate(), GETDATE());