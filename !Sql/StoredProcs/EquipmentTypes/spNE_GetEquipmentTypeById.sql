use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetEquipmentTypeById')
		and type='P')
	drop procedure dbo.spNE_GetEquipmentTypeById
go

create proc spNE_GetEquipmentTypeById (
                                @TYPE_CODE             numeric(10,0)
)
as
begin
			select 	TYPE_CODE as Id, rtrim(TYPE_NAME) as Name, POSITION as Position, USER_CODE as UserId, DATE_CHANGE as ChangeDate
			from		MEDIATE..NE_EQUIPMENT_TYPES
      where   TYPE_CODE = @TYPE_CODE
end


/*

  exec MEDIATE..spNE_GetEquipmentTypeById 2


*/