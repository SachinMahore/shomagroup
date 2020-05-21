alter table [dbo].[tbl_TenantOnline] Add ParentTOID BIGINT
GO
alter table [dbo].[tbl_ApplicantHistory] Add ParentTOID BIGINT
GO
alter table [dbo].[tbl_EmployerHistory] Add ParentTOID BIGINT
GO
alter table [dbo].[tbl_Login] Add ParentUserID BIGINT