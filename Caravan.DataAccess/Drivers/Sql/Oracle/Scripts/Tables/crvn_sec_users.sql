-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_users
(
     cusr_id          NUMBER          NOT NULL
   , capp_id          NUMBER          NOT NULL 
   , cusr_login       NVARCHAR2(32)   NOT NULL
   , cusr_hashed_pwd  NVARCHAR2(256)
   , cusr_active      NUMBER(1)       NOT NULL
   , cusr_first_name  NVARCHAR2(256)
   , cusr_last_name   NVARCHAR2(256)
   , cusr_email       NVARCHAR2(256)
   , CHECK (cusr_login = lower(cusr_login)) ENABLE
   , CONSTRAINT pk_crvn_sec_users PRIMARY KEY (cusr_id) ENABLE
   , CONSTRAINT uk_crvn_sec_users UNIQUE (capp_id, cusr_login) ENABLE
   , CONSTRAINT fk_crvnsecusers_crvnsecapps FOREIGN KEY (capp_id) REFERENCES mydb.crvn_sec_apps (capp_id) ON DELETE CASCADE ENABLE
);