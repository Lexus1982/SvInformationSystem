use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetEquipmentsByNetworkId')
		and type='P')
	drop procedure dbo.spNE_GetEquipmentsByNetworkId
go

create proc spNE_GetEquipmentsByNetworkId (
                              @NETWORK_CODE         numeric(10,0)
)
as
begin
		select  EQUIPMENT_CODE as Id, NETWORK_CODE as NetworkId, ADDRESS_CODE as AddressId, ENTRANCE as Entrance,
						EQUIPMENT_TYPE_CODE as EquipmentTypeId, rtrim(IP) as Ip, 
            rtrim(COMMENTARY) as Commentary, USER_CODE as UserId, DATE_CHANGE as ChangeDate
		from 		MEDIATE..NE_NETWORK_EQUIPMENTS
		where		NETWORK_CODE = @NETWORK_CODE

		return 0
end

/*

  exec MEDIATE..spNE_GetEquipmentsByNetworkId 1

  select * from MEDIATE..NE_NETWORK_EQUIPMENTS

*/