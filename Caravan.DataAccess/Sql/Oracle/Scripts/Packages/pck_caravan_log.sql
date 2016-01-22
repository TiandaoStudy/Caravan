-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE PACKAGE mydb.pck_caravan_log AS

   procedure sp_log_debug         (p_app_id                in number,
                                   p_user                  in nvarchar2,
                                   p_code_unit             in nvarchar2,
                                   p_function              in nvarchar2,
                                   p_short_msg             in nvarchar2,
                                   p_long_msg              in nvarchar2,
                                   p_context               in nvarchar2 default null,
                                   p_key_0                 in nvarchar2 default null,
                                   p_value_0               in nvarchar2 default null,
                                   p_key_1                 in nvarchar2 default null,
                                   p_value_1               in nvarchar2 default null,
                                   p_key_2                 in nvarchar2 default null,
                                   p_value_2               in nvarchar2 default null,
                                   p_key_3                 in nvarchar2 default null,
                                   p_value_3               in nvarchar2 default null,
                                   p_key_4                 in nvarchar2 default null,
                                   p_value_4               in nvarchar2 default null,
                                   p_key_5                 in nvarchar2 default null,
                                   p_value_5               in nvarchar2 default null,
                                   p_key_6                 in nvarchar2 default null,
                                   p_value_6               in nvarchar2 default null,
                                   p_key_7                 in nvarchar2 default null,
                                   p_value_7               in nvarchar2 default null,
                                   p_key_8                 in nvarchar2 default null,
                                   p_value_8               in nvarchar2 default null,
                                   p_key_9                 in nvarchar2 default null,
                                   p_value_9               in nvarchar2 default null);

   procedure sp_log_info          (p_app_id                in number,
                                   p_user                  in nvarchar2,
                                   p_code_unit             in nvarchar2,
                                   p_function              in nvarchar2,
                                   p_short_msg             in nvarchar2,
                                   p_long_msg              in nvarchar2,
                                   p_context               in nvarchar2 default null,
                                   p_key_0                 in nvarchar2 default null,
                                   p_value_0               in nvarchar2 default null,
                                   p_key_1                 in nvarchar2 default null,
                                   p_value_1               in nvarchar2 default null,
                                   p_key_2                 in nvarchar2 default null,
                                   p_value_2               in nvarchar2 default null,
                                   p_key_3                 in nvarchar2 default null,
                                   p_value_3               in nvarchar2 default null,
                                   p_key_4                 in nvarchar2 default null,
                                   p_value_4               in nvarchar2 default null,
                                   p_key_5                 in nvarchar2 default null,
                                   p_value_5               in nvarchar2 default null,
                                   p_key_6                 in nvarchar2 default null,
                                   p_value_6               in nvarchar2 default null,
                                   p_key_7                 in nvarchar2 default null,
                                   p_value_7               in nvarchar2 default null,
                                   p_key_8                 in nvarchar2 default null,
                                   p_value_8               in nvarchar2 default null,
                                   p_key_9                 in nvarchar2 default null,
                                   p_value_9               in nvarchar2 default null);

   procedure sp_log_warn          (p_app_id                in number,
                                   p_user                  in nvarchar2,
                                   p_code_unit             in nvarchar2,
                                   p_function              in nvarchar2,
                                   p_short_msg             in nvarchar2,
                                   p_long_msg              in nvarchar2,
                                   p_context               in nvarchar2 default null,
                                   p_key_0                 in nvarchar2 default null,
                                   p_value_0               in nvarchar2 default null,
                                   p_key_1                 in nvarchar2 default null,
                                   p_value_1               in nvarchar2 default null,
                                   p_key_2                 in nvarchar2 default null,
                                   p_value_2               in nvarchar2 default null,
                                   p_key_3                 in nvarchar2 default null,
                                   p_value_3               in nvarchar2 default null,
                                   p_key_4                 in nvarchar2 default null,
                                   p_value_4               in nvarchar2 default null,
                                   p_key_5                 in nvarchar2 default null,
                                   p_value_5               in nvarchar2 default null,
                                   p_key_6                 in nvarchar2 default null,
                                   p_value_6               in nvarchar2 default null,
                                   p_key_7                 in nvarchar2 default null,
                                   p_value_7               in nvarchar2 default null,
                                   p_key_8                 in nvarchar2 default null,
                                   p_value_8               in nvarchar2 default null,
                                   p_key_9                 in nvarchar2 default null,
                                   p_value_9               in nvarchar2 default null);

   procedure sp_log_error         (p_app_id                in number,
                                   p_user                  in nvarchar2,
                                   p_code_unit             in nvarchar2,
                                   p_function              in nvarchar2,
                                   p_short_msg             in nvarchar2,
                                   p_long_msg              in nvarchar2,
                                   p_context               in nvarchar2 default null,
                                   p_key_0                 in nvarchar2 default null,
                                   p_value_0               in nvarchar2 default null,
                                   p_key_1                 in nvarchar2 default null,
                                   p_value_1               in nvarchar2 default null,
                                   p_key_2                 in nvarchar2 default null,
                                   p_value_2               in nvarchar2 default null,
                                   p_key_3                 in nvarchar2 default null,
                                   p_value_3               in nvarchar2 default null,
                                   p_key_4                 in nvarchar2 default null,
                                   p_value_4               in nvarchar2 default null,
                                   p_key_5                 in nvarchar2 default null,
                                   p_value_5               in nvarchar2 default null,
                                   p_key_6                 in nvarchar2 default null,
                                   p_value_6               in nvarchar2 default null,
                                   p_key_7                 in nvarchar2 default null,
                                   p_value_7               in nvarchar2 default null,
                                   p_key_8                 in nvarchar2 default null,
                                   p_value_8               in nvarchar2 default null,
                                   p_key_9                 in nvarchar2 default null,
                                   p_value_9               in nvarchar2 default null);

   procedure sp_log_fatal         (p_app_id                in number,
                                   p_user                  in nvarchar2,
                                   p_code_unit             in nvarchar2,
                                   p_function              in nvarchar2,
                                   p_short_msg             in nvarchar2,
                                   p_long_msg              in nvarchar2,
                                   p_context               in nvarchar2 default null,
                                   p_key_0                 in nvarchar2 default null,
                                   p_value_0               in nvarchar2 default null,
                                   p_key_1                 in nvarchar2 default null,
                                   p_value_1               in nvarchar2 default null,
                                   p_key_2                 in nvarchar2 default null,
                                   p_value_2               in nvarchar2 default null,
                                   p_key_3                 in nvarchar2 default null,
                                   p_value_3               in nvarchar2 default null,
                                   p_key_4                 in nvarchar2 default null,
                                   p_value_4               in nvarchar2 default null,
                                   p_key_5                 in nvarchar2 default null,
                                   p_value_5               in nvarchar2 default null,
                                   p_key_6                 in nvarchar2 default null,
                                   p_value_6               in nvarchar2 default null,
                                   p_key_7                 in nvarchar2 default null,
                                   p_value_7               in nvarchar2 default null,
                                   p_key_8                 in nvarchar2 default null,
                                   p_value_8               in nvarchar2 default null,
                                   p_key_9                 in nvarchar2 default null,
                                   p_value_9               in nvarchar2 default null);

   procedure sp_log               (p_type                  in nvarchar2,
                                   p_app_id                in number,
                                   p_user                  in nvarchar2,
                                   p_code_unit             in nvarchar2,
                                   p_function              in nvarchar2,
                                   p_short_msg             in nvarchar2,
                                   p_long_msg              in nvarchar2,
                                   p_context               in nvarchar2 default null,
                                   p_key_0                 in nvarchar2 default null,
                                   p_value_0               in nvarchar2 default null,
                                   p_key_1                 in nvarchar2 default null,
                                   p_value_1               in nvarchar2 default null,
                                   p_key_2                 in nvarchar2 default null,
                                   p_value_2               in nvarchar2 default null,
                                   p_key_3                 in nvarchar2 default null,
                                   p_value_3               in nvarchar2 default null,
                                   p_key_4                 in nvarchar2 default null,
                                   p_value_4               in nvarchar2 default null,
                                   p_key_5                 in nvarchar2 default null,
                                   p_value_5               in nvarchar2 default null,
                                   p_key_6                 in nvarchar2 default null,
                                   p_value_6               in nvarchar2 default null,
                                   p_key_7                 in nvarchar2 default null,
                                   p_value_7               in nvarchar2 default null,
                                   p_key_8                 in nvarchar2 default null,
                                   p_value_8               in nvarchar2 default null,
                                   p_key_9                 in nvarchar2 default null,
                                   p_value_9               in nvarchar2 default null);

end pck_caravan_log;

/

create or replace PACKAGE body          mydb.pck_caravan_log AS

   --****************************************************************************************
   PROCEDURE sp_log_debug         (p_app_id                in number,
                                   p_user                  in nvarchar2,
                                   p_code_unit             in nvarchar2,
                                   p_function              in nvarchar2,
                                   p_short_msg             in nvarchar2,
                                   p_long_msg              in nvarchar2,
                                   p_context               in nvarchar2 default null,
                                   p_key_0                 in nvarchar2 default null,
                                   p_value_0               in nvarchar2 default null,
                                   p_key_1                 in nvarchar2 default null,
                                   p_value_1               in nvarchar2 default null,
                                   p_key_2                 in nvarchar2 default null,
                                   p_value_2               in nvarchar2 default null,
                                   p_key_3                 in nvarchar2 default null,
                                   p_value_3               in nvarchar2 default null,
                                   p_key_4                 in nvarchar2 default null,
                                   p_value_4               in nvarchar2 default null,
                                   p_key_5                 in nvarchar2 default null,
                                   p_value_5               in nvarchar2 default null,
                                   p_key_6                 in nvarchar2 default null,
                                   p_value_6               in nvarchar2 default null,
                                   p_key_7                 in nvarchar2 default null,
                                   p_value_7               in nvarchar2 default null,
                                   p_key_8                 in nvarchar2 default null,
                                   p_value_8               in nvarchar2 default null,
                                   p_key_9                 in nvarchar2 default null,
                                   p_value_9               in nvarchar2 default null)
   AS
   BEGIN
      mydb.pck_caravan_log.sp_log('debug', lower(p_app_id), lower(p_user), lower(p_code_unit), lower(p_function), p_short_msg, p_long_msg,
                               lower(p_key_0), p_value_0, lower(p_key_1), p_value_1, lower(p_key_2), p_value_2, lower(p_key_3), p_value_3, lower(p_key_4), p_value_4,
                               lower(p_key_5), p_value_5, lower(p_key_6), p_value_6, lower(p_key_7), p_value_7, lower(p_key_8), p_value_8, lower(p_key_9), p_value_9); 
   END sp_log_debug;


   --****************************************************************************************
   PROCEDURE sp_log_info          (p_app_id                in number,
                                   p_user                  in nvarchar2,
                                   p_code_unit             in nvarchar2,
                                   p_function              in nvarchar2,
                                   p_short_msg             in nvarchar2,
                                   p_long_msg              in nvarchar2,
                                   p_context               in nvarchar2 default null,
                                   p_key_0                 in nvarchar2 default null,
                                   p_value_0               in nvarchar2 default null,
                                   p_key_1                 in nvarchar2 default null,
                                   p_value_1               in nvarchar2 default null,
                                   p_key_2                 in nvarchar2 default null,
                                   p_value_2               in nvarchar2 default null,
                                   p_key_3                 in nvarchar2 default null,
                                   p_value_3               in nvarchar2 default null,
                                   p_key_4                 in nvarchar2 default null,
                                   p_value_4               in nvarchar2 default null,
                                   p_key_5                 in nvarchar2 default null,
                                   p_value_5               in nvarchar2 default null,
                                   p_key_6                 in nvarchar2 default null,
                                   p_value_6               in nvarchar2 default null,
                                   p_key_7                 in nvarchar2 default null,
                                   p_value_7               in nvarchar2 default null,
                                   p_key_8                 in nvarchar2 default null,
                                   p_value_8               in nvarchar2 default null,
                                   p_key_9                 in nvarchar2 default null,
                                   p_value_9               in nvarchar2 default null)
   AS
   BEGIN
      mydb.pck_caravan_log.sp_log('info', lower(p_app_id), lower(p_user), lower(p_code_unit), lower(p_function), p_short_msg, p_long_msg,
                               lower(p_key_0), p_value_0, lower(p_key_1), p_value_1, lower(p_key_2), p_value_2, lower(p_key_3), p_value_3, lower(p_key_4), p_value_4,
                               lower(p_key_5), p_value_5, lower(p_key_6), p_value_6, lower(p_key_7), p_value_7, lower(p_key_8), p_value_8, lower(p_key_9), p_value_9); 
   END sp_log_info;


   --****************************************************************************************
   PROCEDURE sp_log_warn          (p_app_id                in number,
                                   p_user                  in nvarchar2,
                                   p_code_unit             in nvarchar2,
                                   p_function              in nvarchar2,
                                   p_short_msg             in nvarchar2,
                                   p_long_msg              in nvarchar2,
                                   p_context               in nvarchar2 default null,
                                   p_key_0                 in nvarchar2 default null,
                                   p_value_0               in nvarchar2 default null,
                                   p_key_1                 in nvarchar2 default null,
                                   p_value_1               in nvarchar2 default null,
                                   p_key_2                 in nvarchar2 default null,
                                   p_value_2               in nvarchar2 default null,
                                   p_key_3                 in nvarchar2 default null,
                                   p_value_3               in nvarchar2 default null,
                                   p_key_4                 in nvarchar2 default null,
                                   p_value_4               in nvarchar2 default null,
                                   p_key_5                 in nvarchar2 default null,
                                   p_value_5               in nvarchar2 default null,
                                   p_key_6                 in nvarchar2 default null,
                                   p_value_6               in nvarchar2 default null,
                                   p_key_7                 in nvarchar2 default null,
                                   p_value_7               in nvarchar2 default null,
                                   p_key_8                 in nvarchar2 default null,
                                   p_value_8               in nvarchar2 default null,
                                   p_key_9                 in nvarchar2 default null,
                                   p_value_9               in nvarchar2 default null)
   AS
   BEGIN
      mydb.pck_caravan_log.sp_log('warn', lower(p_app_id), lower(p_user), lower(p_code_unit), lower(p_function), p_short_msg, p_long_msg,
                               lower(p_key_0), p_value_0, lower(p_key_1), p_value_1, lower(p_key_2), p_value_2, lower(p_key_3), p_value_3, lower(p_key_4), p_value_4,
                               lower(p_key_5), p_value_5, lower(p_key_6), p_value_6, lower(p_key_7), p_value_7, lower(p_key_8), p_value_8, lower(p_key_9), p_value_9); 
   END sp_log_warn;


   --****************************************************************************************
   PROCEDURE sp_log_error         (p_app_id                in number,
                                   p_user                  in nvarchar2,
                                   p_code_unit             in nvarchar2,
                                   p_function              in nvarchar2,
                                   p_short_msg             in nvarchar2,
                                   p_long_msg              in nvarchar2,
                                   p_context               in nvarchar2 default null,
                                   p_key_0                 in nvarchar2 default null,
                                   p_value_0               in nvarchar2 default null,
                                   p_key_1                 in nvarchar2 default null,
                                   p_value_1               in nvarchar2 default null,
                                   p_key_2                 in nvarchar2 default null,
                                   p_value_2               in nvarchar2 default null,
                                   p_key_3                 in nvarchar2 default null,
                                   p_value_3               in nvarchar2 default null,
                                   p_key_4                 in nvarchar2 default null,
                                   p_value_4               in nvarchar2 default null,
                                   p_key_5                 in nvarchar2 default null,
                                   p_value_5               in nvarchar2 default null,
                                   p_key_6                 in nvarchar2 default null,
                                   p_value_6               in nvarchar2 default null,
                                   p_key_7                 in nvarchar2 default null,
                                   p_value_7               in nvarchar2 default null,
                                   p_key_8                 in nvarchar2 default null,
                                   p_value_8               in nvarchar2 default null,
                                   p_key_9                 in nvarchar2 default null,
                                   p_value_9               in nvarchar2 default null)
   AS
   BEGIN
      mydb.pck_caravan_log.sp_log('error', lower(p_app_id), lower(p_user), lower(p_code_unit), lower(p_function), p_short_msg, p_long_msg,
                               lower(p_key_0), p_value_0, lower(p_key_1), p_value_1, lower(p_key_2), p_value_2, lower(p_key_3), p_value_3, lower(p_key_4), p_value_4,
                               lower(p_key_5), p_value_5, lower(p_key_6), p_value_6, lower(p_key_7), p_value_7, lower(p_key_8), p_value_8, lower(p_key_9), p_value_9); 
   END sp_log_error;


   --****************************************************************************************
   PROCEDURE sp_log_fatal         (p_app_id                in number,
                                   p_user                  in nvarchar2,
                                   p_code_unit             in nvarchar2,
                                   p_function              in nvarchar2,
                                   p_short_msg             in nvarchar2,
                                   p_long_msg              in nvarchar2,
                                   p_context               in nvarchar2 default null,
                                   p_key_0                 in nvarchar2 default null,
                                   p_value_0               in nvarchar2 default null,
                                   p_key_1                 in nvarchar2 default null,
                                   p_value_1               in nvarchar2 default null,
                                   p_key_2                 in nvarchar2 default null,
                                   p_value_2               in nvarchar2 default null,
                                   p_key_3                 in nvarchar2 default null,
                                   p_value_3               in nvarchar2 default null,
                                   p_key_4                 in nvarchar2 default null,
                                   p_value_4               in nvarchar2 default null,
                                   p_key_5                 in nvarchar2 default null,
                                   p_value_5               in nvarchar2 default null,
                                   p_key_6                 in nvarchar2 default null,
                                   p_value_6               in nvarchar2 default null,
                                   p_key_7                 in nvarchar2 default null,
                                   p_value_7               in nvarchar2 default null,
                                   p_key_8                 in nvarchar2 default null,
                                   p_value_8               in nvarchar2 default null,
                                   p_key_9                 in nvarchar2 default null,
                                   p_value_9               in nvarchar2 default null)
   AS
   BEGIN
      mydb.pck_caravan_log.sp_log('fatal', lower(p_app_id), lower(p_user), lower(p_code_unit), lower(p_function), p_short_msg, p_long_msg,
                               lower(p_key_0), p_value_0, lower(p_key_1), p_value_1, lower(p_key_2), p_value_2, lower(p_key_3), p_value_3, lower(p_key_4), p_value_4,
                               lower(p_key_5), p_value_5, lower(p_key_6), p_value_6, lower(p_key_7), p_value_7, lower(p_key_8), p_value_8, lower(p_key_9), p_value_9); 
   END sp_log_fatal;


   --****************************************************************************************
   PROCEDURE sp_log               (p_type                  in nvarchar2,
                                   p_app_id                in number,
                                   p_user                  in nvarchar2,
                                   p_code_unit             in nvarchar2,
                                   p_function              in nvarchar2,
                                   p_short_msg             in nvarchar2,
                                   p_long_msg              in nvarchar2,
                                   p_context               in nvarchar2 default null,
                                   p_key_0                 in nvarchar2 default null,
                                   p_value_0               in nvarchar2 default null,
                                   p_key_1                 in nvarchar2 default null,
                                   p_value_1               in nvarchar2 default null,
                                   p_key_2                 in nvarchar2 default null,
                                   p_value_2               in nvarchar2 default null,
                                   p_key_3                 in nvarchar2 default null,
                                   p_value_3               in nvarchar2 default null,
                                   p_key_4                 in nvarchar2 default null,
                                   p_value_4               in nvarchar2 default null,
                                   p_key_5                 in nvarchar2 default null,
                                   p_value_5               in nvarchar2 default null,
                                   p_key_6                 in nvarchar2 default null,
                                   p_value_6               in nvarchar2 default null,
                                   p_key_7                 in nvarchar2 default null,
                                   p_value_7               in nvarchar2 default null,
                                   p_key_8                 in nvarchar2 default null,
                                   p_value_8               in nvarchar2 default null,
                                   p_key_9                 in nvarchar2 default null,
                                   p_value_9               in nvarchar2 default null)
   AS
      PRAGMA AUTONOMOUS_TRANSACTION;

      v_enabled     NUMBER(1);
      v_days        NUMBER(3);
      v_max_entries NUMBER(5);
      v_entry_count NUMBER(5);
      v_max_id      NUMBER;
   BEGIN
      
      SELECT clos_enabled, clos_days, clos_max_entries
        INTO v_enabled, v_days, v_max_entries
        FROM mydb.caravan_log_settings
       WHERE clos_type = lower(p_type)
         AND capp_id = p_app_id;

      BEGIN
         -- We delete logs older than "clos_days"
         DELETE 
           FROM mydb.caravan_log 
          WHERE clog_date < SYSDATE - v_days
            AND clos_type = lower(p_type)
            AND capp_id = p_app_id;
         
         -- We delete enough entries to preserve the upper limit
         SELECT COUNT(*) INTO v_entry_count
           FROM mydb.caravan_log 
          WHERE clos_type = lower(p_type)
            AND capp_id = p_app_id;
         
         IF v_entry_count >= v_max_entries THEN
            DELETE
              FROM mydb.caravan_log
             WHERE clos_type = lower(p_type)
               AND capp_id = p_app_id
               AND clog_id IN (SELECT clog_id 
                                 FROM (SELECT f.clog_id 
                                         FROM mydb.caravan_log f 
                                        WHERE lower(f.clos_type) = lower(p_type)
                                          AND lower(f.capp_id) = lower(p_app_id)
                                        ORDER BY f.clog_date ASC)
                                WHERE rownum <= (v_entry_count - v_max_entries + 1));
         END IF;
      EXCEPTION 
         WHEN OTHERS THEN NULL; -- Errors are ignored
      END;
      
      -- If log is enabled, then we can insert a new entry
      IF v_enabled = 1 THEN
         SELECT NVL(MAX(clog_id) + 1, 0) 
           INTO v_max_id
           FROM mydb.caravan_log;

         INSERT INTO mydb.caravan_log(clog_id, clog_date, clos_type, capp_id, clog_user, clog_code_unit, clog_function, 
                                   clog_short_msg, clog_long_msg, clog_context, 
                                   clog_key_0, clog_value_0, clog_key_1, clog_value_1, clog_key_2, clog_value_2, clog_key_3, clog_value_3, clog_key_4, clog_value_4,
                                   clog_key_5, clog_value_5, clog_key_6, clog_value_6, clog_key_7, clog_value_7, clog_key_8, clog_value_8, clog_key_9, clog_value_9)
                           VALUES (v_max_id, sysdate, lower(p_type), p_app_id, lower(p_user), lower(p_code_unit), lower(p_function), 
                                   p_short_msg, p_long_msg, p_context,
                                   lower(p_key_0), p_value_0, lower(p_key_1), p_value_1, lower(p_key_2), p_value_2, lower(p_key_3), p_value_3, lower(p_key_4), p_value_4,
                                   lower(p_key_5), p_value_5, lower(p_key_6), p_value_6, lower(p_key_7), p_value_7, lower(p_key_8), p_value_8, lower(p_key_9), p_value_9);         
      END IF;
      
      -- Leave at the end of procedure
      COMMIT;
   END sp_log;

END pck_caravan_log;