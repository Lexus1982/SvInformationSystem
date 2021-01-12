use MEDIATE
GO

-- drop table MEDIATE..NE_NETWORKS
create table  NE_NETWORKS (
  NETWORK_CODE    numeric(10,0)   identity not null,      --
  ADDRESS_CODE    numeric(10,0)   not null,               -- ��� ������ (fk INTEGRAL..ADDRESS)
  SEGMENT_NUM     int             null,                   -- � ��������
  VLAN_MANAGE     varchar(20)     null,                   -- VLAN ����������
  VLAN_INET       varchar(20)     null,                   -- VLAN ��������
  IP_INTERVAL     varchar(250)    null,                   -- �������� IP
  COMMENTARY      varchar(250)    null,                   -- ����������
	USER_CODE       numeric(5,0)    not null,               -- ��� ������������ (fk INTEGRAL..USERS)
  DATE_CHANGE     datetime        not null,               -- ���� ���������� ���������

	CONSTRAINT PK_NETWORK_CODE PRIMARY KEY CLUSTERED ( NETWORK_CODE )  on 'default'
)

--��������� ����������� null ��������, ����� unique ������, �������� ������������ (�� null ��������) ����������� � �����������
--CREATE UNIQUE INDEX UI_NE_NETWORKS_IP_INTERVAL ON NE_NETWORKS (IP_INTERVAL)

ALTER TABLE MEDIATE.dbo.NE_NETWORKS
ADD CONSTRAINT FK_NE_NETWORKS_ADDRESS_CODE
FOREIGN KEY ( ADDRESS_CODE )
REFERENCES INTEGRAL.dbo.ADDRESS (ADDRESS_CODE)
go

ALTER TABLE MEDIATE.dbo.NE_NETWORKS
ADD CONSTRAINT FK_NE_NETWORKS_USER_CODE
FOREIGN KEY ( USER_CODE )
REFERENCES INTEGRAL.dbo.USERS (USER_CODE)
go

/*
    select * from MEDIATE..NE_NETWORKS

      exec MEDIATE..spNE_AddNetwork 5052413, 111, '1111','1111', '192.168.33.0', 3
      exec MEDIATE..spNE_AddNetwork 5052413, 222, '2222','2222', '192.168.33.2', 3
      exec MEDIATE..spNE_AddNetwork 5052413, 333, '3333','3333', '192.168.33.3', 3
      exec MEDIATE..spNE_AddNetwork 5052413, 444, '4444','4444', '192.168.33.4', 3

  exec MEDIATE..spNE_GetNetworks
*/