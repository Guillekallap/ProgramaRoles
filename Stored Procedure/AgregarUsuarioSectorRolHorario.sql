USE [KLINICOS_INTERNO]
GO

/****** Object:  StoredProcedure [dbo].[AgregarUsuarioSectorRolHorario]    Script Date: 7/5/2018 12:14:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AgregarUsuarioSectorRolHorario] 
(
	@idUsuarioSector  int,
	@nombreUsuario varchar(MAX),
	@rolesTemporales varchar(MAX),
	@email varchar(255),
	@emailChked bit,
	@fechaInicio datetime,
	@fechaFin  datetime,
	@fechaModificacion datetime,
	@vigente bit
)AS
BEGIN
	INSERT INTO dbo.UsuarioRolHorario(idUsuarioSector,nombreUsuario,rolesTemporales,email,emailChked,fechaInicio,fechaFin,fechaModificacion,vigente) values(@idUsuarioSector,
	@nombreUsuario,
	@rolesTemporales,
	@email,
	@emailChked,
	@fechaInicio,
	@fechaFin,
	@fechaModificacion,
	@vigente)
END
GO

