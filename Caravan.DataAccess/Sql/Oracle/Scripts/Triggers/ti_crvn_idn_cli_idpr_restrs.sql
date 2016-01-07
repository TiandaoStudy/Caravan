-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_cli_idpr_restrs
BEFORE INSERT ON mydb.crvn_idn_cli_idpr_restrs 
FOR EACH ROW
BEGIN
  SELECT mydb.sq_crvn_idn_cli_idpr_restrs.nextval
    INTO :new.CCPR_ID
    FROM DUAL;
END;
/