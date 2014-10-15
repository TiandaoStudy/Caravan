-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.caravan_log 
(  
     clog_id           NUMBER               NOT NULL
   , clog_date         DATE DEFAULT SYSDATE NOT NULL 
   , clos_type         VARCHAR2(5 CHAR)     NOT NULL
   , clos_application  VARCHAR2(30 CHAR)    NOT NULL
   , clog_user         VARCHAR2(30 CHAR)
   , clog_code_unit    VARCHAR2(100 CHAR)   NOT NULL
   , clog_function     VARCHAR2(100 CHAR)   NOT NULL
   , clog_short_msg    VARCHAR2(400 CHAR)   NOT NULL
   , clog_long_msg     VARCHAR2(4000 CHAR)
   , clog_context      VARCHAR2(400 CHAR)
   , clog_key_0        VARCHAR2(100 CHAR)
   , clog_value_0      VARCHAR2(400 CHAR)
   , clog_key_1        VARCHAR2(100 CHAR) 
   , clog_value_1      VARCHAR2(400 CHAR)
   , clog_key_2        VARCHAR2(100 CHAR) 
   , clog_value_2      VARCHAR2(400 CHAR)
   , clog_key_3        VARCHAR2(100 CHAR) 
   , clog_value_3      VARCHAR2(400 CHAR) 
   , clog_key_4        VARCHAR2(100 CHAR) 
   , clog_value_4      VARCHAR2(400 CHAR) 
   , clog_key_5        VARCHAR2(100 CHAR) 
   , clog_value_5      VARCHAR2(400 CHAR) 
   , clog_key_6        VARCHAR2(100 CHAR) 
   , clog_value_6      VARCHAR2(400 CHAR) 
   , clog_key_7        VARCHAR2(100 CHAR) 
   , clog_value_7      VARCHAR2(400 CHAR) 
   , clog_key_8        VARCHAR2(100 CHAR) 
   , clog_value_8      VARCHAR2(400 CHAR) 
   , clog_key_9        VARCHAR2(100 CHAR)
   , clog_value_9      VARCHAR2(400 CHAR)
   , CONSTRAINT pk_caravan_log PRIMARY KEY (clog_id) USING INDEX TABLESPACE dati_base_index ENABLE
   , CONSTRAINT fk_crvnlog_crvnlogsettings FOREIGN KEY (clos_type, clos_application) REFERENCES mydb.caravan_log_settings (clos_type, clos_application) ENABLE
)
TABLESPACE dati_base;

COMMENT ON TABLE mydb.caravan_log IS 'Tabella di log per le applicazioni FINSA';
COMMENT ON COLUMN mydb.caravan_log.clog_id IS 'Identificativo riga, è una sequenza';
COMMENT ON COLUMN mydb.caravan_log.clog_date IS 'Data di inserimento della riga';
COMMENT ON COLUMN mydb.caravan_log.clos_type IS 'Tipo di messaggio log, può assumere i valori debug, info, warn, error, fatal';
COMMENT ON COLUMN mydb.caravan_log.clos_application IS 'Applicazione FINSA a cui si riferisce il log';
COMMENT ON COLUMN mydb.caravan_log.clog_user IS 'Utente loggato che ha attivato il logger';
COMMENT ON COLUMN mydb.caravan_log.clog_code_unit IS 'Package or fully qualified .NET type';
COMMENT ON COLUMN mydb.caravan_log.clog_function IS 'Metodo o procedura';
COMMENT ON COLUMN mydb.caravan_log.clog_short_msg IS 'Messaggio breve';
COMMENT ON COLUMN mydb.caravan_log.clog_long_msg IS 'Messaggio verboso';
COMMENT ON COLUMN mydb.caravan_log.clog_context IS 'Infomazione dettagliata del contesto in cui viene inserito il messaggio';
COMMENT ON COLUMN mydb.caravan_log.clog_key_0 IS 'Nome del parametro opzionale, ad esempio AZI_ID';
COMMENT ON COLUMN mydb.caravan_log.clog_value_0 IS 'Valore del parametro opzionale, ad esempio RS'; 

CREATE INDEX mydb.idx_caravan_log_date ON mydb.caravan_log (clog_date DESC);
CREATE INDEX mydb.idx_caravan_log_settings ON mydb.caravan_log (clos_type, clos_application);

-- DROP da fare per la transizione da FLEX_LOG:
--> pck_flex_log
--> flex_log
--> flex_log_seq
--> flex_log_settings
