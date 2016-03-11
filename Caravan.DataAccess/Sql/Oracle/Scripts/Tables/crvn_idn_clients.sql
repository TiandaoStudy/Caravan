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
   , CCLI_REQUIRE_SIGNOUT_PROMPT    NUMBER(1)       DEFAULT 0               NOT NULL
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
COMMENT ON COLUMN mydb.crvn_idn_clients.CCLI_REQUIRE_SIGNOUT_PROMPT 
     IS 'Specifies is the user session ID should be sent to the LogoutUri. Defaults to false';
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