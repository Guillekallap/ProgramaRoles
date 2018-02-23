USE [KLINICOS]
GO

/****** Object:  StoredProcedure [dbo].[ListarUsuarioSector]    Script Date: 23/2/2018 13:50:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ListarUsuarioSector]
(
	@dni varchar(8) = null,
	@nombreUsuario varchar (255) = null,
	@nombreSector varchar(100) = null
)
as
begin
	select usec.id as id, usec.idSector as idSector, sec.nombre as nombreSector, usec.idUsuario as idUsuario, us.nombreUsuario as nombreUsuario,  us.numeroDocumento as dni, usec.roles as roles from Usuario.UsuariosSectores usec 
	 join GeneralLocal.Sectores sec on usec.idSector = sec.id  
	 join Usuario.Usuarios us on usec.idUsuario = us.id
	 where (us.numeroDocumento like  '%'+@dni+'%'  or @dni is null) and ( us.nombreUsuario like '%'+@nombreUsuario+'%'  or @nombreUsuario is null) and (sec.nombre like '%'+@nombreSector+'%' or @nombreSector is null)
end

GO

