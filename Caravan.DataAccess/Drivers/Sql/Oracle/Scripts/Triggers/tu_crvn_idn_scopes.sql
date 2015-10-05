﻿-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.tu_crvn_idn_scopes
BEFORE UPDATE ON mydb.crvn_idn_scopes 
FOR EACH ROW
BEGIN
  IF UPDATING('TRCK_INSERT_DATE') 
  OR UPDATING('TRCK_INSERT_DB_USER') 
  OR UPDATING('TRCK_UPDATE_DATE') 
  OR UPDATING('TRCK_UPDATE_DB_USER') 
  THEN
    RAISE_APPLICATION_ERROR(-20999, 'Tracking columns cannot be manually updated');
  END IF;

  SELECT mydb.sq_crvn_idn_scopes.nextval, mydb.pck_caravan_utils.f_get_sysdate_utc, mydb.pck_caravan_utils.f_get_sysuser
    INTO :new.CSCO_ID, :new.TRCK_UPDATE_DATE, :new.TRCK_UPDATE_DB_USER
    FROM DUAL;
END;
/