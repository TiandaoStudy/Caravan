-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.caravan_log 
(  
     clog_id           NUMBER               NOT NULL
   , clog_date         DATE DEFAULT SYSDATE NOT NULL 
   , clos_type         NVARCHAR2(5)         NOT NULL
   , capp_name         NVARCHAR2(30)        NOT NULL
   , clog_user         NVARCHAR2(30)
   , clog_code_unit    NVARCHAR2(100)       NOT NULL
   , clog_function     NVARCHAR2(100)       NOT NULL
   , clog_short_msg    NVARCHAR2(400)       NOT NULL
   , clog_long_msg     NVARCHAR2(2000)
   , clog_context      NVARCHAR2(400)
   , clog_key_0        NVARCHAR2(100)
   , clog_value_0      NVARCHAR2(400)
   , clog_key_1        NVARCHAR2(100) 
   , clog_value_1      NVARCHAR2(400)
   , clog_key_2        NVARCHAR2(100) 
   , clog_value_2      NVARCHAR2(400)
   , clog_key_3        NVARCHAR2(100) 
   , clog_value_3      NVARCHAR2(400) 
   , clog_key_4        NVARCHAR2(100) 
   , clog_value_4      NVARCHAR2(400) 
   , clog_key_5        NVARCHAR2(100) 
   , clog_value_5      NVARCHAR2(400) 
   , clog_key_6        NVARCHAR2(100) 
   , clog_value_6      NVARCHAR2(400) 
   , clog_key_7        NVARCHAR2(100) 
   , clog_value_7      NVARCHAR2(400) 
   , clog_key_8        NVARCHAR2(100) 
   , clog_value_8      NVARCHAR2(400) 
   , clog_key_9        NVARCHAR2(100)
   , clog_value_9      NVARCHAR2(400)
   , CHECK (clog_user is null or clog_user = lower(clog_user)) ENABLE
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
   , CONSTRAINT pk_caravan_log PRIMARY KEY (clog_id) USING INDEX TABLESPACE dati_base_index ENABLE
   , CONSTRAINT fk_crvnlog_crvnlogsettings FOREIGN KEY (clos_type, capp_name) REFERENCES mydb.caravan_log_settings (clos_type, capp_name) ON DELETE CASCADE ENABLE
)
TABLESPACE dati_base;

COMMENT ON TABLE mydb.caravan_log IS 'Tabella di log per le applicazioni FINSA';
COMMENT ON COLUMN mydb.caravan_log.clog_id IS 'Identificativo riga, è una sequenza';
COMMENT ON COLUMN mydb.caravan_log.clog_date IS 'Data di inserimento della riga';
COMMENT ON COLUMN mydb.caravan_log.clos_type IS 'Tipo di messaggio log, può assumere i valori debug, info, warn, error, fatal';
COMMENT ON COLUMN mydb.caravan_log.capp_name IS 'Applicazione FINSA a cui si riferisce il log';
COMMENT ON COLUMN mydb.caravan_log.clog_user IS 'Utente loggato che ha attivato il logger';
COMMENT ON COLUMN mydb.caravan_log.clog_code_unit IS 'Package or fully qualified .NET type';
COMMENT ON COLUMN mydb.caravan_log.clog_function IS 'Metodo o procedura';
COMMENT ON COLUMN mydb.caravan_log.clog_short_msg IS 'Messaggio breve';
COMMENT ON COLUMN mydb.caravan_log.clog_long_msg IS 'Messaggio verboso';
COMMENT ON COLUMN mydb.caravan_log.clog_context IS 'Infomazione dettagliata del contesto in cui viene inserito il messaggio';
COMMENT ON COLUMN mydb.caravan_log.clog_key_0 IS 'Nome del parametro opzionale, ad esempio AZI_ID';
COMMENT ON COLUMN mydb.caravan_log.clog_value_0 IS 'Valore del parametro opzionale, ad esempio RS'; 

CREATE INDEX mydb.idx_caravan_log_date ON mydb.caravan_log (clog_date DESC);
CREATE INDEX mydb.idx_caravan_log_settings ON mydb.caravan_log (clos_type, capp_name);

-- DROP da fare per la transizione da FLEX_LOG:
--> pck_flex_log
--> flex_log
--> flex_log_seq
--> flex_log_settings
