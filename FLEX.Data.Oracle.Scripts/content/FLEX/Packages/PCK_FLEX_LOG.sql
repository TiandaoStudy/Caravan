create or replace
package mydb.pck_flex_log as

   procedure sp_log_info          (p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_arg_0                 in varchar2 default null,
                                   p_arg_1                 in varchar2 default null,
                                   p_arg_2                 in varchar2 default null,
                                   p_arg_3                 in varchar2 default null,
                                   p_arg_4                 in varchar2 default null,
                                   p_arg_5                 in varchar2 default null,
                                   p_arg_6                 in varchar2 default null,
                                   p_arg_7                 in varchar2 default null,
                                   p_arg_8                 in varchar2 default null,
                                   p_arg_9                 in varchar2 default null);

   procedure sp_log_debug         (p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_arg_0                 in varchar2 default null,
                                   p_arg_1                 in varchar2 default null,
                                   p_arg_2                 in varchar2 default null,
                                   p_arg_3                 in varchar2 default null,
                                   p_arg_4                 in varchar2 default null,
                                   p_arg_5                 in varchar2 default null,
                                   p_arg_6                 in varchar2 default null,
                                   p_arg_7                 in varchar2 default null,
                                   p_arg_8                 in varchar2 default null,
                                   p_arg_9                 in varchar2 default null);

   procedure sp_log               (p_type                  in varchar2,
                                   p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_arg_0                 in varchar2 default null,
                                   p_arg_1                 in varchar2 default null,
                                   p_arg_2                 in varchar2 default null,
                                   p_arg_3                 in varchar2 default null,
                                   p_arg_4                 in varchar2 default null,
                                   p_arg_5                 in varchar2 default null,
                                   p_arg_6                 in varchar2 default null,
                                   p_arg_7                 in varchar2 default null,
                                   p_arg_8                 in varchar2 default null,
                                   p_arg_9                 in varchar2 default null);

end pck_flex_log;

/

create or replace
package body mydb.pck_flex_log as

   procedure sp_log_info          (p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_arg_0                 in varchar2 default null,
                                   p_arg_1                 in varchar2 default null,
                                   p_arg_2                 in varchar2 default null,
                                   p_arg_3                 in varchar2 default null,
                                   p_arg_4                 in varchar2 default null,
                                   p_arg_5                 in varchar2 default null,
                                   p_arg_6                 in varchar2 default null,
                                   p_arg_7                 in varchar2 default null,
                                   p_arg_8                 in varchar2 default null,
                                   p_arg_9                 in varchar2 default null)
   as
   begin
      
      mydb.pck_flex_log.sp_log('INFO', UPPER(p_application), UPPER(p_code_unit), UPPER(p_function), p_short_msg, p_long_msg,
         p_arg_0, p_arg_1, p_arg_2, p_arg_3, p_arg_4, p_arg_5, p_arg_6, p_arg_7, p_arg_8, p_arg_9);

   end sp_log_info;

   procedure sp_log_debug         (p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_arg_0                 in varchar2 default null,
                                   p_arg_1                 in varchar2 default null,
                                   p_arg_2                 in varchar2 default null,
                                   p_arg_3                 in varchar2 default null,
                                   p_arg_4                 in varchar2 default null,
                                   p_arg_5                 in varchar2 default null,
                                   p_arg_6                 in varchar2 default null,
                                   p_arg_7                 in varchar2 default null,
                                   p_arg_8                 in varchar2 default null,
                                   p_arg_9                 in varchar2 default null)
   as
   begin
      
      mydb.pck_flex_log.sp_log('DEBUG', UPPER(p_application), UPPER(p_code_unit), UPPER(p_function), p_short_msg, p_long_msg,
         p_arg_0, p_arg_1, p_arg_2, p_arg_3, p_arg_4, p_arg_5, p_arg_6, p_arg_7, p_arg_8, p_arg_9); 
   
   end sp_log_debug;

   procedure sp_log               (p_type                  in varchar2,
                                   p_application           in varchar2,
                                   p_code_unit             in varchar2,
                                   p_function              in varchar2,
                                   p_short_msg             in varchar2,
                                   p_long_msg              in varchar2,
                                   p_arg_0                 in varchar2 default null,
                                   p_arg_1                 in varchar2 default null,
                                   p_arg_2                 in varchar2 default null,
                                   p_arg_3                 in varchar2 default null,
                                   p_arg_4                 in varchar2 default null,
                                   p_arg_5                 in varchar2 default null,
                                   p_arg_6                 in varchar2 default null,
                                   p_arg_7                 in varchar2 default null,
                                   p_arg_8                 in varchar2 default null,
                                   p_arg_9                 in varchar2 default null)
   as
      pragma autonomous_transaction;
   begin
      -- We delete logs older than 3 months
      begin
	      delete from mydb.flex_log where flog_entry_date < sysdate - 90;
	   exception when others then null; -- Errors are ignored
	   end;

      insert into mydb.flex_log("FLOG_ID", "FLOG_ENTRY_DATE", "FLOG_TYPE", "FLOG_APPLICATION", "FLOG_CODE_UNIT", "FLOG_FUNCTION", "FLOG_SHORT_MSG",
                                "FLOG_LONG_MSG", "FLOG_ARG_0", "FLOG_ARG_1", "FLOG_ARG_2", "FLOG_ARG_3", "FLOG_ARG_4", "FLOG_ARG_5",
                                "FLOG_ARG_6", "FLOG_ARG_7", "FLOG_ARG_8", "FLOG_ARG_9")
      values (mydb.flex_log_seq.nextval, sysdate, p_type, p_application, p_code_unit, p_function, p_short_msg, p_long_msg,
              p_arg_0, p_arg_1, p_arg_2, p_arg_3, p_arg_4, p_arg_5, p_arg_6, p_arg_7, p_arg_8, p_arg_9); 

      commit;
   end sp_log;

end pck_flex_log;