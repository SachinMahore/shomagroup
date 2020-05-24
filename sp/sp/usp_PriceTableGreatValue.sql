USE [ShomaRMDev]
GO

/****** Object:  StoredProcedure [dbo].[usp_PriceTableGreatValue]    Script Date: 5/21/2020 2:04:45 PM ******/
DROP PROCEDURE [dbo].[usp_PriceTableGreatValue]
GO

/****** Object:  StoredProcedure [dbo].[usp_PriceTableGreatValue]    Script Date: 5/21/2020 2:04:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_PriceTableGreatValue]
@UnitID bigint,
@LeaseTermID bigint
as
begin
SELECT TOP 1 PU.UID, LT.LeaseTerms, UP.Price, LT.LTID
FROM tbl_PropertyUnits PU INNER JOIN tbl_UnitLeasePrice UP ON PU.UID=UP.UnitID INNER JOIN tbl_LeaseTerms LT ON UP.LeaseID=LT.LTID
WHERE PU.UID=@UnitID AND LT.LTID>@LeaseTermID
ORDER BY UP.Price ASC, LT.LeaseTerms DESC
end;
GO

