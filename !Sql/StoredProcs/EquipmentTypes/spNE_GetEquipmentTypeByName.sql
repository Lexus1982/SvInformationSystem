use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetEquipmentTypeByName')
		and type='P')
	drop procedure dbo.spNE_GetEquipmentTypeByName
go

create proc spNE_GetEquipmentTypeByName (
                                @TYPE_NAME             varchar(255)
)
as
begin
			select 	TYPE_CODE as Id, rtrim(TYPE_NAME) as Name, POSITION as Position, USER_CODE as UserId, DATE_CHANGE as ChangeDate
			from		MEDIATE..NE_EQUIPMENT_TYPES
      where   TYPE_NAME = @TYPE_NAME
end


/*

  exec MEDIATE..spNE_GetEquipmentTypeByName 'WS-C2960-24TC-S'


*/