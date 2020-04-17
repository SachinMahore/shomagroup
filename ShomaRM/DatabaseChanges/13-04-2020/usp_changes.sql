GO

/****** Object:  StoredProcedure [dbo].[usp_Get_UnitParking]    Script Date: 4/13/2020 8:13:41 PM ******/
DROP PROCEDURE [dbo].[usp_Get_UnitParking]
GO

/****** Object:  StoredProcedure [dbo].[usp_Get_UnitParking]    Script Date: 4/13/2020 8:13:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_Get_UnitParking]
@UID INT
as
begin
select [ParkingID], [PropertyID],[ParkingName],[Charges],[Description],ISNULL([Type],0) AS [Type],ISNULL([Status],0) AS [Status] from [tbl_Parking] where PropertyID=@UID ORDER BY [Type] ASC;
end;

GO

GO

/****** Object:  StoredProcedure [dbo].[usp_Get_Parking]    Script Date: 4/13/2020 8:18:09 PM ******/
DROP PROCEDURE [dbo].[usp_Get_Parking]
GO

/****** Object:  StoredProcedure [dbo].[usp_Get_Parking]    Script Date: 4/13/2020 8:18:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_Get_Parking]
as
begin
select [ParkingID], [PropertyID],[ParkingName],[Charges],[Description],ISNULL([Type],0) AS [Type],[Status] from [tbl_Parking] where Type=2 AND PropertyID=0 ORDER BY [Type] ASC;
end;
GO



