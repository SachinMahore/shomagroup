
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_UpdateUnitDepostiteAndModelName
	@OldModelName	VARCHAR(50),
	@NewModelName	VARCHAR(50),
	@Deposit		MONEY
AS
BEGIN
	UPDATE tbl_PropertyUnits SET Building=@NewModelName, Deposit=@Deposit WHERE Building=@OldModelName
END
GO
