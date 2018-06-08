USE [KLINICOS_INTERNO]
GO

/****** Object:  StoredProcedure [dbo].[ActualizarVigente]    Script Date: 8/6/2018 10:29:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ActualizarVigente]
as
begin
	update dbo.UsuarioRolHorario set vigente=1 where fechaInicio < GETDATE() and fechaFin > GETDATE()
	update dbo.UsuarioRolHorario set vigente=0 where GETDATE()> fechaFin and vigente=1
end
GO

