USE [KLINICOS_INTERNO]
GO

/****** Object:  StoredProcedure [dbo].[BuscarUsuarioRolHorario]    Script Date: 8/6/2018 10:29:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BuscarUsuarioRolHorario]
(
	@idUsuarioSector int
)
AS
BEGIN
	select * from UsuarioRolHorario where idUsuarioSector=@idUsuarioSector and fechaFin >= GETDATE()
END
GO

