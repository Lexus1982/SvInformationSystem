use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_UpdEquipmentType')
		and type='P')
	drop procedure dbo.spNE_UpdEquipmentType
go

create proc spNE_UpdEquipmentType (
															@TYPE_CODE       	numeric(10,0),
   														@TYPE_NAME        varchar(255),           -- Наименование типа оборудования
                              @POSITION         int,                    -- Позиция элемента в списке (для сортировки)
															@USER_CODE       	numeric(5,0)			      -- Код пользователя (fk INTEGRAL..USERS)
)
as
begin
		if @TYPE_CODE is null or @TYPE_NAME is null
				return -1

    update 	MEDIATE..NE_EQUIPMENT_TYPES
		set 		TYPE_NAME = rtrim(@TYPE_NAME),
						POSITION = @POSITION,
						USER_CODE = @USER_CODE,
						DATE_CHANGE = getdate()
    where		TYPE_CODE = @TYPE_CODE

		return 0
end


/*

  exec MEDIATE..spNE_UpdEquipmentType 1, 'WS-C2960-24TC-S', 1, 3

	select * from MEDIATE..NE_EQUIPMENT_TYPES
*/