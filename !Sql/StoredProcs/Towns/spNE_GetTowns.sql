use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetTowns')
		and type='P')
	drop procedure dbo.spNE_GetTowns
go

create proc spNE_GetTowns
as
begin
    select  T.TOWN_CODE as Id, rtrim(T.TOWN_PREFIX) + ' ' + rtrim(T.TOWN_NAME) as Name
    from    INTEGRAL..TOWNS T join
						INTEGRAL..TOWN_NODES N on N.TOWN_CODE = T.TOWN_CODE
end


/*

  exec MEDIATE..spNE_GetTowns

*/