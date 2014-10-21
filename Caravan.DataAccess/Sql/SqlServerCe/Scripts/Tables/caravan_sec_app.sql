-- REPLACE 'dbo' WITH SCHEMA NAME
-- REPLACE 'myapp' WITH APP NAME

CREATE TABLE caravan_sec_app
(
     capp_id          BIGINT         NOT NULL 
   , capp_name        NVARCHAR(30)   NOT NULL
   , capp_description NVARCHAR(100)  NOT NULL
   , CONSTRAINT pk_caravan_sec_app PRIMARY KEY (capp_id)
   , CONSTRAINT uk_caravan_sec_app UNIQUE (capp_name)
);

, CHECK (capp_name = lower(capp_name)) ENABLE

INSERT INTO caravan_sec_app (capp_id, capp_name, capp_description) VALUES (0, 'myapp', 'MyApp Description');