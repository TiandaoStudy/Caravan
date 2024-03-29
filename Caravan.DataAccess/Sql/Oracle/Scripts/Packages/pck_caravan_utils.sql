﻿--------------------------------------------------------
--  DDL for Package PCK_CARAVAN_UTILS
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "CARAVAN"."PCK_CARAVAN_UTILS" AS 

  /* Current DB date and time in UTC */ 
  function f_get_sysdate_utc return date;
  
  /* Current DB user */
  function f_get_sysuser return nvarchar2;
  
  /* Error handling */
  procedure sp_err_when_updating_trck_cols;
  
END PCK_CARAVAN_UTILS;

/

--------------------------------------------------------
--  DDL for Package Body PCK_CARAVAN_UTILS
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "CARAVAN"."PCK_CARAVAN_UTILS" AS

  /* Current DB date and time in UTC */ 
  function f_get_sysdate_utc return date
  as
  begin
    return SYS_EXTRACT_UTC(SYSTIMESTAMP);
  end;
  
  /* Current DB user */
  function f_get_sysuser return nvarchar2
  as
  begin
    return LOWER(SYS_CONTEXT('USERENV', 'SESSION_USER'));
  end;
  
  /* Error handling */
  procedure sp_err_when_updating_trck_cols
  as
  begin
    RAISE_APPLICATION_ERROR(-20999, 'Tracking columns cannot be manually updated');
  end;

END PCK_CARAVAN_UTILS;

/
