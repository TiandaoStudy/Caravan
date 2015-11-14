-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_groups
(
     cgrp_id          NUMBER(10)      NOT NULL
   , capp_id          NUMBER(10)      NOT NULL 
   , cgrp_name        NVARCHAR2(32)   NOT NULL
   , cgrp_descr       NVARCHAR2(256)  NOT NULL
   , cgrp_notes       NVARCHAR2(1024) NOT NULL
   , CHECK (cgrp_name = lower(cgrp_name)) ENABLE
   , CONSTRAINT pk_crvn_sec_groups PRIMARY KEY (cgrp_id) ENABLE
   , CONSTRAINT uk_crvn_sec_groups UNIQUE (capp_id, cgrp_name) ENABLE
   , CONSTRAINT fk_crvnsecgroups_crvnsecapps FOREIGN KEY (capp_id) REFERENCES mydb.crvn_sec_apps (capp_id) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.crvn_sec_groups_id NOCACHE;