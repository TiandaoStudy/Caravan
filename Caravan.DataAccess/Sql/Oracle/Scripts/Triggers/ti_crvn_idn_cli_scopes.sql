-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_cli_scopes
BEFORE INSERT ON mydb.crvn_idn_cli_scopes 
FOR EACH ROW
BEGIN
  SELECT mydb.sq_crvn_idn_cli_scopes.nextval
    INTO :new.CCSC_ID
    FROM DUAL;
END;
/