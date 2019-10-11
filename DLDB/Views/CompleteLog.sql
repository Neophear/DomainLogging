CREATE VIEW [dbo].[CompleteLog]
	AS
SELECT			L.[Id],
				[TimeStamp],
				C.[SerialNumber],
				C.[Name],
				[Username],
				[TeamViewerId],
				[Parameters] 
FROM			[Log] L
LEFT OUTER JOIN	[ComputerInfo] C ON L.[ComputerRefId] = C.Id