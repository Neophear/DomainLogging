CREATE PROCEDURE [dbo].[WriteInfo]
	@Name NVARCHAR(50),
	@Model NVARCHAR(100),
	@OS NVARCHAR(100),
	@SerialNumber NVARCHAR(50),
	@TeamViewerId NVARCHAR(50),
	@Id INT OUTPUT
AS
	SET @Id = (SELECT [Id] FROM [ComputerInfo] WHERE [SerialNumber] = @SerialNumber)

	IF @Id IS NULL
	BEGIN
		INSERT INTO [ComputerInfo] ([Name], [Model], [OS], [SerialNumber], [TeamViewerId], [LastAlive]) VALUES(@Name, @Model, @OS, @SerialNumber, @TeamViewerId, GETDATE())
		SET @Id = SCOPE_IDENTITY()
	END
	ELSE
		UPDATE [ComputerInfo] SET [Name] = @Name, [Model] = @Model, [OS] = @OS, [SerialNumber] = @SerialNumber, [TeamViewerId] = @TeamViewerId, [LastAlive] = GETDATE() WHERE [Id] = @Id
RETURN 0