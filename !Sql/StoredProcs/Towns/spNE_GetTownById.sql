use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetTownById')
		and type='P')
	drop procedure dbo.spNE_GetTownById
go

create proc spNE_GetTownById (
														@TOWN_CODE	numeric(5,0)
)
as
begin
    select  TOWN_CODE as Id, rtrim(TOWN_PREFIX) + ' ' + rtrim(TOWN_NAME) as Name
    from    INTEGRAL..TOWNS
		where		TOWN_CODE = @TOWN_CODE
end


/*

  exec MEDIATE..spNE_GetTownById 1

*/