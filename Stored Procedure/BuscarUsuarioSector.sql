USE [KLINICOS]
GO

/****** Object:  StoredProcedure [dbo].[BuscarUsuarioSector]    Script Date: 7/5/2018 12:16:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[BuscarUsuarioSector]
(
	@id int
)
as

begin
	 select usec.id as id, usec.idSector as idSector, sec.nombre as nombreSector, usec.idUsuario as idUsuario, us.nombreUsuario as nombreUsuario, us.numeroDocumento as dni, us.email as email, usec.fechaModi as fechaModi, usec.roles as roles from Usuario.UsuariosSectores usec 
	 join GeneralLocal.Sectores sec on usec.idSector = sec.id  
	 join Usuario.Usuarios us on usec.idUsuario = us.id where usec.id = @id
end
GO

