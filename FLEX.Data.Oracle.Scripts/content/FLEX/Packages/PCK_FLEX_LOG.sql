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
                                   p_arg_9                 in varchar2 default null);

end pck_flex_log;