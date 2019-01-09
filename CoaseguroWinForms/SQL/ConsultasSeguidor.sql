-- **** Coaseguro Seguidor
-- Consultas de prueba y auxiliares en el desarrollo.
-- *****************************************************

-- Consulta pequeña para obtener coaseguros de tipo seguidor.

--Select lider.*
--From [pv_header] As header
--    Inner Join [pv_cia_lider] As lider On header.id_pv = lider.id_pv
--Where header.cod_operacion = 2
--Order By header.id_pv Desc;

-- Búsqueda de tablas.

--Select *
--From Information_Schema.Tables
--Where Table_Name Like '%coas%'
--Order By Table_Name;

Declare @IdPv Int = 716598;

-- Obtener divisa
Declare @IdMoneda Decimal;

Select @IdMoneda = tmoneda.cod_moneda
From [pv_header] As header
    Inner Join [tmoneda] As tmoneda On header.cod_moneda = tmoneda.cod_moneda
Where header.id_pv = @IdPv;

Select
    tmoneda.cod_moneda As Id,
    tmoneda.txt_desc_redu As Simbolo,
    tmoneda.txt_desc As Descripcion
From [pv_header] As header
    Inner Join [tmoneda] As tmoneda On header.cod_moneda = tmoneda.cod_moneda
Where id_pv = @IdPv;

-- Obtener Participación de GMX
Select
    imp_suma_asegurada As MontoParticipacionGMX,
    imp_prima As MontoPrimaGMX
From [pv_importe]
Where
    id_pv = @IdPv
    And cod_moneda = @IdMoneda;

-- Obtener coaseguradora líder.
Select
    mcia.txt_nom_cia As [Líder],
    lider.cod_cia As IdCoaseguradora,
    lider.pje_partic As PorcentajeCoaseguradora
From [pv_cia_lider] As lider
    Inner Join [mcia] As mcia On lider.cod_cia = mcia.cod_cia
Where lider.id_pv = @IdPv;