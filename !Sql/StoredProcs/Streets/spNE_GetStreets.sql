use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetStreets')
		and type='P')
	drop procedure dbo.spNE_GetStreets
go

create proc spNE_GetStreets
as
begin
    select  STREET_CODE as Id, rtrim(STREET_PREFIX) + case when STREET_PREFIX != null then ' ' + rtrim(STREET_NAME) 
																											else rtrim(STREET_NAME) end as Name, TOWN_CODE as TownId
    from    INTEGRAL..STREETS
end


/*

  exec MEDIATE..spNE_GetStreets

*/