use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetRolesByUserId')
		and type='P')
	drop procedure dbo.spNE_GetRolesByUserId
go

create proc spNE_GetRolesByUserId (
                              @USER_CODE          numeric(5,0)    -- Код пользователя
)
as
begin
    select  U.PROP_CODE as Id, rtrim(P.PROP_NAME) as Name
    from    INTEGRAL..USER_PROPERTIES U join
            INTEGRAL..PROPERTIES P on P.PROP_CODE = U.PROP_CODE
    where   U.USER_CODE = @USER_CODE and
            U.PROP_CODE in (1050, 1051)
      /*
        1050	Network Equipments. Администратор
        1051	Network Equipments. Пользователь
      */
end

/*
  exec MEDIATE..spNE_GetRolesByUserId 3
*/