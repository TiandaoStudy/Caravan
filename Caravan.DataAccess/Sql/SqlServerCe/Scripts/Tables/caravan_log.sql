-- REPLACE 'dbo' WITH SCHEMA NAME

CREATE TABLE caravan_log
(  
     clog_id           BIGINT            NOT NULL
   , clog_date         DATETIME          NOT NULL DEFAULT getdate()
   , clos_type         NVARCHAR(5)       NOT NULL
   , capp_id           BIGINT            NOT NULL
   , clog_user         NVARCHAR(30)
   , clog_code_unit    NVARCHAR(100)     NOT NULL
   , clog_function     NVARCHAR(100)     NOT NULL
   , clog_short_msg    NVARCHAR(400)     NOT NULL
   , clog_long_msg     NVARCHAR(2000)
   , clog_context      NVARCHAR(400)
   , clog_key_0        NVARCHAR(100)
   , clog_value_0      NVARCHAR(400)
   , clog_key_1        NVARCHAR(100) 
   , clog_value_1      NVARCHAR(400)
   , clog_key_2        NVARCHAR(100) 
   , clog_value_2      NVARCHAR(400)
   , clog_key_3        NVARCHAR(100) 
   , clog_value_3      NVARCHAR(400) 
   , clog_key_4        NVARCHAR(100) 
   , clog_value_4      NVARCHAR(400) 
   , clog_key_5        NVARCHAR(100) 
   , clog_value_5      NVARCHAR(400) 
   , clog_key_6        NVARCHAR(100) 
   , clog_value_6      NVARCHAR(400) 
   , clog_key_7        NVARCHAR(100) 
   , clog_value_7      NVARCHAR(400) 
   , clog_key_8        NVARCHAR(100) 
   , clog_value_8      NVARCHAR(400) 
   , clog_key_9        NVARCHAR(100)
   , clog_value_9      NVARCHAR(400)   
   , CONSTRAINT pk_caravan_log PRIMARY KEY (clog_id)
   , CONSTRAINT fk_crvnlog_crvnlogsettings FOREIGN KEY (clos_type, capp_id) REFERENCES caravan_log_settings (clos_type, capp_id) ON DELETE CASCADE ON UPDATE CASCADE
);