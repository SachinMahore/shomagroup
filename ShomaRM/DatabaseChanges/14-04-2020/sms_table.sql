GO

/****** Object:  Table [dbo].[tbl_SMSMessages]    Script Date: 4/14/2020 8:23:44 PM ******/
DROP TABLE [dbo].[tbl_SMSMessages]
GO

/****** Object:  Table [dbo].[tbl_SMSMessages]    Script Date: 4/14/2020 8:23:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_SMSMessages](
	[MsgID] [bigint] IDENTITY(1,1) NOT NULL,
	[PhoneNumber] [varchar](15) NULL,
	[Message] [varchar](1000) NULL,
	[ReceivedDate] [datetime] NULL,
 CONSTRAINT [PK_tbl_SMSMessages] PRIMARY KEY CLUSTERED 
(
	[MsgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


