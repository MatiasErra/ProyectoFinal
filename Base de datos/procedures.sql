USE [Proyecto]
GO
/****** Object:  StoredProcedure [dbo].[AltaAdmin]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
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
	@TipoAdm varchar(40),
	@estado varchar(40)

	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
BEGIN TRANSACTION
	 insert into Personas (idPersona, nombre, apellido, email, telefono, fchNacimiento) 
	Values (@id, @nombre, @apellido, @email, @tele, @fchNac)
	
	if  @@ROWCOUNT>0   
	begin 
	insert into Admins(idAdmin,usuario,contrasena,tipoDeAdmin, estado) 
	Values (@id, @user, @pass, @TipoAdm, @estado) 
	end 



	If not exists (Select * From Personas Where idPersona = @id) or not exists (Select * From Admins Where idAdmin = @id)
	Begin
	raiserror('No se puedo crear la persona/admin',1,1);
		rollback;
	End

		Commit tran
	

END
GO
/****** Object:  StoredProcedure [dbo].[AltaCam]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AltaCam]
	-- Add the parameters for the stored procedure here
	@id int,
	@marca varchar(40),
	@modelo varchar(40),
	@carga numeric(10,2),
	@disponible varchar (40)



	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
BEGIN TRANSACTION
	 insert into Camiones (idCamion, marca, modelo, carga, disponible) 
	Values (@id, @marca, @modelo, @carga, @disponible)
	


	If not exists (Select * From Camiones Where idCamion = @id)
	Begin
	raiserror('No se puedo crear el Camion',1,1);
		rollback;
	End

		Commit tran
	

END
GO
/****** Object:  StoredProcedure [dbo].[AltaCamionero]    Script Date: 29/8/2023 22:53:22 ******/
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
BEGIN TRANSACTION

	insert into Personas (idPersona, nombre, apellido, email, telefono, fchNacimiento) 
	values (@id, @nombre, @apellido, @email, @tele, @fchNac)
	if @@ROWCOUNT > 0
	begin
		insert into Camioneros(idCamionero, cedula, disponible, fchManejo) 
		values (@id, @cedula, @disp, @manejo)
	end

		If not exists (Select * From Personas Where idPersona = @id) or not exists (Select * From Camioneros Where idCamionero = @id)
	Begin
	raiserror('No se puedo crear la persona/camionero',1,1);
		rollback;
	End

		Commit tran

		End



GO
/****** Object:  StoredProcedure [dbo].[AltaCli]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
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
	Values (@id, @nombre, @apellido, @email, @tele, @fchNac)
	
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
/****** Object:  StoredProcedure [dbo].[AltaDeposito]    Script Date: 29/8/2023 22:53:22 ******/
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
/****** Object:  StoredProcedure [dbo].[AltaFerti]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AltaFerti]
	@id int,
	@nombre varchar(40),
	@tipo varchar(40),
	@pH numeric(4,2),
	@impacto varchar(30)
AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRANSACTION
	insert into Fertilizantes values (@id, @nombre, @tipo, @pH, @impacto) 

	If not exists (Select * From Fertilizantes Where idFerti = @id)
	Begin
	raiserror('No se pudo crear el Fertilizante',1,1);
		rollback;
	End

		Commit tran
	

END
GO
/****** Object:  StoredProcedure [dbo].[AltaGranja]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AltaGranja]
	@id int,
	@nombre varchar(30),
	@ubicacion varchar(50),
	@idCliente int
AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRANSACTION
	insert into Granjas values (@id, @nombre, @ubicacion, @idCliente) 
	If not exists (Select * From Granjas Where idGranja = @id)
	Begin
	raiserror('No se pudo crear la Granja',1,1);
		rollback;
	End
Commit tran
END
GO
/****** Object:  StoredProcedure [dbo].[AltaLote]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**/
CREATE PROCEDURE [dbo].[AltaLote]
	@idGranja int,
	@idProducto int,
	@fchProduccion varchar(20),
	@cantidad varchar(40),
	@precio numeric(10,2),
	@idDeposito int,
	@cantTotal varchar(40)
AS
BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION
	insert into Lotes values (@idGranja, @idProducto, @fchProduccion, @cantidad, @precio, @idDeposito) 
	If not exists (Select * From Lotes Where idGranja = @idGranja and idProducto = @idProducto and fchProduccion = @fchProduccion)
	Begin
	raiserror('No se pudo crear el Lote',1,1);
		rollback;
	End
	else
	begin
	 Commit tran 
	 Update Productos set CantTotal = @cantTotal 
	 where idProducto = @idProducto
	end

END

GO
/****** Object:  StoredProcedure [dbo].[AltaLoteFerti]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AltaLoteFerti]
	@idFertilizante int,
	@idGranja int,
	@idProducto int,
	@fchProduccion varchar(20),
	@cantidad varchar(30)
AS
BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION
	insert into Lotes_Fertis values(@idFertilizante, @idGranja, @idProducto, CONVERT(DATE,@fchProduccion,103), @cantidad)
	If not exists (Select * From Lotes_Fertis Where idFertilizante = @idFertilizante and idGranja = @idGranja and idProducto = @idProducto and fchProduccion = CONVERT(DATE,@fchProduccion,103))
	Begin
	raiserror('No se pudo crear el Lote_Ferti',1,1);
		rollback;
	End
	
Commit tran
END



GO
/****** Object:  StoredProcedure [dbo].[AltaLotePesti]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AltaLotePesti]
	@idPesticida int,
	@idGranja int,
	@idProducto int,
	@fchProduccion varchar(20),
	@cantidad varchar(30)
AS
BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION
	insert into Lotes_Pestis values(@idPesticida, @idGranja, @idProducto, CONVERT(DATE,@fchProduccion,103), @cantidad)
	If not exists (Select * From Lotes_Pestis Where idPesticida = @idPesticida and idGranja = @idGranja and idProducto = @idProducto and fchProduccion = CONVERT(DATE,@fchProduccion,103))
	Begin
	raiserror('No se pudo crear el Lote_Pesti',1,1);
		rollback;
	End
	
Commit tran
END
GO
/****** Object:  StoredProcedure [dbo].[AltaPesti]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[AltaPesti]
	@id int,
	@nombre varchar(40),
	@tipo varchar(40),
	@pH numeric(4,2),
	@impacto varchar(30)
AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRANSACTION
	insert into Pesticidas values (@id, @nombre, @tipo, @pH, @impacto) 

	If not exists (Select * From Pesticidas Where idPesti = @id)
	Begin
	raiserror('No se pudo crear el Pesticida',1,1);
		rollback;
	End

		Commit tran
	

END
GO
/****** Object:  StoredProcedure [dbo].[AltaProducto]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AltaProducto]
	@id int,
	@nombre varchar(30),
	@tipo varchar(30),
	@tipoVenta varchar(15),
	@imagen varchar(MAX)
	
AS
BEGIN



	SET NOCOUNT ON;

BEGIN TRANSACTION
	insert into Productos values (@id, @nombre, @tipo, @tipoVenta, @imagen, 0) 
	If not exists (Select * From Productos Where idProducto = @id)
	Begin
	raiserror('No se pudo crear el Producto',1,1);
		rollback;
	End
Commit tran
END
GO
/****** Object:  StoredProcedure [dbo].[BajaAdmin]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[BajaAdmin] @id int
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
/****** Object:  StoredProcedure [dbo].[BajaCam]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[BajaCam] @id int
As
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from

	SET NOCOUNT ON;

	BEGIN TRANSACTION
	Delete from Camiones where idCamion = @id

	If exists (Select * From Camiones Where idCamion = @id)
	Begin
	raiserror('No se puedo borrar el camion',1,1);
		rollback;
	End

		Commit tran
	

End
GO
/****** Object:  StoredProcedure [dbo].[BajaCamionero]    Script Date: 29/8/2023 22:53:22 ******/
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
/****** Object:  StoredProcedure [dbo].[BajaCli]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[BajaCli] @id int
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
/****** Object:  StoredProcedure [dbo].[BajaDeposito]    Script Date: 29/8/2023 22:53:22 ******/
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
/****** Object:  StoredProcedure [dbo].[BajaFerti]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE  [dbo].[BajaFerti] @id int
As
BEGIN

	SET NOCOUNT ON;

	BEGIN TRANSACTION
	Delete from Fertilizantes where idFerti = @id

	If exists (Select * From Fertilizantes Where idFerti = @id)
	Begin
	raiserror('No se pudo borrar el fertilizante',1,1);
		rollback;
	End

	Commit tran
	

End
GO
/****** Object:  StoredProcedure [dbo].[BajaGranja]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[BajaGranja]
	@id int
As
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION
	Delete from Granjas where idGranja = @id
	If exists (Select * From Granjas Where idGranja = @id)
	Begin
	raiserror('No se pudo borrar la Granja',1,1);
		rollback;
	End
Commit tran
End
GO
/****** Object:  StoredProcedure [dbo].[BajaLote]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[BajaLote]
	@nombreGranja varchar(30),
	@nombreProducto varchar(30),
	@fchProduccion varchar(20),
	@cantTotal varchar(40)
As
BEGIN

	BEGIN TRANSACTION
	




		Delete L from Lotes_Pestis L
		inner join Granjas G on L.idGranja = G.idGranja
		inner join Productos P on L.idProducto = P.idProducto
		where G.nombre = @nombreGranja and P.nombre = @nombreGranja and  L.fchProduccion =  CONVERT(DATE,@fchProduccion,103)



		Delete L from Lotes_Fertis L
		inner join Granjas G on L.idGranja = G.idGranja 
		inner join Productos P on L.idProducto = P.idProducto
		where G.nombre = @nombreGranja and P.nombre = @nombreProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103)


		
		Delete L from Lotes L 
		inner join Granjas G on L.idGranja = G.idGranja 
		inner join Productos P on L.idProducto = P.idProducto
		where G.nombre = @nombreGranja and P.nombre = @nombreProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103)
	


		If exists (Select * From Lotes L 
		inner join Granjas G on L.idGranja = G.idGranja 
		inner join Productos P on L.idProducto = P.idProducto
		where G.nombre = @nombreGranja and P.nombre = @nombreProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103))
		or exists (Select * From Lotes_Fertis L
		inner join Granjas G on L.idGranja = G.idGranja 
		inner join Productos P on L.idProducto = P.idProducto
		where G.nombre = @nombreGranja and P.nombre = @nombreProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103)) 
		or exists (Select * From Lotes_Pestis L inner join Granjas G on L.idGranja = G.idGranja 
		inner join Productos P on L.idProducto = P.idProducto
		where G.nombre = @nombreGranja and P.nombre = @nombreProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103))
		begin
			raiserror('No se pudo borrar el Lote',1,1);
			rollback;
		end
		else
		begin

				 Update Productos set CantTotal = @cantTotal 
	 where nombre = @nombreProducto
		Commit TRANSACTION
		end

		

End

/****** Object:  StoredProcedure [dbo].[BuscarLote]    Script Date: 15/8/2023 23:58:05 ******/
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[BajaLoteFerti]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[BajaLoteFerti]
	@idFertilizante int,
	@idGranja int,
	@idProducto int,
	@fchProduccion varchar(20)
As
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION

		Delete from Lotes_Fertis where idFertilizante = @idFertilizante and idGranja = @idGranja and idProducto = @idProducto and fchProduccion = CONVERT(DATE,@fchProduccion,103)
		If exists (Select * From Lotes_Fertis Where idFertilizante = @idFertilizante and idGranja = @idGranja and idProducto = @idProducto and fchProduccion = CONVERT(DATE,@fchProduccion,103))
		begin
			raiserror('No se pudo borrar el Lote_Ferti',1,1);
			rollback;
		end

	
	
Commit tran
End

GO
/****** Object:  StoredProcedure [dbo].[BajaLotePesti]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[BajaLotePesti]
	@idPesticida int,
	@idGranja int,
	@idProducto int,
	@fchProduccion varchar(20)
As
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION

		Delete from Lotes_Pestis where idPesticida = @idPesticida and idGranja = @idGranja and idProducto = @idProducto and fchProduccion = CONVERT(DATE,@fchProduccion,103)
		If exists (Select * From Lotes_Pestis Where idPesticida = @idPesticida and idGranja = @idGranja and idProducto = @idProducto and fchProduccion = CONVERT(DATE,@fchProduccion,103))
		begin
			raiserror('No se pudo borrar el Lote_Pesti',1,1);
			rollback;
		end
Commit tran
End

GO
/****** Object:  StoredProcedure [dbo].[BajaPesti]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE  [dbo].[BajaPesti] @id int
As
BEGIN

	SET NOCOUNT ON;

	BEGIN TRANSACTION
	Delete from Pesticidas where idPesti = @id

	If exists (Select * From Pesticidas Where idPesti = @id)
	Begin
	raiserror('No se pudo borrar el pesticida',1,1);
		rollback;
	End

	Commit tran
	

End
GO
/****** Object:  StoredProcedure [dbo].[BajaProducto]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[BajaProducto]
	@id int
As
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION
	Delete from Productos where idProducto = @id
	If exists (Select * From Productos Where idProducto = @id)
	Begin
	raiserror('No se pudo borrar el Producto',1,1);
		rollback;
	End
Commit tran
End
GO
/****** Object:  StoredProcedure [dbo].[BuscarAdm]    Script Date: 29/8/2023 22:53:22 ******/
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
	 	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,   A.usuario, A.contrasena , A.tipoDeAdmin, A.estado
		from Personas P inner join Admins A on P.idPersona = A.idAdmin
	Where P.idPersona = @id
END
GO
/****** Object:  StoredProcedure [dbo].[BuscarAdminFiltro]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE   PROCEDURE [dbo].[BuscarAdminFiltro]
	-- Add the parameters for the stored procedure here
	@buscar varchar(40),
	@varEst varchar(40),
	@varAdm varchar(40),
	@ordenar varchar(40)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	if(@ordenar ='')
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
	where  (nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar +  '%' or email LIKE '%' + @buscar + '%' or usuario LIKE '%' + @buscar + '%')
	and ( tipoDeAdmin like  @varAdm + '%')   and  (estado LIKE  @varEst + '%' )
	order by  P.idPersona
	end


		if(@ordenar ='Nombre')
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
	where  (nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar +  '%' or email LIKE '%' + @buscar + '%' or usuario LIKE '%' + @buscar + '%')
	and ( tipoDeAdmin like  @varAdm + '%')   and  (estado LIKE  @varEst + '%' )
	order by  P.nombre
	end


	
		if(@ordenar ='Apellido')
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
	where  (nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar +  '%' or email LIKE '%' + @buscar + '%' or usuario LIKE '%' + @buscar + '%')
	and ( tipoDeAdmin like  @varAdm + '%')   and  (estado LIKE  @varEst + '%' )
	order by  P.apellido
	end

			if(@ordenar ='E-Mail')
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
	where  (nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar +  '%' or email LIKE '%' + @buscar + '%' or usuario LIKE '%' + @buscar + '%')
	and ( tipoDeAdmin like  @varAdm + '%')   and  (estado LIKE  @varEst + '%' )
	order by  P.email
	end


			if(@ordenar ='Teléfono')
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
	where  (nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar +  '%' or email LIKE '%' + @buscar + '%' or usuario LIKE '%' + @buscar + '%')
	and ( tipoDeAdmin like  @varAdm + '%')   and  (estado LIKE  @varEst + '%' )
	order by  P.telefono
	end

		if(@ordenar ='Fecha de Nacimiento')
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
	where  (nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar +  '%' or email LIKE '%' + @buscar + '%' or usuario LIKE '%' + @buscar + '%')
	and ( tipoDeAdmin like  @varAdm + '%')   and  (estado LIKE  @varEst + '%' )
	order by  P.fchNacimiento
	end

		if(@ordenar ='Usuario')
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
	where  (nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar +  '%' or email LIKE '%' + @buscar + '%' or usuario LIKE '%' + @buscar + '%')
	and ( tipoDeAdmin like  @varAdm + '%')   and  (estado LIKE  @varEst + '%' )
	order by  A.usuario
	end

			if(@ordenar ='Tipo de Admin')
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
	where  (nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar +  '%' or email LIKE '%' + @buscar + '%' or usuario LIKE '%' + @buscar + '%')
	and ( tipoDeAdmin like  @varAdm + '%')   and  (estado LIKE  @varEst + '%' )
	order by  A.tipoDeAdmin
	end


			if(@ordenar ='Estado')
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
	where  (nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar +  '%' or email LIKE '%' + @buscar + '%' or usuario LIKE '%' + @buscar + '%')
	and ( tipoDeAdmin like  @varAdm + '%')   and  (estado LIKE  @varEst + '%' )
	order by A.estado
	end
END


GO
/****** Object:  StoredProcedure [dbo].[BuscarCam]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[BuscarCam] 
@id int
AS
BEGIN
	
	SET NOCOUNT ON;
	 	Select idCamion, marca, modelo, carga, disponible from Camiones Where idCamion = @id
END

GO
/****** Object:  StoredProcedure [dbo].[BuscarCamionero]    Script Date: 29/8/2023 22:53:22 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarCamioneroFiltro]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BuscarCamioneroFiltro]

	@buscar varchar(40),
	@disp varchar(40),
	@ordenar varchar(40)
AS
BEGIN
	SET NOCOUNT ON;
	if @ordenar = ''
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
	where ( nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar + '%' or email LIKE '%' + @buscar + '%' or 
	cedula LIKE '%' + @buscar + '%' or fchManejo LIKE '%' + @buscar + '%') and (disponible LIKE @disp + '%') 
	order by P.idPersona
	end


		if @ordenar = 'Nombre'
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
	where ( nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar + '%' or email LIKE '%' + @buscar + '%' or 
	cedula LIKE '%' + @buscar + '%' or fchManejo LIKE '%' + @buscar + '%') and (disponible LIKE @disp + '%') 
	order by P.nombre
	end


			if @ordenar = 'Apellido'
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
	where ( nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar + '%' or email LIKE '%' + @buscar + '%' or 
	cedula LIKE '%' + @buscar + '%' or fchManejo LIKE '%' + @buscar + '%') and (disponible LIKE @disp + '%') 
	order by P.Apellido
	end

			if @ordenar = 'E-Mail'
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
	where ( nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar + '%' or email LIKE '%' + @buscar + '%' or 
	cedula LIKE '%' + @buscar + '%' or fchManejo LIKE '%' + @buscar + '%') and (disponible LIKE @disp + '%') 
	order by P.email
	end

	
			if @ordenar = 'Teléfono'
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
	where ( nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar + '%' or email LIKE '%' + @buscar + '%' or 
	cedula LIKE '%' + @buscar + '%' or fchManejo LIKE '%' + @buscar + '%') and (disponible LIKE @disp + '%') 
	order by P.Telefono
	end


		if @ordenar = 'Fecha de Nacimiento'
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
	where ( nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar + '%' or email LIKE '%' + @buscar + '%' or 
	cedula LIKE '%' + @buscar + '%' or fchManejo LIKE '%' + @buscar + '%') and (disponible LIKE @disp + '%') 
	order by P.fchNacimiento
	end


		if @ordenar = 'Cedula'
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
	where ( nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar + '%' or email LIKE '%' + @buscar + '%' or 
	cedula LIKE '%' + @buscar + '%' or fchManejo LIKE '%' + @buscar + '%') and (disponible LIKE @disp + '%') 
	order by C.cedula
	end


	
		if @ordenar = 'Vencimiento de libreta'
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
	where ( nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar + '%' or email LIKE '%' + @buscar + '%' or 
	cedula LIKE '%' + @buscar + '%' or fchManejo LIKE '%' + @buscar + '%') and (disponible LIKE @disp + '%') 
	order by C.fchManejo
	end



	
		if @ordenar = 'Disponible'
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
	where ( nombre LIKE '%' + @buscar + '%' or apellido LIKE '%' + @buscar + '%' or email LIKE '%' + @buscar + '%' or 
	cedula LIKE '%' + @buscar + '%' or fchManejo LIKE '%' + @buscar + '%') and (disponible LIKE @disp + '%') 
	order by C.disponible
	end
END
GO
/****** Object:  StoredProcedure [dbo].[BuscarCli]    Script Date: 29/8/2023 22:53:22 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarCliFiltro]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[BuscarCliFiltro]
	-- Add the parameters for the stored procedure here
@buscar varchar(40),

	@ordenar varchar(40)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if(@ordenar ='')
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
	where idPersona LIKE '%' + @buscar + '%'  or @buscar LIKE '%' + @buscar + '%'  or apellido LIKE '%' + @buscar + '%' or email LIKE '%' + @buscar + '%' or usuario LIKE '%' + @buscar + '%' or Direccion LIKE '%' + @buscar + '%'
	order by P.idPersona
	end

		if(@ordenar ='Nombre')
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
	where idPersona LIKE '%' + @buscar + '%'  or @buscar LIKE '%' + @buscar + '%'  or apellido LIKE '%' + @buscar + '%' or email LIKE '%' + @buscar + '%' or usuario LIKE '%' + @buscar + '%' or Direccion LIKE '%' + @buscar + '%'
	order by P.nombre
	end

		if(@ordenar ='Apellido')
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
	where idPersona LIKE '%' + @buscar + '%'  or @buscar LIKE '%' + @buscar + '%'  or apellido LIKE '%' + @buscar + '%' or email LIKE '%' + @buscar + '%' or usuario LIKE '%' + @buscar + '%' or Direccion LIKE '%' + @buscar + '%'
	order by P.apellido
	end


		if(@ordenar ='E-Mail')
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
	where idPersona LIKE '%' + @buscar + '%'  or @buscar LIKE '%' + @buscar + '%'  or apellido LIKE '%' + @buscar + '%' or email LIKE '%' + @buscar + '%' or usuario LIKE '%' + @buscar + '%' or Direccion LIKE '%' + @buscar + '%'
	order by P.email
	end


		if(@ordenar ='Teléfono')
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
	where idPersona LIKE '%' + @buscar + '%'  or @buscar LIKE '%' + @buscar + '%'  or apellido LIKE '%' + @buscar + '%' or email LIKE '%' + @buscar + '%' or usuario LIKE '%' + @buscar + '%' or Direccion LIKE '%' + @buscar + '%'
	order by P.telefono
	end



		if(@ordenar ='Fecha de Nacimiento')
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
	where idPersona LIKE '%' + @buscar + '%'  or @buscar LIKE '%' + @buscar + '%'  or apellido LIKE '%' + @buscar + '%' or email LIKE '%' + @buscar + '%' or usuario LIKE '%' + @buscar + '%' or Direccion LIKE '%' + @buscar + '%'
	order by P.fchNacimiento
	end

		if(@ordenar ='Usuario')
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
	where idPersona LIKE '%' + @buscar + '%'  or @buscar LIKE '%' + @buscar + '%'  or apellido LIKE '%' + @buscar + '%' or email LIKE '%' + @buscar + '%' or usuario LIKE '%' + @buscar + '%' or Direccion LIKE '%' + @buscar + '%'
	order by c.usuario
	end

		if(@ordenar ='Dirección')
	begin
	Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
	where idPersona LIKE '%' + @buscar + '%'  or @buscar LIKE '%' + @buscar + '%'  or apellido LIKE '%' + @buscar + '%' or email LIKE '%' + @buscar + '%' or usuario LIKE '%' + @buscar + '%' or Direccion LIKE '%' + @buscar + '%'
	order by C.Direccion
	end

END
GO
/****** Object:  StoredProcedure [dbo].[BuscarDeposito]    Script Date: 29/8/2023 22:53:22 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarDepositoFiltro]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[BuscarDepositoFiltro]

	@buscar varchar(40),
	@ordenar varchar(40)
AS
BEGIN

	SET NOCOUNT ON;

	if @ordenar = ''
	begin
	Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
	where  capacidad  LIKE '%' + @buscar + '%' or ubicacion LIKE '%' + @buscar + '%' or condiciones LIKE '%' + @buscar + '%' or temperatura LIKE '%' + @buscar + '%'
	order by idDeposito
	end

		if @ordenar = 'Capacidad'
	begin
	Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
	where  capacidad  LIKE '%' + @buscar + '%' or ubicacion LIKE '%' + @buscar + '%' or condiciones LIKE '%' + @buscar + '%' or temperatura LIKE '%' + @buscar + '%'
	order by Capacidad
	end

		if @ordenar = 'Ubicación'
	begin
	Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
	where  capacidad  LIKE '%' + @buscar + '%' or ubicacion LIKE '%' + @buscar + '%' or condiciones LIKE '%' + @buscar + '%' or temperatura LIKE '%' + @buscar + '%'
	order by ubicacion
	end

		if @ordenar = 'Temperatura'
	begin
	Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
	where  capacidad  LIKE '%' + @buscar + '%' or ubicacion LIKE '%' + @buscar + '%' or condiciones LIKE '%' + @buscar + '%' or temperatura LIKE '%' + @buscar + '%'
	order by Temperatura
	end

		if @ordenar = 'Condiciones'
	begin
	Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
	where  capacidad  LIKE '%' + @buscar + '%' or ubicacion LIKE '%' + @buscar + '%' or condiciones LIKE '%' + @buscar + '%' or temperatura LIKE '%' + @buscar + '%'
	order by Condiciones
	end


END
GO
/****** Object:  StoredProcedure [dbo].[BuscarFerti]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[BuscarFerti] 
@id int
AS
BEGIN
	
	SET NOCOUNT ON;
	 	Select idFerti, nombre, tipo, pH, impacto from Fertilizantes Where idFerti = @id
END

GO
/****** Object:  StoredProcedure [dbo].[BuscarFertilizanteFiltro]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[BuscarFertilizanteFiltro]

	@buscar varchar(40),
	@impact varchar(40),
	@ordenar varchar(40)

AS
BEGIN

	SET NOCOUNT ON;

	if @ordenar = ''
	begin
		Select idFerti, nombre, tipo, pH, impacto from Fertilizantes
	where ( nombre  LIKE '%' + @buscar + '%' or tipo LIKE '%' + @buscar + '%'
	or pH LIKE '%' + @buscar + '%') and ( impacto LIKE '%' + @impact + '%')
	order by idFerti
	end
		if @ordenar = 'Nombre'
		begin
		Select idFerti, nombre, tipo, pH, impacto from Fertilizantes
	where ( nombre  LIKE '%' + @buscar + '%' or tipo LIKE '%' + @buscar + '%'
	or pH LIKE '%' + @buscar + '%') and ( impacto LIKE '%' + @impact + '%')
	order by Nombre
	end
		if @ordenar = 'Tipo'
		begin
		Select idFerti, nombre, tipo, pH, impacto from Fertilizantes
	where ( nombre  LIKE '%' + @buscar + '%' or tipo LIKE '%' + @buscar + '%'
	or pH LIKE '%' + @buscar + '%') and ( impacto LIKE '%' + @impact + '%')
	order by tipo
	end


		if @ordenar = 'PH'
		begin
		Select idFerti, nombre, tipo, pH, impacto from Fertilizantes
	where ( nombre  LIKE '%' + @buscar + '%' or tipo LIKE '%' + @buscar + '%'
	or pH LIKE '%' + @buscar + '%') and ( impacto LIKE '%' + @impact + '%')
	order by pH
	end
		if @ordenar = 'Impacto'
		begin
		Select idFerti, nombre, tipo, pH, impacto from Fertilizantes
	where ( nombre  LIKE '%' + @buscar + '%' or tipo LIKE '%' + @buscar + '%'
	or pH LIKE '%' + @buscar + '%') and ( impacto LIKE '%' + @impact + '%')
	order by Impacto
	end
	end

	
GO
/****** Object:  StoredProcedure [dbo].[BuscarFiltrarLotes]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BuscarFiltrarLotes]
	@buscar varchar(40),
	@ordenar varchar(40)
AS
BEGIN
	SET NOCOUNT ON;

	if @ordenar = ''
	begin
	Select L.idGranja, G.nombre as nombreGranja, L.idProducto, P.nombre as nombreProducto, L.fchProduccion, L.cantidad, L.precio, L.idDeposito, D.ubicacion as ubicacionDeposito from Lotes L 
	inner join Granjas G on L.idGranja = G.idGranja 
	inner join Productos P on L.idProducto = P.idProducto
	inner join Depositos D on L.idDeposito = D.idDeposito
	where G.nombre LIKE '%' + @buscar + '%'  or P.nombre  LIKE '%' + @buscar + '%' or L.fchProduccion LIKE '%' + @buscar + '%' 
	or L.cantidad LIKE '%' + @buscar + '%' or L.precio LIKE '%' + @buscar + '%' or D.ubicacion LIKE '%' + @buscar + '%'
	order by idGranja
	end



		if @ordenar = 'Granja'
	begin
	Select L.idGranja, G.nombre as nombreGranja, L.idProducto, P.nombre as nombreProducto, L.fchProduccion, L.cantidad, L.precio, L.idDeposito, D.ubicacion as ubicacionDeposito from Lotes L 
	inner join Granjas G on L.idGranja = G.idGranja 
	inner join Productos P on L.idProducto = P.idProducto
	inner join Depositos D on L.idDeposito = D.idDeposito
	where G.nombre LIKE '%' + @buscar + '%'  or P.nombre  LIKE '%' + @buscar + '%' or L.fchProduccion LIKE '%' + @buscar + '%' 
	or L.cantidad LIKE '%' + @buscar + '%' or L.precio LIKE '%' + @buscar + '%' or D.ubicacion LIKE '%' + @buscar + '%'
	order by G.nombre
	end


		if @ordenar = 'Producto'
	begin
	Select L.idGranja, G.nombre as nombreGranja, L.idProducto, P.nombre as nombreProducto, L.fchProduccion, L.cantidad, L.precio, L.idDeposito, D.ubicacion as ubicacionDeposito from Lotes L 
	inner join Granjas G on L.idGranja = G.idGranja 
	inner join Productos P on L.idProducto = P.idProducto
	inner join Depositos D on L.idDeposito = D.idDeposito
	where G.nombre LIKE '%' + @buscar + '%'  or P.nombre  LIKE '%' + @buscar + '%' or L.fchProduccion LIKE '%' + @buscar + '%' 
	or L.cantidad LIKE '%' + @buscar + '%' or L.precio LIKE '%' + @buscar + '%' or D.ubicacion LIKE '%' + @buscar + '%'
	order by p.nombre
	end


			if @ordenar = 'Fecha de producción'
	begin
	Select L.idGranja, G.nombre as nombreGranja, L.idProducto, P.nombre as nombreProducto, L.fchProduccion, L.cantidad, L.precio, L.idDeposito, D.ubicacion as ubicacionDeposito from Lotes L 
	inner join Granjas G on L.idGranja = G.idGranja 
	inner join Productos P on L.idProducto = P.idProducto
	inner join Depositos D on L.idDeposito = D.idDeposito
	where G.nombre LIKE '%' + @buscar + '%'  or P.nombre  LIKE '%' + @buscar + '%' or L.fchProduccion LIKE '%' + @buscar + '%' 
	or L.cantidad LIKE '%' + @buscar + '%' or L.precio LIKE '%' + @buscar + '%' or D.ubicacion LIKE '%' + @buscar + '%'
	order by L.fchProduccion
	end


	
			if @ordenar = 'Cantidad de  producción'
	begin
	Select L.idGranja, G.nombre as nombreGranja, L.idProducto, P.nombre as nombreProducto, L.fchProduccion, L.cantidad, L.precio, L.idDeposito, D.ubicacion as ubicacionDeposito from Lotes L 
	inner join Granjas G on L.idGranja = G.idGranja 
	inner join Productos P on L.idProducto = P.idProducto
	inner join Depositos D on L.idDeposito = D.idDeposito
	where G.nombre LIKE '%' + @buscar + '%'  or P.nombre  LIKE '%' + @buscar + '%' or L.fchProduccion LIKE '%' + @buscar + '%' 
	or L.cantidad LIKE '%' + @buscar + '%' or L.precio LIKE '%' + @buscar + '%' or D.ubicacion LIKE '%' + @buscar + '%'
	order by L.cantidad
	end

				if @ordenar = 'Precio'
	begin
	Select L.idGranja, G.nombre as nombreGranja, L.idProducto, P.nombre as nombreProducto, L.fchProduccion, L.cantidad, L.precio, L.idDeposito, D.ubicacion as ubicacionDeposito from Lotes L 
	inner join Granjas G on L.idGranja = G.idGranja 
	inner join Productos P on L.idProducto = P.idProducto
	inner join Depositos D on L.idDeposito = D.idDeposito
	where G.nombre LIKE '%' + @buscar + '%'  or P.nombre  LIKE '%' + @buscar + '%' or L.fchProduccion LIKE '%' + @buscar + '%' 
	or L.cantidad LIKE '%' + @buscar + '%' or L.precio LIKE '%' + @buscar + '%' or D.ubicacion LIKE '%' + @buscar + '%'
	order by L.precio
	end


					if @ordenar = 'Depósito'
	begin
	Select L.idGranja, G.nombre as nombreGranja, L.idProducto, P.nombre as nombreProducto, L.fchProduccion, L.cantidad, L.precio, L.idDeposito, D.ubicacion as ubicacionDeposito from Lotes L 
	inner join Granjas G on L.idGranja = G.idGranja 
	inner join Productos P on L.idProducto = P.idProducto
	inner join Depositos D on L.idDeposito = D.idDeposito
	where G.nombre LIKE '%' + @buscar + '%'  or P.nombre  LIKE '%' + @buscar + '%' or L.fchProduccion LIKE '%' + @buscar + '%' 
	or L.cantidad LIKE '%' + @buscar + '%' or L.precio LIKE '%' + @buscar + '%' or D.ubicacion LIKE '%' + @buscar + '%'
	order by D.ubicacion
	end
END

/****** Object:  StoredProcedure [dbo].[LstLotes]    Script Date: 15/8/2023 23:59:26 ******/
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[BuscarFiltroCam]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BuscarFiltroCam]
	@buscar varchar(40),
	@disp varchar(40),
	@ordenar varchar(40)
AS
BEGIN
	SET NOCOUNT ON;

	if @ordenar = ''
	Select idCamion, marca, modelo, carga, disponible from Camiones
	Where (marca  LIKE '%' + @buscar + '%'  or modelo  LIKE '%' + @buscar + '%' or carga LIKE '%' + @buscar + '%') and (disponible LIKE   @disp + '%')
	order by idCamion


	if @ordenar = 'Marca'
	Select idCamion, marca, modelo, carga, disponible from Camiones
	Where (marca  LIKE '%' + @buscar + '%'  or modelo  LIKE '%' + @buscar + '%' or carga LIKE '%' + @buscar + '%') and (disponible LIKE   @disp + '%')
	order by marca


	if @ordenar = 'Modelo'
	Select idCamion, marca, modelo, carga, disponible from Camiones
	Where (marca  LIKE '%' + @buscar + '%'  or modelo  LIKE '%' + @buscar + '%' or carga LIKE '%' + @buscar + '%') and (disponible LIKE   @disp + '%')
	order by modelo


	if @ordenar = 'Carga'
	Select idCamion, marca, modelo, carga, disponible from Camiones
	Where (marca  LIKE '%' + @buscar + '%'  or modelo  LIKE '%' + @buscar + '%' or carga LIKE '%' + @buscar + '%') and (disponible LIKE   @disp + '%')
	order by carga

		if @ordenar = 'Disponible'
	Select idCamion, marca, modelo, carga, disponible from Camiones
	Where (marca  LIKE '%' + @buscar + '%'  or modelo  LIKE '%' + @buscar + '%' or carga LIKE '%' + @buscar + '%') and (disponible LIKE   @disp + '%')
	order by disponible

END
GO
/****** Object:  StoredProcedure [dbo].[BuscarGranja]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BuscarGranja] 
@id int
AS
BEGIN
	SET NOCOUNT ON;
	Select idGranja, nombre, ubicacion, idCliente from Granjas Where idGranja = @id
END

GO
/****** Object:  StoredProcedure [dbo].[BuscarGranjaFiltro]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BuscarGranjaFiltro]
	@buscar varchar(40),
	@orden varchar(40)
AS
BEGIN
	SET NOCOUNT ON;

	if @orden ='' or @orden ='Nombre del dueño'
	begin
	Select idGranja, nombre, ubicacion, idCliente from Granjas
	where idGranja LIKE '%' + @buscar + '%'  or nombre  LIKE '%' + @buscar + '%' or ubicacion LIKE '%' + @buscar + '%' or idCliente LIKE '%' + @buscar + '%'
	order by idGranja
	end


	
	if @orden ='Nombre'
	begin
	Select idGranja, nombre, ubicacion, idCliente from Granjas
	where idGranja LIKE '%' + @buscar + '%'  or nombre  LIKE '%' + @buscar + '%' or ubicacion LIKE '%' + @buscar + '%' or idCliente LIKE '%' + @buscar + '%'
	order by nombre
	end

	if @orden ='Ubicación'
	begin
	Select idGranja, nombre, ubicacion, idCliente from Granjas
	where idGranja LIKE '%' + @buscar + '%'  or nombre  LIKE '%' + @buscar + '%' or ubicacion LIKE '%' + @buscar + '%' or idCliente LIKE '%' + @buscar + '%'
	order by ubicacion
	end


END
GO
/****** Object:  StoredProcedure [dbo].[BuscarLote]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BuscarLote] 
	@nombreGranja varchar(30),
	@nombreProducto varchar(30),
	@fchProduccion varchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	Select L.idGranja, G.nombre as nombreGranja, L.idProducto, P.nombre as nombreProducto, L.fchProduccion, L.cantidad, L.precio, L.idDeposito, D.ubicacion as ubicacionDeposito from Lotes L 
	inner join Granjas G on L.idGranja = G.idGranja 
	inner join Productos P on L.idProducto = P.idProducto
	inner join Depositos D on L.idDeposito = D.idDeposito
	Where G.nombre = @nombreGranja and P.nombre = @nombreProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103)
END

/****** Object:  StoredProcedure [dbo].[BuscarVarLote]    Script Date: 15/8/2023 23:58:55 ******/
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[BuscarLote_Ferti]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BuscarLote_Ferti] 
	@idFertilizante int,
	@idGranja int,
	@idProducto int,
	@fchProduccion varchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	Select idFertilizante, idGranja, idProducto, fchProduccion, cantidad from Lotes_Fertis
	Where idFertilizante = @idFertilizante and idGranja = @idGranja and idProducto = @idProducto and fchProduccion = CONVERT(DATE,@fchProduccion,103)
END
GO
/****** Object:  StoredProcedure [dbo].[BuscarLote_Pesti]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BuscarLote_Pesti] 
	@idPesticida int,
	@idGranja int,
	@idProducto int,
	@fchProduccion varchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	Select idPesticida, idGranja, idProducto, fchProduccion, cantidad from Lotes_Pestis
	Where idPesticida = @idPesticida and idGranja = @idGranja and idProducto = @idProducto and fchProduccion = CONVERT(DATE,@fchProduccion,103)
END
GO
/****** Object:  StoredProcedure [dbo].[BuscarPesti]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE PROCEDURE [dbo].[BuscarPesti] 
@id int
AS
BEGIN
	
	SET NOCOUNT ON;
	 	Select idPesti, nombre, tipo, pH, impacto from Pesticidas Where idPesti = @id
END

GO
/****** Object:  StoredProcedure [dbo].[BuscarPesticidaFiltro]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[BuscarPesticidaFiltro]

	@buscar varchar(40),
	@impact varchar(40),
	@ordenar varchar(40)

AS
BEGIN

	SET NOCOUNT ON;

	if @ordenar = ''
	begin
		Select idPesti, nombre, tipo, pH, impacto from Pesticidas
	where ( nombre  LIKE '%' + @buscar + '%' or tipo LIKE '%' + @buscar + '%'
	or pH LIKE '%' + @buscar + '%') and ( impacto LIKE '%' + @impact + '%')
	order by idPesti
	end
		if @ordenar = 'Nombre'
		begin
		Select idPesti, nombre, tipo, pH, impacto from Pesticidas
	where ( nombre  LIKE '%' + @buscar + '%' or tipo LIKE '%' + @buscar + '%'
	or pH LIKE '%' + @buscar + '%') and ( impacto LIKE '%' + @impact + '%')
	order by Nombre
	end
		if @ordenar = 'Tipo'
		begin
		Select idPesti, nombre, tipo, pH, impacto from Pesticidas
	where ( nombre  LIKE '%' + @buscar + '%' or tipo LIKE '%' + @buscar + '%'
	or pH LIKE '%' + @buscar + '%') and ( impacto LIKE '%' + @impact + '%')
	order by tipo
	end


		if @ordenar = 'PH'
		begin
		Select idPesti, nombre, tipo, pH, impacto from Pesticidas
	where ( nombre  LIKE '%' + @buscar + '%' or tipo LIKE '%' + @buscar + '%'
	or pH LIKE '%' + @buscar + '%') and ( impacto LIKE '%' + @impact + '%')
	order by pH
	end
		if @ordenar = 'Impacto'
		begin
		Select idPesti, nombre, tipo, pH, impacto from Pesticidas
	where ( nombre  LIKE '%' + @buscar + '%' or tipo LIKE '%' + @buscar + '%'
	or pH LIKE '%' + @buscar + '%') and ( impacto LIKE '%' + @impact + '%')
	order by Impacto
	end
	end

	
GO
/****** Object:  StoredProcedure [dbo].[BuscarProducto]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[BuscarProducto] 
@id int
AS
BEGIN
	
	SET NOCOUNT ON;
	 	Select idProducto, nombre, tipo, tipoVenta, imagen, CantTotal from Productos Where idProducto = @id
END

GO
/****** Object:  StoredProcedure [dbo].[BuscarProductoFiltro]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BuscarProductoFiltro] 
@buscar varchar(40),
@tipo varchar(40),
@tipoVen varchar(40),
@ordenar varchar(40)
AS
BEGIN
	if @ordenar = 'Nombre'
		begin
		Select idProducto, nombre, tipo, tipoVenta, imagen, cantTotal from Productos Where nombre LIKE '%' + @buscar + '%' and (tipo LIKE   @tipo + '%') and ( tipoVenta LIKE   @tipoVen + '%')
		order by nombre
		end
	if @ordenar = 'Tipo'
		begin
		Select idProducto, nombre, tipo, tipoVenta, imagen, cantTotal from Productos Where nombre LIKE '%' + @buscar + '%' and (tipo LIKE   @tipo + '%') and ( tipoVenta LIKE   @tipoVen + '%')
		order by tipo
		end
		if @ordenar = 'Tipo de venta'
		begin
		Select idProducto, nombre, tipo, tipoVenta, imagen, cantTotal from Productos Where nombre LIKE '%' + @buscar + '%' and (tipo LIKE   @tipo + '%') and ( tipoVenta LIKE   @tipoVen + '%')
		order by tipoVenta
		end
else 
		begin
		Select idProducto, nombre, tipo, tipoVenta, imagen, cantTotal from Productos Where nombre LIKE '%' + @buscar + '%' and (tipo LIKE   @tipo + '%') and ( tipoVenta LIKE   @tipoVen + '%')
		order by idProducto
		end
END

GO
/****** Object:  StoredProcedure [dbo].[IniciarSesionAdm]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[IniciarSesionAdm]
	-- Add the parameters for the stored procedure here
	@User varchar(40),
	@Pass varchar(1000)
As
BEGIN
	

		Select idAdmin  from Admins
		where usuario = @User and contrasena = @Pass 
		and estado = 'Habilitado'
END
GO
/****** Object:  StoredProcedure [dbo].[IniciarSesionCli]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[IniciarSesionCli]
	-- Add the parameters for the stored procedure here
	@User varchar(40),
	@Pass varchar(1000)
As
BEGIN
	

		Select idCliente  from Clientes
		where usuario = @User and contrasena = @Pass
END
GO
/****** Object:  StoredProcedure [dbo].[LstFertisEnLote]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LstFertisEnLote]
	@idGranja int,
	@idProducto int,
	@fchProduccion varchar(20)
as
BEGIN

	SET NOCOUNT ON;
	Select F.idFerti, F.nombre, F.tipo, L.cantidad from Fertilizantes F inner join Lotes_Fertis L on L.idFertilizante = F.idFerti 
	where L.idGranja = @idGranja and L.idProducto = @idProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103)
	order by F.idFerti
END
GO
/****** Object:  StoredProcedure [dbo].[LstIdPersonas]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[LstIdPersonas]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select idPersona from Personas
END
GO
/****** Object:  StoredProcedure [dbo].[LstPestisEnLote]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LstPestisEnLote]
	@idGranja int,
	@idProducto int,
	@fchProduccion varchar(20)
as
BEGIN

	SET NOCOUNT ON;
	Select P.idPesti, P.nombre, P.tipo, L.cantidad from Pesticidas P inner join Lotes_Pestis L on L.idPesticida = P.idPesti
	where L.idGranja = @idGranja and L.idProducto = @idProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103)
 order by P.idPesti

END

select * from Lotes_Pestis
GO
/****** Object:  StoredProcedure [dbo].[ModificarAdm]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ModificarAdm]
	@id int,
	@nombre varchar(40),
	@apellido varchar(40),
	@fchNac varchar (40),
	
	@TipoAdm varchar(40),
	@estado varchar(40)
AS
BEGIN
    UPDATE Admins SET tipoDeAdmin = @TipoAdm, estado = @estado  where idAdmin = @id
	
		UPDATE Personas SET nombre = @nombre, apellido = @apellido, fchNacimiento = @fchNac where idPersona = @id

END
GO
/****** Object:  StoredProcedure [dbo].[ModificarCam]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ModificarCam]
	@id int,
	@marca varchar(40),
	@modelo varchar(40),
	@carga numeric(10,2),
	@disponible varchar (40)

AS
BEGIN
    UPDATE Camiones SET idCamion = @id, marca = @marca, modelo = @modelo, carga = @carga, disponible = @disponible where idCamion = @id



END
GO
/****** Object:  StoredProcedure [dbo].[ModificarCamionero]    Script Date: 29/8/2023 22:53:22 ******/
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
    UPDATE Camioneros SET cedula = @cedula, disponible = @disp, fchManejo = @manejo where idCamionero = @id

		UPDATE Personas SET nombre = @nombre, apellido = @apellido, email = @email, telefono = @tele, fchNacimiento =@fchNac where idPersona = @id

END
GO
/****** Object:  StoredProcedure [dbo].[ModificarCli]    Script Date: 29/8/2023 22:53:22 ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarDeposito]    Script Date: 29/8/2023 22:53:22 ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarFerti]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[ModificarFerti]
	@id int,
	@nombre varchar(40),
	@tipo varchar(40),
	@pH numeric(4,2),
	@impacto varchar(30)

AS
BEGIN
    UPDATE Fertilizantes SET nombre = @nombre, tipo = @tipo, pH = @pH, impacto = @impacto  where idFerti = @id
END
GO
/****** Object:  StoredProcedure [dbo].[ModificarGranja]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModificarGranja]
	@id int,
	@nombre varchar(30),
	@ubicacion varchar(50),
	@idCliente int
AS
BEGIN
    UPDATE Granjas SET nombre = @nombre, ubicacion = @ubicacion, idCliente = @idCliente where idGranja = @id
END
GO
/****** Object:  StoredProcedure [dbo].[ModificarLote]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModificarLote]
	@idGranja int,
	@idProducto int,
	@fchProduccion varchar(20),
	@cantidad varchar(40),
	@precio numeric(10,2),
	@idDeposito int,
	@cantTotal varchar(40)
AS
BEGIN
    UPDATE Lotes SET cantidad = @cantidad, precio = @precio, idDeposito = @idDeposito 
	where idGranja = @idGranja and idProducto = @idProducto and fchProduccion = CONVERT(DATE,@fchProduccion,103)
		if @@ROWCOUNT > 0
	begin
		 Update Productos set CantTotal = @cantTotal 
	 where idProducto = @idProducto
	end
END
GO
/****** Object:  StoredProcedure [dbo].[ModificarLoteFerti]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModificarLoteFerti]
	@idFertilizante int,
	@idGranja int,
	@idProducto int,
	@fchProduccion varchar(20),
	@cantidad varchar(30)
AS
BEGIN
    UPDATE Lotes_Fertis SET cantidad = @cantidad
	where idFertilizante = @idFertilizante and idGranja = @idGranja and idProducto = @idProducto and fchProduccion = CONVERT(DATE,@fchProduccion,103)
END
GO
/****** Object:  StoredProcedure [dbo].[ModificarLotePesti]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModificarLotePesti]
	@idPesticida int,
	@idGranja int,
	@idProducto int,
	@fchProduccion varchar(20),
	@cantidad varchar(30)
AS
BEGIN
    UPDATE Lotes_Pestis SET cantidad = @cantidad
	where idPesticida = @idPesticida and idGranja = @idGranja and idProducto = @idProducto and fchProduccion = CONVERT(DATE,@fchProduccion,103)
END
GO
/****** Object:  StoredProcedure [dbo].[ModificarPesti]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[ModificarPesti]
	@id int,
	@nombre varchar(40),
	@tipo varchar(40),
	@pH numeric(4,2),
	@impacto varchar(30)

AS
BEGIN
    UPDATE Pesticidas SET nombre = @nombre, tipo = @tipo, pH = @pH, impacto = @impacto  where idPesti = @id
END
GO
/****** Object:  StoredProcedure [dbo].[ModificarProducto]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModificarProducto]
	@id int,
	@nombre varchar(30),
	@tipo varchar(30),
	@tipoVenta varchar(15),
	@imagen varchar(MAX)
AS
BEGIN
    UPDATE Productos SET nombre = @nombre, tipo = @tipo, tipoVenta = @tipoVenta, imagen = @imagen where idProducto = @id
END
GO
/****** Object:  StoredProcedure [dbo].[userRepetidoAdm]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[userRepetidoAdm]

AS
BEGIN

select usuario from Admins 


END
GO
/****** Object:  StoredProcedure [dbo].[userRepetidoCli]    Script Date: 29/8/2023 22:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[userRepetidoCli]

AS
BEGIN

select usuario from Clientes


END
GO
