-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_contexts
(
     cctx_id          NUMBER          NOT NULL
   , capp_id          NUMBER          NOT NULL 
   , cctx_name        NVARCHAR2(32)   NOT NULL
   , cctx_descr       NVARCHAR2(256)  NOT NULL
   , CHECK (cctx_name = lower(cctx_name)) ENABLE
   , CONSTRAINT pk_crvn_sec_context PRIMARY KEY (cctx_id) ENABLE
   , CONSTRAINT uk_crvn_sec_context UNIQUE (capp_id, cctx_name) ENABLE   
   , CONSTRAINT fk_crvnsecctx_crvnsecapp FOREIGN KEY (capp_id) REFERENCES mydb.crvn_sec_app (capp_id) ON DELETE CASCADE ENABLE
);