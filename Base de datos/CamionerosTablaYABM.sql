USE [Proyecto]
GO

/****** Object:  Table [dbo].[Camioneros]    Script Date: 14/7/2023 19:48:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Camioneros](
	[idCamionero] [int] NOT NULL,
	[cedula] [varchar](11) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idCamionero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Camioneros]  WITH CHECK ADD FOREIGN KEY([idCamionero])
REFERENCES [dbo].[Personas] ([idPersona])
GO

/****** Object:  StoredProcedure [dbo].[BuscarCamionero]    Script Date: 14/7/2023 19:46:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BuscarCamionero]
@id int
AS
BEGIN

	SELECT [idCamionero],[nombre],[apellido],[email],[telefono],[fchNacimiento],[cedula] FROM Camioneros C inner join Personas P on P.idPersona = C.idCamionero where idCamionero = @id

END
GO


/****** Object:  StoredProcedure [dbo].[ObtenerCamioneros]    Script Date: 14/7/2023 19:46:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ObtenerCamioneros]
AS
BEGIN

	SELECT [idCamionero],[nombre],[apellido],[email],[telefono],[fchNacimiento],[cedula] FROM Camioneros C inner join Personas P on P.idPersona = C.idCamionero

END
GO

/****** Object:  StoredProcedure [dbo].[AltaCamionero]    Script Date: 14/7/2023 19:45:46 ******/
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
	@fchNac varchar (40),
	@cedula varchar(11)


	
AS
BEGIN
	insert into Personas (idPersona, nombre, apellido, email, telefono, fchNacimiento) 
	values (@id, @nombre, @apellido, @email, @tele, CONVERT(DATE,@fchNac,103))
	if @@ROWCOUNT > 0
	begin
		insert into Camioneros(idCamionero, cedula) 
		values (@id, @cedula)
	end
END
GO

/****** Object:  StoredProcedure [dbo].[BajaCamionero]    Script Date: 14/7/2023 19:46:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[BajaCamionero]
	@id int
AS
BEGIN
	DELETE FROM Camioneros WHERE idCamionero = @id
	if @@ROWCOUNT > 0
	begin
		DELETE FROM Personas WHERE idPersona = @id
	end
END
GO

/****** Object:  StoredProcedure [dbo].[ModificarCamionero]    Script Date: 14/7/2023 19:46:18 ******/
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
	@cedula varchar(11)

AS
BEGIN
    UPDATE Camioneros SET cedula = @cedula where idCamionero = @id
	if @@ROWCOUNT > 0
	begin
		UPDATE Personas SET nombre = @nombre, apellido = @apellido, email = @email, telefono = @tele, fchNacimiento = @fchNac where idPersona = @id
	end
END
GO