
CREATE OR ALTER PROCEDURE USP_Eliminar_Categoria(
    @Id AS INT
)
AS
BEGIN
    DELETE FROM Categoria WHERE Id = @Id
END
GO

CREATE OR ALTER PROCEDURE USP_Buscar_Categoria_x_Id(
    @Id AS INT
)
AS
BEGIN
    SELECT * FROM Categoria WHERE Id = @Id
END
GO

CREATE OR ALTER PROCEDURE USP_Listar_Categoria
AS
BEGIN
    SELECT * FROM Categoria
END
GO


CREATE OR ALTER PROCEDURE USP_Paginate_Categoria(
    @Page INT,
    @Size INT,
    @Search VARCHAR(200) = NULL,
    @OrderBy VARCHAR(50) = 'Detalle',
    @OrderDir VARCHAR(4) = 'ASC',
    @TotalGlobal INT OUTPUT,
    @TotalFiltered INT OUTPUT
)
AS
BEGIN
    DECLARE @Skip INT;
    SET @Skip = (@Size * @Page) - @Size;


    SELECT @TotalGlobal = COUNT(*)
    FROM dbo.Categoria


    SELECT @TotalFiltered = COUNT(*)
    FROM dbo.Categoria
    WHERE (@Search IS NULL OR UPPER(Detalle) LIKE '%' + upper(@Search) + '%');


    SELECT *
    FROM Categoria
    WHERE (@Search IS NULL OR UPPER(Detalle) LIKE '%' + upper(@Search) + '%')
    ORDER BY @OrderBy + ' ' + @OrderDir
    OFFSET @Skip ROWS FETCH NEXT (@Size) ROWS ONLY
END
GO

CREATE OR ALTER PROCEDURE USP_Registrar_Categoria(
    @Detalle AS VARCHAR(50)
)
AS
BEGIN
    INSERT INTO Categoria (Detalle)
    VALUES (@Detalle)
END
GO


CREATE OR ALTER PROCEDURE USP_Actualizar_Categoria(
    @Id AS INT,
    @Detalle AS VARCHAR(50)
)
AS
BEGIN
    UPDATE Categoria
    SET Detalle=@Detalle
    WHERE Id = @Id
END
GO



CREATE OR ALTER PROCEDURE USP_Eliminar_Escala(
    @Id AS INT
)
AS
BEGIN
    DELETE FROM Escala WHERE Id = @Id
END
GO

CREATE OR ALTER PROCEDURE USP_Buscar_Escala_x_Id(
    @Id AS INT
)
AS
BEGIN
    SELECT * FROM Escala WHERE Id = @Id
END
GO

CREATE OR ALTER PROCEDURE USP_Listar_Escala
AS
BEGIN
    SELECT * FROM Escala
END
GO


CREATE OR ALTER PROCEDURE USP_Paginate_Escala(
    @Page INT,
    @Size INT,
    @Search VARCHAR(200) = NULL,
    @OrderBy VARCHAR(50) = 'Detalle',
    @OrderDir VARCHAR(4) = 'ASC',
    @TotalGlobal INT OUTPUT,
    @TotalFiltered INT OUTPUT
)
AS
BEGIN
    DECLARE @Skip INT;
    SET @Skip = (@Size * @Page) - @Size;


    SELECT @TotalGlobal = COUNT(*)
    FROM dbo.Escala


    SELECT @TotalFiltered = COUNT(*)
    FROM dbo.Escala
    WHERE (@Search IS NULL OR UPPER(Detalle) LIKE '%' + upper(@Search) + '%');


    SELECT *
    FROM Escala
    WHERE (@Search IS NULL OR UPPER(Detalle) LIKE '%' + upper(@Search) + '%')
    ORDER BY @OrderBy + ' ' + @OrderDir
    OFFSET @Skip ROWS FETCH NEXT (@Size) ROWS ONLY
END
GO

CREATE OR ALTER PROCEDURE USP_Registrar_Escala(
    @Detalle AS VARCHAR(50)
)
AS
BEGIN
    INSERT INTO Escala (Detalle)
    VALUES (@Detalle)
END
GO


CREATE OR ALTER PROCEDURE USP_Actualizar_Escala(
    @Id AS INT,
    @Detalle AS VARCHAR(50)
)
AS
BEGIN
    UPDATE Escala
    SET Detalle=@Detalle
    WHERE Id = @Id
END
GO









CREATE OR ALTER PROCEDURE USP_Eliminar_Provincia(
    @Id AS INT
)
AS
BEGIN
    DELETE FROM Provincia WHERE Id = @Id
END
GO

CREATE OR ALTER PROCEDURE USP_Buscar_Provincia_x_Id(
    @Id AS INT
)
AS
BEGIN
    SELECT * FROM Provincia WHERE Id = @Id
END
GO

CREATE OR ALTER PROCEDURE USP_Listar_Provincia
AS
BEGIN
    SELECT * FROM Provincia
END
GO


CREATE OR ALTER PROCEDURE USP_Paginate_Provincia(
    @Page INT,
    @Size INT,
    @Search VARCHAR(200) = NULL,
    @OrderBy VARCHAR(50) = 'Nombre',
    @OrderDir VARCHAR(4) = 'ASC',
    @TotalGlobal INT OUTPUT,
    @TotalFiltered INT OUTPUT
)
AS
BEGIN
    DECLARE @Skip INT;
    SET @Skip = (@Size * @Page) - @Size;


    SELECT @TotalGlobal = COUNT(*)
    FROM dbo.Provincia


    SELECT @TotalFiltered = COUNT(*)
    FROM dbo.Provincia
    WHERE (@Search IS NULL OR UPPER(Nombre) LIKE '%' + upper(@Search) + '%');


    SELECT *
    FROM Provincia
    WHERE (@Search IS NULL OR UPPER(Nombre) LIKE '%' + upper(@Search) + '%')
    ORDER BY @OrderBy + ' ' + @OrderDir
    OFFSET @Skip ROWS FETCH NEXT (@Size) ROWS ONLY
END
GO

CREATE OR ALTER PROCEDURE USP_Registrar_Provincia(
    @Nombre AS VARCHAR(50)
)
AS
BEGIN
    INSERT INTO Provincia (Nombre)
    VALUES (@Nombre)
END
GO


CREATE OR ALTER PROCEDURE USP_Actualizar_Provincia(
    @Id AS INT,
    @Nombre AS VARCHAR(50)
)
AS
BEGIN
    UPDATE Provincia
    SET Nombre=@Nombre
    WHERE Id = @Id
END
GO





























CREATE OR ALTER PROCEDURE USP_Eliminar_Ciudad(
    @Id AS INT
)
AS
BEGIN
    DELETE FROM Ciudad WHERE Id = @Id
END
GO

CREATE OR ALTER PROCEDURE USP_Buscar_Ciudad_x_Id(
    @Id AS INT
)
AS
BEGIN
    SELECT * FROM Ciudad WHERE Id = @Id
END
GO

CREATE OR ALTER PROCEDURE USP_Listar_Ciudad
AS
BEGIN
    SELECT * FROM Ciudad
END
GO


CREATE OR ALTER PROCEDURE USP_Paginate_Ciudad(
    @Page INT,
    @Size INT,
    @Search VARCHAR(200) = NULL,
    @OrderBy VARCHAR(50) = 'Nombre',
    @OrderDir VARCHAR(4) = 'ASC',
    @TotalGlobal INT OUTPUT,
    @TotalFiltered INT OUTPUT
)
AS
BEGIN
    DECLARE @Skip INT;
    SET @Skip = (@Size * @Page) - @Size;


    SELECT @TotalGlobal = COUNT(*)
    FROM dbo.Ciudad


    SELECT @TotalFiltered = COUNT(*)
    FROM dbo.Ciudad
    WHERE (@Search IS NULL OR UPPER(Nombre) LIKE '%' + upper(@Search) + '%');


    SELECT *
    FROM Ciudad
    WHERE (@Search IS NULL OR UPPER(Nombre) LIKE '%' + upper(@Search) + '%')
    ORDER BY @OrderBy + ' ' + @OrderDir
    OFFSET @Skip ROWS FETCH NEXT (@Size) ROWS ONLY
END
GO

CREATE OR ALTER PROCEDURE USP_Registrar_Ciudad(
    @Nombre AS VARCHAR(50),
    @Provincia_Id AS INT
)
AS
BEGIN
    INSERT INTO Ciudad (Nombre, Provincia_Id)
    VALUES (@Nombre, @Provincia_Id)
END
GO


CREATE OR ALTER PROCEDURE USP_Actualizar_Ciudad(
    @Id AS INT,
    @Nombre AS VARCHAR(50),
    @Provincia_Id AS INT
)
AS
BEGIN
    UPDATE Ciudad
    SET Nombre=@Nombre,
        Provincia_Id=@Provincia_Id
    WHERE Id = @Id
END
GO























CREATE OR ALTER PROCEDURE USP_Eliminar_Sucursal(
    @Id AS INT
)
AS
BEGIN
    DELETE FROM Sucursal WHERE Id = @Id
END
GO

CREATE OR ALTER PROCEDURE USP_Buscar_Sucursal_x_Id(
    @Id AS INT
)
AS
BEGIN
    SELECT * FROM Sucursal WHERE Id = @Id
END
GO

CREATE OR ALTER PROCEDURE USP_Listar_Sucursal
AS
BEGIN
    SELECT * FROM Sucursal
END
GO


CREATE OR ALTER PROCEDURE USP_Paginate_Sucursal(
    @Page INT,
    @Size INT,
    @Search VARCHAR(200) = NULL,
    @OrderBy VARCHAR(50) = 'Nombre',
    @OrderDir VARCHAR(4) = 'ASC',
    @TotalGlobal INT OUTPUT,
    @TotalFiltered INT OUTPUT
)
AS
BEGIN
    DECLARE @Skip INT;
    SET @Skip = (@Size * @Page) - @Size;


    SELECT @TotalGlobal = COUNT(*)
    FROM dbo.Sucursal


    SELECT @TotalFiltered = COUNT(*)
    FROM dbo.Sucursal
    WHERE (@Search IS NULL OR UPPER(Nombre) LIKE '%' + upper(@Search) + '%');


    SELECT *
    FROM Sucursal
    WHERE (@Search IS NULL OR UPPER(Nombre) LIKE '%' + upper(@Search) + '%')
    ORDER BY @OrderBy + ' ' + @OrderDir
    OFFSET @Skip ROWS FETCH NEXT (@Size) ROWS ONLY
END
GO

CREATE OR ALTER PROCEDURE USP_Registrar_Sucursal(
    @Nombre AS VARCHAR(50),
    @Ciudad_Id AS INT
)
AS
BEGIN
    INSERT INTO Sucursal (Nombre, Ciudad_Id)
    VALUES (@Nombre, @Ciudad_Id)
END
GO


CREATE OR ALTER PROCEDURE USP_Actualizar_Sucursal(
    @Id AS INT,
    @Nombre AS VARCHAR(50),
    @Ciudad_Id AS INT
)
AS
BEGIN
    UPDATE Sucursal
    SET Nombre=@Nombre,
        Ciudad_Id=@Ciudad_Id
    WHERE Id = @Id
END
GO




















CREATE OR ALTER PROCEDURE USP_Eliminar_Encuestado(
    @Id AS VARCHAR(10)
)
AS
BEGIN
    DELETE FROM Encuestado WHERE Ci = @Id
END
GO

CREATE OR ALTER PROCEDURE USP_Buscar_Encuestado_x_Id(
    @Id AS VARCHAR(10)
)
AS
BEGIN
    SELECT * FROM Encuestado WHERE Ci = @Id
END
GO

CREATE OR ALTER PROCEDURE USP_Listar_Encuestado
AS
BEGIN
    SELECT * FROM Encuestado
END
GO


CREATE OR ALTER PROCEDURE USP_Paginate_Encuestado(
    @Page INT,
    @Size INT,
    @Search VARCHAR(200) = NULL,
    @OrderBy VARCHAR(50) = 'Nombre_Completo',
    @OrderDir VARCHAR(4) = 'ASC',
    @TotalGlobal INT OUTPUT,
    @TotalFiltered INT OUTPUT
)
AS
BEGIN
    DECLARE @Skip INT;
    SET @Skip = (@Size * @Page) - @Size;


    SELECT @TotalGlobal = COUNT(*)
    FROM dbo.Encuestado


    SELECT @TotalFiltered = COUNT(*)
    FROM dbo.Encuestado
    WHERE (@Search IS NULL OR UPPER(Nombre_Completo) LIKE '%' + upper(@Search) + '%');


    SELECT *
    FROM Encuestado
    WHERE (@Search IS NULL OR UPPER(Nombre_Completo) LIKE '%' + upper(@Search) + '%')
    ORDER BY @OrderBy + ' ' + @OrderDir
    OFFSET @Skip ROWS FETCH NEXT (@Size) ROWS ONLY
END
GO

CREATE OR ALTER PROCEDURE USP_Registrar_Encuestado(
    @Ci AS VARCHAR(10),
    @Nombre_Completo AS VARCHAR(150),
    @Sexo AS INT,
    @Edad AS INT
)
AS
BEGIN
    INSERT INTO Encuestado (Ci, Nombre_Completo, Sexo, Edad)
    VALUES (@Ci, @Nombre_Completo, @Sexo, @Edad)
END
GO


CREATE OR ALTER PROCEDURE USP_Actualizar_Encuestado(
    @Ci AS VARCHAR(10),
    @Nombre_Completo AS VARCHAR(150),
    @Sexo AS INT,
    @Edad AS INT
)
AS
BEGIN
    UPDATE Encuestado
    SET Nombre_Completo=@Nombre_Completo,
        Sexo=@Sexo,
        Edad=@Edad
    WHERE Ci = @Ci
END
GO
















CREATE OR ALTER PROCEDURE USP_Eliminar_Encuesta(
    @Id AS INT
)
AS
BEGIN
    DELETE FROM Encuesta WHERE Id = @Id
END
GO

CREATE OR ALTER PROCEDURE USP_Buscar_Encuesta_x_Id(
    @Id AS INT
)
AS
BEGIN
    SELECT * FROM Encuesta WHERE Id = @Id
END
GO

CREATE OR ALTER PROCEDURE USP_Listar_Encuesta
AS
BEGIN
    SELECT enc.*,esc.* FROM Encuesta enc INNER JOIN Escala esc on esc.Id = enc.Escala_Id
END
GO


CREATE OR ALTER PROCEDURE USP_Paginate_Encuesta(
    @Page INT,
    @Size INT,
    @Search VARCHAR(200) = NULL,
    @OrderBy VARCHAR(50) = 'Pregunta',
    @OrderDir VARCHAR(4) = 'ASC',
    @TotalGlobal INT OUTPUT,
    @TotalFiltered INT OUTPUT
)
AS
BEGIN
    DECLARE @Skip INT;
    SET @Skip = (@Size * @Page) - @Size;


    SELECT @TotalGlobal = COUNT(*)
    FROM dbo.Encuesta


    SELECT @TotalFiltered = COUNT(*)
    FROM dbo.Encuesta
    WHERE (@Search IS NULL OR UPPER(Pregunta) LIKE '%' + upper(@Search) + '%');


    SELECT *
    FROM Encuesta
    WHERE (@Search IS NULL OR UPPER(Pregunta) LIKE '%' + upper(@Search) + '%')
    ORDER BY @OrderBy + ' ' + @OrderDir
    OFFSET @Skip ROWS FETCH NEXT (@Size) ROWS ONLY
END
GO

CREATE OR ALTER PROCEDURE USP_Registrar_Encuesta(
    @Pregunta AS VARCHAR(50),
    @Categoria_Id AS INT,
    @Escala_Id AS INT
)
AS
BEGIN
    INSERT INTO Encuesta (Pregunta, Escala_Id, Categoria_Id)
    VALUES (@Pregunta, @Escala_Id, @Categoria_Id)
END
GO


CREATE OR ALTER PROCEDURE USP_Actualizar_Encuesta(
    @Id AS INT,
    @Pregunta AS VARCHAR(50),
    @Categoria_Id AS INT,
    @Escala_Id AS INT
)
AS
BEGIN
    UPDATE Encuesta
    SET Categoria_Id=@Categoria_Id,
        Pregunta=@Pregunta,
        Escala_Id=@Escala_Id
    WHERE Id = @Id
END
GO
























CREATE OR ALTER PROCEDURE USP_Listar_EncuestaRequerimiento_X_ID(
    @Id AS INT
)
AS
BEGIN
    SELECT ER.*, DE.*, E.*, E2.*
    FROM Encuesta_Realizada ER
             INNER JOIN Detalle_Encuesta DE on ER.Id = DE.EncuestaRealizada_Id
             INNER JOIN Encuesta E on E.Id = DE.Encuesta_Id
             INNER JOIN Encuestado E2 on E2.Ci = ER.Encuestado_Id
    WHERE ER.Id = @Id
END
GO



CREATE OR ALTER PROCEDURE USP_Paginate_EncuestaRequerimiento(
    @Page INT,
    @Size INT,
    @Search VARCHAR(200) = NULL,
    @OrderBy VARCHAR(50) = 'Nombre_Completo',
    @OrderDir VARCHAR(4) = 'ASC',
    @TotalGlobal INT OUTPUT,
    @TotalFiltered INT OUTPUT
)
AS
BEGIN
    DECLARE @Skip INT;
    SET @Skip = (@Size * @Page) - @Size;


    SELECT @TotalGlobal = COUNT(*)
    FROM dbo.Encuesta_Realizada
             inner join Sucursal S on S.Id = Encuesta_Realizada.Sucursal_Id
             inner join Encuestado E on E.Ci = Encuesta_Realizada.Encuestado_Id


    SELECT @TotalFiltered = COUNT(*)
    FROM dbo.Encuesta_Realizada ER
             inner join Sucursal S on S.Id = ER.Sucursal_Id
             inner join Encuestado E on E.Ci = ER.Encuestado_Id
    WHERE (@Search IS NULL OR UPPER(Nombre_Completo) LIKE '%' + upper(@Search) + '%');


    SELECT ER.*, E.Nombre_Completo AS Encuestado_Nombre, S.Nombre AS Sucursal_Nombre
    FROM dbo.Encuesta_Realizada ER
             inner join Sucursal S on S.Id = ER.Sucursal_Id
             inner join Encuestado E on E.Ci = ER.Encuestado_Id
    WHERE (@Search IS NULL OR UPPER(Nombre_Completo) LIKE '%' + upper(@Search) + '%')
    ORDER BY @OrderBy + ' ' + @OrderDir
    OFFSET @Skip ROWS FETCH NEXT (@Size) ROWS ONLY
END
GO




CREATE OR ALTER PROCEDURE USP_Registrar_EncuestaRequerimiento(
    @Encuestado_Id AS INT,
    @Sucursal_Id AS INT
)
AS
BEGIN
    INSERT INTO Encuesta_Realizada (Encuestado_Id, Sucursal_Id, Fecha_Realizada)
    VALUES (@Encuestado_Id, @Sucursal_Id, GETDATE());
    SELECT CAST(SCOPE_IDENTITY() as int)
END
GO

CREATE OR ALTER PROCEDURE USP_Registrar_DetalleEncuesta(
    @Encuesta_Id AS INT,
    @EncuestaRealizada_Id AS INT,
    @Respuesta AS VARCHAR(500)
)
AS
BEGIN
    INSERT INTO Detalle_Encuesta (EncuestaRealizada_Id, Encuesta_Id, Respuesta)
    VALUES (@EncuestaRealizada_Id, @Encuesta_Id, @Respuesta);
END
GO