use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetStreetsByTownId')
		and type='P')
	drop procedure dbo.spNE_GetStreetsByTownId
go

create proc spNE_GetStreetsByTownId (
													@TOWN_CODE		numeric(5,0)
)
as
begin
    select  STREET_CODE as Id, rtrim(STREET_PREFIX) + case when STREET_PREFIX != null then ' ' + rtrim(STREET_NAME)
																											else rtrim(STREET_NAME) end as Name, TOWN_CODE as TownId
    from    INTEGRAL..STREETS
		where		TOWN_CODE = @TOWN_CODE
end


/*

  exec MEDIATE..spNE_GetStreetsByTownId 6201

*/