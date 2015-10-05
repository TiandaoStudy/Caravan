-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_cli_claims
BEFORE INSERT ON mydb.crvn_idn_cli_claims 
FOR EACH ROW
BEGIN
  SELECT mydb.sq_crvn_idn_cli_claims.nextval
    INTO :new.CCLM_ID
    FROM DUAL;
END;
/