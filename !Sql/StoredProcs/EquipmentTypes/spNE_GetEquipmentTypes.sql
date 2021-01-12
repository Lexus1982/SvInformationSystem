use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetEquipmentTypes')
		and type='P')
	drop procedure dbo.spNE_GetEquipmentTypes
go

create proc spNE_GetEquipmentTypes
as
begin
			select 	TYPE_CODE as Id, rtrim(TYPE_NAME) as Name, POSITION as Position, USER_CODE as UserId, DATE_CHANGE as ChangeDate
			from		MEDIATE..NE_EQUIPMENT_TYPES
end


/*

  exec MEDIATE..spNE_GetEquipmentTypes

*/