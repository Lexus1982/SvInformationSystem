use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_UpdNetwork')
		and type='P')
	drop procedure dbo.spNE_UpdNetwork
go

create proc spNE_UpdNetwork (
															@NETWORK_CODE  		numeric(10,0),
   														@ADDRESS_CODE    	numeric(10,0),           	-- Код адреса (fk INTEGRAL..ADDRESS)
   														@SEGMENT_NUM     	int,                   		-- № сегмента
   														@VLAN_MANAGE     	varchar(20),             	-- VLAN управления
   														@VLAN_INET       	varchar(20),             	-- VLAN интернет
                              @COMMENTARY       varchar(250),            	--
  														@IP_INTERVAL     	varchar(250),            	-- Диапазон IP
															@USER_CODE       	numeric(5,0)			        -- Код пользователя (fk INTEGRAL..USERS)
)
as
begin
		update 	MEDIATE..NE_NETWORKS
		set 		ADDRESS_CODE = @ADDRESS_CODE,
						SEGMENT_NUM = @SEGMENT_NUM,
						VLAN_MANAGE = rtrim(@VLAN_MANAGE),
						VLAN_INET = rtrim(@VLAN_INET),
						IP_INTERVAL = rtrim(@IP_INTERVAL),
            COMMENTARY = @COMMENTARY,
						USER_CODE = @USER_CODE,
						DATE_CHANGE = getdate()
		where		NETWORK_CODE = @NETWORK_CODE

		return 0
end


/*

  exec MEDIATE..spNE_UpdNetwork 1, 5052413, 222, '333','444', 'IP2', 3

*/