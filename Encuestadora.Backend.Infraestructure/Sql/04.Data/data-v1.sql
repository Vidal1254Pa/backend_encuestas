insert into dbo.Provincia (Id, Nombre)
values  (1, N'LOJA'),
        (2, N'MANABÍ'),
        (3, N'PICHINCHA'),
        (4, N'GUAYAS');
GO
insert into dbo.Ciudad (Id, Nombre, Provincia_Id)
values  (1, N'QUITO', 3),
        (2, N'GUAYAQUIL', 4),
        (3, N'RUMIÑAHUI', 3),
        (4, N'PEDERNALES', 2),
        (5, N'LOJA', 1);
GO
insert into dbo.Sucursal (Id, Nombre, Ciudad_Id)
values  (1, N'29 DE MAYO', 5),
        (2, N'TARQUI', 4),
        (3, N'SAN RAFAEL', 3),
        (4, N'GUASMO', 2),
        (5, N'MENA 2', 1),
        (6, N'QUICENTRO NORTE', 1);
GO
insert into dbo.Escala (Id, Detalle)
values  (1, N'SI/NO'),
        (2, N'0-10'),
        (3, N'ABIERTA');
GO
insert into dbo.Categoria (Id, Detalle)
values  (1, N'ESTÁNDARES'),
        (2, N'SATISFACCIÓN'),
        (3, N'RECOMENDACIÓN');
GO
insert into dbo.Encuesta (Id, Pregunta, Escala_Id, Categoria_Id)
values  (1, N'LE DESPACHARON LOS PRODUCTOS REVISANDO LA FACTURA', 1, 1),
        (2, N'LE ENTREGARON LA FACTURA', 1, 1),
        (3, N'LA AMABILIDAD DEL CAJERO', 2, 2),
        (4, N'LA SATSFACIÓN EN GENERAL', 2, 2),
        (5, N'EN QUE NIVEL RECOMEDARÍA A XYZ A AMIGOS O FAMILIARES SIENDO 0 DEFINITIVAMENTE NO RECOMENDARÍA Y 10 SI RECOMENDARÍA', 2, 3),
        (6, N'SUS OBSERVACIONES FINALES POR FAVOR', 3, 3);