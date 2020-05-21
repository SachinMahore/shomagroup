ALTER TABLE tbl_Vehicle ADD VehicleType INT
GO
ALTER TABLE tbl_BackgroundScreening ADD SSNNumber VARCHAR(500)
GO
ALTER TABLE tbl_PropertyUnits ADD IsFurnished INT
GO
UPDATE tbl_PropertyUnits SET IsFurnished = 1

GO

/****** Object:  StoredProcedure [dbo].[usp_GetApplicationSummaryData]    Script Date: 5/15/2020 6:04:24 PM ******/
DROP PROCEDURE [dbo].[usp_GetApplicationSummaryData]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetApplicationSummaryData]    Script Date: 5/15/2020 6:04:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--EXEC usp_GetApplicationSummaryData 39

CREATE PROCEDURE [dbo].[usp_GetApplicationSummaryData]
	@TenantID BIGINT,
	@UserId bigint
AS
BEGIN
	-- ApartmentInfo --
	SELECT UnitNo, Building, MoveInDate, ISNULL((SELECT TOP 1 CONVERT(VARCHAR(50),LeaseTerms)+' Mo' FROM tbl_LeaseTerms WHERE LTID=AN.LeaseTerm),'') AS LeaseTerm,
	PU.Bedroom, PU.Bathroom, PU.Area, PU.Deposit,AN.MonthlyCharges FROM tbl_ApplyNow AN INNER JOIN tbl_PropertyUnits PU ON AN.PropertyId=PU.UID WHERE ID=@TenantID
	
	-- ApplicantFullInfo --
	SELECT FirstName+' '+LastName AS ApplicantName, DateOfBirth AS ApplicantDOB, ISNULL(Mobile,'') AS ApplicantPhone, Email AS ApplicantEmail, SSN AS ApplicantSSN,
	CASE WHEN IDType=1 THEN 'Driver''s License' WHEN IDType=2 THEN 'Military ID' WHEN IDType=3 THEN 'Passport' WHEN IDType=4 THEN 'State Issued ID' ELSE '' END AS ApplicantIDType, IDNumber AS ApplicantIDNumber,
	(SELECT StateName FROM tbl_State WHERE ID=OT.State) AS ApplicantIDIssuingState,(SELECT CountryName FROM tbl_Country WHERE ID=OT.Country) AS ApplicantIssuingCountry,

	MoveInDateFrom AS CurrentMoveInDate, MonthlyPayment AS CurrentMonthlyPayment,
	(SELECT CountryName FROM tbl_Country WHERE ID=OT.Country) AS CurrentCountry,
	ISNULL(HomeAddress1,'') +CASE WHEN ISNULL(HomeAddress2,'')!='' THEN ', '+HomeAddress2 ELSE ',' END+ ISNULL(CityHome,'')+', '+ISNULL((SELECT StateName FROM tbl_State WHERE ID=OT.StateHome),'')+' '+ ISNULL(ZipHome,'') AS CurrentAddress,
	
	ApartmentCommunity AS CurrentAptCommunity,
	ManagementCompany AS CurrentMgmtCompany, ManagementCompanyPhone AS CurrentMgmtCompanyPhone,CASE WHEN IsProprNoticeLeaseAgreement=1 THEN 'Yes' ELSE 'No' END AS CurrentNoticeGiven,

	CASE WHEN Evicted=1 THEN 'No' ELSE 'Yes' END AS Evicted, ISNULL(EvictedDetails,'') AS EvictedDetails,
	CASE WHEN ConvictedFelony=1 THEN 'No' ELSE 'Yes' END AS ConvictedFelony, ISNULL(ConvictedFelonyDetails,'') AS ConvictedFelonyDetails,
	CASE WHEN CriminalChargPen=1 THEN 'No' ELSE 'Yes' END AS CriminalChargPen, ISNULL(CriminalChargPenDetails,'') AS CriminalChargPenDetails,
	CASE WHEN DoYouSmoke=1 THEN 'No' ELSE 'Yes' END AS DoYouSmoke,CASE WHEN ReferredResident=1 THEN 'No' ELSE 'Yes' END AS ReferredResident, ISNULL(ReferredResidentName,'') AS ReferredResidentName,
	CASE WHEN ReferredBrokerMerchant=1 THEN 'No' ELSE 'Yes' END AS ReferredBrokerMerchant, 
	
	EmployerName AS CurrentEmployerName, ISNULL(JobTitle,'') AS CurrentJobTitle,
	CASE WHEN JobType=1 THEN 'Permanent' WHEN JobType=2 THEN 'Contract Basis' ELSE '' END AS CurrentJobType, StartDate AS CurrentStartDate, Income AS CurrentAnnualIncome, AdditionalIncome AS CurrentAnnualAddIncome,
	ISNULL(SupervisorName,'') AS CurrentSupervisorName, ISNULL(SupervisorPhone,'') AS CurrentSupervisorPhone,
	(SELECT CountryName FROM tbl_Country WHERE ID=OT.OfficeCountry) AS CurrentOfficeCountry,
	ISNULL(OfficeAddress1,'') +CASE WHEN ISNULL(OfficeAddress2,'')!='' THEN ', '+OfficeAddress2 ELSE ',' END+ ISNULL(OfficeCity,'')+', '+ISNULL((SELECT StateName FROM tbl_State WHERE ID=OT.OfficeState),'')+' '+ ISNULL(OfficeZip,'') AS CurrentEmployeerAddress,
	
	Relationship AS EmergencyRelationship, ISNULL(EmergencyFirstName,'')+' '+ ISNULL(EmergencyLastName,'') AS EmergencyName,EmergencyMobile AS EmergencyHomePhone,
	ISNULL(EmergencyAddress1,'') +CASE WHEN ISNULL(EmergencyAddress2,'')!='' THEN ', '+EmergencyAddress2 ELSE ',' END+ ISNULL(EmergencyCityHome,'')+', '+ISNULL((SELECT StateName FROM tbl_State WHERE ID=OT.EmergencyStateHome),'')+' '+ ISNULL(EmergencyZipHome,'') AS EmergencyAddress,
	
	(SELECT CountryName FROM tbl_Country WHERE ID=OT.EmergencyCountry) AS EmergencyCountry, EmergencyEmail
	FROM tbl_TenantOnline OT WHERE ParentTOID=@UserId

	--OtherResInfo--
	SELECT ISNULL(HomeAddress1,'') +CASE WHEN ISNULL(HomeAddress2,'')!='' THEN ', '+HomeAddress2 ELSE ',' END+ ISNULL(CityHome,'')+', '+ISNULL((SELECT StateName FROM tbl_State WHERE ID=ah.StateHome),'')+' '+ ISNULL(ZipHome,'') AS OtherAddress,
	(SELECT CountryName FROM tbl_Country WHERE ID=AH.Country) AS OtherCountry, MonthlyPayment AS OtherMonthlyPayment, ApartmentCommunity AS OtherAptCommunity,
	ManagementCompany AS OtherMgmtCompany,  ManagementCompanyPhone AS OtherMgmtCompanyPhone, MoveInDateFrom AS OtherMoveInDateFrom, MoveInDateTo AS OtherMoveInDateTo, 
	ISNULL(Reason,'') AS OtherResion, CASE WHEN IsProprNoticeLeaseAgreement=1 THEN 'Yes' ELSE 'No' END AS OtherNoticeGiven
	FROM tbl_ApplicantHistory AH WHERE ParentTOID=@UserId

	--OtherEmpInfo--
	SELECT EmployerName AS OtherEmployerName, ISNULL(JobTitle,'') AS OtherJobTitle,
	CASE WHEN JobType=1 THEN 'Permanent' WHEN JobType=2 THEN 'Contract Basis' ELSE '' END AS OtherJobType, StartDate AS OtherStartDate, TerminationDate AS OtherTerminationDate, 
	AnnualIncome AS OtherAnnualIncome, AddAnnualIncome AS OtherAnnualAddIncome, ISNULL(SupervisorName,'') AS OtherSupervisorName, ISNULL(SupervisorPhone,'') AS OtherSupervisorPhone,
	(SELECT CountryName FROM tbl_Country WHERE ID=EH.Country) AS OtherOfficeCountry,
	ISNULL(Address1,'') +CASE WHEN ISNULL(Address2,'')!='' THEN ', '+Address2 ELSE ',' END+ ISNULL(City,'')+', '+ISNULL((SELECT StateName FROM tbl_State WHERE ID=EH.State),'')+' '+ ISNULL(Zip,'') AS OtherEmployeerAddress,
	 ISNULL(TerminationReason,'') AS OtherTerminationReason
	FROM tbl_EmployerHistory EH WHERE ParentTOID=@UserId

	--PetInfo--
	SELECT PetName, Breed, Weight, VetsName FROM tbl_TenantPet WHERE AddedBy=@UserId

	--VehicleInfo--
	SELECT Make, Model, Year, Color, License, (SELECT StateName FROM tbl_State WHERE ID=V.State) AS StateName  FROM tbl_Vehicle V WHERE AddedBy=@UserId

END
GO

/****** Object:  StoredProcedure [dbo].[usp_GetClientModelUnitList]    Script Date: 5/14/2020 2:07:41 PM ******/
DROP PROCEDURE [dbo].[usp_GetClientModelUnitList]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetClientModelUnitList]    Script Date: 5/14/2020 2:07:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- EXEC [usp_GetClientModelUnitList] 8,'3/1/2020',0,0,10000,0
CREATE PROCEDURE [dbo].[usp_GetClientModelUnitList]
    @PID BIGINT,
	@AvailableDate		datetime,
	@FloorNo int=0,
	@Bedroom int=0,
	@Current_Rent decimal,
	@SortOrder int=0,
	@Furnished int=0
AS
BEGIN
DECLARE @RentRange nvarchar(20)

DECLARE @ModelData TABLE (ModelID int, ModelName nvarchar(50),RentRange decimal, Bedroom int, Bathroom int, Area nvarchar(50),FloorPlan nvarchar(50), NoAvailable int)
DECLARE @RentRangeData TABLE (ModelName nvarchar(50),RentMinRange nvarchar(20),RentMaxRange  nvarchar(20), NoAvailable int)
DECLARE @UnitsNotAvailable TABLE (UnitId BIGINT)

INSERT INTO @UnitsNotAvailable (UnitId)
SELECT UID FROM tbl_Lease WHERE STATUS=1

INSERT INTO @UnitsNotAvailable (UnitId)
SELECT PropertyId FROM tbl_ApplyNow WHERE LTRIM(RTRIM((ISNULL(Status,''))))!='Denied'

	IF (@Bedroom!=0)
	BEGIN
		INSERT INTO @ModelData (ModelID , ModelName ,RentRange, Bedroom , Bathroom , Area ,FloorPlan, NoAvailable)
		SELECT M.ModelID,M.ModelName,PU.Current_Rent as RentRange,M.Bedroom,M.Bathroom,M.Area,M.FloorPlan,1
		FROM tbl_PropertyUnits PU INNER JOIN tbl_Models M ON PU.Building=M.ModelName
		WHERE PU.AvailableDate<=@AvailableDate AND M.Bedroom=@Bedroom 
		AND PU.Current_Rent<=@Current_Rent 
		AND PU.UID NOT IN (SELECT UnitId FROM @UnitsNotAvailable) AND (ISNULL(PU.IsFurnished,1)=@Furnished OR @Furnished=0)

		INSERT INTO @RentRangeData (ModelName,RentMinRange,RentMaxRange,NoAvailable)
		SELECT M.ModelName, M.MinRent, M.MaxRent, COUNT(*)  FROM tbl_Models M INNER JOIN @ModelData MD ON M.ModelID=MD.ModelID
		GROUP BY M.ModelName, M.MinRent, M.MaxRent

		IF(@SortOrder=0)
		BEGIN
			SELECT DISTINCT MD.ModelName,'$'+RR.RentMinRange +' - $'+RR.RentMaxRange as RentRange,RR.NoAvailable,MD.Bathroom,MD.Bedroom,MD.Area,MD.FloorPlan, RR.RentMinRange  FROM @ModelData MD INNER JOIN @RentRangeData  RR ON MD.ModelName=RR.ModelName ORDER BY RR.RentMinRange ASC, MD.ModelName
		END
		ELSE
		BEGIN
			SELECT DISTINCT MD.ModelName,'$'+RR.RentMinRange +' - $'+RR.RentMaxRange as RentRange,RR.NoAvailable,MD.Bathroom,MD.Bedroom,MD.Area,MD.FloorPlan, RR.RentMaxRange   FROM @ModelData MD INNER JOIN @RentRangeData  RR ON MD.ModelName=RR.ModelName ORDER BY RR.RentMaxRange DESC, MD.ModelName
		END
	END
	ELSE
	BEGIN
		INSERT INTO @ModelData (ModelID , ModelName ,RentRange, Bedroom , Bathroom , Area ,FloorPlan, NoAvailable)
		SELECT M.ModelID,M.ModelName,PU.Current_Rent as RentRange,M.Bedroom,M.Bathroom,M.Area,M.FloorPlan,1
		FROM tbl_PropertyUnits PU INNER JOIN tbl_Models M ON PU.Building=M.ModelName
		WHERE PU.AvailableDate<=@AvailableDate  AND PU.Current_Rent<=@Current_Rent AND PU.UID NOT IN (SELECT UnitId FROM @UnitsNotAvailable) AND (ISNULL(PU.IsFurnished,1)=@Furnished OR @Furnished=0)

		INSERT INTO @RentRangeData (ModelName,RentMinRange,RentMaxRange,NoAvailable)
		SELECT M.ModelName, M.MinRent, M.MaxRent, COUNT(*)  FROM tbl_Models M INNER JOIN @ModelData MD ON M.ModelID=MD.ModelID
		GROUP BY M.ModelName, M.MinRent, M.MaxRent

		IF(@SortOrder=0)
		BEGIN
			SELECT DISTINCT MD.ModelName,'$'+RR.RentMinRange +' - $'+RR.RentMaxRange as RentRange,RR.NoAvailable,MD.Bathroom,MD.Bedroom,MD.Area,MD.FloorPlan, RR.RentMinRange  FROM @ModelData MD INNER JOIN @RentRangeData  RR ON MD.ModelName=RR.ModelName ORDER BY RR.RentMinRange ASC, MD.ModelName
		END
		ELSE
		BEGIN
			SELECT DISTINCT MD.ModelName,'$'+RR.RentMinRange +' - $'+RR.RentMaxRange as RentRange,RR.NoAvailable,MD.Bathroom,MD.Bedroom,MD.Area,MD.FloorPlan, RR.RentMaxRange   FROM @ModelData MD INNER JOIN @RentRangeData  RR ON MD.ModelName=RR.ModelName ORDER BY RR.RentMaxRange DESC, MD.ModelName
		END
	END
END


GO

/****** Object:  StoredProcedure [dbo].[usp_GetCoapplicantSummaryData]    Script Date: 5/15/2020 6:02:49 PM ******/
DROP PROCEDURE [dbo].[usp_GetCoapplicantSummaryData]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetCoapplicantSummaryData]    Script Date: 5/15/2020 6:02:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--EXEC usp_usp_GetCoapplicantSummaryData 44 , 1482

CREATE PROCEDURE [dbo].[usp_GetCoapplicantSummaryData]
	@TenantID BIGINT,
	@UserId BIGINT
AS
BEGIN
	-- ApartmentInfo --
	SELECT UnitNo, Building, MoveInDate, ISNULL((SELECT TOP 1 CONVERT(VARCHAR(50),LeaseTerms)+' Mo' FROM tbl_LeaseTerms WHERE LTID=AN.LeaseTerm),'') AS LeaseTerm,
	PU.Bedroom, PU.Bathroom, PU.Area, PU.Deposit,AN.MonthlyCharges FROM tbl_ApplyNow AN INNER JOIN tbl_PropertyUnits PU ON AN.PropertyId=PU.UID WHERE ID=@TenantID
	
	-- ApplicantFullInfo --
	SELECT FirstName+' '+LastName AS ApplicantName, DateOfBirth AS ApplicantDOB, ISNULL(Mobile,'') AS ApplicantPhone, Email AS ApplicantEmail, SSN AS ApplicantSSN,
	CASE WHEN IDType=1 THEN 'Driver''s License' WHEN IDType=2 THEN 'Military ID' WHEN IDType=3 THEN 'Passport' WHEN IDType=4 THEN 'State Issued ID' ELSE '' END AS ApplicantIDType, IDNumber AS ApplicantIDNumber,
	(SELECT StateName FROM tbl_State WHERE ID=OT.State) AS ApplicantIDIssuingState,(SELECT CountryName FROM tbl_Country WHERE ID=OT.Country) AS ApplicantIssuingCountry,

	MoveInDateFrom AS CurrentMoveInDate, MonthlyPayment AS CurrentMonthlyPayment,
	(SELECT CountryName FROM tbl_Country WHERE ID=OT.Country) AS CurrentCountry,
	ISNULL(HomeAddress1,'') +CASE WHEN ISNULL(HomeAddress2,'')!='' THEN ', '+HomeAddress2 ELSE ',' END+ ISNULL(CityHome,'')+', '+ISNULL((SELECT StateName FROM tbl_State WHERE ID=OT.StateHome),'')+' '+ ISNULL(ZipHome,'') AS CurrentAddress,
	
	ApartmentCommunity AS CurrentAptCommunity,
	ManagementCompany AS CurrentMgmtCompany, ManagementCompanyPhone AS CurrentMgmtCompanyPhone,CASE WHEN IsProprNoticeLeaseAgreement=1 THEN 'Yes' ELSE 'No' END AS CurrentNoticeGiven,

	CASE WHEN Evicted=1 THEN 'No' ELSE 'Yes' END AS Evicted, ISNULL(EvictedDetails,'') AS EvictedDetails,
	CASE WHEN ConvictedFelony=1 THEN 'No' ELSE 'Yes' END AS ConvictedFelony, ISNULL(ConvictedFelonyDetails,'') AS ConvictedFelonyDetails,
	CASE WHEN CriminalChargPen=1 THEN 'No' ELSE 'Yes' END AS CriminalChargPen, ISNULL(CriminalChargPenDetails,'') AS CriminalChargPenDetails,
	CASE WHEN DoYouSmoke=1 THEN 'No' ELSE 'Yes' END AS DoYouSmoke,CASE WHEN ReferredResident=1 THEN 'No' ELSE 'Yes' END AS ReferredResident, ISNULL(ReferredResidentName,'') AS ReferredResidentName,
	CASE WHEN ReferredBrokerMerchant=1 THEN 'No' ELSE 'Yes' END AS ReferredBrokerMerchant, 
	
	EmployerName AS CurrentEmployerName, ISNULL(JobTitle,'') AS CurrentJobTitle,
	CASE WHEN JobType=1 THEN 'Permanent' WHEN JobType=2 THEN 'Contract Basis' ELSE '' END AS CurrentJobType, StartDate AS CurrentStartDate, Income AS CurrentAnnualIncome, AdditionalIncome AS CurrentAnnualAddIncome,
	ISNULL(SupervisorName,'') AS CurrentSupervisorName, ISNULL(SupervisorPhone,'') AS CurrentSupervisorPhone,
	(SELECT CountryName FROM tbl_Country WHERE ID=OT.OfficeCountry) AS CurrentOfficeCountry,
	ISNULL(OfficeAddress1,'') +CASE WHEN ISNULL(OfficeAddress2,'')!='' THEN ', '+OfficeAddress2 ELSE ',' END+ ISNULL(OfficeCity,'')+', '+ISNULL((SELECT StateName FROM tbl_State WHERE ID=OT.OfficeState),'')+' '+ ISNULL(OfficeZip,'') AS CurrentEmployeerAddress,
	
	Relationship AS EmergencyRelationship, ISNULL(EmergencyFirstName,'')+' '+ ISNULL(EmergencyLastName,'') AS EmergencyName,EmergencyMobile AS EmergencyHomePhone,
	ISNULL(EmergencyAddress1,'') +CASE WHEN ISNULL(EmergencyAddress2,'')!='' THEN ', '+EmergencyAddress2 ELSE ',' END+ ISNULL(EmergencyCityHome,'')+', '+ISNULL((SELECT StateName FROM tbl_State WHERE ID=OT.EmergencyStateHome),'')+' '+ ISNULL(EmergencyZipHome,'') AS EmergencyAddress,
	
	(SELECT CountryName FROM tbl_Country WHERE ID=OT.EmergencyCountry) AS EmergencyCountry, EmergencyEmail
	FROM tbl_TenantOnline OT WHERE OT.ParentTOID = @UserId

	--OtherResInfo--
	SELECT ISNULL(HomeAddress1,'') +CASE WHEN ISNULL(HomeAddress2,'')!='' THEN ', '+HomeAddress2 ELSE ',' END+ ISNULL(CityHome,'')+', '+ISNULL((SELECT StateName FROM tbl_State WHERE ID=ah.StateHome),'')+' '+ ISNULL(ZipHome,'') AS OtherAddress,
	(SELECT CountryName FROM tbl_Country WHERE ID=AH.Country) AS OtherCountry, MonthlyPayment AS OtherMonthlyPayment, ApartmentCommunity AS OtherAptCommunity,
	ManagementCompany AS OtherMgmtCompany,  ManagementCompanyPhone AS OtherMgmtCompanyPhone, MoveInDateFrom AS OtherMoveInDateFrom, MoveInDateTo AS OtherMoveInDateTo, 
	ISNULL(Reason,'') AS OtherResion, CASE WHEN IsProprNoticeLeaseAgreement=1 THEN 'Yes' ELSE 'No' END AS OtherNoticeGiven
	FROM tbl_ApplicantHistory AH WHERE ParentTOID = @UserId

	--OtherEmpInfo--
	SELECT EmployerName AS OtherEmployerName, ISNULL(JobTitle,'') AS OtherJobTitle,
	CASE WHEN JobType=1 THEN 'Permanent' WHEN JobType=2 THEN 'Contract Basis' ELSE '' END AS OtherJobType, StartDate AS OtherStartDate, TerminationDate AS OtherTerminationDate, 
	AnnualIncome AS OtherAnnualIncome, AddAnnualIncome AS OtherAnnualAddIncome, ISNULL(SupervisorName,'') AS OtherSupervisorName, ISNULL(SupervisorPhone,'') AS OtherSupervisorPhone,
	(SELECT CountryName FROM tbl_Country WHERE ID=EH.Country) AS OtherOfficeCountry,
	ISNULL(Address1,'') +CASE WHEN ISNULL(Address2,'')!='' THEN ', '+Address2 ELSE ',' END+ ISNULL(City,'')+', '+ISNULL((SELECT StateName FROM tbl_State WHERE ID=EH.State),'')+' '+ ISNULL(Zip,'') AS OtherEmployeerAddress,
	 ISNULL(TerminationReason,'') AS OtherTerminationReason
	FROM tbl_EmployerHistory EH WHERE ParentTOID = @UserId

	--PetInfo--
	SELECT PetName, Breed, Weight, VetsName FROM tbl_TenantPet WHERE AddedBy=@UserId

	--VehicleInfo--
	SELECT Make, Model, Year, Color, License, (SELECT StateName FROM tbl_State WHERE ID=V.State) AS StateName  FROM tbl_Vehicle V WHERE AddedBy=@UserId

END
GO

/****** Object:  StoredProcedure [dbo].[usp_GetGuarantorApplicationSummaryData]    Script Date: 5/15/2020 6:03:44 PM ******/
DROP PROCEDURE [dbo].[usp_GetGuarantorApplicationSummaryData]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetGuarantorApplicationSummaryData]    Script Date: 5/15/2020 6:03:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--EXEC usp_GetGuarantorApplicationSummaryData 44,1484

CREATE PROCEDURE [dbo].[usp_GetGuarantorApplicationSummaryData]
	@TenantID BIGINT,
	@UserId bigint
AS
BEGIN
	-- ApartmentInfo --
	SELECT UnitNo, Building, MoveInDate, ISNULL((SELECT TOP 1 CONVERT(VARCHAR(50),LeaseTerms)+' Mo' FROM tbl_LeaseTerms WHERE LTID=AN.LeaseTerm),'') AS LeaseTerm,
	PU.Bedroom, PU.Bathroom, PU.Area, PU.Deposit,AN.MonthlyCharges FROM tbl_ApplyNow AN INNER JOIN tbl_PropertyUnits PU ON AN.PropertyId=PU.UID WHERE ID=@TenantID
	
	-- ApplicantFullInfo --
	SELECT FirstName+' '+LastName AS ApplicantName, DateOfBirth AS ApplicantDOB, ISNULL(Mobile,'') AS ApplicantPhone, Email AS ApplicantEmail, SSN AS ApplicantSSN,
	CASE WHEN IDType=1 THEN 'Driver''s License' WHEN IDType=2 THEN 'Military ID' WHEN IDType=3 THEN 'Passport' WHEN IDType=4 THEN 'State Issued ID' ELSE '' END AS ApplicantIDType, IDNumber AS ApplicantIDNumber,
	(SELECT StateName FROM tbl_State WHERE ID=OT.State) AS ApplicantIDIssuingState,(SELECT CountryName FROM tbl_Country WHERE ID=OT.Country) AS ApplicantIssuingCountry,

	MoveInDateFrom AS CurrentMoveInDate, MonthlyPayment AS CurrentMonthlyPayment,
	(SELECT CountryName FROM tbl_Country WHERE ID=OT.Country) AS CurrentCountry,
	ISNULL(HomeAddress1,'') +CASE WHEN ISNULL(HomeAddress2,'')!='' THEN ', '+HomeAddress2 ELSE ',' END+ ISNULL(CityHome,'')+', '+ISNULL((SELECT StateName FROM tbl_State WHERE ID=OT.StateHome),'')+' '+ ISNULL(ZipHome,'') AS CurrentAddress,
	
	ApartmentCommunity AS CurrentAptCommunity,
	ManagementCompany AS CurrentMgmtCompany, ManagementCompanyPhone AS CurrentMgmtCompanyPhone,CASE WHEN IsProprNoticeLeaseAgreement=1 THEN 'Yes' ELSE 'No' END AS CurrentNoticeGiven,

	CASE WHEN Evicted=1 THEN 'No' ELSE 'Yes' END AS Evicted, ISNULL(EvictedDetails,'') AS EvictedDetails,
	CASE WHEN ConvictedFelony=1 THEN 'No' ELSE 'Yes' END AS ConvictedFelony, ISNULL(ConvictedFelonyDetails,'') AS ConvictedFelonyDetails,
	CASE WHEN CriminalChargPen=1 THEN 'No' ELSE 'Yes' END AS CriminalChargPen, ISNULL(CriminalChargPenDetails,'') AS CriminalChargPenDetails,
	CASE WHEN DoYouSmoke=1 THEN 'No' ELSE 'Yes' END AS DoYouSmoke,CASE WHEN ReferredResident=1 THEN 'No' ELSE 'Yes' END AS ReferredResident, ISNULL(ReferredResidentName,'') AS ReferredResidentName,
	CASE WHEN ReferredBrokerMerchant=1 THEN 'No' ELSE 'Yes' END AS ReferredBrokerMerchant, 
	
	EmployerName AS CurrentEmployerName, ISNULL(JobTitle,'') AS CurrentJobTitle,
	CASE WHEN JobType=1 THEN 'Permanent' WHEN JobType=2 THEN 'Contract Basis' ELSE '' END AS CurrentJobType, StartDate AS CurrentStartDate, Income AS CurrentAnnualIncome, AdditionalIncome AS CurrentAnnualAddIncome,
	ISNULL(SupervisorName,'') AS CurrentSupervisorName, ISNULL(SupervisorPhone,'') AS CurrentSupervisorPhone,
	(SELECT CountryName FROM tbl_Country WHERE ID=OT.OfficeCountry) AS CurrentOfficeCountry,
	ISNULL(OfficeAddress1,'') +CASE WHEN ISNULL(OfficeAddress2,'')!='' THEN ', '+OfficeAddress2 ELSE ',' END+ ISNULL(OfficeCity,'')+', '+ISNULL((SELECT StateName FROM tbl_State WHERE ID=OT.OfficeState),'')+' '+ ISNULL(OfficeZip,'') AS CurrentEmployeerAddress,
	
	Relationship AS EmergencyRelationship, ISNULL(EmergencyFirstName,'')+' '+ ISNULL(EmergencyLastName,'') AS EmergencyName,EmergencyMobile AS EmergencyHomePhone,
	ISNULL(EmergencyAddress1,'') +CASE WHEN ISNULL(EmergencyAddress2,'')!='' THEN ', '+EmergencyAddress2 ELSE ',' END+ ISNULL(EmergencyCityHome,'')+', '+ISNULL((SELECT StateName FROM tbl_State WHERE ID=OT.EmergencyStateHome),'')+' '+ ISNULL(EmergencyZipHome,'') AS EmergencyAddress,
	
	(SELECT CountryName FROM tbl_Country WHERE ID=OT.EmergencyCountry) AS EmergencyCountry, EmergencyEmail
	FROM tbl_TenantOnline OT WHERE ParentTOID=@UserId

	
	--OtherEmpInfo--
	SELECT EmployerName AS OtherEmployerName, ISNULL(JobTitle,'') AS OtherJobTitle,
	CASE WHEN JobType=1 THEN 'Permanent' WHEN JobType=2 THEN 'Contract Basis' ELSE '' END AS OtherJobType, StartDate AS OtherStartDate, TerminationDate AS OtherTerminationDate, 
	AnnualIncome AS OtherAnnualIncome, AddAnnualIncome AS OtherAnnualAddIncome, ISNULL(SupervisorName,'') AS OtherSupervisorName, ISNULL(SupervisorPhone,'') AS OtherSupervisorPhone,
	(SELECT CountryName FROM tbl_Country WHERE ID=EH.Country) AS OtherOfficeCountry,
	ISNULL(Address1,'') +CASE WHEN ISNULL(Address2,'')!='' THEN ', '+Address2 ELSE ',' END+ ISNULL(City,'')+', '+ISNULL((SELECT StateName FROM tbl_State WHERE ID=EH.State),'')+' '+ ISNULL(Zip,'') AS OtherEmployeerAddress,
	 ISNULL(TerminationReason,'') AS OtherTerminationReason
	FROM tbl_EmployerHistory EH WHERE ParentTOID=@UserId
END
GO


/****** Object:  StoredProcedure [dbo].[usp_GetQuotationNoByEmail]    Script Date: 5/15/2020 3:39:06 PM ******/
DROP PROCEDURE [dbo].[usp_GetQuotationNoByEmail]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetQuotationNoByEmail]    Script Date: 5/15/2020 3:39:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[usp_GetQuotationNoByEmail]
@Email varchar(100)
as
begin
select AN.ID, AN.PropertyId, AN.FirstName,AN.LastName, AN.Email,AN.Phone,AN.UserId,
(CONVERT(varchar(20),CreatedDate,112) + REPLACE(CONVERT(varchar(5),CreatedDate,108),':','')) + '-' + (CONVERT(varchar(20),ID)) as QuotationNo 
from tbl_applynow AN
where 
AN.Email = @Email
end;
GO

/****** Object:  StoredProcedure [dbo].[usp_GetSignInCredentialByQuotationNo]    Script Date: 5/15/2020 1:22:36 PM ******/
DROP PROCEDURE [dbo].[usp_GetSignInCredentialByQuotationNo]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetSignInCredentialByQuotationNo]    Script Date: 5/15/2020 1:22:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_GetSignInCredentialByQuotationNo]
@QuotationNo varchar(100)
as
begin
select AN.ID, AN.PropertyId, AN.FirstName,AN.LastName, AN.Email,AN.Phone,AN.UserId,
(CONVERT(varchar(20),CreatedDate,112) + REPLACE(CONVERT(varchar(5),CreatedDate,108),':','')) + '-' + (CONVERT(varchar(20),ID)) as QuotationNo 
from tbl_applynow AN
where 
(CONVERT(varchar(20),CreatedDate,112) + REPLACE(CONVERT(varchar(5),CreatedDate,108),':','')) + '-' + (CONVERT(varchar(20),ID)) = @QuotationNo
end;
GO

/****** Object:  StoredProcedure [dbo].[usp_GetPreMovingPaginationAndSearchData]    Script Date: 5/16/2020 12:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--EXEC usp_GetPreMovingPaginationAndSearchData '2019-04-01','2020-04-30',1,10,'FirstName','ASC'
ALTER PROCEDURE [dbo].[usp_GetPreMovingPaginationAndSearchData]
	@PageNumber		INT,
	@NumberOfRows	INT,
	@SortBy         varchar(20),
	@OrderBy        varchar(20),
	@FromDate		DATETIME='01/01/1900',
	@ToDate			DATETIME='12/31/2150'
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
    where AN.Status = 'Approved' and AN.CreatedDate BETWEEN @FromDate AND @ToDate+' 23:59:59'
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











