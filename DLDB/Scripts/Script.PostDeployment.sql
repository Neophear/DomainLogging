/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO [Actions] ([Name]) VALUES('Logon'), ('Logoff'), ('Error')
CREATE USER dldbw FOR LOGIN dldbw /*DLDB-write user*/
CREATE USER dldbs FOR LOGIN dldbs /*DLDB-select*/
EXEC sp_addrolemember N'db_datareader', N'dldbs'
GRANT EXECUTE ON dbo.WriteLog TO dldbw
GRANT EXECUTE ON dbo.GetOnlineMinutes TO dldbs
GRANT EXECUTE ON dbo.GetTeamViewerId TO dldbs