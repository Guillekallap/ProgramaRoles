USE [KLINICOS]
GO

/****** Object:  StoredProcedure [dbo].[ListarRoles]    Script Date: 23/2/2018 13:50:35 ******/
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

