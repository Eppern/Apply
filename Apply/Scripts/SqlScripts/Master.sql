/**
* Before running script please 
* run program and register a user
* in order to create the Identity Tables
*/

/**
* Create Tables
*/

CREATE TABLE CV (
	CVId int Primary Key Identity (1,1) not null,
	CreatedById nvarchar(128) not null,
	ModifiedById nvarchar(128) not null,
	DateCreated datetime default CURRENT_TIMESTAMP not null,
	DateModified datetime default CURRENT_TIMESTAMP not null,

	Constraint fk_cv_createdBy foreign key (CreatedById) references AspNetUsers(Id),
	Constraint fk_cv_modifiedBy foreign key (ModifiedById) references AspNetUsers(Id)
);

GO

CREATE TABLE Education (
	EducationId int Primary Key Identity (1,1) not null,
	MonthStart nvarchar(5) not null,
	MonthEnd nvarchar(5) not null,
	YearStart int not null,
	YearEnd int not null,
	InstitutionName nvarchar(256) not null,
	Notes nvarchar(max),

	CreatedById nvarchar(128) not null,
	ModifiedById nvarchar(128) not null,
	DateCreated datetime default CURRENT_TIMESTAMP not null,
	DateModified datetime default CURRENT_TIMESTAMP not null,

	Constraint fk_Education_createdBy foreign key (CreatedById) references AspNetUsers(Id),
	Constraint fk_Education_modifiedBy foreign key (ModifiedById) references AspNetUsers(Id)
);

GO

CREATE TABLE LanguageCompetenceLevel (
	LanguageCompetenceLevelId int Primary Key Identity (1,1) not null,
	LevelName nvarchar(128) not null,
	CreatedById nvarchar(128) not null,
	ModifiedById nvarchar(128) not null,
	DateCreated datetime default CURRENT_TIMESTAMP not null,
	DateModified datetime default CURRENT_TIMESTAMP not null,

	Constraint fk_LanguageCompetenceLevel_createdBy foreign key (CreatedById) references AspNetUsers(Id),
	Constraint fk_LanguageCompetenceLevel_modifiedBy foreign key (ModifiedById) references AspNetUsers(Id)
);

GO

CREATE TABLE LanguageCompetence (
	LanguageCompetenceId int Primary Key Identity (1,1) not null,
	LanguageName nvarchar(128) not null,
	CreatedById nvarchar(128) not null,
	ModifiedById nvarchar(128) not null,
	DateCreated datetime default CURRENT_TIMESTAMP not null,
	DateModified datetime default CURRENT_TIMESTAMP not null,
	LanguageCompetenceLevelId int not null,

	Constraint fk_LanguageCompetence_LanguageCompetenceLevel foreign key (LanguageCompetenceLevelId) references LanguageCompetenceLevel(LanguageCompetenceLevelId),
	Constraint fk_LanguageCompetence_createdBy foreign key (CreatedById) references AspNetUsers(Id),
	Constraint fk_LanguageCompetence_modifiedBy foreign key (ModifiedById) references AspNetUsers(Id)
);

GO

CREATE TABLE SkillLevel (
	SkillLevelId int Primary Key Identity (1,1) not null,
	LevelName nvarchar(128) not null,
	CreatedById nvarchar(128) not null,
	ModifiedById nvarchar(128) not null,
	DateCreated datetime default CURRENT_TIMESTAMP not null,
	DateModified datetime default CURRENT_TIMESTAMP not null,

	Constraint fk_SkillLevel_createdBy foreign key (CreatedById) references AspNetUsers(Id),
	Constraint fk_SkillLevel_modifiedBy foreign key (ModifiedById) references AspNetUsers(Id)
);

GO

CREATE TABLE Skill (
	SkillId int Primary Key Identity (1,1) not null,
	SkillName nvarchar(128) not null,
	CreatedById nvarchar(128) not null,
	ModifiedById nvarchar(128) not null,
	DateCreated datetime default CURRENT_TIMESTAMP not null,
	DateModified datetime default CURRENT_TIMESTAMP not null,

	SkillLevelId int not null,

	Constraint fk_Skill_SkillLevel foreign key (SkillLevelId) references SkillLevel(SkillLevelId),
	Constraint fk_Skill_createdBy foreign key (CreatedById) references AspNetUsers(Id),
	Constraint fk_Skill_modifiedBy foreign key (ModifiedById) references AspNetUsers(Id)
);

GO

CREATE TABLE WorkExperience (
	WorkExperienceId int Primary Key Identity (1,1) not null,
	CreatedById nvarchar(128) not null,
	ModifiedById nvarchar(128) not null,
	DateCreated datetime default CURRENT_TIMESTAMP not null,
	DateModified datetime default CURRENT_TIMESTAMP not null,
	MonthStart nvarchar(5) not null,
	MonthEnd nvarchar(5) not null,
	YearStart int not null,
	YearEnd int not null,
	CompanyName nvarchar(256) not null,
	PositionHeld nvarchar(max),
	Notes nvarchar(max),

	Constraint fk_WorkExperience_createdBy foreign key (CreatedById) references AspNetUsers(Id),
	Constraint fk_WorkExperience_modifiedBy foreign key (ModifiedById) references AspNetUsers(Id)
);

GO

CREATE TABLE Addresses (
	AddressId int Primary Key Identity (1,1) not null,
	Street nvarchar(256) null,
	PostCode nvarchar(128) null,
	Town nvarchar(256) null,
	Country nvarchar(128) null,
	[State] nvarchar(256) null,
	HouseNr nvarchar(256) null,
	DoorNr nvarchar(256) null,

	CreatedById nvarchar(128) not null,
	ModifiedById nvarchar(128) not null,
	DateCreated datetime default CURRENT_TIMESTAMP not null,
	DateModified datetime default CURRENT_TIMESTAMP not null,

	Constraint fk_Address_createdBy foreign key (CreatedById) references AspNetUsers(Id),
	Constraint fk_Address_modifiedBy foreign key (ModifiedById) references AspNetUsers(Id)
);

GO

CREATE TABLE Salutations (
    SalutationId int Primary Key Identity (1,1) not null,
	ShortName nvarchar(16) not null,
	LongName nvarchar(64) not null,
	CreatedById nvarchar(128) not null,
	ModifiedById nvarchar(128) not null,
	DateCreated datetime default CURRENT_TIMESTAMP not null,
	DateModified datetime default CURRENT_TIMESTAMP not null,

	Constraint fk_Salutation_createdBy foreign key (CreatedById) references AspNetUsers(Id),
	Constraint fk_Salutation_modifiedBy foreign key (ModifiedById) references AspNetUsers(Id)
);

GO

CREATE TABLE Contacts (
	ContactId int Primary Key Identity (1,1) not null,
	SalutationId int not null,
	AddressId int not null,
	CreatedById nvarchar(128) not null,
	ModifiedById nvarchar(128) not null,
	DateCreated datetime default CURRENT_TIMESTAMP not null,
	DateModified datetime default CURRENT_TIMESTAMP not null,

	Constraint fk_Contacts_Salutation foreign key (SalutationId) references Salutations(SalutationId),
	Constraint fk_Contacts_Address foreign key (AddressId) references Addresses(AddressId),
	Constraint fk_Contacts_createdBy foreign key (CreatedById) references AspNetUsers(Id),
	Constraint fk_Contacts_modifiedBy foreign key (ModifiedById) references AspNetUsers(Id)
); 

GO

CREATE TABLE TargetCompany (
	TargetCompanyId int Primary Key Identity (1,1) not null,
	CompanyName nvarchar(256) not null,
	Email nvarchar(256) not null,

	ContactId int not null,
	AddressId int not null,

	CreatedById nvarchar(128) not null,
	ModifiedById nvarchar(128) not null,
	DateCreated datetime default CURRENT_TIMESTAMP not null,
	DateModified datetime default CURRENT_TIMESTAMP not null,

	Constraint fk_TargetCompany_Contact foreign key (ContactId) references Contacts(ContactId),
	Constraint fk_TargetCompany_Address foreign key (AddressId) references Addresses(AddressId),
	Constraint fk_TargetCompany_createdBy foreign key (CreatedById) references AspNetUsers(Id),
	Constraint fk_TargetCompany_modifiedBy foreign key (ModifiedById) references AspNetUsers(Id)
);

GO

CREATE TABLE Applicant (
	ApplicantId nvarchar(256) Primary Key not null,
	ForeName nvarchar(256) not null,
	SurName nvarchar(256) not null,
	DOB Date,
	Title nvarchar(256),
	
	AddressId int not null,
	SalutationId int not null,
	CVId int not null,
	CreatedById nvarchar(128) not null,
	ModifiedById nvarchar(128) not null,
	DateCreated datetime default CURRENT_TIMESTAMP not null,
	DateModified datetime default CURRENT_TIMESTAMP not null,

	Constraint fk_Applicant_Addresses foreign key (AddressId) references Addresses(AddressId),
	Constraint fk_Applicant_Salutation foreign key (SalutationId) references Salutations(SalutationId),
	Constraint fk_Applicant_CV foreign key (CVId) references CV(CVId),
	Constraint fk_Applicant_createdBy foreign key (CreatedById) references AspNetUsers(Id),
	Constraint fk_Applicant_modifiedBy foreign key (ModifiedById) references AspNetUsers(Id)
);

GO

CREATE TABLE Applicant_Professions (
    Applicant_ProfessionsId int Primary Key Identity (1,1) not null,
    ApplicantId nvarchar(256) not null,
	Profession nvarchar(256) not null,
	
	CreatedById nvarchar(128) not null,
	ModifiedById nvarchar(128) not null,
	DateCreated datetime default CURRENT_TIMESTAMP not null,
	DateModified datetime default CURRENT_TIMESTAMP not null,

	Constraint fk_Applicant_Professions_Applicant foreign key (ApplicantId) references Applicant(ApplicantId),
	Constraint fk_Applicant_Professions_createdBy foreign key (CreatedById) references AspNetUsers(Id),
	Constraint fk_Applicant_Professions_modifiedBy foreign key (ModifiedById) references AspNetUsers(Id)
);

Create Table Company (
	CompanyId int Primary Key Identity(1,1),
	CompanyName nvarchar(128) not null,
	UserName nvarchar(128) not null,
	ContactName nvarchar(128) null,
	EmailAddress nvarchar(128) not null,
	Telephone nvarchar(128) not null,
	Website nvarchar(128) not null,

	AspNetUserId nvarchar(128) not null,

	Constraint FK_Company_AspNetUser Foreign Key (AspNetUserId) References AspNetUsers(Id)
);

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


/*************************************
************** SEED ******************
**************************************/

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


/**
* Seed SkillLevel
*/

DELETE FROM SkillLevel

GO

DBCC CHECKIDENT (SkillLevel, RESEED, 0);

GO

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

