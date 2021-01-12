use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_DelNetwork')
		and type='P')
	drop procedure dbo.spNE_DelNetwork
go

create proc spNE_DelNetwork (
															@NETWORK_CODE  		numeric(10,0)
)
as
begin
		delete 	MEDIATE..NE_NETWORKS
		where		NETWORK_CODE = @NETWORK_CODE

		return 0
end


/*

		exec MEDIATE..spNE_DelNetwork 3

  --
  select * from MEDIATE..NE_NETWORKS

*/