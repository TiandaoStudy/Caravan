-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE PACKAGE mydb.pck_flex_log AS

   procedure sp_log_debug         (p_application           in varchar2,
                                   p_user                  in varchar2,
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
                                   p_user                  in varchar2,
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

   procedure sp_log_warn          (p_application           in varchar2,
                                   p_user                  in varchar2,
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
                                   p_user                  in varchar2,
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
                                   p_user                  in varchar2,
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
                                   p_user                  in varchar2,
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
                                   p_user                  in varchar2,
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
      mydb.pck_flex_log.sp_log('debug', lower(p_application), lower(p_user), lower(p_code_unit), lower(p_function), p_short_msg, p_long_msg,
                               lower(p_key_0), p_value_0, lower(p_key_1), p_value_1, lower(p_key_2), p_value_2, lower(p_key_3), p_value_3, lower(p_key_4), p_value_4,
                               lower(p_key_5), p_value_5, lower(p_key_6), p_value_6, lower(p_key_7), p_value_7, lower(p_key_8), p_value_8, lower(p_key_9), p_value_9); 
   END sp_log_debug;


   --****************************************************************************************
   PROCEDURE sp_log_info          (p_application           in varchar2,
                                   p_user                  in varchar2,
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
      mydb.pck_flex_log.sp_log('info', lower(p_application), lower(p_user), lower(p_code_unit), lower(p_function), p_short_msg, p_long_msg,
                               lower(p_key_0), p_value_0, lower(p_key_1), p_value_1, lower(p_key_2), p_value_2, lower(p_key_3), p_value_3, lower(p_key_4), p_value_4,
                               lower(p_key_5), p_value_5, lower(p_key_6), p_value_6, lower(p_key_7), p_value_7, lower(p_key_8), p_value_8, lower(p_key_9), p_value_9); 
   END sp_log_info;


   --****************************************************************************************
   PROCEDURE sp_log_warn          (p_application           in varchar2,
                                   p_user                  in varchar2,
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
      mydb.pck_flex_log.sp_log('warn', lower(p_application), lower(p_user), lower(p_code_unit), lower(p_function), p_short_msg, p_long_msg,
                               lower(p_key_0), p_value_0, lower(p_key_1), p_value_1, lower(p_key_2), p_value_2, lower(p_key_3), p_value_3, lower(p_key_4), p_value_4,
                               lower(p_key_5), p_value_5, lower(p_key_6), p_value_6, lower(p_key_7), p_value_7, lower(p_key_8), p_value_8, lower(p_key_9), p_value_9); 
   END sp_log_warning;


   --****************************************************************************************
   PROCEDURE sp_log_error         (p_application           in varchar2,
                                   p_user                  in varchar2,
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
      mydb.pck_flex_log.sp_log('error', lower(p_application), lower(p_user), lower(p_code_unit), lower(p_function), p_short_msg, p_long_msg,
                               lower(p_key_0), p_value_0, lower(p_key_1), p_value_1, lower(p_key_2), p_value_2, lower(p_key_3), p_value_3, lower(p_key_4), p_value_4,
                               lower(p_key_5), p_value_5, lower(p_key_6), p_value_6, lower(p_key_7), p_value_7, lower(p_key_8), p_value_8, lower(p_key_9), p_value_9); 
   END sp_log_error;


   --****************************************************************************************
   PROCEDURE sp_log_fatal         (p_application           in varchar2,
                                   p_user                  in varchar2,
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
      mydb.pck_flex_log.sp_log('fatal', lower(p_application), lower(p_user), lower(p_code_unit), lower(p_function), p_short_msg, p_long_msg,
                               lower(p_key_0), p_value_0, lower(p_key_1), p_value_1, lower(p_key_2), p_value_2, lower(p_key_3), p_value_3, lower(p_key_4), p_value_4,
                               lower(p_key_5), p_value_5, lower(p_key_6), p_value_6, lower(p_key_7), p_value_7, lower(p_key_8), p_value_8, lower(p_key_9), p_value_9); 
   END sp_log_fatal;


   --****************************************************************************************
   PROCEDURE sp_log               (p_type                  in varchar2,
                                   p_application           in varchar2,
                                   p_user                  in varchar2,
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

      v_enabled     NUMBER(1);
      v_days        NUMBER(3);
      v_max_entries NUMBER(5);
      v_entry_count NUMBER(5);
   BEGIN
      
      SELECT flos_enabled, flos_days
        INTO v_enabled, v_days
        FROM mydb.flex_log_settings
       WHERE lower(flos_type) = lower(p_type)
         AND lower(flos_application) = lower(p_application);

      BEGIN
         -- We delete logs older than "flos_days"
         DELETE 
           FROM mydb.flex_log 
          WHERE flog_entry_date < SYSDATE - v_days
            AND lower(flos_type) = lower(p_type)
            AND lower(flos_application) = lower(p_application);
         
         -- We delete enough entries to preserve the upper limit
         SELECT COUNT(*) INTO v_entry_count
           FROM mydb.flex_log 
          WHERE lower(flos_type) = lower(p_type)
            AND lower(flos_application) = lower(p_application)
         
         IF v_entry_count >= v_max_entries THEN
            DELETE
              FROM mydb.flex_log
             WHERE lower(flos_type) = lower(p_type)
               AND lower(flos_application) = lower(p_application)
               AND flog_id IN (SELECT f.flog_id 
                                 FROM mydb.flex_log f 
                                WHERE lower(f.flos_type) = lower(p_type)
                                  AND lower(f.flos_application) = lower(p_application)
                                  AND rownum <= (v_entry_count - v_max_entries + 1)
                                ORDER BY flog_date DESC);
         END IF;
      EXCEPTION 
         WHEN OTHERS THEN NULL; -- Errors are ignored
      END;
      
      -- If log is enabled, then we can insert a new entry
      IF v_enabled = 1 THEN         
         INSERT INTO mydb.flex_log(flog_id, flog_entry_date, flos_type, flos_application, flog_user, flog_code_unit, flog_function, 
                                   flog_short_msg, flog_long_msg, flog_context, 
                                   flog_key_0, flog_value_0, flog_key_1, flog_value_1, flog_key_2, flog_value_2, flog_key_3, flog_value_3, flog_key_4, flog_value_4,
                                   flog_key_5, flog_value_5, flog_key_6, flog_value_6, flog_key_7, flog_value_7, flog_key_8, flog_value_8, flog_key_9, flog_value_9)
                           VALUES (mydb.flex_log_seq.nextval, sysdate, lower(p_type), lower(p_application), lower(p_user), lower(p_code_unit), lower(p_function), 
                                   p_short_msg, p_long_msg, p_context,
                                   lower(p_key_0), p_value_0, lower(p_key_1), p_value_1, lower(p_key_2), p_value_2, lower(p_key_3), p_value_3, lower(p_key_4), p_value_4,
                                   lower(p_key_5), p_value_5, lower(p_key_6), p_value_6, lower(p_key_7), p_value_7, lower(p_key_8), p_value_8, lower(p_key_9), p_value_9);         
      END IF;
      
      -- Leave at the end of procedure
      COMMIT;
   END sp_log;

END pck_flex_log;