use MEDIATE
GO

-- drop table MEDIATE..NE_NETWORK_EQUIPMENTS
create table  NE_NETWORK_EQUIPMENTS (
  EQUIPMENT_CODE        numeric(10,0)   identity not null,      --
  NETWORK_CODE          numeric(10,0)   not null,               -- Код диапазона сети (fk MEDIATE..NE_NETWORK)
  ADDRESS_CODE          numeric(10,0)   not null,               -- Код адреса (fk INTEGRAL..ADDRESS)
  ENTRANCE              int             null,                   -- № подъезда
  EQUIPMENT_TYPE_CODE   numeric(10,0)   not null,               -- Код типа оборудования (fk MEDIATE..NE_EQUIPMENT_TYPES)
  IP                    varchar(15)     null,                   -- IP адрес оборудования
  COMMENTARY            varchar(250)    null,                   -- Примечание
	USER_CODE             numeric(5,0)    not null,               -- Код пользователя (fk INTEGRAL..USERS)
  DATE_CHANGE           datetime        not null,               -- Дата последнего изменения

	CONSTRAINT PK_EQUIPMENT_CODE PRIMARY KEY CLUSTERED ( EQUIPMENT_CODE )  on 'default'
)

CREATE UNIQUE INDEX UI_NE_NETWORKS_IP ON NE_NETWORK_EQUIPMENTS (IP)

ALTER TABLE MEDIATE.dbo.NE_NETWORK_EQUIPMENTS
ADD CONSTRAINT FK_NE_NETWORK_EQUIPMENTS_NETWORK_CODE
FOREIGN KEY ( NETWORK_CODE )
REFERENCES MEDIATE.dbo.NE_NETWORKS (NETWORK_CODE)

ALTER TABLE MEDIATE.dbo.NE_NETWORK_EQUIPMENTS
ADD CONSTRAINT FK_NE_NETWORK_EQUIPMENTS_ADDRESS_CODE
FOREIGN KEY ( ADDRESS_CODE )
REFERENCES INTEGRAL.dbo.ADDRESS (ADDRESS_CODE)
go

ALTER TABLE MEDIATE.dbo.NE_NETWORK_EQUIPMENTS
ADD CONSTRAINT FK_NE_NETWORK_EQUIPMENTS_USER_CODE
FOREIGN KEY ( USER_CODE )
REFERENCES INTEGRAL.dbo.USERS (USER_CODE)
go

ALTER TABLE MEDIATE.dbo.NE_NETWORK_EQUIPMENTS
ADD CONSTRAINT FK_NE_NETWORK_EQUIPMENTS_TYPE_CODE
FOREIGN KEY ( EQUIPMENT_TYPE_CODE )
REFERENCES MEDIATE.dbo.NE_EQUIPMENT_TYPES (TYPE_CODE)
go

/*
    select * from MEDIATE..NE_NETWORK_EQUIPMENTS

  exec MEDIATE..spNE_AddNetworkEquipment 1, 5052413, 1, 1, '192.168.33.1', 3
  exec MEDIATE..spNE_AddNetworkEquipment 1, 5052413, 2, 1, '192.168.33.2', 3
*/
