/*
 Plantilla de script anterior a la implementación							
--------------------------------------------------------------------------------------
 Este archivo contiene instrucciones de SQL que se ejecutarán antes del script de compilación	
 Use sintaxis de SQLCMD para incluir un archivo en el script anterior a la implementación			
 Ejemplo:      :r .\miarchivo.sql								
 Use sintaxis de SQLCMD para hacer referencia a una variable del script anterior a la implementación		
 Ejemplo:      :setvar TableName Mi tabla							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
-- :r .\TipoEquipo_IdCategoríaFK.sql
:r .\Participante.0.sql
:r .\BorrarTablasDeRelacion.sql
:r .\User.sql
:r .\Carrera.sql
:r .\DeleteControlsWithOutUse.sql
:r .\Participante.1.sql
:r .\Participante.2.sql
:r .\Control.0.sql
:r .\ControlXCarrera.0.sql
:r .\Resultados.0.sql
:r .\ConfiguracionResultados.0.sql
:r .\User.1.sql
:r .\Equipo.0.sql