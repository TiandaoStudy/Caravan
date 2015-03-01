-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_entries
(
     csec_id          NUMBER          NOT NULL
   , cobj_id          NUMBER          NOT NULL
   , cusr_id          NUMBER          -- Might be null, either user, group or role
   , cgrp_id          NUMBER          -- Might be null, either user, group or role
   , crol_id          NUMBER          -- Might be null, either user, group or role
   , CHECK ((cusr_id IS NULL AND crol IS NULL AND cgrp_id IS NOT NULL) OR 
            (cusr_id IS NOT NULL AND crol IS NULL AND cgrp_id IS NULL) OR
			(cusr_id IS NULL AND crol IS NOT NULL AND cgrp_id IS NULL)) ENABLE
   , CONSTRAINT pk_crvn_sec_entries PRIMARY KEY (csec_id) ENABLE
   , CONSTRAINT fk_crvnsecurity_crvnsecapp FOREIGN KEY (capp_id) REFERENCES mydb.crvn_sec_apps (capp_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecurity_crvnsecusr FOREIGN KEY (cusr_id) REFERENCES mydb.crvn_sec_users (cusr_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecurity_crvnsecgrp FOREIGN KEY (cgrp_id) REFERENCES mydb.crvn_sec_groups (cgrp_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecurity_crvnsecobj FOREIGN KEY (cobj_id) REFERENCES mydb.crvn_sec_objects (cobj_id) ON DELETE CASCADE ENABLE
);