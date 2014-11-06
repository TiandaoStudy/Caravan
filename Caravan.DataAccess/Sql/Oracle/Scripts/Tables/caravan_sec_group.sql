-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.caravan_sec_group
(
     cgrp_id          NUMBER          NOT NULL
   , capp_id          NUMBER          NOT NULL 
   , cgrp_name        NVARCHAR2(50)   NOT NULL
   , cgrp_description NVARCHAR2(150)  NOT NULL
   , cgrp_admin       NUMBER(1)       NOT NULL
   , CHECK (cgrp_name = lower(cgrp_name)) ENABLE
   , CONSTRAINT pk_caravan_sec_group PRIMARY KEY (cgrp_id, capp_id) USING INDEX TABLESPACE dati_base_index ENABLE
   , CONSTRAINT uk_caravan_sec_group UNIQUE (cgrp_name, capp_id) USING INDEX TABLESPACE dati_base_index ENABLE
   , CONSTRAINT fk_crvnsecgroup_crvnsecapp FOREIGN KEY (capp_id) REFERENCES mydb.caravan_sec_app (capp_id) ON DELETE CASCADE ENABLE
)
TABLESPACE dati_base;

INSERT INTO mydb.caravan_sec_group (cgrp_id, capp_id, cgrp_name, cgrp_description, cgrp_admin) VALUES (0, 0, 'admin', 'Admin Description', 1);
INSERT INTO mydb.caravan_sec_group (cgrp_id, capp_id, cgrp_name, cgrp_description, cgrp_admin) VALUES (1, 0, 'user', 'User Description', 0);