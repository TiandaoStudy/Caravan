-- REPLACE 'mydb' WITH DB NAME

--------------------------------------------------------
--  DDL for Table FLEX_LOG_SETTINGS
--------------------------------------------------------

  CREATE TABLE "mydb"."FLEX_LOG_SETTINGS" 
   (	"FLOS_TYPE" VARCHAR2(10 CHAR), 
	"FLOS_ENABLED" NUMBER(*,0), 
	"FLOS_DAYS" NUMBER(*,0)
   ) ;
REM INSERTING into mydb.FLEX_LOG_SETTINGS
SET DEFINE OFF;
Insert into mydb.FLEX_LOG_SETTINGS (FLOS_TYPE,FLOS_ENABLED,FLOS_DAYS) values ('DEBUG','1','90');
Insert into mydb.FLEX_LOG_SETTINGS (FLOS_TYPE,FLOS_ENABLED,FLOS_DAYS) values ('INFO','1','90');
Insert into mydb.FLEX_LOG_SETTINGS (FLOS_TYPE,FLOS_ENABLED,FLOS_DAYS) values ('WARNING','1','90');
Insert into mydb.FLEX_LOG_SETTINGS (FLOS_TYPE,FLOS_ENABLED,FLOS_DAYS) values ('ERROR','1','90');
Insert into mydb.FLEX_LOG_SETTINGS (FLOS_TYPE,FLOS_ENABLED,FLOS_DAYS) values ('FATAL','1','90');
--------------------------------------------------------
--  DDL for Index FLEX_LOG_SETTINGS_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "mydb"."FLEX_LOG_SETTINGS_PK" ON "mydb"."FLEX_LOG_SETTINGS" ("FLOS_TYPE") 
  ;
--------------------------------------------------------
--  Constraints for Table FLEX_LOG_SETTINGS
--------------------------------------------------------

  ALTER TABLE "mydb"."FLEX_LOG_SETTINGS" ADD CONSTRAINT "FLEX_LOG_SETTINGS_PK" PRIMARY KEY ("FLOS_TYPE") ENABLE;
 
  ALTER TABLE "mydb"."FLEX_LOG_SETTINGS" MODIFY ("FLOS_TYPE" NOT NULL ENABLE);
 
  ALTER TABLE "mydb"."FLEX_LOG_SETTINGS" MODIFY ("FLOS_ENABLED" NOT NULL ENABLE);
 
  ALTER TABLE "mydb"."FLEX_LOG_SETTINGS" MODIFY ("FLOS_DAYS" NOT NULL ENABLE);
