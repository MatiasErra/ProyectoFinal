USE [Proyecto]
GO
/****** Object:  StoredProcedure [dbo].[CambiarEstadoPed]    Script Date: 14/10/2023 1:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[CambiarEstadoPed]
	@estado varchar(40),
	@idPedido int,
	@idAdmin int
AS
BEGIN
	Update  Pedidos set estado = @estado
	where idPedido = @idPedido

	if @estado = 'Finalizado'
	begin
		Update Pedidos set fechaEntre = CONVERT(VARCHAR(16), GETDATE(), 120) where idPedido = @idPedido
	end

	if @idAdmin != 0
	begin
		insert into Auditoria values (@idAdmin, CONVERT(VARCHAR(16), GETDATE(), 120), 'Pedidos', 'Modificar');
	end
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[BajaPedido]

@idPedido int,
@idAdmin int
AS
BEGIN

	SET NOCOUNT ON;
	Begin  Tran



	Delete from Pedidos
	Where idPedido = @idPedido

	if exists (Select * from Pedidos where idPedido = @idPedido) 
	or exists (Select * from Pedidos_Prod where idPedido = @idPedido) 
		begin
			raiserror('No se pudo borrar el Pedido',1,1);
			rollback;
		end
	else
	begin
		if @idAdmin != 0
		begin
			insert into Auditoria values (@idAdmin, CONVERT(VARCHAR(16), GETDATE(), 120), 'Pedidos', 'Baja');
			
		end
		Commit Tran
	end


END