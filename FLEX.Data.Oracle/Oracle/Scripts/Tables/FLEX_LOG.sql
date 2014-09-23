﻿-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.flex_log 
   (  flog_id           NUMBER         NOT NULL
    , flog_entry_date   DATE DEFAULT SYSDATE NOT NULL 
    , flos_type         VARCHAR2(10)   NOT NULL
    , flog_application  VARCHAR2(30)   NOT NULL
    , flog_code_unit    VARCHAR2(100)  NOT NULL
    , flog_function     VARCHAR2(100)  NOT NULL
    , flog_short_msg    VARCHAR2(400)  NOT NULL
    , flog_long_msg     VARCHAR2(4000)
    , flog_context      VARCHAR2(400)
    , flog_key_0        VARCHAR2(100)
    , flog_value_0      VARCHAR2(400)
    , flog_key_1        VARCHAR2(100) 
    , flog_value_1      VARCHAR2(400)
    , flog_key_2        VARCHAR2(100) 
    , flog_value_2      VARCHAR2(400)
    , flog_key_3        VARCHAR2(100) 
    , flog_value_3      VARCHAR2(400) 
    , flog_key_4        VARCHAR2(100) 
    , flog_value_4      VARCHAR2(400) 
    , flog_key_5        VARCHAR2(100) 
    , flog_value_5      VARCHAR2(400) 
    , flog_key_6        VARCHAR2(100) 
    , flog_value_6      VARCHAR2(400) 
    , flog_key_7        VARCHAR2(100) 
    , flog_value_7      VARCHAR2(400) 
    , flog_key_8        VARCHAR2(100) 
    , flog_value_8      VARCHAR2(400) 
    , flog_key_9        VARCHAR2(100)
    , flog_value_9      VARCHAR2(400)
    , CONSTRAINT pk_flex_log PRIMARY KEY (flog_id) USING INDEX TABLESPACE dati_base_index ENABLE
    , CONSTRAINT fk_flexlog_flexlogsettings FOREIGN KEY (flos_type) REFERENCES mydb.flex_log_settings (flos_type) ENABLE
   )
   TABLESPACE dati_base;
 

   COMMENT ON TABLE mydb.flex_log IS 'Tabella di log per le applicazioni FINSA';
   COMMENT ON COLUMN mydb.flex_log.flog_id IS 'Isentificativo riga, è una sequenza';
   COMMENT ON COLUMN mydb.flex_log.flog_entry_date IS 'Data di inserimento della riga';
   COMMENT ON COLUMN mydb.flex_log.flos_type IS 'Tipo di messaggio log, pò assumere i valori DEBUG, INFO, WARNING, ERROR, FATAL';
   COMMENT ON COLUMN mydb.flex_log.flog_application IS 'Applicazione FINSA a cui si riferisce il log';
   COMMENT ON COLUMN mydb.flex_log.flog_code_unit IS 'Package or fully qualified .NET type';
   COMMENT ON COLUMN mydb.flex_log.flog_function IS 'Metodo o procedura';
   COMMENT ON COLUMN mydb.flex_log.flog_short_msg IS 'Messaggio berve';
   COMMENT ON COLUMN mydb.flex_log.flog_long_msg IS 'Messaggio verboso';
   COMMENT ON COLUMN mydb.flex_log.flog_context IS 'Infomazione dettagliata del contesto in cui viene inserito il messaggio';
   COMMENT ON COLUMN mydb.flex_log.flog_key_0 IS 'Nome del parametro opzionale, ad esempio AZI_ID';
   COMMENT ON COLUMN mydb.flex_log.flog_value_0 IS 'Valore del parametro opzionale, ad esempio RS';
   

   CREATE INDEX mydb.idx_flex_log_date ON mydb.flex_log (flog_entry_date DESC);
   CREATE INDEX mydb.idx_flex_los_type ON mydb.flex_log (flos_type) ;