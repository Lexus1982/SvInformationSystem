use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetStreetById')
		and type='P')
	drop procedure dbo.spNE_GetStreetById
go

create proc spNE_GetStreetById (
													@STREET_CODE		numeric(10,0)
)
as
begin
    select  STREET_CODE as Id, rtrim(STREET_PREFIX) + case when STREET_PREFIX != null then ' ' + rtrim(STREET_NAME)
																											else rtrim(STREET_NAME) end as Name, TOWN_CODE as TownId
    from    INTEGRAL..STREETS
		where		STREET_CODE = @STREET_CODE
end


/*

  exec MEDIATE..spNE_GetStreetById 5061027

*/