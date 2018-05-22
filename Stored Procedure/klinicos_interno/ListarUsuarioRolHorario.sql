USE [KLINICOS_INTERNO]
GO

/****** Object:  StoredProcedure [dbo].[ListarUsuarioRolHorario]    Script Date: 22/5/2018 10:03:36 ******/
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
	Select * from dbo.UsuarioRolHorario where idUsuarioSector=@idUsuarioSector and (
	 --(fechaInicio>=@fechaInicio and fechaInicio<@fechaFin) 
	 --or 
	 (fechaFin>@fechaInicio and fechaFin<=@fechaFin)--((fechaInicio between @fechaInicio and @fechaFin)or (fechaFin between @fechaInicio and @fechaFin
	 --or (fechaInicio<=@fechaInicio and fechaFin>=@fechaFin)
	 --or (fechaInicio>=@fechaInicio and fechaFin<=@fechaFin)
	 )
END
GO

