USE [KLINICOS_INTERNO]
GO

/****** Object:  StoredProcedure [dbo].[ListarUsuarioRolHorario]    Script Date: 7/5/2018 12:15:14 ******/
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

