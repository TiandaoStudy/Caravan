-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_cli_cst_grnt_types
BEFORE INSERT ON mydb.crvn_idn_cli_cst_grnt_types 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_idn_cli_cst_grnt_types_id.nextval
    INTO :new.CCGT_ID
    FROM DUAL;
END;
/