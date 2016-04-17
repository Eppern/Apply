/**
* Creates all tables. Run this only after executing the program and registering a user. 
* This ensures creation of the Asp.Net Identity Tables.
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
	MonthStart int not null,
	MonthEnd int not null,
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
	MonthStart int not null,
	MonthEnd int not null,
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
	ApplicantId int Primary Key Identity (1,1) not null,
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
    ApplicantId int not null,
	Profession nvarchar(256) not null,
	
	CreatedById nvarchar(128) not null,
	ModifiedById nvarchar(128) not null,
	DateCreated datetime default CURRENT_TIMESTAMP not null,
	DateModified datetime default CURRENT_TIMESTAMP not null,

	Constraint fk_Applicant_Professions_Applicant foreign key (ApplicantId) references Applicant(ApplicantId),
	Constraint fk_Applicant_Professions_createdBy foreign key (CreatedById) references AspNetUsers(Id),
	Constraint fk_Applicant_Professions_modifiedBy foreign key (ModifiedById) references AspNetUsers(Id)
);