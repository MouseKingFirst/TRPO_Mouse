USE [master]
GO
/****** Object:  Database [library]    Script Date: 23.10.2021 13:18:21 ******/
CREATE DATABASE [library]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'library', FILENAME = N'D:\Programming\DataBase\msSqlExpress\MSSQL14.SQLEXPRESS\MSSQL\DATA\library.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'library_log', FILENAME = N'D:\Programming\DataBase\msSqlExpress\MSSQL14.SQLEXPRESS\MSSQL\DATA\library_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [library] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [library].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [library] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [library] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [library] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [library] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [library] SET ARITHABORT OFF 
GO
ALTER DATABASE [library] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [library] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [library] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [library] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [library] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [library] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [library] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [library] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [library] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [library] SET  DISABLE_BROKER 
GO
ALTER DATABASE [library] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [library] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [library] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [library] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [library] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [library] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [library] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [library] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [library] SET  MULTI_USER 
GO
ALTER DATABASE [library] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [library] SET DB_CHAINING OFF 
GO
ALTER DATABASE [library] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [library] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [library] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [library] SET QUERY_STORE = OFF
GO
USE [library]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 23.10.2021 13:18:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[library_authors]    Script Date: 23.10.2021 13:18:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[library_authors](
	[author_code] [int] IDENTITY(1,1) NOT NULL,
	[author_FullName] [varchar](128) NOT NULL,
	[photo] [varchar](255) NULL,
 CONSTRAINT [PK_library_authors] PRIMARY KEY CLUSTERED 
(
	[author_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[library_book_example]    Script Date: 23.10.2021 13:18:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[library_book_example](
	[inventory_number] [int] IDENTITY(1,1) NOT NULL,
	[book_id] [int] NOT NULL,
	[user_id] [int] NULL,
	[present] [bit] NOT NULL,
	[date_in] [date] NULL,
	[date_return] [date] NULL,
 CONSTRAINT [PK_library_book_example] PRIMARY KEY CLUSTERED 
(
	[inventory_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[library_books]    Script Date: 23.10.2021 13:18:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[library_books](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ISBN] [varchar](50) NOT NULL,
	[title] [varchar](128) NOT NULL,
	[author] [int] NOT NULL,
	[genre] [varchar](30) NOT NULL,
	[publisher] [varchar](80) NOT NULL,
	[publication_place] [varchar](50) NOT NULL,
	[publication_year] [date] NOT NULL,
	[pages] [int] NOT NULL,
	[price] [money] NOT NULL,
 CONSTRAINT [PK_library_books] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[library_books_relation]    Script Date: 23.10.2021 13:18:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[library_books_relation](
	[ISBN] [varchar](50) NOT NULL,
	[inv_number] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[library_sessions]    Script Date: 23.10.2021 13:18:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[library_sessions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[AccessToken] [nvarchar](500) NOT NULL,
	[RefreshToken] [nvarchar](200) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[ExpiresIn] [datetime] NOT NULL,
 CONSTRAINT [PK_library_sessions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[library_users]    Script Date: 23.10.2021 13:18:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[library_users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[login] [varchar](32) NOT NULL,
	[password] [varchar](128) NOT NULL,
	[email] [varchar](254) NOT NULL,
	[role] [varchar](32) NOT NULL,
	[photo] [varchar](255) NULL,
 CONSTRAINT [PK_library_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[library_users_data]    Script Date: 23.10.2021 13:18:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[library_users_data](
	[user_id] [int] NOT NULL,
	[last_name] [varchar](50) NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[middle_name] [varchar](50) NULL,
	[birth_date] [date] NOT NULL,
	[phone_number] [varchar](20) NOT NULL,
	[city] [varchar](50) NOT NULL,
	[street] [varchar](50) NOT NULL,
	[house_number] [int] NOT NULL,
	[apartment_number] [int] NULL,
	[passport_series] [varchar](4) NOT NULL,
	[passport_number] [varchar](6) NOT NULL,
 CONSTRAINT [PK_library_users_data] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[library_authors] ON 

INSERT [dbo].[library_authors] ([author_code], [author_FullName], [photo]) VALUES (1, N'Николай Васильевич Гоголь', N'gogol.jpg')
INSERT [dbo].[library_authors] ([author_code], [author_FullName], [photo]) VALUES (2, N'Булгаков Михаил Афанасьевич', N'bulgakov.jpg')
INSERT [dbo].[library_authors] ([author_code], [author_FullName], [photo]) VALUES (3, N'Достоевский Фёдор Михайлович', NULL)
INSERT [dbo].[library_authors] ([author_code], [author_FullName], [photo]) VALUES (5, N'Маяковский Владимир Владимирович', NULL)
INSERT [dbo].[library_authors] ([author_code], [author_FullName], [photo]) VALUES (6, N'Тургенев Иван Сергеевич', N'turgenev.png')
SET IDENTITY_INSERT [dbo].[library_authors] OFF
GO
SET IDENTITY_INSERT [dbo].[library_book_example] ON 

INSERT [dbo].[library_book_example] ([inventory_number], [book_id], [user_id], [present], [date_in], [date_return]) VALUES (2, 2, 1, 0, CAST(N'2021-02-28' AS Date), CAST(N'2021-03-28' AS Date))
INSERT [dbo].[library_book_example] ([inventory_number], [book_id], [user_id], [present], [date_in], [date_return]) VALUES (3, 2, NULL, 1, NULL, NULL)
INSERT [dbo].[library_book_example] ([inventory_number], [book_id], [user_id], [present], [date_in], [date_return]) VALUES (4, 11, NULL, 1, NULL, NULL)
INSERT [dbo].[library_book_example] ([inventory_number], [book_id], [user_id], [present], [date_in], [date_return]) VALUES (5, 11, 3, 0, CAST(N'2021-01-06' AS Date), NULL)
INSERT [dbo].[library_book_example] ([inventory_number], [book_id], [user_id], [present], [date_in], [date_return]) VALUES (6, 3, NULL, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[library_book_example] OFF
GO
SET IDENTITY_INSERT [dbo].[library_books] ON 

INSERT [dbo].[library_books] ([id], [ISBN], [title], [author], [genre], [publisher], [publication_place], [publication_year], [pages], [price]) VALUES (2, N'978-5-699-80790-1', N'Мёртвые души', 1, N'Классическая проза', N'Эксмо', N'Москва', CAST(N'2017-09-28' AS Date), 768, 532.0000)
INSERT [dbo].[library_books] ([id], [ISBN], [title], [author], [genre], [publisher], [publication_place], [publication_year], [pages], [price]) VALUES (3, N'978-966-22-7000-6', N'Вий', 2, N'Повесть', N'Микко', N'Москва', CAST(N'2009-01-01' AS Date), 128, 925.0000)
INSERT [dbo].[library_books] ([id], [ISBN], [title], [author], [genre], [publisher], [publication_place], [publication_year], [pages], [price]) VALUES (8, N'978-5-699-92529-2', N'Мастер и Маргарита', 2, N'Роман', N'Эксмо', N'Москва', CAST(N'2016-01-01' AS Date), 480, 329.0000)
INSERT [dbo].[library_books] ([id], [ISBN], [title], [author], [genre], [publisher], [publication_place], [publication_year], [pages], [price]) VALUES (10, N'978-5-699-54689-3', N'Облако в штанах', 5, N'Поэма', N'Эксмо', N'Москва', CAST(N'2012-01-01' AS Date), 320, 500.0000)
INSERT [dbo].[library_books] ([id], [ISBN], [title], [author], [genre], [publisher], [publication_place], [publication_year], [pages], [price]) VALUES (11, N'978-5-699-93016-6', N'Преступление и наказание', 3, N'Роман', N'Эксмо', N'Москва', CAST(N'2017-01-01' AS Date), 592, 1200.0000)
SET IDENTITY_INSERT [dbo].[library_books] OFF
GO
INSERT [dbo].[library_books_relation] ([ISBN], [inv_number]) VALUES (N'978-5-699-80790-1', 2)
INSERT [dbo].[library_books_relation] ([ISBN], [inv_number]) VALUES (N'978-5-699-93016-6', 4)
INSERT [dbo].[library_books_relation] ([ISBN], [inv_number]) VALUES (N'978-5-699-93016-6', 5)
INSERT [dbo].[library_books_relation] ([ISBN], [inv_number]) VALUES (N'978-966-22-7000-6', 6)
INSERT [dbo].[library_books_relation] ([ISBN], [inv_number]) VALUES (N'978-5-699-80790-1', 3)
GO
SET IDENTITY_INSERT [dbo].[library_sessions] ON 

INSERT [dbo].[library_sessions] ([id], [userId], [AccessToken], [RefreshToken], [CreateAt], [ExpiresIn]) VALUES (23, 1, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsIm5iZiI6MTYxODE2NTgxNywiZXhwIjoxNjE4MTY1OTM3LCJpc3MiOiJMaWJyYXJ5IiwiYXVkIjoiTGlicmFyeUNsaWVudCJ9.OamSEv1DWdxLT5tcqUx8nbu9pNTM_Rxy44LvEcSE-n0', N'LZKQebmwi1ftz4dikFMjxEP3NkKGk4+V8tzegofMWXI=', CAST(N'2021-04-11T18:30:17.510' AS DateTime), CAST(N'2021-04-13T00:30:17.510' AS DateTime))
SET IDENTITY_INSERT [dbo].[library_sessions] OFF
GO
SET IDENTITY_INSERT [dbo].[library_users] ON 

INSERT [dbo].[library_users] ([id], [login], [password], [email], [role], [photo]) VALUES (1, N'admin', N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', N'admin@example.ru', N'Admin', N'2.png')
INSERT [dbo].[library_users] ([id], [login], [password], [email], [role], [photo]) VALUES (2, N'sushiwarrior', N'64022612e6bd7ea5a51d6ea0fc379f1c88afe71f9bdc6708aec36b0f1785b3e0', N'redakter@gmail.com', N'Reader', N'1.png')
INSERT [dbo].[library_users] ([id], [login], [password], [email], [role], [photo]) VALUES (3, N'heaven', N'818efe46a6c2ddb95df1bdecd7869fbd6ac790f24182f681cc6b7edf43ec7b69', N'heaventv@gmail.com', N'Reader', N'3.png')
INSERT [dbo].[library_users] ([id], [login], [password], [email], [role], [photo]) VALUES (5, N'mrworldvide', N'bfabee4d51e68169bd485c0b2555eb7a674c759a4ab787de21902817623b32f1', N'NoobMaster69@Thor.ez', N'Reader', NULL)
INSERT [dbo].[library_users] ([id], [login], [password], [email], [role], [photo]) VALUES (6, N'sherlock', N'4bfbdf7ebee257ff0658b04db68feca37c7b68ef3f419b437efef43d3f5d84b6', N'theworldking99@gmail.com', N'Librarian', NULL)
INSERT [dbo].[library_users] ([id], [login], [password], [email], [role], [photo]) VALUES (7, N'admin1', N'25f43b1486ad95a1398e3eeb3d83bc4010015fcc9bedb35b432e00298d5021f7', N'test@local.host', N'Reader', N'4.png')
INSERT [dbo].[library_users] ([id], [login], [password], [email], [role], [photo]) VALUES (12, N'admin12', N'114663ab194edcb3f61d409883ce4ae6c3c2f9854194095a5385011d15becbef', N'test@local.host', N'Reader', NULL)
INSERT [dbo].[library_users] ([id], [login], [password], [email], [role], [photo]) VALUES (13, N'admin13', N'b5045cc120e1fcd12b46ed02b6872fe77d145983ab5ad1e752d73143203d4ce4', N'test@local.host', N'Reader', NULL)
INSERT [dbo].[library_users] ([id], [login], [password], [email], [role], [photo]) VALUES (14, N'Admin16', N'25f43b1486ad95a1398e3eeb3d83bc4010015fcc9bedb35b432e00298d5021f7', N'test@local.host', N'Reader', NULL)
INSERT [dbo].[library_users] ([id], [login], [password], [email], [role], [photo]) VALUES (15, N'SimpleLogin', N'2308f81e73d9f89b94031f9f90e162c8847f389b5f5a067b7ae6d5324eca27f8', N'admin@local.host', N'Librarian', NULL)
SET IDENTITY_INSERT [dbo].[library_users] OFF
GO
INSERT [dbo].[library_users_data] ([user_id], [last_name], [first_name], [middle_name], [birth_date], [phone_number], [city], [street], [house_number], [apartment_number], [passport_series], [passport_number]) VALUES (1, N'Петров', N'Антон', N'Иванович', CAST(N'1994-03-10' AS Date), N'88005553535', N'Москва', N'Ленина', 2, 45, N'1234', N'123456')
INSERT [dbo].[library_users_data] ([user_id], [last_name], [first_name], [middle_name], [birth_date], [phone_number], [city], [street], [house_number], [apartment_number], [passport_series], [passport_number]) VALUES (2, N'Иванов', N'Иван', N'Иванович', CAST(N'1980-09-01' AS Date), N'8800553645', N'Москва', N'Ленина', 8, 123, N'2222', N'222222')
INSERT [dbo].[library_users_data] ([user_id], [last_name], [first_name], [middle_name], [birth_date], [phone_number], [city], [street], [house_number], [apartment_number], [passport_series], [passport_number]) VALUES (3, N'Борисов', N'Иван', N'Петрович', CAST(N'1989-06-05' AS Date), N'88005536566', N'Москва', N'Алексея Дикого', 4, 78, N'3333', N'333333')
INSERT [dbo].[library_users_data] ([user_id], [last_name], [first_name], [middle_name], [birth_date], [phone_number], [city], [street], [house_number], [apartment_number], [passport_series], [passport_number]) VALUES (5, N'Смирнов', N'Сергей', N'Андреевич', CAST(N'2000-07-26' AS Date), N'89638792134', N'Москва', N'50 лет Октября', 1, 32, N'4545', N'454554')
INSERT [dbo].[library_users_data] ([user_id], [last_name], [first_name], [middle_name], [birth_date], [phone_number], [city], [street], [house_number], [apartment_number], [passport_series], [passport_number]) VALUES (6, N'Попов', N'Дмитрий', N'Владимирович', CAST(N'1986-03-06' AS Date), N'89763214566', N'Москва', N'Лавров переулок', 5, 23, N'6546', N'342432')
INSERT [dbo].[library_users_data] ([user_id], [last_name], [first_name], [middle_name], [birth_date], [phone_number], [city], [street], [house_number], [apartment_number], [passport_series], [passport_number]) VALUES (7, N'Иванов', N'Иван', N'Иванович', CAST(N'2021-09-22' AS Date), N'12345678910', N'None', N'None', 0, 0, N'1234', N'567890')
INSERT [dbo].[library_users_data] ([user_id], [last_name], [first_name], [middle_name], [birth_date], [phone_number], [city], [street], [house_number], [apartment_number], [passport_series], [passport_number]) VALUES (12, N'admin1', N'admin1', N'', CAST(N'2021-09-22' AS Date), N'12345678910', N'None', N'None', 0, 0, N'1234', N'567890')
INSERT [dbo].[library_users_data] ([user_id], [last_name], [first_name], [middle_name], [birth_date], [phone_number], [city], [street], [house_number], [apartment_number], [passport_series], [passport_number]) VALUES (13, N'adm', N'adm', N'', CAST(N'2021-09-23' AS Date), N'12345678910', N'None', N'None', 0, 0, N'1234', N'567890')
INSERT [dbo].[library_users_data] ([user_id], [last_name], [first_name], [middle_name], [birth_date], [phone_number], [city], [street], [house_number], [apartment_number], [passport_series], [passport_number]) VALUES (14, N'Иванов', N'Иван', N'Иванович', CAST(N'2021-09-23' AS Date), N'12345678910', N'None', N'None', 0, 0, N'1234', N'567890')
INSERT [dbo].[library_users_data] ([user_id], [last_name], [first_name], [middle_name], [birth_date], [phone_number], [city], [street], [house_number], [apartment_number], [passport_series], [passport_number]) VALUES (15, N'Петров', N'Пётр', N'', CAST(N'2021-09-23' AS Date), N'12345678910', N'None', N'None', 0, 0, N'1234', N'567890')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_library_books]    Script Date: 23.10.2021 13:18:21 ******/
ALTER TABLE [dbo].[library_books] ADD  CONSTRAINT [IX_library_books] UNIQUE NONCLUSTERED 
(
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_library_users]    Script Date: 23.10.2021 13:18:21 ******/
CREATE NONCLUSTERED INDEX [IX_library_users] ON [dbo].[library_users]
(
	[login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[library_book_example]  WITH CHECK ADD  CONSTRAINT [FK_library_book_example_library_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[library_users] ([id])
GO
ALTER TABLE [dbo].[library_book_example] CHECK CONSTRAINT [FK_library_book_example_library_users]
GO
ALTER TABLE [dbo].[library_books]  WITH CHECK ADD  CONSTRAINT [FK_library_books_library_authors1] FOREIGN KEY([author])
REFERENCES [dbo].[library_authors] ([author_code])
GO
ALTER TABLE [dbo].[library_books] CHECK CONSTRAINT [FK_library_books_library_authors1]
GO
ALTER TABLE [dbo].[library_books_relation]  WITH CHECK ADD  CONSTRAINT [FK_library_books_relation_library_book_example] FOREIGN KEY([inv_number])
REFERENCES [dbo].[library_book_example] ([inventory_number])
GO
ALTER TABLE [dbo].[library_books_relation] CHECK CONSTRAINT [FK_library_books_relation_library_book_example]
GO
ALTER TABLE [dbo].[library_books_relation]  WITH CHECK ADD  CONSTRAINT [FK_library_books_relation_library_books] FOREIGN KEY([ISBN])
REFERENCES [dbo].[library_books] ([ISBN])
GO
ALTER TABLE [dbo].[library_books_relation] CHECK CONSTRAINT [FK_library_books_relation_library_books]
GO
ALTER TABLE [dbo].[library_sessions]  WITH CHECK ADD  CONSTRAINT [FK_library_sessions_library_users] FOREIGN KEY([userId])
REFERENCES [dbo].[library_users] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[library_sessions] CHECK CONSTRAINT [FK_library_sessions_library_users]
GO
ALTER TABLE [dbo].[library_users_data]  WITH CHECK ADD  CONSTRAINT [FK_library_users_data_library_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[library_users] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[library_users_data] CHECK CONSTRAINT [FK_library_users_data_library_users]
GO
USE [master]
GO
ALTER DATABASE [library] SET  READ_WRITE 
GO
