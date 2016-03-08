USE [WDTAssignment2NWBA]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 8/09/2013 8:58:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[TransactionID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionTypeID] [int] NOT NULL,
	[TransactionType] [nvarchar](1) NULL,
	[AccountNumber] [int] NOT NULL,
	[DestinationAccount] [int] NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[Comment] [nvarchar](255) NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TransactionCategory]    Script Date: 8/09/2013 8:58:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TransactionCategory](
	[TransactionCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionCategory] [nvarchar](1) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_TransactionCategory] PRIMARY KEY CLUSTERED 
(
	[TransactionCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

GO
/****** Object:  Table [dbo].[TransactionType]    Script Date: 8/09/2013 8:58:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TransactionType](
	[TransactionTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionCategoryID] [int] NOT NULL,
	[TransactionType] [nvarchar](1) NOT NULL,
	[DebitCredit] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_TransactionType] PRIMARY KEY CLUSTERED 
(
	[TransactionTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TransactionType]  WITH CHECK ADD  CONSTRAINT [FK_TransactionType_TransactionCategory] FOREIGN KEY([TransactionCategoryID])
REFERENCES [dbo].[TransactionCategory] ([TransactionCategoryID])
GO
ALTER TABLE [dbo].[TransactionType] CHECK CONSTRAINT [FK_TransactionType_TransactionCategory]
GO

ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_TransactionType] FOREIGN KEY([TransactionTypeID])
REFERENCES [dbo].[TransactionType] ([TransactionTypeID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_TransactionType]
GO

ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Account] FOREIGN KEY([AccountNumber])
REFERENCES [dbo].[Account] ([AccountNumber])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Account]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Account1] FOREIGN KEY([DestinationAccount])
REFERENCES [dbo].[Account] ([AccountNumber])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Account1]
GO
