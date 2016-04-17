/**
* Add Unique contraints where appropriate
*/

ALTER TABLE Company
ADD UNIQUE (CompanyName)

GO

ALTER TABLE LanguageCompetence
ADD CONSTRAINT User_Language UNIQUE (LanguageName, CreatedById)

GO

ALTER TABLE LanguageCompetenceLevel
ADD UNIQUE (LevelName)

GO

ALTER TABLE Salutations
ADD UNIQUE (ShortName)

GO

ALTER TABLE Salutations
ADD UNIQUE (LongName)

GO

ALTER TABLE Skill
ADD CONSTRAINT User_Skill UNIQUE (SkillName, CreatedById)

GO

ALTER TABLE SkillLevel
ADD UNIQUE (LevelName)

GO



