-- REPLACE 'mydb' WITH DB NAME

create or replace
package mydb.pck_flex_log as

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

create or replace
package body mydb.pck_flex_log as

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
                                   p_value_9               in varchar2 default null)
   as
   begin
      
      mydb.pck_flex_log.sp_log('DEBUG', UPPER(p_application), UPPER(p_code_unit), UPPER(p_function), p_short_msg, p_long_msg,
         p_key_0, p_value_0, p_key_1, p_value_1, p_key_2, p_value_2, p_key_3, p_value_3, p_key_4, p_value_4,
         p_key_5, p_value_5, p_key_6, p_value_6, p_key_7, p_value_7, p_key_8, p_value_8, p_key_9, p_value_9); 
   
   end sp_log_debug;

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
                                   p_value_9               in varchar2 default null)
   as
   begin
      
      mydb.pck_flex_log.sp_log('INFO', UPPER(p_application), UPPER(p_code_unit), UPPER(p_function), p_short_msg, p_long_msg,
         p_key_0, p_value_0, p_key_1, p_value_1, p_key_2, p_value_2, p_key_3, p_value_3, p_key_4, p_value_4,
         p_key_5, p_value_5, p_key_6, p_value_6, p_key_7, p_value_7, p_key_8, p_value_8, p_key_9, p_value_9); 

   end sp_log_info;

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
                                   p_value_9               in varchar2 default null)
   as
   begin
      
      mydb.pck_flex_log.sp_log('WARNING', UPPER(p_application), UPPER(p_code_unit), UPPER(p_function), p_short_msg, p_long_msg,
         p_key_0, p_value_0, p_key_1, p_value_1, p_key_2, p_value_2, p_key_3, p_value_3, p_key_4, p_value_4,
         p_key_5, p_value_5, p_key_6, p_value_6, p_key_7, p_value_7, p_key_8, p_value_8, p_key_9, p_value_9); 

   end sp_log_warning;

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
                                   p_value_9               in varchar2 default null)
   as
   begin
      
      mydb.pck_flex_log.sp_log('ERROR', UPPER(p_application), UPPER(p_code_unit), UPPER(p_function), p_short_msg, p_long_msg,
         p_key_0, p_value_0, p_key_1, p_value_1, p_key_2, p_value_2, p_key_3, p_value_3, p_key_4, p_value_4,
         p_key_5, p_value_5, p_key_6, p_value_6, p_key_7, p_value_7, p_key_8, p_value_8, p_key_9, p_value_9); 

   end sp_log_error;

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
                                   p_value_9               in varchar2 default null)
   as
   begin
      
      mydb.pck_flex_log.sp_log('FATAL', UPPER(p_application), UPPER(p_code_unit), UPPER(p_function), p_short_msg, p_long_msg,
         p_key_0, p_value_0, p_key_1, p_value_1, p_key_2, p_value_2, p_key_3, p_value_3, p_key_4, p_value_4,
         p_key_5, p_value_5, p_key_6, p_value_6, p_key_7, p_value_7, p_key_8, p_value_8, p_key_9, p_value_9); 

   end sp_log_fatal;

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
                                   p_value_9               in varchar2 default null)
   as
      pragma autonomous_transaction;

      v_enabled    int;
      v_days       int;
   begin
      select flos_enabled, flos_days
        into v_enabled, v_days
        from userbase.flex_log_settings
       where flos_type = p_type;

      if v_enabled = 1 then

         -- We delete logs older than 3 months
         begin
	         delete from mydb.flex_log where flog_entry_date < sysdate - v_days;
	      exception when others then null; -- Errors are ignored
	      end;

         insert into mydb.flex_log("FLOG_ID", "FLOG_ENTRY_DATE", "FLOG_TYPE", "FLOG_APPLICATION", "FLOG_CODE_UNIT", "FLOG_FUNCTION", "FLOG_SHORT_MSG", "FLOG_LONG_MSG", "FLOG_CONTEXT", 
                                   "FLOG_KEY_0", "FLOG_VALUE_0", "FLOG_KEY_1", "FLOG_VALUE_1", "FLOG_KEY_2", "FLOG_VALUE_2", "FLOG_KEY_3", "FLOG_VALUE_3", "FLOG_KEY_4", "FLOG_VALUE_4",
                                   "FLOG_KEY_5", "FLOG_VALUE_5", "FLOG_KEY_6", "FLOG_VALUE_6", "FLOG_KEY_7", "FLOG_VALUE_7", "FLOG_KEY_8", "FLOG_VALUE_8", "FLOG_KEY_9", "FLOG_VALUE_9")
         values (mydb.flex_log_seq.nextval, sysdate, UPPER(p_type), UPPER(p_application), p_code_unit, p_function, p_short_msg, p_long_msg, p_context,
                 p_key_0, p_value_0, p_key_1, p_value_1, p_key_2, p_value_2, p_key_3, p_value_3, p_key_4, p_value_4,
                 p_key_5, p_value_5, p_key_6, p_value_6, p_key_7, p_value_7, p_key_8, p_value_8, p_key_9, p_value_9); 

         commit;

      end if;
   end sp_log;

end pck_flex_log;