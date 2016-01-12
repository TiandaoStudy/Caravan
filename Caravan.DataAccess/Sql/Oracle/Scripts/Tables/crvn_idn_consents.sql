﻿-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_consents
(
     CCON_SUBJECT               NVARCHAR2(200)  NOT NULL
   , CCLI_CLIENT_ID             NVARCHAR2(200)  NOT NULL
   , CSCO_SCOPE_NAMES           NVARCHAR2(2000) NOT NULL

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

   , CONSTRAINT pk_crvn_idn_consents PRIMARY KEY (CCON_SUBJECT, CCLI_CLIENT_ID) ENABLE
);