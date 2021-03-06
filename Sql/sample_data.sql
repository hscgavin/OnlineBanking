USE [WDTAssignment2NWBA]
GO
SET IDENTITY_INSERT [dbo].[TransactionCategory] ON 

INSERT [dbo].[TransactionCategory] ([TransactionCategoryID], [TransactionCategory], [Description], [ModifyDate]) VALUES (1, N'A', N'ATM Transactions', CAST(0x0000A24900000000 AS DateTime))
INSERT [dbo].[TransactionCategory] ([TransactionCategoryID], [TransactionCategory], [Description], [ModifyDate]) VALUES (2, N'S', N'Service Charges', CAST(0x0000A24900000000 AS DateTime))
INSERT [dbo].[TransactionCategory] ([TransactionCategoryID], [TransactionCategory], [Description], [ModifyDate]) VALUES (3, N'B', N'Bills Pay Transactions', CAST(0x0000A24900000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[TransactionCategory] OFF
SET IDENTITY_INSERT [dbo].[TransactionType] ON 

INSERT [dbo].[TransactionType] ([TransactionTypeID], [TransactionCategoryID], [TransactionType], [DebitCredit], [Description], [ModifyDate]) VALUES (1, 1, N'D', N'Credit', N'Deposit', CAST(0x0000A24900000000 AS DateTime))
INSERT [dbo].[TransactionType] ([TransactionTypeID], [TransactionCategoryID], [TransactionType], [DebitCredit], [Description], [ModifyDate]) VALUES (2, 1, N'W', N'Debit', N'Withdraw', CAST(0x0000A24900000000 AS DateTime))
INSERT [dbo].[TransactionType] ([TransactionTypeID], [TransactionCategoryID], [TransactionType], [DebitCredit], [Description], [ModifyDate]) VALUES (3, 1, N'T', N'Debit', N'Transfer', CAST(0x0000A24900000000 AS DateTime))
INSERT [dbo].[TransactionType] ([TransactionTypeID], [TransactionCategoryID], [TransactionType], [DebitCredit], [Description], [ModifyDate]) VALUES (4, 2, N'S', N'Debit', N'Service Charge', CAST(0x0000A24900000000 AS DateTime))
INSERT [dbo].[TransactionType] ([TransactionTypeID], [TransactionCategoryID], [TransactionType], [DebitCredit], [Description], [ModifyDate]) VALUES (5, 3, N'B', N'Debit', N'Bills Pay', CAST(0x0000A24900000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[TransactionType] OFF
