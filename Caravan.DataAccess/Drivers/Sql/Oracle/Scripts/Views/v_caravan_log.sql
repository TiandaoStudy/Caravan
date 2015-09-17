﻿-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE FORCE VIEW mydb."V_CARAVAN_LOG" ("CLOG_ID", "CAPP_NAME", "CLOS_TYPE", "CLOG_DATE", "CUSR_LOGIN", "CLOG_CODE_UNIT", "CLOG_FUNCTION", "CLOG_SHORT_MSG", "CLOG_LONG_MSG", "CLOG_CONTEXT", "CLOG_KEY_0", "CLOG_VALUE_0", "CLOG_KEY_1", "CLOG_VALUE_1", "CLOG_KEY_2", "CLOG_VALUE_2", "CLOG_KEY_3", "CLOG_VALUE_3", "CLOG_KEY_4", "CLOG_VALUE_4", "CLOG_KEY_5", "CLOG_VALUE_5", "CLOG_KEY_6", "CLOG_VALUE_6", "CLOG_KEY_7", "CLOG_VALUE_7", "CLOG_KEY_8", "CLOG_VALUE_8", "CLOG_KEY_9", "CLOG_VALUE_9") AS 
SELECT "CLOG_ID",
       "CAPP_NAME",
       "CLOS_TYPE",
       "CLOG_DATE",
       "CUSR_LOGIN",
       "CLOG_CODE_UNIT",
       "CLOG_FUNCTION",
       "CLOG_SHORT_MSG",
       "CLOG_LONG_MSG",
       "CLOG_CONTEXT",
       "CLOG_KEY_0", "CLOG_VALUE_0",
       "CLOG_KEY_1", "CLOG_VALUE_1",
       "CLOG_KEY_2", "CLOG_VALUE_2",
       "CLOG_KEY_3", "CLOG_VALUE_3",
       "CLOG_KEY_4", "CLOG_VALUE_4",
       "CLOG_KEY_5", "CLOG_VALUE_5",
       "CLOG_KEY_6", "CLOG_VALUE_6",
       "CLOG_KEY_7", "CLOG_VALUE_7",
       "CLOG_KEY_8", "CLOG_VALUE_8",
       "CLOG_KEY_9", "CLOG_VALUE_9"    
    FROM mydb.crvn_log_entries e
    JOIN mydb.crvn_sec_apps a ON e."CAPP_ID" = a."CAPP_ID"
   ORDER BY "CLOG_DATE" DESC, "CLOG_ID" DESC