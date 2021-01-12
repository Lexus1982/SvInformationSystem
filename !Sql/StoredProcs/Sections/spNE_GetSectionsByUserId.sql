use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetSectionsByUserId')
		and type='P')
	drop procedure dbo.spNE_GetSectionsByUserId
go

create proc spNE_GetSectionsByUserId (
																	@GROUP_CODE     numeric(10,0),
                                  @USER_CODE     	numeric(5,0)
)
as
declare	@cId							varchar(250),
				@cSectionCode   	numeric(10,0)
begin
		create table #Sections (
			SECTION_CODE    numeric(10,0)   not null,      					--
  		PARENT_CODE     numeric(10,0)   null,                   -- SECTION_CODE родителя
  		SECTION_NAME    varchar(255)    not null,               -- Наименование секции
  		--GROUP_CODE      numeric(10,0)   not null,               -- Код группы секции (1 - Администрирование, 2 - Отчетность)
  		TYPE_CODE       numeric(10,0)   not null,               -- Код типа секции (1 - Раздел/подраздел, 2 - Отчет)
			ORDER_SORT			varchar(255)  	null										--
		)
	create table #ParentSections (
			ID							numeric(10,0)		identity not null,
			SECTION_CODE    numeric(10,0)   not null
		)

		-- Формируем список отчетов к которым предоставлен доступ
		insert into #Sections (SECTION_CODE, PARENT_CODE, SECTION_NAME, /*GROUP_CODE,*/ TYPE_CODE)
		select	S.SECTION_CODE, S.PARENT_CODE, S.SECTION_NAME, /*S.GROUP_CODE,*/ S.TYPE_CODE
		from 		MEDIATE..NE_SECTIONS S join
						(
								select 	SECTION_CODE
								from 		MEDIATE..NE_SECTIONS_USER_ACCESS_RIGHTS
								where		USER_CODE = @USER_CODE
								union
								select 	SECTION_CODE
								from 		MEDIATE..NE_SECTIONS_ROLE_ACCESS_RIGHTS
								where		ROLE_CODE IN (
														    select  U.PROP_CODE
								 								from    INTEGRAL..USER_PROPERTIES U
								         				where   U.USER_CODE = @USER_CODE
												)
						) as A on A.SECTION_CODE = S.SECTION_CODE
		where		S.GROUP_CODE = @GROUP_CODE and
						S.TYPE_CODE = 2

		-- Сформируем список ID родительских секций
		insert into #ParentSections (SECTION_CODE)
		select distinct PARENT_CODE
		from #Sections

		update #Sections
		set		ORDER_SORT = cast(ID as varchar(10))
		from	#Sections S join
					#ParentSections PS on PS.SECTION_CODE = S.PARENT_CODE

		declare sect_cur cursor for select cast(ID as varchar(250)), SECTION_CODE from #ParentSections
		open sect_cur
		fetch sect_cur into @cId, @cSectionCode

		while (@@sqlstatus!=2)
 		begin
				-- Формируем список разделов к отчетам
				exec MEDIATE..spNE_BuildingSectionById @cSectionCode, @cId

				fetch sect_cur into @cId, @cSectionCode
		end

		close sect_cur

		--insert into #Sections (SECTION_CODE, PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE)


	select 	SECTION_CODE as Id, PARENT_CODE as ParentId, rtrim(SECTION_NAME) as Name, TYPE_CODE as TypeId, ORDER_SORT as Position
	from 		#Sections
	order by ORDER_SORT desc, SECTION_NAME asc

	drop table #Sections
	drop table #ParentSections
	deallocate cursor sect_cur

end
/*

  exec MEDIATE..spNE_GetSectionsByUserId 2, 3
 exec MEDIATE..spNE_GetRolesByUserId 3
*/
