use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_DelNetworkEquipment')
		and type='P')
	drop procedure dbo.spNE_DelNetworkEquipment
go

create proc spNE_DelNetworkEquipment (
                              @EQUIPMENT_CODE         numeric(10,0)
)
as
begin
    delete  MEDIATE..NE_NETWORK_EQUIPMENTS
    where   EQUIPMENT_CODE = @EQUIPMENT_CODE

		return 0
end

/*

  exec MEDIATE..spNE_DelNetworkEquipment 1

  select * from MEDIATE..NE_NETWORK_EQUIPMENTS
	select * from MEDIATE..NE_EQUIPMENT_TYPES
	select * from MEDIATE..NE_NETWORKS
*/