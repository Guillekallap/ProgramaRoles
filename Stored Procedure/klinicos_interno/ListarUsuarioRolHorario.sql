USE [KLINICOS_INTERNO]
GO

/****** Object:  StoredProcedure [dbo].[ListarUsuarioRolHorario]    Script Date: 8/6/2018 10:29:48 ******/
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
	Select * from dbo.UsuarioRolHorario where idUsuarioSector=@idUsuarioSector and ((@fechaInicio between fechaInicio and fechaFin) 
	or (@fechaFin between fechaInicio and fechaFin)
	or (@fechaInicio >= fechaInicio and @fechaFin<= fechaFin) 
	or (@fechaInicio <= fechaInicio and @fechaFin >= fechaFin) 
	)
END
GO

