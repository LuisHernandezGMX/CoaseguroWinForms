-- **** Coaseguradora Líder
-- Consultas de prueba y auxiliares en el desarrollo.
-- *****************************************************

--Declare @IdPv Int = 711062; -- Coaseguro con cifras altas.
Declare @IdPv Int = 520179;   -- Coaseguro negativo (devolución) con más de un seguidor

-- Consulta pequeña para obtener coaseguros con más de un seguidor.

--Select Top 640 coas.*
--From [pv_header] As header
--    Inner Join [pv_coas_cia] As coas On header.id_pv = coas.id_pv
--Where header.cod_operacion = 3
--Order By header.id_pv Desc;

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

-- Obtener coaseguradoras
Select
    mcia.txt_nom_cia As Coaseguradora,
    coasCia.cod_cia_part As IdCoaseguradora,
    coasCia.pje_part_prima As PorcentajePrima,
    coasCia.imp_prima As MontoPrima
From [pv_coas_cia] As coasCia
    Inner Join [mcia] As mcia On coasCia.cod_cia_part = mcia.cod_cia
Where id_pv = @IdPv;

-- Obtener Límite Máximo de Responsabilidad y Prima Neta
Select
    imp_suma_asegurada As LimiteMaximoResponsabilidad,
    imp_prima As PrimaNeta
From [pv_importe_coas]
Where
    id_pv = @IdPv
    And cod_moneda = @IdMoneda;