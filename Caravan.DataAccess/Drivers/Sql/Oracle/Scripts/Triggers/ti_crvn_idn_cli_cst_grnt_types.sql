-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_cli_cst_grnt_types
BEFORE INSERT ON mydb.crvn_idn_cli_cst_grnt_types 
FOR EACH ROW
BEGIN
  SELECT mydb.sq_crvn_idn_cli_cst_grnt_types.nextval
    INTO :new.CCGT_ID
    FROM DUAL;
END;
/