USE [KLINICOS]
GO

/****** Object:  StoredProcedure [dbo].[ListarRoles]    Script Date: 7/5/2018 12:17:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[ListarRoles]
as
begin
	select * from Usuario.Roles
end
GO

