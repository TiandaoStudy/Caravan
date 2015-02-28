-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.caravan_sec_user_group
(
     cusr_id          NUMBER          NOT NULL 
   , cusr_app_id      NUMBER          NOT NULL
   , cgrp_id          NUMBER          NOT NULL
   , cgrp_app_id      NUMBER          NOT NULL   
   , CHECK (cusr_app_id = cgrp_app_id) ENABLE
   , CONSTRAINT pk_caravan_sec_usrgrp PRIMARY KEY (cusr_id, cusr_app_id, cgrp_id, cgrp_app_id) USING INDEX TABLESPACE dati_base_index ENABLE
   , CONSTRAINT fk_crvnsecusrgrp_crvnsecusr FOREIGN KEY (cusr_id, cusr_app_id) REFERENCES mydb.caravan_sec_user (cusr_id, capp_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecusrgrp_crvnsecgrp FOREIGN KEY (cgrp_id, cgrp_app_id) REFERENCES mydb.caravan_sec_group (cgrp_id, capp_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecusrgrp1_crvnsecapp FOREIGN KEY (cusr_app_id) REFERENCES mydb.caravan_sec_app (capp_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecusrgrp2_crvnsecapp FOREIGN KEY (cgrp_app_id) REFERENCES mydb.caravan_sec_app (capp_id) ON DELETE CASCADE ENABLE
)
TABLESPACE dati_base;

INSERT INTO mydb.caravan_sec_user_group (cusr_id, cusr_app_id, cgrp_id, cgrp_app_id) VALUES (0, 0, 0, 0);