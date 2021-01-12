use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetAddressesByStreetId')
		and type='P')
	drop procedure dbo.spNE_GetAddressesByStreetId
go

create proc spNE_GetAddressesByStreetId (
																		@STREET_CODE			numeric(10,0)
)
as
begin
		if @STREET_CODE = null return -1

    select  ADDRESS_CODE as Id, cast(HOUSE  as varchar(5)) as House, rtrim(HOUSE_POSTFIX) as Corp,
						STREET_CODE as StreetId
    from    INTEGRAL..ADDRESS
		where		STREET_CODE = @STREET_CODE
end

/*

  exec MEDIATE..spNE_GetAddressesByStreetId 50003

*/
