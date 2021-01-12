use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetAddressById')
		and type='P')
	drop procedure dbo.spNE_GetAddressById
go

create proc spNE_GetAddressById (
																		@ADDRESS_CODE			numeric(10,0)
)
as
begin
		if @ADDRESS_CODE = null return -1

    select  ADDRESS_CODE as Id, cast(HOUSE  as varchar(5)) as House, rtrim(HOUSE_POSTFIX) as Corp,
						STREET_CODE as StreetId
    from    INTEGRAL..ADDRESS
		where		ADDRESS_CODE = @ADDRESS_CODE
end

/*

  exec MEDIATE..spNE_GetAddressById 5052413

*/
