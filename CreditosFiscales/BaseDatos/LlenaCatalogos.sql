USE [MotorTraductor]
GO

--- LLena CatEstadoProceso

INSERT INTO CatEstadoProceso (IdEstatus, Descripcion) VALUES (1, 'Registrado')
INSERT INTO CatEstadoProceso (IdEstatus, Descripcion) VALUES (2, 'Procesando')
INSERT INTO CatEstadoProceso (IdEstatus, Descripcion) VALUES (3, 'Procesado')
INSERT INTO CatEstadoProceso (IdEstatus, Descripcion) VALUES (4, 'Generando XML/ZIP')
INSERT INTO CatEstadoProceso (IdEstatus, Descripcion) VALUES (5, 'Terminado')
INSERT INTO CatEstadoProceso (IdEstatus, Descripcion) VALUES (99, 'Con error')

GO

--- Llena CatErrorProceso


INSERT INTO CatErrorProceso (IdError, Descripcion) VALUES (0, 'Sin error')
INSERT INTO CatErrorProceso (IdError, Descripcion) VALUES (1, 'No existe banco')
INSERT INTO CatErrorProceso (IdError, Descripcion) VALUES (2, 'Archivo con contenido invalido')
INSERT INTO CatErrorProceso (IdError, Descripcion) VALUES (3, 'Fecha de archivo no valida')
INSERT INTO CatErrorProceso (IdError, Descripcion) VALUES (4, 'Clave de banco no coincide con el nombre del archivo')
INSERT INTO CatErrorProceso (IdError, Descripcion) VALUES (5, 'Clave de banco no coinde con encabezadoPago Invalido')
INSERT INTO CatErrorProceso (IdError, Descripcion) VALUES (6, 'Numero de registros no coincide')
INSERT INTO CatErrorProceso (IdError, Descripcion) VALUES (7, 'Suma de importes no coincide')
INSERT INTO CatErrorProceso (IdError, Descripcion) VALUES (8, 'Fechas de operación no coinciden con el nombre de archivo')
INSERT INTO CatErrorProceso (IdError, Descripcion) VALUES (9, 'Versión no válida')
INSERT INTO CatErrorProceso (IdError, Descripcion) VALUES (10, 'Monto Invalido')
INSERT INTO CatErrorProceso (IdError, Descripcion) VALUES (11, 'Solicitud Invalida')
INSERT INTO CatErrorProceso (IdError, Descripcion) VALUES (12, 'Sin archivo E')
INSERT INTO CatErrorProceso (IdError, Descripcion) VALUES (13, 'Tipo de linea no válido')

INSERT INTO CatErrorProceso (IdError, Descripcion) VALUES (99, 'Error en archivo Q')




