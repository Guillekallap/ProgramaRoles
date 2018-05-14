USE [KLINICOS_INTERNO]
GO

/****** Object:  StoredProcedure [dbo].[BuscarUsuarioRolHorario]    Script Date: 14/5/2018 11:12:04 ******/
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

