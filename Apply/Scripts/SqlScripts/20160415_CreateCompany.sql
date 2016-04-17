/**
* Create Company Table
*/

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