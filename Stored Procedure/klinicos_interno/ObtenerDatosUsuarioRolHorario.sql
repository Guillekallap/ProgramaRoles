USE [KLINICOS_INTERNO]
GO

/****** Object:  StoredProcedure [dbo].[ObtenerDatosUsuarioRolHorario]    Script Date: 14/5/2018 11:12:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[ObtenerDatosUsuarioRolHorario]
as
begin 
	select * from dbo.UsuarioRolHorario where vigente=1 
end
GO

