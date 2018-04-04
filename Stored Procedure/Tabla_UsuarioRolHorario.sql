USE [KLINICOS_INTERNO]
GO

/****** Object:  Table [dbo].[UsuarioRolHorario]    Script Date: 4/4/2018 10:25:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UsuarioRolHorario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idUsuarioSector] [int] NOT NULL,
	[nombreUsuario] [nvarchar](max) NOT NULL,
	[rolesTemporales] [nvarchar](max) NULL,
	[email] [nvarchar](255) NULL,
	[emailChked] [bit] NOT NULL,
	[fechaInicio] [datetime] NOT NULL,
	[fechaFin] [datetime] NOT NULL,
	[fechaModificacion] [datetime] NOT NULL,
	[vigente] [bit] NOT NULL,
 CONSTRAINT [PK_UsuarioRolHorario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

