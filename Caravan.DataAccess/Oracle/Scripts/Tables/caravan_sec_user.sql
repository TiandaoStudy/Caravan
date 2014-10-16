-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.caravan_sec_user
(
     cusr_id          NUMBER          NOT NULL
   , capp_id          NUMBER          NOT NULL 
   , cusr_active      NUMBER(1)       NOT NULL
   , cusr_login       NVARCHAR2(50)   NOT NULL
   , cusr_hashed_pwd  NVARCHAR2(150)
   , cusr_first_name  NVARCHAR2(50)
   , cusr_last_name   NVARCHAR2(100)
   , cusr_email       NVARCHAR2(100)
   , CHECK (cusr_login = lower(cusr_login)) ENABLE
   , CONSTRAINT pk_caravan_sec_user PRIMARY KEY (cusr_id, capp_id) USING INDEX TABLESPACE dati_base_index ENABLE
   , CONSTRAINT uk_caravan_sec_user UNIQUE (cusr_login, capp_id) USING INDEX TABLESPACE dati_base_index ENABLE
   , CONSTRAINT fk_crvnsecuser_crvnsecapp FOREIGN KEY (capp_id) REFERENCES mydb.caravan_sec_app (capp_id) ON DELETE CASCADE ENABLE
)
TABLESPACE dati_base;

INSERT INTO mydb.caravan_sec_user (cusr_id, capp_id, cusr_active, cusr_login, cusr_hashed_pwd, cusr_first_name, cusr_last_name, cusr_email) 
VALUES (0, 0, 1, 'pino', null, 'Pino', 'La Lavatrice', 'pino@lavatrice.la');