CREATE FUNCTION [dbo].[GetOnlineMinutes]
(
	@SerialNumber VARCHAR(50),
	@Date DATE = NULL
)
RETURNS INT
AS
BEGIN
	SET @Date = CONVERT(DATE, @Date)
	DECLARE @Minutes INT = 0
	DECLARE @PrevLogon DATETIME = NULL
	DECLARE @PrevAction INT = 0
	DECLARE @LastID INT = (SELECT TOP 1 [Id] FROM [CompleteLog] WHERE [SerialNumber] = @SerialNumber AND CONVERT(DATE, [TimeStamp]) = @Date ORDER BY [TimeStamp] DESC)

	SELECT
		@Minutes =	CAST(CASE
						WHEN @PrevAction = 0 AND [Parameters] = '/logoff' THEN DATEDIFF(MINUTE, @Date, [TimeStamp])
						WHEN @PrevAction = 1 AND ([Parameters] = '/logon' OR [Parameters] = '/logoff') THEN @Minutes + DATEDIFF(MINUTE, @PrevLogon, [TimeStamp])
						ELSE @Minutes
					END AS INT),
		@Minutes = IIF(@LastID = [ID] AND [Parameters] = '/logon', IIF(@Date = CONVERT(DATE, GETDATE()), @Minutes + DATEDIFF(MINUTE, [TimeStamp], GETDATE()), @Minutes + DATEDIFF(MINUTE, [TimeStamp], DATEADD(DAY, 1, @Date))), @Minutes),
		@PrevLogon = IIF([Parameters] = '/logon', [TimeStamp], @PrevLogon),
		@PrevAction = IIF([Parameters] = '/logon', 1, 2)
	FROM
		[Log]
	WHERE
		[SerialNumber] = @SerialNumber AND CONVERT(DATE, [TimeStamp]) = @Date

	RETURN @Minutes
END