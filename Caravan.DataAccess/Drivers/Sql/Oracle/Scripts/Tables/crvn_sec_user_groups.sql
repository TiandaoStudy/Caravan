-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_user_groups
(
     cusr_id          NUMBER          NOT NULL
   , cgrp_id          NUMBER          NOT NULL
   , CONSTRAINT pk_crvn_sec_usrgrp PRIMARY KEY (cusr_id, cgrp_id) ENABLE
   , CONSTRAINT fk_crvnsecusrgrp_crvnsecusr FOREIGN KEY (cusr_id) REFERENCES mydb.crvn_sec_users (cusr_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecusrgrp_crvnsecgrp FOREIGN KEY (cgrp_id) REFERENCES mydb.crvn_sec_groups (cgrp_id) ON DELETE CASCADE ENABLE
);