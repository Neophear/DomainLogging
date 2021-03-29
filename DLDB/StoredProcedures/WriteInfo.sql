CREATE PROCEDURE [dbo].[WriteInfo]
	@Name NVARCHAR(50),
	@Model NVARCHAR(100),
	@OS NVARCHAR(100),
	@SerialNumber NVARCHAR(50),
	@CPU NVARCHAR(100),
	@CPUCores NVARCHAR(50),
	@RAM NVARCHAR(100),
	@DiskType NVARCHAR(50),
	@DiskSize NVARCHAR(100),
	@GFX NVARCHAR(100),
	@TeamViewerId NVARCHAR(50),
	@Id INT OUTPUT
AS
	SET @Id = (SELECT [Id] FROM [ComputerInfo] WHERE [SerialNumber] = @SerialNumber)

	IF @Id IS NULL
	BEGIN
		INSERT INTO [ComputerInfo] ([Name], [Model], [OS], [SerialNumber], [CPU], [CPUCores], [RAM], [DiskType], [DiskSize], [GFX], [TeamViewerId], [LastAlive]) VALUES(@Name, @Model, @OS, @SerialNumber, @CPU, @CPUCores, @RAM, @DiskType, @DiskSize, @GFX, @TeamViewerId, GETDATE())
		SET @Id = SCOPE_IDENTITY()
	END
	ELSE
		UPDATE [ComputerInfo] SET [Name] = @Name, [Model] = @Model, [OS] = @OS, [SerialNumber] = @SerialNumber, [CPU] = @CPU, [CPUCores] = @CPUCores, [RAM] = @RAM, [DiskType] = @DiskType, [DiskSize] = @DiskSize, [GFX] = @GFX, [TeamViewerId] = @TeamViewerId, [LastAlive] = GETDATE() WHERE [Id] = @Id
RETURN 0