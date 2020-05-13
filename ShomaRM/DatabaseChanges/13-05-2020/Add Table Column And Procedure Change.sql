alter table [dbo].[tbl_TenantOnline] add TaxReturn4 nvarchar(500)
GO
alter table [dbo].[tbl_TenantOnline] add TaxReturn5 nvarchar(500)
GO
alter table [dbo].[tbl_TenantOnline] add TaxReturn6 nvarchar(500)
GO
alter table [dbo].[tbl_TenantOnline] add TaxReturn7 nvarchar(500)
GO
alter table [dbo].[tbl_TenantOnline] add TaxReturn8 nvarchar(500)
GO

alter table [dbo].[tbl_TenantOnline] add TaxReturnOrginalFile4 nvarchar(500)
GO
alter table [dbo].[tbl_TenantOnline] add TaxReturnOrginalFile5 nvarchar(500)
GO
alter table [dbo].[tbl_TenantOnline] add TaxReturnOrginalFile6 nvarchar(500)
GO
alter table [dbo].[tbl_TenantOnline] add TaxReturnOrginalFile7 nvarchar(500)
GO
alter table [dbo].[tbl_TenantOnline] add TaxReturnOrginalFile8 nvarchar(500)
GO

alter table [dbo].[tbl_TenantOnline] add IsFedralTax int
GO
alter table [dbo].[tbl_TenantOnline] add IsBankState int
GO


/****** Object:  StoredProcedure [dbo].[usp_GetTenantOnlineData]    Script Date: 05/11/2020 7:19:46 PM ******/
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

	   ,ISNULL([TaxReturn4],'') AS TaxReturn4
	  ,ISNULL([TaxReturn5],'') AS TaxReturn5
	   ,ISNULL([TaxReturn6],'') AS TaxReturn6
	  ,ISNULL([TaxReturn7],'') AS TaxReturn7
	  ,ISNULL([TaxReturn8],'') AS TaxReturn8

	  ,ISNULL([TaxReturnOrginalFile4],'') AS TaxReturnOrginalFile4
	  ,ISNULL([TaxReturnOrginalFile5],'') AS TaxReturnOrginalFile5
	  ,ISNULL([TaxReturnOrginalFile6],'') AS TaxReturnOrginalFile6
	  ,ISNULL([TaxReturnOrginalFile7],'') AS TaxReturnOrginalFile7
	  ,ISNULL([TaxReturnOrginalFile8],'') AS TaxReturnOrginalFile8
	  ,ISNULL([IsFedralTax] ,0) AS IsFedralTax
	  ,ISNULL([IsBankState] ,0) AS IsBankState
  FROM [tbl_TenantOnline]
  where ProspectID = @id AND ParentTOID =@toid
end;

GO

alter table [dbo].[tbl_TenantOnline] add ResidenceStatus int
GO
alter table [dbo].[tbl_TenantOnline] add ResidenceNotes nvarchar(max)
GO
alter table [dbo].[tbl_TenantOnline] add EmpStatus int
GO
alter table [dbo].[tbl_TenantOnline] add EmpNotes nvarchar(max)
GO
alter table [dbo].[tbl_BackgroundScreening] add Notes nvarchar(max)
GO
alter table [dbo].[tbl_ApplyNow] add Notes nvarchar(max)
GO
alter table tbl_Vehicle Add VehicleType INT
GO