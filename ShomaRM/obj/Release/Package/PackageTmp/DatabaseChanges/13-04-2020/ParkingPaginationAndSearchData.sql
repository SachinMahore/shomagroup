
GO

/****** Object:  StoredProcedure [dbo].[usp_GetParkingPaginationAndSearchData]    Script Date: 04/14/2020 12:55:14 PM ******/
DROP PROCEDURE [dbo].[usp_GetParkingPaginationAndSearchData]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetParkingPaginationAndSearchData]    Script Date: 04/14/2020 12:55:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--EXEC usp_GetParkingPaginationAndSearchData 4,'',1,25
CREATE PROCEDURE [dbo].[usp_GetParkingPaginationAndSearchData]
	@Criteria			INT,
	@CriteriaByText		VARCHAR(100)=NULL,
	@PageNumber			INT,
	@NumberOfRows		INT
AS
BEGIN
	DECLARE @RowCount			INT
	DECLARE	@TotalRows			BIGINT
	DECLARE @LowerLimit			BIGINT
	DECLARE	@UpperLimit			BIGINT
	DECLARE @TotalNumberOfPages	BIGINT
	DECLARE @ModValue			BIGINT
	DECLARE @ParkingData		TABLE 
	(
	ParkingID BIGINT, PropertyID BIGINT, ParkingName VARCHAR(500), Charges VARCHAR(50), Description VARCHAR(500),
	Type VARCHAR(500), Status VARCHAR(500), Available VARCHAR(50), UnitID NVARCHAR(50), TenantName NVARCHAR(50), VehicleTag NVARCHAR(50), NOP INT
	)

	DECLARE @RowNumLower BIGINT
	DECLARE @RowNumUpper BIGINT
	IF(@Criteria = 0)
	BEGIN
		INSERT INTO @ParkingData (ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, NOP)

		SELECT P.ParkingID,P.PropertyID, P.ParkingName,P.Charges,P.Description,P.Type,ISNULL(P.Status,''), PU.UnitNo AS UnitID, ISNULL(V.OwnerName,'') AS TenantName, ISNULL(V.Tag,'') AS VehicleTag, 0 AS NOP
		FROM tbl_Parking P
		LEFT OUTER JOIN tbl_Vehicle V ON P.ParkingID=V.ParkingID
		LEFT OUTER JOIN tbl_PropertyUnits PU ON P.PropertyID=PU.[UID]
		LEFT OUTER JOIN tbl_Tenant T ON V.TenantID=T.ID

		ORDER BY P.ParkingID DESC
	 END
	ELSE IF(@Criteria = 1)
	BEGIN
		INSERT INTO @ParkingData (ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, NOP)

		SELECT P.ParkingID,P.PropertyID, P.ParkingName,P.Charges,P.Description,P.Type,ISNULL(P.Status,''), PU.UnitNo AS UnitID, ISNULL(V.OwnerName,'') AS TenantName, ISNULL(V.Tag,'') AS VehicleTag, 0 AS NOP
		FROM tbl_Parking P
		LEFT OUTER JOIN tbl_Vehicle V ON P.ParkingID=V.ParkingID
		LEFT OUTER JOIN tbl_PropertyUnits PU ON P.PropertyID=PU.[UID]
		LEFT OUTER JOIN tbl_Tenant T ON V.TenantID=T.ID

		WHERE P.ParkingName LIKE '%'+@CriteriaByText+'%' OR @CriteriaByText IS NULL
		ORDER BY P.ParkingName DESC
	 END
	ELSE IF(@Criteria = 2)
	BEGIN
		INSERT INTO @ParkingData (ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, NOP)

		SELECT P.ParkingID,P.PropertyID, P.ParkingName,P.Charges,P.Description,P.Type,ISNULL(P.Status,''), PU.UnitNo AS UnitID, ISNULL(V.OwnerName,'') AS TenantName, ISNULL(V.Tag,'') AS VehicleTag, 0 AS NOP
		FROM tbl_Parking P
		LEFT OUTER JOIN tbl_Vehicle V ON P.ParkingID=V.ParkingID
		LEFT OUTER JOIN tbl_PropertyUnits PU ON P.PropertyID=PU.[UID]
		LEFT OUTER JOIN tbl_Tenant T ON V.TenantID=T.ID

		WHERE PU.UnitNo LIKE '%'+@CriteriaByText+'%' OR @CriteriaByText IS NULL
		ORDER BY PU.UnitNo DESC
	END
	ELSE IF(@Criteria = 3)
	BEGIN
		INSERT INTO @ParkingData (ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, NOP)

		SELECT P.ParkingID,P.PropertyID, P.ParkingName,P.Charges,P.Description,P.Type,ISNULL(P.Status,''), PU.UnitNo AS UnitID, ISNULL(V.OwnerName,'') AS TenantName, ISNULL(V.Tag,'') AS VehicleTag, 0 AS NOP
		FROM tbl_Parking P
		LEFT OUTER JOIN tbl_Vehicle V ON P.ParkingID=V.ParkingID
		LEFT OUTER JOIN tbl_PropertyUnits PU ON P.PropertyID=PU.[UID]
		LEFT OUTER JOIN tbl_Tenant T ON V.TenantID=T.ID

		WHERE V.Tag LIKE '%'+@CriteriaByText+'%' OR @CriteriaByText IS NULL
		ORDER BY V.Tag DESC
	END
	ELSE IF(@Criteria = 4)
	BEGIN
		INSERT INTO @ParkingData (ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, NOP)

		SELECT P.ParkingID,P.PropertyID, P.ParkingName,P.Charges,P.Description,P.Type,ISNULL(P.Status,''), PU.UnitNo AS UnitID, ISNULL(V.OwnerName,'') AS TenantName, ISNULL(V.Tag,'') AS VehicleTag, 0 AS NOP
		FROM tbl_Parking P
		LEFT OUTER JOIN tbl_Vehicle V ON P.ParkingID=V.ParkingID
		LEFT OUTER JOIN tbl_PropertyUnits PU ON P.PropertyID=PU.[UID]
		LEFT OUTER JOIN tbl_Tenant T ON V.TenantID=T.ID

		WHERE V.OwnerName LIKE '%'+@CriteriaByText+'%' OR @CriteriaByText IS NULL
		ORDER BY V.OwnerName DESC
	END
	SET @RowNumLower=(@PageNumber*@NumberOfRows-@NumberOfRows)+1
	SET @RowNumUpper=@PageNumber*@NumberOfRows
	SET @RowCount=(SELECT COUNT(*) FROM @ParkingData)

	 IF @RowCount<@RowNumLower
	 BEGIN
		SET @RowNumLower=0
	 END
	--------------------------------------------------------------------------------------------------------------------------------------
	;WITH FilterRows AS (
	SELECT (ROW_NUMBER() OVER (ORDER BY PD.ParkingID DESC)) AS RowNum
	FROM @ParkingData PD )
	SELECT @TotalRows=COUNT(*) FROM FilterRows
	;
	SET @TotalNumberOfPages=(@TotalRows/@NumberOfRows);
	SET @ModValue=(@TotalRows%@NumberOfRows);

	IF(@ModValue>0)
	BEGIN
		SET @TotalNumberOfPages=@TotalNumberOfPages+1;
	END

	UPDATE @ParkingData SET NOP=@TotalNumberOfPages
	--------------------------------------------------------------------------------------------------------------------------------------
	
	;WITH FilterRows AS (
	SELECT (ROW_NUMBER() OVER (ORDER BY TD.ParkingID ASC)) AS RowNum, ParkingID
	FROM @ParkingData TD)
	SELECT
		FR.ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, ND.NOP AS NumberOfPages
	FROM 
		FilterRows FR INNER JOIN @ParkingData ND ON FR.ParkingID = ND.ParkingID
	WHERE 
		RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
	ORDER BY 
		RowNum
	;
END


GO

