USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[AltaAdmin]    Script Date: 17/7/2023 12:11:06 ******/
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

/****** Object:  StoredProcedure [dbo].[AltaCamionero]    Script Date: 17/7/2023 12:11:27 ******/
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
/****** Object:  StoredProcedure [dbo].[AltaDeposito]    Script Date: 18/7/2023 21:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AltaDeposito]
	@id int,
	@capacidad varchar(20),
	@ubicacion varchar(50),
	@temperatura numeric(2,0),
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

/****** Object:  StoredProcedure [dbo].[BajaAdmin]    Script Date: 17/7/2023 12:11:40 ******/
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

/****** Object:  StoredProcedure [dbo].[BajaCamionero]    Script Date: 17/7/2023 12:11:48 ******/
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
/****** Object:  StoredProcedure [dbo].[BajaDeposito]    Script Date: 18/7/2023 21:40:50 ******/
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

/****** Object:  StoredProcedure [dbo].[BuscarAdm]    Script Date: 17/7/2023 12:12:11 ******/
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

/****** Object:  StoredProcedure [dbo].[BuscarCamionero]    Script Date: 17/7/2023 12:12:28 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarDeposito]    Script Date: 18/7/2023 21:41:45 ******/
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



/****** Object:  StoredProcedure [dbo].[LstAdmin]    Script Date: 17/7/2023 12:12:42 ******/
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

/****** Object:  StoredProcedure [dbo].[LstCamioneros]    Script Date: 17/7/2023 12:12:50 ******/
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

/****** Object:  StoredProcedure [dbo].[lstIdPersonas]    Script Date: 17/7/2023 12:12:59 ******/
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
/****** Object:  StoredProcedure [dbo].[ListIdDepositos]    Script Date: 18/7/2023 21:42:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ListIdDepositos]

AS
BEGIN
	SET NOCOUNT ON;

	Select idDeposito from Depositos
END

GO
USE [Proyecto]
GO

/****** Object:  StoredProcedure [dbo].[ModificarAdm]    Script Date: 17/7/2023 12:13:07 ******/
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

/****** Object:  StoredProcedure [dbo].[ModificarCamionero]    Script Date: 17/7/2023 12:13:17 ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarDeposito]    Script Date: 18/7/2023 21:45:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[ModificarDeposito]
	@id int,
	@capacidad varchar(20),
	@ubicacion varchar(50),
	@temperatura numeric(2,0),
	@condiciones varchar(80)

AS
BEGIN
    UPDATE Depositos SET capacidad = @capacidad, ubicacion = @ubicacion, temperatura = @temperatura, condiciones = @condiciones  where idDeposito = @id
END
GO


