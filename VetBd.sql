CREATE TABLE [dbo].[Clientes](
    [IdCliente]         INT IDENTITY(1,1) NOT NULL,
    [Cedula]            NVARCHAR(20)  NOT NULL,
    [Nombre]            NVARCHAR(250) NOT NULL,
    [CorreoElectronico] NVARCHAR(100) NOT NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED ([IdCliente] ASC),
    CONSTRAINT [UK_Clientes_Cedula] UNIQUE ([Cedula])
)
GO

CREATE TABLE [dbo].[Mascotas](
    [IdMascota] INT IDENTITY(1,1) NOT NULL,
    [Nombre]    NVARCHAR(100) NOT NULL,
    [Especie]   NVARCHAR(50)  NOT NULL,
    [Raza]      NVARCHAR(50)  NOT NULL,
    [Peso]      DECIMAL(8,2)  NOT NULL,
    [IdCliente] INT NOT NULL,
    CONSTRAINT [PK_Mascotas] PRIMARY KEY CLUSTERED ([IdMascota] ASC),
    CONSTRAINT [FK_Mascotas_Clientes] FOREIGN KEY ([IdCliente]) REFERENCES [dbo].[Clientes]([IdCliente])
)
GO

-- Registrar cliente (cédula no repetida)
CREATE PROCEDURE [dbo].[sp_RegistrarCliente]
    @Cedula            NVARCHAR(20),
    @Nombre            NVARCHAR(250),
    @CorreoElectronico NVARCHAR(100)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM dbo.Clientes WHERE Cedula = @Cedula)
    BEGIN
        SELECT 0 AS Resultado, N'La cédula ya se encuentra registrada' AS Mensaje
        RETURN
    END

    INSERT INTO dbo.Clientes (Cedula, Nombre, CorreoElectronico)
    VALUES (@Cedula, @Nombre, @CorreoElectronico)

    SELECT 1 AS Resultado, N'Cliente registrado correctamente' AS Mensaje
END
GO

-- Lista de cliente
CREATE PROCEDURE [dbo].[sp_ConsultarClientes]
AS
BEGIN
    SELECT IdCliente, Cedula, Nombre, CorreoElectronico
    FROM   dbo.Clientes
    ORDER BY Nombre
END
GO

-- Registrar mascota max 2 de la misma especie por cliente
CREATE PROCEDURE [dbo].[sp_RegistrarMascota]
    @Nombre    NVARCHAR(100),
    @Especie   NVARCHAR(50),
    @Raza      NVARCHAR(50),
    @Peso      DECIMAL(8,2),
    @IdCliente INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.Clientes WHERE IdCliente = @IdCliente)
    BEGIN
        SELECT 0 AS Resultado, N'El cliente seleccionado no existe' AS Mensaje
        RETURN
    END

    IF (SELECT COUNT(1) FROM dbo.Mascotas
        WHERE IdCliente = @IdCliente AND Especie = @Especie) >= 2
    BEGIN
        SELECT 0 AS Resultado, N'El cliente ya tiene 2 mascotas de la especie ' + @Especie AS Mensaje
        RETURN
    END

    INSERT INTO dbo.Mascotas (Nombre, Especie, Raza, Peso, IdCliente)
    VALUES (@Nombre, @Especie, @Raza, @Peso, @IdCliente)

    SELECT 1 AS Resultado, N'Mascota registrada correctamente' AS Mensaje
END
GO

-- Consulta de mascotas con datos del cliente
CREATE PROCEDURE [dbo].[sp_ConsultarMascotas]
AS
BEGIN
    SELECT  C.Cedula, C.Nombre AS NombreCliente,
            M.Nombre AS NombreMascota, M.Especie, M.Peso
    FROM    dbo.Mascotas M
    INNER JOIN dbo.Clientes C ON C.IdCliente = M.IdCliente
    ORDER BY C.Nombre, M.Nombre
END
GO