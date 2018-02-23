USE [KLINICOS]
GO

/****** Object:  StoredProcedure [dbo].[BuscarUsuarioSectorPorRol]    Script Date: 23/2/2018 15:50:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[BuscarUsuarioSectorPorRol]
(
	@rol varchar(MAX)
)
as

begin
	 select usec.id as id, usec.idSector as idSector, sec.nombre as nombreSector, usec.idUsuario as idUsuario, us.nombreUsuario as nombreUsuario, us.numeroDocumento as dni, usec.roles as roles from Usuario.UsuariosSectores usec 
	 join GeneralLocal.Sectores sec on usec.idSector = sec.id  
	 join Usuario.Usuarios us on usec.idUsuario = us.id where roles not like '%'+@rol+'%'
end
GO

