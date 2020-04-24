GO
ALTER TABLE tbl_ESignatureKeys ADD IsLeaseExecuted INT


GO

/****** Object:  StoredProcedure [dbo].[usp_GetApplicationSummaryData]    Script Date: 4/22/2020 5:05:44 PM ******/
DROP PROCEDURE [dbo].[usp_GetApplicationSummaryData]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetApplicationSummaryData]    Script Date: 4/22/2020 5:05:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--EXEC usp_GetApplicationSummaryData 39

CREATE PROCEDURE [dbo].[usp_GetApplicationSummaryData]
	@TenantID BIGINT
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
	FROM tbl_TenantOnline OT WHERE ProspectID=@TenantID

	--OtherResInfo--
	SELECT ISNULL(HomeAddress1,'') +CASE WHEN ISNULL(HomeAddress2,'')!='' THEN ', '+HomeAddress2 ELSE ',' END+ ISNULL(CityHome,'')+', '+ISNULL((SELECT StateName FROM tbl_State WHERE ID=ah.StateHome),'')+' '+ ISNULL(ZipHome,'') AS OtherAddress,
	(SELECT CountryName FROM tbl_Country WHERE ID=AH.Country) AS OtherCountry, MonthlyPayment AS OtherMonthlyPayment, ApartmentCommunity AS OtherAptCommunity,
	ManagementCompany AS OtherMgmtCompany,  ManagementCompanyPhone AS OtherMgmtCompanyPhone, MoveInDateFrom AS OtherMoveInDateFrom, MoveInDateTo AS OtherMoveInDateTo, 
	ISNULL(Reason,'') AS OtherResion, CASE WHEN IsProprNoticeLeaseAgreement=1 THEN 'Yes' ELSE 'No' END AS OtherNoticeGiven
	FROM tbl_ApplicantHistory AH WHERE TenantID=@TenantID

	--OtherEmpInfo--
	SELECT EmployerName AS OtherEmployerName, ISNULL(JobTitle,'') AS OtherJobTitle,
	CASE WHEN JobType=1 THEN 'Permanent' WHEN JobType=2 THEN 'Contract Basis' ELSE '' END AS OtherJobType, StartDate AS OtherStartDate, TerminationDate AS OtherTerminationDate, 
	AnnualIncome AS OtherAnnualIncome, AddAnnualIncome AS OtherAnnualAddIncome, ISNULL(SupervisorName,'') AS OtherSupervisorName, ISNULL(SupervisorPhone,'') AS OtherSupervisorPhone,
	(SELECT CountryName FROM tbl_Country WHERE ID=EH.Country) AS OtherOfficeCountry,
	ISNULL(Address1,'') +CASE WHEN ISNULL(Address2,'')!='' THEN ', '+Address2 ELSE ',' END+ ISNULL(City,'')+', '+ISNULL((SELECT StateName FROM tbl_State WHERE ID=EH.State),'')+' '+ ISNULL(Zip,'') AS OtherEmployeerAddress,
	 ISNULL(TerminationReason,'') AS OtherTerminationReason
	FROM tbl_EmployerHistory EH WHERE TenantId=@TenantID

	--PetInfo--
	SELECT PetName, Breed, Weight, VetsName FROM tbl_TenantPet WHERE TenantID=@TenantID

	--VehicleInfo--
	SELECT Make, Model, Year, Color, License, (SELECT StateName FROM tbl_State WHERE ID=V.State) AS StateName  FROM tbl_Vehicle V WHERE TenantID=@TenantID

END
GO


GO
/****** Object:  StoredProcedure [dbo].[usp_GetSignedList]    Script Date: 4/22/2020 5:01:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[usp_GetSignedList] 
	@TenantID BIGINT
AS
BEGIN
	SELECT [ESID]    
      ,[DateSigned]
      ,(SELECT  TOP 1 FirstName+' '+ LastName FROM tbl_Applicant WHERE ApplicantID=T.[ApplicantID]) AS ApplicantName
	  ,(SELECT  TOP 1 Email FROM tbl_Applicant WHERE ApplicantID=T.[ApplicantID]) AS Email
      ,[Key]
	  ,(SELECT  TOP 1 ApplicantID FROM tbl_Applicant WHERE ApplicantID=T.[ApplicantID]) AS ApplicantID
	  ,CASE WHEN ((SELECT  COUNT(*) FROM tbl_ESignatureKeys WHERE  TenantID=@TenantID AND ISNULL(DateSigned,'')='')>0) THEN 0 ELSE 1 END AS IsSignedAll
	  ,CASE WHEN ((SELECT  COUNT(*) FROM tbl_ESignatureKeys WHERE  TenantID=@TenantID AND ISNULL(IsLeaseExecuted,0)=0)>0) THEN 0 ELSE 1 END AS IsLeaseExecuted
	FROM  [tbl_ESignatureKeys] T
  WHERE  TenantID=@TenantID
END