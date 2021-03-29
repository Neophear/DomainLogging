CREATE PROCEDURE [dbo].[WriteLog]
	@Username NVARCHAR(50),
	@Name NVARCHAR(50),
	@Model NVARCHAR(50),
	@OS NVARCHAR(100),
	@SerialNumber NVARCHAR(50),
	@CPU NVARCHAR(100),
	@CPUCores NVARCHAR(50),
	@RAM NVARCHAR(100),
	@DiskType NVARCHAR(50),
	@DiskSize NVARCHAR(100),
	@GFX NVARCHAR(100),
	@TeamViewerId NVARCHAR(50),
	@Args NVARCHAR(255)
AS
	DECLARE @ComputerId INT
	EXEC dbo.WriteInfo @Name, @Model, @OS, @SerialNumber, @CPU, @CPUCores, @RAM, @DiskType, @DiskSize, @GFX, @TeamViewerId, @ComputerId OUTPUT

	INSERT INTO [Log] ([ComputerRefId], [Username], [Parameters]) VALUES(@ComputerId, @Username, @Args)
RETURN 0