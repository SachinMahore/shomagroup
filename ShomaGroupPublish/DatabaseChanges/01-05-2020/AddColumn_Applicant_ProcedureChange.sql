ALTER TABLE tbl_Applicant ADD AddedBy BIGINT

GO

ALTER TABLE tbl_vehicle ADD AddedBy BIGINT

GO

ALTER TABLE tbl_TenantPet ADD AddedBy BIGINT

GO

ALTER TABLE tbl_Parking ADD AddedBy BIGINT

GO

/****** Object:  StoredProcedure [dbo].[usp_GetParkingPaginationAndSearchData]    Script Date: 05/04/2020 11:09:39 AM ******/
DROP PROCEDURE [dbo].[usp_GetParkingPaginationAndSearchData]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetParkingPaginationAndSearchData]    Script Date: 05/04/2020 11:09:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--EXEC usp_GetParkingPaginationAndSearchData 1,'1',1,100
CREATE PROCEDURE [dbo].[usp_GetParkingPaginationAndSearchData]
	@Criteria			INT,
	@CriteriaByText		VARCHAR(100)=NULL,
	@PageNumber			INT,
	@NumberOfRows		INT,
	@SortBy				Varchar(20),
	@OrderBy			Varchar(20)
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
	Type VARCHAR(500), Status VARCHAR(500), UnitID NVARCHAR(50), TenantName NVARCHAR(50), 
	VehicleTag NVARCHAR(50),VehicleMake NVARCHAR(50), VehicleModel NVARCHAR(50),NOP INT
	)

	DECLARE @RowNumLower BIGINT
	DECLARE @RowNumUpper BIGINT
	IF(@Criteria = 0)
	BEGIN
		INSERT INTO @ParkingData (ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, NOP)

		SELECT P.ParkingID,P.PropertyID, P.ParkingName,P.Charges,P.Description,P.Type,ISNULL(P.Status,''), PU.UnitNo AS UnitID, ISNULL(V.OwnerName,'') AS TenantName, ISNULL(V.Tag,'') AS VehicleTag, ISNULL(V.Make,'') AS VehicleMake,ISNULL(V.Model,'') AS VehicleModel, 0 AS NOP
		FROM tbl_Parking P
		LEFT OUTER JOIN tbl_Vehicle V ON P.ParkingID=V.ParkingID
		LEFT OUTER JOIN tbl_PropertyUnits PU ON P.PropertyID=PU.[UID]
		LEFT OUTER JOIN tbl_Tenant T ON V.TenantID=T.ID

		ORDER BY P.ParkingID DESC
	 END
	ELSE IF(@Criteria = 1)
		BEGIN
		IF(@CriteriaByText = '0')
			BEGIN
		INSERT INTO @ParkingData (ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, NOP)

				SELECT P.ParkingID,P.PropertyID, P.ParkingName,P.Charges,P.Description,P.Type,ISNULL(P.Status,''), PU.UnitNo AS UnitID, ISNULL(V.OwnerName,'') AS TenantName, ISNULL(V.Tag,'') AS VehicleTag, ISNULL(V.Make,'') AS VehicleMake,ISNULL(V.Model,'') AS VehicleModel, 0 AS NOP
				FROM tbl_Parking P
				LEFT OUTER JOIN tbl_Vehicle V ON P.ParkingID=V.ParkingID
				LEFT OUTER JOIN tbl_PropertyUnits PU ON P.PropertyID=PU.[UID]
				LEFT OUTER JOIN tbl_Tenant T ON V.TenantID=T.ID

				WHERE P.Type = 2
				ORDER BY P.Type DESC
		END
		ELSE IF(@CriteriaByText = '1')
		BEGIN
		INSERT INTO @ParkingData (ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, NOP)

			SELECT P.ParkingID,P.PropertyID, P.ParkingName,P.Charges,P.Description,P.Type,ISNULL(P.Status,''), PU.UnitNo AS UnitID, ISNULL(V.OwnerName,'') AS TenantName, ISNULL(V.Tag,'') AS VehicleTag, ISNULL(V.Make,'') AS VehicleMake,ISNULL(V.Model,'') AS VehicleModel, 0 AS NOP
			FROM tbl_Parking P
			LEFT OUTER JOIN tbl_Vehicle V ON P.ParkingID=V.ParkingID
			LEFT OUTER JOIN tbl_PropertyUnits PU ON P.PropertyID=PU.[UID]
			LEFT OUTER JOIN tbl_Tenant T ON V.TenantID=T.ID

			WHERE P.Type = 1
			ORDER BY P.Type DESC
		END
	END
	ELSE IF(@Criteria = 2)
	BEGIN
		INSERT INTO @ParkingData (ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, NOP)

		SELECT P.ParkingID,P.PropertyID, P.ParkingName,P.Charges,P.Description,P.Type,ISNULL(P.Status,''), PU.UnitNo AS UnitID, ISNULL(V.OwnerName,'') AS TenantName, ISNULL(V.Tag,'') AS VehicleTag, ISNULL(V.Make,'') AS VehicleMake,ISNULL(V.Model,'') AS VehicleModel, 0 AS NOP
		FROM tbl_Parking P
		LEFT OUTER JOIN tbl_Vehicle V ON P.ParkingID=V.ParkingID
		LEFT OUTER JOIN tbl_PropertyUnits PU ON P.PropertyID=PU.[UID]
		LEFT OUTER JOIN tbl_Tenant T ON V.TenantID=T.ID

		WHERE P.ParkingName LIKE '%'+@CriteriaByText+'%' OR @CriteriaByText IS NULL
		ORDER BY P.ParkingName DESC
	 END
	ELSE IF(@Criteria = 3)
	BEGIN
		INSERT INTO @ParkingData (ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, NOP)

		SELECT P.ParkingID,P.PropertyID, P.ParkingName,P.Charges,P.Description,P.Type,ISNULL(P.Status,''), PU.UnitNo AS UnitID, ISNULL(V.OwnerName,'') AS TenantName, ISNULL(V.Tag,'') AS VehicleTag, ISNULL(V.Make,'') AS VehicleMake,ISNULL(V.Model,'') AS VehicleModel, 0 AS NOP
		FROM tbl_Parking P
		LEFT OUTER JOIN tbl_Vehicle V ON P.ParkingID=V.ParkingID
		LEFT OUTER JOIN tbl_PropertyUnits PU ON P.PropertyID=PU.[UID]
		LEFT OUTER JOIN tbl_Tenant T ON V.TenantID=T.ID

		WHERE PU.UnitNo LIKE '%'+@CriteriaByText+'%' OR @CriteriaByText IS NULL
		ORDER BY PU.UnitNo DESC
	END
	ELSE IF(@Criteria = 4)
	BEGIN
		INSERT INTO @ParkingData (ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, NOP)

		SELECT P.ParkingID,P.PropertyID, P.ParkingName,P.Charges,P.Description,P.Type,ISNULL(P.Status,''), PU.UnitNo AS UnitID, ISNULL(V.OwnerName,'') AS TenantName, ISNULL(V.Tag,'') AS VehicleTag, ISNULL(V.Make,'') AS VehicleMake,ISNULL(V.Model,'') AS VehicleModel, 0 AS NOP
		FROM tbl_Parking P
		LEFT OUTER JOIN tbl_Vehicle V ON P.ParkingID=V.ParkingID
		LEFT OUTER JOIN tbl_PropertyUnits PU ON P.PropertyID=PU.[UID]
		LEFT OUTER JOIN tbl_Tenant T ON V.TenantID=T.ID

		WHERE V.Tag LIKE '%'+@CriteriaByText+'%' OR @CriteriaByText IS NULL
		ORDER BY V.Tag DESC
	END
	ELSE IF(@Criteria = 5)
	BEGIN
		INSERT INTO @ParkingData (ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, NOP)

		SELECT P.ParkingID,P.PropertyID, P.ParkingName,P.Charges,P.Description,P.Type,ISNULL(P.Status,''), PU.UnitNo AS UnitID, ISNULL(V.OwnerName,'') AS TenantName, ISNULL(V.Tag,'') AS VehicleTag, ISNULL(V.Make,'') AS VehicleMake,ISNULL(V.Model,'') AS VehicleModel, 0 AS NOP
		FROM tbl_Parking P
		LEFT OUTER JOIN tbl_Vehicle V ON P.ParkingID=V.ParkingID
		LEFT OUTER JOIN tbl_PropertyUnits PU ON P.PropertyID=PU.[UID]
		LEFT OUTER JOIN tbl_Tenant T ON V.TenantID=T.ID

		WHERE V.OwnerName LIKE '%'+@CriteriaByText+'%' OR @CriteriaByText IS NULL
		ORDER BY V.OwnerName DESC
	END
	ELSE IF(@Criteria = 6)
	BEGIN
		INSERT INTO @ParkingData (ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, NOP)

		SELECT P.ParkingID,P.PropertyID, P.ParkingName,P.Charges,P.Description,P.Type,ISNULL(P.Status,''), PU.UnitNo AS UnitID, ISNULL(V.OwnerName,'') AS TenantName, ISNULL(V.Tag,'') AS VehicleTag, ISNULL(V.Make,'') AS VehicleMake,ISNULL(V.Model,'') AS VehicleModel, 0 AS NOP
		FROM tbl_Parking P
		LEFT OUTER JOIN tbl_Vehicle V ON P.ParkingID=V.ParkingID
		LEFT OUTER JOIN tbl_PropertyUnits PU ON P.PropertyID=PU.[UID]
		LEFT OUTER JOIN tbl_Tenant T ON V.TenantID=T.ID

		WHERE P.Type = 1
		ORDER BY P.Type DESC
	END
	ELSE IF(@Criteria = 7)
	BEGIN
		INSERT INTO @ParkingData (ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, NOP)

		SELECT P.ParkingID,P.PropertyID, P.ParkingName,P.Charges,P.Description,P.Type,ISNULL(P.Status,''), PU.UnitNo AS UnitID, ISNULL(V.OwnerName,'') AS TenantName, ISNULL(V.Tag,'') AS VehicleTag, ISNULL(V.Make,'') AS VehicleMake,ISNULL(V.Model,'') AS VehicleModel, 0 AS NOP
		FROM tbl_Parking P
		LEFT OUTER JOIN tbl_Vehicle V ON P.ParkingID=V.ParkingID
		LEFT OUTER JOIN tbl_PropertyUnits PU ON P.PropertyID=PU.[UID]
		LEFT OUTER JOIN tbl_Tenant T ON V.TenantID=T.ID

		WHERE P.Type = 2
		ORDER BY P.Type DESC
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


IF(@OrderBy='ASC')
	BEGIN
		IF(@SortBy='ParkingID')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.ParkingID ASC)) AS RowNum, ParkingID
			FROM @ParkingData TD)
			SELECT
				FR.ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, ND.NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @ParkingData ND ON FR.ParkingID = ND.ParkingID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @ParkingData ORDER BY ParkingID ASC
		END
		IF(@SortBy='Charges')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.Charges ASC)) AS RowNum, ParkingID
			FROM @ParkingData TD)
			SELECT
				FR.ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, ND.NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @ParkingData ND ON FR.ParkingID = ND.ParkingID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @ParkingData ORDER BY Charges ASC
		END
		IF(@SortBy='Type')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.Type ASC)) AS RowNum, ParkingID
			FROM @ParkingData TD)
			SELECT
				FR.ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, ND.NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @ParkingData ND ON FR.ParkingID = ND.ParkingID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @ParkingData ORDER BY Type ASC
		END
		IF(@SortBy='Description')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.Description ASC)) AS RowNum, ParkingID
			FROM @ParkingData TD)
			SELECT
				FR.ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, ND.NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @ParkingData ND ON FR.ParkingID = ND.ParkingID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @ParkingData ORDER BY Description ASC
		END
		IF(@SortBy='ParkingName')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.ParkingName ASC)) AS RowNum, ParkingID
			FROM @ParkingData TD)
			SELECT
				FR.ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, ND.NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @ParkingData ND ON FR.ParkingID = ND.ParkingID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @ParkingData ORDER BY ParkingName ASC
		END
		IF(@SortBy='PropertyID')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.PropertyID ASC)) AS RowNum, ParkingID
			FROM @ParkingData TD)
			SELECT
				FR.ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, ND.NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @ParkingData ND ON FR.ParkingID = ND.ParkingID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @ParkingData ORDER BY PropertyID ASC
		END
		IF(@SortBy='Tag')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.VehicleTag ASC)) AS RowNum, ParkingID
			FROM @ParkingData TD)
			SELECT
				FR.ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, ND.NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @ParkingData ND ON FR.ParkingID = ND.ParkingID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @ParkingData ORDER BY VehicleTag ASC
		END
		IF(@SortBy='OwnerName')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.TenantName ASC)) AS RowNum, ParkingID
			FROM @ParkingData TD)
			SELECT
				FR.ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, ND.NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @ParkingData ND ON FR.ParkingID = ND.ParkingID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @ParkingData ORDER BY TenantName ASC
		END
	END


IF(@OrderBy='DESC')
	BEGIN
		IF(@SortBy='ParkingID')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.ParkingID DESC)) AS RowNum, ParkingID
			FROM @ParkingData TD)
			SELECT
				FR.ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, ND.NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @ParkingData ND ON FR.ParkingID = ND.ParkingID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @ParkingData ORDER BY ParkingID DESC
		END
		IF(@SortBy='Charges')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.Charges DESC)) AS RowNum, ParkingID
			FROM @ParkingData TD)
			SELECT
				FR.ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, ND.NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @ParkingData ND ON FR.ParkingID = ND.ParkingID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @ParkingData ORDER BY Charges DESC
		END
		IF(@SortBy='Type')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.Type DESC)) AS RowNum, ParkingID
			FROM @ParkingData TD)
			SELECT
				FR.ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, ND.NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @ParkingData ND ON FR.ParkingID = ND.ParkingID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @ParkingData ORDER BY Type DESC
		END
		IF(@SortBy='Description')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.Description DESC)) AS RowNum, ParkingID
			FROM @ParkingData TD)
			SELECT
				FR.ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, ND.NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @ParkingData ND ON FR.ParkingID = ND.ParkingID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @ParkingData ORDER BY Description DESC
		END
		IF(@SortBy='ParkingName')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.ParkingName DESC)) AS RowNum, ParkingID
			FROM @ParkingData TD)
			SELECT
				FR.ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, ND.NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @ParkingData ND ON FR.ParkingID = ND.ParkingID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @ParkingData ORDER BY ParkingName DESC
		END
		IF(@SortBy='PropertyID')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.PropertyID DESC)) AS RowNum, ParkingID
			FROM @ParkingData TD)
			SELECT
				FR.ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, ND.NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @ParkingData ND ON FR.ParkingID = ND.ParkingID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @ParkingData ORDER BY PropertyID DESC
		END
		IF(@SortBy='Tag')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.VehicleTag DESC)) AS RowNum, ParkingID
			FROM @ParkingData TD)
			SELECT
				FR.ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, ND.NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @ParkingData ND ON FR.ParkingID = ND.ParkingID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @ParkingData ORDER BY VehicleTag DESC
		END
		IF(@SortBy='OwnerName')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.TenantName DESC)) AS RowNum, ParkingID
			FROM @ParkingData TD)
			SELECT
				FR.ParkingID, PropertyID, ParkingName, Charges, Description, Type, Status, UnitID, TenantName, VehicleTag, VehicleMake, VehicleModel, ND.NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @ParkingData ND ON FR.ParkingID = ND.ParkingID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @ParkingData ORDER BY TenantName DESC
		END
END

END




GO

