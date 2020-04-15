USE [ShomaRMDev]
GO
/****** Object:  StoredProcedure [dbo].[usp_Get_Parking]    Script Date: 4/14/2020 5:26:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[usp_Get_Parking]
	@TenantID BIGINT=0
AS
BEGIN
	SELECT [ParkingID], [PropertyID],[ParkingName],[Charges],[Description],ISNULL([Type],0) AS [Type],[Status] FROM [tbl_Parking] 
	WHERE Type=2 AND PropertyID=0 AND ParkingID NOT IN (SELECT ParkingID FROM tbl_TenantParking WHERE TenantID!=@TenantID) ORDER BY [ParkingName] ASC;
END


GO
/****** Object:  StoredProcedure [dbo].[usp_Get_Storage]    Script Date: 4/14/2020 5:17:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[usp_Get_Storage]
@TenantID BIGINT=0
AS
BEGIN
	
	SELECT [StorageID],[PropertyID],[StorageName],[Charges],[Description],ISNULL([Type],0) AS [Type],[Status] FROM [tbl_Storage] 
	WHERE StorageID NOT IN (SELECT StorageID FROM tbl_TenantStorage WHERE TenantID!=@TenantID)
	ORDER BY [StorageName] ASC;
END



