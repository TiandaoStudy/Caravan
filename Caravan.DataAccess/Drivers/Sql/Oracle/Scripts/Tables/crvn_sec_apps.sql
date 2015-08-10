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