GO
/****** Object:  StoredProcedure [dbo].[usp_GetOnlineTransactionList]    Script Date: 5/11/2020 10:36:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--EXEC usp_GetOnlineTransactionList 1539, 137
ALTER PROCEDURE [dbo].[usp_GetOnlineTransactionList] 
	@TenantID BIGINT,
	@AppID	INT=0
AS
BEGIN

	DECLARE @IsPrimaryApp INT =0
	DECLARE @UserID BIGINT=0
	SET @IsPrimaryApp=ISNULL((SELECT CASE WHEN COUNT(*)>0 THEN 1 ELSE 0 END FROM tbl_Applicant WHERE ApplicantID=@AppID AND Type='Primary Applicant'),0)
	SET @UserID=ISNULL((SELECT UserID FROM tbl_Applicant WHERE ApplicantID=@AppID),0)

	DECLARE  @ApplicationIDs TABLE(AppID INT)

	INSERT INTO @ApplicationIDs (AppID)
	SELECT ApplicantID FROM tbl_Applicant WHERE (AddedBy=@UserID OR UserID=@UserID)

	SELECT [TransID]    
      ,[LeaseID]
      ,[Revision_Num]
      ,[Transaction_Date]
      ,ISNULL([Run],0) AS Run
      ,CASE WHEN [Charge_Type]=1 THEN 'Application Fees'  WHEN [Charge_Type]=2 THEN 'Move In Charge' WHEN [Charge_Type]=3 THEN 'Administrative Fee' WHEN [Charge_Type]=4 THEN 'Credit Check Fee' WHEN [Charge_Type]=5 THEN 'Background Check Fee'  ELSE 'CHARGE' END AS [Transaction_Type]
      ,[Description]
      ,[Charge_Date]
      ,(SELECT  top 1 Charge_Type FROM tbl_ChargeType WHERE CTID=T.[Charge_Type]) AS Charge_Type
      ,[Reference]
      ,ISNULL([Credit_Amount],0) AS [Credit_Amount]
      ,ISNULL([Charge_Amount],0) AS [Charge_Amount]
	  ,[TBankName]
      ,[CreatedDate]
	  ,A.FirstName+' '+A.LastName AS ApplicantName
	  ,A.Type AS ApplicantType
  FROM  [tbl_Transaction] T INNER JOIN tbl_OnlinePayment OP ON T.Transaction_Type=OP.ID LEFT JOIN tbl_Applicant A ON OP.ApplicantID=A.ApplicantID
  WHERE  (T.TenantID=@TenantID OR T.TenantID=@UserID)  AND (OP.ApplicantID IN (SELECT AppID FROm @ApplicationIDs) OR  @IsPrimaryApp=1)
END


GO
/****** Object:  StoredProcedure [dbo].[usp_GetTenantOnlineData]    Script Date: 5/11/2020 10:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[usp_GetTenantOnlineData]
	@id bigint ,
	@toid bigint=0
as
begin
SELECT [ID]
       ,[ProspectID]
      ,[FirstName]
      ,[MiddleInitial]
      ,[LastName]
      ,[DateOfBirth]
      ,ISNULL([Gender],0) as [Gender]
      ,[Email]
      ,[Mobile]
      ,[PassportNumber]
      ,[CountryIssuance]
      ,[DateIssuance]
      ,[DateExpire]
	  ,ISNULL([IDType],0) as [IDType]
	  ,ISNULL([State],0) as [State]
	  ,ISNULL([IDNumber],'') as [IDNumber]
      ,[Country]
      ,[HomeAddress1]
      ,[HomeAddress2]
	  ,ISNULL([StateHome],0) as [StateHome]
      ,[CityHome]
      ,[ZipHome]
	  ,ISNULL([RentOwn],0) as [RentOwn]
      ,[MoveInDate]
      ,[MonthlyPayment]
      ,[Reason]
      ,[EmployerName]
      ,[JobTitle]
	  ,ISNULL([JobType],0) as [JobType]
      ,[StartDate]
	  ,ISNULL([Income],0.00) as [Income]
	  ,ISNULL([AdditionalIncome],0.00) as [AdditionalIncome]
      ,[SupervisorName]
      ,[SupervisorPhone]
      ,[SupervisorEmail]
      ,[OfficeCountry]
      ,[OfficeAddress1]
      ,[OfficeAddress2]
	  ,ISNULL([OfficeState],0) as [OfficeState]
      ,[OfficeCity]
      ,[OfficeZip]
      ,[Relationship]
      ,[EmergencyFirstName]
      ,[EmergencyLastName]
      ,[EmergencyMobile]
      ,[EmergencyHomePhone]
      ,[EmergencyWorkPhone]
      ,[EmergencyEmail]
      ,[EmergencyCountry]
      ,[EmergencyAddress1]
      ,[EmergencyAddress2]
	  ,ISNULL([EmergencyStateHome],0) as [EmergencyStateHome]
      ,[EmergencyCityHome]
      ,[EmergencyZipHome]
      ,[CreatedDate]
	  ,[IsInternational]
	  ,ISNULL([OtherGender],'') as [OtherGender]
	  ,[MoveInDateFrom]
      ,[MoveInDateTo]
      ,[Country2]
      ,[HomeAddress12]
      ,[HomeAddress22]
      ,ISNULL([StateHome2],0) as [StateHome2]
      ,[CityHome2]
      ,[ZipHome2]
      ,ISNULL([RentOwn2],0) as [RentOwn2]
      ,[MoveInDateFrom2]
      ,[MoveInDateTo2]
      ,[MonthlyPayment2]
      ,[Reason2]
	   ,[SSN]
	  ,ISNULL([IsAdditionalRHistory],0) as [IsAdditionalRHistory]
	  ,ISNULL([PassportDocument],'') AS PassportDocument
	  ,ISNULL([IdentityDocument] ,'') AS IdentityDocument
	  ,ISNULL([TaxReturn],'') AS TaxReturn
	  ,ISNULL([TaxReturn2],'') AS TaxReturn2
	  ,ISNULL([TaxReturn3],'') AS TaxReturn3
	  ,ISNULL([HaveVehicle],'') AS HaveVehicle
	  ,ISNULL([TaxReturnOrginalFile],'') AS TaxReturnOrginalFile
	  ,ISNULL([TaxReturnOrginalFile2],'') AS TaxReturnOrginalFile2
	  ,ISNULL([TaxReturnOrginalFile3],'') AS TaxReturnOrginalFile3
	  ,ISNULL([PassportDocumentOriginalFile],'') AS PassportDocumentOriginalFile
	  ,ISNULL([IdentityDocumentOriginalFile] ,'') AS IdentityDocumentOriginalFile
	  ,ISNULL([IsPaystub] ,0) AS IsPaystub
	  ,ISNULL([HavePet],'') AS HavePet
	  --,ISNULL((SELECT StepCompleted FROM tbl_ApplyNow WHERE ID=[tbl_TenantOnline].ProspectID),1) AS StepCompleted

	  ,ISNULL(StepCompleted,0) AS StepCompleted

	  ,ISNULL([CountryOfOrigin],'2') as CountryOfOrigin
      ,ISNULL([Evicted],'1') as Evicted
      ,ISNULL([EvictedDetails],'') as EvictedDetails
      ,ISNULL([ConvictedFelony],'1') as ConvictedFelony
      ,ISNULL([ConvictedFelonyDetails],'') as ConvictedFelonyDetails
      ,ISNULL([CriminalChargPen],'1') as CriminalChargPen
      ,ISNULL([CriminalChargPenDetails],'') as CriminalChargPenDetails
      ,ISNULL([DoYouSmoke],'1') as DoYouSmoke
      ,ISNULL([ReferredResident],'1') as ReferredResident
      ,ISNULL([ReferredResidentName],'') as ReferredResidentName
      ,ISNULL([ReferredBrokerMerchant],'1') as ReferredBrokerMerchant
      ,ISNULL([ApartmentCommunity],'') as ApartmentCommunity
      ,ISNULL([ManagementCompany],'') as ManagementCompany
      ,ISNULL([ManagementCompanyPhone],'') as ManagementCompanyPhone
      ,ISNULL([IsProprNoticeLeaseAgreement],'1') as IsProprNoticeLeaseAgreement
  FROM [tbl_TenantOnline]
  where ProspectID = @id AND ParentTOID =@toid
end;






















