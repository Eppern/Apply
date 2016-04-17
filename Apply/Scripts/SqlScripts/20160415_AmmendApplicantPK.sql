/**
* Change the Applicant Primary Key to nvarchar(128) to mirror the AspNetUser Id.
* Ensure SqlCmd mode is enabled
*/


GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Apply"
:setvar DefaultFilePrefix "Apply"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Überprüfen Sie den SQLCMD-Modus, und deaktivieren Sie die Skriptausführung, wenn der SQLCMD-Modus nicht unterstützt wird.
Um das Skript nach dem Aktivieren des SQLCMD-Modus erneut zu aktivieren, führen Sie folgenden Befehl aus:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Der SQLCMD-Modus muss aktiviert sein, damit dieses Skript erfolgreich ausgeführt werden kann.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO

IF (SELECT OBJECT_ID('tempdb..#tmpErrors')) IS NOT NULL DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
BEGIN TRANSACTION
GO
PRINT N'[dbo].[fk_Applicant_Professions_Applicant] wird gelöscht....';


GO
ALTER TABLE [dbo].[Applicant_Professions] DROP CONSTRAINT [fk_Applicant_Professions_Applicant];


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'unbenannte Einschränkungen auf [dbo].[Applicant] wird gelöscht....';


GO
ALTER TABLE [dbo].[Applicant] DROP CONSTRAINT [PK__Applican__39AE91A8043E25B6];


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO

IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT N'Der transaktive Teil des Datenbankupdates wurde erfolgreich durchgeführt.'
COMMIT TRANSACTION
END
ELSE PRINT N'Fehler beim transaktiven Teil des Datenbankupdates.'
GO
DROP TABLE #tmpErrors
GO
PRINT N'Update abgeschlossen.';


GO

--change data type
ALTER TABLE Applicant
DROP COLUMN ApplicantId;
ALTER TABLE Applicant
ADD ApplicantId nvarchar(128) not null

GO

--add primary key
ALTER TABLE Applicant
ADD CONSTRAINT PK_Applicant PRIMARY KEY (ApplicantId)

GO

--update references
ALTER TABLE Applicant_Professions
ALTER COLUMN ApplicantId nvarchar(128) NOT NULL

GO

ALTER TABLE Applicant_Professions
ADD FOREIGN KEY (ApplicantId)
REFERENCES Applicant(ApplicantId)

GO

ALTER TABLE Applicant
ALTER COLUMN AddressId int NULL
ALTER TABLE Applicant
ALTER COLUMN SalutationId int NULL
ALTER TABLE Applicant
ALTER COLUMN CVId int NULL