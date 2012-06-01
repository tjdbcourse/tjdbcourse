USE [lib]
GO
/****** Object:  Table [dbo].[category]    Script Date: 06/01/2012 18:43:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[parent_id] [int] NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_category_category]    Script Date: 06/01/2012 18:43:19 ******/
ALTER TABLE [dbo].[category]  WITH CHECK ADD  CONSTRAINT [FK_category_category] FOREIGN KEY([parent_id])
REFERENCES [dbo].[category] ([id])
GO
ALTER TABLE [dbo].[category] CHECK CONSTRAINT [FK_category_category]
GO
