-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_log_entries 
(  
     clog_id           NUMBER               NOT NULL
   , capp_id           NUMBER               NOT NULL
   , clos_type         NVARCHAR2(8)         NOT NULL
   , clog_date         DATE DEFAULT SYSDATE NOT NULL 
   , cusr_login        NVARCHAR2(32)
   , clog_code_unit    NVARCHAR2(256)
   , clog_function     NVARCHAR2(256)
   , clog_short_msg    NVARCHAR2(256)       NOT NULL
   , clog_context      NVARCHAR2(256)
   , clog_key_0        NVARCHAR2(32)
   , clog_value_0      NVARCHAR2(1024)
   , clog_key_1        NVARCHAR2(32) 
   , clog_value_1      NVARCHAR2(1024)
   , clog_key_2        NVARCHAR2(32) 
   , clog_value_2      NVARCHAR2(1024)
   , clog_key_3        NVARCHAR2(32) 
   , clog_value_3      NVARCHAR2(1024) 
   , clog_key_4        NVARCHAR2(32) 
   , clog_value_4      NVARCHAR2(1024) 
   , clog_key_5        NVARCHAR2(32) 
   , clog_value_5      NVARCHAR2(1024) 
   , clog_key_6        NVARCHAR2(32) 
   , clog_value_6      NVARCHAR2(1024) 
   , clog_key_7        NVARCHAR2(32) 
   , clog_value_7      NVARCHAR2(1024) 
   , clog_key_8        NVARCHAR2(32) 
   , clog_value_8      NVARCHAR2(1024) 
   , clog_key_9        NVARCHAR2(32)
   , clog_value_9      NVARCHAR2(1024)
   , clog_long_msg     CLOB
   , CHECK (cusr_login is null or cusr_login = lower(cusr_login)) ENABLE
   , CHECK (clog_code_unit = lower(clog_code_unit)) ENABLE
   , CHECK (clog_function = lower(clog_function)) ENABLE
   , CHECK (clog_key_0 is null or clog_key_0 = lower(clog_key_0)) ENABLE
   , CHECK (clog_key_1 is null or clog_key_1 = lower(clog_key_1)) ENABLE
   , CHECK (clog_key_2 is null or clog_key_2 = lower(clog_key_2)) ENABLE
   , CHECK (clog_key_3 is null or clog_key_3 = lower(clog_key_3)) ENABLE
   , CHECK (clog_key_4 is null or clog_key_4 = lower(clog_key_4)) ENABLE
   , CHECK (clog_key_5 is null or clog_key_5 = lower(clog_key_5)) ENABLE
   , CHECK (clog_key_6 is null or clog_key_6 = lower(clog_key_6)) ENABLE
   , CHECK (clog_key_7 is null or clog_key_7 = lower(clog_key_7)) ENABLE
   , CHECK (clog_key_8 is null or clog_key_8 = lower(clog_key_8)) ENABLE
   , CHECK (clog_key_9 is null or clog_key_9 = lower(clog_key_9)) ENABLE
   , CONSTRAINT pk_crvn_log_entries PRIMARY KEY (clog_id) ENABLE
   , CONSTRAINT fk_crvnlog_crvnlogsettings FOREIGN KEY (capp_id, clos_type) REFERENCES mydb.crvn_log_settings (capp_id, clos_type) ON DELETE CASCADE ENABLE
);

COMMENT ON TABLE mydb.crvn_log_entries IS 'Tabella di log per le applicazioni FINSA';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_id IS 'Identificativo riga, è una sequenza';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_date IS 'Data di inserimento della riga';
COMMENT ON COLUMN mydb.crvn_log_entries.clos_type IS 'Tipo di messaggio log, può assumere i valori debug, info, warn, error, fatal';
COMMENT ON COLUMN mydb.crvn_log_entries.capp_id IS 'Applicazione FINSA a cui si riferisce il log';
COMMENT ON COLUMN mydb.crvn_log_entries.cusr_login IS 'Utente loggato che ha attivato il logger';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_code_unit IS 'Package or fully qualified .NET type';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_function IS 'Metodo o procedura';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_short_msg IS 'Messaggio breve';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_long_msg IS 'Messaggio verboso';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_context IS 'Infomazione dettagliata del contesto in cui viene inserito il messaggio';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_key_0 IS 'Nome del parametro opzionale, ad esempio AZI_ID';
COMMENT ON COLUMN mydb.crvn_log_entries.clog_value_0 IS 'Valore del parametro opzionale, ad esempio RS'; 

CREATE INDEX mydb.ix_crvn_log_date ON mydb.crvn_log_entries (capp_id, clog_date DESC);
CREATE INDEX mydb.ix_crvn_log_type ON mydb.crvn_log_entries (capp_id, clos_type);

-- DROP da fare per la transizione da FLEX_LOG:
--> pck_flex_log
--> flex_log
--> flex_log_seq
--> flex_log_settings
