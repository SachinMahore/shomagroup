USE [ShomaRMDev]
GO

/****** Object:  StoredProcedure [dbo].[usp_PriceTableCurrentValue]    Script Date: 5/21/2020 2:00:59 PM ******/
DROP PROCEDURE [dbo].[usp_PriceTableCurrentValue]
GO

/****** Object:  StoredProcedure [dbo].[usp_PriceTableCurrentValue]    Script Date: 5/21/2020 2:00:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_PriceTableCurrentValue]
@UnitID bigint,
@LeaseTermID bigint
as
begin
SELECT TOP 1 PU.UID, LT.LeaseTerms, UP.Price, LT.LTID
FROM tbl_PropertyUnits PU INNER JOIN tbl_UnitLeasePrice UP ON PU.UID=UP.UnitID INNER JOIN tbl_LeaseTerms LT ON UP.LeaseID=LT.LTID
WHERE PU.UID=@UnitID AND LT.LTID=@LeaseTermID
end;
GO

