GO

/****** Object:  StoredProcedure [dbo].[usp_Get_Storage]    Script Date: 5/7/2020 2:34:19 PM ******/
DROP PROCEDURE [dbo].[usp_Get_Storage]
GO

/****** Object:  StoredProcedure [dbo].[usp_Get_Storage]    Script Date: 5/7/2020 2:34:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_Get_Storage]
@TenantID BIGINT=0,
@OrderBy varchar(50),
@SortBy varchar(50)
as
if (@OrderBy = 'ASC')
	begin
	if (@SortBy = 'Storage1Storage')
	begin
	SELECT [StorageID],[PropertyID],[StorageName],[Charges],[Description],ISNULL([Type],0) AS [Type],[Status] FROM [tbl_Storage] 
	WHERE StorageID NOT IN (SELECT StorageID FROM tbl_TenantStorage WHERE TenantID!=@TenantID)
	ORDER BY [StorageName] ASC;
	end
	else if (@SortBy = 'Storage1Description')
	begin
	SELECT [StorageID],[PropertyID],[StorageName],[Charges],[Description],ISNULL([Type],0) AS [Type],[Status] FROM [tbl_Storage] 
	WHERE StorageID NOT IN (SELECT StorageID FROM tbl_TenantStorage WHERE TenantID!=@TenantID)
	ORDER BY [Description] ASC;
	end
end
else if (@OrderBy = 'DESC')
	begin
	if (@SortBy = 'Storage1Storage')
	begin
	SELECT [StorageID],[PropertyID],[StorageName],[Charges],[Description],ISNULL([Type],0) AS [Type],[Status] FROM [tbl_Storage] 
	WHERE StorageID NOT IN (SELECT StorageID FROM tbl_TenantStorage WHERE TenantID!=@TenantID)
	ORDER BY [StorageName] DESC;
	end
	else if (@SortBy = 'Storage1Description')
	begin
	SELECT [StorageID],[PropertyID],[StorageName],[Charges],[Description],ISNULL([Type],0) AS [Type],[Status] FROM [tbl_Storage] 
	WHERE StorageID NOT IN (SELECT StorageID FROM tbl_TenantStorage WHERE TenantID!=@TenantID)
	ORDER BY [Description] DESC;
	end
end
GO

