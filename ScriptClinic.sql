USE [Clinic]
GO
/****** Object:  Table [dbo].[Doctors]    Script Date: 11.06.2025 16:51:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Patronymic] [nvarchar](100) NOT NULL,
	[IdGender] [int] NULL,
	[Phone] [char](12) NOT NULL,
	[Salary] [decimal](8, 2) NOT NULL,
	[IdSpeciality] [int] NOT NULL,
 CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoctorsShedule]    Script Date: 11.06.2025 16:51:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorsShedule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BeginingDate] [time](6) NOT NULL,
	[EndDate] [time](6) NOT NULL,
	[IdDoctor] [int] NOT NULL,
 CONSTRAINT [PK_DoctorsShedule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genders]    Script Date: 11.06.2025 16:51:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 11.06.2025 16:51:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Patronymic] [nvarchar](100) NOT NULL,
	[IdGender] [int] NULL,
	[Phone] [char](12) NOT NULL,
	[DateBirthday] [datetime] NOT NULL,
	[IdPlots] [int] NOT NULL,
	[Age]  AS (datediff(year,[DateBirthday],getdate())),
 CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plots]    Script Date: 11.06.2025 16:51:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plots](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Plots] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reception]    Script Date: 11.06.2025 16:51:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reception](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdDoctor] [int] NULL,
	[IdPatient] [int] NOT NULL,
	[TimeNote] [datetime] NOT NULL,
 CONSTRAINT [PK_Reception] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialitys]    Script Date: 11.06.2025 16:51:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialitys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Specialitys] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Doctors] ON 

INSERT [dbo].[Doctors] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [Salary], [IdSpeciality]) VALUES (1, N'Петрова', N'Елена', N'Сергеевна', 2, N'+79393702410', CAST(35000.00 AS Decimal(8, 2)), 1)
INSERT [dbo].[Doctors] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [Salary], [IdSpeciality]) VALUES (2, N'Сидоров', N'Андрей', N'Михайлович', 1, N'+79393701311', CAST(50000.00 AS Decimal(8, 2)), 2)
INSERT [dbo].[Doctors] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [Salary], [IdSpeciality]) VALUES (3, N'Кузнецова', N'Ольга', N'Викторовна', 2, N'+79393707812', CAST(100000.00 AS Decimal(8, 2)), 3)
INSERT [dbo].[Doctors] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [Salary], [IdSpeciality]) VALUES (4, N'Смирнов', N'Алексей', N'Петрович', 1, N'+79393700813', CAST(40000.00 AS Decimal(8, 2)), 4)
INSERT [dbo].[Doctors] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [Salary], [IdSpeciality]) VALUES (5, N'Васильева', N'Татьяна', N'Дмитриевна', 2, N'+79393725314', CAST(45000.00 AS Decimal(8, 2)), 5)
INSERT [dbo].[Doctors] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [Salary], [IdSpeciality]) VALUES (6, N'Новиков', N'Сергей', N'Андреевич', 1, N'+79393700015', CAST(60000.00 AS Decimal(8, 2)), 6)
INSERT [dbo].[Doctors] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [Salary], [IdSpeciality]) VALUES (7, N'Павлова', N'Екатерина', N'Игоревна', 2, N'+79393707316', CAST(55000.00 AS Decimal(8, 2)), 7)
INSERT [dbo].[Doctors] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [Salary], [IdSpeciality]) VALUES (8, N'Морозов', N'Дмитрий', N'Владимирович', 1, N'+79393701217', CAST(50000.00 AS Decimal(8, 2)), 8)
INSERT [dbo].[Doctors] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [Salary], [IdSpeciality]) VALUES (9, N'Соколова', N'Анна', N'Юрьевна', 2, N'+79393707258', CAST(70000.00 AS Decimal(8, 2)), 9)
INSERT [dbo].[Doctors] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [Salary], [IdSpeciality]) VALUES (10, N'Игнатьев', N'Семен', N'Романович', 1, N'+79393707319', CAST(65000.00 AS Decimal(8, 2)), 10)
SET IDENTITY_INSERT [dbo].[Doctors] OFF
GO
SET IDENTITY_INSERT [dbo].[DoctorsShedule] ON 

INSERT [dbo].[DoctorsShedule] ([Id], [BeginingDate], [EndDate], [IdDoctor]) VALUES (1, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), 1)
INSERT [dbo].[DoctorsShedule] ([Id], [BeginingDate], [EndDate], [IdDoctor]) VALUES (2, CAST(N'08:00:00' AS Time), CAST(N'12:00:00' AS Time), 2)
INSERT [dbo].[DoctorsShedule] ([Id], [BeginingDate], [EndDate], [IdDoctor]) VALUES (3, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), 3)
INSERT [dbo].[DoctorsShedule] ([Id], [BeginingDate], [EndDate], [IdDoctor]) VALUES (4, CAST(N'07:30:00' AS Time), CAST(N'14:00:00' AS Time), 4)
INSERT [dbo].[DoctorsShedule] ([Id], [BeginingDate], [EndDate], [IdDoctor]) VALUES (5, CAST(N'07:00:00' AS Time), CAST(N'13:00:00' AS Time), 5)
INSERT [dbo].[DoctorsShedule] ([Id], [BeginingDate], [EndDate], [IdDoctor]) VALUES (6, CAST(N'10:00:00' AS Time), CAST(N'18:00:00' AS Time), 6)
INSERT [dbo].[DoctorsShedule] ([Id], [BeginingDate], [EndDate], [IdDoctor]) VALUES (7, CAST(N'13:00:00' AS Time), CAST(N'15:00:00' AS Time), 7)
INSERT [dbo].[DoctorsShedule] ([Id], [BeginingDate], [EndDate], [IdDoctor]) VALUES (8, CAST(N'08:00:00' AS Time), CAST(N'20:00:00' AS Time), 8)
INSERT [dbo].[DoctorsShedule] ([Id], [BeginingDate], [EndDate], [IdDoctor]) VALUES (9, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), 9)
INSERT [dbo].[DoctorsShedule] ([Id], [BeginingDate], [EndDate], [IdDoctor]) VALUES (10, CAST(N'07:00:00' AS Time), CAST(N'13:00:00' AS Time), 10)
SET IDENTITY_INSERT [dbo].[DoctorsShedule] OFF
GO
SET IDENTITY_INSERT [dbo].[Genders] ON 

INSERT [dbo].[Genders] ([Id], [Name]) VALUES (1, N'Мужской')
INSERT [dbo].[Genders] ([Id], [Name]) VALUES (2, N'Женский')
SET IDENTITY_INSERT [dbo].[Genders] OFF
GO
SET IDENTITY_INSERT [dbo].[Patients] ON 

INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (1, N'Галиев', N'Айрат', N'Маратович', 1, N'+79393701234', CAST(N'1980-05-05T00:00:00.000' AS DateTime), 3)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (2, N'Иванова', N'Светлана', N'Петровна', 2, N'+79393707310', CAST(N'2000-01-01T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (3, N'Смирнов', N'Дмитрий', N'Алексеевич', 1, N'+79393709104', CAST(N'2001-12-12T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (4, N'Закирова', N'Лейла', N'Фаритовна', 2, N'+79393702231', CAST(N'1970-08-30T00:00:00.000' AS DateTime), 5)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (5, N'Петрова', N'Алсу', N'Наилевна', 2, N'+79393701125', CAST(N'1980-01-02T00:00:00.000' AS DateTime), 3)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (6, N'Хабибуллин', N'Рамиль', N'Ильдарович', 1, N'+79393702215', CAST(N'1999-05-15T00:00:00.000' AS DateTime), 10)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (7, N'Юсупова', N'Ильмира', N'Руслановна', 2, N'+79393706125', CAST(N'2001-10-20T00:00:00.000' AS DateTime), 8)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (8, N'Михайлов', N'Андрей', N'Юрьевич', 1, N'+79393701785', CAST(N'2000-08-10T00:00:00.000' AS DateTime), 9)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (9, N'Кузнецов', N'Сергей', N'Владимирович', 1, N'+79393700085', CAST(N'1999-05-10T00:00:00.000' AS DateTime), 4)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (10, N'Большутаева', N'Диана', N'Тимержановна', 2, N'+79393708821', CAST(N'2008-12-16T00:00:00.000' AS DateTime), 10)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (11, N'Арбиров', N'Карим', N'Ильнурович', 1, N'+79393707333', CAST(N'2003-02-11T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (12, N'Амиров', N'Амир', N'Амирович', 1, N'+79393707555', CAST(N'2000-10-10T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (13, N'Галеев', N'Рамазан', N'Рафаэлевич', 1, N'+79393477261', CAST(N'2007-07-18T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (14, N'Марданов', N'Айдар', N'Булатович', 1, N'+71213234742', CAST(N'2008-04-15T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (15, N'Хусаенов', N'Ильдус', N'Ильнурович', 1, N'+75252528923', CAST(N'2007-06-26T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (16, N'ропашщп', N'кгплпл111', N'копл1', 1, N'5ке3це2йц   ', CAST(N'2025-06-05T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (17, N'1', N'1', N'1', 2, N'3цу13ц      ', CAST(N'2025-06-18T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (18, N'gfdfs', N'dfgds', N'dsgdsf', 1, N'gfseer      ', CAST(N'2025-05-27T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (31, N'Темирзянов', N'Амир', N'Маратович', 1, N'+79278128492', CAST(N'2007-12-13T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (32, N'Петров', N'Егор', N'Петрович', 1, N'+79349329495', CAST(N'2007-06-08T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (33, N'равр', N'ирва', N'рщиар', 1, N'+72838402384', CAST(N'2025-06-04T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (34, N'пап', N'вап', N'вап', 1, N'+73950239421', CAST(N'2025-06-04T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (35, N'tr', N'gew', N'erg', 1, N'+72352124598', CAST(N'2025-06-11T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (36, N'eh', N'hf', N'ghf', 1, N'+73854820912', CAST(N'2025-06-12T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Patients] ([Id], [Surname], [Name], [Patronymic], [IdGender], [Phone], [DateBirthday], [IdPlots]) VALUES (40, N'Петров', N'Петр', N'Петрович', 1, N'+73820120420', CAST(N'2025-06-03T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Patients] OFF
GO
SET IDENTITY_INSERT [dbo].[Plots] ON 

INSERT [dbo].[Plots] ([Id], [Name]) VALUES (1, N'Камалеева')
INSERT [dbo].[Plots] ([Id], [Name]) VALUES (2, N'Баумана')
INSERT [dbo].[Plots] ([Id], [Name]) VALUES (3, N'Кремлевская')
INSERT [dbo].[Plots] ([Id], [Name]) VALUES (4, N'Кутуя')
INSERT [dbo].[Plots] ([Id], [Name]) VALUES (5, N'Тукая')
INSERT [dbo].[Plots] ([Id], [Name]) VALUES (6, N'Ершова')
INSERT [dbo].[Plots] ([Id], [Name]) VALUES (7, N'Сююмбике')
INSERT [dbo].[Plots] ([Id], [Name]) VALUES (8, N'Галеева')
INSERT [dbo].[Plots] ([Id], [Name]) VALUES (9, N'Чистопольская')
INSERT [dbo].[Plots] ([Id], [Name]) VALUES (10, N'Победы')
SET IDENTITY_INSERT [dbo].[Plots] OFF
GO
SET IDENTITY_INSERT [dbo].[Reception] ON 

INSERT [dbo].[Reception] ([Id], [IdDoctor], [IdPatient], [TimeNote]) VALUES (1, 1, 3, CAST(N'2025-06-02T15:00:00.000' AS DateTime))
INSERT [dbo].[Reception] ([Id], [IdDoctor], [IdPatient], [TimeNote]) VALUES (2, 5, 11, CAST(N'2025-06-11T10:00:00.000' AS DateTime))
INSERT [dbo].[Reception] ([Id], [IdDoctor], [IdPatient], [TimeNote]) VALUES (3, 5, 12, CAST(N'2025-06-11T11:30:00.000' AS DateTime))
INSERT [dbo].[Reception] ([Id], [IdDoctor], [IdPatient], [TimeNote]) VALUES (4, 8, 13, CAST(N'2025-06-09T19:30:00.000' AS DateTime))
INSERT [dbo].[Reception] ([Id], [IdDoctor], [IdPatient], [TimeNote]) VALUES (5, 5, 14, CAST(N'2025-06-10T10:00:00.000' AS DateTime))
INSERT [dbo].[Reception] ([Id], [IdDoctor], [IdPatient], [TimeNote]) VALUES (6, 2, 15, CAST(N'2025-06-10T08:00:00.000' AS DateTime))
INSERT [dbo].[Reception] ([Id], [IdDoctor], [IdPatient], [TimeNote]) VALUES (7, 3, 16, CAST(N'2025-06-09T09:00:00.000' AS DateTime))
INSERT [dbo].[Reception] ([Id], [IdDoctor], [IdPatient], [TimeNote]) VALUES (8, 1, 17, CAST(N'2025-05-27T10:30:00.000' AS DateTime))
INSERT [dbo].[Reception] ([Id], [IdDoctor], [IdPatient], [TimeNote]) VALUES (9, 3, 18, CAST(N'2025-06-03T09:30:00.000' AS DateTime))
INSERT [dbo].[Reception] ([Id], [IdDoctor], [IdPatient], [TimeNote]) VALUES (10, 1, 31, CAST(N'2025-06-12T10:00:00.000' AS DateTime))
INSERT [dbo].[Reception] ([Id], [IdDoctor], [IdPatient], [TimeNote]) VALUES (11, 6, 32, CAST(N'2025-06-09T10:30:00.000' AS DateTime))
INSERT [dbo].[Reception] ([Id], [IdDoctor], [IdPatient], [TimeNote]) VALUES (12, 3, 33, CAST(N'2025-06-09T10:30:00.000' AS DateTime))
INSERT [dbo].[Reception] ([Id], [IdDoctor], [IdPatient], [TimeNote]) VALUES (13, 5, 34, CAST(N'2025-06-03T12:30:00.000' AS DateTime))
INSERT [dbo].[Reception] ([Id], [IdDoctor], [IdPatient], [TimeNote]) VALUES (14, 3, 35, CAST(N'2025-06-13T09:30:00.000' AS DateTime))
INSERT [dbo].[Reception] ([Id], [IdDoctor], [IdPatient], [TimeNote]) VALUES (15, 2, 36, CAST(N'2025-06-06T08:00:00.000' AS DateTime))
INSERT [dbo].[Reception] ([Id], [IdDoctor], [IdPatient], [TimeNote]) VALUES (16, 4, 40, CAST(N'2025-06-12T13:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Reception] OFF
GO
SET IDENTITY_INSERT [dbo].[Specialitys] ON 

INSERT [dbo].[Specialitys] ([Id], [Name]) VALUES (1, N'Кардиолог')
INSERT [dbo].[Specialitys] ([Id], [Name]) VALUES (2, N'Отоларинголог')
INSERT [dbo].[Specialitys] ([Id], [Name]) VALUES (3, N'Терапевт')
INSERT [dbo].[Specialitys] ([Id], [Name]) VALUES (4, N'Психиатр')
INSERT [dbo].[Specialitys] ([Id], [Name]) VALUES (5, N'Гематолог')
INSERT [dbo].[Specialitys] ([Id], [Name]) VALUES (6, N'Уролог')
INSERT [dbo].[Specialitys] ([Id], [Name]) VALUES (7, N'Офтальмолог')
INSERT [dbo].[Specialitys] ([Id], [Name]) VALUES (8, N'Акушер-гинеколог')
INSERT [dbo].[Specialitys] ([Id], [Name]) VALUES (9, N'Травматолог')
INSERT [dbo].[Specialitys] ([Id], [Name]) VALUES (10, N'Ревматолог')
INSERT [dbo].[Specialitys] ([Id], [Name]) VALUES (11, N'Гинеколог')
SET IDENTITY_INSERT [dbo].[Specialitys] OFF
GO
ALTER TABLE [dbo].[Doctors]  WITH CHECK ADD  CONSTRAINT [FK_Doctors_Gender] FOREIGN KEY([IdGender])
REFERENCES [dbo].[Genders] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Doctors] CHECK CONSTRAINT [FK_Doctors_Gender]
GO
ALTER TABLE [dbo].[Doctors]  WITH CHECK ADD  CONSTRAINT [FK_Doctors_Specialitys] FOREIGN KEY([IdSpeciality])
REFERENCES [dbo].[Specialitys] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Doctors] CHECK CONSTRAINT [FK_Doctors_Specialitys]
GO
ALTER TABLE [dbo].[DoctorsShedule]  WITH CHECK ADD  CONSTRAINT [FK_DoctorsShedule_Doctors] FOREIGN KEY([IdDoctor])
REFERENCES [dbo].[Doctors] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DoctorsShedule] CHECK CONSTRAINT [FK_DoctorsShedule_Doctors]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_Gender] FOREIGN KEY([IdGender])
REFERENCES [dbo].[Genders] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_Gender]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_Plots] FOREIGN KEY([IdPlots])
REFERENCES [dbo].[Plots] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_Plots]
GO
ALTER TABLE [dbo].[Reception]  WITH CHECK ADD  CONSTRAINT [FK_Reception_Doctors] FOREIGN KEY([IdDoctor])
REFERENCES [dbo].[Doctors] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Reception] CHECK CONSTRAINT [FK_Reception_Doctors]
GO
ALTER TABLE [dbo].[Reception]  WITH CHECK ADD  CONSTRAINT [FK_Reception_Patients] FOREIGN KEY([IdPatient])
REFERENCES [dbo].[Patients] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reception] CHECK CONSTRAINT [FK_Reception_Patients]
GO
