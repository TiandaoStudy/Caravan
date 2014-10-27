-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.caravan_sec_object
(
     cobj_id          NUMBER          NOT NULL
   , cctx_id          NUMBER          NOT NULL
   , capp_id          NUMBER          NOT NULL
   , cobj_name        NVARCHAR2(30)   NOT NULL
   , cobj_description NVARCHAR2(100)  NOT NULL
   , cobj_type        NVARCHAR2(20)   NOT NULL
   , CHECK (cobj_name = lower(cobj_name)) ENABLE
   , CONSTRAINT pk_caravan_sec_object PRIMARY KEY (cobj_id, cctx_id, capp_id) USING INDEX TABLESPACE dati_base_index ENABLE
   , CONSTRAINT uk_caravan_sec_object UNIQUE (cobj_name, cctx_id) USING INDEX TABLESPACE dati_base_index ENABLE
   , CONSTRAINT fk_crvnsecobj_crvnsecctx FOREIGN KEY (cctx_id, capp_id) REFERENCES mydb.caravan_sec_context (cctx_id, capp_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecobj_crvnsecapp FOREIGN KEY (capp_id) REFERENCES mydb.caravan_sec_app (capp_id) ON DELETE CASCADE ENABLE
)
TABLESPACE dati_base;