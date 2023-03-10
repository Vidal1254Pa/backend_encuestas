-- Disable all referential integrity constraints
EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
GO

-- Drop all PKs and FKs
declare @sql nvarchar(max)
SELECT @sql = STUFF((SELECT '; ' + 'ALTER TABLE ' + Table_Name  +'  drop constraint ' + Constraint_Name  from Information_Schema.CONSTRAINT_TABLE_USAGE ORDER BY Constraint_Name FOR XML PATH('')),1,1,'')
EXECUTE (@sql)
GO

-- Drop all tables
EXEC sp_msforeachtable 'DROP TABLE ?'
GO

-- Script para renombrar tabla
--EXEC sp_rename 'dbo.ConfiguracionDocumentoSustento', 'TbConfiguracionDocumentoSustento';  
--GO   

-- Script para renombrar columna
--EXEC sp_rename 'dbo.TbProveedorTipoContacto.chDescripcion','vcDescripcion','COLUMN';  