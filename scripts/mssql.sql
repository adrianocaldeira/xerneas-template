/****** Object:  Table [dbo].[Profiles]    Script Date: 03/25/2016 05:39:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Active] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Profiles] ON
INSERT [dbo].[Profiles] ([Id], [Name], [Active], [Created], [Updated]) VALUES (1, N'Master', 1, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
SET IDENTITY_INSERT [dbo].[Profiles] OFF
/****** Object:  Table [dbo].[Modules]    Script Date: 03/25/2016 05:39:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[CssClass] [nvarchar](50) NULL,
	[ModuleOrder] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Modules] ON
INSERT [dbo].[Modules] ([Id], [ParentId], [Name], [Description], [CssClass], [ModuleOrder], [Created], [Updated]) VALUES (1, NULL, N'Módulos', N'Módulos do sistema', N'fa-sitemap', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Modules] ([Id], [ParentId], [Name], [Description], [CssClass], [ModuleOrder], [Created], [Updated]) VALUES (2, NULL, N'Segurança', N'Módulo de segurança', N'fa-lock', 1, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Modules] ([Id], [ParentId], [Name], [Description], [CssClass], [ModuleOrder], [Created], [Updated]) VALUES (3, 2, N'Perfis de Usuário', N'Perfis de usuários do sistema', NULL, 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Modules] ([Id], [ParentId], [Name], [Description], [CssClass], [ModuleOrder], [Created], [Updated]) VALUES (4, 2, N'Usuários', N'Usuários do sistema', NULL, 1, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
SET IDENTITY_INSERT [dbo].[Modules] OFF
/****** Object:  Table [dbo].[Functionalities]    Script Date: 03/25/2016 05:39:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Functionalities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[Action] [nvarchar](100) NOT NULL,
	[Controller] [nvarchar](100) NOT NULL,
	[HttpMethod] [nvarchar](100) NOT NULL,
	[DefaultFunctionality] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Functionalities] ON
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (1, 1, N'Exibir', N'Exibição de módulos do sistema', N'Index', N'Modules', N'GET', 1, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (2, 1, N'Listar', N'Listagem de módulos do sistema', N'List', N'Modules', N'POST', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (3, 1, N'Excluir', N'Exclusão de módulos do sistema', N'Delete', N'Modules', N'DELETE', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (4, 1, N'Criar', N'Criação de módulo do sistema', N'New', N'Modules', N'GET', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (5, 1, N'Alterar', N'Alteração de módulo do sistema', N'Edit', N'Modules', N'GET', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (6, 1, N'Salvar', N'Salva módulo do sistema', N'Save', N'Modules', N'POST', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (7, 1, N'Criar/Editar Funcionalidade', N'Cria/Edita funcionalidade de um módulo', N'Form', N'Functionalities', N'POST', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (8, 1, N'Salvar Funcionalidade', N'Salva funcionalidade de um módulo', N'Save', N'Functionalities', N'POST', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (9, 3, N'Exibir', N'Exibição de perfis de usuário do sistema', N'Index', N'UserProfiles', N'GET', 1, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (10, 3, N'Listar', N'Listagem de perfis de usuário do sistema', N'List', N'UserProfiles', N'POST', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (11, 3, N'Excluir', N'Exclusão de perfis de usuário do sistema', N'Delete', N'UserProfiles', N'DELETE', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (12, 3, N'Criar', N'Criação de perfil de usuário do sistema', N'New', N'UserProfiles', N'GET', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (13, 3, N'Alterar', N'Alteração de perfil de usuário do sistema', N'Edit', N'UserProfiles', N'GET', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (14, 3, N'Salvar', N'Salva perfil de usuário do sistema', N'Save', N'UserProfiles', N'POST', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (15, 4, N'Exibir', N'Exibição de usuário do sistema', N'Index', N'Users', N'GET', 1, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (16, 4, N'Listar', N'Listagem de usuário do sistema', N'List', N'Users', N'POST', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (17, 4, N'Excluir', N'Exclusão de usuário do sistema', N'Delete', N'Users', N'Delete', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (18, 4, N'Criar', N'Criação de usuário do sistema', N'New', N'Users', N'GET', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (19, 4, N'Alterar', N'Alteração de usuário do sistema', N'Edit', N'Users', N'GET', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
INSERT [dbo].[Functionalities] ([Id], [ModuleId], [Name], [Description], [Action], [Controller], [HttpMethod], [DefaultFunctionality], [Created], [Updated]) VALUES (20, 4, N'Salvar', N'Salva usuário do sistema', N'Save', N'Users', N'POST', 0, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
SET IDENTITY_INSERT [dbo].[Functionalities] OFF
/****** Object:  Table [dbo].[Users]    Script Date: 03/25/2016 05:39:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProfileId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Login] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Salt] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
	[LastAccess] [datetime] NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Login] ASC,
	[Email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([Id], [ProfileId], [Name], [Login], [Email], [Password], [Salt], [Active], [LastAccess], [Created], [Updated]) VALUES (1, 1, N'Administrador', N'adm', N'adcaldeira@outlook.com', N'KQKIcedT93ciCGufWC97xw==', N'546626091f444748a62e217f72b7269e', 1, NULL, CAST(0x0000A5D4005CC790 AS DateTime), CAST(0x0000A5D4005CC790 AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  Table [dbo].[Profiles_x_Functionalities]    Script Date: 03/25/2016 05:39:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profiles_x_Functionalities](
	[ProfileId] [int] NOT NULL,
	[FunctionalityId] [int] NOT NULL
) ON [PRIMARY]
GO
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 1)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 2)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 3)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 4)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 5)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 6)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 7)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 8)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 9)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 10)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 11)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 12)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 13)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 14)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 15)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 16)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 17)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 18)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 19)
INSERT [dbo].[Profiles_x_Functionalities] ([ProfileId], [FunctionalityId]) VALUES (1, 20)
/****** Object:  ForeignKey [FK3ACA2223FBB441AD]    Script Date: 03/25/2016 05:39:36 ******/
ALTER TABLE [dbo].[Functionalities]  WITH CHECK ADD  CONSTRAINT [FK3ACA2223FBB441AD] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Modules] ([Id])
GO
ALTER TABLE [dbo].[Functionalities] CHECK CONSTRAINT [FK3ACA2223FBB441AD]
GO
/****** Object:  ForeignKey [FKA476911EC9EE27BC]    Script Date: 03/25/2016 05:39:36 ******/
ALTER TABLE [dbo].[Modules]  WITH CHECK ADD  CONSTRAINT [FKA476911EC9EE27BC] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Modules] ([Id])
GO
ALTER TABLE [dbo].[Modules] CHECK CONSTRAINT [FKA476911EC9EE27BC]
GO
/****** Object:  ForeignKey [FKBD05BD57E3E546D0]    Script Date: 03/25/2016 05:39:36 ******/
ALTER TABLE [dbo].[Profiles_x_Functionalities]  WITH CHECK ADD  CONSTRAINT [FKBD05BD57E3E546D0] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profiles] ([Id])
GO
ALTER TABLE [dbo].[Profiles_x_Functionalities] CHECK CONSTRAINT [FKBD05BD57E3E546D0]
GO
/****** Object:  ForeignKey [FKBD05BD57ED9CFA47]    Script Date: 03/25/2016 05:39:36 ******/
ALTER TABLE [dbo].[Profiles_x_Functionalities]  WITH CHECK ADD  CONSTRAINT [FKBD05BD57ED9CFA47] FOREIGN KEY([FunctionalityId])
REFERENCES [dbo].[Functionalities] ([Id])
GO
ALTER TABLE [dbo].[Profiles_x_Functionalities] CHECK CONSTRAINT [FKBD05BD57ED9CFA47]
GO
/****** Object:  ForeignKey [FK2C1C7FE5E3E546D0]    Script Date: 03/25/2016 05:39:36 ******/
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK2C1C7FE5E3E546D0] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profiles] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK2C1C7FE5E3E546D0]
GO
