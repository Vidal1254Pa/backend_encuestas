--create database examen_macro;
go
--use examen_macro;
go
CREATE TABLE  Provincia(
    Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Nombre VARCHAR(150) NOT NULL
)
GO
CREATE TABLE Ciudad(
    Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Nombre VARCHAR(150) NOT NULL,
    Provincia_Id INT NOT NULL,
    CONSTRAINT FK_CIUDAD_PROVINCIA FOREIGN KEY (Provincia_Id)
                   REFERENCES Provincia(Id)
)
GO
CREATE TABLE Sucursal(
    Id INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
    Nombre VARCHAR(150) NOT NULL,
    Ciudad_Id INT NOT NULL,
    CONSTRAINT FK_SUCURSAL_CIUDAD FOREIGN KEY (Ciudad_Id)
                     REFERENCES Ciudad(Id)
)
GO
CREATE TABLE Categoria(
    Id INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
    Detalle VARCHAR(50)
)
GO
CREATE TABLE Escala(
    Id INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
    Detalle VARCHAR(50)
)
GO
CREATE TABLE Encuesta(
    Id INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
    Pregunta VARCHAR(1000) NOT NULL,
    Escala_Id INT NOT NULL,
    Categoria_Id INT NOT NULL,
    CONSTRAINT FK_ENCUESTA_ESCALA FOREIGN KEY (Escala_Id)
                     REFERENCES Escala(Id),
    CONSTRAINT FK_ENCUESTA_CATEGORIA FOREIGN KEY (Categoria_Id)
                     REFERENCES Categoria(Id)
)
GO
CREATE TABLE Encuestado(
    Ci VARCHAR(10) PRIMARY KEY NOT NULL,
    Nombre_Completo VARCHAR(150) NOT NULL,
    Sexo INT NOT NULL,
    Edad INT NOT NULL
)
GO
CREATE TABLE Encuesta_Realizada(
    Id INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
    Encuestado_Id VARCHAR(10) NOT NULL,
    Sucursal_Id INT NOT NULL,
    Fecha_Realizada DATETIME,
    CONSTRAINT FK_ER_ENCUESTADO FOREIGN KEY (Encuestado_Id)
                               REFERENCES Encuestado(Ci),
    CONSTRAINT FK_ER_SUCURSAL FOREIGN KEY (Sucursal_Id)
                               REFERENCES Sucursal(Id)
)
GO
CREATE TABLE Detalle_Encuesta(
    EncuestaRealizada_Id INT NOT NULL,
    Encuesta_Id INT NOT NULL,
    Respuesta VARCHAR(500) NOT NULL,
    CONSTRAINT FK_DE_ENCUESTA_REALIZADA FOREIGN KEY (EncuestaRealizada_Id)
                             REFERENCES Encuesta_Realizada(Id),
    CONSTRAINT FK_DE_ENCUESTA FOREIGN KEY (Encuesta_Id)
                             REFERENCES Encuesta(Id)
)