USE [KLINICOS_INTERNO]
GO

/****** Object:  StoredProcedure [dbo].[ObtenerDatosUsuarioRolHorario]    Script Date: 8/6/2018 10:29:58 ******/
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

