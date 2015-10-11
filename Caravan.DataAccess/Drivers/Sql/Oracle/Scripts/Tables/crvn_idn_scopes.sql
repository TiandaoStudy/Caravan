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

CREATE SEQUENCE mydb.sq_crvn_idn_scopes;
