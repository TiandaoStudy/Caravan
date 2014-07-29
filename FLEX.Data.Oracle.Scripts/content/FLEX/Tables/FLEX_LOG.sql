-- REPLACE 'mydb' WITH DB NAME

--------------------------------------------------------
--  DDL for Table FLEX_LOG
--------------------------------------------------------

  CREATE TABLE "mydb"."FLEX_LOG" 
   (	"FLOG_ID" NUMBER, 
	"FLOG_ENTRY_DATE" DATE DEFAULT SYSDATE, 
	"FLOG_TYPE" VARCHAR2(10 CHAR), 
	"FLOG_APPLICATION" VARCHAR2(30 CHAR), 
	"FLOG_CODE_UNIT" VARCHAR2(100 CHAR), 
	"FLOG_FUNCTION" VARCHAR2(100 CHAR),
	"FLOG_SHORT_MSG" VARCHAR2(400 CHAR), 
	"FLOG_LONG_MSG" VARCHAR2(4000 CHAR),
   "FLOG_CONTEXT" VARCHAR2(400 CHAR),
	"FLOG_KEY_0" VARCHAR2(100 CHAR), 
	"FLOG_VALUE_0" VARCHAR2(4000 CHAR), 
	"FLOG_KEY_1" VARCHAR2(100 CHAR), 
	"FLOG_VALUE_1" VARCHAR2(4000 CHAR), 
	"FLOG_KEY_2" VARCHAR2(100 CHAR), 
	"FLOG_VALUE_2" VARCHAR2(4000 CHAR), 
	"FLOG_KEY_3" VARCHAR2(100 CHAR), 
	"FLOG_VALUE_3" VARCHAR2(4000 CHAR), 
	"FLOG_KEY_4" VARCHAR2(100 CHAR), 
	"FLOG_VALUE_4" VARCHAR2(4000 CHAR), 
	"FLOG_KEY_5" VARCHAR2(100 CHAR), 
	"FLOG_VALUE_5" VARCHAR2(4000 CHAR), 
	"FLOG_KEY_6" VARCHAR2(100 CHAR), 
	"FLOG_VALUE_6" VARCHAR2(4000 CHAR), 
	"FLOG_KEY_7" VARCHAR2(100 CHAR), 
	"FLOG_VALUE_7" VARCHAR2(4000 CHAR), 
	"FLOG_KEY_8" VARCHAR2(100 CHAR), 
	"FLOG_VALUE_8" VARCHAR2(4000 CHAR), 
	"FLOG_KEY_9" VARCHAR2(100 CHAR),
	"FLOG_VALUE_9" VARCHAR2(4000 CHAR)
   ) ;
 

   COMMENT ON COLUMN "mydb"."FLEX_LOG"."FLOG_CODE_UNIT" IS 'Package or fully qualified .NET type';
--------------------------------------------------------
--  DDL for Index FLEX_LOG_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "mydb"."FLEX_LOG_PK" ON "mydb"."FLEX_LOG" ("FLOG_ID") 
  ;
--------------------------------------------------------
--  DDL for Index FLEX_LOG_DATE_INDEX
--------------------------------------------------------

  CREATE INDEX "mydb"."FLEX_LOG_DATE_INDEX" ON "mydb"."FLEX_LOG" ("FLOG_ENTRY_DATE" DESC) 
  ;
--------------------------------------------------------
--  DDL for Index FLEX_LOG_TYPE_INDEX
--------------------------------------------------------

  CREATE INDEX "mydb"."FLEX_LOG_TYPE_INDEX" ON "mydb"."FLEX_LOG" ("FLOG_TYPE") 
  ;
--------------------------------------------------------
--  Constraints for Table FLEX_LOG
--------------------------------------------------------

  ALTER TABLE "mydb"."FLEX_LOG" ADD CONSTRAINT "FLEX_LOG_PK" PRIMARY KEY ("FLOG_ID") ENABLE;
 
  ALTER TABLE "mydb"."FLEX_LOG" MODIFY ("FLOG_ID" NOT NULL ENABLE);
 
  ALTER TABLE "mydb"."FLEX_LOG" MODIFY ("FLOG_ENTRY_DATE" NOT NULL ENABLE);
 
  ALTER TABLE "mydb"."FLEX_LOG" MODIFY ("FLOG_TYPE" NOT NULL ENABLE);
 
  ALTER TABLE "mydb"."FLEX_LOG" MODIFY ("FLOG_APPLICATION" NOT NULL ENABLE);
 
  ALTER TABLE "mydb"."FLEX_LOG" MODIFY ("FLOG_CODE_UNIT" NOT NULL ENABLE);
 
  ALTER TABLE "mydb"."FLEX_LOG" MODIFY ("FLOG_FUNCTION" NOT NULL ENABLE);
 
  ALTER TABLE "mydb"."FLEX_LOG" MODIFY ("FLOG_SHORT_MSG" NOT NULL ENABLE);
--------------------------------------------------------
--  Ref Constraints for Table FLEX_LOG
--------------------------------------------------------

  ALTER TABLE "mydb"."FLEX_LOG" ADD CONSTRAINT "FLEX_LOG_SETTINGS_FK" FOREIGN KEY ("FLOG_TYPE")
	  REFERENCES "mydb"."FLEX_LOG_SETTINGS" ("FLOS_TYPE") ENABLE;
