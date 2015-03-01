-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_groups
(
     cgrp_id          NUMBER          NOT NULL
   , capp_id          NUMBER          NOT NULL 
   , cgrp_name        NVARCHAR2(32)   NOT NULL
   , cgrp_descr       NVARCHAR2(256)  NOT NULL
   , cgrp_notes       NVARCHAR2(1024) NOT NULL
   , CHECK (cgrp_name = lower(cgrp_name)) ENABLE
   , CONSTRAINT pk_caravan_sec_group PRIMARY KEY (cgrp_id) ENABLE
   , CONSTRAINT uk_caravan_sec_group UNIQUE (cgrp_name, capp_id) ENABLE
   , CONSTRAINT fk_crvnsecgroup_crvnsecapp FOREIGN KEY (capp_id) REFERENCES mydb.caravan_sec_app (capp_id) ON DELETE CASCADE ENABLE
);