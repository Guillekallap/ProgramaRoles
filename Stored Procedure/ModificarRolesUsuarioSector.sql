USE [KLINICOS]
GO

/****** Object:  StoredProcedure [dbo].[ModificarRolesUsuarioSector]    Script Date: 7/5/2018 12:20:38 ******/
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
	update Usuario.UsuariosSectores set roles= @roles, fechaModi=GETDATE() where id= @id
END 
GO

