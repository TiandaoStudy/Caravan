-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_roles
(
     crol_id          NUMBER(10)      NOT NULL
   , cgrp_id          NUMBER(10)      NOT NULL 
   , crol_name        NVARCHAR2(32)   NOT NULL
   , crol_descr       NVARCHAR2(256)  NOT NULL
   , crol_notes       NVARCHAR2(1024) NOT NULL
   , CHECK (crol_name = lower(crol_name)) ENABLE
   , CONSTRAINT pk_crvn_sec_roles PRIMARY KEY (crol_id) ENABLE
   , CONSTRAINT uk_crvn_sec_roles UNIQUE (cgrp_id, crol_name) ENABLE
   , CONSTRAINT fk_crvnsecroles_crvnsecgrps FOREIGN KEY (cgrp_id) REFERENCES mydb.crvn_sec_groups (cgrp_id) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.crvn_sec_roles_id NOCACHE;