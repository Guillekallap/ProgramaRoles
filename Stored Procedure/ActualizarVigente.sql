USE [KLINICOS_INTERNO]
GO

/****** Object:  StoredProcedure [dbo].[ActualizarVigente]    Script Date: 7/5/2018 12:14:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ActualizarVigente]
as
begin
	update dbo.UsuarioRolHorario set vigente=1 where fechaInicio < GETDATE() and fechaFin > GETDATE()
	--update dbo.UsuarioRolHorario set vigente=0 where fechaFin<GETDATE()
	--update dbo.UsuarioRolHorario set vigente=0 where fechaInicio<GETDATE()
end
GO

