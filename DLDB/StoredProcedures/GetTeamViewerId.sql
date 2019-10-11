CREATE PROCEDURE [dbo].[GetTeamViewerId]
	@SearchTerm NVARCHAR(50)
AS
	IF EXISTS(SELECT [Id] FROM [ComputerInfo] WHERE [SerialNumber] = @SearchTerm)
	BEGIN
		SELECT		[SerialNumber],
					MAX([TimeStamp]) AS [TimeStamp],
					[TeamViewerId]
		FROM		[CompleteLog]
		WHERE		[SerialNumber] = @SearchTerm
		GROUP BY	[SerialNumber],
					[TeamViewerId]
		ORDER BY	[TimeStamp] DESC
	END
	ELSE
	BEGIN
		SELECT		[SerialNumber],
					MAX([TimeStamp]) AS [TimeStamp],
					[TeamViewerId]
		FROM		[CompleteLog]
		WHERE		[Username] = @SearchTerm
		GROUP BY	[SerialNumber],
					[TeamViewerId]
		ORDER BY	[TimeStamp] DESC
	END
RETURN 0