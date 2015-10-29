-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_objects
(
     cobj_id          NUMBER          NOT NULL
   , cctx_id          NUMBER          NOT NULL
   , cobj_name        NVARCHAR2(32)   NOT NULL
   , cobj_descr       NVARCHAR2(256)  NOT NULL
   , cobj_type        NVARCHAR2(8)    NOT NULL
   , CHECK (cobj_name = lower(cobj_name)) ENABLE
   , CONSTRAINT pk_crvn_sec_objects PRIMARY KEY (cobj_id) ENABLE
   , CONSTRAINT uk_crvn_sec_objects UNIQUE (cctx_id, cobj_name) ENABLE
   , CONSTRAINT fk_crvnsecobjs_crvnsecctxs FOREIGN KEY (cctx_id) REFERENCES mydb.crvn_sec_contexts (cctx_id) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.crvn_sec_objects_id NOCACHE;