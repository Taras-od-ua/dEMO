
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF object_id(N'dbo.Lists', N'U') IS NULL
BEGIN

CREATE TABLE [dbo].Lists(
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
  CategoryId [bigint] NOT NULL,
  [is_deleted] [bit] NOT NULL,
	[created_at] [datetime] NOT NULL,
	[updated_at] [datetime] NULL,
	
 CONSTRAINT [PK_Lists] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Lists] ADD  CONSTRAINT [DF_Lists_is_Lists]  DEFAULT ((0)) FOR [is_deleted]

ALTER TABLE [dbo].Lists  WITH CHECK ADD  CONSTRAINT [FK_lists_cats] FOREIGN KEY([CategoryId])
	REFERENCES [dbo].Categories ([Id])

CREATE NONCLUSTERED INDEX IX_is_deleted ON dbo.Lists
		(
		Id, [is_deleted]
		) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

END
GO

