USE [Proyecto]
GO
/****** Object:  StoredProcedure [dbo].[AltaAdmin]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[AltaCam]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[AltaCamionero]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[AltaCli]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[AltaDeposito]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[AltaFerti]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[AltaGranja]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[AltaLote]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[AltaLoteFerti]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[AltaLotePesti]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[AltaPedido]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AltaPedido] 
	-- Add the parameters for the stored procedure here
	@IdPedido int,
	@IdCliente int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into Pedidos (idPedido, idCliente, fchPedido,estado, costo)
	values (@IdPedido, @IdCliente,(CONVERT(date,getdate())), 'Sin finalizar' ,0)
	


	
END
GO
/****** Object:  StoredProcedure [dbo].[AltaPedido_Prod]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AltaPedido_Prod] 
	-- Add the parameters for the stored procedure here
	@IdPedido int,
	@IdProducto int,
	@Cantidad varchar(40),
	@CantidadRes varchar(40),
	@precio numeric(10,2)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	begin tran

	insert into Pedidos_Prod (idPedido, idProducto, cantidad)
	Values (@IdPedido, @IdProducto, @Cantidad) 
		if  @@ROWCOUNT>0   
	begin 

	Update Productos set cantRes = @CantidadRes
	where idProducto = @IdProducto 
	

	Update Pedidos set costo = @precio + costo
	where idPedido = @IdPedido

	end


	If not exists (Select * From Pedidos_Prod Where idPedido = @IdPedido)
	Begin
	raiserror('No se puedo crear el pedido del cliente',1,1);
		rollback;
	End

		Commit tran
	

	
END
GO
/****** Object:  StoredProcedure [dbo].[AltaPesti]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[AltaProducto]    Script Date: 11/9/2023 19:03:13 ******/
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
	insert into Productos values (@id, @nombre, @tipo, @tipoVenta, @imagen, 0,0) 
	If not exists (Select * From Productos Where idProducto = @id)
	Begin
	raiserror('No se pudo crear el Producto',1,1);
		rollback;
	End
Commit tran
END
GO
/****** Object:  StoredProcedure [dbo].[BajaAdmin]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BajaCam]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BajaCamionero]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BajaCli]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BajaDeposito]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BajaFerti]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BajaGranja]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BajaLote]    Script Date: 11/9/2023 19:03:13 ******/
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
		where G.nombre = @nombreGranja and P.nombre = @nombreProducto and  L.fchProduccion =  CONVERT(DATE,@fchProduccion,103)



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
/****** Object:  StoredProcedure [dbo].[BajaLoteFerti]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BajaLotePesti]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BajaPedido]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[BajaPedido]

@idPedido int
AS
BEGIN

	SET NOCOUNT ON;
	Begin  Tran



	Delete from Pedidos
	Where idPedido = @idPedido

	if exists (Select * from Pedidos where idPedido = @idPedido) 
	or exists (Select * from Pedidos_Prod where idPedido = @idPedido) 
		begin
		raiserror('No se pudo borrar el Lote',1,1);
			rollback;
		end
	else
		begin
	
			Commit Tran
		end


END
GO
/****** Object:  StoredProcedure [dbo].[BajaPedidoProd]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[BajaPedidoProd]

@idPedido int, 
@idProducto int,

@cantRess varchar(40),
@precio numeric(10,2)

AS
BEGIN
	
	delete from Pedidos_Prod 
	where idPedido =@idPedido and idProducto = @idProducto

		if  @@ROWCOUNT>0   
	begin 

	Update Productos set cantRes = @cantRess
	where idProducto = @IdProducto 
	

	Update Pedidos set costo = @precio 
	where idPedido = @IdPedido

	end


END
GO
/****** Object:  StoredProcedure [dbo].[BajaPesti]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BajaProducto]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarAdm]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarAdminFiltro]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[BuscarAdminFiltro]
	@nombre varchar(40),
	@apellido varchar(40),
	@email varchar(40),
	@telefono varchar(40),
	@usuario varchar(40),
	@tipoAdmin varchar(40),
	@estado varchar(40),
	@fchDesde varchar(40),
	@fchHasta varchar(40),
	@ordenar varchar(40)
AS
BEGIN

	if(@ordenar ='')
	begin
		if(@fchDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like '%' + @tipoAdmin + '%' and  estado LIKE @estado + '%')
			order by  P.idPersona
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like '%' + @tipoAdmin + '%' and  estado LIKE @estado + '%'
			and (fchNacimiento >= @fchDesde and fchNacimiento <= @fchHasta))
			order by  P.idPersona
		end
	end


	if(@ordenar ='Nombre')
	begin
		if(@fchDesde = '')
		begin 
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like '%' + @tipoAdmin + '%' and  estado LIKE @estado + '%')
			order by  P.nombre
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like '%' + @tipoAdmin + '%' and  estado LIKE @estado + '%'
			and (fchNacimiento >= @fchDesde and fchNacimiento <= @fchHasta))
			order by  P.nombre
		end
	end

	if(@ordenar ='Apellido')
	begin
		if(@fchDesde = '')
		begin 
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like '%' + @tipoAdmin + '%' and  estado LIKE @estado + '%')
			order by  P.apellido
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like '%' + @tipoAdmin + '%' and  estado LIKE @estado + '%'
			and (fchNacimiento >= @fchDesde and fchNacimiento <= @fchHasta))
			order by  P.apellido
		end
	end

	if(@ordenar ='E-Mail')
	begin
		if(@fchDesde = '')
		begin 
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like '%' + @tipoAdmin + '%' and  estado LIKE @estado + '%')
			order by  P.email
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like '%' + @tipoAdmin + '%' and  estado LIKE @estado + '%'
			and (fchNacimiento >= @fchDesde and fchNacimiento <= @fchHasta))
			order by  P.email
		end
	end


	if(@ordenar ='Teléfono')
	begin
		if(@fchDesde = '')
		begin 
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like '%' + @tipoAdmin + '%' and  estado LIKE @estado + '%')
			order by  P.telefono
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like'%' +  @tipoAdmin + '%' and  estado LIKE @estado + '%'
			and (fchNacimiento >= @fchDesde and fchNacimiento <= @fchHasta))
			order by  P.telefono
		end
	end

	if(@ordenar ='Fecha de Nacimiento')
	begin
		if(@fchDesde = '')
		begin 
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like '%' + @tipoAdmin + '%' and  estado LIKE @estado + '%')
			order by  P.fchNacimiento
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like '%' + @tipoAdmin + '%' and  estado LIKE @estado + '%'
			and (fchNacimiento >= @fchDesde and fchNacimiento <= @fchHasta))
			order by  P.fchNacimiento
		end
	end

	if(@ordenar ='Usuario')
	begin
		if(@fchDesde = '')
		begin 
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like '%' + @tipoAdmin + '%' and  estado LIKE @estado + '%')
			order by  A.usuario
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like '%' + @tipoAdmin + '%' and  estado LIKE @estado + '%'
			and (fchNacimiento >= @fchDesde and fchNacimiento <= @fchHasta))
			order by  A.usuario
		end
	end

	if(@ordenar ='Tipo de Admin')
	begin
		if(@fchDesde = '')
		begin 
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like '%' + @tipoAdmin + '%' and  estado LIKE @estado + '%')
			order by  A.tipoDeAdmin
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like '%' + @tipoAdmin + '%' and  estado LIKE @estado + '%'
			and (fchNacimiento >= @fchDesde and fchNacimiento <= @fchHasta))
			order by  A.tipoDeAdmin
		end
	end

	if(@ordenar ='Estado')
	begin
		if(@fchDesde = '')
		begin 
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like '%' + @tipoAdmin + '%' and  estado LIKE @estado + '%')
			order by  A.estado
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  A.usuario, A.tipoDeAdmin,  A.estado from Personas P inner join Admins A on P.idPersona = A.idAdmin
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and tipoDeAdmin like '%' + @tipoAdmin + '%' and  estado LIKE @estado + '%'
			and (fchNacimiento >= @fchDesde and fchNacimiento <= @fchHasta))
			order by  A.estado
		end
	end
END


/****** Object:  StoredProcedure [dbo].[BuscarCamioneroFiltro]    Script Date: 9/9/2023 4:06:58 ******/
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[BuscarCam]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarCamionero]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarCamioneroFiltro]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[BuscarCamioneroFiltro]
	@nombre varchar(40),
	@apellido varchar(40),
	@email varchar(40),
	@telefono varchar(40),
	@cedula varchar(40),
	@disponible varchar(40),
	@fchNacDesde varchar(40),
	@fchNacHasta varchar(40),
	@fchVencDesde varchar(40),
	@fchVencHasta varchar(40),
	@ordenar varchar(40)
AS
BEGIN

	if(@ordenar ='')
	begin
		if(@fchNacDesde = '' and @fchVencDesde = '')
		begin 
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%')
			order by  P.idPersona
		end
		if(@fchNacDesde != '' and @fchVencDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta))
			order by  P.idPersona
		end
		if(@fchNacDesde = '' and @fchVencDesde != '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  P.idPersona
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta) and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  P.idPersona
		end
	end


	if(@ordenar ='Nombre')
	begin
		if(@fchNacDesde = '' and @fchVencDesde = '')
		begin 
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%')
			order by  P.nombre
		end
		if(@fchNacDesde != '' and @fchVencDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta))
			order by  P.nombre
		end
		if(@fchNacDesde = '' and @fchVencDesde != '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  P.nombre
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta) and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  P.nombre
		end
	end

	if(@ordenar ='Apellido')
	begin
		if(@fchNacDesde = '' and @fchVencDesde = '')
		begin 
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%')
			order by  P.apellido
		end
		if(@fchNacDesde != '' and @fchVencDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta))
			order by  P.apellido
		end
		if(@fchNacDesde = '' and @fchVencDesde != '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  P.apellido
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta) and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  P.apellido
		end
	end

	if(@ordenar ='E-Mail')
	begin
		if(@fchNacDesde = '' and @fchVencDesde = '')
		begin 
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%')
			order by  P.email
		end
		if(@fchNacDesde != '' and @fchVencDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta))
			order by  P.email
		end
		if(@fchNacDesde = '' and @fchVencDesde != '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  P.email
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta) and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  P.email
		end
	end


	if(@ordenar ='Teléfono')
	begin
		if(@fchNacDesde = '' and @fchVencDesde = '')
		begin 
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%')
			order by  P.telefono
		end
		if(@fchNacDesde != '' and @fchVencDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta))
			order by  P.telefono
		end
		if(@fchNacDesde = '' and @fchVencDesde != '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  P.telefono
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta) and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  P.telefono
		end
	end

	if(@ordenar ='Fecha de Nacimiento')
	begin
		if(@fchNacDesde = '' and @fchVencDesde = '')
		begin 
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%')
			order by  P.fchNacimiento
		end
		if(@fchNacDesde != '' and @fchVencDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta))
			order by  P.fchNacimiento
		end
		if(@fchNacDesde = '' and @fchVencDesde != '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  P.fchNacimiento
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta) and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  P.fchNacimiento
		end
	end

	if(@ordenar ='Cedula')
	begin
		if(@fchNacDesde = '' and @fchVencDesde = '')
		begin 
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%')
			order by  C.cedula
		end
		if(@fchNacDesde != '' and @fchVencDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta))
			order by  C.cedula
		end
		if(@fchNacDesde = '' and @fchVencDesde != '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  C.cedula
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta) and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  C.cedula
		end
	end

	if(@ordenar ='Disponible')
	begin
		if(@fchNacDesde = '' and @fchVencDesde = '')
		begin 
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%')
			order by  C.disponible
		end
		if(@fchNacDesde != '' and @fchVencDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta))
			order by  C.disponible
		end
		if(@fchNacDesde = '' and @fchVencDesde != '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  C.disponible
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta) and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  C.disponible
		end
	end

	if(@ordenar ='Vencimiento de libreta')
	begin
		if(@fchNacDesde = '' and @fchVencDesde = '')
		begin 
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%')
			order by  C.fchManejo
		end
		if(@fchNacDesde != '' and @fchVencDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta))
			order by  C.fchManejo
		end
		if(@fchNacDesde = '' and @fchVencDesde != '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  C.fchManejo
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.cedula, C.disponible, C.fchManejo from Personas P inner join Camioneros C on P.idPersona = C.idCamionero
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and cedula LIKE '%' + @cedula + '%' and disponible like '%' + @disponible + '%'
			and (fchNacimiento >= @fchNacDesde and fchNacimiento <= @fchNacHasta) and (fchManejo >= @fchVencDesde and fchManejo <= @fchVencHasta))
			order by  C.fchManejo
		end
	end
END


/****** Object:  StoredProcedure [dbo].[BuscarCliFiltro]    Script Date: 9/9/2023 4:07:19 ******/
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[BuscarCli]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarCliFiltro]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[BuscarCliFiltro]
	@nombre varchar(40),
	@apellido varchar(40),
	@email varchar(40),
	@telefono varchar(40),
	@usuario varchar(40),
	@direccion varchar(60),
	@fchDesde varchar(40),
	@fchHasta varchar(40),
	@ordenar varchar(40)
AS
BEGIN

	if(@ordenar ='')
	begin
		if(@fchDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and Direccion like '%' + @direccion + '%')
			order by  P.idPersona
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and Direccion like '%' + @direccion + '%'
			and (fchNacimiento >= @fchDesde and fchNacimiento <= @fchHasta))
			order by  P.idPersona
		end
	end


	if(@ordenar ='Nombre')
	begin
		if(@fchDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and Direccion like '%' + @direccion + '%')
			order by  P.nombre
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and Direccion like '%' + @direccion + '%'
			and (fchNacimiento >= @fchDesde and fchNacimiento <= @fchHasta))
			order by  P.nombre
		end
	end

	if(@ordenar ='Apellido')
	begin
		if(@fchDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and Direccion like '%' + @direccion + '%')
			order by  P.apellido
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and Direccion like '%' + @direccion + '%'
			and (fchNacimiento >= @fchDesde and fchNacimiento <= @fchHasta))
			order by  P.apellido
		end
	end

	if(@ordenar ='E-Mail')
	begin
		if(@fchDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and Direccion like '%' + @direccion + '%')
			order by  P.email
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and Direccion like '%' + @direccion + '%'
			and (fchNacimiento >= @fchDesde and fchNacimiento <= @fchHasta))
			order by  P.email
		end
	end


	if(@ordenar ='Teléfono')
	begin
		if(@fchDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and Direccion like '%' + @direccion + '%')
			order by  P.telefono
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and Direccion like '%' + @direccion + '%'
			and (fchNacimiento >= @fchDesde and fchNacimiento <= @fchHasta))
			order by  P.telefono
		end
	end

	if(@ordenar ='Fecha de Nacimiento')
	begin
		if(@fchDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and Direccion like '%' + @direccion + '%')
			order by  P.fchNacimiento
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and Direccion like '%' + @direccion + '%'
			and (fchNacimiento >= @fchDesde and fchNacimiento <= @fchHasta))
			order by  P.fchNacimiento
		end
	end

	if(@ordenar ='Usuario')
	begin
		if(@fchDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and Direccion like '%' + @direccion + '%')
			order by  C.usuario
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and Direccion like '%' + @direccion + '%'
			and (fchNacimiento >= @fchDesde and fchNacimiento <= @fchHasta))
			order by  C.usuario
		end
	end

	if(@ordenar ='Dirección')
	begin
		if(@fchDesde = '')
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and Direccion like '%' + @direccion + '%')
			order by  C.Direccion
		end
		else
		begin
			Select P.idPersona, P.nombre, P.apellido, P.email, P.telefono, P.fchNacimiento,  C.usuario, C.Direccion from Personas P inner join Clientes C on P.idPersona = C.idCliente
			where  (nombre LIKE '%' + @nombre + '%' and apellido LIKE '%' + @apellido +  '%' and email LIKE '%' + @email + '%' and 
			telefono LIKE '%' + @telefono + '%' and usuario LIKE '%' + @usuario + '%' and Direccion like '%' + @direccion + '%'
			and (fchNacimiento >= @fchDesde and fchNacimiento <= @fchHasta))
			order by  C.Direccion
		end
	end
END


/****** Object:  StoredProcedure [dbo].[BuscarDepositoFiltro]    Script Date: 9/9/2023 4:07:47 ******/
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[BuscarDeposito]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarDepositoFiltro]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[BuscarDepositoFiltro]
	@ubicacion varchar(40),
	@condiciones varchar(60),
	@capacidadMenor numeric(10,0),
	@capacidadMayor numeric(10,0),
	@temperaturaMenor numeric(3,0),
	@temperaturaMayor numeric(3,0),
	@ordenar varchar(40)
AS
BEGIN

	SET NOCOUNT ON;

	if @ordenar = ''
	begin
		if(@capacidadMenor = 0 and @temperaturaMenor = -1)
		begin 
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%'
			order by idDeposito
		end
		if(@capacidadMenor != 0 and @temperaturaMenor = -1)
		begin 
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%' and (capacidad >= @capacidadMenor and capacidad <= @capacidadMayor)
			order by idDeposito
		end
		if(@capacidadMenor = 0 and @temperaturaMenor != -1)
		begin
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%' and (temperatura >= @temperaturaMenor and temperatura <= @temperaturaMayor)
			order by idDeposito
		end
		else
		begin
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%' and (capacidad >= @capacidadMenor and capacidad <= @capacidadMayor)
			and (temperatura >= @temperaturaMenor and temperatura <= @temperaturaMayor)
			order by idDeposito
		end
	end

	if @ordenar = 'Condiciones'
	begin
		if(@capacidadMenor = 0 and @temperaturaMenor = -1)
		begin 
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%'
			order by condiciones
		end
		if(@capacidadMenor != 0 and @temperaturaMenor = -1)
		begin 
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%' and (capacidad >= @capacidadMenor and capacidad <= @capacidadMayor)
			order by condiciones
		end
		if(@capacidadMenor = 0 and @temperaturaMenor != -1)
		begin
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%' and (temperatura >= @temperaturaMenor and temperatura <= @temperaturaMayor)
			order by condiciones
		end
		else
		begin
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%' and (capacidad >= @capacidadMenor and capacidad <= @capacidadMayor)
			and (temperatura >= @temperaturaMenor and temperatura <= @temperaturaMayor)
			order by condiciones
		end
	end

	if @ordenar = 'Ubicación'
	begin
		if(@capacidadMenor = 0 and @temperaturaMenor = -1)
		begin 
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%'
			order by ubicacion
		end
		if(@capacidadMenor != 0 and @temperaturaMenor = -1)
		begin 
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%' and (capacidad >= @capacidadMenor and capacidad <= @capacidadMayor)
			order by ubicacion
		end
		if(@capacidadMenor = 0 and @temperaturaMenor != -1)
		begin
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%' and (temperatura >= @temperaturaMenor and temperatura <= @temperaturaMayor)
			order by ubicacion
		end
		else
		begin
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%' and (capacidad >= @capacidadMenor and capacidad <= @capacidadMayor)
			and (temperatura >= @temperaturaMenor and temperatura <= @temperaturaMayor)
			order by ubicacion
		end
	end

	if @ordenar = 'Capacidad'
	begin
		if(@capacidadMenor = 0 and @temperaturaMenor = -1)
		begin 
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%'
			order by capacidad
		end
		if(@capacidadMenor != 0 and @temperaturaMenor = -1)
		begin 
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%' and (capacidad >= @capacidadMenor and capacidad <= @capacidadMayor)
			order by capacidad
		end
		if(@capacidadMenor = 0 and @temperaturaMenor != -1)
		begin
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%' and (temperatura >= @temperaturaMenor and temperatura <= @temperaturaMayor)
			order by capacidad
		end
		else
		begin
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%' and (capacidad >= @capacidadMenor and capacidad <= @capacidadMayor)
			and (temperatura >= @temperaturaMenor and temperatura <= @temperaturaMayor)
			order by capacidad
		end
	end

	if @ordenar = 'Temperatura'
	begin
		if(@capacidadMenor = 0 and @temperaturaMenor = -1)
		begin 
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%'
			order by temperatura
		end
		if(@capacidadMenor != 0 and @temperaturaMenor = -1)
		begin 
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%' and (capacidad >= @capacidadMenor and capacidad <= @capacidadMayor)
			order by temperatura
		end
		if(@capacidadMenor = 0 and @temperaturaMenor != -1)
		begin
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%' and (temperatura >= @temperaturaMenor and temperatura <= @temperaturaMayor)
			order by temperatura
		end
		else
		begin
			Select idDeposito, capacidad, ubicacion, temperatura, condiciones from Depositos
			where  ubicacion LIKE '%' + @ubicacion + '%' and condiciones LIKE '%' + @condiciones + '%' and (capacidad >= @capacidadMenor and capacidad <= @capacidadMayor)
			and (temperatura >= @temperaturaMenor and temperatura <= @temperaturaMayor)
			order by temperatura
		end
	end
END


/****** Object:  StoredProcedure [dbo].[BuscarFertilizanteFiltro]    Script Date: 9/9/2023 4:08:00 ******/
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[BuscarFerti]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarFertilizanteFiltro]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[BuscarFertilizanteFiltro]
	@nombre varchar(40),
	@tipo varchar(40),
	@phMenor numeric(10,2),
	@phMayor numeric(10,2),
	@impacto varchar (40),
	@ordenar varchar(40)

AS
BEGIN

	SET NOCOUNT ON;

	if @ordenar = ''
	begin
		if @phMayor = -1
		begin
			Select idFerti, nombre, tipo, pH, impacto from Fertilizantes
			Where (nombre  LIKE '%' + @nombre + '%' and tipo LIKE '%' + @tipo + '%' and impacto LIKE '%' + @impacto + '%')
			order by idFerti
		end
		else
		begin
			Select idFerti, nombre, tipo, pH, impacto from Fertilizantes
			Where (nombre  LIKE '%' + @nombre + '%' and tipo LIKE '%' + @tipo + '%' and impacto LIKE '%' + @impacto + '%' and (pH >= @phMenor and ph <= @phMayor))
			order by idFerti
		end
	end
	if @ordenar = 'Nombre'
	begin
		if @phMayor = -1
		begin
			Select idFerti, nombre, tipo, pH, impacto from Fertilizantes
			Where (nombre  LIKE '%' + @nombre + '%' and tipo LIKE '%' + @tipo + '%' and impacto LIKE '%' + @impacto + '%')
			order by nombre
		end
		else
		begin
			Select idFerti, nombre, tipo, pH, impacto from Fertilizantes
			Where (nombre  LIKE '%' + @nombre + '%' and tipo LIKE '%' + @tipo + '%' and impacto LIKE '%' + @impacto + '%' and (pH >= @phMenor and ph <= @phMayor))
			order by nombre
		end
	end
		if @ordenar = 'Tipo'
		begin
		if @phMayor = -1
		begin
			Select idFerti, nombre, tipo, pH, impacto from Fertilizantes
			Where (nombre  LIKE '%' + @nombre + '%' and tipo LIKE '%' + @tipo + '%' and impacto LIKE '%' + @impacto + '%')
			order by tipo
		end
		else
		begin
			Select idFerti, nombre, tipo, pH, impacto from Fertilizantes
			Where (nombre  LIKE '%' + @nombre + '%' and tipo LIKE '%' + @tipo + '%' and impacto LIKE '%' + @impacto + '%' and (pH >= @phMenor and ph <= @phMayor))
			order by tipo
		end
	end


	if @ordenar = 'PH'
	begin
		if @phMayor = -1
		begin
			Select idFerti, nombre, tipo, pH, impacto from Fertilizantes
			Where (nombre  LIKE '%' + @nombre + '%' and tipo LIKE '%' + @tipo + '%' and impacto LIKE '%' + @impacto + '%')
			order by pH
		end
		else
		begin
			Select idFerti, nombre, tipo, pH, impacto from Fertilizantes
			Where (nombre  LIKE '%' + @nombre + '%' and tipo LIKE '%' + @tipo + '%' and impacto LIKE '%' + @impacto + '%' and (pH >= @phMenor and ph <= @phMayor))
			order by pH
		end
	end
		if @ordenar = 'Impacto'
		begin
		if @phMayor = -1
		begin
			Select idFerti, nombre, tipo, pH, impacto from Fertilizantes
			Where (nombre  LIKE '%' + @nombre + '%' and tipo LIKE '%' + @tipo + '%' and impacto LIKE '%' + @impacto + '%')
			order by impacto
		end
		else
		begin
			Select idFerti, nombre, tipo, pH, impacto from Fertilizantes
			Where (nombre  LIKE '%' + @nombre + '%' and tipo LIKE '%' + @tipo + '%' and impacto LIKE '%' + @impacto + '%' and (pH >= @phMenor and ph <= @phMayor))
			order by impacto
		end
	end
end

	
/****** Object:  StoredProcedure [dbo].[BuscarFiltroCam]    Script Date: 9/9/2023 4:08:17 ******/
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[BuscarFiltrarLotes]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarFiltroCam]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BuscarFiltroCam]
	@marca varchar(40),
	@modelo varchar(40),
	@cargaMenor numeric(10,2),
	@cargaMayor numeric(10,2),
	@disponible varchar (40),
	@ordenar varchar(40)
AS
BEGIN
	SET NOCOUNT ON;


	if @ordenar = ''
	begin
		if @cargaMenor = 0
		begin
			Select idCamion, marca, modelo, carga, disponible from Camiones
			Where (marca  LIKE '%' + @marca + '%' and modelo  LIKE '%' + @modelo + '%' and disponible LIKE @disponible + '%')
			order by idCamion
		end
		else
		begin
			Select idCamion, marca, modelo, carga, disponible from Camiones
			Where (marca  LIKE '%' + @marca + '%'  and modelo  LIKE '%' + @modelo + '%' and (carga >= @cargaMenor and carga <= @cargaMayor) and disponible LIKE @disponible + '%')
			order by idCamion
		end
	end

	if @ordenar = 'Marca'
	begin
		if @cargaMenor = 0
		begin
			Select idCamion, marca, modelo, carga, disponible from Camiones
			Where (marca  LIKE '%' + @marca + '%' and modelo  LIKE '%' + @modelo + '%' and disponible LIKE @disponible + '%')
			order by marca
		end
		else
		begin
			Select idCamion, marca, modelo, carga, disponible from Camiones
			Where (marca  LIKE '%' + @marca + '%'  and modelo  LIKE '%' + @modelo + '%' and (carga >= @cargaMenor and carga <= @cargaMayor) and disponible LIKE @disponible + '%')
			order by marca
		end
	end

	if @ordenar = 'Modelo'
	begin
		if @cargaMenor = 0
		begin
			Select idCamion, marca, modelo, carga, disponible from Camiones
			Where (marca  LIKE '%' + @marca + '%' and modelo  LIKE '%' + @modelo + '%' and disponible LIKE @disponible + '%')
			order by modelo
		end
		else
		begin
			Select idCamion, marca, modelo, carga, disponible from Camiones
			Where (marca  LIKE '%' + @marca + '%'  and modelo  LIKE '%' + @modelo + '%' and (carga >= @cargaMenor and carga <= @cargaMayor) and disponible LIKE @disponible + '%')
			order by modelo
		end
	end
	
	if @ordenar = 'Carga'
	begin
		if @cargaMenor = 0
		begin
			Select idCamion, marca, modelo, carga, disponible from Camiones
			Where (marca  LIKE '%' + @marca + '%' and modelo  LIKE '%' + @modelo + '%' and disponible LIKE @disponible + '%')
			order by carga
		end
		else
		begin
			Select idCamion, marca, modelo, carga, disponible from Camiones
			Where (marca  LIKE '%' + @marca + '%'  and modelo  LIKE '%' + @modelo + '%' and (carga >= @cargaMenor and carga <= @cargaMayor) and disponible LIKE @disponible + '%')
			order by carga
		end
	end

	if @ordenar = 'Disponible'
	begin
		if @cargaMenor = 0
		begin
			Select idCamion, marca, modelo, carga, disponible from Camiones
			Where (marca  LIKE '%' + @marca + '%' and modelo  LIKE '%' + @modelo + '%' and disponible LIKE @disponible + '%')
			order by disponible
		end
		else
		begin
			Select idCamion, marca, modelo, carga, disponible from Camiones
			Where (marca  LIKE '%' + @marca + '%'  and modelo  LIKE '%' + @modelo + '%' and (carga >= @cargaMenor and carga <= @cargaMayor) and disponible LIKE @disponible + '%')
			order by disponible
		end
	end

END


/****** Object:  StoredProcedure [dbo].[BuscarGranjaFiltro]    Script Date: 9/9/2023 4:08:27 ******/
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[BuscarGranja]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarGranjaFiltro]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BuscarGranjaFiltro]
	@nombre varchar(40),
	@ubicacion varchar(40),
	@idCli int,
	@ordenar varchar(40)
AS
BEGIN
	SET NOCOUNT ON;

	if @ordenar =''
	begin
		if @idCli != 0
		begin
			Select G.idGranja, G.nombre, G.ubicacion, P.nombre as nombreCliente from Granjas G inner join Personas P on G.idCliente = P.idPersona
			where G.nombre  LIKE '%' + @nombre + '%' and ubicacion LIKE '%' + @ubicacion + '%' and G.idCliente = @idCli
			order by idGranja
		end
		else
		begin
			Select G.idGranja, G.nombre, G.ubicacion, P.nombre as nombreCliente from Granjas G inner join Personas P on G.idCliente = P.idPersona
			where G.nombre  LIKE '%' + @nombre + '%' and ubicacion LIKE '%' + @ubicacion + '%'
			order by idGranja
		end
	end


	
	if @ordenar ='Nombre'
	begin
		if @idCli != 0
		begin
			Select G.idGranja, G.nombre, G.ubicacion, P.nombre as nombreCliente from Granjas G inner join Personas P on G.idCliente = P.idPersona
			where G.nombre  LIKE '%' + @nombre + '%' and ubicacion LIKE '%' + @ubicacion + '%' and G.idCliente = @idCli
			order by G.nombre
		end
		else
		begin
			Select G.idGranja, G.nombre, G.ubicacion, P.nombre as nombreCliente from Granjas G inner join Personas P on G.idCliente = P.idPersona
			where G.nombre  LIKE '%' + @nombre + '%' and ubicacion LIKE '%' + @ubicacion + '%'
			order by G.nombre
		end
	end

	if @ordenar ='Ubicación'
	begin
		if @idCli != 0
		begin
			Select G.idGranja, G.nombre, G.ubicacion, P.nombre as nombreCliente from Granjas G inner join Personas P on G.idCliente = P.idPersona
			where G.nombre  LIKE '%' + @nombre + '%' and ubicacion LIKE '%' + @ubicacion + '%' and G.idCliente = @idCli
			order by G.ubicacion
		end
		else
		begin
			Select G.idGranja, G.nombre, G.ubicacion, P.nombre as nombreCliente from Granjas G inner join Personas P on G.idCliente = P.idPersona
			where G.nombre  LIKE '%' + @nombre + '%' and ubicacion LIKE '%' + @ubicacion + '%'
			order by G.ubicacion
		end
	end

	if @ordenar ='Nombre del dueño'
	begin
		if @idCli != 0
		begin
			Select G.idGranja, G.nombre, G.ubicacion, P.nombre as nombreCliente from Granjas G inner join Personas P on G.idCliente = P.idPersona
			where G.nombre  LIKE '%' + @nombre + '%' and ubicacion LIKE '%' + @ubicacion + '%' and G.idCliente = @idCli
			order by P.nombre
		end
		else
		begin
			Select G.idGranja, G.nombre, G.ubicacion, P.nombre as nombreCliente from Granjas G inner join Personas P on G.idCliente = P.idPersona
			where G.nombre  LIKE '%' + @nombre + '%' and ubicacion LIKE '%' + @ubicacion + '%'
			order by P.nombre
		end
	end
END
GO
/****** Object:  StoredProcedure [dbo].[BuscarLote]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarLote_Ferti]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarLote_Pesti]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarPedidoProd]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BuscarPedidoProd]

@idPedido int

AS
BEGIN

	SET NOCOUNT ON;

	SELECT Pp.idPedido as idPedido, Pp.idProducto as idProducto, P.nombre as nombre, P.tipo as tipo, P.precio as precio, P.imagen as imagen,
	Pp.cantidad as cantidad, P.cantTotal as cantTotal, P.cantRes as cantRes
	from Pedidos_Prod Pp inner join  Productos P 
	on Pp.idProducto = P.idProducto
	where Pp.idPedido = @idPedido
END
GO
/****** Object:  StoredProcedure [dbo].[BuscarPesti]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarPesticidaFiltro]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[BuscarProducto]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[BuscarProducto] 
@id int
AS
BEGIN
	
	SET NOCOUNT ON;
	 	Select idProducto, nombre, tipo, precio, tipoVenta, imagen, CantTotal, cantRes from Productos Where idProducto = @id
END

GO
/****** Object:  StoredProcedure [dbo].[BuscarProductoCatFiltro]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BuscarProductoCatFiltro] 
@buscar varchar(40),
@tipo varchar(40),
@tipoVen varchar(40),
@ordenar varchar(40)
AS
BEGIN
	if @ordenar = 'Nombre'
		begin
		Select idProducto, nombre, tipo, tipoVenta, imagen, cantTotal, cantRes  from Productos
		Where nombre LIKE '%' + @buscar + '%' and (tipo LIKE   @tipo + '%') and ( tipoVenta LIKE   @tipoVen + '%')
		and (cantTotal != '0') and (cantRes != cantTotal)
		order by nombre
		end
	if @ordenar = 'Tipo'
		begin
		Select idProducto, nombre, tipo, tipoVenta, imagen, cantTotal, cantRes from Productos 
		Where nombre LIKE '%' + @buscar + '%' and (tipo LIKE   @tipo + '%') and ( tipoVenta LIKE   @tipoVen + '%')
		and (cantTotal != '0') and (cantRes != cantTotal)

		order by tipo
		end
		if @ordenar = 'Tipo de venta'
		begin
		Select idProducto, nombre, tipo, tipoVenta, imagen, cantTotal, cantRes from Productos 
		Where nombre LIKE '%' + @buscar + '%' and (tipo LIKE   @tipo + '%') and ( tipoVenta LIKE   @tipoVen + '%')
		and (cantTotal != '0') and (cantRes != cantTotal)

		order by tipoVenta
		end
else 
		begin
		Select idProducto, nombre, tipo, tipoVenta, imagen, cantTotal, cantRes from Productos 
		Where nombre LIKE '%' + @buscar + '%' and (tipo LIKE   @tipo + '%') and ( tipoVenta LIKE   @tipoVen + '%')
		and (cantTotal != '0') and (cantRes != cantTotal)
		order by idProducto
		end
END

GO
/****** Object:  StoredProcedure [dbo].[BuscarProductoCli]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[BuscarProductoCli]
 @idProducto int,
 @idCliente int
AS
BEGIN
	select Pp.idPedido as idPedido   from Pedidos_Prod Pp inner join Pedidos  P on  Pp.idPedido = P.idPedido
	where Pp.idProducto = @idProducto and P.idCliente = @idCliente



END
GO
/****** Object:  StoredProcedure [dbo].[BuscarProductoClixNom]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[BuscarProductoClixNom]

@idPedido int, 
 @nomProd varchar(40)
AS
BEGIN
	
	SELECT Pp.idPedido as idPedido, Pp.idProducto, P.nombre as nombre,  Pp.cantidad as cantidad from Pedidos_Prod Pp inner join  Productos P 
	on Pp.idProducto = P.idProducto
	where P.nombre = @nomProd and Pp.idPedido = @idPedido


END
GO
/****** Object:  StoredProcedure [dbo].[BuscarProductoFiltro]    Script Date: 11/9/2023 19:03:13 ******/
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
		Select idProducto, nombre, tipo, tipoVenta, imagen, cantTotal, cantRes  from Productos Where nombre LIKE '%' + @buscar + '%' and (tipo LIKE   @tipo + '%') and ( tipoVenta LIKE   @tipoVen + '%')
		order by nombre
		end
	if @ordenar = 'Tipo'
		begin
		Select idProducto, nombre, tipo, tipoVenta, imagen, cantTotal, cantRes from Productos Where nombre LIKE '%' + @buscar + '%' and (tipo LIKE   @tipo + '%') and ( tipoVenta LIKE   @tipoVen + '%')
		order by tipo
		end
		if @ordenar = 'Tipo de venta'
		begin
		Select idProducto, nombre, tipo, tipoVenta, imagen, cantTotal, cantRes from Productos Where nombre LIKE '%' + @buscar + '%' and (tipo LIKE   @tipo + '%') and ( tipoVenta LIKE   @tipoVen + '%')
		order by tipoVenta
		end
else 
		begin
		Select idProducto, nombre, tipo, tipoVenta, imagen, cantTotal, cantRes from Productos Where nombre LIKE '%' + @buscar + '%' and (tipo LIKE   @tipo + '%') and ( tipoVenta LIKE   @tipoVen + '%')
		order by idProducto
		end
END

GO
/****** Object:  StoredProcedure [dbo].[CambiarEstadoPed]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[CambiarEstadoPed]
	@estado varchar(40),
 @idPedido int
AS
BEGIN
	Update  Pedidos set estado = @estado
	where idPedido = @idPedido


END
GO
/****** Object:  StoredProcedure [dbo].[IniciarSesionAdm]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[IniciarSesionCli]    Script Date: 11/9/2023 19:03:13 ******/
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
	

		Select idCliente, usuario  from Clientes
		where usuario = @User and contrasena = @Pass
END
GO
/****** Object:  StoredProcedure [dbo].[listPedido]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[listPedido] 
	-- Add the parameters for the stored procedure here
@idCli int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT idPedido, idCliente estado, fchPedido from Pedidos_Cli

END
GO
/****** Object:  StoredProcedure [dbo].[listPedidoCli]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[listPedidoCli] 
	-- Add the parameters for the stored procedure here
@idCli int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT idPedido, idCliente, estado, fchPedido, costo from Pedidos
	where idCliente = @idCli
END
GO
/****** Object:  StoredProcedure [dbo].[listPedidoCli_Prod]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[listPedidoCli_Prod] 
	-- Add the parameters for the stored procedure here
@idProducto int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT idPedido, idProducto, cantidad  from Pedidos_Prod
	where idProducto = @idProducto
END
GO
/****** Object:  StoredProcedure [dbo].[LstFertisEnLote]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[LstFertisEnLoteFiltro]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LstFertisEnLoteFiltro]
	@idGranja int,
	@idProducto int,
	@fchProduccion varchar(20),
	@buscar varchar(40),
	@ordenar varchar(40)
as
BEGIN

	SET NOCOUNT ON;
	if @ordenar = ''
	begin
	Select F.idFerti, F.nombre, F.tipo, F.pH,  L.cantidad from Fertilizantes F inner join Lotes_Fertis L on L.idFertilizante = F.idFerti 
	where (L.idGranja = @idGranja and L.idProducto = @idProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103))
	and (F.nombre  LIKE '%' + @buscar + '%' or F.tipo LIKE '%' + @buscar + '%'
	or F.pH LIKE '%' + @buscar + '%')
	order by F.idFerti
	end

	if @ordenar = 'Nombre'
	begin
	Select F.idFerti, F.nombre, F.tipo, F.pH,  L.cantidad from Fertilizantes F inner join Lotes_Fertis L on L.idFertilizante = F.idFerti 
	where (L.idGranja = @idGranja and L.idProducto = @idProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103))
	and (F.nombre  LIKE '%' + @buscar + '%' or F.tipo LIKE '%' + @buscar + '%'
	or F.pH LIKE '%' + @buscar + '%')
	order by F.nombre
	end

	
	if @ordenar = 'Tipo'
	begin
	Select F.idFerti, F.nombre, F.tipo, F.pH,  L.cantidad from Fertilizantes F inner join Lotes_Fertis L on L.idFertilizante = F.idFerti 
	where (L.idGranja = @idGranja and L.idProducto = @idProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103))
	and (F.nombre  LIKE '%' + @buscar + '%' or F.tipo LIKE '%' + @buscar + '%'
	or F.pH LIKE '%' + @buscar + '%')
	order by F.tipo
	end

	if @ordenar = 'pH'
	begin
	Select F.idFerti, F.nombre, F.tipo, F.pH,  L.cantidad from Fertilizantes F inner join Lotes_Fertis L on L.idFertilizante = F.idFerti 
	where (L.idGranja = @idGranja and L.idProducto = @idProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103))
	and (F.nombre  LIKE '%' + @buscar + '%' or F.tipo LIKE '%' + @buscar + '%'
	or F.pH LIKE '%' + @buscar + '%')
	order by F.pH
	end


		if @ordenar = 'Cantidad'
	begin
	Select F.idFerti, F.nombre, F.tipo, F.pH,  L.cantidad from Fertilizantes F inner join Lotes_Fertis L on L.idFertilizante = F.idFerti 
	where (L.idGranja = @idGranja and L.idProducto = @idProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103))
	and (F.nombre  LIKE '%' + @buscar + '%' or F.tipo LIKE '%' + @buscar + '%'
	or F.pH LIKE '%' + @buscar + '%')
	order by L.Cantidad
	end



END
GO
/****** Object:  StoredProcedure [dbo].[LstIdPersonas]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[LstPestiEnLoteFiltro]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[LstPestiEnLoteFiltro]
	@idGranja int,
	@idProducto int,
	@fchProduccion varchar(20),
	@buscar varchar(40),
	@ordenar varchar(40)
as
BEGIN

	SET NOCOUNT ON;
	if @ordenar = ''
	begin
	Select P.idPesti, P.nombre, P.tipo, P.pH,  L.cantidad from Pesticidas P inner join Lotes_Pestis L on L.idPesticida = P.idPesti 
	where (L.idGranja = @idGranja and L.idProducto = @idProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103))
	and (P.nombre  LIKE '%' + @buscar + '%' or P.tipo LIKE '%' + @buscar + '%'
	or P.pH LIKE '%' + @buscar + '%')
	order by P.idPesti
	end

	if @ordenar = 'Nombre'
	begin
	Select P.idPesti, P.nombre, P.tipo, P.pH,  L.cantidad from Pesticidas P inner join Lotes_Pestis L on L.idPesticida = P.idPesti 
	where (L.idGranja = @idGranja and L.idProducto = @idProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103))
	and (P.nombre  LIKE '%' + @buscar + '%' or P.tipo LIKE '%' + @buscar + '%'
	or P.pH LIKE '%' + @buscar + '%')
	order by P.nombre
	end

	
	if @ordenar = 'Tipo'
	begin
	Select P.idPesti, P.nombre, P.tipo, P.pH,  L.cantidad from Pesticidas P inner join Lotes_Pestis L on L.idPesticida = P.idPesti 
	where (L.idGranja = @idGranja and L.idProducto = @idProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103))
	and (P.nombre  LIKE '%' + @buscar + '%' or P.tipo LIKE '%' + @buscar + '%'
	or P.pH LIKE '%' + @buscar + '%')
	order by P.tipo
	end

	if @ordenar = 'pH'
	begin
	Select P.idPesti, P.nombre, P.tipo, P.pH,  L.cantidad from Pesticidas P inner join Lotes_Pestis L on L.idPesticida = P.idPesti 
	where (L.idGranja = @idGranja and L.idProducto = @idProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103))
	and (P.nombre  LIKE '%' + @buscar + '%' or P.tipo LIKE '%' + @buscar + '%'
	or P.pH LIKE '%' + @buscar + '%')
	order by P.pH
	end


		if @ordenar = 'Cantidad'
	begin
	Select P.idPesti, P.nombre, P.tipo, P.pH,  L.cantidad from Pesticidas P inner join Lotes_Pestis L on L.idPesticida = P.idPesti 
	where (L.idGranja = @idGranja and L.idProducto = @idProducto and L.fchProduccion = CONVERT(DATE,@fchProduccion,103))
	and (P.nombre  LIKE '%' + @buscar + '%' or P.tipo LIKE '%' + @buscar + '%'
	or P.pH LIKE '%' + @buscar + '%')
	order by L.Cantidad
	end



END
GO
/****** Object:  StoredProcedure [dbo].[LstPestisEnLote]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[ModCantPediodCli]    Script Date: 11/9/2023 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ModCantPediodCli]

@idPedido int, 
@idProducto int,
@cant varchar(40),
@cantRess varchar(40),
@precio numeric(10,2)

AS
BEGIN
	
	update Pedidos_Prod set cantidad = @cant
	where idPedido =@idPedido and idProducto = @idProducto

		if  @@ROWCOUNT>0   
	begin 

	Update Productos set cantRes = @cantRess
	where idProducto = @IdProducto 
	

	Update Pedidos set costo = @precio 
	where idPedido = @IdPedido

	end


END
GO
/****** Object:  StoredProcedure [dbo].[ModificarAdm]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarCam]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarCamionero]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarCli]    Script Date: 11/9/2023 19:03:13 ******/
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
	@pass varchar(40),
	@dirr varchar(40)

AS
BEGIN
	if @pass = ''
	begin
		UPDATE Clientes SET usuario = @user,  direccion = @dirr  where idCliente = @id
	end
	else
	begin
		UPDATE Clientes SET usuario = @user, contrasena = @pass,  direccion = @dirr  where idCliente = @id
	end
	UPDATE Personas SET nombre = @nombre, apellido = @apellido, email = @email, telefono = @tele, fchNacimiento = @fchNac where idPersona = @id
END
GO
/****** Object:  StoredProcedure [dbo].[ModificarDeposito]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarFerti]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarGranja]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarLote]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarLoteFerti]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarLotePesti]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarPesti]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarProducto]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[userRepetidoAdm]    Script Date: 11/9/2023 19:03:13 ******/
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
/****** Object:  StoredProcedure [dbo].[userRepetidoCli]    Script Date: 11/9/2023 19:03:13 ******/
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
