
/****** Object:  StoredProcedure [dbo].[usp_GetPropertyUnitListCord]    Script Date: 4/13/2020 6:51:12 AM ******/
DROP PROCEDURE [dbo].[usp_GetPropertyUnitListCord]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetPropertyUnitListCord]    Script Date: 4/13/2020 6:51:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- EXEC [usp_GetPropertyUnitListCord] 8,1,1,'05/31/2020',20000,'A1A'
CREATE PROCEDURE [dbo].[usp_GetPropertyUnitListCord]
    @PID BIGINT,
	@FloorNo int=0,
	@Bedroom int=0,
	@AvailableDate datetime,
	@MaxRent decimal,
	@ModelName VARCHAR(50)='',
	@ProspectId	BIGINT=0
AS
BEGIN
DECLARE @UnitsNotAvailable TABLE (UnitId BIGINT)

INSERT INTO @UnitsNotAvailable (UnitId)
SELECT UID FROM tbl_Lease WHERE STATUS=1

INSERT INTO @UnitsNotAvailable (UnitId)
SELECT PropertyId FROM tbl_ApplyNow WHERE LTRIM(RTRIM((ISNULL(Status,''))))!='Denied'


DECLARE @UnitID BIGINT =0

SET @UnitID=ISNULL((SELECT PropertyId FROM tbl_ApplyNow WHERE ID=@ProspectId),0)

DELETE FROM @UnitsNotAvailable WHERE UnitId=@UnitID

CREATE TABLE #TEMPUNIT (UID BIGINT,UnitNo NVARCHAR(50),FloorNo INT,Coordinates NVARCHAR(MAX),IsAvail INT, IsProcess INT, Bedroom INT, Bathroom INT,Area  NVARCHAR(50),Premium  NVARCHAR(1000),Current_Rent Decimal(18,2),AvailableDate DateTime, ModelName VARCHAR(50), UnitNoInt INT)
INSERT INTO #TEMPUNIT 
SELECT PU.UID,PU.UnitNo,PF.FloorNo,PU.Coordinates,0,0, PU.Bedroom, PU.Bathroom,PU.Area,
(SELECT TOP 1 PremiumType FROM tbl_PremiumType PT WHERE Pt.PTID=ISNULL(PU.Premium,0)) AS  Premium,PU.Current_Rent,PU.AvailableDate, PU.Building, 
SUBSTRING(PU.UnitNo, PATINDEX('%[0-9]%', PU.UnitNo), PATINDEX('%[0-9][^0-9]%', PU.UnitNo + 't') - PATINDEX('%[0-9]%', PU.UnitNo) + 1)
FROM tbl_PropertyUnits PU INNER JOIN tbl_PropertyFloor PF ON PU.FloorNo=PF.FloorID
WHERE PU.PID=@PID AND PU.FloorNo=@FloorNo 
 
 WHILE((SELECT COUNT(*) FROM #TEMPUNIT WHERE IsProcess=0)>0)
	BEGIN
		DECLARE @UID BIGINT=0
		DECLARE @IsAval INT=0
		SET @UID=(SELECT TOP 1 UID FROM #TEMPUNIT WHERE IsProcess=0)

		IF(@Bedroom>0)
		BEGIN
			SET @IsAval=(SELECT CASE WHEN COUNT(*)>0 THEN 1 ELSE 0 END FROM tbl_PropertyUnits PU WHERE PU.UID=@UID AND (PU.Building=@ModelName OR @ModelName='') AND PU.Bedroom=@BedRoom AND PU.AvailableDate<=@AvailableDate AND PU.Current_Rent<=@MaxRent AND PU.UID NOT IN (SELECT UnitId FROM @UnitsNotAvailable))
			IF(@IsAval=0)
			BEGIN
				SET @IsAval=(SELECT CASE WHEN COUNT(*)>0 THEN 2 ELSE 0 END FROM tbl_PropertyUnits PU WHERE PU.UID=@UID AND PU.AvailableDate<=@AvailableDate AND PU.Current_Rent<=@MaxRent AND PU.UID NOT IN (SELECT UnitId FROM @UnitsNotAvailable))
			END
		END
		ELSE
		BEGIN
			SET @IsAval=(SELECT CASE WHEN COUNT(*)>0 THEN 1 ELSE 0 END FROM tbl_PropertyUnits PU WHERE PU.UID=@UID AND PU.AvailableDate<=@AvailableDate AND PU.Current_Rent<=@MaxRent AND PU.UID NOT IN (SELECT UnitId FROM @UnitsNotAvailable))
		END

		UPDATE #TEMPUNIT SET IsAvail=@IsAval, IsProcess=1 WHERE UID=@UID
	END

  SELECT * FROM #TEMPUNIT ORDER BY UnitNoInt

  DROP TABLE #TEMPUNIT
END









GO


