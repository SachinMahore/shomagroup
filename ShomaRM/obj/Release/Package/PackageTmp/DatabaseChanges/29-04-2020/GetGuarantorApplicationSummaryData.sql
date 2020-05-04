USE [ShomaRMDev]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetGuarantorApplicationSummaryData]    Script Date: 4/28/2020 8:52:51 PM ******/
DROP PROCEDURE [dbo].[usp_GetGuarantorApplicationSummaryData]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetGuarantorApplicationSummaryData]    Script Date: 4/28/2020 8:52:51 PM ******/
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

