use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_SearchEquipmentsInNetwork')
		and type='P')
	drop procedure dbo.spNE_SearchEquipmentsInNetwork
go

create proc spNE_SearchEquipmentsInNetwork (
                                        @NETWORK_CODE         numeric(10,0),
																				@SEARCH_VALUE         varchar(250)
)
as
begin
		select  EQUIPMENT_CODE as Id, NETWORK_CODE as NetworkId, ADDRESS_CODE as AddressId, ENTRANCE as Entrance,
						EQUIPMENT_TYPE_CODE as EquipmentTypeId, rtrim(IP) as Ip,
						rtrim(COMMENTARY) as Commentary, USER_CODE as UserId, DATE_CHANGE as ChangeDate
		from 		MEDIATE..NE_NETWORK_EQUIPMENTS
		where 	NETWORK_CODE = @NETWORK_CODE and
           (
                ADDRESS_CODE in (
                select  ADDRESS_CODE
                from    INTEGRAL..ADDRESS A join
                        INTEGRAL..STREETS S on S.STREET_CODE = A.STREET_CODE join
                        INTEGRAL..TOWNS T on T.TOWN_CODE = S.TOWN_CODE
                where   upper(T.TOWN_PREFIX + ' ' + T.TOWN_NAME + ' ' + S.STREET_PREFIX + ' ' + S.STREET_NAME + ' ' +
                        cast(A.HOUSE as varchar(50)) + ' ' + A.HOUSE_POSTFIX )like '%' + upper(rtrim(@SEARCH_VALUE)) + '%'
              ) or  upper(cast(ENTRANCE as varchar(50)) + ' ' +  IP) like '%' + upper(rtrim(@SEARCH_VALUE)) + '%'
          )

		return 0
end

/*

  exec MEDIATE..spNE_SearchEquipmentsInNetwork 1, '.1'

  select * from MEDIATE..NE_NETWORK_EQUIPMENTS

*/