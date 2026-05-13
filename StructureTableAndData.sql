/*
 Navicat Premium Dump SQL

 Source Server         : AdventureWork
 Source Server Type    : SQL Server
 Source Server Version : 16004250 (16.00.4250)
 Source Host           : localhost:1433
 Source Catalog        : E-Commerce
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 16004250 (16.00.4250)
 File Encoding         : 65001

 Date: 13/05/2026 18:20:10
*/


-- ----------------------------
-- Table structure for __EFMigrationsHistory
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type IN ('U'))
	DROP TABLE [dbo].[__EFMigrationsHistory]
GO

CREATE TABLE [dbo].[__EFMigrationsHistory] (
  [MigrationId] nvarchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [ProductVersion] nvarchar(32) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[__EFMigrationsHistory] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of __EFMigrationsHistory
-- ----------------------------
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20260508140424_InitialCreate', N'9.0.0')
GO

INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20260508141901_AddConfiguration', N'9.0.0')
GO

INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20260508142033_UpdateDatabaseTest', N'9.0.0')
GO

INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20260513081439_AddAdminTable', N'9.0.0')
GO

INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20260513095419_AddAdminUserTable', N'9.0.0')
GO


-- ----------------------------
-- Table structure for AdminUsers
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AdminUsers]') AND type IN ('U'))
	DROP TABLE [dbo].[AdminUsers]
GO

CREATE TABLE [dbo].[AdminUsers] (
  [Id] uniqueidentifier  NOT NULL,
  [Name] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Email] nvarchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [PasswordHash] nvarchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [CreatedAt] datetime2(7)  NOT NULL,
  [CreatedBy] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [UpdatedAt] datetime2(7)  NULL,
  [UpdatedBy] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [IsDeleted] bit  NOT NULL
)
GO

ALTER TABLE [dbo].[AdminUsers] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of AdminUsers
-- ----------------------------
INSERT INTO [dbo].[AdminUsers] ([Id], [Name], [Email], [PasswordHash], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'AB2958E7-794F-4913-B446-10DB65E47EBD', N'Admin', N'admin@gmail.com', N'$2a$12$Ui/dFrGDj7GO2lLOg20pN.mOmQ.tEsWFlOGrtLKZK36CWRvUHBuue', N'2026-05-13 17:56:30.0000000', NULL, NULL, NULL, N'0')
GO


-- ----------------------------
-- Table structure for Categories
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type IN ('U'))
	DROP TABLE [dbo].[Categories]
GO

CREATE TABLE [dbo].[Categories] (
  [Id] uniqueidentifier  NOT NULL,
  [Name] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Description] nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [CreatedAt] datetime2(7)  NOT NULL,
  [CreatedBy] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [UpdatedAt] datetime2(7)  NULL,
  [UpdatedBy] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [IsDeleted] bit  NOT NULL
)
GO

ALTER TABLE [dbo].[Categories] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Categories
-- ----------------------------
INSERT INTO [dbo].[Categories] ([Id], [Name], [Description], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'A3BD55B8-8A7A-4377-904E-0115CC602989', N'Office & Stationery', N'Professional office equipment, writing tools, and organized supplies for work or study.', N'2026-05-10 09:32:16.3896442', NULL, N'2026-05-11 03:53:35.9848152', NULL, N'0')
GO

INSERT INTO [dbo].[Categories] ([Id], [Name], [Description], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'FCE2A9BD-B61D-4967-AA10-25A58209DACB', N'Sports & Outdoors', N'High-quality athletic gear, fitness equipment, and durable supplies for outdoor adventures.', N'2026-05-10 09:24:05.2703286', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Categories] ([Id], [Name], [Description], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'6E0722FF-6575-49F7-ADC5-331E9C85D60A', N'Beauty & Personal Care', N'Premium skincare, cosmetics, and wellness products for your daily self-care routine.', N'2026-05-10 09:23:04.8751570', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Categories] ([Id], [Name], [Description], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'881B9452-9E38-4F3A-92C0-5F4A8C9163FC', N'Automotive', N'Reliable vehicle parts, maintenance tools, and innovative car accessories for every driver.', N'2026-05-10 09:28:37.1495791', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Categories] ([Id], [Name], [Description], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'7AA9B9D5-B5D6-4246-9438-7CD06BFE5618', N'Electronics', N'Cutting-edge gadgets including smartphones, laptops, and high-performance computing accessories.', N'2026-05-10 09:22:07.7775328', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Categories] ([Id], [Name], [Description], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'757B0960-3E67-4021-8608-8B1074C8A37A', N'Toys & Hobbies', N'Engaging toys, collectibles, and creative DIY kits designed for all ages and interests.', N'2026-05-10 09:24:31.6610975', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Categories] ([Id], [Name], [Description], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'54B443CD-E3C8-49D1-B36B-8EC6B3839B5F', N'Groceries & Pantry', N'Fresh produce, essential food items, and daily household supplies delivered to your door.', N'2026-05-10 09:28:49.8824895', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Categories] ([Id], [Name], [Description], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'7EB4A026-171E-4D30-89D4-A6F82A1D052A', N'Books & Media', N'A diverse collection of literature, educational resources, and digital entertainment media.', N'2026-05-10 09:24:19.0519428', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Categories] ([Id], [Name], [Description], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'0B505B4C-7251-4963-B0EA-B4E9EA9CB581', N'Health & Wellness', N'Vitamins, supplements, and medical essentials to support a healthy and active lifestyle.', N'2026-05-10 09:29:02.4557375', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Categories] ([Id], [Name], [Description], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'6915C36A-C516-4BD8-AEA1-BD4B1ADBE948', N'Fashion & Apparel', N'Trendy clothing, footwear, and stylish accessories for men, women, and children.', N'2026-05-10 09:22:23.4435800', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Categories] ([Id], [Name], [Description], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'3FC2ACF7-BE10-438E-AFBA-DF7BF3712BD4', N'Book', N'No', N'2026-05-13 11:16:43.6009034', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Categories] ([Id], [Name], [Description], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'D4A19F8A-33DF-44F2-AD67-E205168F82A3', N'Pet Supplies', N'Nutritious food, fun toys, and comfort accessories tailored for your beloved pets.', N'2026-05-10 09:29:20.8771925', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Categories] ([Id], [Name], [Description], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'098E0EF8-1580-4351-B97D-EA269B4A5C4C', N'Home & Living', N'Modern furniture, decorative items, and essential appliances to enhance your living space.', N'2026-05-10 09:22:39.5149450', NULL, NULL, NULL, N'0')
GO


-- ----------------------------
-- Table structure for Customers
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND type IN ('U'))
	DROP TABLE [dbo].[Customers]
GO

CREATE TABLE [dbo].[Customers] (
  [Id] uniqueidentifier  NOT NULL,
  [Name] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [CreatedAt] datetime2(7)  NOT NULL,
  [CreatedBy] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [UpdatedAt] datetime2(7)  NULL,
  [UpdatedBy] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [IsDeleted] bit  NOT NULL,
  [Email] nvarchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Phone] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[Customers] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Customers
-- ----------------------------
INSERT INTO [dbo].[Customers] ([Id], [Name], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted], [Email], [Phone]) VALUES (N'0DBBA87E-80CA-421B-A78C-03E6D5657BB9', N'string', N'2026-05-11 08:46:30.1625709', NULL, NULL, NULL, N'0', N'test3@gmail.com', N'0123456789')
GO

INSERT INTO [dbo].[Customers] ([Id], [Name], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted], [Email], [Phone]) VALUES (N'F034C982-0B4A-4C3B-B664-409484AF807D', N'string', N'2026-05-11 08:47:09.9944559', NULL, NULL, NULL, N'0', N'test11@gmail.com', N'0123456789')
GO

INSERT INTO [dbo].[Customers] ([Id], [Name], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted], [Email], [Phone]) VALUES (N'04C56812-987E-4FA6-8D01-6173503ADCC7', N'string', N'2026-05-11 08:46:55.4436290', NULL, NULL, NULL, N'0', N'test8@gmail.com', N'0123456789')
GO

INSERT INTO [dbo].[Customers] ([Id], [Name], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted], [Email], [Phone]) VALUES (N'0164AA14-645A-4083-8524-8BF5C9C4B5B5', N'string', N'2026-05-11 08:11:06.9131790', NULL, NULL, NULL, N'0', N'test2@gmail.com', N'0123456789')
GO

INSERT INTO [dbo].[Customers] ([Id], [Name], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted], [Email], [Phone]) VALUES (N'BE3920BC-8BB1-466B-9202-8F23F2FAEC38', N'string', N'2026-05-11 08:46:34.9876114', NULL, NULL, NULL, N'0', N'test4@gmail.com', N'0123456789')
GO

INSERT INTO [dbo].[Customers] ([Id], [Name], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted], [Email], [Phone]) VALUES (N'400107F3-6B8C-4C0C-A122-99EF5D24036E', N'string', N'2026-05-11 08:46:51.6675254', NULL, NULL, NULL, N'0', N'test7@gmail.com', N'0123456789')
GO

INSERT INTO [dbo].[Customers] ([Id], [Name], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted], [Email], [Phone]) VALUES (N'DE303623-87EA-4B73-9767-C11339FA94B1', N'Viet Lam', N'2026-05-11 08:08:19.6345121', NULL, NULL, NULL, N'0', N'test1@gmail.com', N'0')
GO

INSERT INTO [dbo].[Customers] ([Id], [Name], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted], [Email], [Phone]) VALUES (N'93D7122D-E22E-4F41-A916-D674C68E87A9', N'string', N'2026-05-11 08:47:05.0582231', NULL, NULL, NULL, N'0', N'test10@gmail.com', N'0123456789')
GO

INSERT INTO [dbo].[Customers] ([Id], [Name], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted], [Email], [Phone]) VALUES (N'BA7A37BC-F0C2-4103-81B2-D7F05E650177', N'string', N'2026-05-11 08:47:00.8214882', NULL, NULL, NULL, N'0', N'test9@gmail.com', N'0123456789')
GO

INSERT INTO [dbo].[Customers] ([Id], [Name], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted], [Email], [Phone]) VALUES (N'AE0F9101-13BE-4C7D-90D5-EDA1A00DC41B', N'string', N'2026-05-11 08:46:41.3863467', NULL, NULL, NULL, N'0', N'test5@gmail.com', N'0123456789')
GO

INSERT INTO [dbo].[Customers] ([Id], [Name], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted], [Email], [Phone]) VALUES (N'517CCAEE-E6B0-4D82-B241-F3B4686F1246', N'string', N'2026-05-11 08:46:47.0412916', NULL, NULL, NULL, N'0', N'test6@gmail.com', N'0123456789')
GO


-- ----------------------------
-- Table structure for ProductImages
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductImages]') AND type IN ('U'))
	DROP TABLE [dbo].[ProductImages]
GO

CREATE TABLE [dbo].[ProductImages] (
  [Id] uniqueidentifier  NOT NULL,
  [Url] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [ProductId] uniqueidentifier  NOT NULL,
  [CreatedAt] datetime2(7)  NOT NULL,
  [CreatedBy] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [UpdatedAt] datetime2(7)  NULL,
  [UpdatedBy] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [IsDeleted] bit  NOT NULL
)
GO

ALTER TABLE [dbo].[ProductImages] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of ProductImages
-- ----------------------------
INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'037D97F0-2FA2-48E9-89A8-05F585B4EC4F', N'/uploads/99bbc277-9933-49cd-9368-149c6b305d72.jpg', N'825F232F-EC82-4DA1-A9B4-99510729274E', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'72F2F752-BEF7-4719-9A17-09D0062437F6', N'/uploads/68572f13-7228-43ac-8a49-cf712641858d.jpg', N'0B0F7F4F-D81D-49EE-B44F-560323A4392D', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'9A32374F-5199-416E-8C80-0A6BCB9CEAB1', N'/uploads/0c2cf9fb-5d66-4816-874d-ccf1009c5af1.jpg', N'EFA84F27-B2FC-4949-9503-A793F0E145DC', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'4F1F7C2B-C6E7-4CBF-9988-0A6CBA081095', N'/uploads/986dd8eb-49fe-498b-9ee0-29a0163019a6.jpg', N'906827D2-341F-4321-B472-1EA5D60213A2', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'6BBB324E-127D-48A5-98BC-0B069797C90D', N'/uploads/d7258162-1c9d-4deb-a4c3-fc66cf0cc59d.jpg', N'E6E6E69F-2D09-45A1-B194-AD2ADDEB5BD3', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'3870C6AA-27E3-4616-A191-0EC31621D0EF', N'/uploads/94de5b11-9f73-43c0-9ae1-901178359056.jpg', N'AF5D97ED-DC43-4A28-AFB5-0339FF5A38D5', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'6FE67362-4DFD-43EB-BF30-156941C3810D', N'/uploads/121e8cf7-1f2f-4052-ae91-e3d23446306b.jpg', N'F6EE9DCB-6458-49BE-A636-C42230F9E9B8', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'96D7754B-6081-4DF4-B496-17CAB6CAAC03', N'/uploads/e6d151fd-3239-458d-a686-9e20a2ba579a.jpg', N'AF5D97ED-DC43-4A28-AFB5-0339FF5A38D5', N'2026-05-12 14:37:19.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'14B581BB-A5EF-41B1-A3A7-192964B9A169', N'/uploads/2753cf22-b181-495e-8eee-8bc70a099a82.jpg', N'745786A2-A838-4405-9021-5A40D91FB20D', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'70926CBF-7303-437A-AF6A-1A972FEF5E3A', N'/uploads/ee55e157-b2d6-40fb-810e-cd767ce6e3bd.jpg', N'0D663098-A605-40A0-9D51-4796741A0BBE', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'707B9F1C-24F2-4D81-9E7C-1F3262BAB559', N'/uploads/92732ee3-930d-444a-8cb0-18282a3cfdc5.jpg', N'790398DC-CFFF-4527-8A60-778DDE829F24', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'7E07D1F3-F388-4E71-A6BC-234C6BF1DA80', N'/uploads/5edaff73-56c6-4629-a6b5-90aee78a7f0d.jpg', N'5B05FF22-27DD-4BC0-B5FD-67EF927A12CD', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'027B60CD-2DAA-4312-B853-25B39F480582', N'/uploads/ed6ce303-3c50-4b09-b6c5-aa4734260f46.jpg', N'80D10F06-6296-4972-AAA7-98D7D84C8B28', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'602FF68A-3DA1-4497-BB0C-2B3A7C9E6E13', N'/uploads/3edfab6f-2a9c-439d-8f17-3cbc408b2dee.jpg', N'A1FD3247-BF76-41AF-9BEE-79A36A233A4D', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'1D58AC3A-EDCA-47F6-BAF3-2B63651B3517', N'/uploads/18825557-8101-4057-b0b0-a5d90660b8af.jpg', N'E88D810B-E75D-41CE-B377-6DC03C84BAD6', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'F4624813-9338-4BFF-B480-2F3795DE296B', N'/uploads/b3d9f559-503e-48a0-9fed-e66475c2b03a.jpg', N'2B6238BE-C0E2-4BDC-B0D9-1513D371CB4E', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'D8BD9DB1-B1E6-4DDB-ACFC-371CCEF2F045', N'/uploads/39cf821b-aa00-4220-9ead-3bf5d3435486.jpg', N'DCB124A4-BC30-4C55-B412-D20DAA88BBF4', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'F1DCADB0-F091-494E-BB9B-38DB1B0DD7F8', N'/uploads/a27dbd37-1b90-44ae-91ac-714f1f84ebf0.jpg', N'C3C8C925-3DE4-42F4-84E5-B72522390517', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'72909440-6470-4C70-ADAD-39CABB8BEEDF', N'/uploads/36f2e0bc-a97c-446c-b969-3a00daecef83.jpg', N'02CE4D07-A487-4DC4-85C6-D6B7B3828F5E', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'ADC2AA43-015F-4116-8B3C-454753CDD5F1', N'/uploads/84fe7d02-0278-44ce-ae1c-6b4a9b7d7323.jpg', N'C752CAF3-8B81-4C29-8810-B7D6C9CB1D56', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'A8105463-7A2E-4F3D-A111-484E35AF0252', N'/uploads/8911407c-8de5-437c-9d39-244f79afdc3b.jpg', N'43DC6891-0BB2-4E05-9F50-9B01196EBB75', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'35DF581C-0B11-42D6-A6F3-489E35C1EF51', N'/uploads/1dca4410-7b24-4873-a4f8-81c4f68480c5.jpg', N'DAF6990E-CED3-4F15-B10F-6C1D144C4351', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'E67591AD-FE3B-4846-B2EB-4D72D23E356E', N'/uploads/9c642318-2454-406c-9aba-17ee45b815ea.jpg', N'34227AF8-BB2A-4021-A2B6-5673D7BDED51', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'53EA7394-0E0C-49A0-8683-4EC30ACE5C6C', N'/uploads/da36ba38-9af9-426f-bbaf-48de122c98b2.jpg', N'33B9CE4D-A8ED-4877-BD03-C51D29CFE507', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'6E97C586-586C-4902-95D0-5403A7E18D4C', N'/uploads/f0085a58-0327-4181-89df-624882429590.jpg', N'CE2B5248-C189-4C22-A703-FDF536196121', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'F4676384-C909-46B8-AD70-542211DB2271', N'/uploads/2552524a-6ed7-4cab-b103-26c157f75ad2.jpg', N'B8B1DF4C-0C1D-45DB-8E3C-7DBE98840F63', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'9F3095AB-7C69-4709-ADC1-56D97ABF6832', N'/uploads/4f85a742-354f-47c7-b187-7b783dfb3288.jpg', N'BD06E3C7-F029-4532-BDA8-B1BB499CB308', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'F1C304BF-874C-4435-BB09-6816BD41D413', N'/uploads/97b2e9e7-68e4-4259-bba7-f34f113abdfe.jpg', N'67567C7A-6BF0-41FE-B70B-0D12F82C0509', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'1F831643-9D5D-4D88-A500-69DE6A98FE5D', N'/uploads/7f0bce8d-f721-4e60-b51a-f316ac4b5f54.jpg', N'B50C06DB-9523-4B03-B159-F5BA92B00A00', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'9B513B4D-8E9E-402C-8933-713F1FBAD2E4', N'/uploads/c4fa17f4-f739-423e-9c73-169266dfc3f3.jpg', N'6480062E-F847-4CE8-9857-071930483657', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'C22B71FF-F2C0-431E-89B6-7626D52C6849', N'/uploads/d24e262e-dd28-4de9-8012-f8c237a315f6.jpg', N'E8A80FD3-7B8A-47AA-B1C8-A73C7794C5C7', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'3D716DC6-6C98-46C0-9B4B-7EAD7E8C2974', N'/uploads/d946f962-08ac-40d1-8e2d-34b3455f2bdd.jpg', N'D5C219FC-B92B-4063-8EA1-BEEA0595D195', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'4A1C5B8D-ADAA-468F-970C-80118630687C', N'/uploads/402626dc-32ef-4408-9636-f4f911e471c4.jpg', N'1FCE23E6-D941-434B-8D99-7D6B1576FA4F', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'120279EA-D834-4ABA-9F7B-81B268D5A541', N'/uploads/66a70343-02b7-48eb-8231-dc6e0afe9011.jpg', N'F171E4D7-5448-4F9E-8402-CB850A098A95', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'EDAC00BE-5EB4-470B-927B-838AFACC2F53', N'/uploads/4b4b28d2-4983-40cf-a9b5-ae31ba283f18.jpg', N'4BF8B02C-E91F-406A-BAF1-B77508485EA6', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'79245EB9-9E45-4C99-BD2E-891335320001', N'/uploads/bb074b0d-746f-4313-9aa7-4b636abe25cd.jpg', N'178800EA-7F91-47C8-8611-1D42444BCDEA', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'548B0CC7-E41D-451F-87C2-906378550E4D', N'/uploads/decb0ac3-0434-43f6-b4fe-a43d9b9e5d70.jpg', N'41BD6C06-DCB7-4AAA-A02F-7DC9F65CAAA8', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'C8993AFF-CA7B-4E6B-B2CF-96611CD8F82E', N'/uploads/176d2e72-2798-4008-b556-0eba2e276dad.jpg', N'51D12769-36BB-43A2-8047-CB5EDDE3B803', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'EE15B632-A082-43A5-B968-9D0E6F3E0FED', N'/uploads/a9628c0c-2e7f-4521-b4ed-5e71f5a5f0a5.jpg', N'4BE13FA5-CE61-4F2E-A994-E94D1C71149A', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'E6341F7C-0699-4A19-B6C8-AC4D49F9FFFC', N'/uploads/58c117fc-3804-4eed-8f46-d3b1323ff745.jpg', N'78C45639-A32B-4754-B1DA-AD878646A373', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'D5B89B3D-EB63-433C-90F0-B0CBA75A4E6B', N'/uploads/0139bc4f-1333-4944-bc76-de0effc8911a.jpg', N'519E87F2-F42F-4D1E-B36A-A6C7CFE54361', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'85FA43DB-032C-4AC7-B607-B2A512718B69', N'/uploads/08484bb9-0fd3-4e4c-9d4d-13bfe4d26445.jpg', N'4F176D87-1097-44A2-A0A4-5FD24FD3A4DD', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'1B36414F-65BD-45DF-9E0C-B3F2D7EA7CA7', N'/uploads/0a0d1e3f-44c2-4509-a5fc-22b1e4346450.jpg', N'15BDAE32-8958-45FB-8ECE-F70AA9055438', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'3A34A1D4-CF92-42D1-B132-BC67198EA619', N'/uploads/1da70397-9b05-45f1-898f-7dfce9f67738.jpg', N'979CFEFE-DDD4-412A-82A2-8C1BC96C7C90', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'11F01368-13C1-4071-9243-BD81EF896EF6', N'/uploads/49a0c335-45ef-4bfb-a944-26e5a767651c.jpg', N'CAC92B53-D1CD-401C-A846-B6FC25C8601A', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'9F4DA486-1849-4159-B73B-C03AB18AFBA2', N'/uploads/f842bfc4-5469-4812-bd50-4fe1d8495458.jpeg', N'65436821-7B0E-4054-9FB8-C40D3C096376', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'07D65C76-5907-4079-989A-C76CB0525CAA', N'/uploads/7b8e7ffe-78e3-4bc8-b687-3a4770146d8e.jpg', N'45DCA003-2521-4197-A598-BF7F0C997886', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'736A0F15-E9B1-4D94-98DC-C784561D9BFD', N'/uploads/258b7910-9592-48eb-a374-0b8c700c9aea.jpg', N'C8B01BD5-0F69-4AA9-9172-C5A0424651CB', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'28892A49-AA05-4A80-9A3D-CE2A1353C717', N'/uploads/e2add361-a76c-433b-a66c-402bcfeafb5a.jpg', N'2A46EDF5-519F-4AAA-8808-E67985D57E4C', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'49AAA3A0-D18D-4820-999E-CEAE265DC4C9', N'/uploads/e687f544-5259-4f83-952d-585efd592e02.jpg', N'266C0620-9D99-4A8B-89B7-D2CF9FBE9FC4', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'1C9CD45E-6D6D-4CD5-B9F0-D43E542C9AD4', N'/uploads/97b75dad-06d1-49fb-bb24-4f3978d5fa48.jpg', N'7207148B-E3A3-4D89-A018-EBCCC2F32E46', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'527B08BA-5EB5-41FD-AC2E-D788B3FCD4DA', N'/uploads/58ad8658-34c2-4a83-8fda-d9afdd38b34d.jpg', N'395AA095-98D3-4F1E-8608-BF9F56366A7E', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'8E27E197-36D1-49C7-847F-D79C7F79D678', N'/uploads/3baa96b7-3d63-42f0-98f6-1d71a65be6da.jpg', N'A89CC3AD-749C-4CF6-95D4-CA3A1EA459C8', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'1ACF6520-EE43-4313-9C23-DD389D00E273', N'/uploads/71a717a1-b1d4-44ea-a397-6b598e714bae.jpg', N'14DF777B-D676-4361-82D6-1096B2745112', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'8156FCE9-0661-42FA-AFCF-DD69BF6A3396', N'/uploads/998e68b4-8fa7-4616-9256-1991fc6623c9.jpg', N'C81DABBB-2503-4ECF-9F25-71409501BAC6', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'A24AEDD3-C006-4CE5-BAC2-E1DED15C60BD', N'/uploads/bcfd51a8-c835-4574-be09-d051eaaaaa21.jpg', N'AD25CFAB-B382-48FE-886F-0F76767FF1A9', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'1D4B2D20-EE0D-47DD-A006-E41FB07C6EDB', N'/uploads/e7c4e0d2-3d32-449d-af06-bec01ea83d99.jpg', N'D00D8933-D103-4A3A-84E8-F6775BD09D82', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'0DF386AE-260E-4521-BE55-E582DDE21AAB', N'/uploads/147403ee-8a86-4df5-8c77-415ffd122a10.jpg', N'2DFAD7C1-3A9E-4A41-9170-B37D71F2890B', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'059C28DC-DA1D-4634-8F6B-E75A60253453', N'/uploads/41c5e56b-2aa2-4b6e-9456-42dddfb927a2.jpg', N'0CDE1A66-5578-4D6F-B003-B20E24987AB1', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'80F222F5-A225-414E-97E2-E8F9B94E921A', N'/uploads/9bc67650-9a85-4bf4-a413-ad36f0c772c8.jpg', N'790F8283-1FDF-4CFF-AF40-CDFAEDE69526', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[ProductImages] ([Id], [Url], [ProductId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'FB8E8BF7-B5A6-4493-9751-EA71D253F305', N'/uploads/e6d151fd-3239-458d-a686-9e20a2ba579a.jpg', N'E95E6716-F5A2-4192-A650-9127D32658BA', N'0001-01-01 00:00:00.0000000', NULL, NULL, NULL, N'0')
GO


-- ----------------------------
-- Table structure for Products
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type IN ('U'))
	DROP TABLE [dbo].[Products]
GO

CREATE TABLE [dbo].[Products] (
  [Id] uniqueidentifier  NOT NULL,
  [Name] nvarchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Description] nvarchar(1000) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Price] decimal(18,2)  NOT NULL,
  [CategoryId] uniqueidentifier  NOT NULL,
  [CreatedAt] datetime2(7)  NOT NULL,
  [CreatedBy] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [UpdatedAt] datetime2(7)  NULL,
  [UpdatedBy] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [IsDeleted] bit  NOT NULL
)
GO

ALTER TABLE [dbo].[Products] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Products
-- ----------------------------
INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'AF5D97ED-DC43-4A28-AFB5-0339FF5A38D5', N'Mesh Desk Organizer', N'Tiered metal tray system to keep documents and office tools organized.', N'20.00', N'A3BD55B8-8A7A-4377-904E-0115CC602989', N'2026-05-10 10:53:48.7173792', NULL, N'2026-05-12 08:36:05.6169988', NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'6480062E-F847-4CE8-9857-071930483657', N'Mesh Desk Organizer', N'Tiered metal tray system to keep documents and office tools organized.', N'20.00', N'A3BD55B8-8A7A-4377-904E-0115CC602989', N'2026-05-10 10:53:47.0043987', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'67567C7A-6BF0-41FE-B70B-0D12F82C0509', N'Daily Multivitamins', N'Comprehensive nutrient support for energy, immunity, and overall vitality.', N'30.00', N'0B505B4C-7251-4963-B0EA-B4E9EA9CB581', N'2026-05-10 10:52:02.4206037', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'AD25CFAB-B382-48FE-886F-0F76767FF1A9', N'LEGO Star Wars X-Wing', N'Detailed brick-built model of the iconic starfighter with pilot minifigures.', N'80.00', N'757B0960-3E67-4021-8608-8B1074C8A37A', N'2026-05-10 10:49:02.8326434', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'14DF777B-D676-4361-82D6-1096B2745112', N'Ergonomic Office Chair', N'Adjustable height and lumbar support for maximum productivity and comfort.', N'199.00', N'098E0EF8-1580-4351-B97D-EA269B4A5C4C', N'2026-05-10 10:43:16.0572761', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'2B6238BE-C0E2-4BDC-B0D9-1513D371CB4E', N'Logitech MX Master 3S', N'Ergonomic wireless mouse designed for precision, speed, and silent clicking.', N'99.00', N'7AA9B9D5-B5D6-4246-9438-7CD06BFE5618', N'2026-05-10 10:01:07.5662759', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'178800EA-7F91-47C8-8611-1D42444BCDEA', N'Ergonomic Office Chair', N'Adjustable height and lumbar support for maximum productivity and comfort.', N'199.00', N'098E0EF8-1580-4351-B97D-EA269B4A5C4C', N'2026-05-10 10:43:01.0343298', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'906827D2-341F-4321-B472-1EA5D60213A2', N'LEGO Star Wars X-Wing', N'Detailed brick-built model of the iconic starfighter with pilot minifigures.', N'80.00', N'757B0960-3E67-4021-8608-8B1074C8A37A', N'2026-05-10 10:49:05.2271779', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'0D663098-A605-40A0-9D51-4796741A0BBE', N'Waterproof Camping Tent', N'Spacious 4-person tent with seam-taped rainfly for all-weather protection.', N'120.00', N'FCE2A9BD-B61D-4967-AA10-25A58209DACB', N'2026-05-10 10:45:03.0982749', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'0B0F7F4F-D81D-49EE-B44F-560323A4392D', N'"Atomic Habits" Book', N'A proven framework for improving every day through tiny, consistent changes.', N'12.00', N'7EB4A026-171E-4D30-89D4-A6F82A1D052A', N'2026-05-10 10:47:30.2325045', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'34227AF8-BB2A-4021-A2B6-5673D7BDED51', N'Waterproof Camping Tent', N'Spacious 4-person tent with seam-taped rainfly for all-weather protection.', N'120.00', N'FCE2A9BD-B61D-4967-AA10-25A58209DACB', N'2026-05-10 10:44:52.9739391', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'745786A2-A838-4405-9021-5A40D91FB20D', N'Ergonomic Office Chair', N'Adjustable height and lumbar support for maximum productivity and comfort.', N'199.00', N'098E0EF8-1580-4351-B97D-EA269B4A5C4C', N'2026-05-10 10:43:00.4422168', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'4F176D87-1097-44A2-A0A4-5FD24FD3A4DD', N'LEGO Star Wars X-Wing', N'Detailed brick-built model of the iconic starfighter with pilot minifigures.', N'80.00', N'757B0960-3E67-4021-8608-8B1074C8A37A', N'2026-05-10 10:49:04.7842896', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'5B05FF22-27DD-4BC0-B5FD-67EF927A12CD', N'Daily Multivitamins', N'Comprehensive nutrient support for energy, immunity, and overall vitality.', N'30.00', N'0B505B4C-7251-4963-B0EA-B4E9EA9CB581', N'2026-05-10 10:52:06.2603941', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'DAF6990E-CED3-4F15-B10F-6C1D144C4351', N'Leather Bound Journal', N'Elegant notebook with thick, acid-free paper and a secure elastic closure.', N'12.00', N'A3BD55B8-8A7A-4377-904E-0115CC602989', N'2026-05-10 10:53:07.7536353', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'E88D810B-E75D-41CE-B377-6DC03C84BAD6', N'"Atomic Habits" Book', N'A proven framework for improving every day through tiny, consistent changes.', N'12.00', N'7EB4A026-171E-4D30-89D4-A6F82A1D052A', N'2026-05-10 10:47:31.3675249', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'C81DABBB-2503-4ECF-9F25-71409501BAC6', N'Sony WH-1000XM5', N'Industry-leading noise-canceling headphones with exceptional sound clarity.', N'348.00', N'7AA9B9D5-B5D6-4246-9438-7CD06BFE5618', N'2026-05-10 09:58:18.3944196', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'790398DC-CFFF-4527-8A60-778DDE829F24', N'Leather Bound Journal', N'Elegant notebook with thick, acid-free paper and a secure elastic closure.', N'12.00', N'A3BD55B8-8A7A-4377-904E-0115CC602989', N'2026-05-10 10:53:08.6281574', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'A1FD3247-BF76-41AF-9BEE-79A36A233A4D', N'Samsung Galaxy Watch 6', N'Advanced smartwatch with comprehensive health tracking and sleep coaching.', N'299.00', N'7AA9B9D5-B5D6-4246-9438-7CD06BFE5618', N'2026-05-10 09:59:09.4171468', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'1FCE23E6-D941-434B-8D99-7D6B1576FA4F', N'Daily Multivitamins', N'Comprehensive nutrient support for energy, immunity, and overall vitality.', N'30.00', N'0B505B4C-7251-4963-B0EA-B4E9EA9CB581', N'2026-05-10 10:52:06.7026796', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'B8B1DF4C-0C1D-45DB-8E3C-7DBE98840F63', N'Waterproof Camping Tent', N'Spacious 4-person tent with seam-taped rainfly for all-weather protection.', N'120.00', N'FCE2A9BD-B61D-4967-AA10-25A58209DACB', N'2026-05-10 10:44:51.8698778', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'41BD6C06-DCB7-4AAA-A02F-7DC9F65CAAA8', N'"Atomic Habits" Book', N'A proven framework for improving every day through tiny, consistent changes.', N'12.00', N'7EB4A026-171E-4D30-89D4-A6F82A1D052A', N'2026-05-10 10:47:30.8001824', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'979CFEFE-DDD4-412A-82A2-8C1BC96C7C90', N'Daily Multivitamins', N'Comprehensive nutrient support for energy, immunity, and overall vitality.', N'30.00', N'0B505B4C-7251-4963-B0EA-B4E9EA9CB581', N'2026-05-10 10:52:05.6903973', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'E95E6716-F5A2-4192-A650-9127D32658BA', N'"Atomic Habits" Book', N'A proven framework for improving every day through tiny, consistent changes.', N'12.00', N'7EB4A026-171E-4D30-89D4-A6F82A1D052A', N'2026-05-10 10:47:29.1078009', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'80D10F06-6296-4972-AAA7-98D7D84C8B28', N'Waterproof Camping Tent', N'Spacious 4-person tent with seam-taped rainfly for all-weather protection.', N'120.00', N'FCE2A9BD-B61D-4967-AA10-25A58209DACB', N'2026-05-10 10:44:51.3508121', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'825F232F-EC82-4DA1-A9B4-99510729274E', N'LEGO Star Wars X-Wing', N'Detailed brick-built model of the iconic starfighter with pilot minifigures.', N'80.00', N'757B0960-3E67-4021-8608-8B1074C8A37A', N'2026-05-10 10:49:01.9056427', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'43DC6891-0BB2-4E05-9F50-9B01196EBB75', N'Mesh Desk Organizer', N'Tiered metal tray system to keep documents and office tools organized.', N'20.00', N'A3BD55B8-8A7A-4377-904E-0115CC602989', N'2026-05-10 10:53:43.8664011', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'519E87F2-F42F-4D1E-B36A-A6C7CFE54361', N'Daily Multivitamins', N'Comprehensive nutrient support for energy, immunity, and overall vitality.', N'30.00', N'0B505B4C-7251-4963-B0EA-B4E9EA9CB581', N'2026-05-10 10:52:07.3081181', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'E8A80FD3-7B8A-47AA-B1C8-A73C7794C5C7', N'New', N'OpenTikDemo', N'2.00', N'3FC2ACF7-BE10-438E-AFBA-DF7BF3712BD4', N'2026-05-13 11:17:07.3003960', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'EFA84F27-B2FC-4949-9503-A793F0E145DC', N'Daily Multivitamins', N'Comprehensive nutrient support for energy, immunity, and overall vitality.', N'30.00', N'0B505B4C-7251-4963-B0EA-B4E9EA9CB581', N'2026-05-10 10:52:07.1503117', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'E6E6E69F-2D09-45A1-B194-AD2ADDEB5BD3', N'LEGO Star Wars X-Wing', N'Detailed brick-built model of the iconic starfighter with pilot minifigures.', N'80.00', N'757B0960-3E67-4021-8608-8B1074C8A37A', N'2026-05-10 10:49:04.0507700', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'78C45639-A32B-4754-B1DA-AD878646A373', N'Daily Multivitamins', N'Comprehensive nutrient support for energy, immunity, and overall vitality.', N'30.00', N'0B505B4C-7251-4963-B0EA-B4E9EA9CB581', N'2026-05-10 10:52:05.1605297', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'BD06E3C7-F029-4532-BDA8-B1BB499CB308', N'Leather Bound Journal', N'Elegant notebook with thick, acid-free paper and a secure elastic closure.', N'12.00', N'A3BD55B8-8A7A-4377-904E-0115CC602989', N'2026-05-10 10:53:08.2676128', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'0CDE1A66-5578-4D6F-B003-B20E24987AB1', N'Mesh Desk Organizer', N'Tiered metal tray system to keep documents and office tools organized.', N'20.00', N'A3BD55B8-8A7A-4377-904E-0115CC602989', N'2026-05-10 10:53:44.4626204', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'2DFAD7C1-3A9E-4A41-9170-B37D71F2890B', N'Slim Fit Denim Jeans', N'Durable and stylish indigo denim with a hint of stretch for mobility.', N'59.00', N'6915C36A-C516-4BD8-AEA1-BD4B1ADBE948', N'2026-05-10 10:41:40.7131999', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'CAC92B53-D1CD-401C-A846-B6FC25C8601A', N'Mesh Desk Organizer', N'Tiered metal tray system to keep documents and office tools organized.', N'20.00', N'A3BD55B8-8A7A-4377-904E-0115CC602989', N'2026-05-10 10:53:44.6417805', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'C3C8C925-3DE4-42F4-84E5-B72522390517', N'Ergonomic Office Chair', N'Adjustable height and lumbar support for maximum productivity and comfort.', N'199.00', N'098E0EF8-1580-4351-B97D-EA269B4A5C4C', N'2026-05-10 10:42:54.4008814', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'4BF8B02C-E91F-406A-BAF1-B77508485EA6', N'Leather Bound Journal', N'Elegant notebook with thick, acid-free paper and a secure elastic closure.', N'12.00', N'A3BD55B8-8A7A-4377-904E-0115CC602989', N'2026-05-10 10:53:07.3566523', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'C752CAF3-8B81-4C29-8810-B7D6C9CB1D56', N'Classic White T-Shirt', N'Premium 100% organic cotton tee with a comfortable, breathable fit.', N'25.00', N'6915C36A-C516-4BD8-AEA1-BD4B1ADBE948', N'2026-05-10 10:37:10.7168141', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'D5C219FC-B92B-4063-8EA1-BEEA0595D195', N'"Atomic Habits" Book', N'A proven framework for improving every day through tiny, consistent changes.', N'12.00', N'7EB4A026-171E-4D30-89D4-A6F82A1D052A', N'2026-05-10 10:47:29.6627733', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'45DCA003-2521-4197-A598-BF7F0C997886', N'Leather Bound Journal', N'Elegant notebook with thick, acid-free paper and a secure elastic closure.', N'12.00', N'A3BD55B8-8A7A-4377-904E-0115CC602989', N'2026-05-10 10:53:08.0130892', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'395AA095-98D3-4F1E-8608-BF9F56366A7E', N'Mesh Desk Organizer', N'Tiered metal tray system to keep documents and office tools organized.', N'20.00', N'A3BD55B8-8A7A-4377-904E-0115CC602989', N'2026-05-10 10:53:44.7968118', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'65436821-7B0E-4054-9FB8-C40D3C096376', N'iPhone 15 Pro Max', N'Apple''s flagship smartphone with a titanium design and A17 Pro chip.', N'1199.00', N'7AA9B9D5-B5D6-4246-9438-7CD06BFE5618', N'2026-05-10 09:47:49.6311209', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'F6EE9DCB-6458-49BE-A636-C42230F9E9B8', N'Daily Multivitamins', N'Comprehensive nutrient support for energy, immunity, and overall vitality.', N'30.00', N'0B505B4C-7251-4963-B0EA-B4E9EA9CB581', N'2026-05-10 10:52:04.1278534', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'33B9CE4D-A8ED-4877-BD03-C51D29CFE507', N'Slim Fit Denim Jeans', N'Durable and stylish indigo denim with a hint of stretch for mobility.', N'59.00', N'6915C36A-C516-4BD8-AEA1-BD4B1ADBE948', N'2026-05-10 10:41:39.6596370', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'C8B01BD5-0F69-4AA9-9172-C5A0424651CB', N'Slim Fit Denim Jeans', N'Durable and stylish indigo denim with a hint of stretch for mobility.', N'59.00', N'6915C36A-C516-4BD8-AEA1-BD4B1ADBE948', N'2026-05-10 10:40:47.3800788', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'A89CC3AD-749C-4CF6-95D4-CA3A1EA459C8', N'Slim Fit Denim Jeans', N'Durable and stylish indigo denim with a hint of stretch for mobility.', N'59.00', N'6915C36A-C516-4BD8-AEA1-BD4B1ADBE948', N'2026-05-10 10:41:41.5066735', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'51D12769-36BB-43A2-8047-CB5EDDE3B803', N'Daily Multivitamins', N'Comprehensive nutrient support for energy, immunity, and overall vitality.', N'30.00', N'0B505B4C-7251-4963-B0EA-B4E9EA9CB581', N'2026-05-10 10:52:06.5581861', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'F171E4D7-5448-4F9E-8402-CB850A098A95', N'MacBook Air M3', N'Ultra-slim laptop featuring the powerful M3 chip for seamless multitasking.', N'2099.00', N'7AA9B9D5-B5D6-4246-9438-7CD06BFE5618', N'2026-05-10 09:54:56.5029868', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'790F8283-1FDF-4CFF-AF40-CDFAEDE69526', N'Daily Multivitamins', N'Comprehensive nutrient support for energy, immunity, and overall vitality.', N'30.00', N'0B505B4C-7251-4963-B0EA-B4E9EA9CB581', N'2026-05-10 10:52:06.9906785', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'DCB124A4-BC30-4C55-B412-D20DAA88BBF4', N'Ergonomic Office Chair', N'Adjustable height and lumbar support for maximum productivity and comfort.', N'199.00', N'098E0EF8-1580-4351-B97D-EA269B4A5C4C', N'2026-05-10 10:43:01.6146411', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'266C0620-9D99-4A8B-89B7-D2CF9FBE9FC4', N'Leather Bound Journal', N'Elegant notebook with thick, acid-free paper and a secure elastic closure.', N'12.00', N'A3BD55B8-8A7A-4377-904E-0115CC602989', N'2026-05-10 10:53:07.5669993', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'02CE4D07-A487-4DC4-85C6-D6B7B3828F5E', N'Daily Multivitamins', N'Comprehensive nutrient support for energy, immunity, and overall vitality.', N'30.00', N'0B505B4C-7251-4963-B0EA-B4E9EA9CB581', N'2026-05-10 10:52:06.8242085', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'2A46EDF5-519F-4AAA-8808-E67985D57E4C', N'Ergonomic Office Chair', N'Adjustable height and lumbar support for maximum productivity and comfort.', N'199.00', N'098E0EF8-1580-4351-B97D-EA269B4A5C4C', N'2026-05-10 10:42:59.8591988', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'4BE13FA5-CE61-4F2E-A994-E94D1C71149A', N'Waterproof Camping Tent', N'Spacious 4-person tent with seam-taped rainfly for all-weather protection.', N'120.00', N'FCE2A9BD-B61D-4967-AA10-25A58209DACB', N'2026-05-10 10:44:52.4940737', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'7207148B-E3A3-4D89-A018-EBCCC2F32E46', N'Waterproof Camping Tent', N'Spacious 4-person tent with seam-taped rainfly for all-weather protection.', N'120.00', N'FCE2A9BD-B61D-4967-AA10-25A58209DACB', N'2026-05-10 10:44:50.3365725', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'B50C06DB-9523-4B03-B159-F5BA92B00A00', N'Mesh Desk Organizer', N'Tiered metal tray system to keep documents and office tools organized.', N'20.00', N'A3BD55B8-8A7A-4377-904E-0115CC602989', N'2026-05-10 10:53:46.5561322', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'D00D8933-D103-4A3A-84E8-F6775BD09D82', N'Daily Multivitamins', N'Comprehensive nutrient support for energy, immunity, and overall vitality.', N'30.00', N'0B505B4C-7251-4963-B0EA-B4E9EA9CB581', N'2026-05-10 10:52:04.6030074', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'15BDAE32-8958-45FB-8ECE-F70AA9055438', N'Leather Bound Journal', N'Elegant notebook with thick, acid-free paper and a secure elastic closure.', N'12.00', N'A3BD55B8-8A7A-4377-904E-0115CC602989', N'2026-05-10 10:53:07.1136894', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (N'CE2B5248-C189-4C22-A703-FDF536196121', N'LEGO Star Wars X-Wing', N'Detailed brick-built model of the iconic starfighter with pilot minifigures.', N'80.00', N'757B0960-3E67-4021-8608-8B1074C8A37A', N'2026-05-10 10:49:03.3839501', NULL, NULL, NULL, N'0')
GO


-- ----------------------------
-- Primary Key structure for table __EFMigrationsHistory
-- ----------------------------
ALTER TABLE [dbo].[__EFMigrationsHistory] ADD CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table AdminUsers
-- ----------------------------
ALTER TABLE [dbo].[AdminUsers] ADD CONSTRAINT [PK_AdminUsers] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Categories
-- ----------------------------
ALTER TABLE [dbo].[Categories] ADD CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Customers
-- ----------------------------
CREATE UNIQUE NONCLUSTERED INDEX [IX_Customers_Email]
ON [dbo].[Customers] (
  [Email] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Customers
-- ----------------------------
ALTER TABLE [dbo].[Customers] ADD CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table ProductImages
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_ProductImages_ProductId]
ON [dbo].[ProductImages] (
  [ProductId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table ProductImages
-- ----------------------------
ALTER TABLE [dbo].[ProductImages] ADD CONSTRAINT [PK_ProductImages] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Products
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Products_CategoryId]
ON [dbo].[Products] (
  [CategoryId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Products
-- ----------------------------
ALTER TABLE [dbo].[Products] ADD CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table ProductImages
-- ----------------------------
ALTER TABLE [dbo].[ProductImages] ADD CONSTRAINT [FK_ProductImages_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Products
-- ----------------------------
ALTER TABLE [dbo].[Products] ADD CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

