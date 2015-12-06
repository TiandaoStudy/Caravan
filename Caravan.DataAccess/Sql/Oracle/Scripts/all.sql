﻿
-- Security: Apps
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_apps
(
     capp_id          NUMBER(10)        NOT NULL 
   , capp_name        NVARCHAR2(32)     NOT NULL
   , capp_descr       NVARCHAR2(256)    NOT NULL
   , capp_pwd_hasher  NVARCHAR2(256)    DEFAULT 'BrockAllen.IdentityReboot.AdaptivePasswordHasher, BrockAllen.IdentityReboot' NOT NULL
   , CHECK (capp_name = lower(capp_name)) ENABLE
   , CONSTRAINT pk_crvn_sec_apps PRIMARY KEY (capp_id) ENABLE
   , CONSTRAINT uk_crvn_sec_apps UNIQUE (capp_name) ENABLE
);

CREATE SEQUENCE mydb.crvn_sec_apps_id NOCACHE; 

COMMENT ON TABLE mydb.crvn_sec_apps 
     IS 'Tabella che censisce le applicazioni FINSA';
COMMENT ON COLUMN mydb.crvn_sec_apps.capp_id 
     IS 'Identificativo riga, è una sequenza autoincrementale';
COMMENT ON COLUMN mydb.crvn_sec_apps.capp_name 
     IS 'Il nome sintetico della applicazione';
COMMENT ON COLUMN mydb.crvn_sec_apps.capp_descr 
     IS 'Il nome esteso della applicazione';
COMMENT ON COLUMN mydb.crvn_sec_apps.capp_pwd_hasher 
     IS 'Il tipo .NET usato per fare un HASH delle password';

-- Security: Apps ID trigger
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

-- Logging: Settings
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_log_settings
(
     capp_id          NUMBER(19)      NOT NULL
   , clos_type        NVARCHAR2(8)    NOT NULL
   , clos_enabled     NUMBER(1)       NOT NULL
   , clos_days        NUMBER(3)       NOT NULL
   , clos_max_entries NUMBER(7)       NOT NULL
   -- clos_ins_date, clos_ins_user, clos_upd_date, clos_upd_user
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

-- REPLACE '<CAPP_ID>' WITH CAPP_ID
Insert into mydb.CRVN_LOG_SETTINGS (CAPP_ID,CLOS_TYPE,CLOS_ENABLED,CLOS_DAYS,CLOS_MAX_ENTRIES) values (<CAPP_ID>,'debug',1,30,1000);
Insert into mydb.CRVN_LOG_SETTINGS (CAPP_ID,CLOS_TYPE,CLOS_ENABLED,CLOS_DAYS,CLOS_MAX_ENTRIES) values (<CAPP_ID>,'info',1,30,1000);
Insert into mydb.CRVN_LOG_SETTINGS (CAPP_ID,CLOS_TYPE,CLOS_ENABLED,CLOS_DAYS,CLOS_MAX_ENTRIES) values (<CAPP_ID>,'warn',1,30,1000);
Insert into mydb.CRVN_LOG_SETTINGS (CAPP_ID,CLOS_TYPE,CLOS_ENABLED,CLOS_DAYS,CLOS_MAX_ENTRIES) values (<CAPP_ID>,'error',1,30,1000);
Insert into mydb.CRVN_LOG_SETTINGS (CAPP_ID,CLOS_TYPE,CLOS_ENABLED,CLOS_DAYS,CLOS_MAX_ENTRIES) values (<CAPP_ID>,'fatal',1,30,1000);
Insert into mydb.CRVN_LOG_SETTINGS (CAPP_ID,CLOS_TYPE,CLOS_ENABLED,CLOS_DAYS,CLOS_MAX_ENTRIES) values (<CAPP_ID>,'trace',1,30,10000);


-- Logging: Entries
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

CREATE INDEX mydb.ix_crvn_log_date ON mydb.crvn_log_entries (clog_date DESC, capp_id);
CREATE INDEX mydb.ix_crvn_log_type ON mydb.crvn_log_entries (clos_type, capp_id);

-- DROP da fare per la transizione da FLEX_LOG:
--> pck_flex_log
--> flex_log
--> flex_log_seq
--> flex_log_settings

CREATE SEQUENCE mydb.crvn_log_entries_id NOCACHE;

-- Logging: Entries ID trigger
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

-- Identity: Clients
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_clients
(
     CCLI_ID                        NUMBER(10)                              NOT NULL
   , CAPP_ID                        NUMBER(10)                              NOT NULL
   , CCLI_ENABLED                   NUMBER(1)       DEFAULT 1               NOT NULL
   , CCLI_CLIENT_ID                 NVARCHAR2(200)                          NOT NULL
   , CCLI_CLIENT_NAME               NVARCHAR2(200)                          NOT NULL
   , CCLI_CLIENT_URI                NVARCHAR2(2000)
   , CCLI_LOGO_URI                  NVARCHAR2(2000)
   , CCLI_REQUIRE_CONSENT           NUMBER(1)       DEFAULT 1               NOT NULL
   , CCLI_ALLOW_REMEMBER_CONSENT    NUMBER(1)       DEFAULT 1               NOT NULL
   , CCLI_FLOW                      NVARCHAR2(100)  DEFAULT 'implicit'      NOT NULL
   , CCLI_ALLOW_CLIENT_CREDS_ONLY   NUMBER(1)       DEFAULT 0               NOT NULL
   , CCLI_LOGOUT_URI                NVARCHAR2(2000)      
   , CCLI_LOGOUT_SESSION_REQUIRED   NUMBER(1)       DEFAULT 1               NOT NULL
   , CCLI_ALLOW_ACCESSALL_SCOPES    NUMBER(1)       DEFAULT 0               NOT NULL
   , CCLI_IDENTITY_TOKEN_LIFETIME   NUMBER(10)      DEFAULT 300             NOT NULL
   , CCLI_ACCESS_TOKEN_LIFETIME     NUMBER(10)      DEFAULT 3600            NOT NULL
   , CCLI_AUTH_CODE_LIFETIME        NUMBER(10)      DEFAULT 300             NOT NULL
   , CCLI_ABS_REFR_TOKEN_LIFETIME   NUMBER(10)      DEFAULT 2592000         NOT NULL
   , CCLI_SLID_REFR_TOKEN_LIFETIME  NUMBER(10)      DEFAULT 1296000         NOT NULL
   , CCLI_REFRESH_TOKEN_USAGE       NVARCHAR2(100)  DEFAULT 'onetimeonly'   NOT NULL
   , CCLI_UPD_ACCESS_TOKEN_ON_REFR  NUMBER(1)       DEFAULT 0               NOT NULL
   , CCLI_REFRESH_TOKEN_EXPIRATION  NVARCHAR2(100)  DEFAULT 'absolute'      NOT NULL
   , CCLI_ACCESS_TOKEN_TYPE         NVARCHAR2(100)  DEFAULT 'jwt'           NOT NULL
   , CCLI_ENABLE_LOCAL_LOGIN        NUMBER(1)       DEFAULT 1               NOT NULL
   , CCLI_INCLUDE_JWT_ID            NUMBER(1)       DEFAULT 0               NOT NULL
   , CCLI_ALWAYS_SEND_CLIENT_CLAIMS NUMBER(1)       DEFAULT 0               NOT NULL
   , CCLI_PREFIX_CLIENT_CLAIMS      NUMBER(1)       DEFAULT 1               NOT NULL
   , CCLI_ALLOW_ACCESSALL_CST_GRTP  NUMBER(1)       DEFAULT 0               NOT NULL

   -- INSERT tracking
   , TRCK_INSERT_DATE               DATE            NOT NULL
   , TRCK_INSERT_DB_USER            NVARCHAR2(32)   NOT NULL
   , TRCK_INSERT_APP_USER           NVARCHAR2(32) 
   
   -- UPDATE tracking
   , TRCK_UPDATE_DATE               DATE            
   , TRCK_UPDATE_DB_USER            NVARCHAR2(32)   
   , TRCK_UPDATE_APP_USER           NVARCHAR2(32) 

   , CHECK (CCLI_IDENTITY_TOKEN_LIFETIME > 0) ENABLE
   , CHECK (CCLI_ACCESS_TOKEN_LIFETIME > 0) ENABLE
   , CHECK (CCLI_AUTH_CODE_LIFETIME > 0) ENABLE
   , CHECK (CCLI_ABS_REFR_TOKEN_LIFETIME > 0) ENABLE
   , CHECK (CCLI_SLID_REFR_TOKEN_LIFETIME > 0) ENABLE
   , CHECK (CCLI_FLOW IN ('authorizationcode', 'implicit', 'hybrid', 'clientcredentials', 'resourceowner', 'custom')) ENABLE
   , CHECK (CCLI_REFRESH_TOKEN_USAGE IN ('reuse', 'onetimeonly')) ENABLE
   , CHECK (CCLI_REFRESH_TOKEN_EXPIRATION IN ('sliding', 'absolute')) ENABLE
   , CHECK (CCLI_ACCESS_TOKEN_TYPE IN ('jwt', 'reference')) ENABLE
   
   -- CHECKs for tracking
   , CHECK (TRCK_INSERT_DB_USER = LOWER(TRCK_INSERT_DB_USER)) ENABLE
   , CHECK (TRCK_INSERT_APP_USER = LOWER(TRCK_INSERT_APP_USER)) ENABLE
   , CHECK (TRCK_UPDATE_DB_USER = LOWER(TRCK_UPDATE_DB_USER)) ENABLE
   , CHECK (TRCK_UPDATE_APP_USER = LOWER(TRCK_UPDATE_APP_USER)) ENABLE
   , CHECK ((TRCK_UPDATE_DATE IS NULL AND TRCK_UPDATE_DB_USER IS NULL) OR (TRCK_UPDATE_DATE IS NOT NULL AND TRCK_UPDATE_DB_USER IS NOT NULL)) ENABLE
   , CHECK (TRCK_UPDATE_DATE IS NULL OR TRCK_UPDATE_DATE >= TRCK_INSERT_DATE) ENABLE

   , CONSTRAINT pk_crvn_idn_clients PRIMARY KEY (CCLI_ID) ENABLE
   , CONSTRAINT uk_crvn_idn_clients UNIQUE (CCLI_CLIENT_ID) ENABLE
   , CONSTRAINT fk_crvnidnclients_crvnsecapps FOREIGN KEY (CAPP_ID) REFERENCES mydb.crvn_sec_apps (capp_id) ON DELETE CASCADE ENABLE
);

COMMENT ON TABLE mydb.crvn_idn_clients 
     IS 'Models an OpenID Connect or OAuth2 client';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_ID 
     IS 'Auto-increment ID';
COMMENT ON COLUMN mydb.crvn_idn_clients.CAPP_ID 
     IS 'Caravan application ID to which this client belongs';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_ENABLED 
     IS 'Specifies if client is enabled (defaults to true)';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_CLIENT_ID 
     IS 'Unique ID of the client';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_CLIENT_NAME 
     IS 'Client display name (used for logging and consent screen)';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_CLIENT_URI 
     IS 'URI to further information about client (used on consent screen)';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_LOGO_URI 
     IS 'URI to client logo (used on consent screen)';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_REQUIRE_CONSENT 
     IS 'Specifies whether a consent screen is required (defaults to true)';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_ALLOW_REMEMBER_CONSENT 
     IS 'Specifies whether user can choose to store consent decisions (defaults to true)';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_FLOW 
     IS 'Specifies allowed flow for client (either AuthorizationCode, Implicit, Hybrid, ResourceOwner, ClientCredentials or Custom). Defaults to Implicit';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_ALLOW_CLIENT_CREDS_ONLY 
     IS 'Indicates whether this client is allowed to request token using client credentials only. This is e.g. useful when you want a client to be able to use both a user-centric flow like implicit and additionally client credentials flow';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_LOGOUT_URI 
     IS 'Specifies logout URI at client for HTTP based logout';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_LOGOUT_SESSION_REQUIRED 
     IS 'Specifies is the user session ID should be sent to the LogoutUri. Defaults to true';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_ALLOW_ACCESSALL_SCOPES 
     IS 'Indicates whether the client has access to all scopes. Defaults to false. You can set the allowed scopes via the CRVN_IDN_CLI_SCOPES table';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_IDENTITY_TOKEN_LIFETIME 
     IS 'Lifetime of identity token in seconds (defaults to 300 seconds / 5 minutes)';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_ACCESS_TOKEN_LIFETIME 
     IS 'Lifetime of access token in seconds (defaults to 3600 seconds / 1 hour)';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_AUTH_CODE_LIFETIME 
     IS 'Lifetime of authorization code in seconds (defaults to 300 seconds / 5 minutes)';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_ABS_REFR_TOKEN_LIFETIME 
     IS 'Maximum lifetime of a refresh token in seconds. Defaults to 2592000 seconds / 30 days';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_SLID_REFR_TOKEN_LIFETIME 
     IS 'Sliding lifetime of a refresh token in seconds. Defaults to 1296000 seconds / 15 days';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_REFRESH_TOKEN_USAGE 
     IS 'ReUse: the refresh token handle will stay the same when refreshing tokens. OneTime: the refresh token handle will be updated when refreshing tokens';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_UPD_ACCESS_TOKEN_ON_REFR 
     IS 'Indicates whether the access token (and its claims) should be updated on a refresh token request';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_REFRESH_TOKEN_EXPIRATION 
     IS 'Absolute: the refresh token will expire on a fixed point in time (specified by the CCLI_ABS_REFR_TOKEN_LIFETIME). Sliding: when refreshing the token, the lifetime of the refresh token will be renewed (by the amount specified in CCLI_SLID_REFR_TOKEN_LIFETIME). The lifetime will not exceed CCLI_ABS_REFR_TOKEN_LIFETIME';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_ACCESS_TOKEN_TYPE 
     IS 'Specifies whether the access token is a reference token or a self contained JWT token (defaults to Jwt)';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_ENABLE_LOCAL_LOGIN 
     IS 'Indicates whether the local login is allowed for this client. Defaults to true';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_INCLUDE_JWT_ID 
     IS 'Indicates whether JWT access tokens should include an identifier';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_ALWAYS_SEND_CLIENT_CLAIMS 
     IS 'Indicates whether client claims should be always included in the access tokens - or only for client credentials flow';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_PREFIX_CLIENT_CLAIMS 
     IS 'Indicates whether all client claims should be prefixed';
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_ALLOW_ACCESSALL_CST_GRTP 
     IS 'Indicates whether the client has access to all custom grant types. Defaults to false. You can set the allowed custom grant types via the CRVN_IDN_CLI_CST_GRNT_TYPES table';

CREATE SEQUENCE mydb.sq_crvn_idn_clients NOCACHE;

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_clients
BEFORE INSERT ON mydb.crvn_idn_clients 
FOR EACH ROW
BEGIN
  SELECT mydb.sq_crvn_idn_clients.nextval, mydb.pck_caravan_utils.f_get_sysdate_utc, mydb.pck_caravan_utils.f_get_sysuser, NULL, NULL
    INTO :new.CCLI_ID, :new.TRCK_INSERT_DATE, :new.TRCK_INSERT_DB_USER, :new.TRCK_UPDATE_DATE, :new.TRCK_UPDATE_DB_USER
    FROM DUAL;
END;
/

create or replace TRIGGER mydb.tu_crvn_idn_clients
BEFORE UPDATE ON mydb.crvn_idn_clients 
FOR EACH ROW
BEGIN
  IF UPDATING('TRCK_INSERT_DATE') 
  OR UPDATING('TRCK_INSERT_DB_USER') 
  OR UPDATING('TRCK_UPDATE_DATE') 
  OR UPDATING('TRCK_UPDATE_DB_USER') 
  THEN
    mydb.pck_caravan_utils.sp_err_when_updating_trck_cols;
  END IF;

  SELECT mydb.pck_caravan_utils.f_get_sysdate_utc, mydb.pck_caravan_utils.f_get_sysuser
    INTO :new.TRCK_UPDATE_DATE, :new.TRCK_UPDATE_DB_USER
    FROM DUAL;
END;
/

-- Identity: Scopes
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_scopes
(
     CSCO_ID                        NUMBER(10)                          NOT NULL 
   , CSCO_ENABLED                   NUMBER(1)      DEFAULT 1            NOT NULL 
   , CSCO_SCOPE_NAME                NVARCHAR2(200)                      NOT NULL
   , CSCO_DISPLAY_NAME              NVARCHAR2(200)
   , CSCO_DESCR                     NVARCHAR2(1000)
   , CSCO_REQUIRED                  NUMBER(1)      DEFAULT 0            NOT NULL 
   , CSCO_EMPHASIZE                 NUMBER(1)      DEFAULT 0            NOT NULL 
   , CSCO_TYPE                      NVARCHAR2(100) DEFAULT 'resource'   NOT NULL 
   , CSCO_INCL_ALL_CLAIMS_FOR_USER  NUMBER(1)      DEFAULT 0            NOT NULL 
   , CSCO_CLAIMS_RULE               NVARCHAR2(200)
   , CSCO_SHOW_IN_DISCOVERY_DOC     NUMBER(1)      DEFAULT 1            NOT NULL 

   -- INSERT tracking
   , TRCK_INSERT_DATE               DATE            NOT NULL
   , TRCK_INSERT_DB_USER            NVARCHAR2(32)   NOT NULL
   , TRCK_INSERT_APP_USER           NVARCHAR2(32) 
   
   -- UPDATE tracking
   , TRCK_UPDATE_DATE               DATE            
   , TRCK_UPDATE_DB_USER            NVARCHAR2(32)   
   , TRCK_UPDATE_APP_USER           NVARCHAR2(32)   

   , CHECK (csco_type IN ('identity', 'resource')) ENABLE
   
   -- CHECKs for tracking
   , CHECK (TRCK_INSERT_DB_USER = LOWER(TRCK_INSERT_DB_USER)) ENABLE
   , CHECK (TRCK_INSERT_APP_USER = LOWER(TRCK_INSERT_APP_USER)) ENABLE
   , CHECK (TRCK_UPDATE_DB_USER = LOWER(TRCK_UPDATE_DB_USER)) ENABLE
   , CHECK (TRCK_UPDATE_APP_USER = LOWER(TRCK_UPDATE_APP_USER)) ENABLE
   , CHECK ((TRCK_UPDATE_DATE IS NULL AND TRCK_UPDATE_DB_USER IS NULL) OR (TRCK_UPDATE_DATE IS NOT NULL AND TRCK_UPDATE_DB_USER IS NOT NULL)) ENABLE
   , CHECK (TRCK_UPDATE_DATE IS NULL OR TRCK_UPDATE_DATE >= TRCK_INSERT_DATE) ENABLE

   , CONSTRAINT pk_crvn_idn_scopes PRIMARY KEY (CSCO_ID) ENABLE
);

COMMENT ON TABLE mydb.crvn_idn_scopes 
     IS 'Models a resource (either identity resource or web api resource)';
COMMENT ON COLUMN mydb.crvn_idn_scopes.CSCO_ID 
     IS 'Auto-increment ID';
COMMENT ON COLUMN mydb.crvn_idn_scopes.CSCO_ENABLED 
     IS 'Indicates if scope is enabled and can be requested. Defaults to true';
COMMENT ON COLUMN mydb.crvn_idn_scopes.CSCO_SCOPE_NAME
     IS 'Name of the scope. This is the value a client will use to request the scope';
COMMENT ON COLUMN mydb.crvn_idn_scopes.CSCO_DISPLAY_NAME
     IS 'Display name. This value will be used e.g. on the consent screen';
COMMENT ON COLUMN mydb.crvn_idn_scopes.CSCO_DESCR
     IS 'Description. This value will be used e.g. on the consent screen';
COMMENT ON COLUMN mydb.crvn_idn_scopes.CSCO_REQUIRED
     IS 'Specifies whether the user can de-select the scope on the consent screen. Defaults to false';
COMMENT ON COLUMN mydb.crvn_idn_scopes.CSCO_EMPHASIZE
     IS 'Specifies whether the consent screen will emphasize this scope. Use this setting for sensitive or important scopes. Defaults to false';
COMMENT ON COLUMN mydb.crvn_idn_scopes.CSCO_TYPE
     IS 'Specifies whether this scope is about identity information from the userinfo endpoint, or a resource (e.g. a Web API). Defaults to Resource';
COMMENT ON COLUMN mydb.crvn_idn_scopes.CSCO_INCL_ALL_CLAIMS_FOR_USER
     IS 'If enabled, all claims for the user will be included in the token. Defaults to false';
COMMENT ON COLUMN mydb.crvn_idn_scopes.CSCO_CLAIMS_RULE
     IS 'Rule for determining which claims should be included in the token (this is implementation specific)';
COMMENT ON COLUMN mydb.crvn_idn_scopes.CSCO_SHOW_IN_DISCOVERY_DOC
     IS 'Specifies whether this scope is shown in the discovery document. Defaults to true';

CREATE SEQUENCE mydb.sq_crvn_idn_scopes NOCACHE;


-- Identity: Scopes ID trigger
-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_scopes
BEFORE INSERT ON mydb.crvn_idn_scopes 
FOR EACH ROW
BEGIN
  SELECT mydb.sq_crvn_idn_scopes.nextval, mydb.pck_caravan_utils.f_get_sysdate_utc, mydb.pck_caravan_utils.f_get_sysuser, NULL, NULL
    INTO :new.CSCO_ID, :new.TRCK_INSERT_DATE, :new.TRCK_INSERT_DB_USER, :new.TRCK_UPDATE_DATE, :new.TRCK_UPDATE_DB_USER
    FROM DUAL;
END;
/

-- Identity: Client claims
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_cli_claims
(
     CCLM_ID        NUMBER(10)      NOT NULL
   , CCLI_ID        NUMBER(10)      NOT NULL
   , CCLM_TYPE      NVARCHAR2(250)  NOT NULL
   , CCLM_VALUE     NVARCHAR2(250)  NOT NULL

   , CONSTRAINT pk_crvn_idn_cli_claims PRIMARY KEY (CCLI_ID) ENABLE
   , CONSTRAINT fk_crvnidn_cliclaims_clients FOREIGN KEY (CCLI_ID) REFERENCES mydb.crvn_idn_clients (CCLI_ID) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.sq_crvn_idn_cli_claims NOCACHE;

-- Identity: Client claims ID trigger
-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_cli_claims
BEFORE INSERT ON mydb.crvn_idn_cli_claims 
FOR EACH ROW
BEGIN
  SELECT mydb.sq_crvn_idn_cli_claims.nextval
    INTO :new.CCLM_ID
    FROM DUAL;
END;
/

-- Identity: Client CORS origins
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_cli_cors_origins
(
     CCCO_ID                NUMBER(10)      NOT NULL
   , CCLI_ID                NUMBER(10)      NOT NULL
   , CCCO_ORIGIN            NVARCHAR2(150)  NOT NULL

   , CONSTRAINT pk_crvn_idn_cli_cors_origins PRIMARY KEY (CCCO_ID) ENABLE
   , CONSTRAINT fk_crvnidn_clicorsorig_clients FOREIGN KEY (CCLI_ID) REFERENCES mydb.crvn_idn_clients (CCLI_ID) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.sq_crvn_idn_cli_cors_origins NOCACHE;


-- Identity: Client CORS origins ID trigger
-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_cli_cors_origins
BEFORE INSERT ON mydb.crvn_idn_cli_cors_origins 
FOR EACH ROW
BEGIN
  SELECT mydb.sq_crvn_idn_cli_cors_origins.nextval
    INTO :new.CCCO_ID
    FROM DUAL;
END;
/

-- Identity: Scope secrets
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_sco_secrets
(
     CSSE_ID                NUMBER(10)      NOT NULL
   , CSCO_ID                NUMBER(10)      NOT NULL
   , CSSE_VALUE             NVARCHAR2(250)  NOT NULL
   , CSSE_TYPE              NVARCHAR2(250)  DEFAULT 'SharedSecret'
   , CSSE_DESCR             NVARCHAR2(2000)      
   , CSSE_EXPIRATION        DATE  

   , CONSTRAINT pk_crvn_idn_sco_secrets PRIMARY KEY (CSSE_ID) ENABLE
   , CONSTRAINT fk_crvnidnsco_secrets_scopes FOREIGN KEY (CSCO_ID) REFERENCES mydb.crvn_idn_scopes (CSCO_ID) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.sq_crvn_idn_sco_secrets NOCACHE;

COMMENT ON COLUMN mydb.crvn_idn_sco_secrets.CSSE_VALUE 
     IS 'Hash SHA256 o SHA512 codificato in Base64';

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_sco_secrets
BEFORE INSERT ON mydb.crvn_idn_sco_secrets 
FOR EACH ROW
BEGIN
  SELECT mydb.sq_crvn_idn_sco_secrets.nextval
    INTO :new.CSSE_ID
    FROM DUAL;
END;
/

-- Users
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_users
(
     cusr_id                        NUMBER(19)      NOT NULL
   , capp_id                        NUMBER(10)      NOT NULL 
   , cusr_login                     NVARCHAR2(32)   NOT NULL
   , cusr_hashed_pwd                NVARCHAR2(256)
   , cusr_active                    NUMBER(1)       DEFAULT 0 NOT NULL
   , cusr_first_name                NVARCHAR2(256)
   , cusr_last_name                 NVARCHAR2(256)
   , cusr_email                     NVARCHAR2(256)
   , cusr_email_confirmed           NUMBER(1)       DEFAULT 0 NOT NULL
   , cusr_security_stamp            NVARCHAR2(256)
   , cusr_lockout_enabled           NUMBER(1)       DEFAULT 1 NOT NULL
   , cusr_lockout_end_date          DATE            NOT NULL
   , cusr_access_failed_count       NUMBER(3)       DEFAULT 0 NOT NULL
   , cusr_two_factor_auth_enabled   NUMBER(1)       DEFAULT 0 NOT NULL

   -- INSERT tracking
   , TRCK_INSERT_DATE               DATE            NOT NULL
   , TRCK_INSERT_DB_USER            NVARCHAR2(32)   NOT NULL
   , TRCK_INSERT_APP_USER           NVARCHAR2(32) 
   
   -- UPDATE tracking
   , TRCK_UPDATE_DATE               DATE            
   , TRCK_UPDATE_DB_USER            NVARCHAR2(32)   
   , TRCK_UPDATE_APP_USER           NVARCHAR2(32) 

   , CHECK (cusr_login = lower(cusr_login)) ENABLE
   
   -- CHECKs for tracking
   , CHECK (TRCK_INSERT_DB_USER = LOWER(TRCK_INSERT_DB_USER)) ENABLE
   , CHECK (TRCK_INSERT_APP_USER = LOWER(TRCK_INSERT_APP_USER)) ENABLE
   , CHECK (TRCK_UPDATE_DB_USER = LOWER(TRCK_UPDATE_DB_USER)) ENABLE
   , CHECK (TRCK_UPDATE_APP_USER = LOWER(TRCK_UPDATE_APP_USER)) ENABLE
   , CHECK ((TRCK_UPDATE_DATE IS NULL AND TRCK_UPDATE_DB_USER IS NULL) OR (TRCK_UPDATE_DATE IS NOT NULL AND TRCK_UPDATE_DB_USER IS NOT NULL)) ENABLE
   , CHECK (TRCK_UPDATE_DATE IS NULL OR TRCK_UPDATE_DATE >= TRCK_INSERT_DATE) ENABLE

   , CONSTRAINT pk_crvn_sec_users PRIMARY KEY (cusr_id) ENABLE
   , CONSTRAINT uk_crvn_sec_users UNIQUE (capp_id, cusr_login) ENABLE
   , CONSTRAINT fk_crvnsecusers_crvnsecapps FOREIGN KEY (capp_id) REFERENCES mydb.crvn_sec_apps (capp_id) ON DELETE CASCADE ENABLE
);

COMMENT ON TABLE mydb.crvn_sec_users 
     IS 'Tabella che censisce gli utenti delle applicazioni FINSA';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_id 
     IS 'Identificativo riga, è una sequenza autoincrementale';
COMMENT ON COLUMN mydb.crvn_sec_users.capp_id 
     IS 'Identificativo della applicazione a cui un certo utente appartiene';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_login 
     IS 'La sigla usata per effettuare la login';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_hashed_pwd 
     IS 'Hash della password fissata per un certo utente';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_active 
     IS 'Indica se un certo utente sia attivo o meno';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_first_name 
     IS 'Nome di un certo utente';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_last_name 
     IS 'Cognome di un certo utente';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_email 
     IS 'Indirizzo e-mail di un certo utente';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_email_confirmed 
     IS 'Indica se il dato indirizzo e-mail sia stato confermato';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_phone 
     IS 'Numero di telefono di un certo utente';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_phone_confirmed 
     IS 'Indica se il dato numero di telefono sia stato confermato';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_security_stamp 
     IS 'Rappresenta una sintesi delle informazioni di accesso per un certo utente';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_lockout_enabled 
     IS 'Indica se ad un certo utente possa essere bloccata la login a fronte di un certo numero di accessi falliti';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_lockout_end_date 
     IS 'Indica la data di fine del blocco della login, valorizzare nel passato in fase di creazione utente';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_access_failed_count 
     IS 'Il numero di login fallite per un certo utente';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_two_factor_auth_enabled 
     IS 'Indica se un certo utente abbia abilitato la autenticazione a due fattori';

CREATE SEQUENCE mydb.crvn_sec_users_id NOCACHE;

CREATE OR REPLACE TRIGGER mydb.ti_crvn_sec_users
BEFORE INSERT ON mydb.crvn_sec_users 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_sec_users_id.nextval, lower(:new.cusr_login), mydb.pck_caravan_utils.f_get_sysdate_utc, mydb.pck_caravan_utils.f_get_sysdate_utc, mydb.pck_caravan_utils.f_get_sysuser, NULL, NULL
    INTO :new.cusr_id, :new.cusr_login, :new.cusr_lockout_end_date, :new.TRCK_INSERT_DATE, :new.TRCK_INSERT_DB_USER, :new.TRCK_UPDATE_DATE, :new.TRCK_UPDATE_DB_USER
    FROM DUAL;
END;
/

create or replace TRIGGER mydb.tu_crvn_sec_users
BEFORE UPDATE ON mydb.crvn_sec_users 
FOR EACH ROW
BEGIN
  IF UPDATING('TRCK_INSERT_DATE') 
  OR UPDATING('TRCK_INSERT_DB_USER') 
  OR UPDATING('TRCK_UPDATE_DATE') 
  OR UPDATING('TRCK_UPDATE_DB_USER') 
  THEN
    mydb.pck_caravan_utils.sp_err_when_updating_trck_cols;
  END IF;

  SELECT mydb.pck_caravan_utils.f_get_sysdate_utc, mydb.pck_caravan_utils.f_get_sysuser
    INTO :new.TRCK_UPDATE_DATE, :new.TRCK_UPDATE_DB_USER
    FROM DUAL;
END;
/

-- Groups
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_groups
(
     cgrp_id          NUMBER(10)      NOT NULL
   , capp_id          NUMBER(10)      NOT NULL 
   , cgrp_name        NVARCHAR2(32)   NOT NULL
   , cgrp_descr       NVARCHAR2(256)  NOT NULL
   , cgrp_notes       NVARCHAR2(1024) NOT NULL
   , CHECK (cgrp_name = lower(cgrp_name)) ENABLE
   , CONSTRAINT pk_crvn_sec_groups PRIMARY KEY (cgrp_id) ENABLE
   , CONSTRAINT uk_crvn_sec_groups UNIQUE (capp_id, cgrp_name) ENABLE
   , CONSTRAINT fk_crvnsecgroups_crvnsecapps FOREIGN KEY (capp_id) REFERENCES mydb.crvn_sec_apps (capp_id) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.crvn_sec_groups_id NOCACHE;

-- Roles
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_roles
(
     crol_id          NUMBER(10)      NOT NULL
   , cgrp_id          NUMBER(10)      NOT NULL 
   , crol_name        NVARCHAR2(32)   NOT NULL
   , crol_descr       NVARCHAR2(256)  NOT NULL
   , crol_notes       NVARCHAR2(1024) NOT NULL
   , CHECK (crol_name = lower(crol_name)) ENABLE
   , CONSTRAINT pk_crvn_sec_roles PRIMARY KEY (crol_id) ENABLE
   , CONSTRAINT uk_crvn_sec_roles UNIQUE (cgrp_id, crol_name) ENABLE
   , CONSTRAINT fk_crvnsecroles_crvnsecgrps FOREIGN KEY (cgrp_id) REFERENCES mydb.crvn_sec_groups (cgrp_id) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.crvn_sec_roles_id NOCACHE;

-- User Roles
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_user_roles
(
     cusr_id          NUMBER(19)      NOT NULL
   , crol_id          NUMBER(10)      NOT NULL
   , CONSTRAINT pk_crvn_sec_usrrol PRIMARY KEY (cusr_id, crol_id) ENABLE
   , CONSTRAINT fk_crvnsecusrrol_crvnsecusr FOREIGN KEY (cusr_id) REFERENCES mydb.crvn_sec_users (cusr_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecusrrol_crvnsecrol FOREIGN KEY (crol_id) REFERENCES mydb.crvn_sec_roles (crol_id) ON DELETE CASCADE ENABLE
);

-- Contexts
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_contexts
(
     cctx_id          NUMBER(10)      NOT NULL
   , capp_id          NUMBER(10)      NOT NULL 
   , cctx_name        NVARCHAR2(32)   NOT NULL
   , cctx_descr       NVARCHAR2(256)  NOT NULL
   , CHECK (cctx_name = lower(cctx_name)) ENABLE
   , CONSTRAINT pk_crvn_sec_contexts PRIMARY KEY (cctx_id) ENABLE
   , CONSTRAINT uk_crvn_sec_contexts UNIQUE (capp_id, cctx_name) ENABLE   
   , CONSTRAINT fk_crvnsecctxs_crvnsecapps FOREIGN KEY (capp_id) REFERENCES mydb.crvn_sec_apps (capp_id) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.crvn_sec_contexts_id NOCACHE;

-- Objects
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_objects
(
     cobj_id          NUMBER(19)      NOT NULL
   , cctx_id          NUMBER(10)      NOT NULL
   , cobj_name        NVARCHAR2(32)   NOT NULL
   , cobj_descr       NVARCHAR2(256)  NOT NULL
   , cobj_type        NVARCHAR2(8)    NOT NULL
   , CHECK (cobj_name = lower(cobj_name)) ENABLE
   , CONSTRAINT pk_crvn_sec_objects PRIMARY KEY (cobj_id) ENABLE
   , CONSTRAINT uk_crvn_sec_objects UNIQUE (cctx_id, cobj_name) ENABLE
   , CONSTRAINT fk_crvnsecobjs_crvnsecctxs FOREIGN KEY (cctx_id) REFERENCES mydb.crvn_sec_contexts (cctx_id) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.crvn_sec_objects_id NOCACHE;

-- Claims
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_claims
(
     ccla_id          NUMBER(19)      NOT NULL
   , cusr_id          NUMBER(19)      NOT NULL 
   , ccla_hash        NVARCHAR2(32)   NOT NULL
   , ccla_claim       NCLOB           NOT NULL

   -- INSERT tracking
   , TRCK_INSERT_DATE               DATE            NOT NULL
   , TRCK_INSERT_DB_USER            NVARCHAR2(32)   NOT NULL
   , TRCK_INSERT_APP_USER           NVARCHAR2(32) 
   
   -- UPDATE tracking
   , TRCK_UPDATE_DATE               DATE            
   , TRCK_UPDATE_DB_USER            NVARCHAR2(32)   
   , TRCK_UPDATE_APP_USER           NVARCHAR2(32) 
   
   -- CHECKs for tracking
   , CHECK (TRCK_INSERT_DB_USER = LOWER(TRCK_INSERT_DB_USER)) ENABLE
   , CHECK (TRCK_INSERT_APP_USER = LOWER(TRCK_INSERT_APP_USER)) ENABLE
   , CHECK (TRCK_UPDATE_DB_USER = LOWER(TRCK_UPDATE_DB_USER)) ENABLE
   , CHECK (TRCK_UPDATE_APP_USER = LOWER(TRCK_UPDATE_APP_USER)) ENABLE
   , CHECK ((TRCK_UPDATE_DATE IS NULL AND TRCK_UPDATE_DB_USER IS NULL) OR (TRCK_UPDATE_DATE IS NOT NULL AND TRCK_UPDATE_DB_USER IS NOT NULL)) ENABLE
   , CHECK (TRCK_UPDATE_DATE IS NULL OR TRCK_UPDATE_DATE >= TRCK_INSERT_DATE) ENABLE

   , CONSTRAINT pk_crvn_sec_claims PRIMARY KEY (ccla_id) ENABLE
   , CONSTRAINT uk_crvn_sec_claims UNIQUE (cusr_id, ccla_hash) ENABLE
   , CONSTRAINT fk_crvnsecclm_crvnsecusr FOREIGN KEY (cusr_id) REFERENCES mydb.crvn_sec_users (cusr_id) ON DELETE CASCADE ENABLE
);

COMMENT ON TABLE mydb.crvn_sec_claims 
     IS 'Tabella che censisce i claim degli utenti delle applicazioni FINSA';
COMMENT ON COLUMN mydb.crvn_sec_claims.ccla_id 
     IS 'Identificativo riga, è una sequenza autoincrementale';
COMMENT ON COLUMN mydb.crvn_sec_claims.cusr_id 
     IS 'Identificativo dello utente a cui un certo claim appartiene';
COMMENT ON COLUMN mydb.crvn_sec_claims.ccla_hash 
     IS 'Hash prodotto dopo la serializzazione del claim';
COMMENT ON COLUMN mydb.crvn_sec_claims.ccla_claim 
     IS 'Il claim serializzato';

CREATE SEQUENCE mydb.sq_crvn_sec_claims NOCACHE;

CREATE OR REPLACE TRIGGER mydb.ti_crvn_sec_claims
BEFORE INSERT ON mydb.crvn_sec_claims 
FOR EACH ROW
BEGIN
  SELECT mydb.sq_crvn_sec_claims.nextval, mydb.pck_caravan_utils.f_get_sysdate_utc, mydb.pck_caravan_utils.f_get_sysuser, NULL, NULL
    INTO :new.ccla_id, :new.TRCK_INSERT_DATE, :new.TRCK_INSERT_DB_USER, :new.TRCK_UPDATE_DATE, :new.TRCK_UPDATE_DB_USER
    FROM DUAL;
END;
/

create or replace TRIGGER mydb.tu_crvn_sec_claims
BEFORE UPDATE ON mydb.crvn_sec_claims 
FOR EACH ROW
BEGIN
  IF UPDATING('TRCK_INSERT_DATE') 
  OR UPDATING('TRCK_INSERT_DB_USER') 
  OR UPDATING('TRCK_UPDATE_DATE') 
  OR UPDATING('TRCK_UPDATE_DB_USER') 
  THEN
    mydb.pck_caravan_utils.sp_err_when_updating_trck_cols;
  END IF;

  SELECT mydb.pck_caravan_utils.f_get_sysdate_utc, mydb.pck_caravan_utils.f_get_sysuser
    INTO :new.TRCK_UPDATE_DATE, :new.TRCK_UPDATE_DB_USER
    FROM DUAL;
END;
/

-- Sec Entries
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_entries
(
     csec_id          NUMBER(10)      NOT NULL
   , cobj_id          NUMBER(19)      NOT NULL
   , cusr_id          NUMBER(19)      -- Might be null, either user, group or role
   , cgrp_id          NUMBER(10)      -- Might be null, either user, group or role
   , crol_id          NUMBER(10)      -- Might be null, either user, group or role
   , CHECK ((cusr_id IS NULL AND crol_id IS NULL AND cgrp_id IS NOT NULL) OR 
            (cusr_id IS NOT NULL AND crol_id IS NULL AND cgrp_id IS NULL) OR
			(cusr_id IS NULL AND crol_id IS NOT NULL AND cgrp_id IS NULL)) ENABLE
   , CONSTRAINT pk_crvn_sec_entries PRIMARY KEY (csec_id) ENABLE
   , CONSTRAINT fk_crvnsec_crvnsecobj FOREIGN KEY (cobj_id) REFERENCES mydb.crvn_sec_objects (cobj_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsec_crvnsecusr FOREIGN KEY (cusr_id) REFERENCES mydb.crvn_sec_users (cusr_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsec_crvnsecgrp FOREIGN KEY (cgrp_id) REFERENCES mydb.crvn_sec_groups (cgrp_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsec_crvnsecrol FOREIGN KEY (crol_id) REFERENCES mydb.crvn_sec_roles (crol_id) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.crvn_sec_entries_id NOCACHE;

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

CREATE OR REPLACE FORCE VIEW mydb."V_CARAVAN_LOG" ("CLOG_ID", "CAPP_ID", "CAPP_NAME", "CLOS_TYPE", "CLOG_DATE", "CUSR_LOGIN", "CLOG_CODE_UNIT", "CLOG_FUNCTION", "CLOG_SHORT_MSG", "CLOG_LONG_MSG", "CLOG_CONTEXT", "CLOG_KEY_0", "CLOG_VALUE_0", "CLOG_KEY_1", "CLOG_VALUE_1", "CLOG_KEY_2", "CLOG_VALUE_2", "CLOG_KEY_3", "CLOG_VALUE_3", "CLOG_KEY_4", "CLOG_VALUE_4", "CLOG_KEY_5", "CLOG_VALUE_5", "CLOG_KEY_6", "CLOG_VALUE_6", "CLOG_KEY_7", "CLOG_VALUE_7", "CLOG_KEY_8", "CLOG_VALUE_8", "CLOG_KEY_9", "CLOG_VALUE_9") AS 
SELECT "CLOG_ID",
       a."CAPP_ID",
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

-- Views: V_CARAVAN_LOG_SUMMARY
-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE FORCE VIEW mydb."V_CARAVAN_LOG_SUMMARY" ("CAPP_ID", "CAPP_NAME", "CLOS_TYPE", "CLOG_ENTRIES", "CLOS_MAX_ENTRIES", "CLOG_MIN_DATE", "CLOG_MAX_DATE", "CLOG_DAYS", "CLOS_MAX_DAYS", "CLOS_ENABLED") AS
SELECT a."CAPP_ID",
       a."CAPP_NAME",
       s."CLOS_TYPE",
       COUNT(*) CLOG_ENTRIES,
       MAX(s."CLOS_MAX_ENTRIES") CLOS_MAX_ENTRIES,
       MIN(e."CLOG_DATE") CLOG_MIN_DATE,
       MAX(e."CLOG_DATE") CLOG_MAX_DATE,
       (TRUNC(SYS_EXTRACT_UTC(SYSTIMESTAMP)) - TRUNC(MIN(e."CLOG_DATE"))) CLOG_DAYS,
       MAX(s."CLOS_DAYS") CLOS_MAX_DAYS,
       MAX(s."CLOS_ENABLED") CLOS_ENABLED
    FROM mydb.crvn_sec_apps a
    LEFT JOIN mydb.crvn_log_settings s ON s."CAPP_ID" = a."CAPP_ID"
    LEFT JOIN mydb.crvn_log_entries e ON e."CAPP_ID" = a."CAPP_ID" AND e."CLOS_TYPE" = s."CLOS_TYPE"
   GROUP BY a."CAPP_ID", a."CAPP_NAME", s."CLOS_TYPE"
   ORDER BY a."CAPP_NAME", s."CLOS_TYPE"