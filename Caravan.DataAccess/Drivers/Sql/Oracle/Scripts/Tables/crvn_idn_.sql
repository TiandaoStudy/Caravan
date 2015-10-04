-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_TABNAME
(
     CCLI_ID                        NUMBER(10)      NOT NULL
   , CCLI_ENABLED                   NVARCHAR2(8)    NOT NULL
   , CCLI_CLIENT_ID                 NUMBER(1)       NOT NULL
   , CCLI_CLIENT_NAME               NUMBER(3)       NOT NULL

   , CHECK (clos_type IN ('debug', 'trace', 'info', 'warn', 'error', 'fatal')) ENABLE

   , CONSTRAINT pk_crvn_idn_TABNAME PRIMARY KEY (CCLI_ID) ENABLE
   , CONSTRAINT uk_crvn_idn_TABNAME UNIQUE (CCLI_CLIENT_ID) ENABLE
   , CONSTRAINT fk_crvnidnTABNAME_ FOREIGN KEY (capp_id) REFERENCES mydb.crvn_sec_apps (capp_id) ON DELETE CASCADE ENABLE
);
