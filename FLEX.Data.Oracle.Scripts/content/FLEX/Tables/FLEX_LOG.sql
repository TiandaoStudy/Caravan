--------------------------------------------------------
--  DDL for Table FLEX_LOG
--------------------------------------------------------

  CREATE TABLE "USERBASE"."FLEX_LOG" 
   (	"FLOG_ID" NUMBER, 
	"FLOG_ENTRY_DATE" DATE DEFAULT SYSDATE, 
	"FLOG_TYPE" VARCHAR2(10 CHAR), 
	"FLOG_APPLICATION" VARCHAR2(30 CHAR), 
	"FLOG_CODE_UNIT" VARCHAR2(100 CHAR), 
	"FLOG_FUNCTION" VARCHAR2(100 CHAR), 
	"FLOG_SHORT_MSG" VARCHAR2(400 CHAR), 
	"FLOG_LONG_MSG" VARCHAR2(4000 CHAR), 
	"FLOG_ARG_0" VARCHAR2(4000 CHAR), 
	"FLOG_ARG_1" VARCHAR2(4000 CHAR), 
	"FLOG_ARG_2" VARCHAR2(4000 CHAR), 
	"FLOG_ARG_3" VARCHAR2(4000 CHAR), 
	"FLOG_ARG_4" VARCHAR2(4000 CHAR), 
	"FLOG_ARG_5" VARCHAR2(4000 CHAR), 
	"FLOG_ARG_6" VARCHAR2(4000 CHAR), 
	"FLOG_ARG_7" VARCHAR2(4000 CHAR), 
	"FLOG_ARG_8" VARCHAR2(4000 CHAR), 
	"FLOG_ARG_9" VARCHAR2(4000 CHAR)
   ) ;
 

   COMMENT ON COLUMN "USERBASE"."FLEX_LOG"."FLOG_CODE_UNIT" IS 'Package or fully qualified .NET type';
--------------------------------------------------------
--  DDL for Index FLEX_LOG_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "USERBASE"."FLEX_LOG_PK" ON "USERBASE"."FLEX_LOG" ("FLOG_ID") 
  ;
--------------------------------------------------------
--  DDL for Index FLEX_LOG_DATE_INDEX
--------------------------------------------------------

  CREATE INDEX "USERBASE"."FLEX_LOG_DATE_INDEX" ON "USERBASE"."FLEX_LOG" ("FLOG_ENTRY_DATE" DESC) 
  ;
--------------------------------------------------------
--  DDL for Index FLEX_LOG_TYPE_INDEX
--------------------------------------------------------

  CREATE INDEX "USERBASE"."FLEX_LOG_TYPE_INDEX" ON "USERBASE"."FLEX_LOG" ("FLOG_TYPE") 
  ;
--------------------------------------------------------
--  Constraints for Table FLEX_LOG
--------------------------------------------------------

  ALTER TABLE "USERBASE"."FLEX_LOG" ADD CONSTRAINT "FLEX_LOG_PK" PRIMARY KEY ("FLOG_ID") ENABLE;
 
  ALTER TABLE "USERBASE"."FLEX_LOG" ADD CONSTRAINT "FLEX_LOG_VALID_TYPE" CHECK (FLOG_TYPE IN ('INFO', 'ERROR', 'WARNING', 'DEBUG', 'FATAL')) ENABLE;
 
  ALTER TABLE "USERBASE"."FLEX_LOG" MODIFY ("FLOG_ID" NOT NULL ENABLE);
 
  ALTER TABLE "USERBASE"."FLEX_LOG" MODIFY ("FLOG_ENTRY_DATE" NOT NULL ENABLE);
 
  ALTER TABLE "USERBASE"."FLEX_LOG" MODIFY ("FLOG_TYPE" NOT NULL ENABLE);
 
  ALTER TABLE "USERBASE"."FLEX_LOG" MODIFY ("FLOG_APPLICATION" NOT NULL ENABLE);
 
  ALTER TABLE "USERBASE"."FLEX_LOG" MODIFY ("FLOG_CODE_UNIT" NOT NULL ENABLE);
 
  ALTER TABLE "USERBASE"."FLEX_LOG" MODIFY ("FLOG_FUNCTION" NOT NULL ENABLE);
 
  ALTER TABLE "USERBASE"."FLEX_LOG" MODIFY ("FLOG_SHORT_MSG" NOT NULL ENABLE);
