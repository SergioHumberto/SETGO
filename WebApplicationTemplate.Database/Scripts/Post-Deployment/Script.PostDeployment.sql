﻿/*
Plantilla de script posterior a la implementación							
--------------------------------------------------------------------------------------
 Este archivo contiene instrucciones de SQL que se anexarán al script de compilación		
 Use sintaxis de SQLCMD para incluir un archivo en el script posterior a la implementación			
 Ejemplo:      :r .\miarchivo.sql								
 Use sintaxis de SQLCMD para hacer referencia a una variable del script posterior a la implementación		
 Ejemplo:      :setvar TableName Mi tabla							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
:r .\PostData\Control.0.sql
--:r .\PostData\ControlXCarrera.0.sql ####  NO SE DEBE EJECUTAR EN PRODUCTIVO ####
:r .\ControlXCarrera.1.sql