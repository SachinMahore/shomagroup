
GO

/****** Object:  StoredProcedure [dbo].[usp_GetPreMovingPaginationAndSearchData]    Script Date: 5/12/2020 5:02:19 PM ******/
DROP PROCEDURE [dbo].[usp_GetPreMovingPaginationAndSearchData]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetPreMovingPaginationAndSearchData]    Script Date: 5/12/2020 5:02:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--EXEC usp_GetPreMovingPaginationAndSearchData '2019-04-01','2020-04-30',1,10,'FirstName','ASC'
CREATE PROCEDURE [dbo].[usp_GetPreMovingPaginationAndSearchData]
	@PageNumber		INT,
	@NumberOfRows	INT,
	@SortBy         varchar(20),
	@OrderBy        varchar(20)
AS
BEGIN
	DECLARE @RowCount			INT
	DECLARE	@TotalRows			BIGINT
	DECLARE @LowerLimit			BIGINT
	DECLARE	@UpperLimit			BIGINT
	DECLARE @TotalNumberOfPages	BIGINT
	DECLARE @ModValue			BIGINT
	DECLARE @PremovingData		TABLE 
	(
		ApplyNowID BIGINT, PropertyID BIGINT, FirstName VARCHAR(100), LastName VARCHAR(100), FullName VARCHAR(100), 
		Email NVARCHAR(100), PhoneNo VARCHAR(20), UnitNo VARCHAR(100), Building VARCHAR(100), Bedroom int,
		CreatedByDate VARCHAR(10), CreatedBy VARCHAR(100), NOP INT
	)
	
	DECLARE @RowNumLower BIGINT
	DECLARE @RowNumUpper BIGINT

	INSERT INTO @PremovingData (ApplyNowID, PropertyID, FirstName, LastName, FullName, Email, PhoneNo, UnitNo, Building, Bedroom, CreatedByDate, CreatedBy, NOP) 

	select AN.ID as ApplyNowID, AN.PropertyId, AN.FirstName, AN.LastName, AN.FirstName + ' ' + AN.LastName as FullName,
           AN.Email, AN.Phone, PU.UnitNo, PU.Building, PU.Bedroom, CONVERT(varchar, AN.CreatedDate,101) as CreatedDate, AN.CreatedBy,0 AS NOP
    from tbl_ApplyNow AN left outer join tbl_PropertyUnits PU on AN.PropertyId = PU.UID
    where AN.Status = 'Approved' and AN.MoveInDate >= GETDATE()
    order by CreatedDate desc
	 
	SET @RowNumLower=(@PageNumber*@NumberOfRows-@NumberOfRows)+1
	SET @RowNumUpper=@PageNumber*@NumberOfRows
	SET @RowCount=(SELECT COUNT(*) FROM @PremovingData)

	 IF @RowCount<@RowNumLower
	 BEGIN
		SET @RowNumLower=0
	 END
	--------------------------------------------------------------------------------------------------------------------------------------
	;WITH FilterRows AS (
	SELECT (ROW_NUMBER() OVER (ORDER BY PD.CreatedByDate DESC)) AS RowNum
	FROM @PremovingData PD )
	SELECT @TotalRows=COUNT(*) FROM FilterRows
	;
	SET @TotalNumberOfPages=(@TotalRows/@NumberOfRows);
	SET @ModValue=(@TotalRows%@NumberOfRows);

	IF(@ModValue>0)
	BEGIN
		SET @TotalNumberOfPages=@TotalNumberOfPages+1;
	END

	UPDATE @PremovingData SET NOP=@TotalNumberOfPages
	--------------------------------------------------------------------------------------------------------------------------------------
	
	--;WITH FilterRows AS (
	--SELECT (ROW_NUMBER() OVER (ORDER BY TD.EventName ASC)) AS RowNum, EventID
	--FROM @EventData TD)
	--SELECT
	--	FR.EventID, ED.EventName, ED.EventDate, ED.PropertyName, ED.Description, ED.CreatedByDate, ED.CreatedBy, ED.Photo, ED.NOP AS NumberOfPages, ED.Type
	--FROM 
	--	FilterRows FR INNER JOIN @EventData ED ON FR.EventID=ED.EventID
	--WHERE 
	--	RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
	--ORDER BY 
	--	RowNum
	--;

	----------------------------------------------------------------------------------------------------------------------------------------

	if(@OrderBy = 'ASC')
	begin
	if(@SortBy = 'FirstName')
	begin
	;WITH FilterRows AS (
	SELECT (ROW_NUMBER() OVER (ORDER BY TD.FirstName ASC)) AS RowNum, ApplyNowID
	FROM @PremovingData TD)
	SELECT
		FR.ApplyNowID, ED.PropertyID, ED.FirstName, ED.LastName, ED.FullName, ED.Email, ED.PhoneNo, ED.UnitNo, ED.Building,ED.Bedroom,
		ED.CreatedByDate,ED.CreatedBy, ED.NOP AS NumberOfPages
	FROM 
		FilterRows FR INNER JOIN @PremovingData ED ON FR.ApplyNowID=ED.ApplyNowID
	WHERE 
		RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
	ORDER BY 
		RowNum
	;
	end
	else if(@SortBy = 'LastName')
	begin
	;WITH FilterRows AS (
	SELECT (ROW_NUMBER() OVER (ORDER BY TD.LastName ASC)) AS RowNum, ApplyNowID
	FROM @PremovingData TD)
	SELECT
		FR.ApplyNowID, ED.PropertyID, ED.FirstName, ED.LastName, ED.FullName, ED.Email, ED.PhoneNo, ED.UnitNo, ED.Building,ED.Bedroom,
		ED.CreatedByDate,ED.CreatedBy, ED.NOP AS NumberOfPages
	FROM 
		FilterRows FR INNER JOIN @PremovingData ED ON FR.ApplyNowID=ED.ApplyNowID
	WHERE 
		RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
	ORDER BY 
		RowNum
	;
	end
	else if(@SortBy = 'UnitNo')
	begin
	;WITH FilterRows AS (
	SELECT (ROW_NUMBER() OVER (ORDER BY TD.UnitNo ASC)) AS RowNum, ApplyNowID
	FROM @PremovingData TD)
	SELECT
		FR.ApplyNowID, ED.PropertyID, ED.FirstName, ED.LastName, ED.FullName, ED.Email, ED.PhoneNo, ED.UnitNo, ED.Building,ED.Bedroom,
		ED.CreatedByDate,ED.CreatedBy, ED.NOP AS NumberOfPages
	FROM 
		FilterRows FR INNER JOIN @PremovingData ED ON FR.ApplyNowID=ED.ApplyNowID
	WHERE 
		RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
	ORDER BY 
		RowNum
	;
	end
	else if(@SortBy = 'Model')
	begin
	;WITH FilterRows AS (
	SELECT (ROW_NUMBER() OVER (ORDER BY TD.Building ASC)) AS RowNum, ApplyNowID
	FROM @PremovingData TD)
	SELECT
		FR.ApplyNowID, ED.PropertyID, ED.FirstName, ED.LastName, ED.FullName, ED.Email, ED.PhoneNo, ED.UnitNo, ED.Building,ED.Bedroom,
		ED.CreatedByDate,ED.CreatedBy, ED.NOP AS NumberOfPages
	FROM 
		FilterRows FR INNER JOIN @PremovingData ED ON FR.ApplyNowID=ED.ApplyNowID
	WHERE 
		RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
	ORDER BY 
		RowNum
	;
	end
	else if(@SortBy = 'CreatedDate')
	begin
	;WITH FilterRows AS (
	SELECT (ROW_NUMBER() OVER (ORDER BY TD.CreatedByDate ASC)) AS RowNum, ApplyNowID
	FROM @PremovingData TD)
	SELECT
		FR.ApplyNowID, ED.PropertyID, ED.FirstName, ED.LastName, ED.FullName, ED.Email, ED.PhoneNo, ED.UnitNo, ED.Building,ED.Bedroom,
		ED.CreatedByDate,ED.CreatedBy, ED.NOP AS NumberOfPages
	FROM 
		FilterRows FR INNER JOIN @PremovingData ED ON FR.ApplyNowID=ED.ApplyNowID
	WHERE 
		RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
	ORDER BY 
		RowNum
	;
	end
	end

	if(@OrderBy = 'DESC')
	begin
	if(@SortBy = 'FirstName')
	begin
	;WITH FilterRows AS (
	SELECT (ROW_NUMBER() OVER (ORDER BY TD.FirstName DESC)) AS RowNum, ApplyNowID
	FROM @PremovingData TD)
	SELECT
		FR.ApplyNowID, ED.PropertyID, ED.FirstName, ED.LastName, ED.FullName, ED.Email, ED.PhoneNo, ED.UnitNo, ED.Building,ED.Bedroom,
		ED.CreatedByDate,ED.CreatedBy, ED.NOP AS NumberOfPages
	FROM 
		FilterRows FR INNER JOIN @PremovingData ED ON FR.ApplyNowID=ED.ApplyNowID
	WHERE 
		RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
	ORDER BY 
		RowNum
	;
	end
	else if(@SortBy = 'LastName')
	begin
	;WITH FilterRows AS (
	SELECT (ROW_NUMBER() OVER (ORDER BY TD.LastName DESC)) AS RowNum, ApplyNowID
	FROM @PremovingData TD)
	SELECT
		FR.ApplyNowID, ED.PropertyID, ED.FirstName, ED.LastName, ED.FullName, ED.Email, ED.PhoneNo, ED.UnitNo, ED.Building,ED.Bedroom,
		ED.CreatedByDate,ED.CreatedBy, ED.NOP AS NumberOfPages
	FROM 
		FilterRows FR INNER JOIN @PremovingData ED ON FR.ApplyNowID=ED.ApplyNowID
	WHERE 
		RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
	ORDER BY 
		RowNum
	;
	end
	else if(@SortBy = 'UnitNo')
	begin
	;WITH FilterRows AS (
	SELECT (ROW_NUMBER() OVER (ORDER BY TD.UnitNo DESC)) AS RowNum, ApplyNowID
	FROM @PremovingData TD)
	SELECT
		FR.ApplyNowID, ED.PropertyID, ED.FirstName, ED.LastName, ED.FullName, ED.Email, ED.PhoneNo, ED.UnitNo, ED.Building,ED.Bedroom,
		ED.CreatedByDate,ED.CreatedBy, ED.NOP AS NumberOfPages
	FROM 
		FilterRows FR INNER JOIN @PremovingData ED ON FR.ApplyNowID=ED.ApplyNowID
	WHERE 
		RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
	ORDER BY 
		RowNum
	;
	end
	else if(@SortBy = 'Model')
	begin
	;WITH FilterRows AS (
	SELECT (ROW_NUMBER() OVER (ORDER BY TD.Building DESC)) AS RowNum, ApplyNowID
	FROM @PremovingData TD)
	SELECT
		FR.ApplyNowID, ED.PropertyID, ED.FirstName, ED.LastName, ED.FullName, ED.Email, ED.PhoneNo, ED.UnitNo, ED.Building,ED.Bedroom,
		ED.CreatedByDate,ED.CreatedBy, ED.NOP AS NumberOfPages
	FROM 
		FilterRows FR INNER JOIN @PremovingData ED ON FR.ApplyNowID=ED.ApplyNowID
	WHERE 
		RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
	ORDER BY 
		RowNum
	;
	end
	else if(@SortBy = 'CreatedDate')
	begin
	;WITH FilterRows AS (
	SELECT (ROW_NUMBER() OVER (ORDER BY TD.CreatedByDate DESC)) AS RowNum, ApplyNowID
	FROM @PremovingData TD)
	SELECT
		FR.ApplyNowID, ED.PropertyID, ED.FirstName, ED.LastName, ED.FullName, ED.Email, ED.PhoneNo, ED.UnitNo, ED.Building,ED.Bedroom,
		ED.CreatedByDate,ED.CreatedBy, ED.NOP AS NumberOfPages
	FROM 
		FilterRows FR INNER JOIN @PremovingData ED ON FR.ApplyNowID=ED.ApplyNowID
	WHERE 
		RowNum>=@RowNumLower AND RowNum<=@RowNumUpper 
	ORDER BY 
		RowNum
	;
	end
	end
END


GO