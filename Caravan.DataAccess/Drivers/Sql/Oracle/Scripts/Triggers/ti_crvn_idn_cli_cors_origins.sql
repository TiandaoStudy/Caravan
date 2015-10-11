-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_cli_cors_origins
BEFORE INSERT ON mydb.crvn_idn_cli_cors_origins 
FOR EACH ROW
BEGIN
  SELECT mydb.sq_crvn_idn_cli_cors_origins.nextval
    INTO :new.CCCO_ID
    FROM DUAL;
END;
/