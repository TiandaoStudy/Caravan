-- REPLACE 'mydb' WITH DB NAME
-- REPLACE 'myapp' WITH APP NAME

CREATE TABLE mydb.caravan_sec_app
(
     capp_name        NVARCHAR2(30)   NOT NULL
   , capp_description NVARCHAR2(100)  NOT NULL
   , CHECK (capp_name = lower(capp_name)) ENABLE
   , CONSTRAINT pk_caravan_sec_app PRIMARY KEY (capp_name) USING INDEX TABLESPACE dati_base_index ENABLE
)
TABLESPACE dati_base;

INSERT INTO mydb.caravan_sec_app (capp_name, capp_description) VALUES ('myapp', 'MyApp Description');