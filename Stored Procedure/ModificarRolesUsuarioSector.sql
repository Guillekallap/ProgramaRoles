USE [KLINICOS]
GO

/****** Object:  StoredProcedure [dbo].[ModificarRolesUsuarioSector]    Script Date: 23/2/2018 13:51:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ModificarRolesUsuarioSector](
	@id int,
	@roles varchar(MAX)
)
AS
BEGIN 
	update Usuario.UsuariosSectores set roles= @roles where id= @id
END 
GO

