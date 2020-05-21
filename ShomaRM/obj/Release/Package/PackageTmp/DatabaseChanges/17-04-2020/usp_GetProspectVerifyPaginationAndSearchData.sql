GO
/****** Object:  StoredProcedure [dbo].[usp_GetProspectVerifyPaginationAndSearchData]    Script Date: 04/16/2020 6:34:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--EXEC [usp_GetProspectVerifyPaginationAndSearchData] '03/01/2020','04/23/2020',1,25,'FirstName','ASC'
ALTER PROCEDURE [dbo].[usp_GetProspectVerifyPaginationAndSearchData]
	@FromDate		DATETIME,
	@ToDate			DATETIME,
	@PageNumber		INT,
	@NumberOfRows	INT,	
	@UnitNo      	Varchar(50)=null,
	@Astatus		Varchar(50)=null,
	@SortBy			Varchar(20),
	@OrderBy		Varchar(20)

AS
BEGIN
	DECLARE @RowCount			INT
	DECLARE	@TotalRows			BIGINT
	DECLARE @LowerLimit			BIGINT
	DECLARE	@UpperLimit			BIGINT
	DECLARE @TotalNumberOfPages	BIGINT
	DECLARE @ModValue			BIGINT
	DECLARE @PropsectVerificationData		TABLE 
	(
		ID BIGINT,UserID BIGINT, DocID BIGINT, FirstName VARCHAR(100), LastName VARCHAR(100), Phone VARCHAR(100), Email  VARCHAR(100), DocumentType VARCHAR(100), 
		DocumentName VARCHAR(100), VerificationStatus VARCHAR(100), NOP INT, CreatedDate varchar(100), PropertyId BIGINT, UnitNo varchar(100),ApplicationStatus varchar(100)
		
	)

	DECLARE @FinalData		TABLE 
	(
		ID BIGINT,UserID BIGINT, DocID BIGINT, FirstName VARCHAR(100), LastName VARCHAR(100), Phone VARCHAR(100), Email  VARCHAR(100), DocumentType VARCHAR(100), 
		DocumentName VARCHAR(100), VerificationStatus VARCHAR(100), NOP INT,CreatedDate varchar(100), PropertyId BIGINT, UnitNo varchar(100),ApplicationStatus varchar(100)
		
	)

	DECLARE @RowNumLower BIGINT
	DECLARE @RowNumUpper BIGINT


	INSERT INTO @PropsectVerificationData (ID,UserID, DocID, FirstName, LastName, Phone, Email, DocumentType, DocumentName, VerificationStatus, NOP, CreatedDate, PropertyId, UnitNo ,ApplicationStatus) 
	SELECT AN.ID,AN.UserID, ISNULL(DV.DocID,0) AS DocID, AN.FirstName, AN.LastName, AN.Phone, AN.Email, 
	CASE ISNULL(DV.DocumentType,0) WHEN 1 THEN 'Driving Licence' WHEN 2 THEN 'SSN' WHEN 3 THEN 'PAN' WHEN 4 THEN 'Passport' ELSE '' END AS DocumentType,
	ISNULL(DV.DocumentName,'') AS DocumentName, ISNULL(DV.VerificationStatus,0) AS VerificationStatus, 0 AS NOP , Convert(varchar, AN.CreatedDate,101) as CreatedDate, ISNULL(AN.PropertyId,0),PU.UnitNo,ISNULL(AN.Status,0) as status
	FROM tbl_ApplyNow AN 
	LEFT OUTER JOIN tbl_DocumentVerification DV ON AN.ID=DV.ProspectusID
	left outer join [tbl_PropertyUnits] PU on AN.PropertyId = PU.UID
	WHERE AN.CreatedDate BETWEEN @FromDate AND @ToDate+' 23:59:59'
	AND AN.IsApplyNow!=3 AND ISNULL(AN.PropertyId,0)>0	
	and (( pu.UnitNo LIKE ISNULL(@UnitNo,'')+'%' OR @UnitNo IS NULL)
	and (an.Status LIKE ISNULL(@Astatus,'')+'%' OR @Astatus IS NULL))
	ORDER BY AN.CreatedDate DESC
	 
	SET @RowNumLower=(@PageNumber*@NumberOfRows-@NumberOfRows)+1
	SET @RowNumUpper=@PageNumber*@NumberOfRows
	SET @RowCount=(SELECT COUNT(*) FROM @PropsectVerificationData)

	IF @RowCount<@RowNumLower
	BEGIN
	SET @RowNumLower=0
	END
	--------------------------------------------------------------------------------------------------------------------------------------
	;WITH FilterRows AS (
	SELECT (ROW_NUMBER() OVER (ORDER BY PD.ID DESC)) AS RowNum
	FROM @PropsectVerificationData PD )
	SELECT @TotalRows=COUNT(*) FROM FilterRows
	;
	SET @TotalNumberOfPages=(@TotalRows/@NumberOfRows);
	SET @ModValue=(@TotalRows%@NumberOfRows);

	IF(@ModValue>0)
	BEGIN
		SET @TotalNumberOfPages=@TotalNumberOfPages+1;
	END

	UPDATE @PropsectVerificationData SET NOP=@TotalNumberOfPages
	--------------------------------------------------------------------------------------------------------------------------------------
	
	IF(@OrderBy='ASC')
	BEGIN
		IF(@SortBy='FirstName')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.FirstName ASC)) AS RowNum, ID
			FROM @PropsectVerificationData TD)
			INSERT INTO @FinalData (ID,UserID, DocID, FirstName, LastName, Phone, Email, DocumentType, DocumentName, VerificationStatus, NOP, CreatedDate, PropertyId, UnitNo,ApplicationStatus )
			SELECT
				FR.ID,PVD.UserID, PVD.DocID, PVD.FirstName, PVD.LastName, PVD.Phone, PVD.Email, PVD.DocumentType, PVD.DocumentName, PVD.VerificationStatus, PVD.NOP AS NumberOfPages, Convert(varchar, PVD.CreatedDate, 101) as CreatedDate, PVD.PropertyId, PVD.UnitNo,PVD.ApplicationStatus
			FROM 
				FilterRows FR INNER JOIN @PropsectVerificationData PVD ON FR.ID=PVD.ID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @FinalData ORDER BY FirstName ASC
		END
		IF(@SortBy='LastName')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.LastName ASC)) AS RowNum, ID
			FROM @PropsectVerificationData TD)
			INSERT INTO @FinalData (ID,UserID, DocID, FirstName, LastName, Phone, Email, DocumentType, DocumentName, VerificationStatus, NOP, CreatedDate, PropertyId, UnitNo ,ApplicationStatus)
			SELECT
				FR.ID,PVD.UserID, PVD.DocID, PVD.FirstName, PVD.LastName, PVD.Phone, PVD.Email, PVD.DocumentType, PVD.DocumentName, PVD.VerificationStatus, PVD.NOP AS NumberOfPages, Convert(varchar, PVD.CreatedDate, 101) as CreatedDate, PVD.PropertyId, PVD.UnitNo,PVD.ApplicationStatus
			FROM 
				FilterRows FR INNER JOIN @PropsectVerificationData PVD ON FR.ID=PVD.ID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @FinalData ORDER BY LastName ASC
		END
		IF(@SortBy='PhoneNo')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.Phone ASC)) AS RowNum, ID
			FROM @PropsectVerificationData TD)
			INSERT INTO @FinalData (ID,UserID, DocID, FirstName, LastName, Phone, Email, DocumentType, DocumentName, VerificationStatus, NOP, CreatedDate, PropertyId, UnitNo ,ApplicationStatus)
			SELECT
				FR.ID,PVD.UserID, PVD.DocID, PVD.FirstName, PVD.LastName, PVD.Phone, PVD.Email, PVD.DocumentType, PVD.DocumentName, PVD.VerificationStatus, PVD.NOP AS NumberOfPages, Convert(varchar, PVD.CreatedDate, 101) as CreatedDate, PVD.PropertyId, PVD.UnitNo,PVD.ApplicationStatus
			FROM 
				FilterRows FR INNER JOIN @PropsectVerificationData PVD ON FR.ID=PVD.ID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @FinalData ORDER BY Phone ASC
		END
		IF(@SortBy='EmailAddress')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.Email ASC)) AS RowNum, ID
			FROM @PropsectVerificationData TD)
			INSERT INTO @FinalData (ID,UserID, DocID, FirstName, LastName, Phone, Email, DocumentType, DocumentName, VerificationStatus, NOP, CreatedDate, PropertyId, UnitNo,ApplicationStatus )
			SELECT
				FR.ID,PVD.UserID, PVD.DocID, PVD.FirstName, PVD.LastName, PVD.Phone, PVD.Email, PVD.DocumentType, PVD.DocumentName, PVD.VerificationStatus, PVD.NOP AS NumberOfPages, Convert(varchar, PVD.CreatedDate, 101) as CreatedDate, PVD.PropertyId, PVD.UnitNo,PVD.ApplicationStatus
			FROM 
				FilterRows FR INNER JOIN @PropsectVerificationData PVD ON FR.ID=PVD.ID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @FinalData ORDER BY Email ASC
		END
		IF(@SortBy='ApplyDate')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.CreatedDate ASC)) AS RowNum, ID
			FROM @PropsectVerificationData TD)
			INSERT INTO @FinalData (ID,UserID, DocID, FirstName, LastName, Phone, Email, DocumentType, DocumentName, VerificationStatus, NOP, CreatedDate, PropertyId, UnitNo ,ApplicationStatus)
			SELECT
				FR.ID,PVD.UserID, PVD.DocID, PVD.FirstName, PVD.LastName, PVD.Phone, PVD.Email, PVD.DocumentType, PVD.DocumentName, PVD.VerificationStatus, PVD.NOP AS NumberOfPages, Convert(varchar, PVD.CreatedDate, 101) as CreatedDate, PVD.PropertyId, PVD.UnitNo,PVD.ApplicationStatus
			FROM 
				FilterRows FR INNER JOIN @PropsectVerificationData PVD ON FR.ID=PVD.ID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @FinalData ORDER BY CreatedDate ASC
		END
		IF(@SortBy='UnitNo')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.UnitNo ASC)) AS RowNum, ID
			FROM @PropsectVerificationData TD)
			INSERT INTO @FinalData (ID,UserID, DocID, FirstName, LastName, Phone, Email, DocumentType, DocumentName, VerificationStatus, NOP, CreatedDate, PropertyId, UnitNo ,ApplicationStatus)
			SELECT
				FR.ID,PVD.UserID, PVD.DocID, PVD.FirstName, PVD.LastName, PVD.Phone, PVD.Email, PVD.DocumentType, PVD.DocumentName, PVD.VerificationStatus, PVD.NOP AS NumberOfPages, Convert(varchar, PVD.CreatedDate, 101) as CreatedDate, PVD.PropertyId, PVD.UnitNo,PVD.ApplicationStatus
			FROM 
				FilterRows FR INNER JOIN @PropsectVerificationData PVD ON FR.ID=PVD.ID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @FinalData ORDER BY UnitNo ASC
		END
	END
	IF(@OrderBy='DESC')
	BEGIN
		IF(@SortBy='FirstName')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.FirstName DESC)) AS RowNum, ID
			FROM @PropsectVerificationData TD)
			INSERT INTO @FinalData (ID,UserID, DocID, FirstName, LastName, Phone, Email, DocumentType, DocumentName, VerificationStatus, NOP, CreatedDate, PropertyId, UnitNo,ApplicationStatus )
			SELECT
				FR.ID,PVD.UserID, PVD.DocID, PVD.FirstName, PVD.LastName, PVD.Phone, PVD.Email, PVD.DocumentType, PVD.DocumentName, PVD.VerificationStatus, PVD.NOP AS NumberOfPages, Convert(varchar, PVD.CreatedDate, 101) as CreatedDate, PVD.PropertyId, PVD.UnitNo,PVD.ApplicationStatus
			FROM 
				FilterRows FR INNER JOIN @PropsectVerificationData PVD ON FR.ID=PVD.ID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @FinalData ORDER BY FirstName DESC
		END
		IF(@SortBy='LastName')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.LastName DESC)) AS RowNum, ID
			FROM @PropsectVerificationData TD)
			INSERT INTO @FinalData (ID,UserID, DocID, FirstName, LastName, Phone, Email, DocumentType, DocumentName, VerificationStatus, NOP, CreatedDate, PropertyId, UnitNo ,ApplicationStatus)
			SELECT
				FR.ID,PVD.UserID, PVD.DocID, PVD.FirstName, PVD.LastName, PVD.Phone, PVD.Email, PVD.DocumentType, PVD.DocumentName, PVD.VerificationStatus, PVD.NOP AS NumberOfPages, Convert(varchar, PVD.CreatedDate, 101) as CreatedDate, PVD.PropertyId, PVD.UnitNo,PVD.ApplicationStatus
			FROM 
				FilterRows FR INNER JOIN @PropsectVerificationData PVD ON FR.ID=PVD.ID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @FinalData ORDER BY LastName DESC
		END
		IF(@SortBy='PhoneNo')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.Phone DESC)) AS RowNum, ID
			FROM @PropsectVerificationData TD)
			INSERT INTO @FinalData (ID,UserID, DocID, FirstName, LastName, Phone, Email, DocumentType, DocumentName, VerificationStatus, NOP, CreatedDate, PropertyId, UnitNo ,ApplicationStatus)
			SELECT
				FR.ID,PVD.UserID, PVD.DocID, PVD.FirstName, PVD.LastName, PVD.Phone, PVD.Email, PVD.DocumentType, PVD.DocumentName, PVD.VerificationStatus, PVD.NOP AS NumberOfPages, Convert(varchar, PVD.CreatedDate, 101) as CreatedDate, PVD.PropertyId, PVD.UnitNo,PVD.ApplicationStatus
			FROM 
				FilterRows FR INNER JOIN @PropsectVerificationData PVD ON FR.ID=PVD.ID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @FinalData ORDER BY Phone DESC
		END
		IF(@SortBy='EmailAddress')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.Email DESC)) AS RowNum, ID
			FROM @PropsectVerificationData TD)
			INSERT INTO @FinalData (ID,UserID, DocID, FirstName, LastName, Phone, Email, DocumentType, DocumentName, VerificationStatus, NOP, CreatedDate, PropertyId, UnitNo,ApplicationStatus )
			SELECT
				FR.ID,PVD.UserID, PVD.DocID, PVD.FirstName, PVD.LastName, PVD.Phone, PVD.Email, PVD.DocumentType, PVD.DocumentName, PVD.VerificationStatus, PVD.NOP AS NumberOfPages, Convert(varchar, PVD.CreatedDate, 101) as CreatedDate, PVD.PropertyId, PVD.UnitNo,PVD.ApplicationStatus
			FROM 
				FilterRows FR INNER JOIN @PropsectVerificationData PVD ON FR.ID=PVD.ID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @FinalData ORDER BY Email DESC
		END
		IF(@SortBy='ApplyDate')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.CreatedDate DESC)) AS RowNum, ID
			FROM @PropsectVerificationData TD)
			INSERT INTO @FinalData (ID,UserID, DocID, FirstName, LastName, Phone, Email, DocumentType, DocumentName, VerificationStatus, NOP, CreatedDate, PropertyId, UnitNo ,ApplicationStatus)
			SELECT
				FR.ID,PVD.UserID, PVD.DocID, PVD.FirstName, PVD.LastName, PVD.Phone, PVD.Email, PVD.DocumentType, PVD.DocumentName, PVD.VerificationStatus, PVD.NOP AS NumberOfPages, Convert(varchar, PVD.CreatedDate, 101) as CreatedDate, PVD.PropertyId, PVD.UnitNo,PVD.ApplicationStatus
			FROM 
				FilterRows FR INNER JOIN @PropsectVerificationData PVD ON FR.ID=PVD.ID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @FinalData ORDER BY CreatedDate DESC
		END
		IF(@SortBy='UnitNo')
		BEGIN
			;WITH FilterRows AS (
			SELECT (ROW_NUMBER() OVER (ORDER BY TD.UnitNo DESC)) AS RowNum, ID
			FROM @PropsectVerificationData TD)
			INSERT INTO @FinalData (ID,UserID, DocID, FirstName, LastName, Phone, Email, DocumentType, DocumentName, VerificationStatus, NOP, CreatedDate, PropertyId, UnitNo,ApplicationStatus )
			SELECT
				FR.ID,PVD.UserID, PVD.DocID, PVD.FirstName, PVD.LastName, PVD.Phone, PVD.Email, PVD.DocumentType, PVD.DocumentName, PVD.VerificationStatus, PVD.NOP AS NumberOfPages, Convert(varchar, PVD.CreatedDate, 101) as CreatedDate, PVD.PropertyId, PVD.UnitNo,PVD.ApplicationStatus
			FROM 
				FilterRows FR INNER JOIN @PropsectVerificationData PVD ON FR.ID=PVD.ID
			WHERE 
				RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
			ORDER BY 
				RowNum
			;
			select * from @FinalData ORDER BY UnitNo DESC
		END
	END
END