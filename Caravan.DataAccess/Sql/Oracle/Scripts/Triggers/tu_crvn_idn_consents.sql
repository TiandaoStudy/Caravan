-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.tu_crvn_idn_consents
BEFORE UPDATE ON mydb.crvn_idn_consents 
FOR EACH ROW
BEGIN
  IF UPDATING('TRCK_INSERT_DATE') 
  OR UPDATING('TRCK_INSERT_DB_USER') 
  OR UPDATING('TRCK_UPDATE_DATE') 
  OR UPDATING('TRCK_UPDATE_DB_USER') 
  THEN
    mydb.pck_caravan_utils.sp_err_when_updating_trck_cols;
  END IF;

  SELECT mydb.pck_caravan_utils.f_get_sysdate_utc, mydb.pck_caravan_utils.f_get_sysuser
    INTO :new.TRCK_UPDATE_DATE, :new.TRCK_UPDATE_DB_USER
    FROM DUAL;
END;
/