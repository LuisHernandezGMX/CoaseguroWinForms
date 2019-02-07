-- Único parámetro
Declare @IdPv As Int = 453292;
--Declare @IdPv As Int = 716598;

-- ***** sp_CedulaParticipacionCoaseguro(Int @IdPv) *****
-- Procedimiento que extrae la información para la carátula 'Cédula de Participación en Coaseguro'

Select '***** sp_CedulaParticipacionCoaseguro(Int @IdPv) *****';

-- Datos Generales y Participación GMX
Select
    Cast(header.cod_suc As Varchar(15)) + '-'
        + Cast(header.cod_ramo As Varchar(15)) + '-'
        + Cast(header.nro_pol As Varchar(15)) + '-'
        + Cast(header.nro_endoso As Varchar(15)) + '-'
        + Cast(header.aaaa_endoso As Varchar(15))
    As [Poliza],
    IsNull(mpersona.txt_apellido1, '') + ' '
        + IsNull(mpersona.txt_apellido2, '') + ' '
        + IsNull(mpersona.txt_nombre, '')
    As [Asegurado],
    coaseguro.PorcentajeGMX,
    coaseguro.MontoParticipacionGMX,
    header.fec_emi As [FechaEmision]
From [pv_header] As header
    Inner Join [maseg_header] As maseg On header.cod_aseg = maseg.cod_aseg
    Inner Join [mpersona] As mpersona On maseg.id_persona = mpersona.id_persona
    Inner Join [CoaseguroPrincipal] As coaseguro On header.id_pv = coaseguro.id_pv
Where header.id_pv = @IdPv;

-- Participación Coaseguradoras Seguidoras

Select
    mcia.txt_nom_cia As [Coaseguradora],
    participantes.PorcentajeParticipacion,
    participantes.MontoParticipacion
From [CoaseguroPrincipal] As coaseguro
    Inner Join [CoaseguradorasParticipantes] As participantes On coaseguro.Id = participantes.IdCoaseguro
    Inner Join [mcia] As mcia On participantes.cod_cia = mcia.cod_cia
Where coaseguro.id_pv = @IdPv;


-- ***** sp_AnexoCondicionesParticularesCoaseguro(Int @IdPv) *****
-- Procedimiento que extrae la información para la carátula 'Anexo de Condiciones Particulares de Coaseguro'

Select '***** sp_AnexoCondicionesParticularesCoaseguro(Int @IdPv) *****';

-- Datos Generales
Select Distinct
    IsNull(mpersona.txt_apellido1, '') + ' '
        + IsNull(mpersona.txt_apellido2, '') + ' '
        + IsNull(mpersona.txt_nombre, '')
    As [Asegurado],
    mpersona.nro_nit As [RFC],
    mpersonaDir.txt_direccion As [DomicilioFiscal],
    Substring(giroNegocio.txt_desc, CharIndex('-', giroNegocio.txt_desc) + 1, Len(giroNegocio.txt_desc) - CharIndex('-', giroNegocio.txt_desc)) As [Giro],
    header.nro_pol As [PolizaLider],
    varios.fec_vig_hasta_orig As [Vigencia]
From [pv_header] As header
    Inner Join [maseg_header] As maseg On header.cod_aseg = maseg.cod_aseg
    Inner Join [mpersona] As mpersona On maseg.id_persona = mpersona.id_persona
    Inner Join [mpersona_dir] As mpersonaDir On mpersona.id_persona = mpersonaDir.id_persona
    Inner Join [pv_varios] As varios On header.id_pv = varios.id_pv
    Inner Join [di_reas] As reas On header.id_pv = reas.id_pv
    Inner Join [tgiro_negocio] As giroNegocio On reas.cod_giro_negocio = giroNegocio.cod_giro_negocio
Where header.id_pv = @IdPv;

-- Ramo GMX
Select Distinct
    ramo.txt_desc As [Ramo],   
    coaseguro.PorcentajeGMX
From [CoaseguroPrincipal] As coaseguro
    Inner Join [pv_header] As header On coaseguro.id_pv = header.id_pv
    Inner Join [di_tarif] As tarif On header.id_pv = tarif.id_pv
    Inner Join [di_cober] As cober
        On tarif.cod_SIC = cober.cod_ind_cob 
        And tarif.id_pv = cober.id_pv
    Inner Join [tramo] As ramo On cober.cod_ramo = ramo.cod_ramo
Where coaseguro.id_pv = @IdPv;

-- Ramos, Porcentajes y Fees Coaseguradoras Seguidoras
Select Distinct
    ramo.txt_desc As [Ramo],
    mcia.txt_nom_cia As [Coaseguradora],
    participantes.PorcentajeParticipacion,
    fee.PorcentajeFee,
    fee.MontoFee
From [CoaseguroPrincipal] As coaseguro
    Inner Join [pv_header] As header On coaseguro.id_pv = header.id_pv
    Inner Join [di_tarif] As tarif On header.id_pv = tarif.id_pv
    Inner Join [di_cober] As cober
        On tarif.cod_SIC = cober.cod_ind_cob 
        And tarif.id_pv = cober.id_pv
    Inner Join [tramo] As ramo On cober.cod_ramo = ramo.cod_ramo
    Inner Join [CoaseguradorasParticipantes] As participantes On coaseguro.Id = participantes.IdCoaseguro
    Inner Join [CoaseguradorasFee] As fee On coaseguro.Id = fee.IdCoaseguro
    Inner Join [mcia] As mcia On participantes.cod_cia = mcia.cod_cia
Where coaseguro.id_pv = @IdPv;

-- Info Adicional
Select Distinct
    moneda.txt_desc As [Moneda],
    formaPago.txt_desc As [FormaPago],
    metodoPago.Descripcion As [MetodoPago],
    garantiaPago.Descripcion As [GarantiaPago],
    comisionAgente.Descripcion As [PagoComisionAgente],
    pagoSiniestro.Descripcion As [PagoSiniestro],
    Case
        When pagoSiniestro.Id = 1 Then 'No Aplica'
        Else Cast(coaseguro.PorcentajeSiniestro As Varchar(20))
    End As [PorcentajeSiniestro],
    Case
        When pagoSiniestro.Id = 1 Then 'No Aplica'
        Else Cast(coaseguro.MontoSiniestro As Varchar(20))
    End As [MontoSiniestro]
From [CoaseguroPrincipal] As coaseguro
    Inner Join [tmoneda] As moneda On coaseguro.cod_moneda = moneda.cod_moneda
    Inner Join [pv_pagador] As pagador On coaseguro.id_pv = pagador.id_pv
    Inner Join [tforma_pago] As formaPago On pagador.cod_periodo_pago = formaPago.cod_forma_pago
    Inner Join [MetodoPagoCoaseguro] As metodoPago On coaseguro.IdMetodoPago = metodoPago.Id
    Inner Join [PagoComisionAgenteCoaseguro] As comisionAgente On coaseguro.IdPagoComisionAgente = comisionAgente.Id
    Inner Join [PagoSiniestroCoaseguro] As pagoSiniestro On coaseguro.IdPagoSiniestro = pagoSiniestro.Id
    Inner Join [GarantiaPagoCoaseguro] As garantiaPago On coaseguro.IdGarantiaPago = garantiaPago.Id
Where coaseguro.id_pv = @IdPv;

--select * from pv_header_wkf where id_pv = 656228