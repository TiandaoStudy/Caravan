-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_cli_idpr_restrictions
BEFORE INSERT ON mydb.crvn_idn_cli_idpr_restrictions 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_idn_cli_idpr_restrictions_id.nextval
    INTO :new.CCPR_ID
    FROM DUAL;
END;
/