-- REPLACE 'mydb' WITH DB NAME
-- REPLACE 'myapp' WITH APP NAME

CREATE TABLE mydb.caravan_sec_app
(
     capp_id          NUMBER          NOT NULL 
   , capp_name        NVARCHAR2(2000)   NOT NULL
   , capp_description NVARCHAR2(2000)  NOT NULL
   , CHECK (capp_name = lower(capp_name)) ENABLE
   , CONSTRAINT pk_caravan_sec_app PRIMARY KEY (capp_id) USING INDEX TABLESPACE dati_base_index ENABLE
   , CONSTRAINT uk_caravan_sec_app UNIQUE (capp_name) USING INDEX TABLESPACE dati_base_index ENABLE
)
TABLESPACE dati_base;

INSERT INTO mydb.caravan_sec_app (capp_id, capp_name, capp_description) VALUES (0, 'myapp', 'MyApp Description');