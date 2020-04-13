
/****** Object:  StoredProcedure [dbo].[usp_GetClientModelUnitList]    Script Date: 4/13/2020 6:48:34 AM ******/
DROP PROCEDURE [dbo].[usp_GetClientModelUnitList]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetClientModelUnitList]    Script Date: 4/13/2020 6:48:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- EXEC [usp_GetClientModelUnitList] 8,'3/1/2020',0,0,10000,0
CREATE PROCEDURE [dbo].[usp_GetClientModelUnitList]
    @PID BIGINT,
	@AvailableDate		datetime,
	@FloorNo int=0,
	@Bedroom int=0,
	@Current_Rent decimal,
	@SortOrder int=0
AS
BEGIN
DECLARE @RentRange nvarchar(20)

DECLARE @ModelData TABLE (ModelID int, ModelName nvarchar(50),RentRange decimal, Bedroom int, Bathroom int, Area nvarchar(50),FloorPlan nvarchar(50), NoAvailable int)
DECLARE @RentRangeData TABLE (ModelName nvarchar(50),RentMinRange nvarchar(20),RentMaxRange  nvarchar(20), NoAvailable int)
DECLARE @UnitsNotAvailable TABLE (UnitId BIGINT)

INSERT INTO @UnitsNotAvailable (UnitId)
SELECT UID FROM tbl_Lease WHERE STATUS=1

INSERT INTO @UnitsNotAvailable (UnitId)
SELECT PropertyId FROM tbl_ApplyNow WHERE LTRIM(RTRIM((ISNULL(Status,''))))!='Denied'

	IF (@Bedroom!=0)
	BEGIN
		INSERT INTO @ModelData (ModelID , ModelName ,RentRange, Bedroom , Bathroom , Area ,FloorPlan, NoAvailable)
		SELECT M.ModelID,M.ModelName,PU.Current_Rent as RentRange,M.Bedroom,M.Bathroom,M.Area,M.FloorPlan,1
		FROM tbl_PropertyUnits PU INNER JOIN tbl_Models M ON PU.Building=M.ModelName
		WHERE PU.AvailableDate<=@AvailableDate AND M.Bedroom=@Bedroom 
		AND PU.Current_Rent<=@Current_Rent 
		AND PU.UID NOT IN (SELECT UnitId FROM @UnitsNotAvailable)

		INSERT INTO @RentRangeData (ModelName,RentMinRange,RentMaxRange,NoAvailable)
		SELECT M.ModelName, M.MinRent, M.MaxRent, COUNT(*)  FROM tbl_Models M INNER JOIN @ModelData MD ON M.ModelID=MD.ModelID
		GROUP BY M.ModelName, M.MinRent, M.MaxRent

		IF(@SortOrder=0)
		BEGIN
			SELECT DISTINCT MD.ModelName,'$'+RR.RentMinRange +' - $'+RR.RentMaxRange as RentRange,RR.NoAvailable,MD.Bathroom,MD.Bedroom,MD.Area,MD.FloorPlan, RR.RentMinRange  FROM @ModelData MD INNER JOIN @RentRangeData  RR ON MD.ModelName=RR.ModelName ORDER BY RR.RentMinRange ASC, MD.ModelName
		END
		ELSE
		BEGIN
			SELECT DISTINCT MD.ModelName,'$'+RR.RentMinRange +' - $'+RR.RentMaxRange as RentRange,RR.NoAvailable,MD.Bathroom,MD.Bedroom,MD.Area,MD.FloorPlan, RR.RentMaxRange   FROM @ModelData MD INNER JOIN @RentRangeData  RR ON MD.ModelName=RR.ModelName ORDER BY RR.RentMaxRange DESC, MD.ModelName
		END
	END
	ELSE
	BEGIN
		INSERT INTO @ModelData (ModelID , ModelName ,RentRange, Bedroom , Bathroom , Area ,FloorPlan, NoAvailable)
		SELECT M.ModelID,M.ModelName,PU.Current_Rent as RentRange,M.Bedroom,M.Bathroom,M.Area,M.FloorPlan,1
		FROM tbl_PropertyUnits PU INNER JOIN tbl_Models M ON PU.Building=M.ModelName
		WHERE PU.AvailableDate<=@AvailableDate  AND PU.Current_Rent<=@Current_Rent AND PU.UID NOT IN (SELECT UnitId FROM @UnitsNotAvailable)

		INSERT INTO @RentRangeData (ModelName,RentMinRange,RentMaxRange,NoAvailable)
		SELECT M.ModelName, M.MinRent, M.MaxRent, COUNT(*)  FROM tbl_Models M INNER JOIN @ModelData MD ON M.ModelID=MD.ModelID
		GROUP BY M.ModelName, M.MinRent, M.MaxRent

		IF(@SortOrder=0)
		BEGIN
			SELECT DISTINCT MD.ModelName,'$'+RR.RentMinRange +' - $'+RR.RentMaxRange as RentRange,RR.NoAvailable,MD.Bathroom,MD.Bedroom,MD.Area,MD.FloorPlan, RR.RentMinRange  FROM @ModelData MD INNER JOIN @RentRangeData  RR ON MD.ModelName=RR.ModelName ORDER BY RR.RentMinRange ASC, MD.ModelName
		END
		ELSE
		BEGIN
			SELECT DISTINCT MD.ModelName,'$'+RR.RentMinRange +' - $'+RR.RentMaxRange as RentRange,RR.NoAvailable,MD.Bathroom,MD.Bedroom,MD.Area,MD.FloorPlan, RR.RentMaxRange   FROM @ModelData MD INNER JOIN @RentRangeData  RR ON MD.ModelName=RR.ModelName ORDER BY RR.RentMaxRange DESC, MD.ModelName
		END
	END
END


GO


