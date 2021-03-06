USE [dbBaseProject]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 25/01/2022 16:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[IdCliente] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](30) NOT NULL,
	[Sobrenome] [varchar](100) NOT NULL,
	[Cpf] [varchar](11) NOT NULL,
	[DtNasc] [date] NOT NULL,
	[IdProfissao] [int] NULL,
	[Status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profissao]    Script Date: 25/01/2022 16:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profissao](
	[IdProfissao] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProfissao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cliente] ON 

INSERT [dbo].[Cliente] ([IdCliente], [Nome], [Sobrenome], [Cpf], [DtNasc], [IdProfissao], [Status]) VALUES (1, N'Mateus', N'Santos Andrade', N'42762879876', CAST(N'1994-09-16T13:54:51.250' AS DateTime), 1, 1)
INSERT [dbo].[Cliente] ([IdCliente], [Nome], [Sobrenome], [Cpf], [DtNasc], [IdProfissao], [Status]) VALUES (2, N'Jussara', N'Rocha Coutinho', N'11122233396', CAST(N'1996-07-28T13:54:51.250' AS DateTime), 2, 1)
INSERT [dbo].[Cliente] ([IdCliente], [Nome], [Sobrenome], [Cpf], [DtNasc], [IdProfissao], [Status]) VALUES (3, N'John Lucas', N'Santos Andrade', N'99988877765', CAST(N'1988-01-25T13:54:51.250' AS DateTime), 3, 1)
INSERT [dbo].[Cliente] ([IdCliente], [Nome], [Sobrenome], [Cpf], [DtNasc], [IdProfissao], [Status]) VALUES (4, N'Lucas', N'Miranda', N'44455566678', CAST(N'1988-01-24T13:54:51.250' AS DateTime), 4, 1)
INSERT [dbo].[Cliente] ([IdCliente], [Nome], [Sobrenome], [Cpf], [DtNasc], [IdProfissao], [Status]) VALUES (5, N'Bruna', N'Almeida', N'33322211174', CAST(N'1988-01-26T13:54:51.250' AS DateTime), 5, 1)
INSERT [dbo].[Cliente] ([IdCliente], [Nome], [Sobrenome], [Cpf], [DtNasc], [IdProfissao], [Status]) VALUES (6, N'Bruce', N'Wayne', N'11122233396', CAST(N'2000-03-28T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[Cliente] ([IdCliente], [Nome], [Sobrenome], [Cpf], [DtNasc], [IdProfissao], [Status]) VALUES (7, N'madruguinha', N'Rocha', N'11122233396', CAST(N'2010-03-28T00:00:00.000' AS DateTime), 4, 1)
SET IDENTITY_INSERT [dbo].[Cliente] OFF
GO
SET IDENTITY_INSERT [dbo].[Profissao] ON 

INSERT [dbo].[Profissao] ([IdProfissao], [Descricao], [Status]) VALUES (1, N'Desenvolvedor .NET', 1)
INSERT [dbo].[Profissao] ([IdProfissao], [Descricao], [Status]) VALUES (2, N'Arquiteto de Software', 1)
INSERT [dbo].[Profissao] ([IdProfissao], [Descricao], [Status]) VALUES (3, N'Coordenador de Sistemas', 1)
INSERT [dbo].[Profissao] ([IdProfissao], [Descricao], [Status]) VALUES (4, N'Tech Recruiter', 1)
INSERT [dbo].[Profissao] ([IdProfissao], [Descricao], [Status]) VALUES (5, N'Suporte Tecnico', 1)
INSERT [dbo].[Profissao] ([IdProfissao], [Descricao], [Status]) VALUES (6, N'Analista de Testes', 1)
SET IDENTITY_INSERT [dbo].[Profissao] OFF
GO
ALTER TABLE [dbo].[Cliente] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Profissao] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Cliente]  WITH CHECK ADD  CONSTRAINT [fk_IdProfissao] FOREIGN KEY([IdProfissao])
REFERENCES [dbo].[Profissao] ([IdProfissao])
GO
ALTER TABLE [dbo].[Cliente] CHECK CONSTRAINT [fk_IdProfissao]
GO
