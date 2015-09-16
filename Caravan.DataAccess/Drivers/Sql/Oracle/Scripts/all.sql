
-- Apps
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_apps
(
     capp_id          NUMBER(19)      NOT NULL 
   , capp_name        NVARCHAR2(32)   NOT NULL
   , capp_descr       NVARCHAR2(256)  NOT NULL
   , CHECK (capp_name = lower(capp_name)) ENABLE
   , CONSTRAINT pk_crvn_sec_apps PRIMARY KEY (capp_id) ENABLE
   , CONSTRAINT uk_crvn_sec_apps UNIQUE (capp_name) ENABLE
);

CREATE SEQUENCE mydb.crvn_sec_apps_id; 

COMMENT ON TABLE mydb.crvn_sec_apps 
     IS 'Tabella che censisce le applicazioni FINSA';
COMMENT ON COLUMN mydb.crvn_sec_apps.capp_id 
     IS 'Identificativo riga, è una sequenza autoincrementale';
COMMENT ON COLUMN mydb.crvn_sec_apps.capp_name 
     IS 'Il nome sintetico della applicazione';
COMMENT ON COLUMN mydb.crvn_sec_apps.capp_descr 
     IS 'Il nome esteso della applicazione';

-- Log Settings
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_log_settings
(
     capp_id          NUMBER(19)      NOT NULL
   , clos_type        NVARCHAR2(8)    NOT NULL
   , clos_enabled     NUMBER(1)       NOT NULL
   , clos_days        NUMBER(3)       NOT NULL
   , clos_max_entries NUMBER(7)       NOT NULL
   , CHECK (clos_type IN ('debug', 'trace', 'info', 'warn', 'error', 'fatal')) ENABLE
   , CHECK (clos_enabled IN (0, 1)) ENABLE
   , CHECK (clos_days > 0 AND clos_max_entries > 0) ENABLE
   , CONSTRAINT pk_crvn_log_settings PRIMARY KEY (capp_id, clos_type) ENABLE
   , CONSTRAINT fk_crvnlogsettings_crvnsecapps FOREIGN KEY (capp_id) REFERENCES mydb.crvn_sec_apps (capp_id) ON DELETE CASCADE ENABLE
);

COMMENT ON TABLE mydb.crvn_log_settings 
     IS 'Tabella delle impostazioni del sistema di log delle applicazioni FINSA';
COMMENT ON COLUMN mydb.crvn_log_entries.clos_type 
     IS 'Livello del messaggio di log, può assumere i valori debug, trace, info, warn, error, fatal';
COMMENT ON COLUMN mydb.crvn_log_settings.capp_id 
     IS 'Applicazione relativa alla riga di impostazioni';
COMMENT ON COLUMN mydb.crvn_log_settings.clos_enabled 
     IS 'Attivazione del log - 0 spento, 1 acceso';
COMMENT ON COLUMN mydb.crvn_log_settings.clos_days 
     IS 'Numeri di giorni di persistenza della riga di log';
COMMENT ON COLUMN mydb.crvn_log_settings.clos_max_entries 
     IS 'Massimo numero di righe presenti nel log per il tipo e la applicazione';

-- Log Entries
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_log_entries 
(  
     clog_id           NUMBER(19)           NOT NULL
   , capp_id           NUMBER(19)           NOT NULL
   , clos_type         NVARCHAR2(8)         NOT NULL
   , clog_date         DATE DEFAULT SYS_EXTRACT_UTC(SYSTIMESTAMP) NOT NULL 
   , cusr_login        NVARCHAR2(32)
   , clog_code_unit    NVARCHAR2(256)
   , clog_function     NVARCHAR2(256)
   , clog_short_msg    NVARCHAR2(1024)      NOT NULL
   , clog_long_msg     NCLOB
   , clog_context      NVARCHAR2(256)
   , clog_key_0        NVARCHAR2(32)
   , clog_value_0      NVARCHAR2(1024)
   , clog_key_1        NVARCHAR2(32) 
   , clog_value_1      NVARCHAR2(1024)
   , clog_key_2        NVARCHAR2(32) 
   , clog_value_2      NVARCHAR2(1024)
   , clog_key_3        NVARCHAR2(32) 
   , clog_value_3      NVARCHAR2(1024) 
   , clog_key_4        NVARCHAR2(32) 
   , clog_value_4      NVARCHAR2(1024) 
   , clog_key_5        NVARCHAR2(32) 
   , clog_value_5      NVARCHAR2(1024) 
   , clog_key_6        NVARCHAR2(32) 
   , clog_value_6      NVARCHAR2(1024) 
   , clog_key_7        NVARCHAR2(32) 
   , clog_value_7      NVARCHAR2(1024) 
   , clog_key_8        NVARCHAR2(32) 
   , clog_value_8      NVARCHAR2(1024) 
   , clog_key_9        NVARCHAR2(32)
   , clog_value_9      NVARCHAR2(1024)
   , CHECK (cusr_login is null or cusr_login = lower(cusr_login)) ENABLE
   , CHECK (clog_code_unit = lower(clog_code_unit)) ENABLE
   , CHECK (clog_function = lower(clog_function)) ENABLE
   , CHECK (clog_key_0 is null or clog_key_0 = lower(clog_key_0)) ENABLE
   , CHECK (clog_key_1 is null or clog_key_1 = lower(clog_key_1)) ENABLE
   , CHECK (clog_key_2 is null or clog_key_2 = lower(clog_key_2)) ENABLE
   , CHECK (clog_key_3 is null or clog_key_3 = lower(clog_key_3)) ENABLE
   , CHECK (clog_key_4 is null or clog_key_4 = lower(clog_key_4)) ENABLE
   , CHECK (clog_key_5 is null or clog_key_5 = lower(clog_key_5)) ENABLE
   , CHECK (clog_key_6 is null or clog_key_6 = lower(clog_key_6)) ENABLE
   , CHECK (clog_key_7 is null or clog_key_7 = lower(clog_key_7)) ENABLE
   , CHECK (clog_key_8 is null or clog_key_8 = lower(clog_key_8)) ENABLE
   , CHECK (clog_key_9 is null or clog_key_9 = lower(clog_key_9)) ENABLE
   , CONSTRAINT pk_crvn_log_entries PRIMARY KEY (clog_id) ENABLE
   , CONSTRAINT fk_crvnlog_crvnlogsettings FOREIGN KEY (capp_id, clos_type) REFERENCES mydb.crvn_log_settings (capp_id, clos_type) ON DELETE CASCADE ENABLE
);

COMMENT ON TABLE mydb.crvn_log_entries 
     IS 'Tabella di log per le applicazioni FINSA';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_id 
     IS 'Identificativo riga, è una sequenza autoincrementale';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_date 
     IS 'Data di inserimento della riga, di default è la ora corrente in UTC';
COMMENT ON COLUMN mydb.crvn_log_entries.clos_type 
     IS 'Livello del messaggio di log, può assumere i valori debug, trace, info, warn, error, fatal';
COMMENT ON COLUMN mydb.crvn_log_entries.capp_id 
     IS 'Applicazione FINSA a cui si riferisce il log';
COMMENT ON COLUMN mydb.crvn_log_entries.cusr_login 
     IS 'Utente loggato, oppure entità, che ha attivato il logger';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_code_unit 
     IS 'Blocco di codice (package, classe, modulo) che contiene la funzione da cui è partito il messaggio di log';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_function 
     IS 'Funzione, metodo o procedura, da cui è partito il messaggio di log';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_short_msg 
     IS 'Messaggio breve, contiene informazioni sintetiche o semplici comunicazioni';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_long_msg 
     IS 'Messaggio verboso, contiene informazioni più elaborate (ad esempio, stacktrace o oggetti deserializzati)';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_context 
     IS 'Infomazione dettagliata del contesto in cui viene inserito il messaggio';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_key_0 
     IS 'Nome del parametro opzionale 0, ad esempio my_param_name';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_value_0 
     IS 'Valore del parametro opzionale 0, ad esempio my_param_value'; 
COMMENT ON COLUMN mydb.crvn_log_entries.clog_key_1 
     IS 'Nome del parametro opzionale 1, ad esempio my_param_name';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_value_1 
     IS 'Valore del parametro opzionale 1, ad esempio my_param_value'; 
COMMENT ON COLUMN mydb.crvn_log_entries.clog_key_2 
     IS 'Nome del parametro opzionale 2, ad esempio my_param_name';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_value_2 
     IS 'Valore del parametro opzionale 2, ad esempio my_param_value';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_key_3 
     IS 'Nome del parametro opzionale 3, ad esempio my_param_name';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_value_3 
     IS 'Valore del parametro opzionale 3, ad esempio my_param_value';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_key_4 
     IS 'Nome del parametro opzionale 4, ad esempio my_param_name';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_value_4 
     IS 'Valore del parametro opzionale 4, ad esempio my_param_value';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_key_5 
     IS 'Nome del parametro opzionale 5, ad esempio my_param_name';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_value_5 
     IS 'Valore del parametro opzionale 5, ad esempio my_param_value';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_key_6 
     IS 'Nome del parametro opzionale 6, ad esempio my_param_name';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_value_6 
     IS 'Valore del parametro opzionale 6, ad esempio my_param_value';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_key_7 
     IS 'Nome del parametro opzionale 7, ad esempio my_param_name';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_value_7 
     IS 'Valore del parametro opzionale 7, ad esempio my_param_value';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_key_8 
     IS 'Nome del parametro opzionale 8, ad esempio my_param_name';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_value_8 
     IS 'Valore del parametro opzionale 8, ad esempio my_param_value';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_key_9 
     IS 'Nome del parametro opzionale 9, ad esempio my_param_name';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_value_9 
     IS 'Valore del parametro opzionale 9, ad esempio my_param_value';

CREATE INDEX mydb.ix_crvn_log_date ON mydb.crvn_log_entries (capp_id, clog_date DESC);
CREATE INDEX mydb.ix_crvn_log_type ON mydb.crvn_log_entries (capp_id, clos_type);

-- DROP da fare per la transizione da FLEX_LOG:
--> pck_flex_log
--> flex_log
--> flex_log_seq
--> flex_log_settings

CREATE SEQUENCE mydb.crvn_log_entries_id;

-- Users
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_users
(
     cusr_id          NUMBER          NOT NULL
   , capp_id          NUMBER          NOT NULL 
   , cusr_login       NVARCHAR2(32)   NOT NULL
   , cusr_hashed_pwd  NVARCHAR2(256)
   , cusr_active      NUMBER(1)       NOT NULL
   , cusr_first_name  NVARCHAR2(256)
   , cusr_last_name   NVARCHAR2(256)
   , cusr_email       NVARCHAR2(256)
   , CHECK (cusr_login = lower(cusr_login)) ENABLE
   , CONSTRAINT pk_crvn_sec_users PRIMARY KEY (cusr_id) ENABLE
   , CONSTRAINT uk_crvn_sec_users UNIQUE (capp_id, cusr_login) ENABLE
   , CONSTRAINT fk_crvnsecusers_crvnsecapps FOREIGN KEY (capp_id) REFERENCES mydb.crvn_sec_apps (capp_id) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.crvn_sec_users_id;

-- Groups
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_groups
(
     cgrp_id          NUMBER          NOT NULL
   , capp_id          NUMBER          NOT NULL 
   , cgrp_name        NVARCHAR2(32)   NOT NULL
   , cgrp_descr       NVARCHAR2(256)  NOT NULL
   , cgrp_notes       NVARCHAR2(1024) NOT NULL
   , CHECK (cgrp_name = lower(cgrp_name)) ENABLE
   , CONSTRAINT pk_crvn_sec_groups PRIMARY KEY (cgrp_id) ENABLE
   , CONSTRAINT uk_crvn_sec_groups UNIQUE (capp_id, cgrp_name) ENABLE
   , CONSTRAINT fk_crvnsecgroups_crvnsecapps FOREIGN KEY (capp_id) REFERENCES mydb.crvn_sec_apps (capp_id) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.crvn_sec_groups_id;

-- Roles
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_roles
(
     crol_id          NUMBER          NOT NULL
   , capp_id          NUMBER          NOT NULL 
   , crol_name        NVARCHAR2(32)   NOT NULL
   , crol_descr       NVARCHAR2(256)  NOT NULL
   , crol_notes       NVARCHAR2(1024) NOT NULL
   , CHECK (crol_name = lower(crol_name)) ENABLE
   , CONSTRAINT pk_crvn_sec_roles PRIMARY KEY (crol_id) ENABLE
   , CONSTRAINT uk_crvn_sec_roles UNIQUE (crol_name, capp_id) ENABLE
   , CONSTRAINT fk_crvnsecroles_crvnsecapps FOREIGN KEY (capp_id) REFERENCES mydb.crvn_sec_apps (capp_id) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.crvn_sec_roles_id;

-- User Groups
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_user_groups
(
     cusr_id          NUMBER          NOT NULL
   , cgrp_id          NUMBER          NOT NULL
   , CONSTRAINT pk_crvn_sec_usrgrp PRIMARY KEY (cusr_id, cgrp_id) ENABLE
   , CONSTRAINT fk_crvnsecusrgrp_crvnsecusr FOREIGN KEY (cusr_id) REFERENCES mydb.crvn_sec_users (cusr_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecusrgrp_crvnsecgrp FOREIGN KEY (cgrp_id) REFERENCES mydb.crvn_sec_groups (cgrp_id) ON DELETE CASCADE ENABLE
);

-- User Roles
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_user_roles
(
     cusr_id          NUMBER          NOT NULL
   , crol_id          NUMBER          NOT NULL
   , CONSTRAINT pk_crvn_sec_usrrol PRIMARY KEY (cusr_id, crol_id) ENABLE
   , CONSTRAINT fk_crvnsecusrrol_crvnsecusr FOREIGN KEY (cusr_id) REFERENCES mydb.crvn_sec_users (cusr_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecusrrol_crvnsecrol FOREIGN KEY (crol_id) REFERENCES mydb.crvn_sec_roles (crol_id) ON DELETE CASCADE ENABLE
);

-- Contexts
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_contexts
(
     cctx_id          NUMBER          NOT NULL
   , capp_id          NUMBER          NOT NULL 
   , cctx_name        NVARCHAR2(32)   NOT NULL
   , cctx_descr       NVARCHAR2(256)  NOT NULL
   , CHECK (cctx_name = lower(cctx_name)) ENABLE
   , CONSTRAINT pk_crvn_sec_contexts PRIMARY KEY (cctx_id) ENABLE
   , CONSTRAINT uk_crvn_sec_contexts UNIQUE (capp_id, cctx_name) ENABLE   
   , CONSTRAINT fk_crvnsecctxs_crvnsecapps FOREIGN KEY (capp_id) REFERENCES mydb.crvn_sec_apps (capp_id) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.crvn_sec_contexts_id;

-- Objects
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_objects
(
     cobj_id          NUMBER          NOT NULL
   , cctx_id          NUMBER          NOT NULL
   , cobj_name        NVARCHAR2(32)   NOT NULL
   , cobj_descr       NVARCHAR2(256)  NOT NULL
   , cobj_type        NVARCHAR2(8)    NOT NULL
   , CHECK (cobj_name = lower(cobj_name)) ENABLE
   , CONSTRAINT pk_crvn_sec_objects PRIMARY KEY (cobj_id) ENABLE
   , CONSTRAINT uk_crvn_sec_objects UNIQUE (cctx_id, cobj_name) ENABLE
   , CONSTRAINT fk_crvnsecobjs_crvnsecctxs FOREIGN KEY (cctx_id) REFERENCES mydb.crvn_sec_contexts (cctx_id) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.crvn_sec_objects_id;

-- Sec Entries
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_entries
(
     csec_id          NUMBER          NOT NULL
   , cobj_id          NUMBER          NOT NULL
   , cusr_id          NUMBER          -- Might be null, either user, group or role
   , cgrp_id          NUMBER          -- Might be null, either user, group or role
   , crol_id          NUMBER          -- Might be null, either user, group or role
   , CHECK ((cusr_id IS NULL AND crol_id IS NULL AND cgrp_id IS NOT NULL) OR 
            (cusr_id IS NOT NULL AND crol_id IS NULL AND cgrp_id IS NULL) OR
			(cusr_id IS NULL AND crol_id IS NOT NULL AND cgrp_id IS NULL)) ENABLE
   , CONSTRAINT pk_crvn_sec_entries PRIMARY KEY (csec_id) ENABLE
   , CONSTRAINT fk_crvnsec_crvnsecobj FOREIGN KEY (cobj_id) REFERENCES mydb.crvn_sec_objects (cobj_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsec_crvnsecusr FOREIGN KEY (cusr_id) REFERENCES mydb.crvn_sec_users (cusr_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsec_crvnsecgrp FOREIGN KEY (cgrp_id) REFERENCES mydb.crvn_sec_groups (cgrp_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsec_crvnsecrol FOREIGN KEY (crol_id) REFERENCES mydb.crvn_sec_roles (crol_id) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.crvn_sec_entries_id;

-- Triggers: Apps Id
-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_sec_apps_id
BEFORE INSERT ON mydb.crvn_sec_apps 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_sec_apps_id.nextval
    INTO :new.capp_id
    FROM DUAL;
END;
/

-- Triggers: Log Entries Id
-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_log_entries_id
BEFORE INSERT ON mydb.crvn_log_entries 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_log_entries_id.nextval
    INTO :new.clog_id
    FROM DUAL;
END;
/

-- Triggers: Users Id
-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_sec_users_id
BEFORE INSERT ON mydb.crvn_sec_users 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_sec_users_id.nextval
    INTO :new.cusr_id
    FROM DUAL;
END;
/

-- Triggers: Groups Id
-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_sec_groups_id
BEFORE INSERT ON mydb.crvn_sec_groups 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_sec_groups_id.nextval
    INTO :new.cgrp_id
    FROM DUAL;
END;
/

-- Triggers: Roles Id
-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_sec_roles_id
BEFORE INSERT ON mydb.crvn_sec_roles 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_sec_roles_id.nextval
    INTO :new.crol_id
    FROM DUAL;
END;
/

-- Triggers: Contexts Id
-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_sec_contexts_id
BEFORE INSERT ON mydb.crvn_sec_contexts 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_sec_contexts_id.nextval
    INTO :new.cctx_id
    FROM DUAL;
END;
/

-- Triggers: Objects Id
-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_sec_objects_id
BEFORE INSERT ON mydb.crvn_sec_objects 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_sec_objects_id.nextval
    INTO :new.cobj_id
    FROM DUAL;
END;
/

-- Triggers: Sec Entries Id
-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_sec_entries_id
BEFORE INSERT ON mydb.crvn_sec_entries 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_sec_entries_id.nextval
    INTO :new.csec_id
    FROM DUAL;
END;
/

-- Views: V_CARAVAN_LOG
-- REPLACE 'mydb' WITH DB NAME

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
    FROM caravan.crvn_log_entries e
    JOIN caravan.crvn_sec_apps a ON e."CAPP_ID" = a."CAPP_ID"
   ORDER BY "CLOG_DATE" DESC, "CLOG_ID" DESC

-- Views: V_CARAVAN_LOG_SUMMARY
-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE FORCE VIEW mydb."V_CARAVAN_LOG_SUMMARY" ("CAPP_ID", "CAPP_NAME", "CLOS_TYPE", "CLOG_MIN_DATE", "CLOG_MAX_DATE", "CLOG_ENTRIES", "CLOS_MAX_ENTRIES", "CLOS_ENABLED") AS
SELECT a."CAPP_ID",
       a."CAPP_NAME",
       s."CLOS_TYPE",
       MIN(e."CLOG_DATE") CLOG_MIN_DATE,
       MAX(e."CLOG_DATE") CLOG_MAX_DATE,
       COUNT(*) CLOG_ENTRIES,
       MAX(s."CLOS_MAX_ENTRIES") CLOS_MAX_ENTRIES,
       MAX(s."CLOS_ENABLED") CLOS_ENABLED
    FROM mydb.crvn_sec_apps a
    LEFT JOIN mydb.crvn_log_settings s ON s."CAPP_ID" = a."CAPP_ID"
    LEFT JOIN mydb.crvn_log_entries e ON e."CAPP_ID" = a."CAPP_ID" AND e."CLOS_TYPE" = s."CLOS_TYPE"
   GROUP BY a."CAPP_ID", a."CAPP_NAME", s."CLOS_TYPE"
   ORDER BY a."CAPP_NAME", s."CLOS_TYPE"