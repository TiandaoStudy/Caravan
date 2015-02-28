-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.caravan_security
(
     csec_id          NUMBER          NOT NULL
   , capp_id          NUMBER          NOT NULL
   , cusr_id          NUMBER          -- Might be null, either user or group
   , cgrp_id          NUMBER          -- Might be null, either group or user
   , cctx_id          NUMBER          NOT NULL
   , cobj_id          NUMBER          NOT NULL
   , CHECK ((cusr_id IS NULL AND cgrp_id IS NOT NULL) OR (cusr_id IS NOT NULL AND cgrp_id IS NULL)) ENABLE
   , CONSTRAINT pk_caravan_security PRIMARY KEY (csec_id, capp_id) USING INDEX TABLESPACE dati_base_index ENABLE
   , CONSTRAINT fk_crvnsecurity_crvnsecapp FOREIGN KEY (capp_id) REFERENCES mydb.caravan_sec_app (capp_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecurity_crvnsecusr FOREIGN KEY (cusr_id, capp_id) REFERENCES mydb.caravan_sec_user (cusr_id, capp_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecurity_crvnsecgrp FOREIGN KEY (cgrp_id, capp_id) REFERENCES mydb.caravan_sec_group (cgrp_id, capp_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecurity_crvnsecctx FOREIGN KEY (cctx_id, capp_id) REFERENCES mydb.caravan_sec_context (cctx_id, capp_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecurity_crvnsecobj FOREIGN KEY (cobj_id, cctx_id, capp_id) REFERENCES mydb.caravan_sec_object (cobj_id, cctx_id, capp_id) ON DELETE CASCADE ENABLE
)
TABLESPACE dati_base;