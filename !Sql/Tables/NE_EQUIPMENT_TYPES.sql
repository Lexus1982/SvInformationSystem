use MEDIATE
GO

-- drop table MEDIATE..NE_EQUIPMENT_TYPES
create table  NE_EQUIPMENT_TYPES (
  TYPE_CODE             numeric(10,0)   identity not null,      --
  TYPE_NAME             varchar(255)    not null,               -- Наименование типа оборудования
  POSITION              int             not null,               -- Позиция элемента в списке (для сортировки)
  USER_CODE             numeric(5,0)    not null,               -- Код пользователя (fk INTEGRAL..USERS)
  DATE_CHANGE           datetime        not null,               -- Дата последнего изменения

	CONSTRAINT PK_TYPE_CODE PRIMARY KEY CLUSTERED ( TYPE_CODE )  on 'default'
)

CREATE UNIQUE INDEX UI_NE_EQUIPMENT_TYPES_NAME ON NE_EQUIPMENT_TYPES (TYPE_NAME)


ALTER TABLE MEDIATE.dbo.NE_EQUIPMENT_TYPES
ADD CONSTRAINT FK_NE_EQUIPMENT_TYPES_USER_CODE
FOREIGN KEY ( USER_CODE )
REFERENCES INTEGRAL.dbo.USERS (USER_CODE)
go

/*
    select * from MEDIATE..NE_EQUIPMENT_TYPES

  insert into MEDIATE..NE_EQUIPMENT_TYPES (TYPE_NAME, USER_CODE, DATE_CHANGE)
  values ('WS-C2960-24TC-S', 3, getdate())

*/
