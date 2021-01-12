use MEDIATE
GO

-- drop table MEDIATE..NE_SECTIONS
create table  NE_SECTIONS (
  SECTION_CODE    numeric(10,0)   identity not null,      --
  PARENT_CODE     numeric(10,0)   null,                   -- SECTION_CODE родителя
  SECTION_NAME    varchar(255)    not null,               -- Наименование секции
  GROUP_CODE      numeric(10,0)   not null,               -- Код группы секции (1 - Администрирование, 2 - Отчетность)
  TYPE_CODE       numeric(10,0)   not null,               -- Код типа секции (1 - Раздел/подраздел, 2 - Отчет)
	USER_CODE       numeric(5,0)    not null,               -- Код пользователя (fk INTEGRAL..USERS)
  DATE_CHANGE     datetime        not null,               -- Дата последнего изменения

	CONSTRAINT PK_SECTION_CODE PRIMARY KEY CLUSTERED ( SECTION_CODE )  on 'default'
)

--CREATE UNIQUE INDEX UI_NE_SECTIONS_SECTION_NAME ON NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE)
--go

CREATE INDEX FI_NE_SECTIONS_GROUP_CODE ON NE_SECTIONS (GROUP_CODE)
go

ALTER TABLE MEDIATE.dbo.NE_SECTIONS
ADD CONSTRAINT FK_NE_SECTIONS_USER_CODE
FOREIGN KEY ( USER_CODE )
REFERENCES INTEGRAL.dbo.USERS (USER_CODE)
go

-- Отчетност
-- select * from MEDIATE..NE_SECTIONS
insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (null, 'Оборудование на сети связи', 2, 1, 3, getdate())
  insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
  values (1, 'Оборудование на сети связи (свернуто)', 2, 2, 3, getdate())
  insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
  values (1, 'Оборудование в разрезе населенных пунктов', 2, 2, 3, getdate())
  insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
  values (1, 'Оборудование в разрезе сегментов сети', 2, 2, 3, getdate())

insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (null, 'Мониторинг АСОД', 2, 1, 3, getdate())
insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (null, 'Отчетность АСОД', 2, 1, 3, getdate())


insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (null, 'Раздел 1', 2, 1, 3, getdate())
insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (7, 'Раздел 1-1', 2, 1, 3, getdate())
insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (9, 'Отчетность 1-1', 2, 2, 3, getdate())

insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (null, 'Раздел 2', 2, 1, 3, getdate())
insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (8, 'Раздел 2-1', 2, 1, 3, getdate())
insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (10, 'Раздел 2-1-1', 2, 1, 3, getdate())
insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (11, 'Отчетность 2-1-1', 2, 2, 3, getdate())
/*
    select * from MEDIATE..NE_SECTIONS order by isnull(PARENT_CODE, SECTION_CODE), SECTION_CODE

*/


