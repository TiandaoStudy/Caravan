-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_cli_secrets
BEFORE INSERT ON mydb.crvn_idn_cli_secrets 
FOR EACH ROW
BEGIN
  SELECT mydb.sq_crvn_idn_cli_secrets.nextval
    INTO :new.CCSE_ID
    FROM DUAL;
END;
/