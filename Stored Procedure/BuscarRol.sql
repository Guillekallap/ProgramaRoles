USE [KLINICOS]
GO

/****** Object:  StoredProcedure [dbo].[BuscarRol]    Script Date: 23/2/2018 13:49:46 ******/
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

