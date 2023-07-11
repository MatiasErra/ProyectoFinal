USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[AltaAdmin]    Script Date: 11/7/2023 18:43:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AltaAdmin]
	-- Add the parameters for the stored procedure here
	@id int,
	@nombre varchar(40),
	@apellido varchar(40),
	@email varchar(40),
	@tele varchar (40),
	@fchNac varchar (40),
	@user varchar(40),
	@pass varchar(40),
	@TipoAdm varchar(40)


	
AS
BEGIN

	Declare @insPersonas varchar(1)
	 Declare @insAdm varchar(1)
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
BEGIN TRANSACTION
	 insert into Personas (idPersona, nombre, apellido, email, telefono, fchNacimiento) 
	Values (@id, @nombre, @apellido, @email, @tele, CONVERT(DATE,@fchNac,103))
	
	if  @@ROWCOUNT>0   
	begin 
	insert into Admins(idAdmin,usuario,contrasena,tipoDeAdmin) 
	Values (@id, @user, @pass, @TipoAdm) 
	end 



	If not exists (Select * From Personas Where idPersona = @id) or not exists (Select * From Admins Where idAdmin = @id)
	Begin
	raiserror('No se puedo crear la persona/admin',1,1);
		rollback;
	End

		Commit tran
	

END
GO

