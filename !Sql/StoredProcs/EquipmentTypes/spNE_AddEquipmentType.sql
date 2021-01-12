use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_AddEquipmentType')
		and type='P')
	drop procedure dbo.spNE_AddEquipmentType
go

create proc spNE_AddEquipmentType (
   														@TYPE_NAME        varchar(255),           -- Наименование типа оборудования
                              @POSITION         int,                    -- Позиция элемента в списке (для сортировки)
															@USER_CODE       	numeric(5,0)			      -- Код пользователя (fk INTEGRAL..USERS)
)
as
begin
    insert into MEDIATE..NE_EQUIPMENT_TYPES (TYPE_NAME, POSITION, USER_CODE, DATE_CHANGE)
    values (@TYPE_NAME, @POSITION, @USER_CODE, getdate())

		return @@identity
end


/*

  exec MEDIATE..spNE_AddEquipmentType 'WS-C2960-48TC-S', 2, 3

	select * from MEDIATE..NE_EQUIPMENT_TYPES
*/