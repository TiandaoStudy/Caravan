-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.caravan_sec_user_group
(
     cusr_id          NUMBER          NOT NULL 
   , cgrp_id          NUMBER          NOT NULL
   , capp_id          NUMBER          NOT NULL
   , CONSTRAINT pk_caravan_sec_usrgrp PRIMARY KEY (cusr_id, cgrp_id, capp_id) USING INDEX TABLESPACE dati_base_index ENABLE
   , CONSTRAINT fk_crvnsecusrgrp_crvnsecusr FOREIGN KEY (cusr_id, capp_id) REFERENCES userbase.caravan_sec_user (cusr_id, capp_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecusrgrp_crvnsecgrp FOREIGN KEY (cgrp_id, capp_id) REFERENCES userbase.caravan_sec_group (cgrp_id, capp_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecusrgrp_crvnsecapp FOREIGN KEY (capp_id) REFERENCES mydb.caravan_sec_app (capp_id) ON DELETE CASCADE ENABLE
)
TABLESPACE dati_base;

INSERT INTO mydb.caravan_sec_user_group (cusr_id, cgrp_id, capp_id) VALUES (0, 0, 0);