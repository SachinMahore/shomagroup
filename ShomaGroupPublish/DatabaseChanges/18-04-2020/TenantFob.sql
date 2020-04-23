

/****** Object:  Table [dbo].[tbl_TenantFob]    Script Date: 4/18/2020 9:15:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_TenantFob](
	[TFobID] [bigint] IDENTITY(1,1) NOT NULL,
	[TenantID] [bigint] NULL,
	[FobID] [nvarchar](500) NULL,
	[ApplicantID] [bigint] NULL,
	[Status] [int] NULL,
	[OtherFirstName] [varchar](50) NULL,
	[OtherLastName] [varchar](50) NULL,
	[OtherRelationship] [varchar](50) NULL,
	[OtherId] [bigint] NULL,
 CONSTRAINT [PK_tbl_TenantFob] PRIMARY KEY CLUSTERED 
(
	[TFobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


