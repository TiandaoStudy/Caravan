-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.caravan_sec_context
(
     cctx_id          NUMBER          NOT NULL
   , capp_id          NUMBER          NOT NULL 
   , cctx_name        NVARCHAR2(30)   NOT NULL
   , cctx_description NVARCHAR2(100)  NOT NULL
   , CHECK (cctx_name = lower(cctx_name)) ENABLE
   , CONSTRAINT pk_caravan_sec_context PRIMARY KEY (cctx_id, capp_id) USING INDEX TABLESPACE dati_base_index ENABLE
   , CONSTRAINT uk_caravan_sec_context UNIQUE (cctx_name, capp_id) USING INDEX TABLESPACE dati_base_index ENABLE   
   , CONSTRAINT fk_crvnsecctx_crvnsecapp FOREIGN KEY (capp_id) REFERENCES mydb.caravan_sec_app (capp_id) ON DELETE CASCADE ENABLE
)
TABLESPACE dati_base;