use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_BuildingSectionById')
		and type='P')
	drop procedure dbo.spNE_BuildingSectionById
go

create proc spNE_BuildingSectionById (
																	@SECTION_CODE     numeric(10,0),
																	@CUR_ORDER_SORT		varchar(250)
)
as
declare 	@PARENT_CODE     numeric(10,0),
					@ORDER_SORT			 varchar(250)
begin
		/*
		create table #Sections (
			SECTION_CODE    numeric(10,0)   not null,      					--
  		PARENT_CODE     numeric(10,0)   null,                   -- SECTION_CODE родителя
  		SECTION_NAME    varchar(255)    not null,               -- Наименование секции
  		TYPE_CODE       numeric(10,0)   not null,               -- Код типа секции (1 - Раздел/подраздел, 2 - Отчет)
			ORDER_SORT			varchar(255)  	null										--
		)
		*/

		if @SECTION_CODE = null
				return 0

		select @ORDER_SORT = @CUR_ORDER_SORT + '.1'

		-- Добавляем в итоговую таблицу текущую секцию
		insert into #Sections (SECTION_CODE, PARENT_CODE, SECTION_NAME, /*GROUP_CODE,*/ TYPE_CODE, ORDER_SORT)
		select	S.SECTION_CODE, S.PARENT_CODE, S.SECTION_NAME, /*S.GROUP_CODE,*/ S.TYPE_CODE, @ORDER_SORT
		from 		MEDIATE..NE_SECTIONS S
		where		SECTION_CODE = @SECTION_CODE and
						TYPE_CODE = 1

		-- Получаем код родительской секции
		select 	@PARENT_CODE =	S.PARENT_CODE
		from 		MEDIATE..NE_SECTIONS S
		where		SECTION_CODE = @SECTION_CODE and
					TYPE_CODE = 1

		if @PARENT_CODE is null
				return 0

		-- Если родитель у секции есть, то запускаем рекурсивную обработку родительской секции
		exec MEDIATE..spNE_BuildingSectionById @PARENT_CODE, @ORDER_SORT
end
/*

  exec MEDIATE..spNE_BuildingSectionById
*/