USE [KLINICOS_INTERNO]
GO

/****** Object:  StoredProcedure [dbo].[ListarUsuarioRolHorario]    Script Date: 27/3/2018 14:35:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ListarUsuarioRolHorario] 
(
	@idUsuarioSector int,
	@fechaInicio datetime,
	@fechaFin datetime
)AS 
BEGIN 
	Select * from dbo.UsuarioRolHorario where idUsuarioSector=@idUsuarioSector and fechaInicio >= @fechaInicio and fechaFin <= @fechaFin
END
GO

