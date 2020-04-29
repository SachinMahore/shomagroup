GO

/****** Object:  StoredProcedure [dbo].[usp_GetTenantPetPaginationAndSearchData]    Script Date: 04/28/2020 3:50:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--EXEC [usp_GetTenantPetPaginationAndSearchData] '01/01/2020','12/30/2020',0,'',1,20,'ASC','TenantName'
CREATE PROCEDURE [dbo].[usp_GetTenantPetPaginationAndSearchData]
	@FromDate			DATETIME,
	@ToDate				DATETIME,
	@Criteria			INT,
	@CriteriaByText		VARCHAR(100)=NULL,
	@PageNumber			INT,
	@NumberOfRows		INT,
	@SortBy				varchar(20),
	@OrderBy			varchar(20)
AS
BEGIN
	DECLARE @RowCount				INT
	DECLARE	@TotalRows				BIGINT
	DECLARE @LowerLimit				BIGINT
	DECLARE	@UpperLimit				BIGINT
	DECLARE @TotalNumberOfPages		BIGINT
	DECLARE @ModValue				BIGINT
	DECLARE @PetPlaceData			TABLE 
	(	PetID BIGINT
      ,TenantID BIGINT
      ,PetType INT
      ,Breed VARCHAR(50)
      ,Weight VARCHAR(50)
      ,Age VARCHAR(50)
      ,Photo NVARCHAR(500)
      ,PetVaccinationCert VARCHAR(500)
      ,PetName VARCHAR(100)
      ,VetsName VARCHAR(100)
      ,OriginalPhoto VARCHAR(500)
      ,OriginalVaccinationCert VARCHAR(500)
      ,UniqID VARCHAR(50)
      ,ExpiryDate DATETIME
	  ,UnitNo NVARCHAR(50)
	  ,TenantName NVARCHAR(50)
	  ,NOP INT 
	)
	DECLARE @RowNumLower BIGINT
	DECLARE @RowNumUpper BIGINT
	IF(@Criteria = 0)
		BEGIN
			INSERT INTO @PetPlaceData (PetID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
										OriginalVaccinationCert,UniqID,ExpiryDate,UnitNo,TenantName,NOP) 
	
			SELECT PetID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
			OriginalVaccinationCert,ISNULL(UniqID,'') AS UniqID,ExpiryDate,PU.UnitNo AS UnitNo,ISNULL(T.FirstName+' '+T.LastName,'') AS TenantName,0
			FROM tbl_TenantPet S
			LEFT OUTER JOIN tbl_TenantInfo T ON T.ProspectID = S.TenantID
			LEFT OUTER JOIN tbl_PropertyUnits PU ON T.UnitID = PU.UID
			where S.ExpiryDate BETWEEN @FromDate AND @ToDate+' 23:59:59'
			ORDER BY TenantName
		END
	ELSE IF(@Criteria = 1)
	BEGIN
		INSERT INTO @PetPlaceData (PetID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
									OriginalVaccinationCert,UniqID,ExpiryDate,UnitNo,TenantName,NOP) 
	
		SELECT PetID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
		OriginalVaccinationCert,ISNULL(UniqID,'') AS UniqID,ExpiryDate,ISNULL(PU.UnitNo,'') AS UnitNo,ISNULL(T.FirstName+' '+T.LastName,'') AS TenantName,0
		FROM tbl_TenantPet S
		LEFT OUTER JOIN tbl_TenantInfo T ON T.ProspectID = S.TenantID
		LEFT OUTER JOIN tbl_PropertyUnits PU ON T.UnitID = PU.UID
		WHERE T.FirstName LIKE '%'+@CriteriaByText+'%' OR @CriteriaByText IS NULL OR
			  T.LastName LIKE '%'+@CriteriaByText+'%' OR @CriteriaByText IS NULL AND
			  S.ExpiryDate BETWEEN @FromDate AND @ToDate+' 23:59:59'
		ORDER BY TenantName
	END
	ELSE IF(@Criteria = 2)
		BEGIN
			INSERT INTO @PetPlaceData (PetID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
											OriginalVaccinationCert,UniqID,ExpiryDate,UnitNo,TenantName,NOP) 
	
				SELECT PetID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
				OriginalVaccinationCert,ISNULL(UniqID,'') AS UniqID,ExpiryDate,ISNULL(PU.UnitNo,'') AS UnitNo,ISNULL(T.FirstName+' '+T.LastName,'') AS TenantName,0
				FROM tbl_TenantPet S
				LEFT OUTER JOIN tbl_TenantInfo T ON T.ProspectID = S.TenantID
				LEFT OUTER JOIN tbl_PropertyUnits PU ON T.UnitID = PU.UID
				WHERE PU.UnitNo LIKE '%'+@CriteriaByText+'%' OR @CriteriaByText IS NULL AND
				S.ExpiryDate BETWEEN @FromDate AND @ToDate+' 23:59:59'
				ORDER BY UnitNo
		END
	ELSE IF(@Criteria = 3)
	BEGIN
		INSERT INTO @PetPlaceData (PetID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
										OriginalVaccinationCert,UniqID,ExpiryDate,UnitNo,TenantName,NOP) 
	
			SELECT PetID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
			OriginalVaccinationCert,ISNULL(UniqID,'') AS UniqID,ExpiryDate,ISNULL(PU.UnitNo,'') AS UnitNo,ISNULL(T.FirstName+' '+T.LastName,'') AS TenantName,0
			FROM tbl_TenantPet S
			LEFT OUTER JOIN tbl_TenantInfo T ON T.ProspectID = S.TenantID
			LEFT OUTER JOIN tbl_PropertyUnits PU ON T.UnitID = PU.UID
			WHERE S.UniqID LIKE '%'+@CriteriaByText+'%' OR @CriteriaByText IS NULL AND
			  S.ExpiryDate BETWEEN @FromDate AND @ToDate+' 23:59:59'
			ORDER BY S.UniqID
	END

	SET @RowNumLower=(@PageNumber*@NumberOfRows-@NumberOfRows)+1
	SET @RowNumUpper=@PageNumber*@NumberOfRows
	SET @RowCount=(SELECT COUNT(*) FROM @PetPlaceData)

	 IF @RowCount<@RowNumLower
	 BEGIN
		SET @RowNumLower=0
	 END
	--------------------------------------------------------------------------------------------------------------------------------------
	;WITH FilterRows AS (
	SELECT (ROW_NUMBER() OVER (ORDER BY PD.PetID DESC)) AS RowNum
	FROM @PetPlaceData PD )
	SELECT @TotalRows=COUNT(*) FROM FilterRows
	;

	SET @TotalNumberOfPages=(@TotalRows/@NumberOfRows);
	SET @ModValue=(@TotalRows%@NumberOfRows);

	IF(@ModValue>0)
	BEGIN
		SET @TotalNumberOfPages=@TotalNumberOfPages+1;
	END

	UPDATE @PetPlaceData SET NOP=@TotalNumberOfPages
	--------------------------------------------------------------------------------------------------------------------------------------
	
--	;WITH FilterRows AS (
--			SELECT (ROW_NUMBER() OVER (ORDER BY TD.PetID ASC)) AS RowNum, PetID
--			FROM @PetPlaceData TD)
--			SELECT
--				FR.PetID,TenantID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
--										OriginalVaccinationCert,UniqID,ExpiryDate,UnitNo,TenantName, NOP AS NumberOfPages
--			FROM 
--				FilterRows FR INNER JOIN @PetPlaceData VD ON FR.PetID=VD.PetID
--			WHERE 
--				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
--			ORDER BY 
--				RowNum
--			;
--END

	--------------------------------------------------------------------------------------------------------------------------------------

IF(@OrderBy='ASC')
	BEGIN
		IF(@SortBy='TenantName')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.TenantName ASC)) AS RowNum, PetID
			FROM @PetPlaceData TD)
			SELECT
				FR.PetID,TenantID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
										OriginalVaccinationCert,UniqID,ExpiryDate,UnitNo,TenantName, NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @PetPlaceData VD ON FR.PetID=VD.PetID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			
			select * from @PetPlaceData ORDER BY TenantName ASC
		END

		IF(@SortBy='UnitNo')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.UnitNo ASC)) AS RowNum, PetID
			FROM @PetPlaceData TD)
			SELECT
				FR.PetID,TenantID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
										OriginalVaccinationCert,UniqID,ExpiryDate,UnitNo,TenantName, NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @PetPlaceData VD ON FR.PetID=VD.PetID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @PetPlaceData ORDER BY UnitNo ASC
		END
		IF(@SortBy='PetName')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.PetName ASC)) AS RowNum, PetID
			FROM @PetPlaceData TD)
			SELECT
				FR.PetID,TenantID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
										OriginalVaccinationCert,UniqID,ExpiryDate,UnitNo,TenantName, NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @PetPlaceData VD ON FR.PetID=VD.PetID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @PetPlaceData ORDER BY PetName ASC
		END
		IF(@SortBy='Weight')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.Weight ASC)) AS RowNum, PetID
			FROM @PetPlaceData TD)
			SELECT
				FR.PetID,TenantID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
										OriginalVaccinationCert,UniqID,ExpiryDate,UnitNo,TenantName, NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @PetPlaceData VD ON FR.PetID=VD.PetID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @PetPlaceData ORDER BY Weight ASC
		END
		IF(@SortBy='UniqID')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.UniqID ASC)) AS RowNum, PetID
			FROM @PetPlaceData TD)
			SELECT
				FR.PetID,TenantID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
										OriginalVaccinationCert,UniqID,ExpiryDate,UnitNo,TenantName, NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @PetPlaceData VD ON FR.PetID=VD.PetID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @PetPlaceData ORDER BY UniqID ASC
		END
		IF(@SortBy='ExpiryDate')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.ExpiryDate ASC)) AS RowNum, PetID
			FROM @PetPlaceData TD)
			SELECT
				FR.PetID,TenantID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
										OriginalVaccinationCert,UniqID,ExpiryDate,UnitNo,TenantName, NOP AS NumberOfPages
			FROM 
				FilterRows FR INNER JOIN @PetPlaceData VD ON FR.PetID=VD.PetID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @PetPlaceData ORDER BY ExpiryDate ASC
		END
	END

IF(@OrderBy='DESC')
BEGIN
	IF(@SortBy='TenantName')
	BEGIN
		;WITH FilterRows AS (
		SELECT (ROW_NUMBER() OVER (ORDER BY TD.TenantName DESC)) AS RowNum, PetID
		FROM @PetPlaceData TD)
		SELECT
			FR.PetID,TenantID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
									OriginalVaccinationCert,UniqID,ExpiryDate,UnitNo,TenantName, NOP AS NumberOfPages
		FROM 
			FilterRows FR INNER JOIN @PetPlaceData VD ON FR.PetID=VD.PetID
		WHERE 
			RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
		ORDER BY 
			RowNum
		;
			
		select * from @PetPlaceData ORDER BY TenantName DESC
	END

	IF(@SortBy='UnitNo')
	BEGIN
		;WITH FilterRows AS (
		SELECT (ROW_NUMBER() OVER (ORDER BY TD.UnitNo DESC)) AS RowNum, PetID
		FROM @PetPlaceData TD)
		SELECT
			FR.PetID,TenantID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
									OriginalVaccinationCert,UniqID,ExpiryDate,UnitNo,TenantName, NOP AS NumberOfPages
		FROM 
			FilterRows FR INNER JOIN @PetPlaceData VD ON FR.PetID=VD.PetID
		WHERE 
			RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
		ORDER BY 
			RowNum
		;
		select * from @PetPlaceData ORDER BY UnitNo DESC
	END
	IF(@SortBy='PetName')
	BEGIN
		;WITH FilterRows AS (
		SELECT (ROW_NUMBER() OVER (ORDER BY TD.PetName DESC)) AS RowNum, PetID
		FROM @PetPlaceData TD)
		SELECT
			FR.PetID,TenantID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
									OriginalVaccinationCert,UniqID,ExpiryDate,UnitNo,TenantName, NOP AS NumberOfPages
		FROM 
			FilterRows FR INNER JOIN @PetPlaceData VD ON FR.PetID=VD.PetID
		WHERE 
			RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
		ORDER BY 
			RowNum
		;
		select * from @PetPlaceData ORDER BY PetName DESC
	END
	IF(@SortBy='Weight')
	BEGIN
		;WITH FilterRows AS (
		SELECT (ROW_NUMBER() OVER (ORDER BY TD.Weight DESC)) AS RowNum, PetID
		FROM @PetPlaceData TD)
		SELECT
			FR.PetID,TenantID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
									OriginalVaccinationCert,UniqID,ExpiryDate,UnitNo,TenantName, NOP AS NumberOfPages
		FROM 
			FilterRows FR INNER JOIN @PetPlaceData VD ON FR.PetID=VD.PetID
		WHERE 
			RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
		ORDER BY 
			RowNum
		;
		select * from @PetPlaceData ORDER BY Weight DESC
	END
	IF(@SortBy='UniqID')
	BEGIN
		;WITH FilterRows AS (
		SELECT (ROW_NUMBER() OVER (ORDER BY TD.UniqID DESC)) AS RowNum, PetID
		FROM @PetPlaceData TD)
		SELECT
			FR.PetID,TenantID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
									OriginalVaccinationCert,UniqID,ExpiryDate,UnitNo,TenantName, NOP AS NumberOfPages
		FROM 
			FilterRows FR INNER JOIN @PetPlaceData VD ON FR.PetID=VD.PetID
		WHERE 
			RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
		ORDER BY 
			RowNum
		;
		select * from @PetPlaceData ORDER BY UniqID DESC
	END
	IF(@SortBy='ExpiryDate')
	BEGIN
		;WITH FilterRows AS (
		SELECT (ROW_NUMBER() OVER (ORDER BY TD.ExpiryDate DESC)) AS RowNum, PetID
		FROM @PetPlaceData TD)
		SELECT
			FR.PetID,TenantID,PetType,Breed,Weight,Age,Photo,PetVaccinationCert,PetName,VetsName,OriginalPhoto,
									OriginalVaccinationCert,UniqID,ExpiryDate,UnitNo,TenantName, NOP AS NumberOfPages
		FROM 
			FilterRows FR INNER JOIN @PetPlaceData VD ON FR.PetID=VD.PetID
		WHERE 
			RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
		ORDER BY 
			RowNum
		;
		select * from @PetPlaceData ORDER BY ExpiryDate DESC
	END
END

END
GO

