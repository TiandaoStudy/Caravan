-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE PACKAGE mydb.pck_flex_log AS

   procedure sp_log_debug         (p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_context               in varchar2 default null,
                                   p_key_0                 in varchar2 default null,
                                   p_value_0               in varchar2 default null,
                                   p_key_1                 in varchar2 default null,
                                   p_value_1               in varchar2 default null,
                                   p_key_2                 in varchar2 default null,
                                   p_value_2               in varchar2 default null,
                                   p_key_3                 in varchar2 default null,
                                   p_value_3               in varchar2 default null,
                                   p_key_4                 in varchar2 default null,
                                   p_value_4               in varchar2 default null,
                                   p_key_5                 in varchar2 default null,
                                   p_value_5               in varchar2 default null,
                                   p_key_6                 in varchar2 default null,
                                   p_value_6               in varchar2 default null,
                                   p_key_7                 in varchar2 default null,
                                   p_value_7               in varchar2 default null,
                                   p_key_8                 in varchar2 default null,
                                   p_value_8               in varchar2 default null,
                                   p_key_9                 in varchar2 default null,
                                   p_value_9               in varchar2 default null);

   procedure sp_log_info          (p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_context               in varchar2 default null,
                                   p_key_0                 in varchar2 default null,
                                   p_value_0               in varchar2 default null,
                                   p_key_1                 in varchar2 default null,
                                   p_value_1               in varchar2 default null,
                                   p_key_2                 in varchar2 default null,
                                   p_value_2               in varchar2 default null,
                                   p_key_3                 in varchar2 default null,
                                   p_value_3               in varchar2 default null,
                                   p_key_4                 in varchar2 default null,
                                   p_value_4               in varchar2 default null,
                                   p_key_5                 in varchar2 default null,
                                   p_value_5               in varchar2 default null,
                                   p_key_6                 in varchar2 default null,
                                   p_value_6               in varchar2 default null,
                                   p_key_7                 in varchar2 default null,
                                   p_value_7               in varchar2 default null,
                                   p_key_8                 in varchar2 default null,
                                   p_value_8               in varchar2 default null,
                                   p_key_9                 in varchar2 default null,
                                   p_value_9               in varchar2 default null);

   procedure sp_log_warning       (p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_context               in varchar2 default null,
                                   p_key_0                 in varchar2 default null,
                                   p_value_0               in varchar2 default null,
                                   p_key_1                 in varchar2 default null,
                                   p_value_1               in varchar2 default null,
                                   p_key_2                 in varchar2 default null,
                                   p_value_2               in varchar2 default null,
                                   p_key_3                 in varchar2 default null,
                                   p_value_3               in varchar2 default null,
                                   p_key_4                 in varchar2 default null,
                                   p_value_4               in varchar2 default null,
                                   p_key_5                 in varchar2 default null,
                                   p_value_5               in varchar2 default null,
                                   p_key_6                 in varchar2 default null,
                                   p_value_6               in varchar2 default null,
                                   p_key_7                 in varchar2 default null,
                                   p_value_7               in varchar2 default null,
                                   p_key_8                 in varchar2 default null,
                                   p_value_8               in varchar2 default null,
                                   p_key_9                 in varchar2 default null,
                                   p_value_9               in varchar2 default null);

   procedure sp_log_error         (p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_context               in varchar2 default null,
                                   p_key_0                 in varchar2 default null,
                                   p_value_0               in varchar2 default null,
                                   p_key_1                 in varchar2 default null,
                                   p_value_1               in varchar2 default null,
                                   p_key_2                 in varchar2 default null,
                                   p_value_2               in varchar2 default null,
                                   p_key_3                 in varchar2 default null,
                                   p_value_3               in varchar2 default null,
                                   p_key_4                 in varchar2 default null,
                                   p_value_4               in varchar2 default null,
                                   p_key_5                 in varchar2 default null,
                                   p_value_5               in varchar2 default null,
                                   p_key_6                 in varchar2 default null,
                                   p_value_6               in varchar2 default null,
                                   p_key_7                 in varchar2 default null,
                                   p_value_7               in varchar2 default null,
                                   p_key_8                 in varchar2 default null,
                                   p_value_8               in varchar2 default null,
                                   p_key_9                 in varchar2 default null,
                                   p_value_9               in varchar2 default null);

   procedure sp_log_fatal         (p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_context               in varchar2 default null,
                                   p_key_0                 in varchar2 default null,
                                   p_value_0               in varchar2 default null,
                                   p_key_1                 in varchar2 default null,
                                   p_value_1               in varchar2 default null,
                                   p_key_2                 in varchar2 default null,
                                   p_value_2               in varchar2 default null,
                                   p_key_3                 in varchar2 default null,
                                   p_value_3               in varchar2 default null,
                                   p_key_4                 in varchar2 default null,
                                   p_value_4               in varchar2 default null,
                                   p_key_5                 in varchar2 default null,
                                   p_value_5               in varchar2 default null,
                                   p_key_6                 in varchar2 default null,
                                   p_value_6               in varchar2 default null,
                                   p_key_7                 in varchar2 default null,
                                   p_value_7               in varchar2 default null,
                                   p_key_8                 in varchar2 default null,
                                   p_value_8               in varchar2 default null,
                                   p_key_9                 in varchar2 default null,
                                   p_value_9               in varchar2 default null);

   procedure sp_log               (p_type                  in varchar2,
                                   p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_context               in varchar2 default null,
                                   p_key_0                 in varchar2 default null,
                                   p_value_0               in varchar2 default null,
                                   p_key_1                 in varchar2 default null,
                                   p_value_1               in varchar2 default null,
                                   p_key_2                 in varchar2 default null,
                                   p_value_2               in varchar2 default null,
                                   p_key_3                 in varchar2 default null,
                                   p_value_3               in varchar2 default null,
                                   p_key_4                 in varchar2 default null,
                                   p_value_4               in varchar2 default null,
                                   p_key_5                 in varchar2 default null,
                                   p_value_5               in varchar2 default null,
                                   p_key_6                 in varchar2 default null,
                                   p_value_6               in varchar2 default null,
                                   p_key_7                 in varchar2 default null,
                                   p_value_7               in varchar2 default null,
                                   p_key_8                 in varchar2 default null,
                                   p_value_8               in varchar2 default null,
                                   p_key_9                 in varchar2 default null,
                                   p_value_9               in varchar2 default null);

end pck_flex_log;

/
CREATE OR REPLACE PACKAGE body mydb.pck_flex_log AS

   --****************************************************************************************
   PROCEDURE sp_log_debug         (p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_context               in varchar2 default null,
                                   p_key_0                 in varchar2 default null,
                                   p_value_0               in varchar2 default null,
                                   p_key_1                 in varchar2 default null,
                                   p_value_1               in varchar2 default null,
                                   p_key_2                 in varchar2 default null,
                                   p_value_2               in varchar2 default null,
                                   p_key_3                 in varchar2 default null,
                                   p_value_3               in varchar2 default null,
                                   p_key_4                 in varchar2 default null,
                                   p_value_4               in varchar2 default null,
                                   p_key_5                 in varchar2 default null,
                                   p_value_5               in varchar2 default null,
                                   p_key_6                 in varchar2 default null,
                                   p_value_6               in varchar2 default null,
                                   p_key_7                 in varchar2 default null,
                                   p_value_7               in varchar2 default null,
                                   p_key_8                 in varchar2 default null,
                                   p_value_8               in varchar2 default null,
                                   p_key_9                 in varchar2 default null,
                                   p_value_9               in varchar2 default null)
   AS
   BEGIN
      
      mydb.pck_flex_log.sp_log('DEBUG', UPPER(p_application), UPPER(p_code_unit), UPPER(p_function), p_short_msg, p_long_msg,
                                   p_key_0, p_value_0, p_key_1, p_value_1, p_key_2, p_value_2, p_key_3, p_value_3, p_key_4, p_value_4,
                                   p_key_5, p_value_5, p_key_6, p_value_6, p_key_7, p_value_7, p_key_8, p_value_8, p_key_9, p_value_9); 
   END sp_log_debug;


   --****************************************************************************************
   PROCEDURE sp_log_info          (p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_context               in varchar2 default null,
                                   p_key_0                 in varchar2 default null,
                                   p_value_0               in varchar2 default null,
                                   p_key_1                 in varchar2 default null,
                                   p_value_1               in varchar2 default null,
                                   p_key_2                 in varchar2 default null,
                                   p_value_2               in varchar2 default null,
                                   p_key_3                 in varchar2 default null,
                                   p_value_3               in varchar2 default null,
                                   p_key_4                 in varchar2 default null,
                                   p_value_4               in varchar2 default null,
                                   p_key_5                 in varchar2 default null,
                                   p_value_5               in varchar2 default null,
                                   p_key_6                 in varchar2 default null,
                                   p_value_6               in varchar2 default null,
                                   p_key_7                 in varchar2 default null,
                                   p_value_7               in varchar2 default null,
                                   p_key_8                 in varchar2 default null,
                                   p_value_8               in varchar2 default null,
                                   p_key_9                 in varchar2 default null,
                                   p_value_9               in varchar2 default null)
   AS
   BEGIN
      
      mydb.pck_flex_log.sp_log('INFO', UPPER(p_application), UPPER(p_code_unit), UPPER(p_function), p_short_msg, p_long_msg,
                                   p_key_0, p_value_0, p_key_1, p_value_1, p_key_2, p_value_2, p_key_3, p_value_3, p_key_4, p_value_4,
                                   p_key_5, p_value_5, p_key_6, p_value_6, p_key_7, p_value_7, p_key_8, p_value_8, p_key_9, p_value_9); 

   END sp_log_info;


   --****************************************************************************************
   PROCEDURE sp_log_warning       (p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_context               in varchar2 default null,
                                   p_key_0                 in varchar2 default null,
                                   p_value_0               in varchar2 default null,
                                   p_key_1                 in varchar2 default null,
                                   p_value_1               in varchar2 default null,
                                   p_key_2                 in varchar2 default null,
                                   p_value_2               in varchar2 default null,
                                   p_key_3                 in varchar2 default null,
                                   p_value_3               in varchar2 default null,
                                   p_key_4                 in varchar2 default null,
                                   p_value_4               in varchar2 default null,
                                   p_key_5                 in varchar2 default null,
                                   p_value_5               in varchar2 default null,
                                   p_key_6                 in varchar2 default null,
                                   p_value_6               in varchar2 default null,
                                   p_key_7                 in varchar2 default null,
                                   p_value_7               in varchar2 default null,
                                   p_key_8                 in varchar2 default null,
                                   p_value_8               in varchar2 default null,
                                   p_key_9                 in varchar2 default null,
                                   p_value_9               in varchar2 default null)
   AS
   BEGIN
      
      mydb.pck_flex_log.sp_log('WARNING', UPPER(P_APPLICATION), UPPER(P_CODE_UNIT), UPPER(P_FUNCTION), P_SHORT_MSG, P_LONG_MSG,
                                   p_key_0, p_value_0, p_key_1, p_value_1, p_key_2, p_value_2, p_key_3, p_value_3, p_key_4, p_value_4,
                                   p_key_5, p_value_5, p_key_6, p_value_6, p_key_7, p_value_7, p_key_8, p_value_8, p_key_9, p_value_9); 

   END sp_log_warning;


   --****************************************************************************************
   PROCEDURE sp_log_error         (p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_context               in varchar2 default null,
                                   p_key_0                 in varchar2 default null,
                                   p_value_0               in varchar2 default null,
                                   p_key_1                 in varchar2 default null,
                                   p_value_1               in varchar2 default null,
                                   p_key_2                 in varchar2 default null,
                                   p_value_2               in varchar2 default null,
                                   p_key_3                 in varchar2 default null,
                                   p_value_3               in varchar2 default null,
                                   p_key_4                 in varchar2 default null,
                                   p_value_4               in varchar2 default null,
                                   p_key_5                 in varchar2 default null,
                                   p_value_5               in varchar2 default null,
                                   p_key_6                 in varchar2 default null,
                                   p_value_6               in varchar2 default null,
                                   p_key_7                 in varchar2 default null,
                                   p_value_7               in varchar2 default null,
                                   p_key_8                 in varchar2 default null,
                                   p_value_8               in varchar2 default null,
                                   p_key_9                 in varchar2 default null,
                                   p_value_9               in varchar2 default null)
   AS
   BEGIN
      
      mydb.pck_flex_log.sp_log('ERROR', UPPER(p_application), UPPER(p_code_unit), UPPER(p_function), p_short_msg, p_long_msg,
                                   p_key_0, p_value_0, p_key_1, p_value_1, p_key_2, p_value_2, p_key_3, p_value_3, p_key_4, p_value_4,
                                   p_key_5, p_value_5, p_key_6, p_value_6, p_key_7, p_value_7, p_key_8, p_value_8, p_key_9, p_value_9); 

   END sp_log_error;


   --****************************************************************************************
   PROCEDURE sp_log_fatal         (p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_context               in varchar2 default null,
                                   p_key_0                 in varchar2 default null,
                                   p_value_0               in varchar2 default null,
                                   p_key_1                 in varchar2 default null,
                                   p_value_1               in varchar2 default null,
                                   p_key_2                 in varchar2 default null,
                                   p_value_2               in varchar2 default null,
                                   p_key_3                 in varchar2 default null,
                                   p_value_3               in varchar2 default null,
                                   p_key_4                 in varchar2 default null,
                                   p_value_4               in varchar2 default null,
                                   p_key_5                 in varchar2 default null,
                                   p_value_5               in varchar2 default null,
                                   p_key_6                 in varchar2 default null,
                                   p_value_6               in varchar2 default null,
                                   p_key_7                 in varchar2 default null,
                                   p_value_7               in varchar2 default null,
                                   p_key_8                 in varchar2 default null,
                                   p_value_8               in varchar2 default null,
                                   p_key_9                 in varchar2 default null,
                                   p_value_9               in varchar2 default null)
   AS
   BEGIN
      
      mydb.pck_flex_log.sp_log('FATAL', UPPER(p_application), UPPER(p_code_unit), UPPER(p_function), p_short_msg, p_long_msg,
                                  p_key_0, p_value_0, p_key_1, p_value_1, p_key_2, p_value_2, p_key_3, p_value_3, p_key_4, p_value_4,
                                  p_key_5, p_value_5, p_key_6, p_value_6, p_key_7, p_value_7, p_key_8, p_value_8, p_key_9, p_value_9); 

   END sp_log_fatal;


   --****************************************************************************************
   PROCEDURE sp_log               (p_type                  in varchar2,
                                   p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_context               in varchar2 default null,
                                   p_key_0                 in varchar2 default null,
                                   p_value_0               in varchar2 default null,
                                   p_key_1                 in varchar2 default null,
                                   p_value_1               in varchar2 default null,
                                   p_key_2                 in varchar2 default null,
                                   p_value_2               in varchar2 default null,
                                   p_key_3                 in varchar2 default null,
                                   p_value_3               in varchar2 default null,
                                   p_key_4                 in varchar2 default null,
                                   p_value_4               in varchar2 default null,
                                   p_key_5                 in varchar2 default null,
                                   p_value_5               in varchar2 default null,
                                   p_key_6                 in varchar2 default null,
                                   p_value_6               in varchar2 default null,
                                   p_key_7                 in varchar2 default null,
                                   p_value_7               in varchar2 default null,
                                   p_key_8                 in varchar2 default null,
                                   p_value_8               in varchar2 default null,
                                   p_key_9                 in varchar2 default null,
                                   p_value_9               in varchar2 default null)
   AS
      PRAGMA AUTONOMOUS_TRANSACTION;

      v_enabled    NUMBER(1);
      v_days       NUMBER(3);
  
   BEGIN
      
      SELECT flos_enabled, flos_days
        INTO v_enabled, v_days
        FROM mydb.flex_log_settings
       WHERE flos_type = p_type;

      -- We delete logs older than 3 months
      BEGIN
         DELETE 
           FROM mydb.flex_log 
          WHERE flog_entry_date < SYSDATE - v_days;
      
      EXCEPTION 
         WHEN OTHERS THEN NULL; -- Errors are ignored
      END;
      
      IF v_enabled = 1 THEN
         
         INSERT INTO mydb.flex_log(flog_id, flog_entry_date, flos_type, flog_application, flog_code_unit, flog_function, flog_short_msg, flog_long_msg, flog_context, 
                                       flog_key_0, flog_value_0, flog_key_1, flog_value_1, flog_key_2, flog_value_2, flog_key_3, flog_value_3, flog_key_4, flog_value_4,
                                       flog_key_5, flog_value_5, flog_key_6, flog_value_6, flog_key_7, flog_value_7, flog_key_8, flog_value_8, flog_key_9, flog_value_9)
                               VALUES (mydb.flex_log_seq.nextval, sysdate, UPPER(p_type), UPPER(p_application), p_code_unit, p_function, p_short_msg, p_long_msg, p_context,
                                       p_key_0, p_value_0, p_key_1, p_value_1, p_key_2, p_value_2, p_key_3, p_value_3, p_key_4, p_value_4,
                                       p_key_5, p_value_5, p_key_6, p_value_6, p_key_7, p_value_7, p_key_8, p_value_8, p_key_9, p_value_9); 
         COMMIT;
      END IF;
   
   END sp_log;

END pck_flex_log;