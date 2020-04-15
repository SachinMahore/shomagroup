GO
/****** Object:  Table [dbo].[tbl_Parking]    Script Date: 04/10/2020 3:06:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_Parking]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_Parking]
GO
/****** Object:  Table [dbo].[tbl_Parking]    Script Date: 04/10/2020 3:06:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_Parking]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tbl_Parking](
	[ParkingID] [bigint] IDENTITY(1,1) NOT NULL,
	[PropertyID] [bigint] NULL,
	[ParkingName] [nvarchar](50) NULL,
	[Charges] [money] NULL,
	[Description] [nvarchar](500) NULL,
	[Type] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_tbl_Parking] PRIMARY KEY CLUSTERED 
(
	[ParkingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[tbl_Parking] ON 

GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (1, 229, N'P-Unit 102', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (2, 230, N'P-Unit 103', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (3, 232, N'P-Unit 104', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (4, 233, N'P-Unit 105', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (5, 234, N'P-Unit 106', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (6, 235, N'P-Unit 107', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (7, 236, N'P-Unit 108', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (8, 237, N'P-Unit 109', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (9, 238, N'P-Unit 110', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (10, 239, N'P-Unit 111', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (11, 240, N'P-Unit 112', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (12, 242, N'P-Unit 114', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (13, 243, N'P-Unit 115', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (14, 244, N'P-Unit 116', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (15, 245, N'P-Unit 117', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (16, 246, N'P-Unit 118', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (17, 247, N'P-Unit 119', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (18, 248, N'P-Unit 120', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (19, 249, N'P-Unit 123', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (20, 250, N'P-Unit 124', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (21, 251, N'P-Unit 125', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (22, 252, N'P-Unit 126', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (23, 253, N'P-Unit 127', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (24, 254, N'P-Unit 128', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (25, 255, N'P-Unit 130', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (26, 256, N'P-Unit 131', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (27, 257, N'P-Unit 132', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (28, 258, N'P-Unit 133', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (29, 259, N'P-Unit 134', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (30, 260, N'P-Unit 135', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (31, 261, N'P-Unit 136', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (32, 262, N'P-Unit 137', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (33, 263, N'P-Unit 138', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (34, 264, N'P-Unit 139', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (35, 265, N'P-Unit 113', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (36, 266, N'P-Unit 129', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (37, 267, N'P-Unit 202', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (38, 268, N'P-Unit 203', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (39, 269, N'P-Unit 204', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (40, 270, N'P-Unit 205', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (41, 271, N'P-Unit 206', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (42, 272, N'P-Unit 207', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (43, 273, N'P-Unit 208', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (44, 274, N'P-Unit 209', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (45, 275, N'P-Unit 210', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (46, 276, N'P-Unit 211', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (47, 277, N'P-Unit 212', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (48, 278, N'P-Unit 213', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (49, 279, N'P-Unit 214', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (50, 280, N'P-Unit 215', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (51, 281, N'P-Unit 216', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (52, 282, N'P-Unit 217', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (53, 283, N'P-Unit 218', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (54, 284, N'P-Unit 219', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (55, 285, N'P-Unit 220', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (56, 286, N'P-Unit 221', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (57, 287, N'P-Unit 222', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (58, 288, N'P-Unit 223', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (59, 289, N'P-Unit 224', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (60, 290, N'P-Unit 225', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (61, 291, N'P-Unit 226', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (62, 292, N'P-Unit 227', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (63, 293, N'P-Unit 228', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (64, 294, N'P-Unit 229', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (65, 295, N'P-Unit 230', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (66, 296, N'P-Unit 231', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (67, 297, N'P-Unit 232', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (68, 298, N'P-Unit 233', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (69, 299, N'P-Unit 234', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (70, 300, N'P-Unit 235', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (71, 301, N'P-Unit 236', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (72, 302, N'P-Unit 237', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (73, 303, N'P-Unit 238', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (74, 304, N'P-Unit 239', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (75, 305, N'P-Unit 302', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (76, 306, N'P-Unit 303', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (77, 307, N'P-Unit 304', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (78, 308, N'P-Unit 305', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (79, 309, N'P-Unit 306', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (80, 310, N'P-Unit 307', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (81, 311, N'P-Unit 308', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (82, 312, N'P-Unit 309', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (83, 313, N'P-Unit 310', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (84, 314, N'P-Unit 311', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (85, 315, N'P-Unit 312', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (86, 316, N'P-Unit 313', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (87, 317, N'P-Unit 314', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (88, 318, N'P-Unit 315', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (89, 319, N'P-Unit 316', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (90, 320, N'P-Unit 317', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (91, 321, N'P-Unit 318', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (92, 322, N'P-Unit 319', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (93, 323, N'P-Unit 320', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (94, 324, N'P-Unit 321', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (95, 325, N'P-Unit 322', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (96, 326, N'P-Unit 323', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (97, 327, N'P-Unit 324', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (98, 328, N'P-Unit 325', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (99, 329, N'P-Unit 326', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (100, 330, N'P-Unit 327', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (101, 331, N'P-Unit 328', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (102, 333, N'P-Unit 329', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (103, 334, N'P-Unit 330', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (104, 335, N'P-Unit 331', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (105, 336, N'P-Unit 332', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (106, 337, N'P-Unit 333', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (107, 338, N'P-Unit 334', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (108, 339, N'P-Unit 335', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (109, 340, N'P-Unit 336', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (110, 341, N'P-Unit 337', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (111, 342, N'P-Unit 338', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (112, 343, N'P-Unit 339', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (113, 344, N'P-Unit 402', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (114, 345, N'P-Unit 403', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (115, 346, N'P-Unit 404', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (116, 347, N'P-Unit 405', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (117, 348, N'P-Unit 406', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (118, 349, N'P-Unit 407', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (119, 350, N'P-Unit 408', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (120, 351, N'P-Unit 409', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (121, 352, N'P-Unit 410', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (122, 353, N'P-Unit 411', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (123, 354, N'P-Unit 412', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (124, 355, N'P-Unit 413', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (125, 356, N'P-Unit 414', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (126, 357, N'P-Unit 415', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (127, 358, N'P-Unit 416', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (128, 359, N'P-Unit 417', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (129, 360, N'P-Unit 418', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (130, 361, N'P-Unit 419', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (131, 363, N'P-Unit 420', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (132, 364, N'P-Unit 421', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (133, 365, N'P-Unit 422', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (134, 366, N'P-Unit 423', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (135, 367, N'P-Unit 424', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (136, 368, N'P-Unit 425', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (137, 369, N'P-Unit 426', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (138, 370, N'P-Unit 427', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (139, 371, N'P-Unit 428', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (140, 372, N'P-Unit 429', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (141, 373, N'P-Unit 430', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (142, 374, N'P-Unit 431', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (143, 375, N'P-Unit 432', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (144, 376, N'P-Unit 433', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (145, 377, N'P-Unit 434', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (146, 378, N'P-Unit 435', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (147, 380, N'P-Unit 436', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (148, 381, N'P-Unit 437', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (149, 382, N'P-Unit 438', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (150, 383, N'P-Unit 439', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (151, 384, N'P-Unit 502', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (152, 385, N'P-Unit 503', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (153, 386, N'P-Unit 504', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (154, 387, N'P-Unit 505', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (155, 388, N'P-Unit 506', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (156, 389, N'P-Unit 507', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (157, 390, N'P-Unit 508', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (158, 391, N'P-Unit 509', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (159, 392, N'P-Unit 510', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (160, 393, N'P-Unit 511', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (161, 394, N'P-Unit 512', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (162, 395, N'P-Unit 513', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (163, 396, N'P-Unit 514', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (164, 398, N'P-Unit 515', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (165, 399, N'P-Unit 516', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (166, 400, N'P-Unit 517', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (167, 401, N'P-Unit 518', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (168, 402, N'P-Unit 519', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (169, 403, N'P-Unit 520', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (170, 404, N'P-Unit 521', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (171, 405, N'P-Unit 522', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (172, 406, N'P-Unit 523', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (173, 407, N'P-Unit 524', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (174, 408, N'P-Unit 525', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (175, 409, N'P-Unit 526', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (176, 410, N'P-Unit 527', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (177, 411, N'P-Unit 528', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (178, 412, N'P-Unit 529', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (179, 413, N'P-Unit 530', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (180, 414, N'P-Unit 531', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (181, 415, N'P-Unit 532', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (182, 416, N'P-Unit 533', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (183, 417, N'P-Unit 534', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (184, 418, N'P-Unit 535', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (185, 419, N'P-Unit 536', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (186, 420, N'P-Unit 537', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (187, 421, N'P-Unit 538', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (188, 422, N'P-Unit 539', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (189, 423, N'P-Unit 602', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (190, 424, N'P-Unit 603', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (191, 425, N'P-Unit 604', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (192, 426, N'P-Unit 605', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (193, 427, N'P-Unit 606', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (194, 428, N'P-Unit 607', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (195, 429, N'P-Unit 609', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (196, 430, N'P-Unit 608', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (197, 431, N'P-Unit 611', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (198, 432, N'P-Unit 610', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (199, 433, N'P-Unit 613', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (200, 434, N'P-Unit 612', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (201, 435, N'P-Unit 615', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (202, 436, N'P-Unit 614', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (203, 437, N'P-Unit 616', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (204, 438, N'P-Unit 618', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (205, 439, N'P-Unit 617', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (206, 440, N'P-Unit 620', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (207, 441, N'P-Unit 619', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (208, 442, N'P-Unit 621', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (209, 443, N'P-unit 622', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (210, 444, N'P-Unit 623', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (211, 445, N'P-Unit 624', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (212, 446, N'P-Unit 625', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (213, 447, N'P-Unit 626', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (214, 448, N'P-Unit 627', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (215, 449, N'P-Unit 628', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (216, 450, N'P-Unit 629', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (217, 451, N'P-Unit 630', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (218, 452, N'P-Unit 631', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (219, 453, N'P-Unit 632', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (220, 454, N'P-Unit 633', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (221, 455, N'P-Unit 634', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (222, 456, N'P-Unit 635', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (223, 457, N'P-Unit 636', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (224, 458, N'P-Unit 637', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (225, 459, N'P-Unit 638', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (226, 460, N'P-Unit 639', 100.0000, N'E-3', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (227, 265, N'P-Unit 113', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (228, 266, N'P-Unit 129', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (229, 267, N'P-Unit 202', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (230, 278, N'P-Unit 213', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (231, 294, N'P-Unit 229', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (232, 303, N'P-Unit 238', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (233, 305, N'P-Unit 302', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (234, 316, N'P-Unit 313', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (235, 333, N'P-Unit 329', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (236, 342, N'P-Unit 338', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (237, 344, N'P-Unit 402', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (238, 355, N'P-Unit 413', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (239, 372, N'P-Unit 429', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (240, 382, N'P-Unit 438', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (241, 384, N'P-Unit 502', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (242, 395, N'P-Unit 513', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (243, 412, N'P-Unit 529', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (244, 421, N'P-Unit 538', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (245, 423, N'P-Unit 602', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (246, 433, N'P-Unit 613', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (247, 450, N'P-Unit 629', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (248, 459, N'P-Unit 638', 100.0000, N'W-4', 1, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (249, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (250, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (251, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (252, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (253, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (254, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (255, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (256, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (257, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (258, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (259, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (260, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (261, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (262, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (263, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (264, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (265, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (266, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (267, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (268, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (269, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (270, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (271, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (272, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (273, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (274, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (275, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (276, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (277, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (278, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (279, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (280, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (281, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (282, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (283, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (284, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (285, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (286, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (287, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (288, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (289, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (290, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (291, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (292, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (293, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (294, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (295, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (296, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (297, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (298, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (299, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (300, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (301, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (302, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (303, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (304, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (305, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (306, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (307, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (308, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (309, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (310, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (311, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (312, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (313, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (314, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (315, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (316, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (317, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (318, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (319, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (320, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (321, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (322, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (323, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (324, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (325, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (326, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (327, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (328, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (329, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (330, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (331, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (332, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (333, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (334, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (335, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
INSERT [dbo].[tbl_Parking] ([ParkingID], [PropertyID], [ParkingName], [Charges], [Description], [Type], [Status]) VALUES (336, 0, N'AddPark', 100.0000, N'E-5', 2, NULL)
GO
SET IDENTITY_INSERT [dbo].[tbl_Parking] OFF
GO
