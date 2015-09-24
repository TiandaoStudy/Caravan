-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_apps
(
     capp_id          NUMBER(10)      NOT NULL 
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