USE [KLINICOS]
GO

/****** Object:  StoredProcedure [dbo].[BuscarRol]    Script Date: 7/5/2018 12:16:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[BuscarRol]
(
	@rol varchar(50)
)
as
begin
	select * from Usuario.Roles where rol=@rol  
end
GO

