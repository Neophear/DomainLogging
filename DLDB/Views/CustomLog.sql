CREATE VIEW [dbo].[CustomLog]
	AS
SELECT	*
FROM	[CompleteLog]
WHERE	[Parameters] <> '/logon'
AND		[Parameters] <> '/logoff'