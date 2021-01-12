use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_AddNetworkEquipment')
		and type='P')
	drop procedure dbo.spNE_AddNetworkEquipment
go

create proc spNE_AddNetworkEquipment (
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
    insert into MEDIATE..NE_NETWORK_EQUIPMENTS (NETWORK_CODE, ADDRESS_CODE, ENTRANCE, EQUIPMENT_TYPE_CODE, IP, COMMENTARY, USER_CODE, DATE_CHANGE)
    values (@NETWORK_CODE, @ADDRESS_CODE, @ENTRANCE, @EQUIPMENT_TYPE_CODE, @IP, @COMMENTARY, @USER_CODE, getdate())

		return @@identity
end

/*

  exec MEDIATE..spNE_AddNetworkEquipment 1, 5052413, 2, 1, '123.123.213.123', 3

	select * from MEDIATE..NE_EQUIPMENT_TYPES
	select * from MEDIATE..NE_NETWORKS
*/