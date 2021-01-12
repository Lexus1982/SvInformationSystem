use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_DelEquipmentType')
		and type='P')
	drop procedure dbo.spNE_DelEquipmentType
go

create proc spNE_DelEquipmentType (
															@TYPE_CODE       	numeric(10,0)
)
as
begin
		if @TYPE_CODE is null
				return -1

    delete	MEDIATE..NE_EQUIPMENT_TYPES
		where		TYPE_CODE = @TYPE_CODE

		return 0
end


/*
	--
	-- Удаление типа оборудования
  exec MEDIATE..spNE_DelEquipmentType 1

	select * from MEDIATE..NE_EQUIPMENT_TYPES
*/