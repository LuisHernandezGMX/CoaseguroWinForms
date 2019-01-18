-- Consultas generales en tablas de Coaseguro


declare @IdPv Int = 520179; -- Coaseguro Líder
--declare @IdPv Int = 716598; -- Coaseguro Seguidor

Select imp_cambio From [pv_header] Where id_pv = @IdPv;

Select coas.*
From [CoaseguroPrincipal] As coas
    Inner Join [pv_header] As header On coas.id_pv = header.id_pv
Where header.id_pv = @IdPv;

Select coas.*
From [CoaseguradorasParticipantes] As coas
    Inner Join [CoaseguroPrincipal] As principal On coas.IdCoaseguro = principal.Id
    Inner Join [pv_header] As header On principal.id_pv = header.id_pv
Where header.id_pv = @IdPv;

Select coas.*
From [CoaseguradorasFee] As coas
    Inner Join [CoaseguroPrincipal] As principal On coas.IdCoaseguro = principal.Id
    Inner Join [pv_header] As header On principal.id_pv = header.id_pv
Where header.id_pv =  @IdPv;