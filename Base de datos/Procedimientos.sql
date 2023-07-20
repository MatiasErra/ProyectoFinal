USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[AltaAdmin]    Script Date: 19/7/2023 21:17:56 ******/
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

USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[AltaCamionero]    Script Date: 19/7/2023 21:18:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AltaCamionero]
	@id int,
	@nombre varchar(40),
	@apellido varchar(40),
	@email varchar(40),
	@tele varchar (40),
	@fchNac varchar (20),
	@cedula varchar(11),
	@disp varchar(20),
	@manejo varchar(20)

	
AS
BEGIN
	insert into Personas (idPersona, nombre, apellido, email, telefono, fchNacimiento) 
	values (@id, @nombre, @apellido, @email, @tele, CONVERT(DATE,@fchNac,103))
	if @@ROWCOUNT > 0
	begin
		insert into Camioneros(idCamionero, cedula, disponible, fchManejo) 
		values (@id, @cedula, @disp, CONVERT(DATE,@manejo,103) )
	end
END
GO




USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[AltaCli]    Script Date: 19/7/2023 21:18:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AltaCli]
	-- Add the parameters for the stored procedure here
	@id int,
	@nombre varchar(40),
	@apellido varchar(40),
	@email varchar(40),
	@tele varchar (40),
	@fchNac varchar (40),
	@user varchar(40),
	@pass varchar(40),
	@dirr varchar(40)


	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
BEGIN TRANSACTION
	 insert into Personas (idPersona, nombre, apellido, email, telefono, fchNacimiento) 
	Values (@id, @nombre, @apellido, @email, @tele, CONVERT(DATE,@fchNac,103))
	
	if  @@ROWCOUNT>0   
	begin 
	insert into Clientes(idCliente,usuario,contrasena,direccion) 
	Values (@id, @user, @pass, @dirr) 
	end 



	If not exists (Select * From Personas Where idPersona = @id) or not exists (Select * From Clientes Where idCliente = @id)
	Begin
	raiserror('No se puedo crear la persona/Cliente',1,1);
		rollback;
	End

		Commit tran
	

END
GO

USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[AltaDeposito]    Script Date: 19/7/2023 21:19:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AltaDeposito]
	@id int,
	@capacidad varchar(20),
	@ubicacion varchar(50),
	@temperatura numeric(3,0),
	@condiciones varchar(80)
AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRANSACTION
	insert into Depositos values (@id, @capacidad, @ubicacion, @temperatura, @condiciones) 

	If not exists (Select * From Depositos Where idDeposito = @id)
	Begin
	raiserror('No se pudo crear el Deposito',1,1);
		rollback;
	End

		Commit tran
	

END
GO



USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[BajaAdmin]    Script Date: 19/7/2023 21:19:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE  [dbo].[BajaAdmin] @id int
As
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from

	SET NOCOUNT ON;

	BEGIN TRANSACTION
	Delete from Admins where idAdmin = @id
	
	if  @@ROWCOUNT>0   
	begin 
	Delete from Personas where idPersona = @id
	end 
	If exists (Select * From Personas Where idPersona = @id) or exists (Select * From Admins Where idAdmin = @id)
	Begin
	raiserror('No se puedo borrar la persona/admin',1,1);
		rollback;
	End

		Commit tran
	

End
GO


USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[BajaCamionero]    Script Date: 19/7/2023 21:19:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[BajaCamionero]
	@id int
AS
BEGIN
	Begin Tran

	DELETE FROM Camioneros WHERE idCamionero = @id
	if @@ROWCOUNT > 0
	begin
		DELETE FROM Personas WHERE idPersona = @id
	end
	If exists (Select * From Personas Where idPersona = @id) or exists (Select * From Camioneros Where idCamionero = @id)
	Begin
	raiserror('No se puedo borrar la persona/admin',1,1);
		rollback;
	End

		Commit tran
	

END
GO


USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[BajaCli]    Script Date: 19/7/2023 21:19:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE  [dbo].[BajaCli] @id int
As
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from

	SET NOCOUNT ON;

	BEGIN TRANSACTION
	Delete from Clientes where idCliente = @id
	
	if  @@ROWCOUNT>0   
	begin 
	Delete from Personas where idPersona = @id
	end 
	If exists (Select * From Personas Where idPersona = @id) or exists (Select * From Clientes Where idCliente = @id)
	Begin
	raiserror('No se puedo borrar la persona/admin',1,1);
		rollback;
	End

		Commit tran
	

End
GO



	
	USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[BajaDeposito]    Script Date: 19/7/2023 21:20:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE  [dbo].[BajaDeposito] @id int
As
BEGIN

	SET NOCOUNT ON;

	BEGIN TRANSACTION
	Delete from Depositos where idDeposito = @id

	If exists (Select * From Depositos Where idDeposito = @id)
	Begin
	raiserror('No se pudo borrar el deposito',1,1);
		rollback;
	End

	Commit tran
	

End
GO


USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[BuscarAdm]    Script Date: 19/7/2023 21:20:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[BuscarAdm] 
	-- Add the parameters for the stored procedure here
@id int
AS
BEGIN
	
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,   A.usuario, A.contrasena , A.tipoDeAdmin 
		from Personas P inner join Admins A on P.idPersona = A.idAdmin
	Where P.idPersona = @id
END
GO


USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[BuscarCamionero]    Script Date: 19/7/2023 21:20:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[BuscarCamionero]
@id int
AS
BEGIN

	SELECT [idCamionero],[nombre],[apellido],[email],[telefono],[fchNacimiento],[cedula], disponible, fchManejo FROM Camioneros C inner join Personas P on P.idPersona = C.idCamionero where idCamionero = @id

END
GO


USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[BuscarCli]    Script Date: 19/7/2023 21:20:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[BuscarCli]
-- Add the parameters for the stored procedure here
@id int
AS
BEGIN
	
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento, C.usuario, C.contrasena , C.direccion
		from Personas P inner join Clientes C on P.idPersona = C.idCliente
	Where P.idPersona = @id
END
GO


USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[BuscarDeposito]    Script Date: 19/7/2023 21:21:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[BuscarDeposito] 
@id int
AS
BEGIN
	
	SET NOCOUNT ON;
	 	Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos Where idDeposito = @id
END

GO




USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[LstAdmin]    Script Date: 19/7/2023 21:20:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[LstAdmin]

as
BEGIN

	SET NOCOUNT ON;

	Select P.idPersona, P.nombre, P.apellido, A.usuario, A.tipoDeAdmin from Personas P inner join Admins A on P.idPersona = A.idAdmin



END
GO


USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[LstCamioneros]    Script Date: 19/7/2023 21:21:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[LstCamioneros]
	
AS
BEGIN


	SELECT [idPersona],[nombre],[apellido],[email],[telefono],fchNacimiento, C.cedula, C.disponible, C.fchManejo FROM Camioneros C inner join Personas P on P.idPersona = C.idCamionero

END

GO

USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[LstCliente]    Script Date: 19/7/2023 21:22:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[LstCliente]
AS
BEGIN
	
	Select P.idPersona, P.nombre, P.apellido, C.usuario, C.direccion  from Personas P inner join Clientes C on P.idPersona = C.idCliente
END
GO

USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[LstDepositos]    Script Date: 19/7/2023 21:22:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[LstDepositos]

as
BEGIN

	SET NOCOUNT ON;

	Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos



END
GO

USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[LstIdDepositos]    Script Date: 19/7/2023 21:22:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[LstIdDepositos]

AS
BEGIN
	SET NOCOUNT ON;

	Select idDeposito from Depositos
END

GO

USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[lstIdPersonas]    Script Date: 19/7/2023 21:22:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[lstIdPersonas]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select idPersona from Personas
END
GO

USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[ModificarAdm]    Script Date: 19/7/2023 21:23:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[ModificarAdm]
	@id int,
	@nombre varchar(40),
	@apellido varchar(40),
	@email varchar(40),
	@tele varchar (40),
	@fchNac varchar (40),
	@user varchar(40),
	@TipoAdm varchar(40)

AS
BEGIN
    UPDATE Admins SET usuario = @user ,tipoDeAdmin = @TipoAdm  where idAdmin = @id
	
		UPDATE Personas SET nombre = @nombre, apellido = @apellido, email = @email, telefono = @tele, fchNacimiento = CONVERT(DATE,@fchNac,103) where idPersona = @id

END
GO

USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[ModificarCamionero]    Script Date: 19/7/2023 21:23:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ModificarCamionero]
	@id int,
	@nombre varchar(40),
	@apellido varchar(40),
	@email varchar(40),
	@tele varchar (40),
	@fchNac varchar (40),
	@cedula varchar(11),
	@disp varchar(20),
	@manejo varchar(20)
AS
BEGIN
    UPDATE Camioneros SET cedula = @cedula, disponible = @disp, fchManejo = CONVERT(DATE,@manejo,103) where idCamionero = @id

		UPDATE Personas SET nombre = @nombre, apellido = @apellido, email = @email, telefono = @tele, fchNacimiento =CONVERT(DATE,@fchNac,103) where idPersona = @id

END
GO


USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[ModificarCli]    Script Date: 19/7/2023 21:23:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[ModificarCli]
	@id int,
	@nombre varchar(40),
	@apellido varchar(40),
	@email varchar(40),
	@tele varchar (40),
	@fchNac varchar (40),
	@user varchar(40),
	@dirr varchar(40)

AS
BEGIN
    UPDATE Clientes SET usuario = @user ,  direccion = @dirr  where idCliente = @id
	
		UPDATE Personas SET nombre = @nombre, apellido = @apellido, email = @email, telefono = @tele, fchNacimiento = CONVERT(DATE,@fchNac,103) where idPersona = @id

END
GO

USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[ModificarDeposito]    Script Date: 19/7/2023 21:23:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[ModificarDeposito]
	@id int,
	@capacidad varchar(20),
	@ubicacion varchar(50),
	@temperatura numeric(3,0),
	@condiciones varchar(80)

AS
BEGIN
    UPDATE Depositos SET capacidad = @capacidad, ubicacion = @ubicacion, temperatura = @temperatura, condiciones = @condiciones  where idDeposito = @id
END
GO

