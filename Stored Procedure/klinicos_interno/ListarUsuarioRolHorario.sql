USE [KLINICOS_INTERNO]
GO

/****** Object:  StoredProcedure [dbo].[ListarUsuarioRolHorario]    Script Date: 14/5/2018 11:12:16 ******/
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
	Select * from dbo.UsuarioRolHorario where idUsuarioSector=1007 and ((fechaInicio>@fechaInicio and fechaInicio<@fechaFin) 
	 or (fechaFin>@fechaInicio and fechaFin<@fechaFin)--((fechaInicio between @fechaInicio and @fechaFin)or (fechaFin between @fechaInicio and @fechaFin
	 or (fechaInicio<@fechaInicio and fechaFin>@fechaFin)
	 or (fechaInicio>@fechaInicio and fechaFin<@fechaFin))
END
GO

