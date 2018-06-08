USE [KLINICOS_INTERNO]
GO

/****** Object:  StoredProcedure [dbo].[EliminarUsuarioRolHorario]    Script Date: 8/6/2018 10:29:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[EliminarUsuarioRolHorario]
(
	@id int
)
AS
BEGIN
	DELETE FROM UsuarioRolHorario where id = @id
END
GO

