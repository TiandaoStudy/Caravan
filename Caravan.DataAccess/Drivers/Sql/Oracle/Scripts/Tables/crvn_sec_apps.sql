-- REPLACE 'mydb' WITH DB NAME
-- REPLACE 'myapp' WITH APP NAME

CREATE TABLE mydb.crvn_sec_apps
(
     capp_id          NUMBER          NOT NULL 
   , capp_name        NVARCHAR2(32)   NOT NULL
   , capp_descr       NVARCHAR2(256)  NOT NULL
   , CHECK (capp_name = lower(capp_name)) ENABLE
   , CONSTRAINT pk_caravan_sec_app PRIMARY KEY (capp_id) ENABLE
   , CONSTRAINT uk_caravan_sec_app UNIQUE (capp_name) ENABLE
)
;

INSERT INTO mydb.caravan_sec_app (capp_id, capp_name, capp_descr) VALUES (0, 'myapp', 'MyApp Description');