GO

/****** Object:  StoredProcedure [dbo].[usp_GetUnitLeasePrice]    Script Date: 4/13/2020 8:45:00 PM ******/
DROP PROCEDURE [dbo].[usp_GetUnitLeasePrice]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetUnitLeasePrice]    Script Date: 4/13/2020 8:45:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_GetUnitLeasePrice]
	@PropertyId		INT=8,
	@PageNumber		INT=1,
	@NumberOfRows	INT=10,
	@SortBy			Varchar(20)='UnitNo',
	@OrderBy		Varchar(20)='ASC'
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @PropertyUnit TABLE (UID BIGINT, UnitNo VARCHAR(50), Building VARCHAR(100), UnitNoInt INT, NOP INT)

	DECLARE @PropertyUnitFinal TABLE (UID BIGINT, UnitNo VARCHAR(50), Building VARCHAR(100), UnitNoInt INT, NOP INT)

	DECLARE @UnitLeasePrice TABLE (UID BIGINT, UnitNo VARCHAR(50), Building VARCHAR(100), ULPID BIGINT, LeaseID INT, Price MONEY, NOP INT, UnitNoInt INT)

	DECLARE @RowCount			INT
	DECLARE	@TotalRows			BIGINT
	DECLARE @TotalNumberOfPages	BIGINT
	DECLARE @ModValue			BIGINT
	
	DECLARE @RowNumLower BIGINT
	DECLARE @RowNumUpper BIGINT

	INSERT INTO @PropertyUnit (UID, UnitNo, Building, UnitNoInt, NOP ) 
	SELECT PU.UID, PU.UnitNo, PU.Building,
	SUBSTRING(PU.UnitNo, PATINDEX('%[0-9]%', PU.UnitNo), PATINDEX('%[0-9][^0-9]%', PU.UnitNo + 't') - PATINDEX('%[0-9]%', PU.UnitNo) + 1),0
	FROM tbl_PropertyUnits PU WHERE PU.PID=@PropertyId

	INSERT INTO @UnitLeasePrice (UID, UnitNo, Building, ULPID, LeaseID, Price, NOP, UnitNoInt ) 
	SELECT PU.UID, PU.UnitNo, PU.Building, ULP.ULPID, ULP.LeaseID, ULP.Price, 0,
	SUBSTRING(PU.UnitNo, PATINDEX('%[0-9]%', PU.UnitNo), PATINDEX('%[0-9][^0-9]%', PU.UnitNo + 't') - PATINDEX('%[0-9]%', PU.UnitNo) + 1)
	FROM tbl_PropertyUnits PU INNER JOIN tbl_UnitLeasePrice ULP ON PU.UID=ULP.UnitID WHERE PU.PID=@PropertyId
	 
	SET @RowNumLower=(@PageNumber*@NumberOfRows-@NumberOfRows)+1
	SET @RowNumUpper=@PageNumber*@NumberOfRows
	 SET @RowCount=(SELECT COUNT(*) FROM @PropertyUnit)

	 IF @RowCount<@RowNumLower
	 BEGIN
		SET @RowNumLower=0
	 END
	
	SELECT @TotalRows=(SELECT COUNT(*) FROM @PropertyUnit)
	

	SET @TotalNumberOfPages=(@TotalRows/@NumberOfRows);
	SET @ModValue=(@TotalRows%@NumberOfRows);

	IF(@ModValue>0)
	BEGIN
		SET @TotalNumberOfPages=@TotalNumberOfPages+1;
	END

	UPDATE @PropertyUnit SET NOP=@TotalNumberOfPages
	
	IF(@OrderBy='ASC')
	BEGIN
		IF(@SortBy='UnitNo')
		BEGIN
			WITH FilterRows AS (SELECT (ROW_NUMBER() OVER (ORDER BY TD.UnitNoInt ASC)) AS RowNum, UID , UnitNo, Building, NOP,  UnitNoInt FROM @PropertyUnit TD)
			INSERT INTO @PropertyUnitFinal(UID , UnitNo, Building, NOP,  UnitNoInt)
			SELECT UID , UnitNo, Building, NOP, UnitNoInt
			FROM FilterRows FR  WHERE RowNum>=@RowNumLower AND RowNum<=@RowNumUpper ORDER BY RowNum 
			SELECT PU.UID, PU.UnitNo, PU.Building, ULP.ULPID, ULP.LeaseID, ULP.Price, PU.NOP FROM @PropertyUnitFinal PU INNER JOIN @UnitLeasePrice ULP ON PU.UID=ULP.UID

		END
		IF(@SortBy='Building')
		BEGIN
			WITH FilterRows AS (SELECT (ROW_NUMBER() OVER (ORDER BY TD.Building ASC)) AS RowNum, UID , UnitNo, Building, NOP,  UnitNoInt FROM @PropertyUnit TD)
			INSERT INTO @PropertyUnitFinal(UID , UnitNo, Building, NOP,  UnitNoInt)
			SELECT UID , UnitNo, Building, NOP, UnitNoInt
			FROM FilterRows FR  WHERE RowNum>=@RowNumLower AND RowNum<=@RowNumUpper ORDER BY RowNum 
			SELECT PU.UID, PU.UnitNo, PU.Building, ULP.ULPID, ULP.LeaseID, ULP.Price, PU.NOP FROM @PropertyUnitFinal PU INNER JOIN @UnitLeasePrice ULP ON PU.UID=ULP.UID
		END
	END;
	IF(@OrderBy='DESC')
	BEGIN
		IF(@SortBy='UnitNo')
		BEGIN
			WITH FilterRows AS (SELECT (ROW_NUMBER() OVER (ORDER BY TD.UnitNoInt DESC)) AS RowNum, UID , UnitNo, Building, NOP,  UnitNoInt FROM @PropertyUnit TD)
			INSERT INTO @PropertyUnitFinal(UID , UnitNo, Building, NOP,  UnitNoInt)
			SELECT UID , UnitNo, Building, NOP, UnitNoInt
			FROM FilterRows FR  WHERE RowNum>=@RowNumLower AND RowNum<=@RowNumUpper ORDER BY RowNum 
			SELECT PU.UID, PU.UnitNo, PU.Building, ULP.ULPID, ULP.LeaseID, ULP.Price, PU.NOP FROM @PropertyUnitFinal PU INNER JOIN @UnitLeasePrice ULP ON PU.UID=ULP.UID

		END
		IF(@SortBy='Building')
		BEGIN
			WITH FilterRows AS (SELECT (ROW_NUMBER() OVER (ORDER BY TD.Building DESC)) AS RowNum, UID , UnitNo, Building, NOP,  UnitNoInt FROM @PropertyUnit TD)
			INSERT INTO @PropertyUnitFinal(UID , UnitNo, Building, NOP,  UnitNoInt)
			SELECT UID , UnitNo, Building, NOP, UnitNoInt
			FROM FilterRows FR  WHERE RowNum>=@RowNumLower AND RowNum<=@RowNumUpper ORDER BY RowNum 
			SELECT PU.UID, PU.UnitNo, PU.Building, ULP.ULPID, ULP.LeaseID, ULP.Price, PU.NOP FROM @PropertyUnitFinal PU INNER JOIN @UnitLeasePrice ULP ON PU.UID=ULP.UID
		END
	END;
END
GO


