-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_clients
BEFORE INSERT ON mydb.crvn_idn_clients 
FOR EACH ROW
BEGIN
  SELECT mydb.sq_crvn_idn_clients.nextval, mydb.pck_caravan_utils.f_get_sysdate_utc, mydb.pck_caravan_utils.f_get_sysuser, NULL, NULL
    INTO :new.CCLI_ID, :new.TRCK_INSERT_DATE, :new.TRCK_INSERT_DB_USER, :new.TRCK_UPDATE_DATE, :new.TRCK_UPDATE_DB_USER
    FROM DUAL;
END;
/