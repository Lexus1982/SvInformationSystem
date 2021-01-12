use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_UpdNetworkEquipment')
		and type='P')
	drop procedure dbo.spNE_UpdNetworkEquipment
go

create proc spNE_UpdNetworkEquipment (
                              @EQUIPMENT_CODE         numeric(10,0),
                              @NETWORK_CODE           numeric(10,0),          -- Код диапазона сети (fk MEDIATE..NE_NETWORK)
                              @ADDRESS_CODE           numeric(10,0),          -- Код адреса (fk INTEGRAL..ADDRESS)
                              @ENTRANCE               int,                    -- № подъезда
                              @EQUIPMENT_TYPE_CODE    numeric(10,0),          -- Код типа оборудования (fk MEDIATE..NE_EQUIPMENT_TYPES)
                              @IP                     varchar(250),           -- IP адрес оборудования
                              @COMMENTARY       			varchar(250),           --
															@USER_CODE       	      numeric(5,0)			      -- Код пользователя (fk INTEGRAL..USERS)
)
as
begin
    update  MEDIATE..NE_NETWORK_EQUIPMENTS
    set     NETWORK_CODE = @NETWORK_CODE,
            ADDRESS_CODE = @ADDRESS_CODE,
            ENTRANCE = @ENTRANCE,
            EQUIPMENT_TYPE_CODE = @EQUIPMENT_TYPE_CODE,
            IP = @IP,
            COMMENTARY = @COMMENTARY,
            USER_CODE = @USER_CODE,
            DATE_CHANGE = getdate()
    where   EQUIPMENT_CODE = @EQUIPMENT_CODE

		return 0
end

/*

  exec MEDIATE..spNE_UpdNetworkEquipment 1, 1, 5052413, 2, 1, '456.123.213.456', 3

  select * from MEDIATE..NE_NETWORK_EQUIPMENTS
	select * from MEDIATE..NE_EQUIPMENT_TYPES
	select * from MEDIATE..NE_NETWORKS
*/