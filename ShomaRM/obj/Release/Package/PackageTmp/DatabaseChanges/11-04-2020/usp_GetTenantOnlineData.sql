USE [ShomaRMDev]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetTenantOnlineData]    Script Date: 4/11/2020 2:30:36 PM ******/
DROP PROCEDURE [dbo].[usp_GetTenantOnlineData]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetTenantOnlineData]    Script Date: 4/11/2020 2:30:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





--exec usp_GetTenantOnlineData 1421

CREATE procedure [dbo].[usp_GetTenantOnlineData]
	@id bigint 
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
	  ,ISNULL((SELECT StepCompleted FROM tbl_ApplyNow WHERE ID=[tbl_TenantOnline].ProspectID),1) AS StepCompleted
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
  where ProspectID = @id
end;




GO

