use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_AddNetwork')
		and type='P')
	drop procedure dbo.spNE_AddNetwork
go

create proc spNE_AddNetwork (
   														@ADDRESS_CODE    	numeric(10,0),           	-- ��� ������ (fk INTEGRAL..ADDRESS)
   														@SEGMENT_NUM     	int,                   		-- � ��������
   														@VLAN_MANAGE     	varchar(20),             	-- VLAN ����������
   														@VLAN_INET       	varchar(20),             	-- VLAN ��������
  														@IP_INTERVAL     	varchar(250),            	-- �������� IP
                              @COMMENTARY       varchar(250),            	--
															@USER_CODE       	numeric(5,0)			        -- ��� ������������ (fk INTEGRAL..USERS)
)
as
begin
		insert into MEDIATE..NE_NETWORKS (ADDRESS_CODE, SEGMENT_NUM, VLAN_MANAGE, VLAN_INET, IP_INTERVAL, COMMENTARY, USER_CODE, DATE_CHANGE)
		values (@ADDRESS_CODE, @SEGMENT_NUM, @VLAN_MANAGE, @VLAN_INET, @IP_INTERVAL, @COMMENTARY, @USER_CODE, getdate())

		return @@identity
end


/*

  exec MEDIATE..spNE_AddNetwork 5052413, 222, '333','444', 'IP2', 3

*/