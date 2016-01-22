-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_user_roles
(
     cusr_id          NUMBER(19)      NOT NULL
   , crol_id          NUMBER(10)      NOT NULL
   , CONSTRAINT pk_crvn_sec_usrrol PRIMARY KEY (cusr_id, crol_id) ENABLE
   , CONSTRAINT fk_crvnsecusrrol_crvnsecusr FOREIGN KEY (cusr_id) REFERENCES mydb.crvn_sec_users (cusr_id) ON DELETE CASCADE ENABLE
   , CONSTRAINT fk_crvnsecusrrol_crvnsecrol FOREIGN KEY (crol_id) REFERENCES mydb.crvn_sec_roles (crol_id) ON DELETE CASCADE ENABLE
);