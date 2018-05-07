USE [KLINICOS_INTERNO]
GO

/****** Object:  StoredProcedure [dbo].[ObtenerDatosUsuarioRolHorario]    Script Date: 7/5/2018 12:15:39 ******/
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

