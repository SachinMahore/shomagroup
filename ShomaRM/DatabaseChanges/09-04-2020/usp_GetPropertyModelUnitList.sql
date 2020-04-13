GO

/****** Object:  StoredProcedure [dbo].[usp_GetPropertyModelUnitList]    Script Date: 4/13/2020 6:50:23 AM ******/
DROP PROCEDURE [dbo].[usp_GetPropertyModelUnitList]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetPropertyModelUnitList]    Script Date: 4/13/2020 6:50:23 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- exec [usp_GetPropertyModelUnitList] 'B1','04/30/2020',0,0,10000
CREATE PROCEDURE [dbo].[usp_GetPropertyModelUnitList]
    @ModelName nvarchar(50),
	@AvailableDate		datetime,
	@FloorNo int=0,
	@Bedroom int=0,
	@Current_Rent decimal,
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

IF (@Bedroom!=0)
BEGIN
SELECT PU.UID,PU.PID,PU.UnitNo,PU.Wing,PU.Building     
	,PF.FloorNo ,PU.Current_Rent,PU.Deposit,PU.Leased,PU.Rooms,PU.Bedroom,PU.Bathroom,PU.Hall,PU.Area,PU.FloorPlan,PU.AvailableDate,ISNULL(PT.PremiumType,'') AS  Premium
 FROM tbl_PropertyUnits PU LEFT OUTER JOIN tbl_PropertyFloor PF ON PU.FloorNo=PF.FloorID LEFT OUTER JOIN tbl_PremiumType PT ON PU.Premium=PT.PTID
  WHERE PU.Building=@ModelName AND PU.AvailableDate<=@AvailableDate  AND PU.Bedroom=@Bedroom AND PU.Current_Rent<=@Current_Rent AND PU.UID NOT IN (SELECT UnitId FROM @UnitsNotAvailable)
  ORDER BY SUBSTRING(PU.UnitNo, PATINDEX('%[0-9]%', PU.UnitNo), PATINDEX('%[0-9][^0-9]%', PU.UnitNo + 't') - PATINDEX('%[0-9]%', PU.UnitNo) + 1)
END
ELSE
BEGIN
SELECT PU.UID,PU.PID,PU.UnitNo,PU.Wing,PU.Building     
	,PF.FloorNo ,PU.Current_Rent,PU.Deposit,PU.Leased,PU.Rooms,PU.Bedroom,PU.Bathroom,PU.Hall,PU.Area,PU.FloorPlan,PU.AvailableDate,ISNULL(PT.PremiumType,'') AS  Premium
 FROM tbl_PropertyUnits PU LEFT OUTER JOIN tbl_PropertyFloor PF ON PU.FloorNo=PF.FloorID LEFT OUTER JOIN tbl_PremiumType PT ON PU.Premium=PT.PTID
  WHERE PU.Building=@ModelName AND PU.AvailableDate<=@AvailableDate  AND PU.Current_Rent<=@Current_Rent AND PU.UID NOT IN (SELECT UnitId FROM @UnitsNotAvailable)
  ORDER BY SUBSTRING(PU.UnitNo, PATINDEX('%[0-9]%', PU.UnitNo), PATINDEX('%[0-9][^0-9]%', PU.UnitNo + 't') - PATINDEX('%[0-9]%', PU.UnitNo) + 1)
END
END










GO


