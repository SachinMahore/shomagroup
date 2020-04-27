GO

/****** Object:  Table [dbo].[tbl_BackgroundScreening]    Script Date: 4/24/2020 1:42:08 PM ******/
DROP TABLE [dbo].[tbl_BackgroundScreening]
GO

/****** Object:  Table [dbo].[tbl_BackgroundScreening]    Script Date: 4/24/2020 1:42:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_BackgroundScreening](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenantId] [int] NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[OrderID] [int] NOT NULL,
	[Status] [nvarchar](max) NULL,
	[PDFUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_tbl_BackgroundScreening] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


