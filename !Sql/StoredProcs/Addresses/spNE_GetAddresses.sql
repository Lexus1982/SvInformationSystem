use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetAddresses')
		and type='P')
	drop procedure dbo.spNE_GetAddresses
go

create proc spNE_GetAddresses
as
begin
		-- smallint between -32768 and 32767 = 2 bytes
    select  ADDRESS_CODE as Id, cast(HOUSE  as varchar(5)) as House, rtrim(HOUSE_POSTFIX) as Corp,
						STREET_CODE as StreetId
    from    INTEGRAL..ADDRESS

	/*
	-- Если HOUSE_POSTFIX начинается не с цифры, то в корпус записываем HOUSE_POSTFIX
    update  #AddressData
    set     FULL_HOUSE = HOUSE,
            FULL_HOUSE_POSTFIX = HOUSE_POSTFIX
    WHERE   len(HOUSE_POSTFIX) > 1 and MEDIATE.dbo.IsDigitString(substring(HOUSE_POSTFIX, 1, 1)) = 1

    -- Если HOUSE_POSTFIX начинается с цифры, то корпус = null, данные HOUSE_POSTFIX пишем в HOUSE
    update  #AddressData
    set     FULL_HOUSE = HOUSE + HOUSE_POSTFIX
    WHERE   not ( len(HOUSE_POSTFIX) > 1 AND MEDIATE.dbo.IsDigitString(substring(HOUSE_POSTFIX, 1, 1)) = 1 )

*/
end

/*

  exec MEDIATE..spNE_GetAddresses

*/
