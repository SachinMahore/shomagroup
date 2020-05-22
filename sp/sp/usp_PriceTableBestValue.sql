USE [ShomaRMDev]
GO

/****** Object:  StoredProcedure [dbo].[usp_PriceTableBestValue]    Script Date: 5/21/2020 2:03:18 PM ******/
DROP PROCEDURE [dbo].[usp_PriceTableBestValue]
GO

/****** Object:  StoredProcedure [dbo].[usp_PriceTableBestValue]    Script Date: 5/21/2020 2:03:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_PriceTableBestValue]
@UnitID bigint,
@LeaseTermID bigint
as
begin
SELECT TOP 1 PU.UID, LT.LeaseTerms, UP.Price, LT.LTID
FROM tbl_PropertyUnits PU INNER JOIN tbl_UnitLeasePrice UP ON PU.UID=UP.UnitID INNER JOIN tbl_LeaseTerms LT ON UP.LeaseID=LT.LTID
WHERE PU.UID=@UnitID AND LT.LTID!=@LeaseTermID
ORDER BY UP.Price DESC, LT.LeaseTerms DESC
end;
GO

