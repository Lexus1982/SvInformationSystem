use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetNetworkEquipments')
		and type='P')
	drop procedure dbo.spNE_GetNetworkEquipments
go

create proc spNE_GetNetworkEquipments
as
begin
		select  EQUIPMENT_CODE as Id, NETWORK_CODE as NetworkId, ADDRESS_CODE as AddressId, ENTRANCE as Entrance,
						EQUIPMENT_TYPE_CODE as EquipmentTypeId, rtrim(IP) as Ip,
						rtrim(COMMENTARY) as Commentary, USER_CODE as UserId, DATE_CHANGE as ChangeDate
		from 		MEDIATE..NE_NETWORK_EQUIPMENTS
		return 0
end

/*

  exec MEDIATE..spNE_GetNetworkEquipments

  select * from MEDIATE..NE_NETWORK_EQUIPMENTS

*/