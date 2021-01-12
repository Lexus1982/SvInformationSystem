use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetUserById')
		and type='P')
	drop procedure dbo.spNE_GetUserById
go

create proc spNE_GetUserById (
												@USER_CODE 	numeric(5,0)		-- Код пользователя из INTEGRAL..USERS
)
as
begin
   select USER_CODE as Id, rtrim(USER_NAME) as Login, rtrim(USER_PASSWORD) as Password,	rtrim(FULL_NAME) as Name,
		 			GROUP_CODE as GroupId, rtrim(USER_ACTIVE) as Active
	 from 	INTEGRAL..USERS
	 where 	GROUP_SIGN is null and	-- Исключим все группы
					USER_CODE = @USER_CODE
end

/*
  exec MEDIATE..spNE_GetUserById 3
*/